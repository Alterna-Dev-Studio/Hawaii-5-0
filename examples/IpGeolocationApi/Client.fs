namespace rec IpGeolocationApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open IpGeolocationApi.Types
open IpGeolocationApi.Http

///Abstract IP geolocation API allows developers to retrieve the region, country and city behind any IP worldwide. The API covers the geolocation of IPv4 and IPv6 addresses in 180+ countries worldwide. Extra information can be retrieved like the currency, flag or language associated to an IP.
type IpGeolocationApiClient(httpClient: HttpClient) =
    ///<summary>
    ///Retrieve the location of an IP address
    ///</summary>
    member this.GetV1(apiKey: string, ?ipAddress: string, ?fields: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.query ("api_key", apiKey)
                  if ipAddress.IsSome then
                      RequestPart.query ("ip_address", ipAddress.Value)
                  if fields.IsSome then
                      RequestPart.query ("fields", fields.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/v1/" requestParts cancellationToken
            return GetV1.OK(Serializer.deserialize content)
        }
