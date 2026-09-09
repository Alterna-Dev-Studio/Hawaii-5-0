namespace rec TaskUntypedObjectArrayProperty

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open TaskUntypedObjectArrayProperty.Types
open TaskUntypedObjectArrayProperty.Http

///Regression fixture for #32: a record property whose array items declare `properties` without `type: "object"` must generate a nested record list, not `list&amp;lt;string&amp;gt;`.
type TaskUntypedObjectArrayPropertyClient(httpClient: HttpClient) =
    ///<summary>
    ///Get an order
    ///</summary>
    member this.GetOrder(id: string, ?cancellationToken: CancellationToken) =
        task {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.getAsync httpClient "/orders/{id}" requestParts cancellationToken
            return GetOrder.OK(Serializer.deserialize content)
        }
