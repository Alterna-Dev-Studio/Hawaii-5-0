namespace rec AdafruitIoRestApi.Types

type Activity =
    { action: Option<string>
      created_at: Option<string>
      data: Option<Newtonsoft.Json.Linq.JObject>
      id: Option<float>
      model: Option<string>
      updated_at: Option<string>
      user_id: Option<float> }
    ///Creates an instance of Activity with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Activity =
        { action = None
          created_at = None
          data = None
          id = None
          model = None
          updated_at = None
          user_id = None }

type Block =
    { block_feeds: Option<list<BlockFeed>>
      column: Option<float>
      description: Option<string>
      key: Option<string>
      name: Option<string>
      row: Option<float>
      size_x: Option<float>
      size_y: Option<float>
      visual_type: Option<string> }
    ///Creates an instance of Block with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Block =
        { block_feeds = None
          column = None
          description = None
          key = None
          name = None
          row = None
          size_x = None
          size_y = None
          visual_type = None }

type BlockFeed =
    { feed: Option<Feed>
      group: Option<Group>
      id: Option<string> }
    ///Creates an instance of BlockFeed with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BlockFeed = { feed = None; group = None; id = None }

type Dashboard =
    { blocks: Option<list<Block>>
      description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of Dashboard with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Dashboard =
        { blocks = None
          description = None
          key = None
          name = None }

type Data =
    { completed_at: Option<string>
      created_at: Option<string>
      created_epoch: Option<float>
      ele: Option<float>
      expiration: Option<string>
      feed_id: Option<float>
      group_id: Option<float>
      id: Option<string>
      lat: Option<float>
      lon: Option<float>
      updated_at: Option<string>
      value: Option<string> }
    ///Creates an instance of Data with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Data =
        { completed_at = None
          created_at = None
          created_epoch = None
          ele = None
          expiration = None
          feed_id = None
          group_id = None
          id = None
          lat = None
          lon = None
          updated_at = None
          value = None }

type DataResponse =
    { completed_at: Option<string>
      created_at: Option<string>
      created_epoch: Option<float>
      ele: Option<float>
      expiration: Option<string>
      feed_id: Option<float>
      group_id: Option<float>
      id: Option<string>
      lat: Option<float>
      lon: Option<float>
      updated_at: Option<string>
      value: Option<string> }
    ///Creates an instance of DataResponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): DataResponse =
        { completed_at = None
          created_at = None
          created_epoch = None
          ele = None
          expiration = None
          feed_id = None
          group_id = None
          id = None
          lat = None
          lon = None
          updated_at = None
          value = None }

type Error =
    { code: Option<string>
      message: Option<string> }
    ///Creates an instance of Error with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Error = { code = None; message = None }

type CountAndFirstAndLast =
    { ///Number of data points stored by this feed.
      count: Option<int>
      first: Option<Map<string, Data>>
      last: Option<Map<string, Data>> }
    ///Creates an instance of CountAndFirstAndLast with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CountAndFirstAndLast =
        { count = None
          first = None
          last = None }

///Additional details about this feed.
type Details =
    { data: Option<CountAndFirstAndLast>
      ///Access control list for this feed
      shared_with: Option<Newtonsoft.Json.Linq.JArray> }
    ///Creates an instance of Details with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Details = { data = None; shared_with = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Visibility =
    | [<CompiledName "private">] Private
    | [<CompiledName "public">] Public
    member this.Format() =
        match this with
        | Private -> "private"
        | Public -> "public"

type Feed =
    { created_at: Option<string>
      description: Option<string>
      ///Additional details about this feed.
      details: Option<Details>
      enabled: Option<bool>
      group: Option<Map<string, ShallowGroup>>
      groups: Option<list<ShallowGroup>>
      history: Option<bool>
      id: Option<float>
      key: Option<string>
      last_value: Option<string>
      license: Option<string>
      name: Option<string>
      status: Option<string>
      ///Is status notification active?
      status_notify: Option<bool>
      ///Status notification timeout in minutes.
      status_timeout: Option<int>
      unit_symbol: Option<string>
      unit_type: Option<string>
      updated_at: Option<string>
      visibility: Option<Visibility> }
    ///Creates an instance of Feed with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Feed =
        { created_at = None
          description = None
          details = None
          enabled = None
          group = None
          groups = None
          history = None
          id = None
          key = None
          last_value = None
          license = None
          name = None
          status = None
          status_notify = None
          status_timeout = None
          unit_symbol = None
          unit_type = None
          updated_at = None
          visibility = None }

type Group =
    { created_at: Option<string>
      description: Option<string>
      feeds: Option<list<Feed>>
      id: Option<float>
      name: Option<string>
      updated_at: Option<string> }
    ///Creates an instance of Group with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Group =
        { created_at = None
          description = None
          feeds = None
          id = None
          name = None
          updated_at = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Model =
    | [<CompiledName "feed">] Feed
    | [<CompiledName "group">] Group
    | [<CompiledName "dashboard">] Dashboard
    member this.Format() =
        match this with
        | Feed -> "feed"
        | Group -> "group"
        | Dashboard -> "dashboard"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Scope =
    | [<CompiledName "secret">] Secret
    | [<CompiledName "public">] Public
    | [<CompiledName "user">] User
    | [<CompiledName "organization">] Organization
    member this.Format() =
        match this with
        | Secret -> "secret"
        | Public -> "public"
        | User -> "user"
        | Organization -> "organization"

type Permission =
    { created_at: Option<string>
      id: Option<float>
      model: Option<Model>
      object_id: Option<float>
      scope: Option<Scope>
      scope_value: Option<string>
      updated_at: Option<string>
      user_id: Option<float> }
    ///Creates an instance of Permission with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Permission =
        { created_at = None
          id = None
          model = None
          object_id = None
          scope = None
          scope_value = None
          updated_at = None
          user_id = None }

type ShallowGroup =
    { created_at: Option<string>
      description: Option<string>
      id: Option<float>
      name: Option<string>
      updated_at: Option<string> }
    ///Creates an instance of ShallowGroup with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ShallowGroup =
        { created_at = None
          description = None
          id = None
          name = None
          updated_at = None }

type Token =
    { token: Option<string> }
    ///Creates an instance of Token with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Token = { token = None }

type Trigger =
    { name: Option<string> }
    ///Creates an instance of Trigger with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Trigger = { name = None }

type User =
    { color: Option<string>
      created_at: Option<string>
      id: Option<float>
      name: Option<string>
      time_zone: Option<string>
      updated_at: Option<string>
      username: Option<string> }
    ///Creates an instance of User with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): User =
        { color = None
          created_at = None
          id = None
          name = None
          time_zone = None
          updated_at = None
          username = None }

[<RequireQualifiedAccess>]
type CurrentUser =
    ///A User record
    | OK of payload: User
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateWebhookFeedDataPayload =
    { value: Option<string> }
    ///Creates an instance of CreateWebhookFeedDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateWebhookFeedDataPayload = { value = None }

[<RequireQualifiedAccess>]
type CreateWebhookFeedData =
    ///New feed data record
    | OK of payload: Data
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type CreateRawWebhookFeedData =
    ///New feed data record
    | OK of payload: Data
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyActivities =
    ///Deleted activities successfully
    | OK
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllActivities =
    ///An array of activities
    | OK of payload: list<Activity>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetActivity =
    ///An array of activities
    | OK of payload: list<Activity>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllDashboards =
    ///An array of dashboards
    | OK of payload: list<Dashboard>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateDashboardPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of CreateDashboardPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateDashboardPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type CreateDashboard =
    ///New Dashboard
    | OK of payload: Dashboard
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllBlocks =
    ///An array of blocks
    | OK of payload: list<Block>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type Blockfeeds =
    { feed_id: Option<string>
      group_id: Option<string> }
    ///Creates an instance of Blockfeeds with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Blockfeeds = { feed_id = None; group_id = None }

type CreateBlockPayload =
    { block_feeds: Option<list<Blockfeeds>>
      column: Option<float>
      dashboard_id: Option<float>
      description: Option<string>
      key: Option<string>
      name: Option<string>
      properties: Option<Newtonsoft.Json.Linq.JObject>
      row: Option<float>
      size_x: Option<float>
      size_y: Option<float>
      visual_type: Option<string> }
    ///Creates an instance of CreateBlockPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateBlockPayload =
        { block_feeds = None
          column = None
          dashboard_id = None
          description = None
          key = None
          name = None
          properties = None
          row = None
          size_x = None
          size_y = None
          visual_type = None }

[<RequireQualifiedAccess>]
type CreateBlock =
    ///New Block
    | OK of payload: Block
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyBlock =
    ///Deleted Block successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetBlock =
    ///Block response
    | OK of payload: Block
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

type UpdateBlockPayloadBlockfeeds =
    { feed_id: Option<string>
      group_id: Option<string> }
    ///Creates an instance of UpdateBlockPayloadBlockfeeds with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateBlockPayloadBlockfeeds = { feed_id = None; group_id = None }

type UpdateBlockPayload =
    { block_feeds: Option<list<UpdateBlockPayloadBlockfeeds>>
      column: Option<float>
      dashboard_id: Option<float>
      description: Option<string>
      key: Option<string>
      name: Option<string>
      properties: Option<Newtonsoft.Json.Linq.JObject>
      row: Option<float>
      size_x: Option<float>
      size_y: Option<float>
      visual_type: Option<string> }
    ///Creates an instance of UpdateBlockPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateBlockPayload =
        { block_feeds = None
          column = None
          dashboard_id = None
          description = None
          key = None
          name = None
          properties = None
          row = None
          size_x = None
          size_y = None
          visual_type = None }

[<RequireQualifiedAccess>]
type UpdateBlock =
    ///Updated Block
    | OK of payload: Block
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceBlockPayloadBlockfeeds =
    { feed_id: Option<string>
      group_id: Option<string> }
    ///Creates an instance of ReplaceBlockPayloadBlockfeeds with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceBlockPayloadBlockfeeds = { feed_id = None; group_id = None }

type ReplaceBlockPayload =
    { block_feeds: Option<list<ReplaceBlockPayloadBlockfeeds>>
      column: Option<float>
      dashboard_id: Option<float>
      description: Option<string>
      key: Option<string>
      name: Option<string>
      properties: Option<Newtonsoft.Json.Linq.JObject>
      row: Option<float>
      size_x: Option<float>
      size_y: Option<float>
      visual_type: Option<string> }
    ///Creates an instance of ReplaceBlockPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceBlockPayload =
        { block_feeds = None
          column = None
          dashboard_id = None
          description = None
          key = None
          name = None
          properties = None
          row = None
          size_x = None
          size_y = None
          visual_type = None }

[<RequireQualifiedAccess>]
type ReplaceBlock =
    ///Updated block
    | OK of payload: Block
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyDashboard =
    ///Deleted Dashboard successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetDashboard =
    ///Dashboard response
    | OK of payload: Dashboard
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

type UpdateDashboardPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of UpdateDashboardPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateDashboardPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type UpdateDashboard =
    ///Updated Dashboard
    | OK of payload: Dashboard
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceDashboardPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of ReplaceDashboardPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceDashboardPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type ReplaceDashboard =
    ///Updated dashboard
    | OK of payload: Dashboard
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllFeeds =
    ///An array of feeds
    | OK of payload: list<Feed>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateFeedPayload =
    { description: Option<string>
      key: Option<string>
      license: Option<string>
      name: Option<string> }
    ///Creates an instance of CreateFeedPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateFeedPayload =
        { description = None
          key = None
          license = None
          name = None }

[<RequireQualifiedAccess>]
type CreateFeed =
    ///New feed
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyFeed =
    ///Deleted feed successfully
    | OK
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetFeed =
    ///Feed response
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type UpdateFeedPayload =
    { description: Option<string>
      key: Option<string>
      license: Option<string>
      name: Option<string> }
    ///Creates an instance of UpdateFeedPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateFeedPayload =
        { description = None
          key = None
          license = None
          name = None }

[<RequireQualifiedAccess>]
type UpdateFeed =
    ///Updated feed
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceFeedPayload =
    { description: Option<string>
      key: Option<string>
      license: Option<string>
      name: Option<string> }
    ///Creates an instance of ReplaceFeedPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceFeedPayload =
        { description = None
          key = None
          license = None
          name = None }

[<RequireQualifiedAccess>]
type ReplaceFeed =
    ///Updated feed
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllData =
    ///An array of data
    | OK of payload: list<DataResponse>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateDataPayload =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of CreateDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateDataPayload =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

[<RequireQualifiedAccess>]
type CreateData =
    ///New data
    | OK of payload: Data
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type BatchCreateDataPayloadArrayItem =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of BatchCreateDataPayloadArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BatchCreateDataPayloadArrayItem =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

type BatchCreateDataPayload = list<BatchCreateDataPayloadArrayItem>

[<RequireQualifiedAccess>]
type BatchCreateData =
    ///New data
    | OK of payload: list<DataResponse>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type IdAndKeyAndName =
    { id: Option<int>
      key: Option<string>
      name: Option<string> }

type ChartData_OK =
    { ///The names of the columns returned as data.
      columns: Option<list<string>>
      ///The actual chart data.
      data: Option<list<list<string>>>
      feed: Option<IdAndKeyAndName>
      parameters: Option<Newtonsoft.Json.Linq.JObject> }

[<RequireQualifiedAccess>]
type ChartData =
    ///A JSON record containing chart data and the parameters used to generate it.
    | OK of payload: ChartData_OK
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type FirstData =
    ///Data response
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type LastData =
    ///Data response
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type NextData =
    ///Data response
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type PreviousData =
    ///Data response
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type RetainData =
    ///CSV string in `value,lat,lon,ele` format. The lat, lon, and ele values are left blank if they are not set.
    | OK
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyData =
    ///Deleted Group successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetData =
    ///Data response
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type UpdateDataPayload =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of UpdateDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateDataPayload =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

[<RequireQualifiedAccess>]
type UpdateData =
    ///Updated Data
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceDataPayload =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of ReplaceDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceDataPayload =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

[<RequireQualifiedAccess>]
type ReplaceData =
    ///Updated Data
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetFeedDetails =
    ///Feed response
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllGroups =
    ///An array of groups
    | OK of payload: list<Group>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateGroupPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of CreateGroupPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateGroupPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type CreateGroup =
    ///New Group
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyGroup =
    ///Deleted Group successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetGroup =
    ///Group response
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

type UpdateGroupPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of UpdateGroupPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateGroupPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type UpdateGroup =
    ///Updated Group
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceGroupPayload =
    { description: Option<string>
      key: Option<string>
      name: Option<string> }
    ///Creates an instance of ReplaceGroupPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceGroupPayload =
        { description = None
          key = None
          name = None }

[<RequireQualifiedAccess>]
type ReplaceGroup =
    ///Updated group
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AddFeedToGroup =
    ///Updated group
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type Feeds =
    { key: string
      value: string }
    ///Creates an instance of Feeds with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (key: string, value: string): Feeds = { key = key; value = value }

///A location record with `lat`, `lon`, and [optional] `ele` properties.
type Location =
    { ele: Option<float>
      lat: float
      lon: float }
    ///Creates an instance of Location with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (lat: float, lon: float): Location = { ele = None; lat = lat; lon = lon }

type CreateGroupDataPayload =
    { ///Optional created_at timestamp which will be applied to all feed values created.
      created_at: Option<string>
      ///An array of feed data records with `key` and `value` properties.
      feeds: list<Feeds>
      ///A location record with `lat`, `lon`, and [optional] `ele` properties.
      location: Option<Location> }
    ///Creates an instance of CreateGroupDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (feeds: list<Feeds>): CreateGroupDataPayload =
        { created_at = None
          feeds = feeds
          location = None }

[<RequireQualifiedAccess>]
type CreateGroupData =
    ///New data
    | OK of payload: list<DataResponse>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllGroupFeeds =
    ///An array of feeds
    | OK of payload: list<Feed>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateGroupFeedPayload =
    { description: Option<string>
      key: Option<string>
      license: Option<string>
      name: Option<string> }
    ///Creates an instance of CreateGroupFeedPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateGroupFeedPayload =
        { description = None
          key = None
          license = None
          name = None }

[<RequireQualifiedAccess>]
type CreateGroupFeed =
    ///New feed
    | OK of payload: Feed
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllGroupFeedData =
    ///An array of data
    | OK of payload: list<DataResponse>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateGroupFeedDataPayload =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of CreateGroupFeedDataPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateGroupFeedDataPayload =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

[<RequireQualifiedAccess>]
type CreateGroupFeedData =
    ///New data
    | OK of payload: DataResponse
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type BatchCreateGroupFeedDataPayloadArrayItem =
    { created_at: Option<string>
      ele: Option<string>
      epoch: Option<float>
      lat: Option<string>
      lon: Option<string>
      value: Option<string> }
    ///Creates an instance of BatchCreateGroupFeedDataPayloadArrayItem with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BatchCreateGroupFeedDataPayloadArrayItem =
        { created_at = None
          ele = None
          epoch = None
          lat = None
          lon = None
          value = None }

type BatchCreateGroupFeedDataPayload = list<BatchCreateGroupFeedDataPayloadArrayItem>

[<RequireQualifiedAccess>]
type BatchCreateGroupFeedData =
    ///New data
    | OK of payload: list<DataResponse>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type RemoveFeedFromGroup =
    ///Updated group
    | OK of payload: Group
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type GetCurrentUserThrottle_OK =
    { ///Actions taken inside the time window.
      active_data_rate: Option<int>
      ///Max possible actions inside the time window (usually 1 minute).
      data_rate_limit: Option<int> }

[<RequireQualifiedAccess>]
type GetCurrentUserThrottle =
    ///Data rate limit and current actions.
    | OK of payload: GetCurrentUserThrottle_OK
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllTokens =
    ///An array of tokens
    | OK of payload: list<Token>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateTokenPayload =
    { token: Option<string> }
    ///Creates an instance of CreateTokenPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateTokenPayload = { token = None }

[<RequireQualifiedAccess>]
type CreateToken =
    ///New Token
    | OK of payload: Token
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyToken =
    ///Deleted Token successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetToken =
    ///Token response
    | OK of payload: Token
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

type UpdateTokenPayload =
    { token: Option<string> }
    ///Creates an instance of UpdateTokenPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateTokenPayload = { token = None }

[<RequireQualifiedAccess>]
type UpdateToken =
    ///Updated Token
    | OK of payload: Token
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceTokenPayload =
    { token: Option<string> }
    ///Creates an instance of ReplaceTokenPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceTokenPayload = { token = None }

[<RequireQualifiedAccess>]
type ReplaceToken =
    ///Updated token
    | OK of payload: Token
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllTriggers =
    ///An array of triggers
    | OK of payload: list<Trigger>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type CreateTriggerPayload =
    { name: Option<string> }
    ///Creates an instance of CreateTriggerPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreateTriggerPayload = { name = None }

[<RequireQualifiedAccess>]
type CreateTrigger =
    ///New Trigger
    | OK of payload: Trigger
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyTrigger =
    ///Deleted Trigger successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetTrigger =
    ///Trigger response
    | OK of payload: Trigger
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

type UpdateTriggerPayload =
    { name: Option<string> }
    ///Creates an instance of UpdateTriggerPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdateTriggerPayload = { name = None }

[<RequireQualifiedAccess>]
type UpdateTrigger =
    ///Updated Trigger
    | OK of payload: Trigger
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

type ReplaceTriggerPayload =
    { name: Option<string> }
    ///Creates an instance of ReplaceTriggerPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplaceTriggerPayload = { name = None }

[<RequireQualifiedAccess>]
type ReplaceTrigger =
    ///Updated trigger
    | OK of payload: Trigger
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type AllPermissions =
    ///An array of permissions
    | OK of payload: list<Permission>
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Mode =
    | [<CompiledName "r">] R
    | [<CompiledName "w">] W
    | [<CompiledName "rw">] Rw
    member this.Format() =
        match this with
        | R -> "r"
        | W -> "w"
        | Rw -> "rw"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type CreatePermissionPayloadScope =
    | [<CompiledName "secret">] Secret
    | [<CompiledName "public">] Public
    | [<CompiledName "user">] User
    | [<CompiledName "organization">] Organization
    member this.Format() =
        match this with
        | Secret -> "secret"
        | Public -> "public"
        | User -> "user"
        | Organization -> "organization"

type CreatePermissionPayload =
    { mode: Option<Mode>
      scope: Option<CreatePermissionPayloadScope>
      scope_value: Option<string> }
    ///Creates an instance of CreatePermissionPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CreatePermissionPayload =
        { mode = None
          scope = None
          scope_value = None }

[<RequireQualifiedAccess>]
type CreatePermission =
    ///New Permission
    | OK of payload: Permission
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type DestroyPermission =
    ///Deleted Permission successfully
    | OK of payload: string
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<RequireQualifiedAccess>]
type GetPermission =
    ///Permission response
    | OK of payload: Permission
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error"
    | InternalServerError

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type UpdatePermissionPayloadMode =
    | [<CompiledName "r">] R
    | [<CompiledName "w">] W
    | [<CompiledName "rw">] Rw
    member this.Format() =
        match this with
        | R -> "r"
        | W -> "w"
        | Rw -> "rw"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type UpdatePermissionPayloadScope =
    | [<CompiledName "secret">] Secret
    | [<CompiledName "public">] Public
    | [<CompiledName "user">] User
    | [<CompiledName "organization">] Organization
    member this.Format() =
        match this with
        | Secret -> "secret"
        | Public -> "public"
        | User -> "user"
        | Organization -> "organization"

type UpdatePermissionPayload =
    { mode: Option<UpdatePermissionPayloadMode>
      scope: Option<UpdatePermissionPayloadScope>
      scope_value: Option<string> }
    ///Creates an instance of UpdatePermissionPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): UpdatePermissionPayload =
        { mode = None
          scope = None
          scope_value = None }

[<RequireQualifiedAccess>]
type UpdatePermission =
    ///Updated Permission
    | OK of payload: Permission
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type ReplacePermissionPayloadMode =
    | [<CompiledName "r">] R
    | [<CompiledName "w">] W
    | [<CompiledName "rw">] Rw
    member this.Format() =
        match this with
        | R -> "r"
        | W -> "w"
        | Rw -> "rw"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type ReplacePermissionPayloadScope =
    | [<CompiledName "secret">] Secret
    | [<CompiledName "public">] Public
    | [<CompiledName "user">] User
    | [<CompiledName "organization">] Organization
    member this.Format() =
        match this with
        | Secret -> "secret"
        | Public -> "public"
        | User -> "user"
        | Organization -> "organization"

type ReplacePermissionPayload =
    { mode: Option<ReplacePermissionPayloadMode>
      scope: Option<ReplacePermissionPayloadScope>
      scope_value: Option<string> }
    ///Creates an instance of ReplacePermissionPayload with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ReplacePermissionPayload =
        { mode = None
          scope = None
          scope_value = None }

[<RequireQualifiedAccess>]
type ReplacePermission =
    ///Updated permission
    | OK of payload: Permission
    ///Unauthorized
    | Unauthorized
    ///Forbidden
    | Forbidden
    ///Not Found
    | NotFound
    ///Server Error
    | InternalServerError
