namespace rec FableUnlimitedResponses

open Browser.Types
open Fable.SimpleHttp
open FableUnlimitedResponses.Types
open FableUnlimitedResponses.Http

///Spec containing paths with multiple responses
type FableUnlimitedResponsesClient(url: string, headers: list<Header>) =
    new(url: string) = FableUnlimitedResponsesClient(url, [])

    ///<summary>
    ///Add an item
    ///</summary>
    member this.PostItems(?body: PostItemsPayload) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync url "/items" headers requestParts

            match int status with
            | 200 -> return PostItems.OK
            | 400 -> return PostItems.BadRequest
            | 401 -> return PostItems.Unauthorized
            | 403 -> return PostItems.Forbidden
            | 404 -> return PostItems.NotFound
            | 409 -> return PostItems.Conflict(Serializer.deserialize content)
            | _ -> return PostItems.InternalServerError
        }
