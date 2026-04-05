namespace rec TaskPetStore.Types

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Status =
    | [<CompiledName "placed">] Placed
    | [<CompiledName "approved">] Approved
    | [<CompiledName "delivered">] Delivered
    member this.Format() =
        match this with
        | Placed -> "placed"
        | Approved -> "approved"
        | Delivered -> "delivered"

type Order =
    { id: Option<int64>
      petId: Option<int64>
      quantity: Option<int>
      shipDate: Option<System.DateTimeOffset>
      ///Order Status
      status: Option<Status>
      complete: Option<bool> }
    ///Creates an instance of Order with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Order =
        { id = None
          petId = None
          quantity = None
          shipDate = None
          status = None
          complete = None }

type Category =
    { id: Option<int64>
      name: Option<string> }
    ///Creates an instance of Category with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Category = { id = None; name = None }

type User =
    { id: Option<int64>
      username: Option<string>
      firstName: Option<string>
      lastName: Option<string>
      email: Option<string>
      password: Option<string>
      phone: Option<string>
      ///User Status
      userStatus: Option<int> }
    ///Creates an instance of User with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): User =
        { id = None
          username = None
          firstName = None
          lastName = None
          email = None
          password = None
          phone = None
          userStatus = None }

type Tag =
    { id: Option<int64>
      name: Option<string> }
    ///Creates an instance of Tag with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Tag = { id = None; name = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type PetStatus =
    | [<CompiledName "available">] Available
    | [<CompiledName "pending">] Pending
    | [<CompiledName "sold">] Sold
    member this.Format() =
        match this with
        | Available -> "available"
        | Pending -> "pending"
        | Sold -> "sold"

type Pet =
    { id: Option<int64>
      name: string
      category: Option<Category>
      photoUrls: list<string>
      tags: Option<list<Tag>>
      ///pet status in the store
      status: Option<PetStatus> }
    ///Creates an instance of Pet with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (name: string, photoUrls: list<string>): Pet =
        { id = None
          name = name
          category = None
          photoUrls = photoUrls
          tags = None
          status = None }

type ApiResponse =
    { code: Option<int>
      ``type``: Option<string>
      message: Option<string> }
    ///Creates an instance of ApiResponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ApiResponse =
        { code = None
          ``type`` = None
          message = None }

[<RequireQualifiedAccess>]
type UpdatePet =
    ///Successful operation
    | OK of payload: Pet
    ///Invalid ID supplied
    | BadRequest
    ///Pet not found
    | NotFound
    ///Validation exception
    | UnprocessableEntity
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type AddPet =
    ///Successful operation
    | OK of payload: Pet
    ///Invalid input
    | BadRequest
    ///Validation exception
    | UnprocessableEntity
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type FindPetsByStatus =
    ///successful operation
    | OK of payload: list<Pet>
    ///Invalid status value
    | BadRequest
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type FindPetsByTags =
    ///successful operation
    | OK of payload: list<Pet>
    ///Invalid tag value
    | BadRequest
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetPetById =
    ///successful operation
    | OK of payload: Pet
    ///Invalid ID supplied
    | BadRequest
    ///Pet not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type UpdatePetWithForm =
    ///successful operation
    | OK of payload: Pet
    ///Invalid input
    | BadRequest
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type DeletePet =
    ///Pet deleted
    | OK
    ///Invalid pet value
    | BadRequest
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type UploadFile =
    ///successful operation
    | OK of payload: ApiResponse
    ///No file uploaded
    | BadRequest
    ///Pet not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetInventory =
    ///successful operation
    | OK of payload: Map<string, int>
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type PlaceOrder =
    ///successful operation
    | OK of payload: Order
    ///Invalid input
    | BadRequest
    ///Validation exception
    | UnprocessableEntity
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetOrderById =
    ///successful operation
    | OK of payload: Order
    ///Invalid ID supplied
    | BadRequest
    ///Order not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type DeleteOrder =
    ///order deleted
    | OK
    ///Invalid ID supplied
    | BadRequest
    ///Order not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type CreateUser =
    ///successful operation
    | OK of payload: User
    ///Unexpected error
    | DefaultResponse

type CreateUsersWithListInputPayloadArrayItem =
    { id: Option<int64>
      username: Option<string>
      firstName: Option<string>
      lastName: Option<string>
      email: Option<string>
      password: Option<string>
      phone: Option<string>
      ///User Status
      userStatus: Option<int> }
    ///Creates an instance of CreateUsersWithListInputPayloadArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateUsersWithListInputPayloadArrayItem =
        { id = None
          username = None
          firstName = None
          lastName = None
          email = None
          password = None
          phone = None
          userStatus = None }

type CreateUsersWithListInputPayload = list<CreateUsersWithListInputPayloadArrayItem>

[<RequireQualifiedAccess>]
type CreateUsersWithListInput =
    ///Successful operation
    | OK of payload: User
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type LoginUser =
    ///successful operation
    | OK of payload: string
    ///Invalid username/password supplied
    | BadRequest
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type LogoutUser =
    ///successful operation
    | OK
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetUserByName =
    ///successful operation
    | OK of payload: User
    ///Invalid username supplied
    | BadRequest
    ///User not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type UpdateUser =
    ///successful operation
    | OK
    ///bad request
    | BadRequest
    ///user not found
    | NotFound
    ///Unexpected error
    | DefaultResponse

[<RequireQualifiedAccess>]
type DeleteUser =
    ///User deleted
    | OK
    ///Invalid username supplied
    | BadRequest
    ///User not found
    | NotFound
    ///Unexpected error
    | DefaultResponse
