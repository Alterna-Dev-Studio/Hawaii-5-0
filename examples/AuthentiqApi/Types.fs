namespace rec AuthentiqApi.Types

///Authentiq ID in JWT format, self-signed.
type AuthentiqID =
    { ///device token for push messages
      devtoken: Option<string>
      ///UUID and public signing key
      sub: string }
    ///Creates an instance of AuthentiqID with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (sub: string): AuthentiqID = { devtoken = None; sub = sub }

///Claim in JWT format, self- or issuer-signed.
type Claims =
    { email: Option<string>
      phone: Option<string>
      ///claim scope
      scope: string
      ///UUID
      sub: string
      ``type``: Option<string> }
    ///Creates an instance of Claims with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (scope: string, sub: string): Claims =
        { email = None
          phone = None
          scope = scope
          sub = sub
          ``type`` = None }

type Error =
    { detail: Option<string>
      error: int
      title: Option<string>
      ///unique uri for this error
      ``type``: Option<string> }
    ///Creates an instance of Error with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (error: int): Error =
        { detail = None
          error = error
          title = None
          ``type`` = None }

///PushToken in JWT format, self-signed.
type PushToken =
    { ///audience (URI)
      aud: string
      exp: Option<int>
      iat: Option<int>
      ///issuer (URI)
      iss: string
      nbf: Option<int>
      ///UUID and public signing key
      sub: string }
    ///Creates an instance of PushToken with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (aud: string, iss: string, sub: string): PushToken =
        { aud = aud
          exp = None
          iat = None
          iss = iss
          nbf = None
          sub = sub }

type KeyRevokeNosecret_OK =
    { ///pending or done
      status: Option<string> }

[<RequireQualifiedAccess>]
type KeyRevokeNosecret =
    ///Successfully deleted
    | OK of payload: KeyRevokeNosecret_OK
    ///Authentication error `auth-error`
    | Unauthorized of payload: Error
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Confirm with code sent `confirm-first`
    | Conflict of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type KeyRegister_Created =
    { ///revoke key
      secret: Option<string>
      ///registered
      status: Option<string> }

[<RequireQualifiedAccess>]
type KeyRegister =
    ///Successfully registered
    | Created of payload: KeyRegister_Created
    ///Key already registered `duplicate-key`
    | Conflict of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type KeyRevoke_OK =
    { ///done
      status: Option<string> }

[<RequireQualifiedAccess>]
type KeyRevoke =
    ///Successful response
    | OK of payload: KeyRevoke_OK
    ///Key not found / wrong code `auth-error`
    | Unauthorized of payload: Error
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type KeyRetrieve_OK =
    { since: Option<System.DateTimeOffset>
      status: Option<string>
      ///base64safe encoded public signing key
      sub: Option<string> }

[<RequireQualifiedAccess>]
type KeyRetrieve =
    ///Successfully retrieved
    | OK of payload: KeyRetrieve_OK
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

[<RequireQualifiedAccess>]
type HeadKeyByPK =
    ///Key exists
    | OK
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type KeyUpdate_OK =
    { ///confirmed
      status: Option<string> }

[<RequireQualifiedAccess>]
type KeyUpdate =
    ///Successfully updated
    | OK of payload: KeyUpdate_OK
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type KeyBind_OK =
    { ///confirmed
      status: Option<string> }

[<RequireQualifiedAccess>]
type KeyBind =
    ///Successfully updated
    | OK of payload: KeyBind_OK
    ///Unknown key `unknown-key`
    | NotFound of payload: Error
    ///Already bound to another key `duplicate-hash`
    | Conflict of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type PushLoginRequest_OK =
    { ///sent
      status: Option<string> }

[<RequireQualifiedAccess>]
type PushLoginRequest =
    ///Successful response
    | OK of payload: PushLoginRequest_OK
    ///Unauthorized for this callback audience `aud-error` or JWT should be self-signed `auth-error`
    | Unauthorized of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type SignRequest_Created =
    { ///20-character ID
      job: Option<string>
      ///waiting
      status: Option<string> }

[<RequireQualifiedAccess>]
type SignRequest =
    ///Successful response
    | Created of payload: SignRequest_Created
    ///Error response
    | DefaultResponse of payload: Error

type SignDelete_OK =
    { ///done
      status: Option<string> }

[<RequireQualifiedAccess>]
type SignDelete =
    ///Successfully deleted
    | OK of payload: SignDelete_OK
    ///Job not found `unknown-job`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type SignRetrieve_OK =
    { exp: Option<int>
      field: Option<string>
      ///base64safe encoded public signing key
      sub: Option<string> }

[<RequireQualifiedAccess>]
type SignRetrieve =
    ///Successful response (JWT)
    | OK of payload: SignRetrieve_OK
    ///Confirmed, waiting for signing
    | NoContent
    ///Job not found `unknown-job`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

[<RequireQualifiedAccess>]
type SignRetrieveHead =
    ///Confirmed and signed
    | OK
    ///Confirmed, waiting for signing
    | NoContent
    ///Job not found `unknown-job`
    | NotFound of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

type SignConfirm_Accepted =
    { ///confirmed
      status: Option<string> }

[<RequireQualifiedAccess>]
type SignConfirm =
    ///Successfully confirmed
    | Accepted of payload: SignConfirm_Accepted
    ///Confirmation error `auth-error`
    | Unauthorized of payload: Error
    ///Job not found `unknown-job`
    | NotFound of payload: Error
    ///JWT POSTed to scope `not-supported`
    | MethodNotAllowed of payload: Error
    ///Error response
    | DefaultResponse of payload: Error

[<RequireQualifiedAccess>]
type SignUpdate =
    ///Successfully updated
    | OK
    ///Job not found `unknown-job`
    | NotFound
    ///Job not confirmed yet `confirm-first`
    | Conflict
    ///Error response
    | DefaultResponse of payload: Error
