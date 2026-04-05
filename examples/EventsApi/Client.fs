namespace rec EventsApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open EventsApi.Types
open EventsApi.Http

///1Password Events API Specification.
type EventsApiClient(httpClient: HttpClient) =
    ///<summary>
    ///Performs introspection of the provided Bearer JWT token
    ///</summary>
    member this.GetAuthIntrospect(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/api/auth/introspect" requestParts cancellationToken

            match int status with
            | 200 -> return GetAuthIntrospect.OK(Serializer.deserialize content)
            | 401 -> return GetAuthIntrospect.Unauthorized(Serializer.deserialize content)
            | _ -> return GetAuthIntrospect.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///This endpoint requires your JSON Web Token to have the *itemusages* feature.
    ///</summary>
    member this.GetItemUsages(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/api/v1/itemusages" requestParts cancellationToken

            match int status with
            | 200 -> return GetItemUsages.OK(Serializer.deserialize content)
            | 401 -> return GetItemUsages.Unauthorized(Serializer.deserialize content)
            | _ -> return GetItemUsages.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///This endpoint requires your JSON Web Token to have the *signinattempts* feature.
    ///</summary>
    member this.GetSignInAttempts(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/api/v1/signinattempts" requestParts cancellationToken

            match int status with
            | 200 -> return GetSignInAttempts.OK(Serializer.deserialize content)
            | 401 -> return GetSignInAttempts.Unauthorized(Serializer.deserialize content)
            | _ -> return GetSignInAttempts.DefaultResponse(Serializer.deserialize content)
        }
