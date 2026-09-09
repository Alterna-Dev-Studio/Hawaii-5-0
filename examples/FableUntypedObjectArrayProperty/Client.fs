namespace rec FableUntypedObjectArrayProperty

open Browser.Types
open Fable.SimpleHttp
open FableUntypedObjectArrayProperty.Types
open FableUntypedObjectArrayProperty.Http

///Regression fixture for #32: a record property whose array items declare `properties` without `type: "object"` must generate a nested record list, not `list&amp;lt;string&amp;gt;`.
type FableUntypedObjectArrayPropertyClient(url: string, headers: list<Header>) =
    new(url: string) = FableUntypedObjectArrayPropertyClient(url, [])

    ///<summary>
    ///Get an order
    ///</summary>
    member this.GetOrder(id: string) =
        async {
            let requestParts = [ RequestPart.path ("id", id) ]
            let! (status, content) = OpenApiHttp.getAsync url "/orders/{id}" headers requestParts
            return GetOrder.OK(Serializer.deserialize content)
        }
