namespace rec FableUntypedObjectRequestBody

open Browser.Types
open Fable.SimpleHttp
open FableUntypedObjectRequestBody.Types
open FableUntypedObjectRequestBody.Http

///Regression fixture for #33: inline request bodies that declare `properties` without `type: "object"` must generate a Payload record (or list of records), not `string`.
type FableUntypedObjectRequestBodyClient(url: string, headers: list<Header>) =
    new(url: string) = FableUntypedObjectRequestBodyClient(url, [])

    ///<summary>
    ///Create a subscriber
    ///</summary>
    member this.CreateSubscriber(body: CreateSubscriberPayload) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync url "/subscribers" headers requestParts
            return CreateSubscriber.Created
        }

    ///<summary>
    ///Create many subscribers
    ///</summary>
    member this.CreateSubscribersBulk(body: CreateSubscribersBulkPayload) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync url "/subscribers/bulk" headers requestParts
            return CreateSubscribersBulk.Created
        }
