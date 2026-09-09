namespace rec FableUntypedObjectNested

open Browser.Types
open Fable.SimpleHttp
open FableUntypedObjectNested.Types
open FableUntypedObjectNested.Http

///Regression fixture for #34: untyped object schemas (properties without type) in nested record properties, top-level array components and multipart form bodies must keep their structure.
type FableUntypedObjectNestedClient(url: string, headers: list<Header>) =
    new(url: string) = FableUntypedObjectNestedClient(url, [])

    ///<summary>
    ///Search profiles
    ///</summary>
    member this.SearchProfiles(?country: string) =
        async {
            let requestParts =
                [ if country.IsSome then
                      RequestPart.query ("country", country.Value) ]

            let! (status, content) = OpenApiHttp.getAsync url "/profiles" headers requestParts
            return SearchProfiles.OK(Serializer.deserialize content)
        }

    ///<summary>
    ///Upload a profile picture
    ///</summary>
    member this.UploadProfilePicture(picture: string, profileId: string) =
        async {
            let requestParts =
                [ RequestPart.multipartFormData ("picture", picture)
                  RequestPart.multipartFormData ("profileId", profileId) ]

            let! (status, content) = OpenApiHttp.postAsync url "/profiles" headers requestParts
            return UploadProfilePicture.NoContent
        }
