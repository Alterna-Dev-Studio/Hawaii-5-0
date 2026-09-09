namespace rec TaskUntypedObjectNested

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskUntypedObjectNested.Types
open TaskUntypedObjectNested.Http

///Regression fixture for #34: untyped object schemas (properties without type) in nested record properties, top-level array components and multipart form bodies must keep their structure.
type TaskUntypedObjectNestedClient(httpClient: HttpClient) =
    ///<summary>
    ///Search profiles
    ///</summary>
    member this.SearchProfiles(?country: string, ?cancellationToken: CancellationToken) =
        task {
            let requestParts =
                [ if country.IsSome then
                      RequestPart.query ("country", country.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/profiles" requestParts cancellationToken
            return SearchProfiles.OK(Serializer.deserialize content)
        }

    ///<summary>
    ///Upload a profile picture
    ///</summary>
    member this.UploadProfilePicture(picture: string, profileId: string, ?cancellationToken: CancellationToken) =
        task {
            let requestParts =
                [ RequestPart.multipartFormData ("picture", picture)
                  RequestPart.multipartFormData ("profileId", profileId) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/profiles" requestParts cancellationToken
            return UploadProfilePicture.NoContent
        }
