namespace rec YamlPetStore

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open YamlPetStore.Types
open YamlPetStore.Http

///This is a sample Pet Store Server based on the OpenAPI 3.0 specification.  You can find out more about
///Swagger at [https://swagger.io](https://swagger.io). In the third iteration of the pet store, we've switched to the design first approach!
///You can now help us improve the API whether it's by making changes to the definition itself or to the code.
///That way, with time, we can improve the API in general, and expose some of the new features in OAS3.
///Some useful links:
///- [The Pet Store repository](https://github.com/swagger-api/swagger-petstore)
///- [The source API definition for the Pet Store](https://github.com/swagger-api/swagger-petstore/blob/master/src/main/resources/openapi.yaml)
type YamlPetStoreClient(httpClient: HttpClient) =
    ///<summary>
    ///Update an existing pet by Id.
    ///</summary>
    member this.UpdatePet(body: Pet, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.putAsync httpClient "/pet" requestParts cancellationToken

            match int status with
            | 200 -> return UpdatePet.OK(Serializer.deserialize content)
            | 400 -> return UpdatePet.BadRequest
            | 404 -> return UpdatePet.NotFound
            | 422 -> return UpdatePet.UnprocessableEntity
            | _ -> return UpdatePet.DefaultResponse
        }

    ///<summary>
    ///Add a new pet to the store.
    ///</summary>
    member this.AddPet(body: Pet, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync httpClient "/pet" requestParts cancellationToken

            match int status with
            | 200 -> return AddPet.OK(Serializer.deserialize content)
            | 400 -> return AddPet.BadRequest
            | 422 -> return AddPet.UnprocessableEntity
            | _ -> return AddPet.DefaultResponse
        }

    ///<summary>
    ///Multiple status values can be provided with comma separated strings.
    ///</summary>
    ///<param name="status">Status values that need to be considered for filter</param>
    ///<param name="cancellationToken"></param>
    member this.FindPetsByStatus(status: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.query ("status", status) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/pet/findByStatus" requestParts cancellationToken

            match int status with
            | 200 -> return FindPetsByStatus.OK(Serializer.deserialize content)
            | 400 -> return FindPetsByStatus.BadRequest
            | _ -> return FindPetsByStatus.DefaultResponse
        }

    ///<summary>
    ///Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.
    ///</summary>
    ///<param name="tags">Tags to filter by</param>
    ///<param name="cancellationToken"></param>
    member this.FindPetsByTags(tags: list<string>, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.query ("tags", tags) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/pet/findByTags" requestParts cancellationToken

            match int status with
            | 200 -> return FindPetsByTags.OK(Serializer.deserialize content)
            | 400 -> return FindPetsByTags.BadRequest
            | _ -> return FindPetsByTags.DefaultResponse
        }

    ///<summary>
    ///Returns a single pet.
    ///</summary>
    ///<param name="petId">ID of pet to return</param>
    ///<param name="cancellationToken"></param>
    member this.GetPetById(petId: int64, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("petId", petId) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/pet/{petId}" requestParts cancellationToken

            match int status with
            | 200 -> return GetPetById.OK(Serializer.deserialize content)
            | 400 -> return GetPetById.BadRequest
            | 404 -> return GetPetById.NotFound
            | _ -> return GetPetById.DefaultResponse
        }

    ///<summary>
    ///Updates a pet resource based on the form data.
    ///</summary>
    ///<param name="petId">ID of pet that needs to be updated</param>
    ///<param name="name">Name of pet that needs to be updated</param>
    ///<param name="status">Status of pet that needs to be updated</param>
    ///<param name="cancellationToken"></param>
    member this.UpdatePetWithForm(petId: int64, ?name: string, ?status: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if name.IsSome then
                      RequestPart.query ("name", name.Value)
                  if status.IsSome then
                      RequestPart.query ("status", status.Value) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/pet/{petId}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdatePetWithForm.OK(Serializer.deserialize content)
            | 400 -> return UpdatePetWithForm.BadRequest
            | _ -> return UpdatePetWithForm.DefaultResponse
        }

    ///<summary>
    ///Delete a pet.
    ///</summary>
    ///<param name="petId">Pet id to delete</param>
    ///<param name="apiKey"></param>
    ///<param name="cancellationToken"></param>
    member this.DeletePet(petId: int64, ?apiKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if apiKey.IsSome then
                      RequestPart.header ("api_key", apiKey.Value) ]

            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/pet/{petId}" requestParts cancellationToken

            match int status with
            | 200 -> return DeletePet.OK
            | 400 -> return DeletePet.BadRequest
            | _ -> return DeletePet.DefaultResponse
        }

    ///<summary>
    ///Upload image of the pet.
    ///</summary>
    ///<param name="petId">ID of pet to update</param>
    ///<param name="additionalMetadata">Additional Metadata</param>
    ///<param name="cancellationToken"></param>
    ///<param name="requestBody"></param>
    member this.UploadFile
        (
            petId: int64,
            ?additionalMetadata: string,
            ?cancellationToken: CancellationToken,
            ?requestBody: byte []
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if additionalMetadata.IsSome then
                      RequestPart.query ("additionalMetadata", additionalMetadata.Value)
                  if requestBody.IsSome then
                      RequestPart.binaryContent requestBody.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/pet/{petId}/uploadImage" requestParts cancellationToken

            match int status with
            | 200 -> return UploadFile.OK(Serializer.deserialize content)
            | 400 -> return UploadFile.BadRequest
            | 404 -> return UploadFile.NotFound
            | _ -> return UploadFile.DefaultResponse
        }

    ///<summary>
    ///Returns a map of status codes to quantities.
    ///</summary>
    member this.GetInventory(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/store/inventory" requestParts cancellationToken

            match int status with
            | 200 -> return GetInventory.OK(Serializer.deserialize content)
            | _ -> return GetInventory.DefaultResponse
        }

    ///<summary>
    ///Place a new order in the store.
    ///</summary>
    member this.PlaceOrder(?cancellationToken: CancellationToken, ?body: Order) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/store/order" requestParts cancellationToken

            match int status with
            | 200 -> return PlaceOrder.OK(Serializer.deserialize content)
            | 400 -> return PlaceOrder.BadRequest
            | 422 -> return PlaceOrder.UnprocessableEntity
            | _ -> return PlaceOrder.DefaultResponse
        }

    ///<summary>
    ///For valid response try integer IDs with value &amp;lt;= 5 or &amp;gt; 10. Other values will generate exceptions.
    ///</summary>
    ///<param name="orderId">ID of order that needs to be fetched</param>
    ///<param name="cancellationToken"></param>
    member this.GetOrderById(orderId: int64, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("orderId", orderId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/store/order/{orderId}" requestParts cancellationToken

            match int status with
            | 200 -> return GetOrderById.OK(Serializer.deserialize content)
            | 400 -> return GetOrderById.BadRequest
            | 404 -> return GetOrderById.NotFound
            | _ -> return GetOrderById.DefaultResponse
        }

    ///<summary>
    ///For valid response try integer IDs with value &amp;lt; 1000. Anything above 1000 or non-integers will generate API errors.
    ///</summary>
    ///<param name="orderId">ID of the order that needs to be deleted</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteOrder(orderId: int64, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("orderId", orderId) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/store/order/{orderId}" requestParts cancellationToken

            match int status with
            | 200 -> return DeleteOrder.OK
            | 400 -> return DeleteOrder.BadRequest
            | 404 -> return DeleteOrder.NotFound
            | _ -> return DeleteOrder.DefaultResponse
        }

    ///<summary>
    ///This can only be done by the logged in user.
    ///</summary>
    member this.CreateUser(?cancellationToken: CancellationToken, ?body: User) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/user" requestParts cancellationToken

            match int status with
            | 200 -> return CreateUser.OK(Serializer.deserialize content)
            | _ -> return CreateUser.DefaultResponse
        }

    ///<summary>
    ///Creates list of users with given input array.
    ///</summary>
    member this.CreateUsersWithListInput
        (
            ?cancellationToken: CancellationToken,
            ?body: CreateUsersWithListInputPayload
        ) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/user/createWithList" requestParts cancellationToken

            match int status with
            | 200 -> return CreateUsersWithListInput.OK(Serializer.deserialize content)
            | _ -> return CreateUsersWithListInput.DefaultResponse
        }

    ///<summary>
    ///Log into the system.
    ///</summary>
    ///<param name="username">The user name for login</param>
    ///<param name="password">The password for login in clear text</param>
    ///<param name="cancellationToken"></param>
    member this.LoginUser(?username: string, ?password: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if username.IsSome then
                      RequestPart.query ("username", username.Value)
                  if password.IsSome then
                      RequestPart.query ("password", password.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/user/login" requestParts cancellationToken

            match int status with
            | 200 -> return LoginUser.OK content
            | 400 -> return LoginUser.BadRequest
            | _ -> return LoginUser.DefaultResponse
        }

    ///<summary>
    ///Log user out of the system.
    ///</summary>
    member this.LogoutUser(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/user/logout" requestParts cancellationToken

            match int status with
            | 200 -> return LogoutUser.OK
            | _ -> return LogoutUser.DefaultResponse
        }

    ///<summary>
    ///Get user detail based on username.
    ///</summary>
    ///<param name="username">The name that needs to be fetched. Use user1 for testing</param>
    ///<param name="cancellationToken"></param>
    member this.GetUserByName(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/user/{username}" requestParts cancellationToken

            match int status with
            | 200 -> return GetUserByName.OK(Serializer.deserialize content)
            | 400 -> return GetUserByName.BadRequest
            | 404 -> return GetUserByName.NotFound
            | _ -> return GetUserByName.DefaultResponse
        }

    ///<summary>
    ///This can only be done by the logged in user.
    ///</summary>
    ///<param name="username">name that need to be deleted</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.UpdateUser(username: string, ?cancellationToken: CancellationToken, ?body: User) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.putAsync httpClient "/user/{username}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateUser.OK
            | 400 -> return UpdateUser.BadRequest
            | 404 -> return UpdateUser.NotFound
            | _ -> return UpdateUser.DefaultResponse
        }

    ///<summary>
    ///This can only be done by the logged in user.
    ///</summary>
    ///<param name="username">The name that needs to be deleted</param>
    ///<param name="cancellationToken"></param>
    member this.DeleteUser(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/user/{username}" requestParts cancellationToken

            match int status with
            | 200 -> return DeleteUser.OK
            | 400 -> return DeleteUser.BadRequest
            | 404 -> return DeleteUser.NotFound
            | _ -> return DeleteUser.DefaultResponse
        }
