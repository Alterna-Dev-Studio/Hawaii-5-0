namespace rec TaskAnyOfOneOfCollections.Types

type Point =
    { x: float
      y: float }
    ///Creates an instance of Point with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (x: float, y: float): Point = { x = x; y = y }

type Label =
    { text: string }
    ///Creates an instance of Label with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (text: string): Label = { text = text }

///A shape is either a point or a label. Every variant is a reference, so this becomes a real discriminated union.
[<RequireQualifiedAccess>]
type Shape =
    | Point of value: Point
    | Label of value: Label

///A shape whose second variant is an inline object, so the whole type falls back to a free-form JSON value.
type LooseShape = System.Text.Json.JsonElement

[<RequireQualifiedAccess>]
type BasketItems =
    | String of value: string
    | Int of value: int

[<RequireQualifiedAccess>]
type BasketLabels =
    | String of value: string
    | Bool of value: bool

[<RequireQualifiedAccess>]
type BasketMarkers =
    | Point of value: Point
    | Label of value: Label

[<RequireQualifiedAccess>]
type BasketCodes =
    | String of value: string
    | Guid of value: System.Guid
    | Int of value: int
    | Int64 of value: int64

type Basket =
    { id: string
      items: Option<list<BasketItems>>
      labels: Option<Map<string, BasketLabels>>
      markers: list<BasketMarkers>
      ///A shape is either a point or a label. Every variant is a reference, so this becomes a real discriminated union.
      shape: Option<Shape>
      extra: Option<System.Text.Json.JsonElement>
      tags: Option<list<System.Text.Json.JsonElement>>
      meta: Option<Map<string, System.Text.Json.JsonElement>>
      codes: Option<BasketCodes>
      shapes: Option<list<Shape>> }
    ///Creates an instance of Basket with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string, markers: list<BasketMarkers>): Basket =
        { id = id
          items = None
          labels = None
          markers = markers
          shape = None
          extra = None
          tags = None
          meta = None
          codes = None
          shapes = None }

[<RequireQualifiedAccess>]
type GetBasket =
    ///OK
    | OK of payload: Basket
