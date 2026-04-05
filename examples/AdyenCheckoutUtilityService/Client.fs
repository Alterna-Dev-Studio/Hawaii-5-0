namespace rec AdyenCheckoutUtilityService

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open AdyenCheckoutUtilityService.Types
open AdyenCheckoutUtilityService.Http

///A web service containing utility functions available for merchants integrating with Checkout APIs.
///## Authentication
///Each request to the Checkout Utility API must be signed with an API key. For this, obtain an API Key from your Customer Area, as described in [How to get the Checkout API key](https://docs.adyen.com/developers/user-management/how-to-get-the-checkout-api-key). Then set this key to the `X-API-Key` header value, for example:
///```
///curl
///-H "Content-Type: application/json" \
///-H "X-API-Key: Your_Checkout_API_key" \
///...
///```
///Note that when going live, you need to generate a new API Key to access the [live endpoints](https://docs.adyen.com/developers/api-reference/live-endpoints).
///## Versioning
///Checkout API supports versioning of its endpoints through a version suffix in the endpoint URL. This suffix has the following format: "vXX", where XX is the version number.
///For example:
///```
///https://checkout-test.adyen.com/v1/originKeys
///```
type AdyenCheckoutUtilityServiceClient(httpClient: HttpClient) =
    ///<summary>
    ///This operation takes the origin domains and returns a JSON object containing the corresponding origin keys for the domains.
    ///</summary>
    member this.PostOriginKeys(?cancellationToken: CancellationToken, ?body: CheckoutUtilityRequest) =
        async {
            let requestParts =
                [ if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/originKeys" requestParts cancellationToken

            match int status with
            | 200 -> return PostOriginKeys.OK(Serializer.deserialize content)
            | 400 -> return PostOriginKeys.BadRequest
            | 401 -> return PostOriginKeys.Unauthorized
            | 403 -> return PostOriginKeys.Forbidden
            | 422 -> return PostOriginKeys.UnprocessableEntity
            | _ -> return PostOriginKeys.InternalServerError
        }
