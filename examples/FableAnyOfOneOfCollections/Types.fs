namespace rec FableAnyOfOneOfCollections.Types

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

type LooseShapeCase2 =
    { kind: Option<string> }
    ///Creates an instance of LooseShapeCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): LooseShapeCase2 = { kind = None }

///A shape whose second variant is an inline object. The inline object becomes a nested record (LooseShapeCase2) and the type is still a real discriminated union.
[<RequireQualifiedAccess>]
type LooseShape =
    | Point of value: Point
    | LooseShapeCase2 of value: LooseShapeCase2

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

type BasketExtraCase2 =
    { kind: Option<string> }
    ///Creates an instance of BasketExtraCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BasketExtraCase2 = { kind = None }

[<RequireQualifiedAccess>]
type BasketExtra =
    | String of value: string
    | BasketExtraCase2 of value: BasketExtraCase2

type BasketTagsCase2 =
    { kind: Option<string> }
    ///Creates an instance of BasketTagsCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BasketTagsCase2 = { kind = None }

[<RequireQualifiedAccess>]
type BasketTags =
    | String of value: string
    | BasketTagsCase2 of value: BasketTagsCase2

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
      extra: Option<BasketExtra>
      tags: Option<list<BasketTags>>
      meta: Option<Map<string, obj>>
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
