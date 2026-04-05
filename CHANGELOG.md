# Changelog

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
