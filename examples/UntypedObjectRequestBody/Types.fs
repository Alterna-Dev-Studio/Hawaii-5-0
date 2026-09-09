namespace rec UntypedObjectRequestBody.Types

type CreateSubscriberPayload =
    { email: string
      firstName: Option<string> }
    ///Creates an instance of CreateSubscriberPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (email: string): CreateSubscriberPayload = { email = email; firstName = None }

[<RequireQualifiedAccess>]
type CreateSubscriber =
    ///Created
    | Created

type CreateSubscribersBulkPayloadArrayItem =
    { email: string }
    ///Creates an instance of CreateSubscribersBulkPayloadArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (email: string): CreateSubscribersBulkPayloadArrayItem = { email = email }

type CreateSubscribersBulkPayload = list<CreateSubscribersBulkPayloadArrayItem>

[<RequireQualifiedAccess>]
type CreateSubscribersBulk =
    ///Created
    | Created
