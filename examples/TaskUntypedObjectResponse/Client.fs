namespace rec TaskUntypedObjectResponse

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskUntypedObjectResponse.Types
open TaskUntypedObjectResponse.Http

///Regression fixture for #31: a response schema that declares `properties` without `type: "object"` must generate a record payload, not `string`.
type TaskUntypedObjectResponseClient(httpClient: HttpClient) =
    ///<summary>
    ///Trigger an event
    ///</summary>
    member this.EventsControllerTrigger(body: TriggerEventRequestDto, ?cancellationToken: CancellationToken) =
        task {
            let requestParts = [ RequestPart.jsonContent body ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/v1/events/trigger" requestParts cancellationToken

            match int status with
            | 201 -> return EventsControllerTrigger.Created(Serializer.deserialize content)
            | _ -> return EventsControllerTrigger.BadRequest(Serializer.deserialize content)
        }
