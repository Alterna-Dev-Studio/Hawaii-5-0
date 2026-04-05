namespace rec IpGeolocationApi.Types

type Connection =
    { autonomous_system_number: Option<int>
      autonomous_system_organization: Option<string>
      connection_type: Option<string>
      isp_name: Option<string>
      organization_name: Option<string> }
    ///Creates an instance of Connection with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Connection =
        { autonomous_system_number = None
          autonomous_system_organization = None
          connection_type = None
          isp_name = None
          organization_name = None }

type Currency =
    { currency_code: Option<string>
      currency_name: Option<string> }
    ///Creates an instance of Currency with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Currency =
        { currency_code = None
          currency_name = None }

type Flag =
    { emoji: Option<string>
      png: Option<string>
      svg: Option<string>
      unicode: Option<string> }
    ///Creates an instance of Flag with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Flag =
        { emoji = None
          png = None
          svg = None
          unicode = None }

type Security =
    { is_vpn: Option<bool> }
    ///Creates an instance of Security with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Security = { is_vpn = None }

type Timezone =
    { abbreviation: Option<string>
      current_time: Option<string>
      gmt_offset: Option<int>
      is_dst: Option<bool>
      name: Option<string> }
    ///Creates an instance of Timezone with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Timezone =
        { abbreviation = None
          current_time = None
          gmt_offset = None
          is_dst = None
          name = None }

type inlineresponse200 =
    { city: Option<string>
      city_geoname_id: Option<int>
      connection: Option<Connection>
      continent: Option<string>
      continent_code: Option<string>
      continent_geoname_id: Option<int>
      country: Option<string>
      country_code: Option<string>
      country_geoname_id: Option<int>
      country_is_eu: Option<bool>
      currency: Option<Currency>
      flag: Option<Flag>
      ip_address: Option<string>
      latitude: Option<float>
      longitude: Option<float>
      postal_code: Option<string>
      region: Option<string>
      region_geoname_id: Option<int>
      region_iso_code: Option<string>
      security: Option<Security>
      timezone: Option<Timezone> }
    ///Creates an instance of inlineresponse200 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): inlineresponse200 =
        { city = None
          city_geoname_id = None
          connection = None
          continent = None
          continent_code = None
          continent_geoname_id = None
          country = None
          country_code = None
          country_geoname_id = None
          country_is_eu = None
          currency = None
          flag = None
          ip_address = None
          latitude = None
          longitude = None
          postal_code = None
          region = None
          region_geoname_id = None
          region_iso_code = None
          security = None
          timezone = None }

[<RequireQualifiedAccess>]
type GetV1 =
    ///Location of geolocated IP
    | OK of payload: inlineresponse200
