namespace rec TaskUnlimitedResponses.Types

type PostItemsPayload =
    { name: Option<string> }
    ///Creates an instance of PostItemsPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): PostItemsPayload = { name = None }

type PostItems_Conflict = { reason: Option<string> }

[<RequireQualifiedAccess>]
type PostItems =
    ///OK
    | OK
    ///Bad Request
    | BadRequest
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Conflict
    | Conflict of payload: PostItems_Conflict
    ///Internal Server Error
    | InternalServerError
