namespace rec EventsApi.Types

type DateTimeRFC3339 = System.DateTimeOffset
type UUID = string

///Metadata gathered about the client
type Client =
    { app_name: Option<string>
      app_version: Option<string>
      ip_address: Option<string>
      os_name: Option<string>
      os_version: Option<string>
      platform_name: Option<string>
      ///Depending on the platform used, this can be the version of the browser that the client extension is installed, the model of computer that the native application is installed or the machine's CPU version that the CLI was installed
      platform_version: Option<string> }
    ///Creates an instance of Client with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Client =
        { app_name = None
          app_version = None
          ip_address = None
          os_name = None
          os_version = None
          platform_name = None
          platform_version = None }

///Cursor
type Cursor =
    { ///Cursor to fetch more data if available or continue the polling process if required
      cursor: Option<string> }
    ///Creates an instance of Cursor with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Cursor = { cursor = None }

///Common cursor properties for collection responses
type CursorCollection =
    { ///Cursor to fetch more data if available or continue the polling process if required
      cursor: Option<string>
      ///Whether there may still be more data to fetch using the returned cursor. If true, the subsequent request could still be empty.
      has_more: Option<bool> }
    ///Creates an instance of CursorCollection with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): CursorCollection = { cursor = None; has_more = None }

///Additional information about the sign-in attempt
type Details =
    { ///For firewall prevented sign-ins, the value is the chosen continent, country, etc. that blocked the sign-in attempt
      value: Option<string> }
    ///Creates an instance of Details with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Details = { value = None }

type ErrorFromError =
    { ///The error message.
      Message: Option<string> }
    ///Creates an instance of ErrorFromError with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ErrorFromError = { Message = None }

type Error =
    { Error: Option<ErrorFromError> }
    ///Creates an instance of Error with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Error = { Error = None }

type Introspection =
    { Features: Option<list<string>>
      IssuedAt: Option<DateTimeRFC3339>
      UUID: Option<string> }
    ///Creates an instance of Introspection with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Introspection =
        { Features = None
          IssuedAt = None
          UUID = None }

///A single item usage object
type ItemUsage =
    { action: Option<Newtonsoft.Json.Linq.JToken>
      ///Metadata gathered about the client
      client: Option<Newtonsoft.Json.Linq.JToken>
      item_uuid: Option<UUID>
      timestamp: Option<DateTimeRFC3339>
      used_version: Option<int>
      ///User object
      user: Option<Newtonsoft.Json.Linq.JToken>
      uuid: Option<UUID>
      vault_uuid: Option<UUID> }
    ///Creates an instance of ItemUsage with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ItemUsage =
        { action = None
          client = None
          item_uuid = None
          timestamp = None
          used_version = None
          user = None
          uuid = None
          vault_uuid = None }

///An object wrapping cursor properties and a list of items usages
type ItemUsageItems =
    { items: Option<list<ItemUsage>>
      ///Cursor to fetch more data if available or continue the polling process if required
      cursor: Option<string>
      ///Whether there may still be more data to fetch using the returned cursor. If true, the subsequent request could still be empty.
      has_more: Option<bool> }
    ///Creates an instance of ItemUsageItems with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ItemUsageItems =
        { items = None
          cursor = None
          has_more = None }

///Reset cursor
type ResetCursor =
    { end_time: Option<DateTimeRFC3339>
      limit: Option<float>
      start_time: Option<DateTimeRFC3339> }
    ///Creates an instance of ResetCursor with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ResetCursor =
        { end_time = None
          limit = None
          start_time = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Category =
    | [<CompiledName "success">] Success
    | [<CompiledName "credentials_failed">] Credentials_failed
    | [<CompiledName "mfa_failed">] Mfa_failed
    | [<CompiledName "modern_version_failed">] Modern_version_failed
    | [<CompiledName "firewall_failed">] Firewall_failed
    | [<CompiledName "firewall_reported_success">] Firewall_reported_success
    member this.Format() =
        match this with
        | Success -> "success"
        | Credentials_failed -> "credentials_failed"
        | Mfa_failed -> "mfa_failed"
        | Modern_version_failed -> "modern_version_failed"
        | Firewall_failed -> "firewall_failed"
        | Firewall_reported_success -> "firewall_reported_success"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Type =
    | [<CompiledName "credentials_ok">] Credentials_ok
    | [<CompiledName "mfa_ok">] Mfa_ok
    | [<CompiledName "password_secret_bad">] Password_secret_bad
    | [<CompiledName "mfa_missing">] Mfa_missing
    | [<CompiledName "totp_disabled">] Totp_disabled
    | [<CompiledName "totp_bad">] Totp_bad
    | [<CompiledName "totp_timeout">] Totp_timeout
    | [<CompiledName "u2f_disabled">] U2f_disabled
    | [<CompiledName "u2f_bad">] U2f_bad
    | [<CompiledName "u2f_timout">] U2f_timout
    | [<CompiledName "duo_disabled">] Duo_disabled
    | [<CompiledName "duo_bad">] Duo_bad
    | [<CompiledName "duo_timeout">] Duo_timeout
    | [<CompiledName "duo_native_bad">] Duo_native_bad
    | [<CompiledName "platform_secret_disabled">] Platform_secret_disabled
    | [<CompiledName "platform_secret_bad">] Platform_secret_bad
    | [<CompiledName "platform_secret_proxy">] Platform_secret_proxy
    | [<CompiledName "code_disabled">] Code_disabled
    | [<CompiledName "code_bad">] Code_bad
    | [<CompiledName "code_timeout">] Code_timeout
    | [<CompiledName "ip_blocked">] Ip_blocked
    | [<CompiledName "continent_blocked">] Continent_blocked
    | [<CompiledName "country_blocked">] Country_blocked
    | [<CompiledName "anonymous_blocked">] Anonymous_blocked
    | [<CompiledName "all_blocked">] All_blocked
    | [<CompiledName "modern_version_missing">] Modern_version_missing
    | [<CompiledName "modern_version_old">] Modern_version_old
    member this.Format() =
        match this with
        | Credentials_ok -> "credentials_ok"
        | Mfa_ok -> "mfa_ok"
        | Password_secret_bad -> "password_secret_bad"
        | Mfa_missing -> "mfa_missing"
        | Totp_disabled -> "totp_disabled"
        | Totp_bad -> "totp_bad"
        | Totp_timeout -> "totp_timeout"
        | U2f_disabled -> "u2f_disabled"
        | U2f_bad -> "u2f_bad"
        | U2f_timout -> "u2f_timout"
        | Duo_disabled -> "duo_disabled"
        | Duo_bad -> "duo_bad"
        | Duo_timeout -> "duo_timeout"
        | Duo_native_bad -> "duo_native_bad"
        | Platform_secret_disabled -> "platform_secret_disabled"
        | Platform_secret_bad -> "platform_secret_bad"
        | Platform_secret_proxy -> "platform_secret_proxy"
        | Code_disabled -> "code_disabled"
        | Code_bad -> "code_bad"
        | Code_timeout -> "code_timeout"
        | Ip_blocked -> "ip_blocked"
        | Continent_blocked -> "continent_blocked"
        | Country_blocked -> "country_blocked"
        | Anonymous_blocked -> "anonymous_blocked"
        | All_blocked -> "all_blocked"
        | Modern_version_missing -> "modern_version_missing"
        | Modern_version_old -> "modern_version_old"

///A single sign-in attempt object
type SignInAttempt =
    { category: Option<Category>
      ///Metadata gathered about the client
      client: Option<Newtonsoft.Json.Linq.JToken>
      ///Country ISO Code
      country: Option<string>
      ///Additional information about the sign-in attempt
      details: Option<Newtonsoft.Json.Linq.JToken>
      session_uuid: Option<UUID>
      ///User object
      target_user: Option<Newtonsoft.Json.Linq.JToken>
      timestamp: Option<DateTimeRFC3339>
      ``type``: Option<Type>
      uuid: Option<UUID> }
    ///Creates an instance of SignInAttempt with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SignInAttempt =
        { category = None
          client = None
          country = None
          details = None
          session_uuid = None
          target_user = None
          timestamp = None
          ``type`` = None
          uuid = None }

///An object wrapping cursor properties and a list of sign-in attempts
type SignInAttemptItems =
    { items: Option<list<SignInAttempt>>
      ///Cursor to fetch more data if available or continue the polling process if required
      cursor: Option<string>
      ///Whether there may still be more data to fetch using the returned cursor. If true, the subsequent request could still be empty.
      has_more: Option<bool> }
    ///Creates an instance of SignInAttemptItems with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SignInAttemptItems =
        { items = None
          cursor = None
          has_more = None }

///User object
type User =
    { email: Option<string>
      ///Full name
      name: Option<string>
      uuid: Option<UUID> }
    ///Creates an instance of User with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): User =
        { email = None
          name = None
          uuid = None }

[<RequireQualifiedAccess>]
type GetAuthIntrospect =
    ///Introspection object
    | OK of payload: Introspection
    ///Unauthorized
    | Unauthorized of payload: Error
    ///Generic error
    | DefaultResponse of payload: Error

[<RequireQualifiedAccess>]
type GetItemUsages =
    ///Item usages response object
    | OK of payload: ItemUsageItems
    ///Unauthorized
    | Unauthorized of payload: Error
    ///Generic error
    | DefaultResponse of payload: Error

[<RequireQualifiedAccess>]
type GetSignInAttempts =
    ///Sign-in attempts response object
    | OK of payload: SignInAttemptItems
    ///Unauthorized
    | Unauthorized of payload: Error
    ///Generic error
    | DefaultResponse of payload: Error
