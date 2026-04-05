namespace rec ControlApiV1

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open ControlApiV1.Types
open ControlApiV1.Http

///Use the Control API to manage your applications, namespaces, keys, queues, rules, and more.
///Detailed information on using this API can be found in the Ably &amp;lt;a href="https://ably.com/documentation/control-api"&amp;gt;developer documentation&amp;lt;/a&amp;gt;.
///Control API is currently in Beta.
type ControlApiV1Client(httpClient: HttpClient) =
    ///<summary>
    ///List all applications for the specified account ID.
    ///</summary>
    ///<param name="accountId">The account ID for which to retrieve the associated applications.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAccountsAppsByAccountId(accountId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("account_id", accountId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{account_id}/apps" requestParts cancellationToken

            match int status with
            | 200 -> return GetAccountsAppsByAccountId.OK(Serializer.deserialize content)
            | 401 -> return GetAccountsAppsByAccountId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAccountsAppsByAccountId.NotFound(Serializer.deserialize content)
            | _ -> return GetAccountsAppsByAccountId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Creates an application with the specified properties.
    ///</summary>
    ///<param name="accountId">The account ID of the account in which to create the application.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PostAccountsAppsByAccountId(accountId: string, ?cancellationToken: CancellationToken, ?body: apppost) =
        async {
            let requestParts =
                [ RequestPart.path ("account_id", accountId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/accounts/{account_id}/apps" requestParts cancellationToken

            match int status with
            | 201 -> return PostAccountsAppsByAccountId.Created(Serializer.deserialize content)
            | 400 -> return PostAccountsAppsByAccountId.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAccountsAppsByAccountId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAccountsAppsByAccountId.NotFound(Serializer.deserialize content)
            | 422 -> return PostAccountsAppsByAccountId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PostAccountsAppsByAccountId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Lists the API keys associated with the application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAppsKeysByAppId(appId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("app_id", appId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/apps/{app_id}/keys" requestParts cancellationToken

            match int status with
            | 200 -> return GetAppsKeysByAppId.OK(Serializer.deserialize content)
            | 401 -> return GetAppsKeysByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAppsKeysByAppId.NotFound(Serializer.deserialize content)
            | _ -> return GetAppsKeysByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Creates an API key for the application specified.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PostAppsKeysByAppId(appId: string, ?cancellationToken: CancellationToken, ?body: keypost) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/{app_id}/keys" requestParts cancellationToken

            match int status with
            | 201 -> return PostAppsKeysByAppId.Created(Serializer.deserialize content)
            | 400 -> return PostAppsKeysByAppId.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAppsKeysByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsKeysByAppId.NotFound(Serializer.deserialize content)
            | 422 -> return PostAppsKeysByAppId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PostAppsKeysByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Update the API key with the specified key ID, for the application with the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="keyId">The API key ID.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PatchAppsKeysByAppIdAndKeyId
        (
            appId: string,
            keyId: string,
            ?cancellationToken: CancellationToken,
            ?body: keypatch
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("key_id", keyId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/apps/{app_id}/keys/{key_id}" requestParts cancellationToken

            match int status with
            | 200 -> return PatchAppsKeysByAppIdAndKeyId.OK(Serializer.deserialize content)
            | 400 -> return PatchAppsKeysByAppIdAndKeyId.BadRequest(Serializer.deserialize content)
            | 401 -> return PatchAppsKeysByAppIdAndKeyId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PatchAppsKeysByAppIdAndKeyId.NotFound(Serializer.deserialize content)
            | 422 -> return PatchAppsKeysByAppIdAndKeyId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PatchAppsKeysByAppIdAndKeyId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Revokes the API key with the specified ID, with the Application ID. This deletes the key.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="keyId">The key ID.</param>
    ///<param name="cancellationToken"></param>
    member this.PostAppsKeysRevokeByAppIdAndKeyId(appId: string, keyId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("key_id", keyId) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/{app_id}/keys/{key_id}/revoke" requestParts cancellationToken

            match int status with
            | 200 -> return PostAppsKeysRevokeByAppIdAndKeyId.OK
            | 401 -> return PostAppsKeysRevokeByAppIdAndKeyId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsKeysRevokeByAppIdAndKeyId.NotFound(Serializer.deserialize content)
            | _ -> return PostAppsKeysRevokeByAppIdAndKeyId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///List the namespaces for the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAppsNamespacesByAppId(appId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("app_id", appId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/apps/{app_id}/namespaces" requestParts cancellationToken

            match int status with
            | 200 -> return GetAppsNamespacesByAppId.OK(Serializer.deserialize content)
            | 401 -> return GetAppsNamespacesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAppsNamespacesByAppId.NotFound(Serializer.deserialize content)
            | _ -> return GetAppsNamespacesByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Creates a namespace for the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PostAppsNamespacesByAppId(appId: string, ?cancellationToken: CancellationToken, ?body: namespacepost) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/{app_id}/namespaces" requestParts cancellationToken

            match int status with
            | 201 -> return PostAppsNamespacesByAppId.Created(Serializer.deserialize content)
            | 400 -> return PostAppsNamespacesByAppId.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAppsNamespacesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsNamespacesByAppId.NotFound(Serializer.deserialize content)
            | 422 -> return PostAppsNamespacesByAppId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PostAppsNamespacesByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Deletes the namespace with the specified ID, for the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="namespaceId">The namespace ID.</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteAppsNamespacesByAppIdAndNamespaceId
        (
            appId: string,
            namespaceId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("namespace_id", namespaceId) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/apps/{app_id}/namespaces/{namespace_id}"
                    requestParts
                    cancellationToken

            match int status with
            | 204 -> return DeleteAppsNamespacesByAppIdAndNamespaceId.NoContent
            | 401 -> return DeleteAppsNamespacesByAppIdAndNamespaceId.Unauthorized(Serializer.deserialize content)
            | 404 -> return DeleteAppsNamespacesByAppIdAndNamespaceId.NotFound(Serializer.deserialize content)
            | _ -> return DeleteAppsNamespacesByAppIdAndNamespaceId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Updates the namespace with the specified ID, for the application with the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="namespaceId">The namespace ID.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PatchAppsNamespacesByAppIdAndNamespaceId
        (
            appId: string,
            namespaceId: string,
            ?cancellationToken: CancellationToken,
            ?body: namespacepatch
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("namespace_id", namespaceId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.patchAsync
                    httpClient
                    "/apps/{app_id}/namespaces/{namespace_id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return PatchAppsNamespacesByAppIdAndNamespaceId.OK(Serializer.deserialize content)
            | 400 -> return PatchAppsNamespacesByAppIdAndNamespaceId.BadRequest(Serializer.deserialize content)
            | 401 -> return PatchAppsNamespacesByAppIdAndNamespaceId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PatchAppsNamespacesByAppIdAndNamespaceId.NotFound(Serializer.deserialize content)
            | _ -> return PatchAppsNamespacesByAppIdAndNamespaceId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Lists the queues associated with the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAppsQueuesByAppId(appId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("app_id", appId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/apps/{app_id}/queues" requestParts cancellationToken

            match int status with
            | 200 -> return GetAppsQueuesByAppId.OK(Serializer.deserialize content)
            | 401 -> return GetAppsQueuesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAppsQueuesByAppId.NotFound(Serializer.deserialize content)
            | 500 -> return GetAppsQueuesByAppId.InternalServerError(Serializer.deserialize content)
            | _ -> return GetAppsQueuesByAppId.ServiceUnavailable(Serializer.deserialize content)
        }

    ///<summary>
    ///Creates a queue for the application specified by application ID. The properties for the queue to be created are specified in the request body.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PostAppsQueuesByAppId(appId: string, ?cancellationToken: CancellationToken, ?body: queue) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/{app_id}/queues" requestParts cancellationToken

            match int status with
            | 201 -> return PostAppsQueuesByAppId.Created(Serializer.deserialize content)
            | 400 -> return PostAppsQueuesByAppId.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAppsQueuesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsQueuesByAppId.NotFound(Serializer.deserialize content)
            | 422 -> return PostAppsQueuesByAppId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PostAppsQueuesByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Delete the queue with the specified queue name, from the application with the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="queueId">The queue ID.</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteAppsQueuesByAppIdAndQueueId
        (
            appId: string,
            queueId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("queue_id", queueId) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/apps/{app_id}/queues/{queue_id}" requestParts cancellationToken

            match int status with
            | 204 -> return DeleteAppsQueuesByAppIdAndQueueId.NoContent
            | 400 -> return DeleteAppsQueuesByAppIdAndQueueId.BadRequest(Serializer.deserialize content)
            | 401 -> return DeleteAppsQueuesByAppIdAndQueueId.Unauthorized(Serializer.deserialize content)
            | 404 -> return DeleteAppsQueuesByAppIdAndQueueId.NotFound(Serializer.deserialize content)
            | 500 -> return DeleteAppsQueuesByAppIdAndQueueId.InternalServerError(Serializer.deserialize content)
            | _ -> return DeleteAppsQueuesByAppIdAndQueueId.ServiceUnavailable(Serializer.deserialize content)
        }

    ///<summary>
    ///Lists the rules for the application specified by the application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAppsRulesByAppId(appId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("app_id", appId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/apps/{app_id}/rules" requestParts cancellationToken

            match int status with
            | 200 -> return GetAppsRulesByAppId.OK(Serializer.deserialize content)
            | 401 -> return GetAppsRulesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAppsRulesByAppId.NotFound(Serializer.deserialize content)
            | _ -> return GetAppsRulesByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Creates a rule for the application with the specified application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="cancellationToken"></param>
    member this.PostAppsRulesByAppId(appId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("app_id", appId) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/{app_id}/rules" requestParts cancellationToken

            match int status with
            | 201 -> return PostAppsRulesByAppId.Created
            | 400 -> return PostAppsRulesByAppId.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAppsRulesByAppId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsRulesByAppId.NotFound(Serializer.deserialize content)
            | 422 -> return PostAppsRulesByAppId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PostAppsRulesByAppId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Deletes a Reactor rule
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="ruleId">The rule ID.</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteAppsRulesByAppIdAndRuleId(appId: string, ruleId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("rule_id", ruleId) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/apps/{app_id}/rules/{rule_id}" requestParts cancellationToken

            match int status with
            | 204 -> return DeleteAppsRulesByAppIdAndRuleId.NoContent
            | 401 -> return DeleteAppsRulesByAppIdAndRuleId.Unauthorized(Serializer.deserialize content)
            | 404 -> return DeleteAppsRulesByAppIdAndRuleId.NotFound(Serializer.deserialize content)
            | _ -> return DeleteAppsRulesByAppIdAndRuleId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Returns the rule specified by the rule ID, for the application specified by application ID.
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="ruleId">The rule ID.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAppsRulesByAppIdAndRuleId(appId: string, ruleId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("rule_id", ruleId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/apps/{app_id}/rules/{rule_id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetAppsRulesByAppIdAndRuleId.OK
            | 401 -> return GetAppsRulesByAppIdAndRuleId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAppsRulesByAppIdAndRuleId.NotFound(Serializer.deserialize content)
            | _ -> return GetAppsRulesByAppIdAndRuleId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Updates a Reactor rule
    ///</summary>
    ///<param name="appId">The application ID.</param>
    ///<param name="ruleId">The rule ID.</param>
    ///<param name="cancellationToken"></param>
    member this.PatchAppsRulesByAppIdAndRuleId(appId: string, ruleId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("app_id", appId)
                  RequestPart.path ("rule_id", ruleId) ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/apps/{app_id}/rules/{rule_id}" requestParts cancellationToken

            match int status with
            | 200 -> return PatchAppsRulesByAppIdAndRuleId.OK
            | 400 -> return PatchAppsRulesByAppIdAndRuleId.BadRequest(Serializer.deserialize content)
            | 401 -> return PatchAppsRulesByAppIdAndRuleId.Unauthorized(Serializer.deserialize content)
            | 404 -> return PatchAppsRulesByAppIdAndRuleId.NotFound(Serializer.deserialize content)
            | 422 -> return PatchAppsRulesByAppIdAndRuleId.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return PatchAppsRulesByAppIdAndRuleId.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Deletes the application with the specified application ID.
    ///</summary>
    ///<param name="id">The ID of the application to be deleted.</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteAppsById(id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/apps/{id}" requestParts cancellationToken

            match int status with
            | 204 -> return DeleteAppsById.NoContent
            | 401 -> return DeleteAppsById.Unauthorized(Serializer.deserialize content)
            | 404 -> return DeleteAppsById.NotFound(Serializer.deserialize content)
            | 422 -> return DeleteAppsById.UnprocessableEntity(Serializer.deserialize content)
            | _ -> return DeleteAppsById.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Updates the application with the specified application ID.
    ///</summary>
    ///<param name="id">The ID of application to be updated.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PatchAppsById(id: string, ?cancellationToken: CancellationToken, ?body: apppatch) =
        async {
            let requestParts =
                [ RequestPart.path ("id", id)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.patchAsync httpClient "/apps/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return PatchAppsById.OK(Serializer.deserialize content)
            | 400 -> return PatchAppsById.BadRequest(Serializer.deserialize content)
            | 401 -> return PatchAppsById.Unauthorized(Serializer.deserialize content)
            | 404 -> return PatchAppsById.NotFound(Serializer.deserialize content)
            | _ -> return PatchAppsById.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Updates the application's Apple Push Notification service (APNs) information.
    ///</summary>
    ///<param name="id">The application ID.</param>
    ///<param name="p12Pass">The password for the corresponding `.p12` file.</param>
    ///<param name="p12File">The `.p12` file containing the app's APNs information.</param>
    ///<param name="cancellationToken"></param>
    member this.PostAppsPkcs12ById
        (
            id: string,
            p12Pass: string,
            p12File: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("id", id)
                  RequestPart.multipartFormData ("p12Pass", p12Pass)
                  RequestPart.multipartFormData ("p12File", p12File) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/apps/{id}/pkcs12" requestParts cancellationToken

            match int status with
            | 200 -> return PostAppsPkcs12ById.OK(Serializer.deserialize content)
            | 400 -> return PostAppsPkcs12ById.BadRequest(Serializer.deserialize content)
            | 401 -> return PostAppsPkcs12ById.Unauthorized(Serializer.deserialize content)
            | 404 -> return PostAppsPkcs12ById.NotFound(Serializer.deserialize content)
            | _ -> return PostAppsPkcs12ById.InternalServerError(Serializer.deserialize content)
        }

    ///<summary>
    ///Get token details
    ///</summary>
    member this.GetMe(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/me" requestParts cancellationToken

            match int status with
            | 200 -> return GetMe.OK(Serializer.deserialize content)
            | 401 -> return GetMe.Unauthorized(Serializer.deserialize content)
            | _ -> return GetMe.InternalServerError(Serializer.deserialize content)
        }
