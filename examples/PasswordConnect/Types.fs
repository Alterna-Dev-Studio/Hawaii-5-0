namespace rec PasswordConnect.Types

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Op =
    | [<CompiledName "add">] Add
    | [<CompiledName "remove">] Remove
    | [<CompiledName "replace">] Replace
    member this.Format() =
        match this with
        | Add -> "add"
        | Remove -> "remove"
        | Replace -> "replace"

type PatchArrayItem =
    { op: Op
      ///An RFC6901 JSON Pointer pointing to the Item document, an Item Attribute, and Item Field by Field ID, or an Item Field Attribute
      path: string
      value: Option<Newtonsoft.Json.Linq.JObject> }
    ///Creates an instance of PatchArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (op: Op, path: string): PatchArrayItem = { op = op; path = path; value = None }

type Patch = list<PatchArrayItem>

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Action =
    | [<CompiledName "READ">] READ
    | [<CompiledName "CREATE">] CREATE
    | [<CompiledName "UPDATE">] UPDATE
    | [<CompiledName "DELETE">] DELETE
    member this.Format() =
        match this with
        | READ -> "READ"
        | CREATE -> "CREATE"
        | UPDATE -> "UPDATE"
        | DELETE -> "DELETE"

type Actor =
    { account: Option<string>
      id: Option<System.Guid>
      jti: Option<string>
      requestIp: Option<string>
      userAgent: Option<string> }
    ///Creates an instance of Actor with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Actor =
        { account = None
          id = None
          jti = None
          requestIp = None
          userAgent = None }

type ItemFromResource =
    { id: Option<string> }
    ///Creates an instance of ItemFromResource with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ItemFromResource = { id = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Type =
    | [<CompiledName "ITEM">] ITEM
    | [<CompiledName "VAULT">] VAULT
    member this.Format() =
        match this with
        | ITEM -> "ITEM"
        | VAULT -> "VAULT"

type VaultFromResource =
    { id: Option<string> }
    ///Creates an instance of VaultFromResource with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): VaultFromResource = { id = None }

type Resource =
    { item: Option<ItemFromResource>
      itemVersion: Option<int>
      ``type``: Option<Type>
      vault: Option<VaultFromResource> }
    ///Creates an instance of Resource with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Resource =
        { item = None
          itemVersion = None
          ``type`` = None
          vault = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Result =
    | [<CompiledName "SUCCESS">] SUCCESS
    | [<CompiledName "DENY">] DENY
    member this.Format() =
        match this with
        | SUCCESS -> "SUCCESS"
        | DENY -> "DENY"

///Represents a request that was made to the API. Including what Token was used and what resource was accessed.
type APIRequest =
    { action: Option<Action>
      actor: Option<Actor>
      ///The unique id used to identify a single request.
      requestId: Option<System.Guid>
      resource: Option<Resource>
      result: Option<Result>
      ///The time at which the request was processed by the server.
      timestamp: Option<System.DateTimeOffset> }
    ///Creates an instance of APIRequest with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): APIRequest =
        { action = None
          actor = None
          requestId = None
          resource = None
          result = None
          timestamp = None }

type ErrorResponse =
    { ///A message detailing the error
      message: Option<string>
      ///HTTP Status Code
      status: Option<int> }
    ///Creates an instance of ErrorResponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ErrorResponse = { message = None; status = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Purpose =
    | [<CompiledName "">] EmptyString
    | [<CompiledName "USERNAME">] USERNAME
    | [<CompiledName "PASSWORD">] PASSWORD
    | [<CompiledName "NOTES">] NOTES
    member this.Format() =
        match this with
        | EmptyString -> ""
        | USERNAME -> "USERNAME"
        | PASSWORD -> "PASSWORD"
        | NOTES -> "NOTES"

type Section =
    { id: Option<string> }
    ///Creates an instance of Section with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Section = { id = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type FieldType =
    | [<CompiledName "STRING">] STRING
    | [<CompiledName "EMAIL">] EMAIL
    | [<CompiledName "CONCEALED">] CONCEALED
    | [<CompiledName "URL">] URL
    | [<CompiledName "TOTP">] TOTP
    | [<CompiledName "DATE">] DATE
    | [<CompiledName "MONTH_YEAR">] MONTH_YEAR
    | [<CompiledName "MENU">] MENU
    member this.Format() =
        match this with
        | STRING -> "STRING"
        | EMAIL -> "EMAIL"
        | CONCEALED -> "CONCEALED"
        | URL -> "URL"
        | TOTP -> "TOTP"
        | DATE -> "DATE"
        | MONTH_YEAR -> "MONTH_YEAR"
        | MENU -> "MENU"

type Field =
    { ///For fields with a purpose of `PASSWORD` this is the entropy of the value
      entropy: Option<float>
      ///If value is not present then a new value should be generated for this field
      generate: Option<bool>
      id: string
      label: Option<string>
      ///Some item types, Login and Password, have fields used for autofill. This property indicates that purpose and is required for some item types.
      purpose: Option<Purpose>
      ///The recipe is used in conjunction with the "generate" property to set the character set used to generate a new secure value
      recipe: Option<GeneratorRecipe>
      section: Option<Section>
      ``type``: FieldType
      value: Option<string> }
    ///Creates an instance of Field with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string, ``type``: FieldType): Field =
        { entropy = None
          generate = None
          id = id
          label = None
          purpose = None
          recipe = None
          section = None
          ``type`` = ``type``
          value = None }

///For files that are in a section, this field describes the section.
type FileSection =
    { id: Option<string> }
    ///Creates an instance of FileSection with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): FileSection = { id = None }

type File =
    { ///Base64-encoded contents of the file. Only set if size &amp;lt;= OP_MAX_INLINE_FILE_SIZE_KB kb and `inline_files` is set to `true`.
      content: Option<byte []>
      ///Path of the Connect API that can be used to download the contents of this file.
      content_path: Option<string>
      ///ID of the file
      id: Option<string>
      ///Name of the file
      name: Option<string>
      ///For files that are in a section, this field describes the section.
      section: Option<FileSection>
      ///Size in bytes of the file
      size: Option<int> }
    ///Creates an instance of File with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): File =
        { content = None
          content_path = None
          id = None
          name = None
          section = None
          size = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Category =
    | [<CompiledName "LOGIN">] LOGIN
    | [<CompiledName "PASSWORD">] PASSWORD
    | [<CompiledName "API_CREDENTIAL">] API_CREDENTIAL
    | [<CompiledName "SERVER">] SERVER
    | [<CompiledName "DATABASE">] DATABASE
    | [<CompiledName "CREDIT_CARD">] CREDIT_CARD
    | [<CompiledName "MEMBERSHIP">] MEMBERSHIP
    | [<CompiledName "PASSPORT">] PASSPORT
    | [<CompiledName "SOFTWARE_LICENSE">] SOFTWARE_LICENSE
    | [<CompiledName "OUTDOOR_LICENSE">] OUTDOOR_LICENSE
    | [<CompiledName "SECURE_NOTE">] SECURE_NOTE
    | [<CompiledName "WIRELESS_ROUTER">] WIRELESS_ROUTER
    | [<CompiledName "BANK_ACCOUNT">] BANK_ACCOUNT
    | [<CompiledName "DRIVER_LICENSE">] DRIVER_LICENSE
    | [<CompiledName "IDENTITY">] IDENTITY
    | [<CompiledName "REWARD_PROGRAM">] REWARD_PROGRAM
    | [<CompiledName "DOCUMENT">] DOCUMENT
    | [<CompiledName "EMAIL_ACCOUNT">] EMAIL_ACCOUNT
    | [<CompiledName "SOCIAL_SECURITY_NUMBER">] SOCIAL_SECURITY_NUMBER
    | [<CompiledName "MEDICAL_RECORD">] MEDICAL_RECORD
    | [<CompiledName "SSH_KEY">] SSH_KEY
    | [<CompiledName "CUSTOM">] CUSTOM
    member this.Format() =
        match this with
        | LOGIN -> "LOGIN"
        | PASSWORD -> "PASSWORD"
        | API_CREDENTIAL -> "API_CREDENTIAL"
        | SERVER -> "SERVER"
        | DATABASE -> "DATABASE"
        | CREDIT_CARD -> "CREDIT_CARD"
        | MEMBERSHIP -> "MEMBERSHIP"
        | PASSPORT -> "PASSPORT"
        | SOFTWARE_LICENSE -> "SOFTWARE_LICENSE"
        | OUTDOOR_LICENSE -> "OUTDOOR_LICENSE"
        | SECURE_NOTE -> "SECURE_NOTE"
        | WIRELESS_ROUTER -> "WIRELESS_ROUTER"
        | BANK_ACCOUNT -> "BANK_ACCOUNT"
        | DRIVER_LICENSE -> "DRIVER_LICENSE"
        | IDENTITY -> "IDENTITY"
        | REWARD_PROGRAM -> "REWARD_PROGRAM"
        | DOCUMENT -> "DOCUMENT"
        | EMAIL_ACCOUNT -> "EMAIL_ACCOUNT"
        | SOCIAL_SECURITY_NUMBER -> "SOCIAL_SECURITY_NUMBER"
        | MEDICAL_RECORD -> "MEDICAL_RECORD"
        | SSH_KEY -> "SSH_KEY"
        | CUSTOM -> "CUSTOM"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type State =
    | [<CompiledName "ARCHIVED">] ARCHIVED
    | [<CompiledName "DELETED">] DELETED
    member this.Format() =
        match this with
        | ARCHIVED -> "ARCHIVED"
        | DELETED -> "DELETED"

type Urls =
    { href: string
      label: Option<string>
      primary: Option<bool> }
    ///Creates an instance of Urls with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (href: string): Urls =
        { href = href
          label = None
          primary = None }

type VaultFromFullItem =
    { id: string }
    ///Creates an instance of VaultFromFullItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string): VaultFromFullItem = { id = id }

type Sections =
    { id: Option<string>
      label: Option<string> }
    ///Creates an instance of Sections with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Sections = { id = None; label = None }

type FullItem =
    { category: Option<Category>
      createdAt: Option<System.DateTimeOffset>
      favorite: Option<bool>
      id: Option<string>
      lastEditedBy: Option<string>
      state: Option<State>
      tags: Option<list<string>>
      title: Option<string>
      updatedAt: Option<System.DateTimeOffset>
      urls: Option<list<Urls>>
      vault: Option<VaultFromFullItem>
      version: Option<int>
      fields: Option<list<Field>>
      files: Option<list<File>>
      sections: Option<list<Sections>> }
    ///Creates an instance of FullItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): FullItem =
        { category = None
          createdAt = None
          favorite = None
          id = None
          lastEditedBy = None
          state = None
          tags = None
          title = None
          updatedAt = None
          urls = None
          vault = None
          version = None
          fields = None
          files = None
          sections = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type CharacterSets =
    | [<CompiledName "LETTERS">] LETTERS
    | [<CompiledName "DIGITS">] DIGITS
    | [<CompiledName "SYMBOLS">] SYMBOLS
    member this.Format() =
        match this with
        | LETTERS -> "LETTERS"
        | DIGITS -> "DIGITS"
        | SYMBOLS -> "SYMBOLS"

///The recipe is used in conjunction with the "generate" property to set the character set used to generate a new secure value
type GeneratorRecipe =
    { characterSets: Option<list<CharacterSets>>
      ///List of all characters that should be excluded from generated passwords.
      excludeCharacters: Option<string>
      ///Length of the generated value
      length: Option<int> }
    ///Creates an instance of GeneratorRecipe with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): GeneratorRecipe =
        { characterSets = None
          excludeCharacters = None
          length = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type ItemCategory =
    | [<CompiledName "LOGIN">] LOGIN
    | [<CompiledName "PASSWORD">] PASSWORD
    | [<CompiledName "API_CREDENTIAL">] API_CREDENTIAL
    | [<CompiledName "SERVER">] SERVER
    | [<CompiledName "DATABASE">] DATABASE
    | [<CompiledName "CREDIT_CARD">] CREDIT_CARD
    | [<CompiledName "MEMBERSHIP">] MEMBERSHIP
    | [<CompiledName "PASSPORT">] PASSPORT
    | [<CompiledName "SOFTWARE_LICENSE">] SOFTWARE_LICENSE
    | [<CompiledName "OUTDOOR_LICENSE">] OUTDOOR_LICENSE
    | [<CompiledName "SECURE_NOTE">] SECURE_NOTE
    | [<CompiledName "WIRELESS_ROUTER">] WIRELESS_ROUTER
    | [<CompiledName "BANK_ACCOUNT">] BANK_ACCOUNT
    | [<CompiledName "DRIVER_LICENSE">] DRIVER_LICENSE
    | [<CompiledName "IDENTITY">] IDENTITY
    | [<CompiledName "REWARD_PROGRAM">] REWARD_PROGRAM
    | [<CompiledName "DOCUMENT">] DOCUMENT
    | [<CompiledName "EMAIL_ACCOUNT">] EMAIL_ACCOUNT
    | [<CompiledName "SOCIAL_SECURITY_NUMBER">] SOCIAL_SECURITY_NUMBER
    | [<CompiledName "MEDICAL_RECORD">] MEDICAL_RECORD
    | [<CompiledName "SSH_KEY">] SSH_KEY
    | [<CompiledName "CUSTOM">] CUSTOM
    member this.Format() =
        match this with
        | LOGIN -> "LOGIN"
        | PASSWORD -> "PASSWORD"
        | API_CREDENTIAL -> "API_CREDENTIAL"
        | SERVER -> "SERVER"
        | DATABASE -> "DATABASE"
        | CREDIT_CARD -> "CREDIT_CARD"
        | MEMBERSHIP -> "MEMBERSHIP"
        | PASSPORT -> "PASSPORT"
        | SOFTWARE_LICENSE -> "SOFTWARE_LICENSE"
        | OUTDOOR_LICENSE -> "OUTDOOR_LICENSE"
        | SECURE_NOTE -> "SECURE_NOTE"
        | WIRELESS_ROUTER -> "WIRELESS_ROUTER"
        | BANK_ACCOUNT -> "BANK_ACCOUNT"
        | DRIVER_LICENSE -> "DRIVER_LICENSE"
        | IDENTITY -> "IDENTITY"
        | REWARD_PROGRAM -> "REWARD_PROGRAM"
        | DOCUMENT -> "DOCUMENT"
        | EMAIL_ACCOUNT -> "EMAIL_ACCOUNT"
        | SOCIAL_SECURITY_NUMBER -> "SOCIAL_SECURITY_NUMBER"
        | MEDICAL_RECORD -> "MEDICAL_RECORD"
        | SSH_KEY -> "SSH_KEY"
        | CUSTOM -> "CUSTOM"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type ItemState =
    | [<CompiledName "ARCHIVED">] ARCHIVED
    | [<CompiledName "DELETED">] DELETED
    member this.Format() =
        match this with
        | ARCHIVED -> "ARCHIVED"
        | DELETED -> "DELETED"

type ItemUrls =
    { href: string
      label: Option<string>
      primary: Option<bool> }
    ///Creates an instance of ItemUrls with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (href: string): ItemUrls =
        { href = href
          label = None
          primary = None }

type VaultFromItem =
    { id: string }
    ///Creates an instance of VaultFromItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string): VaultFromItem = { id = id }

type Item =
    { category: ItemCategory
      createdAt: Option<System.DateTimeOffset>
      favorite: Option<bool>
      id: Option<string>
      lastEditedBy: Option<string>
      state: Option<ItemState>
      tags: Option<list<string>>
      title: Option<string>
      updatedAt: Option<System.DateTimeOffset>
      urls: Option<list<ItemUrls>>
      vault: VaultFromItem
      version: Option<int> }
    ///Creates an instance of Item with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (category: ItemCategory, vault: VaultFromItem): Item =
        { category = category
          createdAt = None
          favorite = None
          id = None
          lastEditedBy = None
          state = None
          tags = None
          title = None
          updatedAt = None
          urls = None
          vault = vault
          version = None }

///The state of a registered server dependency.
type ServiceDependency =
    { ///Human-readable message for explaining the current state.
      message: Option<string>
      service: Option<string>
      status: Option<string> }
    ///Creates an instance of ServiceDependency with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ServiceDependency =
        { message = None
          service = None
          status = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type VaultType =
    | [<CompiledName "USER_CREATED">] USER_CREATED
    | [<CompiledName "PERSONAL">] PERSONAL
    | [<CompiledName "EVERYONE">] EVERYONE
    | [<CompiledName "TRANSFER">] TRANSFER
    member this.Format() =
        match this with
        | USER_CREATED -> "USER_CREATED"
        | PERSONAL -> "PERSONAL"
        | EVERYONE -> "EVERYONE"
        | TRANSFER -> "TRANSFER"

type Vault =
    { ///The vault version
      attributeVersion: Option<int>
      ///The version of the vault contents
      contentVersion: Option<int>
      createdAt: Option<System.DateTimeOffset>
      description: Option<string>
      id: Option<string>
      ///Number of active items in the vault
      items: Option<int>
      name: Option<string>
      ``type``: Option<VaultType>
      updatedAt: Option<System.DateTimeOffset> }
    ///Creates an instance of Vault with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Vault =
        { attributeVersion = None
          contentVersion = None
          createdAt = None
          description = None
          id = None
          items = None
          name = None
          ``type`` = None
          updatedAt = None }

[<RequireQualifiedAccess>]
type GetApiActivity =
    ///OK
    | OK of payload: list<APIRequest>
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse

type GetServerHealth_OK =
    { dependencies: Option<list<ServiceDependency>>
      name: string
      ///The Connect server's version
      version: string }

[<RequireQualifiedAccess>]
type GetServerHealth =
    ///OK
    | OK of payload: GetServerHealth_OK

[<RequireQualifiedAccess>]
type GetHeartbeat =
    ///OK
    | OK of text: string

[<RequireQualifiedAccess>]
type GetPrometheusMetrics =
    ///Successfully returned Prometheus metrics
    | OK of text: string

[<RequireQualifiedAccess>]
type GetVaults =
    ///OK
    | OK of payload: list<Vault>
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse

[<RequireQualifiedAccess>]
type GetVaultById =
    ///OK
    | OK of payload: Vault
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Vault not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type GetVaultItems =
    ///OK
    | OK of payload: list<Item>
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Vault not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type CreateVaultItem =
    ///OK
    | OK of payload: FullItem
    ///Unable to create item due to invalid input
    | BadRequest of payload: ErrorResponse
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type DeleteVaultItem =
    ///Successfully deleted an item
    | NoContent
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type GetVaultItemById =
    ///OK
    | OK of payload: FullItem
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type PatchVaultItem =
    ///OK - Item updated. If no Patch operations were provided, Item is unmodified.
    | OK of payload: FullItem
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type UpdateVaultItem =
    ///OK
    | OK of payload: FullItem
    ///Unable to create item due to invalid input
    | BadRequest of payload: ErrorResponse
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type GetItemFiles =
    ///OK
    | OK of payload: list<File>
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Item not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type GetDetailsOfFileById =
    ///OK
    | OK of payload: File
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///Unauthorized access
    | Forbidden of payload: ErrorResponse
    ///File not found
    | NotFound of payload: ErrorResponse

[<RequireQualifiedAccess>]
type DownloadFileByID =
    ///Success
    | OK of payload: byte []
    ///Invalid or missing token
    | Unauthorized of payload: ErrorResponse
    ///File not found
    | NotFound of payload: ErrorResponse
