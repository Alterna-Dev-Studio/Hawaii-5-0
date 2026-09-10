[<AutoOpen>]
module Helpers

open System
open Fantomas
open Microsoft.OpenApi.Models
open System.Linq

let inline isNotNull (x: 't) = not (isNull x)

let capitalize (input: string) =
    if String.IsNullOrWhiteSpace input
    then ""
    else input.First().ToString().ToUpper() + String.Join("", input.Skip(1))

let camelCase (input: string) =
    if String.IsNullOrWhiteSpace input
    then ""
    else input.First().ToString().ToLower() + String.Join("", input.Skip(1))

let normalizeFullCaps (input: string) =
    let fullCaps =
        input |> Seq.forall Char.IsUpper

    if fullCaps
    then input.ToLower()
    else input

let needsBackticks (identifier: string) =
    if String.IsNullOrWhiteSpace identifier then
        false
    else
        identifier.Contains "."
        || identifier.Contains "/"
        || identifier.Contains "@"
        || identifier.Contains "$"
        || identifier.Contains "-"
        || identifier.Contains " "
        || identifier.Contains "+"
        || identifier.Contains "*"
        || identifier.Contains "%"
        || identifier.Contains "#"
        || identifier.Contains "!"
        || Char.IsDigit identifier.[0]
        || match identifier with
           | "abstract" | "and" | "as" | "assert" | "base" | "begin" | "class"
           | "default" | "delegate" | "do" | "done" | "downcast" | "downto" | "elif"
           | "else" | "end" | "exception" | "extern" | "false" | "finally" | "for"
           | "fun" | "function" | "global" | "if" | "in" | "inherit" | "inline"
           | "interface" | "internal" | "lazy" | "let" | "match" | "member" | "module"
           | "mutable" | "namespace" | "new" | "not" | "null" | "of" | "open" | "or"
           | "override" | "private" | "public" | "rec" | "return" | "sig" | "static"
           | "struct" | "then" | "to" | "true" | "try" | "type" | "upcast" | "use"
           | "val" | "void" | "when" | "while" | "with" | "yield" -> true
           | _ -> false

let escapeIdentifier (identifier: string) =
    if String.IsNullOrWhiteSpace identifier then
        identifier
    elif identifier.StartsWith("``") && identifier.EndsWith("``") then
        identifier
    elif needsBackticks identifier then
        $"``{identifier}``"
    else
        identifier

let sanitizeParameterName (fieldName: string) =
    if String.IsNullOrWhiteSpace fieldName then
        fieldName
    else
        let mutable result = fieldName
        result <- result.TrimStart('$', '@', '.', '/', '-', ' ', '+', '*', '%', '#', '!')
        result <-
            result
                .Replace(".", "")
                .Replace("/", "")
                .Replace("-", "")
                .Replace("$", "")
                .Replace("@", "")
                .Replace(" ", "")
                .Replace("+", "")
                .Replace("*", "")
                .Replace("%", "")
                .Replace("#", "")
                .Replace("!", "")
        result <- camelCase result
        if String.IsNullOrWhiteSpace result then
            "_param"
        elif needsBackticks result then
            "_" + result
        else
            result

let sanitizeTypeName (typeName: string) =
    if String.IsNullOrWhiteSpace typeName then
        typeName
    else
        let mutable result = typeName
        let invalidTypeNameChars =
            [| '$'; '@'; '.'; '/'; '-'; '_'; '['; ']'; ' '; '\t'; '\r'; '\n'; '+'; '*'; '%'; '#'; '!'; '`' |]

        result <- result.TrimStart(invalidTypeNameChars)

        if result.Contains "`" then
            result <-
                match result.Split '`' with
                | [| name; _ |] -> name
                | _ -> result.Replace("`", "")

        result <-
            result
            |> Seq.filter (fun c -> not (Array.contains c invalidTypeNameChars))
            |> Array.ofSeq
            |> String
        if String.IsNullOrWhiteSpace result then
            "UnknownType"
        elif Char.IsLetter result.[0] || result.[0] = '_' then
            result
        else
            "T" + result

let invalidTitle (title: string) =
    String.IsNullOrWhiteSpace title
    || (title.Contains "Mediatype identifier" && title.Contains "application/")
    || (title.Split(' ').Length >= 1)

/// Derives the F# type name for a schema reference, preferring a valid
/// `schema.Title` over `Reference.Id` -- the same precedence used
/// throughout the generator wherever a reference type is named.
let referencedSchemaTypeName (schema: OpenApiSchema) =
    if invalidTitle schema.Title
    then sanitizeTypeName schema.Reference.Id
    else sanitizeTypeName schema.Title

let isEmptySchema (schema: OpenApiSchema) =
    isNull schema
    || (
        (isNull schema.Type || schema.Type = "object")
        && (isNull schema.Properties || schema.Properties.Count = 0)
        && (isNull schema.AllOf || schema.AllOf.Count = 0)
        && (isNull schema.AnyOf || schema.AnyOf.Count = 0)
        && (isNull schema.OneOf || schema.OneOf.Count = 0)
    )

/// JSON Schema treats a schema that declares `properties` without an explicit
/// `type` as object-shaped, so both forms must generate the same F# type.
let isObjectSchema (schema: OpenApiSchema) =
    isNotNull schema
    && (schema.Type = "object" || (isNull schema.Type && schema.Properties.Count > 0))
