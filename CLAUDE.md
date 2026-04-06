# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Hawaii-5-0 (NuGet: `Hawaii5O`, CLI: `hawaii5o`) is a .NET CLI tool that generates type-safe F# clients from OpenAPI/Swagger/OData schemas. It is a maintained fork of [Zaid-Ajaj/Hawaii](https://github.com/Zaid-Ajaj/Hawaii).

## Build Commands

All build commands run from `build/`:

```bash
cd build
dotnet run -- build              # Build in Release configuration
dotnet run -- pack               # Build, pack, and install locally as dotnet tool
dotnet run -- publish            # Pack and push to NuGet (requires NUGET_KEY env var)
dotnet run -- generate-and-build # Full test: regenerate all example projects and compile them
dotnet run -- integration        # Test against 10 live API schemas
dotnet run -- rate {n}           # Test against first n of ~2000 API Guru schemas
```

There is no unit test project. Testing is integration-only via `generate-and-build`, which exercises the full pipeline across all schema/target/async permutations.

## Architecture

### Code Generation Pipeline

```
hawaii.json config
  -> Program.fs: readConfig -> CodegenConfig record
  -> Program.fs: getSchema -> loads local/remote JSON, YAML, and XML (OData), merges overrideSchema
  -> Microsoft.OpenApi.Readers: parses into OpenApiDocument
  -> Program.fs: createGlobalTypesModule -> F# AST for Types.fs (records, enums, response DUs)
  -> Program.fs: createOpenApiClient -> F# AST for Client.fs (HTTP client class)
  -> CodeGen.fs: formatAst -> Fantomas formats AST into readable F# code
  -> Writes .fsproj, Types.fs, Client.fs, OpenApiHttp.fs, StringEnum.fs
```

### Key Source Files

- **`src/Program.fs`** (~3000 lines) -- The main codegen engine. Schema loading, type generation, client method generation, and CLI entry point are all here.
- **`src/Domain.fs`** -- Core types: `CodegenConfig`, `Target` (FSharp/FSharpNative/Fable), `AsyncReturnType`, `EmptyDefinitionResolution`.
- **`src/OperationParameters.fs`** -- Parses OpenAPI operation parameters into typed `OperationParameter` records. Handles query/path/header params, deep object expansion, and identifier deduplication.
- **`src/Helpers.fs`** -- String utilities: identifier sanitization, F# keyword escaping (backtick wrapping), camelCase/PascalCase conversion, and config file merging.
- **`src/HttpLibrary.fs`** -- Template source code that gets written verbatim into generated projects as `OpenApiHttp.fs`. Contains the runtime HTTP client helpers.
- **`src/Extensions.fs`** -- Extension methods on FsAst builders (`SynExpr.CreateAsync`, `SynExpr.CreateTask`, etc.).
- **`src/CodeGen.fs`** -- Fantomas wrapper. Formats F# compiler AST into source code strings.
- **`src/FsAst/`** -- Abstraction layer over `FSharp.Compiler.SyntaxTree`. `AstCreate.fs` has fluent builders; `AstRcd.fs` has record-based representations.

### Three Generation Targets

The `target` config option changes how code is generated:

- **`fsharp`** (default) -- Uses `System.Text.Json`, `Async<'T>` or `Task<'T>`, targets `netstandard2.0`
- **`fsharp-native`** -- Same as fsharp but uses `task {}` CE instead of `async {}`
- **`fable`** -- Uses `Fable.SimpleJson`/`Fable.SimpleHttp`, generates JS-compatible code

Use `Domain.isFSharpTarget` to check if target is FSharp or FSharpNative (both share most codegen logic).

### Generated Project Structure

Each run produces a complete F# project:
- `Types.fs` -- Schema types (records), enums with `[<StringEnum>]`, response discriminated unions per operation
- `Client.fs` -- `{Name}Client` class with one method per OpenAPI operation
- `OpenApiHttp.fs` -- Runtime HTTP helpers (copied from `src/HttpLibrary.fs`)
- `StringEnum.fs` -- Fable.Core StringEnum attribute (fsharp targets only)
- `{Name}.fsproj` -- Project file targeting `netstandard2.0`

### Build System

FAKE-based build script in `build/`. The build project targets `net6.0` (works via `rollForward: latestMajor` in `global.json`). Key targets are defined in `build/Program.fs`. `build/Swag.fs` contains the list of API Guru schemas used for integration testing.

## Plans

Implementation plans are tracked as GitHub Issues (or Discussions) with the `plan` label: https://github.com/Alterna-Dev-Studio/Hawaii-5-0/issues?q=label%3Aplan

When creating a new plan, file it as an issue with the `plan` label rather than storing it in the repo.

## Conventions

- The project uses `System.Text.Json` throughout (migrated from Newtonsoft.Json)
- Identifier sanitization is critical -- OpenAPI schemas have arbitrary string names that must become valid F# identifiers. See `Helpers.sanitizeTypeName` and `Helpers.sanitizeParameterName`.
- Response types are discriminated unions named after the operation (e.g., `FindPetsByStatus.OK`, `FindPetsByStatus.BadRequest`).
- Schema type name collisions are resolved by `findNextTypeName()` which appends incrementing numbers.
- The `overrideSchema` config allows users to patch/correct the source schema before codegen.
