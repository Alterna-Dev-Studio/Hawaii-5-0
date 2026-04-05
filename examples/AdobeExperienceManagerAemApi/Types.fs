namespace rec AdobeExperienceManagerAemApi.Types

type BundleData =
    { ///Bundle category
      category: Option<string>
      ///Is bundle a fragment
      fragment: Option<bool>
      ///Bundle ID
      id: Option<int>
      ///Bundle name
      name: Option<string>
      props: Option<list<BundleDataProp>>
      ///Bundle state value
      state: Option<string>
      ///Numeric raw bundle state value
      stateRaw: Option<int>
      ///Bundle symbolic name
      symbolicName: Option<string>
      ///Bundle version
      version: Option<string> }
    ///Creates an instance of BundleData with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BundleData =
        { category = None
          fragment = None
          id = None
          name = None
          props = None
          state = None
          stateRaw = None
          symbolicName = None
          version = None }

type BundleDataProp =
    { ///Bundle data key
      key: Option<string>
      ///Bundle data value
      value: Option<string> }
    ///Creates an instance of BundleDataProp with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BundleDataProp = { key = None; value = None }

type BundleInfo =
    { data: Option<list<BundleData>>
      s: Option<list<int>>
      ///Status description of all bundles
      status: Option<string> }
    ///Creates an instance of BundleInfo with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): BundleInfo = { data = None; s = None; status = None }

type Status =
    { finished: Option<bool>
      itemCount: Option<int> }
    ///Creates an instance of Status with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Status = { finished = None; itemCount = None }

type InstallStatus =
    { status: Option<Status> }
    ///Creates an instance of InstallStatus with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): InstallStatus = { status = None }

type KeystoreChainItems =
    { ///e.g. "CN=Admin"
      issuer: Option<string>
      ///e.g. "Sun Jun 30 23:59:50 AEST 2019"
      notAfter: Option<string>
      ///e.g. "Sun Jul 01 12:00:00 AEST 2018"
      notBefore: Option<string>
      ///18165099476682912368
      serialNumber: Option<int>
      ///e.g. "CN=localhost"
      subject: Option<string> }
    ///Creates an instance of KeystoreChainItems with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): KeystoreChainItems =
        { issuer = None
          notAfter = None
          notBefore = None
          serialNumber = None
          subject = None }

type KeystoreInfo =
    { aliases: Option<list<KeystoreItems>>
      ///False if truststore don't exist
      exists: Option<bool> }
    ///Creates an instance of KeystoreInfo with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): KeystoreInfo = { aliases = None; exists = None }

type KeystoreItems =
    { ///e.g. "RSA"
      algorithm: Option<string>
      ///Keystore alias name
      alias: Option<string>
      chain: Option<list<KeystoreChainItems>>
      ///e.g. "privateKey"
      entryType: Option<string>
      ///e.g. "PKCS#8"
      format: Option<string> }
    ///Creates an instance of KeystoreItems with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): KeystoreItems =
        { algorithm = None
          alias = None
          chain = None
          entryType = None
          format = None }

type SamlConfigurationInfo =
    { ///needed for configuration binding
      bundle_location: Option<string>
      ///Title
      description: Option<string>
      ///Persistent Identity (PID)
      pid: Option<string>
      properties: Option<SamlConfigurationProperties>
      ///needed for configuraiton binding
      service_location: Option<string>
      ///Title
      title: Option<string> }
    ///Creates an instance of SamlConfigurationInfo with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SamlConfigurationInfo =
        { bundle_location = None
          description = None
          pid = None
          properties = None
          service_location = None
          title = None }

type SamlConfigurationProperties =
    { addGroupMemberships: Option<SamlConfigurationPropertyItemsBoolean>
      assertionConsumerServiceURL: Option<SamlConfigurationPropertyItemsString>
      clockTolerance: Option<SamlConfigurationPropertyItemsLong>
      createUser: Option<SamlConfigurationPropertyItemsBoolean>
      defaultGroups: Option<SamlConfigurationPropertyItemsArray>
      defaultRedirectUrl: Option<SamlConfigurationPropertyItemsString>
      digestMethod: Option<SamlConfigurationPropertyItemsString>
      groupMembershipAttribute: Option<SamlConfigurationPropertyItemsString>
      handleLogout: Option<SamlConfigurationPropertyItemsBoolean>
      idpCertAlias: Option<SamlConfigurationPropertyItemsString>
      idpHttpRedirect: Option<SamlConfigurationPropertyItemsBoolean>
      idpUrl: Option<SamlConfigurationPropertyItemsString>
      keyStorePassword: Option<SamlConfigurationPropertyItemsString>
      logoutUrl: Option<SamlConfigurationPropertyItemsString>
      nameIdFormat: Option<SamlConfigurationPropertyItemsString>
      path: Option<SamlConfigurationPropertyItemsArray>
      ``service.ranking``: Option<SamlConfigurationPropertyItemsLong>
      serviceProviderEntityId: Option<SamlConfigurationPropertyItemsString>
      signatureMethod: Option<SamlConfigurationPropertyItemsString>
      spPrivateKeyAlias: Option<SamlConfigurationPropertyItemsString>
      synchronizeAttributes: Option<SamlConfigurationPropertyItemsArray>
      useEncryption: Option<SamlConfigurationPropertyItemsBoolean>
      userIDAttribute: Option<SamlConfigurationPropertyItemsString>
      userIntermediatePath: Option<SamlConfigurationPropertyItemsString> }

type SamlConfigurationPropertyItemsArray =
    { ///Property description
      description: Option<string>
      ///True if property is set
      is_set: Option<bool>
      ///property name
      name: Option<string>
      ///True if optional
      optional: Option<bool>
      ///Property type, 1=String, 3=long, 11=boolean, 12=Password
      ``type``: Option<int>
      ///Property value
      values: Option<list<string>> }
    ///Creates an instance of SamlConfigurationPropertyItemsArray with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SamlConfigurationPropertyItemsArray =
        { description = None
          is_set = None
          name = None
          optional = None
          ``type`` = None
          values = None }

type SamlConfigurationPropertyItemsBoolean =
    { ///Property description
      description: Option<string>
      ///True if property is set
      is_set: Option<bool>
      ///property name
      name: Option<string>
      ///True if optional
      optional: Option<bool>
      ///Property type, 1=String, 3=long, 11=boolean, 12=Password
      ``type``: Option<int>
      ///Property value
      value: Option<bool> }
    ///Creates an instance of SamlConfigurationPropertyItemsBoolean with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SamlConfigurationPropertyItemsBoolean =
        { description = None
          is_set = None
          name = None
          optional = None
          ``type`` = None
          value = None }

type SamlConfigurationPropertyItemsLong =
    { ///Property description
      description: Option<string>
      ///True if property is set
      is_set: Option<bool>
      ///property name
      name: Option<string>
      ///True if optional
      optional: Option<bool>
      ///Property type, 1=String, 3=long, 11=boolean, 12=Password
      ``type``: Option<int>
      ///Property value
      value: Option<int> }
    ///Creates an instance of SamlConfigurationPropertyItemsLong with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SamlConfigurationPropertyItemsLong =
        { description = None
          is_set = None
          name = None
          optional = None
          ``type`` = None
          value = None }

type SamlConfigurationPropertyItemsString =
    { ///Property description
      description: Option<string>
      ///True if property is set
      is_set: Option<bool>
      ///property name
      name: Option<string>
      ///True if optional
      optional: Option<bool>
      ///Property type, 1=String, 3=long, 11=boolean, 12=Password
      ``type``: Option<int>
      ///Property value
      value: Option<string> }
    ///Creates an instance of SamlConfigurationPropertyItemsString with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): SamlConfigurationPropertyItemsString =
        { description = None
          is_set = None
          name = None
          optional = None
          ``type`` = None
          value = None }

type TruststoreInfo =
    { aliases: Option<list<TruststoreItems>>
      ///False if truststore don't exist
      exists: Option<bool> }
    ///Creates an instance of TruststoreInfo with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): TruststoreInfo = { aliases = None; exists = None }

type TruststoreItems =
    { ///Truststore alias name
      alias: Option<string>
      entryType: Option<string>
      ///e.g. "CN=Admin"
      issuer: Option<string>
      ///e.g. "Sun Jun 30 23:59:50 AEST 2019"
      notAfter: Option<string>
      ///e.g. "Sun Jul 01 12:00:00 AEST 2018"
      notBefore: Option<string>
      ///18165099476682912368
      serialNumber: Option<int>
      ///e.g. "CN=localhost"
      subject: Option<string> }
    ///Creates an instance of TruststoreItems with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): TruststoreItems =
        { alias = None
          entryType = None
          issuer = None
          notAfter = None
          notBefore = None
          serialNumber = None
          subject = None }

[<RequireQualifiedAccess>]
type PostCqActions =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigAdobeGraniteSamlAuthenticationHandler =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigAemPasswordReset =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigAemHealthCheckServlet =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigApacheFelixJettyBasedHttpService =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigApacheHttpComponentsProxyConfiguration =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigApacheSlingDavExServlet =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigApacheSlingReferrerFilter =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigApacheSlingGetServlet =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostConfigProperty =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetQuery =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type PostQuery =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type PostSetPassword =
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type GetInstallStatus =
    ///Retrieved CRX package manager install status
    | OK of payload: InstallStatus
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type PostPackageService =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostPackageServiceJson =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type GetPackageManagerServlet =
    ///Package Manager Servlet is disabled
    | NotFound
    ///Package Manager Servlet is active
    | MethodNotAllowed
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostPackageUpdate =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type GetCrxdeStatus =
    ///CRXDE is enabled
    | OK
    ///CRXDE is disabled
    | NotFound

[<RequireQualifiedAccess>]
type GetPackage =
    ///Default response
    | DefaultResponse of payload: byte []

[<RequireQualifiedAccess>]
type GetPackageFilter =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type GetAgents =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type DeleteAgent =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetAgent =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostAgent =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostTruststorePKCS12 =
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type GetTruststore =
    ///Default response
    | DefaultResponse of payload: byte []

[<RequireQualifiedAccess>]
type GetLoginPage =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostAuthorizables =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type SslSetup =
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type PostTruststore =
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type GetTruststoreInfo =
    ///Retrieved AEM Truststore info
    | OK of payload: TruststoreInfo
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type PostTreeActivation =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostBundle =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetBundleInfo =
    ///Retrieved bundle info
    | OK of payload: BundleInfo
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type GetConfigMgr =
    ///OK
    | OK

[<RequireQualifiedAccess>]
type PostSamlConfiguration =
    ///Retrieved AEM SAML Configuration
    | OK of text: string
    ///Default response
    | Found of text: string
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type PostJmxRepository =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetAemProductInfo =
    ///Default response
    | DefaultResponse of payload: list<string>

[<RequireQualifiedAccess>]
type GetAemHealthCheck =
    ///Default response
    | DefaultResponse of payload: string

[<RequireQualifiedAccess>]
type PostAuthorizableKeystore =
    ///Retrieved Authorizable Keystore info
    | OK of text: string
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type GetAuthorizableKeystore =
    ///Retrieved Authorizable Keystore info
    | OK of text: string
    ///Default response
    | DefaultResponse of text: string

[<RequireQualifiedAccess>]
type GetKeystore =
    ///Default response
    | DefaultResponse of payload: byte []

[<RequireQualifiedAccess>]
type PostPath =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type DeleteNode =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type GetNode =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostNode =
    ///Default response
    | DefaultResponse

[<RequireQualifiedAccess>]
type PostNodeRw =
    ///Default response
    | DefaultResponse
