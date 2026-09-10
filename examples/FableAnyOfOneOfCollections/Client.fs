namespace rec FableAnyOfOneOfCollections

open Browser.Types
open Fable.SimpleHttp
open FableAnyOfOneOfCollections.Types
open FableAnyOfOneOfCollections.Http

///Regression fixture for PR 13: array `items` and `additionalProperties` schemas that are themselves multi-element oneOf/anyOf must generate (or safely fall back from) a discriminated union, not a reference to an undeclared type. Also covers inline-object variants (resolved into nested records) and the fallback path when a variant is an unsupported schema, top-level oneOf schemas that become module-level DUs or type abbreviations, required collections of unions, and union case-name deduplication/merging.
type FableAnyOfOneOfCollectionsClient(url: string, headers: list<Header>) =
    new(url: string) = FableAnyOfOneOfCollectionsClient(url, [])

    ///<summary>
    ///Get a basket
    ///</summary>
    member this.GetBasket(id: string) =
        async {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.getAsync url "/baskets/{id}" headers requestParts
            return GetBasket.OK(Serializer.deserialize content)
        }
