namespace rec PasswordConnect

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open PasswordConnect.Types
open PasswordConnect.Http

///REST API interface for 1Password Connect.
type PasswordConnectClient(httpClient: HttpClient) =
    ///<summary>
    ///Retrieve a list of API Requests that have been made.
    ///</summary>
    ///<param name="limit">How many API Events should be retrieved in a single request.</param>
    ///<param name="offset">How far into the collection of API Events should the response start</param>
    ///<param name="cancellationToken"></param>
    member this.GetApiActivity(?limit: int, ?offset: int, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if offset.IsSome then
                      RequestPart.query ("offset", offset.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/activity" requestParts cancellationToken

            match int status with
            | 200 -> return GetApiActivity.OK(Serializer.deserialize content)
            | _ -> return GetApiActivity.Unauthorized(Serializer.deserialize content)
        }

    ///<summary>
    ///Get state of the server and its dependencies.
    ///</summary>
    member this.GetServerHealth(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/health" requestParts cancellationToken
            return GetServerHealth.OK(Serializer.deserialize content)
        }

    ///<summary>
    ///Ping the server for liveness
    ///</summary>
    member this.GetHeartbeat(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/heartbeat" requestParts cancellationToken
            return GetHeartbeat.OK content
        }

    ///<summary>
    ///See Prometheus documentation for a complete data model.
    ///</summary>
    member this.GetPrometheusMetrics(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/metrics" requestParts cancellationToken
            return GetPrometheusMetrics.OK content
        }

    ///<summary>
    ///Get all Vaults
    ///</summary>
    ///<param name="filter">Filter the Vault collection based on Vault name using SCIM eq filter</param>
    ///<param name="cancellationToken"></param>
    member this.GetVaults(?filter: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if filter.IsSome then
                      RequestPart.query ("filter", filter.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/vaults" requestParts cancellationToken

            match int status with
            | 200 -> return GetVaults.OK(Serializer.deserialize content)
            | _ -> return GetVaults.Unauthorized(Serializer.deserialize content)
        }

    ///<summary>
    ///Get Vault details and metadata
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to fetch Items from</param>
    ///<param name="cancellationToken"></param>
    member this.GetVaultById(vaultUuid: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/vaults/{vaultUuid}" requestParts cancellationToken

            match int status with
            | 200 -> return GetVaultById.OK(Serializer.deserialize content)
            | 401 -> return GetVaultById.Unauthorized(Serializer.deserialize content)
            | 403 -> return GetVaultById.Forbidden(Serializer.deserialize content)
            | _ -> return GetVaultById.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Get all items for inside a Vault
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to fetch Items from</param>
    ///<param name="filter">Filter the Item collection based on Item name using SCIM eq filter</param>
    ///<param name="cancellationToken"></param>
    member this.GetVaultItems(vaultUuid: string, ?filter: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  if filter.IsSome then
                      RequestPart.query ("filter", filter.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/vaults/{vaultUuid}/items" requestParts cancellationToken

            match int status with
            | 200 -> return GetVaultItems.OK(Serializer.deserialize content)
            | 401 -> return GetVaultItems.Unauthorized(Serializer.deserialize content)
            | _ -> return GetVaultItems.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Create a new Item
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to create an Item in</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.CreateVaultItem(vaultUuid: string, ?cancellationToken: CancellationToken, ?body: FullItem) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/vaults/{vaultUuid}/items" requestParts cancellationToken

            match int status with
            | 200 -> return CreateVaultItem.OK(Serializer.deserialize content)
            | 400 -> return CreateVaultItem.BadRequest(Serializer.deserialize content)
            | 401 -> return CreateVaultItem.Unauthorized(Serializer.deserialize content)
            | 403 -> return CreateVaultItem.Forbidden(Serializer.deserialize content)
            | _ -> return CreateVaultItem.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Delete an Item
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault the item is in</param>
    ///<param name="itemUuid">The UUID of the Item to update</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteVaultItem(vaultUuid: string, itemUuid: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/vaults/{vaultUuid}/items/{itemUuid}" requestParts cancellationToken

            match int status with
            | 204 -> return DeleteVaultItem.NoContent
            | 401 -> return DeleteVaultItem.Unauthorized(Serializer.deserialize content)
            | 403 -> return DeleteVaultItem.Forbidden(Serializer.deserialize content)
            | _ -> return DeleteVaultItem.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the details of an Item
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to fetch Item from</param>
    ///<param name="itemUuid">The UUID of the Item to fetch</param>
    ///<param name="cancellationToken"></param>
    member this.GetVaultItemById(vaultUuid: string, itemUuid: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/vaults/{vaultUuid}/items/{itemUuid}" requestParts cancellationToken

            match int status with
            | 200 -> return GetVaultItemById.OK(Serializer.deserialize content)
            | 401 -> return GetVaultItemById.Unauthorized(Serializer.deserialize content)
            | 403 -> return GetVaultItemById.Forbidden(Serializer.deserialize content)
            | _ -> return GetVaultItemById.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Applies a modified [RFC6902 JSON Patch](https://tools.ietf.org/html/rfc6902) document to an Item or ItemField. This endpoint only supports `add`, `remove` and `replace` operations.
    ///When modifying a specific ItemField, the ItemField's ID in the `path` attribute of the operation object: `/fields/{fieldId}`
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault the item is in</param>
    ///<param name="itemUuid">The UUID of the Item to update</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PatchVaultItem
        (
            vaultUuid: string,
            itemUuid: string,
            ?cancellationToken: CancellationToken,
            ?body: Patch
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/vaults/{vaultUuid}/items/{itemUuid}" requestParts cancellationToken

            match int status with
            | 200 -> return PatchVaultItem.OK(Serializer.deserialize content)
            | 401 -> return PatchVaultItem.Unauthorized(Serializer.deserialize content)
            | 403 -> return PatchVaultItem.Forbidden(Serializer.deserialize content)
            | _ -> return PatchVaultItem.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Update an Item
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Item's Vault</param>
    ///<param name="itemUuid">The UUID of the Item to update</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.UpdateVaultItem
        (
            vaultUuid: string,
            itemUuid: string,
            ?cancellationToken: CancellationToken,
            ?body: FullItem
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/vaults/{vaultUuid}/items/{itemUuid}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateVaultItem.OK(Serializer.deserialize content)
            | 400 -> return UpdateVaultItem.BadRequest(Serializer.deserialize content)
            | 401 -> return UpdateVaultItem.Unauthorized(Serializer.deserialize content)
            | 403 -> return UpdateVaultItem.Forbidden(Serializer.deserialize content)
            | _ -> return UpdateVaultItem.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Get all the files inside an Item
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to fetch Items from</param>
    ///<param name="itemUuid">The UUID of the Item to fetch files from</param>
    ///<param name="inlineFiles">Tells server to return the base64-encoded file contents in the response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetItemFiles
        (
            vaultUuid: System.Guid,
            itemUuid: System.Guid,
            ?inlineFiles: bool,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid)
                  if inlineFiles.IsSome then
                      RequestPart.query ("inline_files", inlineFiles.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/vaults/{vaultUuid}/items/{itemUuid}/files"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetItemFiles.OK(Serializer.deserialize content)
            | 401 -> return GetItemFiles.Unauthorized(Serializer.deserialize content)
            | _ -> return GetItemFiles.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the details of a File
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault to fetch Item from</param>
    ///<param name="itemUuid">The UUID of the Item to fetch File from</param>
    ///<param name="fileUuid">The UUID of the File to fetch</param>
    ///<param name="inlineFiles">Tells server to return the base64-encoded file contents in the response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetDetailsOfFileById
        (
            vaultUuid: System.Guid,
            itemUuid: System.Guid,
            fileUuid: System.Guid,
            ?inlineFiles: bool,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid)
                  RequestPart.path ("fileUuid", fileUuid)
                  if inlineFiles.IsSome then
                      RequestPart.query ("inline_files", inlineFiles.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/vaults/{vaultUuid}/items/{itemUuid}/files/{fileUuid}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetDetailsOfFileById.OK(Serializer.deserialize content)
            | 401 -> return GetDetailsOfFileById.Unauthorized(Serializer.deserialize content)
            | 403 -> return GetDetailsOfFileById.Forbidden(Serializer.deserialize content)
            | _ -> return GetDetailsOfFileById.NotFound(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the content of a File
    ///</summary>
    ///<param name="vaultUuid">The UUID of the Vault the item is in</param>
    ///<param name="itemUuid">The UUID of the Item the File is in</param>
    ///<param name="fileUuid">UUID of the file to get content from</param>
    ///<param name="cancellationToken"></param>
    member this.DownloadFileByID
        (
            vaultUuid: System.Guid,
            itemUuid: System.Guid,
            fileUuid: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("vaultUuid", vaultUuid)
                  RequestPart.path ("itemUuid", itemUuid)
                  RequestPart.path ("fileUuid", fileUuid) ]

            let! (status, contentBinary) =
                OpenApiHttp.getBinaryAsync
                    httpClient
                    "/vaults/{vaultUuid}/items/{itemUuid}/files/{fileUuid}/content"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return DownloadFileByID.OK contentBinary
            | 401 ->
                let content = Encoding.UTF8.GetString contentBinary
                return DownloadFileByID.Unauthorized(Serializer.deserialize content)
            | _ ->
                let content = Encoding.UTF8.GetString contentBinary
                return DownloadFileByID.NotFound(Serializer.deserialize content)
        }
