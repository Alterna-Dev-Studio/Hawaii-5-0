namespace rec AdafruitIoRestApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open AdafruitIoRestApi.Types
open AdafruitIoRestApi.Http

///### The Internet of Things for Everyone
///The Adafruit IO HTTP API provides access to your Adafruit IO data from any programming language or hardware environment that can speak HTTP. The easiest way to get started is with [an Adafruit IO learn guide](https://learn.adafruit.com/series/adafruit-io-basics) and [a simple Internet of Things capable device like the Feather Huzzah](https://www.adafruit.com/product/2821).
///This API documentation is hosted on GitHub Pages and is available at [https://github.com/adafruit/io-api](https://github.com/adafruit/io-api). For questions or comments visit the [Adafruit IO Forums](https://forums.adafruit.com/viewforum.php?f=56) or the [adafruit-io channel on the Adafruit Discord server](https://discord.gg/adafruit).
///#### Authentication
///Authentication for every API request happens through the `X-AIO-Key` header or query parameter and your IO API key. A simple cURL request to get all available feeds for a user with the username "io_username" and the key "io_key_12345" could look like this:
///    $ curl -H "X-AIO-Key: io_key_12345" https://io.adafruit.com/api/v2/io_username/feeds
///Or like this:
///    $ curl "https://io.adafruit.com/api/v2/io_username/feeds?X-AIO-Key=io_key_12345
///Using the node.js [request](https://github.com/request/request) library, IO HTTP requests are as easy as:
///```js
///var request = require('request');
///var options = {
///  url: 'https://io.adafruit.com/api/v2/io_username/feeds',
///  headers: {
///    'X-AIO-Key': 'io_key_12345',
///    'Content-Type': 'application/json'
///  }
///};
///function callback(error, response, body) {
///  if (!error &amp;&amp; response.statusCode == 200) {
///    var feeds = JSON.parse(body);
///    console.log(feeds.length + " FEEDS AVAILABLE");
///    feeds.forEach(function (feed) {
///      console.log(feed.name, feed.key);
///    })
///  }
///}
///request(options, callback);
///```
///Using the ESP8266 Arduino HTTPClient library, an HTTPS GET request would look like this (replacing `---` with your own values in the appropriate locations):
///```arduino
////// based on
////// https://github.com/esp8266/Arduino/blob/master/libraries/ESP8266HTTPClient/examples/Authorization/Authorization.ino
///#include &amp;lt;Arduino.h&amp;gt;
///#include &amp;lt;ESP8266WiFi.h&amp;gt;
///#include &amp;lt;ESP8266WiFiMulti.h&amp;gt;
///#include &amp;lt;ESP8266HTTPClient.h&amp;gt;
///ESP8266WiFiMulti WiFiMulti;
///const char* ssid = "---";
///const char* password = "---";
///const char* host = "io.adafruit.com";
///const char* io_key = "---";
///const char* path_with_username = "/api/v2/---/dashboards";
///// Use web browser to view and copy
///// SHA1 fingerprint of the certificate
///const char* fingerprint = "77 00 54 2D DA E7 D8 03 27 31 23 99 EB 27 DB CB A5 4C 57 18";
///void setup() {
///  Serial.begin(115200);
///  for(uint8_t t = 4; t &amp;gt; 0; t--) {
///    Serial.printf("[SETUP] WAIT %d...\n", t);
///    Serial.flush();
///    delay(1000);
///  }
///  WiFi.mode(WIFI_STA);
///  WiFiMulti.addAP(ssid, password);
///  // wait for WiFi connection
///  while(WiFiMulti.run() != WL_CONNECTED) {
///    Serial.print('.');
///    delay(1000);
///  }
///  Serial.println("[WIFI] connected!");
///  HTTPClient http;
///  // start request with URL and TLS cert fingerprint for verification
///  http.begin("https://" + String(host) + String(path_with_username), fingerprint);
///  // IO API authentication
///  http.addHeader("X-AIO-Key", io_key);
///  // start connection and send HTTP header
///  int httpCode = http.GET();
///  // httpCode will be negative on error
///  if(httpCode &amp;gt; 0) {
///    // HTTP header has been send and Server response header has been handled
///    Serial.printf("[HTTP] GET response: %d\n", httpCode);
///    // HTTP 200 OK
///    if(httpCode == HTTP_CODE_OK) {
///      String payload = http.getString();
///      Serial.println(payload);
///    }
///    http.end();
///  }
///}
///void loop() {}
///```
///#### Client Libraries
///We have client libraries to help you get started with your project: [Python](https://github.com/adafruit/io-client-python), [Ruby](https://github.com/adafruit/io-client-ruby), [Arduino C++](https://github.com/adafruit/Adafruit_IO_Arduino), [Javascript](https://github.com/adafruit/adafruit-io-node), and [Go](https://github.com/adafruit/io-client-go) are available. They're all open source, so if they don't already do what you want, you can fork and add any feature you'd like.
type AdafruitIoRestApiClient(httpClient: HttpClient) =
    ///<summary>
    ///Get information about the current user
    ///</summary>
    member this.CurrentUser(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []
            let! (status, content) = OpenApiHttp.getAsync httpClient "/user" requestParts cancellationToken

            match int status with
            | 200 -> return CurrentUser.OK(Serializer.deserialize content)
            | 401 -> return CurrentUser.Unauthorized
            | 403 -> return CurrentUser.Forbidden
            | 404 -> return CurrentUser.NotFound
            | _ -> return CurrentUser.InternalServerError
        }

    ///<summary>
    ///Send data to a feed via webhook URL.
    ///</summary>
    member this.CreateWebhookFeedData(payload: CreateWebhookFeedDataPayload, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.jsonContent payload ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/webhooks/feed/:token" requestParts cancellationToken

            match int status with
            | 200 -> return CreateWebhookFeedData.OK(Serializer.deserialize content)
            | 401 -> return CreateWebhookFeedData.Unauthorized
            | 403 -> return CreateWebhookFeedData.Forbidden
            | 404 -> return CreateWebhookFeedData.NotFound
            | _ -> return CreateWebhookFeedData.InternalServerError
        }

    ///<summary>
    ///The raw data webhook receiver accepts POST requests and stores the raw request body on your feed. This is useful when you don't have control of the webhook sender. If feed history is turned on, payloads will be truncated at 1024 bytes. If feed history is turned off, payloads will be truncated at 100KB.
    ///</summary>
    member this.CreateRawWebhookFeedData(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/webhooks/feed/:token/raw" requestParts cancellationToken

            match int status with
            | 200 -> return CreateRawWebhookFeedData.OK(Serializer.deserialize content)
            | 401 -> return CreateRawWebhookFeedData.Unauthorized
            | 403 -> return CreateRawWebhookFeedData.Forbidden
            | 404 -> return CreateRawWebhookFeedData.NotFound
            | _ -> return CreateRawWebhookFeedData.InternalServerError
        }

    ///<summary>
    ///Delete all your activities.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.DestroyActivities(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/activities" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyActivities.OK
            | 401 -> return DestroyActivities.Unauthorized
            | 403 -> return DestroyActivities.Forbidden
            | 404 -> return DestroyActivities.NotFound
            | _ -> return DestroyActivities.InternalServerError
        }

    ///<summary>
    ///The Activities endpoint returns information about the user's activities.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="startTime">Start time for filtering, returns records created after given time.</param>
    ///<param name="endTime">End time for filtering, returns records created before give time.</param>
    ///<param name="limit">Limit the number of records returned.</param>
    ///<param name="cancellationToken"></param>
    member this.AllActivities
        (
            username: string,
            ?startTime: System.DateTimeOffset,
            ?endTime: System.DateTimeOffset,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  if startTime.IsSome then
                      RequestPart.query ("start_time", startTime.Value)
                  if endTime.IsSome then
                      RequestPart.query ("end_time", endTime.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/activities" requestParts cancellationToken

            match int status with
            | 200 -> return AllActivities.OK(Serializer.deserialize content)
            | 401 -> return AllActivities.Unauthorized
            | 403 -> return AllActivities.Forbidden
            | 404 -> return AllActivities.NotFound
            | _ -> return AllActivities.InternalServerError
        }

    ///<summary>
    ///The Activities endpoint returns information about the user's activities.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="startTime">Start time for filtering, returns records created after given time.</param>
    ///<param name="endTime">End time for filtering, returns records created before give time.</param>
    ///<param name="limit">Limit the number of records returned.</param>
    ///<param name="cancellationToken"></param>
    member this.GetActivity
        (
            username: string,
            ``type``: string,
            ?startTime: System.DateTimeOffset,
            ?endTime: System.DateTimeOffset,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  if startTime.IsSome then
                      RequestPart.query ("start_time", startTime.Value)
                  if endTime.IsSome then
                      RequestPart.query ("end_time", endTime.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/activities/{type}" requestParts cancellationToken

            match int status with
            | 200 -> return GetActivity.OK(Serializer.deserialize content)
            | 401 -> return GetActivity.Unauthorized
            | 403 -> return GetActivity.Forbidden
            | 404 -> return GetActivity.NotFound
            | _ -> return GetActivity.InternalServerError
        }

    ///<summary>
    ///The Dashboards endpoint returns information about the user's dashboards.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllDashboards(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/dashboards" requestParts cancellationToken

            match int status with
            | 200 -> return AllDashboards.OK(Serializer.deserialize content)
            | 401 -> return AllDashboards.Unauthorized
            | 403 -> return AllDashboards.Forbidden
            | 404 -> return AllDashboards.NotFound
            | _ -> return AllDashboards.InternalServerError
        }

    ///<summary>
    ///Create a new Dashboard
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboard"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateDashboard
        (
            username: string,
            dashboard: CreateDashboardPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.jsonContent dashboard ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/dashboards" requestParts cancellationToken

            match int status with
            | 200 -> return CreateDashboard.OK(Serializer.deserialize content)
            | 401 -> return CreateDashboard.Unauthorized
            | 403 -> return CreateDashboard.Forbidden
            | 404 -> return CreateDashboard.NotFound
            | _ -> return CreateDashboard.InternalServerError
        }

    ///<summary>
    ///The Blocks endpoint returns information about the user's blocks.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="cancellationToken"></param>
    member this.AllBlocks(username: string, dashboardId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return AllBlocks.OK(Serializer.deserialize content)
            | 401 -> return AllBlocks.Unauthorized
            | 403 -> return AllBlocks.Forbidden
            | 404 -> return AllBlocks.NotFound
            | _ -> return AllBlocks.InternalServerError
        }

    ///<summary>
    ///Create a new Block
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="block"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateBlock
        (
            username: string,
            dashboardId: string,
            block: CreateBlockPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId)
                  RequestPart.jsonContent block ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return CreateBlock.OK(Serializer.deserialize content)
            | 401 -> return CreateBlock.Unauthorized
            | 403 -> return CreateBlock.Forbidden
            | 404 -> return CreateBlock.NotFound
            | _ -> return CreateBlock.InternalServerError
        }

    ///<summary>
    ///Delete an existing Block
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyBlock(username: string, dashboardId: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return DestroyBlock.OK content
            | 401 -> return DestroyBlock.Unauthorized
            | 403 -> return DestroyBlock.Forbidden
            | 404 -> return DestroyBlock.NotFound
            | _ -> return DestroyBlock.InternalServerError
        }

    ///<summary>
    ///Returns Block based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.GetBlock(username: string, dashboardId: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetBlock.OK(Serializer.deserialize content)
            | 401 -> return GetBlock.Unauthorized
            | 403 -> return GetBlock.Forbidden
            | 404 -> return GetBlock.NotFound
            | _ -> return GetBlock.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Block
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="id"></param>
    ///<param name="block"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateBlock
        (
            username: string,
            dashboardId: string,
            id: string,
            block: UpdateBlockPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent block ]

            let! (status, content) =
                OpenApiHttp.patchAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return UpdateBlock.OK(Serializer.deserialize content)
            | 401 -> return UpdateBlock.Unauthorized
            | 403 -> return UpdateBlock.Forbidden
            | 404 -> return UpdateBlock.NotFound
            | _ -> return UpdateBlock.InternalServerError
        }

    ///<summary>
    ///Replace an existing Block
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="dashboardId"></param>
    ///<param name="id"></param>
    ///<param name="block"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceBlock
        (
            username: string,
            dashboardId: string,
            id: string,
            block: ReplaceBlockPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("dashboard_id", dashboardId)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent block ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/{username}/dashboards/{dashboard_id}/blocks/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return ReplaceBlock.OK(Serializer.deserialize content)
            | 401 -> return ReplaceBlock.Unauthorized
            | 403 -> return ReplaceBlock.Forbidden
            | 404 -> return ReplaceBlock.NotFound
            | _ -> return ReplaceBlock.InternalServerError
        }

    ///<summary>
    ///Delete an existing Dashboard
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyDashboard(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/dashboards/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyDashboard.OK content
            | 401 -> return DestroyDashboard.Unauthorized
            | 403 -> return DestroyDashboard.Forbidden
            | 404 -> return DestroyDashboard.NotFound
            | _ -> return DestroyDashboard.InternalServerError
        }

    ///<summary>
    ///Returns Dashboard based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.GetDashboard(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/dashboards/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetDashboard.OK(Serializer.deserialize content)
            | 401 -> return GetDashboard.Unauthorized
            | 403 -> return GetDashboard.Forbidden
            | 404 -> return GetDashboard.NotFound
            | _ -> return GetDashboard.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Dashboard
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="dashboard"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateDashboard
        (
            username: string,
            id: string,
            dashboard: UpdateDashboardPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent dashboard ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/dashboards/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateDashboard.OK(Serializer.deserialize content)
            | 401 -> return UpdateDashboard.Unauthorized
            | 403 -> return UpdateDashboard.Forbidden
            | 404 -> return UpdateDashboard.NotFound
            | _ -> return UpdateDashboard.InternalServerError
        }

    ///<summary>
    ///Replace an existing Dashboard
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="dashboard"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceDashboard
        (
            username: string,
            id: string,
            dashboard: ReplaceDashboardPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent dashboard ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/dashboards/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceDashboard.OK(Serializer.deserialize content)
            | 401 -> return ReplaceDashboard.Unauthorized
            | 403 -> return ReplaceDashboard.Forbidden
            | 404 -> return ReplaceDashboard.NotFound
            | _ -> return ReplaceDashboard.InternalServerError
        }

    ///<summary>
    ///The Feeds endpoint returns information about the user's feeds. The response includes the latest value of each feed, and other metadata about each feed.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllFeeds(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/{username}/feeds" requestParts cancellationToken

            match int status with
            | 200 -> return AllFeeds.OK(Serializer.deserialize content)
            | 401 -> return AllFeeds.Unauthorized
            | 403 -> return AllFeeds.Forbidden
            | 404 -> return AllFeeds.NotFound
            | _ -> return AllFeeds.InternalServerError
        }

    ///<summary>
    ///Create a new Feed
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feed"></param>
    ///<param name="groupKey"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateFeed
        (
            username: string,
            feed: CreateFeedPayload,
            ?groupKey: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.jsonContent feed
                  if groupKey.IsSome then
                      RequestPart.query ("group_key", groupKey.Value) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/{username}/feeds" requestParts cancellationToken

            match int status with
            | 200 -> return CreateFeed.OK(Serializer.deserialize content)
            | 401 -> return CreateFeed.Unauthorized
            | 403 -> return CreateFeed.Forbidden
            | 404 -> return CreateFeed.NotFound
            | _ -> return CreateFeed.InternalServerError
        }

    ///<summary>
    ///Delete an existing Feed
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="cancellationToken"></param>
    member this.DestroyFeed(username: string, feedKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/feeds/{feed_key}" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyFeed.OK
            | 401 -> return DestroyFeed.Unauthorized
            | 403 -> return DestroyFeed.Forbidden
            | 404 -> return DestroyFeed.NotFound
            | _ -> return DestroyFeed.InternalServerError
        }

    ///<summary>
    ///Returns feed based on the feed key
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="cancellationToken"></param>
    member this.GetFeed(username: string, feedKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}" requestParts cancellationToken

            match int status with
            | 200 -> return GetFeed.OK(Serializer.deserialize content)
            | 401 -> return GetFeed.Unauthorized
            | 403 -> return GetFeed.Forbidden
            | 404 -> return GetFeed.NotFound
            | _ -> return GetFeed.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Feed
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="feed"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateFeed
        (
            username: string,
            feedKey: string,
            feed: UpdateFeedPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent feed ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/feeds/{feed_key}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateFeed.OK(Serializer.deserialize content)
            | 401 -> return UpdateFeed.Unauthorized
            | 403 -> return UpdateFeed.Forbidden
            | 404 -> return UpdateFeed.NotFound
            | _ -> return UpdateFeed.InternalServerError
        }

    ///<summary>
    ///Replace an existing Feed
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="feed"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceFeed
        (
            username: string,
            feedKey: string,
            feed: ReplaceFeedPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent feed ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/feeds/{feed_key}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceFeed.OK(Serializer.deserialize content)
            | 401 -> return ReplaceFeed.Unauthorized
            | 403 -> return ReplaceFeed.Forbidden
            | 404 -> return ReplaceFeed.NotFound
            | _ -> return ReplaceFeed.InternalServerError
        }

    ///<summary>
    ///Get all data for the given feed
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="startTime">Start time for filtering, returns records created after given time.</param>
    ///<param name="endTime">End time for filtering, returns records created before give time.</param>
    ///<param name="limit">Limit the number of records returned.</param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.AllData
        (
            username: string,
            feedKey: string,
            ?startTime: System.DateTimeOffset,
            ?endTime: System.DateTimeOffset,
            ?limit: int,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if startTime.IsSome then
                      RequestPart.query ("start_time", startTime.Value)
                  if endTime.IsSome then
                      RequestPart.query ("end_time", endTime.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data" requestParts cancellationToken

            match int status with
            | 200 -> return AllData.OK(Serializer.deserialize content)
            | 401 -> return AllData.Unauthorized
            | 403 -> return AllData.Forbidden
            | 404 -> return AllData.NotFound
            | _ -> return AllData.InternalServerError
        }

    ///<summary>
    ///Create new data records on the given feed.
    ///**NOTE:** when feed history is on, data `value` size is limited to 1KB, when feed history is turned off data value size is limited to 100KB.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="datum"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateData
        (
            username: string,
            feedKey: string,
            datum: CreateDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent datum ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/feeds/{feed_key}/data" requestParts cancellationToken

            match int status with
            | 200 -> return CreateData.OK(Serializer.deserialize content)
            | 401 -> return CreateData.Unauthorized
            | 403 -> return CreateData.Forbidden
            | 404 -> return CreateData.NotFound
            | _ -> return CreateData.InternalServerError
        }

    ///<summary>
    ///Create multiple new Data records
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="data"></param>
    ///<param name="cancellationToken"></param>
    member this.BatchCreateData
        (
            username: string,
            feedKey: string,
            data: BatchCreateDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent data ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/{username}/feeds/{feed_key}/data/batch"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return BatchCreateData.OK(Serializer.deserialize content)
            | 401 -> return BatchCreateData.Unauthorized
            | 403 -> return BatchCreateData.Forbidden
            | 404 -> return BatchCreateData.NotFound
            | _ -> return BatchCreateData.InternalServerError
        }

    ///<summary>
    ///The Chart API is what we use on io.adafruit.com to populate charts over varying timespans with a consistent number of data points. The maximum number of points returned is 480. This API works by aggregating slices of time into a single value by averaging.
    ///All time-based parameters are optional, if none are given it will default to 1 hour at the finest-grained resolution possible.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="startTime">Start time for filtering, returns records created after given time.</param>
    ///<param name="endTime">End time for filtering, returns records created before give time.</param>
    ///<param name="resolution">A resolution size in minutes. By giving a resolution value you will get back grouped data points aggregated over resolution-sized intervals. NOTE: time span is preferred over resolution, so if you request a span of time that includes more than max limit points you may get a larger resolution than you requested. Valid resolutions are 1, 5, 10, 30, 60, and 120.</param>
    ///<param name="hours">The number of hours the chart should cover.</param>
    ///<param name="cancellationToken"></param>
    member this.ChartData
        (
            username: string,
            feedKey: string,
            ?startTime: System.DateTimeOffset,
            ?endTime: System.DateTimeOffset,
            ?resolution: int,
            ?hours: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if startTime.IsSome then
                      RequestPart.query ("start_time", startTime.Value)
                  if endTime.IsSome then
                      RequestPart.query ("end_time", endTime.Value)
                  if resolution.IsSome then
                      RequestPart.query ("resolution", resolution.Value)
                  if hours.IsSome then
                      RequestPart.query ("hours", hours.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data/chart" requestParts cancellationToken

            match int status with
            | 200 -> return ChartData.OK(Serializer.deserialize content)
            | 401 -> return ChartData.Unauthorized
            | 403 -> return ChartData.Forbidden
            | 404 -> return ChartData.NotFound
            | _ -> return ChartData.InternalServerError
        }

    ///<summary>
    ///Get the oldest data point in the feed. This request sets the queue pointer to the beginning of the feed.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.FirstData
        (
            username: string,
            feedKey: string,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data/first" requestParts cancellationToken

            match int status with
            | 200 -> return FirstData.OK(Serializer.deserialize content)
            | 401 -> return FirstData.Unauthorized
            | 403 -> return FirstData.Forbidden
            | 404 -> return FirstData.NotFound
            | _ -> return FirstData.InternalServerError
        }

    ///<summary>
    ///Get the most recent data point in the feed. This request sets the queue pointer to the end of the feed.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.LastData
        (
            username: string,
            feedKey: string,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data/last" requestParts cancellationToken

            match int status with
            | 200 -> return LastData.OK(Serializer.deserialize content)
            | 401 -> return LastData.Unauthorized
            | 403 -> return LastData.Forbidden
            | 404 -> return LastData.NotFound
            | _ -> return LastData.InternalServerError
        }

    ///<summary>
    ///Get the next newest data point in the feed. If queue processing hasn't been started, the first data point in the feed will be returned.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.NextData
        (
            username: string,
            feedKey: string,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data/next" requestParts cancellationToken

            match int status with
            | 200 -> return NextData.OK(Serializer.deserialize content)
            | 401 -> return NextData.Unauthorized
            | 403 -> return NextData.Forbidden
            | 404 -> return NextData.NotFound
            | _ -> return NextData.InternalServerError
        }

    ///<summary>
    ///Get the previously processed data point in the feed. NOTE: this method doesn't move the processing queue pointer.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.PreviousData
        (
            username: string,
            feedKey: string,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{username}/feeds/{feed_key}/data/previous"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return PreviousData.OK(Serializer.deserialize content)
            | 401 -> return PreviousData.Unauthorized
            | 403 -> return PreviousData.Forbidden
            | 404 -> return PreviousData.NotFound
            | _ -> return PreviousData.InternalServerError
        }

    ///<summary>
    ///Get the most recent data point in the feed in an MQTT compatible CSV format: `value,lat,lon,ele`
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="cancellationToken"></param>
    member this.RetainData(username: string, feedKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{username}/feeds/{feed_key}/data/retain"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return RetainData.OK
            | 401 -> return RetainData.Unauthorized
            | 403 -> return RetainData.Forbidden
            | 404 -> return RetainData.NotFound
            | _ -> return RetainData.InternalServerError
        }

    ///<summary>
    ///Delete existing Data
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyData(username: string, feedKey: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/{username}/feeds/{feed_key}/data/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return DestroyData.OK content
            | 401 -> return DestroyData.Unauthorized
            | 403 -> return DestroyData.Forbidden
            | 404 -> return DestroyData.NotFound
            | _ -> return DestroyData.InternalServerError
        }

    ///<summary>
    ///Returns data based on feed key
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="id"></param>
    ///<param name="include">List of Data record fields to include in response as comma separated list. Acceptable values are: `value`, `lat`, `lon`, `ele`, `id`, and `created_at`. </param>
    ///<param name="cancellationToken"></param>
    member this.GetData
        (
            username: string,
            feedKey: string,
            id: string,
            ?``include``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.path ("id", id)
                  if ``include``.IsSome then
                      RequestPart.query ("include", ``include``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/data/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetData.OK(Serializer.deserialize content)
            | 401 -> return GetData.Unauthorized
            | 403 -> return GetData.Forbidden
            | 404 -> return GetData.NotFound
            | _ -> return GetData.InternalServerError
        }

    ///<summary>
    ///Update properties of existing Data
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="id"></param>
    ///<param name="datum"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateData
        (
            username: string,
            feedKey: string,
            id: string,
            datum: UpdateDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent datum ]

            let! (status, content) =
                OpenApiHttp.patchAsync
                    httpClient
                    "/{username}/feeds/{feed_key}/data/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return UpdateData.OK(Serializer.deserialize content)
            | 401 -> return UpdateData.Unauthorized
            | 403 -> return UpdateData.Forbidden
            | 404 -> return UpdateData.NotFound
            | _ -> return UpdateData.InternalServerError
        }

    ///<summary>
    ///Replace existing Data
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="id"></param>
    ///<param name="datum"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceData
        (
            username: string,
            feedKey: string,
            id: string,
            datum: ReplaceDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent datum ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/feeds/{feed_key}/data/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceData.OK(Serializer.deserialize content)
            | 401 -> return ReplaceData.Unauthorized
            | 403 -> return ReplaceData.Forbidden
            | 404 -> return ReplaceData.NotFound
            | _ -> return ReplaceData.InternalServerError
        }

    ///<summary>
    ///Returns more detailed feed record based on the feed key
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="cancellationToken"></param>
    member this.GetFeedDetails(username: string, feedKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("feed_key", feedKey) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/feeds/{feed_key}/details" requestParts cancellationToken

            match int status with
            | 200 -> return GetFeedDetails.OK(Serializer.deserialize content)
            | 401 -> return GetFeedDetails.Unauthorized
            | 403 -> return GetFeedDetails.Forbidden
            | 404 -> return GetFeedDetails.NotFound
            | _ -> return GetFeedDetails.InternalServerError
        }

    ///<summary>
    ///The Groups endpoint returns information about the user's groups. The response includes the latest value of each feed in the group, and other metadata about the group.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllGroups(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/{username}/groups" requestParts cancellationToken

            match int status with
            | 200 -> return AllGroups.OK(Serializer.deserialize content)
            | 401 -> return AllGroups.Unauthorized
            | 403 -> return AllGroups.Forbidden
            | 404 -> return AllGroups.NotFound
            | _ -> return AllGroups.InternalServerError
        }

    ///<summary>
    ///Create a new Group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="group"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateGroup(username: string, group: CreateGroupPayload, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.jsonContent group ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/groups" requestParts cancellationToken

            match int status with
            | 200 -> return CreateGroup.OK(Serializer.deserialize content)
            | 401 -> return CreateGroup.Unauthorized
            | 403 -> return CreateGroup.Forbidden
            | 404 -> return CreateGroup.NotFound
            | _ -> return CreateGroup.InternalServerError
        }

    ///<summary>
    ///Delete an existing Group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyGroup(username: string, groupKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/groups/{group_key}" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyGroup.OK content
            | 401 -> return DestroyGroup.Unauthorized
            | 403 -> return DestroyGroup.Forbidden
            | 404 -> return DestroyGroup.NotFound
            | _ -> return DestroyGroup.InternalServerError
        }

    ///<summary>
    ///Returns Group based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="cancellationToken"></param>
    member this.GetGroup(username: string, groupKey: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/groups/{group_key}" requestParts cancellationToken

            match int status with
            | 200 -> return GetGroup.OK(Serializer.deserialize content)
            | 401 -> return GetGroup.Unauthorized
            | 403 -> return GetGroup.Forbidden
            | 404 -> return GetGroup.NotFound
            | _ -> return GetGroup.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="group"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateGroup
        (
            username: string,
            groupKey: string,
            group: UpdateGroupPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.jsonContent group ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/groups/{group_key}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateGroup.OK(Serializer.deserialize content)
            | 401 -> return UpdateGroup.Unauthorized
            | 403 -> return UpdateGroup.Forbidden
            | 404 -> return UpdateGroup.NotFound
            | _ -> return UpdateGroup.InternalServerError
        }

    ///<summary>
    ///Replace an existing Group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="group"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceGroup
        (
            username: string,
            groupKey: string,
            group: ReplaceGroupPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.jsonContent group ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/groups/{group_key}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceGroup.OK(Serializer.deserialize content)
            | 401 -> return ReplaceGroup.Unauthorized
            | 403 -> return ReplaceGroup.Forbidden
            | 404 -> return ReplaceGroup.NotFound
            | _ -> return ReplaceGroup.InternalServerError
        }

    ///<summary>
    ///Add an existing Feed to a Group
    ///</summary>
    ///<param name="groupKey"></param>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey"></param>
    ///<param name="cancellationToken"></param>
    member this.AddFeedToGroup
        (
            groupKey: string,
            username: string,
            ?feedKey: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("username", username)
                  if feedKey.IsSome then
                      RequestPart.query ("feed_key", feedKey.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/groups/{group_key}/add" requestParts cancellationToken

            match int status with
            | 200 -> return AddFeedToGroup.OK(Serializer.deserialize content)
            | 401 -> return AddFeedToGroup.Unauthorized
            | 403 -> return AddFeedToGroup.Forbidden
            | 404 -> return AddFeedToGroup.NotFound
            | _ -> return AddFeedToGroup.InternalServerError
        }

    ///<summary>
    ///Create new data for multiple feeds in a group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="groupFeedData"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateGroupData
        (
            username: string,
            groupKey: string,
            groupFeedData: CreateGroupDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.jsonContent groupFeedData ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/groups/{group_key}/data" requestParts cancellationToken

            match int status with
            | 200 -> return CreateGroupData.OK(Serializer.deserialize content)
            | 401 -> return CreateGroupData.Unauthorized
            | 403 -> return CreateGroupData.Forbidden
            | 404 -> return CreateGroupData.NotFound
            | _ -> return CreateGroupData.InternalServerError
        }

    ///<summary>
    ///The Group Feeds endpoint returns information about the user's feeds. The response includes the latest value of each feed, and other metadata about each feed, but only for feeds within the given group.
    ///</summary>
    ///<param name="groupKey"></param>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllGroupFeeds(groupKey: string, username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/groups/{group_key}/feeds" requestParts cancellationToken

            match int status with
            | 200 -> return AllGroupFeeds.OK(Serializer.deserialize content)
            | 401 -> return AllGroupFeeds.Unauthorized
            | 403 -> return AllGroupFeeds.Forbidden
            | 404 -> return AllGroupFeeds.NotFound
            | _ -> return AllGroupFeeds.InternalServerError
        }

    ///<summary>
    ///Create a new Feed in a Group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="feed"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateGroupFeed
        (
            username: string,
            groupKey: string,
            feed: CreateGroupFeedPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.jsonContent feed ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/groups/{group_key}/feeds" requestParts cancellationToken

            match int status with
            | 200 -> return CreateGroupFeed.OK(Serializer.deserialize content)
            | 401 -> return CreateGroupFeed.Unauthorized
            | 403 -> return CreateGroupFeed.Forbidden
            | 404 -> return CreateGroupFeed.NotFound
            | _ -> return CreateGroupFeed.InternalServerError
        }

    ///<summary>
    ///All data for current feed in a specific group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="startTime">Start time for filtering data. Returns data created after given time.</param>
    ///<param name="endTime">End time for filtering data. Returns data created before give time.</param>
    ///<param name="limit">Limit the number of records returned.</param>
    ///<param name="cancellationToken"></param>
    member this.AllGroupFeedData
        (
            username: string,
            groupKey: string,
            feedKey: string,
            ?startTime: System.DateTimeOffset,
            ?endTime: System.DateTimeOffset,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("feed_key", feedKey)
                  if startTime.IsSome then
                      RequestPart.query ("start_time", startTime.Value)
                  if endTime.IsSome then
                      RequestPart.query ("end_time", endTime.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{username}/groups/{group_key}/feeds/{feed_key}/data"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return AllGroupFeedData.OK(Serializer.deserialize content)
            | 401 -> return AllGroupFeedData.Unauthorized
            | 403 -> return AllGroupFeedData.Forbidden
            | 404 -> return AllGroupFeedData.NotFound
            | _ -> return AllGroupFeedData.InternalServerError
        }

    ///<summary>
    ///Create new Data in a feed belonging to a particular group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="datum"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateGroupFeedData
        (
            username: string,
            groupKey: string,
            feedKey: string,
            datum: CreateGroupFeedDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent datum ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/{username}/groups/{group_key}/feeds/{feed_key}/data"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return CreateGroupFeedData.OK(Serializer.deserialize content)
            | 401 -> return CreateGroupFeedData.Unauthorized
            | 403 -> return CreateGroupFeedData.Forbidden
            | 404 -> return CreateGroupFeedData.NotFound
            | _ -> return CreateGroupFeedData.InternalServerError
        }

    ///<summary>
    ///Create multiple new Data records in a feed belonging to a particular group
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="groupKey"></param>
    ///<param name="feedKey">a valid feed key</param>
    ///<param name="data"></param>
    ///<param name="cancellationToken"></param>
    member this.BatchCreateGroupFeedData
        (
            username: string,
            groupKey: string,
            feedKey: string,
            data: BatchCreateGroupFeedDataPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("feed_key", feedKey)
                  RequestPart.jsonContent data ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/{username}/groups/{group_key}/feeds/{feed_key}/data/batch"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return BatchCreateGroupFeedData.OK(Serializer.deserialize content)
            | 401 -> return BatchCreateGroupFeedData.Unauthorized
            | 403 -> return BatchCreateGroupFeedData.Forbidden
            | 404 -> return BatchCreateGroupFeedData.NotFound
            | _ -> return BatchCreateGroupFeedData.InternalServerError
        }

    ///<summary>
    ///Remove a Feed from a Group
    ///</summary>
    ///<param name="groupKey"></param>
    ///<param name="username">a valid username string</param>
    ///<param name="feedKey"></param>
    ///<param name="cancellationToken"></param>
    member this.RemoveFeedFromGroup
        (
            groupKey: string,
            username: string,
            ?feedKey: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("group_key", groupKey)
                  RequestPart.path ("username", username)
                  if feedKey.IsSome then
                      RequestPart.query ("feed_key", feedKey.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/groups/{group_key}/remove" requestParts cancellationToken

            match int status with
            | 200 -> return RemoveFeedFromGroup.OK(Serializer.deserialize content)
            | 401 -> return RemoveFeedFromGroup.Unauthorized
            | 403 -> return RemoveFeedFromGroup.Forbidden
            | 404 -> return RemoveFeedFromGroup.NotFound
            | _ -> return RemoveFeedFromGroup.InternalServerError
        }

    ///<summary>
    ///Get the user's data rate limit and current activity level.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.GetCurrentUserThrottle(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/throttle" requestParts cancellationToken

            match int status with
            | 200 -> return GetCurrentUserThrottle.OK(Serializer.deserialize content)
            | 401 -> return GetCurrentUserThrottle.Unauthorized
            | 403 -> return GetCurrentUserThrottle.Forbidden
            | 404 -> return GetCurrentUserThrottle.NotFound
            | _ -> return GetCurrentUserThrottle.InternalServerError
        }

    ///<summary>
    ///The Tokens endpoint returns information about the user's tokens.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllTokens(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/{username}/tokens" requestParts cancellationToken

            match int status with
            | 200 -> return AllTokens.OK(Serializer.deserialize content)
            | 401 -> return AllTokens.Unauthorized
            | 403 -> return AllTokens.Forbidden
            | 404 -> return AllTokens.NotFound
            | _ -> return AllTokens.InternalServerError
        }

    ///<summary>
    ///Create a new Token
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="token"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateToken(username: string, token: CreateTokenPayload, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.jsonContent token ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/tokens" requestParts cancellationToken

            match int status with
            | 200 -> return CreateToken.OK(Serializer.deserialize content)
            | 401 -> return CreateToken.Unauthorized
            | 403 -> return CreateToken.Forbidden
            | 404 -> return CreateToken.NotFound
            | _ -> return CreateToken.InternalServerError
        }

    ///<summary>
    ///Delete an existing Token
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyToken(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/tokens/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyToken.OK content
            | 401 -> return DestroyToken.Unauthorized
            | 403 -> return DestroyToken.Forbidden
            | 404 -> return DestroyToken.NotFound
            | _ -> return DestroyToken.InternalServerError
        }

    ///<summary>
    ///Returns Token based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.GetToken(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/tokens/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetToken.OK(Serializer.deserialize content)
            | 401 -> return GetToken.Unauthorized
            | 403 -> return GetToken.Forbidden
            | 404 -> return GetToken.NotFound
            | _ -> return GetToken.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Token
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="token"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateToken
        (
            username: string,
            id: string,
            token: UpdateTokenPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent token ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/tokens/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateToken.OK(Serializer.deserialize content)
            | 401 -> return UpdateToken.Unauthorized
            | 403 -> return UpdateToken.Forbidden
            | 404 -> return UpdateToken.NotFound
            | _ -> return UpdateToken.InternalServerError
        }

    ///<summary>
    ///Replace an existing Token
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="token"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceToken
        (
            username: string,
            id: string,
            token: ReplaceTokenPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent token ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/tokens/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceToken.OK(Serializer.deserialize content)
            | 401 -> return ReplaceToken.Unauthorized
            | 403 -> return ReplaceToken.Forbidden
            | 404 -> return ReplaceToken.NotFound
            | _ -> return ReplaceToken.InternalServerError
        }

    ///<summary>
    ///The Triggers endpoint returns information about the user's triggers.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="cancellationToken"></param>
    member this.AllTriggers(username: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/triggers" requestParts cancellationToken

            match int status with
            | 200 -> return AllTriggers.OK(Serializer.deserialize content)
            | 401 -> return AllTriggers.Unauthorized
            | 403 -> return AllTriggers.Forbidden
            | 404 -> return AllTriggers.NotFound
            | _ -> return AllTriggers.InternalServerError
        }

    ///<summary>
    ///Create a new Trigger
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="trigger"></param>
    ///<param name="cancellationToken"></param>
    member this.CreateTrigger(username: string, trigger: CreateTriggerPayload, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.jsonContent trigger ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/triggers" requestParts cancellationToken

            match int status with
            | 200 -> return CreateTrigger.OK(Serializer.deserialize content)
            | 401 -> return CreateTrigger.Unauthorized
            | 403 -> return CreateTrigger.Forbidden
            | 404 -> return CreateTrigger.NotFound
            | _ -> return CreateTrigger.InternalServerError
        }

    ///<summary>
    ///Delete an existing Trigger
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyTrigger(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/{username}/triggers/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return DestroyTrigger.OK content
            | 401 -> return DestroyTrigger.Unauthorized
            | 403 -> return DestroyTrigger.Forbidden
            | 404 -> return DestroyTrigger.NotFound
            | _ -> return DestroyTrigger.InternalServerError
        }

    ///<summary>
    ///Returns Trigger based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.GetTrigger(username: string, id: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/triggers/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetTrigger.OK(Serializer.deserialize content)
            | 401 -> return GetTrigger.Unauthorized
            | 403 -> return GetTrigger.Forbidden
            | 404 -> return GetTrigger.NotFound
            | _ -> return GetTrigger.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Trigger
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="trigger"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdateTrigger
        (
            username: string,
            id: string,
            trigger: UpdateTriggerPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent trigger ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/triggers/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdateTrigger.OK(Serializer.deserialize content)
            | 401 -> return UpdateTrigger.Unauthorized
            | 403 -> return UpdateTrigger.Forbidden
            | 404 -> return UpdateTrigger.NotFound
            | _ -> return UpdateTrigger.InternalServerError
        }

    ///<summary>
    ///Replace an existing Trigger
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="id"></param>
    ///<param name="trigger"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceTrigger
        (
            username: string,
            id: string,
            trigger: ReplaceTriggerPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent trigger ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/triggers/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplaceTrigger.OK(Serializer.deserialize content)
            | 401 -> return ReplaceTrigger.Unauthorized
            | 403 -> return ReplaceTrigger.Forbidden
            | 404 -> return ReplaceTrigger.NotFound
            | _ -> return ReplaceTrigger.InternalServerError
        }

    ///<summary>
    ///The Permissions endpoint returns information about the user's permissions.
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="cancellationToken"></param>
    member this.AllPermissions
        (
            username: string,
            ``type``: string,
            typeId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/{type}/{type_id}/acl" requestParts cancellationToken

            match int status with
            | 200 -> return AllPermissions.OK(Serializer.deserialize content)
            | 401 -> return AllPermissions.Unauthorized
            | 403 -> return AllPermissions.Forbidden
            | 404 -> return AllPermissions.NotFound
            | _ -> return AllPermissions.InternalServerError
        }

    ///<summary>
    ///Create a new Permission
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="permission"></param>
    ///<param name="cancellationToken"></param>
    member this.CreatePermission
        (
            username: string,
            ``type``: string,
            typeId: string,
            permission: CreatePermissionPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId)
                  RequestPart.jsonContent permission ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{username}/{type}/{type_id}/acl" requestParts cancellationToken

            match int status with
            | 200 -> return CreatePermission.OK(Serializer.deserialize content)
            | 401 -> return CreatePermission.Unauthorized
            | 403 -> return CreatePermission.Forbidden
            | 404 -> return CreatePermission.NotFound
            | _ -> return CreatePermission.InternalServerError
        }

    ///<summary>
    ///Delete an existing Permission
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.DestroyPermission
        (
            username: string,
            ``type``: string,
            typeId: string,
            id: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/{username}/{type}/{type_id}/acl/{id}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return DestroyPermission.OK content
            | 401 -> return DestroyPermission.Unauthorized
            | 403 -> return DestroyPermission.Forbidden
            | 404 -> return DestroyPermission.NotFound
            | _ -> return DestroyPermission.InternalServerError
        }

    ///<summary>
    ///Returns Permission based on ID
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="id"></param>
    ///<param name="cancellationToken"></param>
    member this.GetPermission
        (
            username: string,
            ``type``: string,
            typeId: string,
            id: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId)
                  RequestPart.path ("id", id) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/{username}/{type}/{type_id}/acl/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetPermission.OK(Serializer.deserialize content)
            | 401 -> return GetPermission.Unauthorized
            | 403 -> return GetPermission.Forbidden
            | 404 -> return GetPermission.NotFound
            | _ -> return GetPermission.InternalServerError
        }

    ///<summary>
    ///Update properties of an existing Permission
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="id"></param>
    ///<param name="permission"></param>
    ///<param name="cancellationToken"></param>
    member this.UpdatePermission
        (
            username: string,
            ``type``: string,
            typeId: string,
            id: string,
            permission: UpdatePermissionPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent permission ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/{username}/{type}/{type_id}/acl/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return UpdatePermission.OK(Serializer.deserialize content)
            | 401 -> return UpdatePermission.Unauthorized
            | 403 -> return UpdatePermission.Forbidden
            | 404 -> return UpdatePermission.NotFound
            | _ -> return UpdatePermission.InternalServerError
        }

    ///<summary>
    ///Replace an existing Permission
    ///</summary>
    ///<param name="username">a valid username string</param>
    ///<param name="type"></param>
    ///<param name="typeId"></param>
    ///<param name="id"></param>
    ///<param name="permission"></param>
    ///<param name="cancellationToken"></param>
    member this.ReplacePermission
        (
            username: string,
            ``type``: string,
            typeId: string,
            id: string,
            permission: ReplacePermissionPayload,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("username", username)
                  RequestPart.path ("type", ``type``)
                  RequestPart.path ("type_id", typeId)
                  RequestPart.path ("id", id)
                  RequestPart.jsonContent permission ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/{username}/{type}/{type_id}/acl/{id}" requestParts cancellationToken

            match int status with
            | 200 -> return ReplacePermission.OK(Serializer.deserialize content)
            | 401 -> return ReplacePermission.Unauthorized
            | 403 -> return ReplacePermission.Forbidden
            | 404 -> return ReplacePermission.NotFound
            | _ -> return ReplacePermission.InternalServerError
        }
