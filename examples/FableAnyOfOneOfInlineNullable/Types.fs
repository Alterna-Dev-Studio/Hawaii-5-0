namespace rec FableAnyOfOneOfInlineNullable.Types

type Card =
    { number: string }
    ///Creates an instance of Card with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (number: string): Card = { number = number }

type PaymentCase2 =
    { iban: string
      bic: Option<string> }
    ///Creates an instance of PaymentCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (iban: string): PaymentCase2 = { iban = iban; bic = None }

type PaymentCase3 =
    { voucherCode: Option<string> }
    ///Creates an instance of PaymentCase3 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): PaymentCase3 = { voucherCode = None }

///A payment is a referenced card or one of two inline objects. Both inline objects become nested records PaymentCase2 and PaymentCase3.
[<RequireQualifiedAccess>]
type Payment =
    | Card of value: Card
    | PaymentCase2 of value: PaymentCase2
    | PaymentCase3 of value: PaymentCase3

///One variant is an inline object and the other is an array (unsupported), so the whole type falls back to a free-form JSON value and no LooseAttachmentCase1 record is generated.
type LooseAttachment = obj

type OrderShippingCase2 =
    { street: string
      city: string }
    ///Creates an instance of OrderShippingCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (street: string, city: string): OrderShippingCase2 = { street = street; city = city }

[<RequireQualifiedAccess>]
type OrderShipping =
    | String of value: string
    | OrderShippingCase2 of value: OrderShippingCase2

type OrderNotesCase2 =
    { author: Option<string>
      body: Option<string> }
    ///Creates an instance of OrderNotesCase2 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): OrderNotesCase2 = { author = None; body = None }

[<RequireQualifiedAccess>]
type OrderNotes =
    | String of value: string
    | OrderNotesCase2 of value: OrderNotesCase2

type Order =
    { id: string
      ///A payment is a referenced card or one of two inline objects. Both inline objects become nested records PaymentCase2 and PaymentCase3.
      payment: Option<Payment>
      shipping: OrderShipping
      notes: Option<list<OrderNotes>>
      nickname: Option<string>
      priority: Option<int64>
      coupon: Option<Card>
      weight: Option<obj>
      ///Parent description wins
      reference: Option<string>
      quantity: Option<int>
      attachment: Option<obj> }
    ///Creates an instance of Order with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string,
                          shipping: OrderShipping,
                          nickname: Option<string>,
                          priority: Option<int64>,
                          reference: Option<string>): Order =
        { id = id
          payment = None
          shipping = shipping
          notes = None
          nickname = nickname
          priority = priority
          coupon = None
          weight = None
          reference = reference
          quantity = None
          attachment = None }

[<RequireQualifiedAccess>]
type GetOrder =
    ///OK
    | OK of payload: Order
