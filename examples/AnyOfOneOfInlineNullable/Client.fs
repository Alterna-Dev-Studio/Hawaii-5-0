namespace rec AnyOfOneOfInlineNullable

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open AnyOfOneOfInlineNullable.Types
open AnyOfOneOfInlineNullable.Http

///Regression fixture for PR 14: inline object variants inside oneOf/anyOf are resolved into nested records and referenced from a discriminated union (at property level, array-item level and top level), while the OpenAPI 3.1 nullable pattern `anyOf: [T, {nullable: true}]` is simplified to `T` with nullable: true instead of generating a union. `{nullable: false}` is NOT the nullable pattern and must be left intact (falling back because the marker object is unsupported).
type AnyOfOneOfInlineNullableClient(httpClient: HttpClient) =
    ///<summary>
    ///Get an order
    ///</summary>
    member this.GetOrder(id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/orders/{id}" requestParts cancellationToken
            return GetOrder.OK(Serializer.deserialize content)
        }
