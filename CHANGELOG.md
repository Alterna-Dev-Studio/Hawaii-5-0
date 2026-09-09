# Changelog

## v0.71.1

### Bug Fixes
Schemas that declare `properties` without an explicit `type: "object"` are object-shaped per JSON Schema, but several code paths only recognised an explicit `type`. All of them now share one `isObjectSchema` predicate. Consumers should regenerate their clients.
- Response payloads with an untyped object schema were typed as `string`, so the generated client threw `JsonException` on the success path at runtime ([#31](https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues/31)).
- Record properties that were arrays of untyped inline objects were generated as `list<string>` and threw at runtime ([#32](https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues/32)).
- Untyped inline request bodies were typed as `string` (or `list<string>`) instead of a `Payload` record, so the client sent a JSON string literal ([#33](https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues/33)).
- Untyped object schemas in nested record properties, top-level array components and `multipart/form-data` bodies now keep their structure ([#34](https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues/34)).
  - **Visible change**: nested fields that were previously `System.Text.Json.JsonElement` (or `obj` on Fable) are now typed nested records, and some generated nested type names change because more records participate in collision resolution. Multipart operations whose form schema was untyped gain their previously-missing parameters.
- Regression fixtures for each case were added to the `generate-and-build` suite.

## v0.71.0

### Bug Fixes
- Task-mode clients (`asyncReturnType: "task"`) no longer depend on the unmaintained **Ply** package -- they now use FSharp.Core's built-in `task { }` computation expression. Ply's builder is no longer inlined by the F# compiler shipped in .NET SDK 10.0.400+, which made every generated task-mode client throw `NotSupportedException: Dynamic invocation of Bind is not supported` at runtime ([#20](https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues/20)).
  - Regenerated task-mode projects no longer emit `open FSharp.Control.Tasks` or a `Ply` PackageReference. Consumers should regenerate their clients and drop Ply from their own package manifests.

## v0.70.0

### Breaking Changes
- **Newtonsoft.Json removed** -- now uses System.Text.Json throughout
- **Requires .NET 10 SDK**

### New Features
- `fsharp-native` target option (uses `task {}` instead of `async {}`)
- `StringEnum` JSON converter for enum serialization
- `text/plain` requestBody support
- Recursive config merge and case-insensitive config keys

### Bug Fixes
- Null-ref guards for edge-case schemas (e.g. Cloudflare spec)
- Identifier sanitization for digit-start and special-char names
- `requestBody.Required` now respected for multipart/form-data and JSON bodies
- Operation name normalization for spaces and nullable required fields
