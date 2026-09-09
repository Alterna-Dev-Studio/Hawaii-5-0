namespace rec TaskUntypedObjectResponse.Types

type TriggerEventRequestDto =
    { name: string }
    ///Creates an instance of TriggerEventRequestDto with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (name: string): TriggerEventRequestDto = { name = name }

type TriggerEventResponseDto =
    { acknowledged: bool
      status: string
      transactionId: Option<string> }
    ///Creates an instance of TriggerEventResponseDto with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (acknowledged: bool, status: string): TriggerEventResponseDto =
        { acknowledged = acknowledged
          status = status
          transactionId = None }

type EventsControllerTrigger_Created =
    { data: Option<TriggerEventResponseDto> }

type EventsControllerTrigger_BadRequest =
    { message: string
      details: Option<System.Text.Json.JsonElement> }

[<RequireQualifiedAccess>]
type EventsControllerTrigger =
    ///Created
    | Created of payload: EventsControllerTrigger_Created
    ///Bad Request
    | BadRequest of payload: EventsControllerTrigger_BadRequest
