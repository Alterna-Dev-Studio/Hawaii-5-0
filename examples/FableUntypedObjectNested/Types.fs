namespace rec FableUntypedObjectNested.Types

type ProfileListArrayItem =
    { id: string
      displayName: Option<string> }
    ///Creates an instance of ProfileListArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string): ProfileListArrayItem = { id = id; displayName = None }

type ProfileList = list<ProfileListArrayItem>

type Address =
    { street: Option<string>
      city: string }
    ///Creates an instance of Address with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (city: string): Address = { street = None; city = city }

type Metadata =
    { createdAt: Option<System.DateTimeOffset> }
    ///Creates an instance of Metadata with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Metadata = { createdAt = None }

type Profile =
    { id: string
      address: Address
      metadata: Option<Metadata> }
    ///Creates an instance of Profile with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string, address: Address): Profile =
        { id = id
          address = address
          metadata = None }

type SearchProfiles_OK =
    { id: string
      displayName: Option<string> }

[<RequireQualifiedAccess>]
type SearchProfiles =
    ///OK
    | OK of payload: list<SearchProfiles_OK>

[<RequireQualifiedAccess>]
type UploadProfilePicture =
    ///No Content
    | NoContent
