namespace rec ForgeFinanceApis

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open ForgeFinanceApis.Types
open ForgeFinanceApis.Http

///Stock and Forex Data and Realtime Quotes
type ForgeFinanceApisClient(httpClient: HttpClient) =
    ///<summary>
    ///Get quotes
    ///</summary>
    member this.GetQuotes(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/quotes" requestParts cancellationToken
            return GetQuotes.OK
        }

    ///<summary>
    ///Symbol List
    ///</summary>
    member this.GetSymbols(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/symbols" requestParts cancellationToken
            return GetSymbols.OK(Serializer.deserialize content)
        }
