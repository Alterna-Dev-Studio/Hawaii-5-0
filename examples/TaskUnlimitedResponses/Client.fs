namespace rec TaskUnlimitedResponses

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskUnlimitedResponses.Types
open TaskUnlimitedResponses.Http
open FSharp.Control.Tasks

///Spec containing paths with multiple responses
type TaskUnlimitedResponsesClient(httpClient: HttpClient) =
    ///<summary>
    ///Add an item
    ///</summary>
    member this.PostItems(?cancellationToken: CancellationToken, ?body: PostItemsPayload) =
        task {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/items" requestParts cancellationToken

            match int status with
            | 200 -> return PostItems.OK
            | 400 -> return PostItems.BadRequest
            | 401 -> return PostItems.Unauthorized
            | 403 -> return PostItems.Forbidden
            | 404 -> return PostItems.NotFound
            | 409 -> return PostItems.Conflict(Serializer.deserialize content)
            | _ -> return PostItems.InternalServerError
        }
