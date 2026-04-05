namespace rec FablePetStore

open Browser.Types
open Fable.SimpleHttp
open FablePetStore.Types
open FablePetStore.Http

///This is a sample Pet Store Server based on the OpenAPI 3.0 specification.  You can find out more about
///Swagger at [https://swagger.io](https://swagger.io). In the third iteration of the pet store, we've switched to the design first approach!
///You can now help us improve the API whether it's by making changes to the definition itself or to the code.
///That way, with time, we can improve the API in general, and expose some of the new features in OAS3.
///Some useful links:
///- [The Pet Store repository](https://github.com/swagger-api/swagger-petstore)
///- [The source API definition for the Pet Store](https://github.com/swagger-api/swagger-petstore/blob/master/src/main/resources/openapi.yaml)
type FablePetStoreClient(url: string, headers: list<Header>) =
    new(url: string) = FablePetStoreClient(url, [])

    ///<summary>
    ///Update an existing pet by Id.
    ///</summary>
    member this.UpdatePet(body: Pet) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.putAsync url "/pet" headers requestParts

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
    member this.AddPet(body: Pet) =
        async {
            let requestParts = [ RequestPart.jsonContent body ]
            let! (status, content) = OpenApiHttp.postAsync url "/pet" headers requestParts

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
    member this.FindPetsByStatus(status: string) =
        async {
            let requestParts = [ RequestPart.query ("status", status) ]
            let! (status, content) = OpenApiHttp.getAsync url "/pet/findByStatus" headers requestParts

            match int status with
            | 200 -> return FindPetsByStatus.OK(Serializer.deserialize content)
            | 400 -> return FindPetsByStatus.BadRequest
            | _ -> return FindPetsByStatus.DefaultResponse
        }

    ///<summary>
    ///Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.
    ///</summary>
    ///<param name="tags">Tags to filter by</param>
    member this.FindPetsByTags(tags: list<string>) =
        async {
            let requestParts = [ RequestPart.query ("tags", tags) ]
            let! (status, content) = OpenApiHttp.getAsync url "/pet/findByTags" headers requestParts

            match int status with
            | 200 -> return FindPetsByTags.OK(Serializer.deserialize content)
            | 400 -> return FindPetsByTags.BadRequest
            | _ -> return FindPetsByTags.DefaultResponse
        }

    ///<summary>
    ///Returns a single pet.
    ///</summary>
    ///<param name="petId">ID of pet to return</param>
    member this.GetPetById(petId: int64) =
        async {
            let requestParts = [ RequestPart.path ("petId", petId) ]
            let! (status, content) = OpenApiHttp.getAsync url "/pet/{petId}" headers requestParts

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
    member this.UpdatePetWithForm(petId: int64, ?name: string, ?status: string) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if name.IsSome then
                      RequestPart.query ("name", name.Value)
                  if status.IsSome then
                      RequestPart.query ("status", status.Value) ]

            let! (status, content) = OpenApiHttp.postAsync url "/pet/{petId}" headers requestParts

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
    member this.DeletePet(petId: int64, ?apiKey: string) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if apiKey.IsSome then
                      RequestPart.header ("api_key", apiKey.Value) ]

            let! (status, content) = OpenApiHttp.deleteAsync url "/pet/{petId}" headers requestParts

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
    ///<param name="requestBody"></param>
    member this.UploadFile(petId: int64, ?additionalMetadata: string, ?requestBody: byte []) =
        async {
            let requestParts =
                [ RequestPart.path ("petId", petId)
                  if additionalMetadata.IsSome then
                      RequestPart.query ("additionalMetadata", additionalMetadata.Value)
                  if requestBody.IsSome then
                      RequestPart.binaryContent requestBody.Value ]

            let! (status, content) = OpenApiHttp.postAsync url "/pet/{petId}/uploadImage" headers requestParts

            match int status with
            | 200 -> return UploadFile.OK(Serializer.deserialize content)
            | 400 -> return UploadFile.BadRequest
            | 404 -> return UploadFile.NotFound
            | _ -> return UploadFile.DefaultResponse
        }

    ///<summary>
    ///Returns a map of status codes to quantities.
    ///</summary>
    member this.GetInventory() =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync url "/store/inventory" headers requestParts

            match int status with
            | 200 -> return GetInventory.OK(Serializer.deserialize content)
            | _ -> return GetInventory.DefaultResponse
        }

    ///<summary>
    ///Place a new order in the store.
    ///</summary>
    member this.PlaceOrder(?body: Order) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync url "/store/order" headers requestParts

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
    member this.GetOrderById(orderId: int64) =
        async {
            let requestParts =
                [ RequestPart.path ("orderId", orderId) ]

            let! (status, content) = OpenApiHttp.getAsync url "/store/order/{orderId}" headers requestParts

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
    member this.DeleteOrder(orderId: int64) =
        async {
            let requestParts =
                [ RequestPart.path ("orderId", orderId) ]

            let! (status, content) = OpenApiHttp.deleteAsync url "/store/order/{orderId}" headers requestParts

            match int status with
            | 200 -> return DeleteOrder.OK
            | 400 -> return DeleteOrder.BadRequest
            | 404 -> return DeleteOrder.NotFound
            | _ -> return DeleteOrder.DefaultResponse
        }

    ///<summary>
    ///This can only be done by the logged in user.
    ///</summary>
    member this.CreateUser(?body: User) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync url "/user" headers requestParts

            match int status with
            | 200 -> return CreateUser.OK(Serializer.deserialize content)
            | _ -> return CreateUser.DefaultResponse
        }

    ///<summary>
    ///Creates list of users with given input array.
    ///</summary>
    member this.CreateUsersWithListInput(?body: CreateUsersWithListInputPayload) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync url "/user/createWithList" headers requestParts

            match int status with
            | 200 -> return CreateUsersWithListInput.OK(Serializer.deserialize content)
            | _ -> return CreateUsersWithListInput.DefaultResponse
        }

    ///<summary>
    ///Log into the system.
    ///</summary>
    ///<param name="username">The user name for login</param>
    ///<param name="password">The password for login in clear text</param>
    member this.LoginUser(?username: string, ?password: string) =
        async {
            let requestParts =
                [ if username.IsSome then
                      RequestPart.query ("username", username.Value)
                  if password.IsSome then
                      RequestPart.query ("password", password.Value) ]

            let! (status, content) = OpenApiHttp.getAsync url "/user/login" headers requestParts

            match int status with
            | 200 -> return LoginUser.OK content
            | 400 -> return LoginUser.BadRequest
            | _ -> return LoginUser.DefaultResponse
        }

    ///<summary>
    ///Log user out of the system.
    ///</summary>
    member this.LogoutUser() =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync url "/user/logout" headers requestParts

            match int status with
            | 200 -> return LogoutUser.OK
            | _ -> return LogoutUser.DefaultResponse
        }

    ///<summary>
    ///Get user detail based on username.
    ///</summary>
    ///<param name="username">The name that needs to be fetched. Use user1 for testing</param>
    member this.GetUserByName(username: string) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.getAsync url "/user/{username}" headers requestParts

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
    ///<param name="body"></param>
    member this.UpdateUser(username: string, ?body: User) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.putAsync url "/user/{username}" headers requestParts

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
    member this.DeleteUser(username: string) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.deleteAsync url "/user/{username}" headers requestParts

            match int status with
            | 200 -> return DeleteUser.OK
            | 400 -> return DeleteUser.BadRequest
            | 404 -> return DeleteUser.NotFound
            | _ -> return DeleteUser.DefaultResponse
        }
