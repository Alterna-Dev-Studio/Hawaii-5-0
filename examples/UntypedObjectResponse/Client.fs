namespace rec UntypedObjectResponse

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open UntypedObjectResponse.Types
open UntypedObjectResponse.Http

///Regression fixture for #31: a response schema that declares `properties` without `type: "object"` must generate a record payload, not `string`.
type UntypedObjectResponseClient(httpClient: HttpClient) =
    ///<summary>
    ///Trigger an event
    ///</summary>
    member this.EventsControllerTrigger(body: TriggerEventRequestDto, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/v1/events/trigger" requestParts cancellationToken

            match int status with
            | 201 -> return EventsControllerTrigger.Created(Serializer.deserialize content)
            | _ -> return EventsControllerTrigger.BadRequest(Serializer.deserialize content)
        }
