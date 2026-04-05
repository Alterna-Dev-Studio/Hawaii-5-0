namespace rec SyncUnlimitedResponses

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open SyncUnlimitedResponses.Types
open SyncUnlimitedResponses.Http

///Spec containing paths with multiple responses
type SyncUnlimitedResponsesClient(httpClient: HttpClient) =
    ///<summary>
    ///Add an item
    ///</summary>
    member this.PostItems(?cancellationToken: CancellationToken, ?body: PostItemsPayload) =
        let requestParts =
            [ if body.IsSome then
                  RequestPart.jsonContent body.Value ]

        let (status, content) =
            OpenApiHttp.post httpClient "/items" requestParts cancellationToken

        match int status with
        | 200 -> PostItems.OK
        | 400 -> PostItems.BadRequest
        | 401 -> PostItems.Unauthorized
        | 403 -> PostItems.Forbidden
        | 404 -> PostItems.NotFound
        | 409 -> PostItems.Conflict(Serializer.deserialize content)
        | _ -> PostItems.InternalServerError
