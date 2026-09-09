namespace rec FableUntypedObjectResponse

open Browser.Types
open Fable.SimpleHttp
open FableUntypedObjectResponse.Types
open FableUntypedObjectResponse.Http

///Regression fixture for #31: a response schema that declares `properties` without `type: "object"` must generate a record payload, not `string`.
type FableUntypedObjectResponseClient(url: string, headers: list<Header>) =
    new(url: string) = FableUntypedObjectResponseClient(url, [])

    ///<summary>
    ///Trigger an event
    ///</summary>
    member this.EventsControllerTrigger(body: TriggerEventRequestDto) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync url "/v1/events/trigger" headers requestParts

            match int status with
            | 201 -> return EventsControllerTrigger.Created(Serializer.deserialize content)
            | _ -> return EventsControllerTrigger.BadRequest(Serializer.deserialize content)
        }
