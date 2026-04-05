namespace rec AuthentiqApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open AuthentiqApi.Types
open AuthentiqApi.Http

///Strong authentication, without the passwords.
type AuthentiqApiClient(httpClient: HttpClient) =
    ///<summary>
    ///Revoke an Authentiq ID using email &amp; phone.
    ///If called with `email` and `phone` only, a verification code
    ///will be sent by email. Do a second call adding `code` to
    ///complete the revocation.
    ///</summary>
    ///<param name="email">primary email associated to Key (ID)</param>
    ///<param name="phone">primary phone number, international representation</param>
    ///<param name="code">verification code sent by email</param>
    ///<param name="cancellationToken"></param>
    member this.KeyRevokeNosecret(email: string, phone: string, ?code: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.query ("email", email)
                  RequestPart.query ("phone", phone)
                  if code.IsSome then
                      RequestPart.query ("code", code.Value) ]

            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/key" requestParts cancellationToken

            match int status with
            | 200 -> return KeyRevokeNosecret.OK(Serializer.deserialize content)
            | 401 -> return KeyRevokeNosecret.Unauthorized(Serializer.deserialize content)
            | 404 -> return KeyRevokeNosecret.NotFound(Serializer.deserialize content)
            | 409 -> return KeyRevokeNosecret.Conflict(Serializer.deserialize content)
            | _ -> return KeyRevokeNosecret.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Register a new ID `JWT(sub, devtoken)`
    ///v5: `JWT(sub, pk, devtoken, ...)`
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    member this.KeyRegister(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.postAsync httpClient "/key" requestParts cancellationToken

            match int status with
            | 201 -> return KeyRegister.Created(Serializer.deserialize content)
            | 409 -> return KeyRegister.Conflict(Serializer.deserialize content)
            | _ -> return KeyRegister.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Revoke an Identity (Key) with a revocation secret
    ///</summary>
    ///<param name="pK">Public Signing Key - Authentiq ID (43 chars)</param>
    ///<param name="secret">revokation secret</param>
    ///<param name="cancellationToken"></param>
    member this.KeyRevoke(pK: string, secret: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("PK", pK)
                  RequestPart.query ("secret", secret) ]

            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/key/{PK}" requestParts cancellationToken

            match int status with
            | 200 -> return KeyRevoke.OK(Serializer.deserialize content)
            | 401 -> return KeyRevoke.Unauthorized(Serializer.deserialize content)
            | 404 -> return KeyRevoke.NotFound(Serializer.deserialize content)
            | _ -> return KeyRevoke.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get public details of an Authentiq ID.
    ///</summary>
    ///<param name="pK">Public Signing Key - Authentiq ID (43 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.KeyRetrieve(pK: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("PK", pK) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/key/{PK}" requestParts cancellationToken

            match int status with
            | 200 -> return KeyRetrieve.OK(Serializer.deserialize content)
            | 404 -> return KeyRetrieve.NotFound(Serializer.deserialize content)
            | _ -> return KeyRetrieve.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///HEAD info on Authentiq ID
    ///</summary>
    ///<param name="pK">Public Signing Key - Authentiq ID (43 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.HeadKeyByPK(pK: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("PK", pK) ]
            let! (status, content) = OpenApiHttp.headAsync httpClient "/key/{PK}" requestParts cancellationToken

            match int status with
            | 200 -> return HeadKeyByPK.OK
            | 404 -> return HeadKeyByPK.NotFound(Serializer.deserialize content)
            | _ -> return HeadKeyByPK.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///update properties of an Authentiq ID.
    ///(not operational in v4; use PUT for now)
    ///v5: POST issuer-signed email &amp; phone scopes in
    ///a self-signed JWT
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    ///<param name="pK">Public Signing Key - Authentiq ID (43 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.KeyUpdate(pK: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("PK", pK) ]
            let! (status, content) = OpenApiHttp.postAsync httpClient "/key/{PK}" requestParts cancellationToken

            match int status with
            | 200 -> return KeyUpdate.OK(Serializer.deserialize content)
            | 404 -> return KeyUpdate.NotFound(Serializer.deserialize content)
            | _ -> return KeyUpdate.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Update Authentiq ID by replacing the object.
    ///v4: `JWT(sub,email,phone)` to bind email/phone hash;
    ///v5: POST issuer-signed email &amp; phone scopes
    ///and PUT to update registration `JWT(sub, pk, devtoken, ...)`
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    ///<param name="pK">Public Signing Key - Authentiq ID (43 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.KeyBind(pK: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("PK", pK) ]
            let! (status, content) = OpenApiHttp.putAsync httpClient "/key/{PK}" requestParts cancellationToken

            match int status with
            | 200 -> return KeyBind.OK(Serializer.deserialize content)
            | 404 -> return KeyBind.NotFound(Serializer.deserialize content)
            | 409 -> return KeyBind.Conflict(Serializer.deserialize content)
            | _ -> return KeyBind.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///push sign-in request
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    ///<param name="callback">URI App will connect to</param>
    ///<param name="cancellationToken"></param>
    member this.PushLoginRequest(callback: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.query ("callback", callback) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/login" requestParts cancellationToken

            match int status with
            | 200 -> return PushLoginRequest.OK(Serializer.deserialize content)
            | 401 -> return PushLoginRequest.Unauthorized(Serializer.deserialize content)
            | _ -> return PushLoginRequest.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///scope verification request
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    ///<param name="test">test only mode, using test issuer</param>
    ///<param name="cancellationToken"></param>
    member this.SignRequest(?test: int, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if test.IsSome then
                      RequestPart.query ("test", test.Value) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/scope" requestParts cancellationToken

            match int status with
            | 201 -> return SignRequest.Created(Serializer.deserialize content)
            | _ -> return SignRequest.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///delete a verification job
    ///</summary>
    ///<param name="job">Job ID (20 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.SignDelete(job: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("job", job) ]
            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/scope/{job}" requestParts cancellationToken

            match int status with
            | 200 -> return SignDelete.OK(Serializer.deserialize content)
            | 404 -> return SignDelete.NotFound(Serializer.deserialize content)
            | _ -> return SignDelete.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///get the status / current content of a verification job
    ///</summary>
    ///<param name="job">Job ID (20 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.SignRetrieve(job: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("job", job) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/scope/{job}" requestParts cancellationToken

            match int status with
            | 200 -> return SignRetrieve.OK(Serializer.deserialize content)
            | 204 -> return SignRetrieve.NoContent
            | 404 -> return SignRetrieve.NotFound(Serializer.deserialize content)
            | _ -> return SignRetrieve.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///HEAD to get the status of a verification job
    ///</summary>
    ///<param name="job">Job ID (20 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.SignRetrieveHead(job: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("job", job) ]
            let! (status, content) = OpenApiHttp.headAsync httpClient "/scope/{job}" requestParts cancellationToken

            match int status with
            | 200 -> return SignRetrieveHead.OK
            | 204 -> return SignRetrieveHead.NoContent
            | 404 -> return SignRetrieveHead.NotFound(Serializer.deserialize content)
            | _ -> return SignRetrieveHead.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///this is a scope confirmation
    ///</summary>
    ///<param name="job">Job ID (20 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.SignConfirm(job: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("job", job) ]
            let! (status, content) = OpenApiHttp.postAsync httpClient "/scope/{job}" requestParts cancellationToken

            match int status with
            | 202 -> return SignConfirm.Accepted(Serializer.deserialize content)
            | 401 -> return SignConfirm.Unauthorized(Serializer.deserialize content)
            | 404 -> return SignConfirm.NotFound(Serializer.deserialize content)
            | 405 -> return SignConfirm.MethodNotAllowed(Serializer.deserialize content)
            | _ -> return SignConfirm.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///authority updates a JWT with its signature
    ///See: https://github.com/skion/authentiq/wiki/JWT-Examples
    ///</summary>
    ///<param name="job">Job ID (20 chars)</param>
    ///<param name="cancellationToken"></param>
    member this.SignUpdate(job: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("job", job) ]
            let! (status, content) = OpenApiHttp.putAsync httpClient "/scope/{job}" requestParts cancellationToken

            match int status with
            | 200 -> return SignUpdate.OK
            | 404 -> return SignUpdate.NotFound
            | 409 -> return SignUpdate.Conflict
            | _ -> return SignUpdate.DefaultResponse(Serializer.deserialize content)
        }
