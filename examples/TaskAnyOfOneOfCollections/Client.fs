namespace rec TaskAnyOfOneOfCollections

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskAnyOfOneOfCollections.Types
open TaskAnyOfOneOfCollections.Http

///Regression fixture for PR 13: array `items` and `additionalProperties` schemas that are themselves multi-element oneOf/anyOf must generate (or safely fall back from) a discriminated union, not a reference to an undeclared type. Also covers inline-object variants (resolved into nested records) and the fallback path when a variant is an unsupported schema, top-level oneOf schemas that become module-level DUs or type abbreviations, required collections of unions, and union case-name deduplication/merging.
type TaskAnyOfOneOfCollectionsClient(httpClient: HttpClient) =
    ///<summary>
    ///Get a basket
    ///</summary>
    member this.GetBasket(id: string, ?cancellationToken: CancellationToken) =
        task {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/baskets/{id}" requestParts cancellationToken
            return GetBasket.OK(Serializer.deserialize content)
        }
