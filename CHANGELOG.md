# Changelog

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
