namespace rec AdyenCheckoutUtilityService.Types

type CheckoutUtilityRequest =
    { ///The list of origin domains, for which origin keys are requested.
      originDomains: list<string> }
    ///Creates an instance of CheckoutUtilityRequest with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (originDomains: list<string>): CheckoutUtilityRequest = { originDomains = originDomains }

type CheckoutUtilityResponse =
    { ///The list of origin keys for all requested domains. For each list item, the key is the domain and the value is the origin key.
      originKeys: Option<Map<string, string>> }
    ///Creates an instance of CheckoutUtilityResponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CheckoutUtilityResponse = { originKeys = None }

[<RequireQualifiedAccess>]
type PostOriginKeys =
    ///OK - the request has succeeded.
    | OK of payload: CheckoutUtilityResponse
    ///Bad Request - a problem reading or understanding the request.
    | BadRequest
    ///Unauthorized - authentication required.
    | Unauthorized
    ///Forbidden - insufficient permissions to process the request.
    | Forbidden
    ///Unprocessable Entity - a request validation error.
    | UnprocessableEntity
    ///Internal Server Error - the server could not process the request.
    | InternalServerError
