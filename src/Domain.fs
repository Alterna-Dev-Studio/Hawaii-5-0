[<AutoOpen>]
module Domain

open FSharp.Compiler.SyntaxTree
open System.Text.Json

[<RequireQualifiedAccess>]
type EmptyDefinitionResolution =
    | Ignore
    | GenerateFreeForm

[<RequireQualifiedAccess>]
/// <summary>Describes the compilation target</summary>
type Target =
    | FSharp
    | FSharpNative
    | Fable

let isFSharpTarget =
    function
    | Target.FSharp
    | Target.FSharpNative -> true
    | Target.Fable -> false

/// <summary>Describes the async return type of the functions of the generated clients</summary>
[<RequireQualifiedAccess>]
type AsyncReturnType =
    | Async
    | Task

[<RequireQualifiedAccess>]
type FactoryFunction =
    | Create
    | None

/// Controls how multi-element anyOf/oneOf schemas are represented in generated code
[<RequireQualifiedAccess>]
type UnionStrategy =
    /// Generate F# discriminated unions (default)
    | DiscriminatedUnion
    /// Fall back to JsonElement (pre-v0.80 behavior)
    | JsonElement

type CodegenConfig = {
    schema: string
    output: string
    target: Target
    project : string
    asyncReturnType: AsyncReturnType
    synchronous: bool
    resolveReferences: bool
    emptyDefinitions: EmptyDefinitionResolution
    overrideSchema: JsonElement option
    filterTags: string list
    odataSchema: bool
    unionStrategy: UnionStrategy
}

type OperationParameter = {
    parameterName: string
    parameterIdent: string
    required: bool
    parameterType: SynType
    docs : string
    location: string
    style: string
    properties: string list
}