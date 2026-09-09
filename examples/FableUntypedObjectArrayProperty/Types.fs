namespace rec FableUntypedObjectArrayProperty.Types

type Lines =
    { sku: string
      quantity: Option<int> }
    ///Creates an instance of Lines with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (sku: string): Lines = { sku = sku; quantity = None }

type Notes =
    { text: Option<string> }
    ///Creates an instance of Notes with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Notes = { text = None }

type Order =
    { id: string
      lines: list<Lines>
      notes: Option<list<Notes>> }
    ///Creates an instance of Order with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string, lines: list<Lines>): Order = { id = id; lines = lines; notes = None }

[<RequireQualifiedAccess>]
type GetOrder =
    ///OK
    | OK of payload: Order
