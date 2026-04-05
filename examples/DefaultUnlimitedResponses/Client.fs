namespace rec DefaultUnlimitedResponses

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open DefaultUnlimitedResponses.Types
open DefaultUnlimitedResponses.Http

///Spec containing paths with multiple responses
type DefaultUnlimitedResponsesClient(httpClient: HttpClient) =
    ///<summary>
    ///Add an item
    ///</summary>
    member this.PostItems(?cancellationToken: CancellationToken, ?body: PostItemsPayload) =
        async {
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
