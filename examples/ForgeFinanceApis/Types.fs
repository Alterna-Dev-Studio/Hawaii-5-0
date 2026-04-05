namespace rec ForgeFinanceApis.Types

[<RequireQualifiedAccess>]
type GetQuotes =
    ///A list of quotes
    | OK

[<RequireQualifiedAccess>]
type GetSymbols =
    ///A list of symbols
    | OK of payload: list<string>
