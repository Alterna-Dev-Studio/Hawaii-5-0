namespace rec TaskUntypedObjectRequestBody

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskUntypedObjectRequestBody.Types
open TaskUntypedObjectRequestBody.Http

///Regression fixture for #33: inline request bodies that declare `properties` without `type: "object"` must generate a Payload record (or list of records), not `string`.
type TaskUntypedObjectRequestBodyClient(httpClient: HttpClient) =
    ///<summary>
    ///Create a subscriber
    ///</summary>
    member this.CreateSubscriber(body: CreateSubscriberPayload, ?cancellationToken: CancellationToken) =
        task {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync httpClient "/subscribers" requestParts cancellationToken
            return CreateSubscriber.Created
        }

    ///<summary>
    ///Create many subscribers
    ///</summary>
    member this.CreateSubscribersBulk(body: CreateSubscribersBulkPayload, ?cancellationToken: CancellationToken) =
        task {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync httpClient "/subscribers/bulk" requestParts cancellationToken
            return CreateSubscribersBulk.Created
        }
