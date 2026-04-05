namespace rec PlatformApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open PlatformApi.Types
open PlatformApi.Http

///The [REST API specification](https://www.ably.io/documentation/rest-api) for Ably.
type PlatformApiClient(httpClient: HttpClient) =
    ///<summary>
    ///Enumerate all active channels of the application
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="limit"></param>
    ///<param name="prefix">Optionally limits the query to only those channels whose name starts with the given prefix</param>
    ///<param name="by">optionally specifies whether to return just channel names (by=id) or ChannelDetails (by=value)</param>
    ///<param name="cancellationToken"></param>
    member this.GetMetadataOfAllChannels
        (
            ?xAblyVersion: string,
            ?format: string,
            ?limit: int,
            ?prefix: string,
            ?by: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if prefix.IsSome then
                      RequestPart.query ("prefix", prefix.Value)
                  if by.IsSome then
                      RequestPart.query ("by", by.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/channels" requestParts cancellationToken
            return GetMetadataOfAllChannels.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get metadata of a channel
    ///</summary>
    ///<param name="channelId">The [Channel's ID](https://www.ably.io/documentation/rest/channels).</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.GetMetadataOfChannel
        (
            channelId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("channel_id", channelId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/channels/{channel_id}" requestParts cancellationToken

            match int status with
            | 200 -> return GetMetadataOfChannel.OK(Serializer.deserialize content)
            | _ -> return GetMetadataOfChannel.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get message history for a channel
    ///</summary>
    ///<param name="channelId">The [Channel's ID](https://www.ably.io/documentation/rest/channels).</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="start"></param>
    ///<param name="limit"></param>
    ///<param name="end"></param>
    ///<param name="direction"></param>
    ///<param name="cancellationToken"></param>
    member this.GetMessagesByChannel
        (
            channelId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?start: string,
            ?limit: int,
            ?``end``: string,
            ?direction: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("channel_id", channelId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if start.IsSome then
                      RequestPart.query ("start", start.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if ``end``.IsSome then
                      RequestPart.query ("end", ``end``.Value)
                  if direction.IsSome then
                      RequestPart.query ("direction", direction.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/channels/{channel_id}/messages" requestParts cancellationToken

            return GetMessagesByChannel.DefaultResponse
        }

    ///<summary>
    ///Publish a message to the specified channel
    ///</summary>
    ///<param name="channelId">The [Channel's ID](https://www.ably.io/documentation/rest/channels).</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body">Message object.</param>
    member this.PublishMessagesToChannel
        (
            channelId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken,
            ?body: Message
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("channel_id", channelId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/channels/{channel_id}/messages" requestParts cancellationToken

            return PublishMessagesToChannel.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get presence on a channel
    ///</summary>
    ///<param name="channelId">The [Channel's ID](https://www.ably.io/documentation/rest/channels).</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="clientId"></param>
    ///<param name="connectionId"></param>
    ///<param name="limit"></param>
    ///<param name="cancellationToken"></param>
    member this.GetPresenceOfChannel
        (
            channelId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?clientId: string,
            ?connectionId: string,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("channel_id", channelId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if clientId.IsSome then
                      RequestPart.query ("clientId", clientId.Value)
                  if connectionId.IsSome then
                      RequestPart.query ("connectionId", connectionId.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/channels/{channel_id}/presence" requestParts cancellationToken

            match int status with
            | 200 -> return GetPresenceOfChannel.OK(Serializer.deserialize content)
            | _ -> return GetPresenceOfChannel.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get presence on a channel
    ///</summary>
    ///<param name="channelId">The [Channel's ID](https://www.ably.io/documentation/rest/channels).</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="start"></param>
    ///<param name="limit"></param>
    ///<param name="end"></param>
    ///<param name="direction"></param>
    ///<param name="cancellationToken"></param>
    member this.GetPresenceHistoryOfChannel
        (
            channelId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?start: string,
            ?limit: int,
            ?``end``: string,
            ?direction: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("channel_id", channelId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if start.IsSome then
                      RequestPart.query ("start", start.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if ``end``.IsSome then
                      RequestPart.query ("end", ``end``.Value)
                  if direction.IsSome then
                      RequestPart.query ("direction", direction.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/channels/{channel_id}/presence/history" requestParts cancellationToken

            return GetPresenceHistoryOfChannel.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///This is the means by which clients obtain access tokens to use the service. You can see how to construct an Ably TokenRequest in the [Ably TokenRequest spec](https://www.ably.io/documentation/rest-api/token-request-spec) documentation, although we recommend you use an Ably SDK rather to create a TokenRequest, as the construction of a TokenRequest is complex. The resulting token response object contains the token properties as defined in Ably TokenRequest spec. Authentication is not required if using a Signed TokenRequest.
    ///</summary>
    ///<param name="keyName">The [key name](https://www.ably.io/documentation/rest-api/token-request-spec#api-key-format) comprises of the app ID and key ID of an API key.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.RequestAccessToken
        (
            keyName: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("keyName", keyName)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/keys/{keyName}/requestToken" requestParts cancellationToken

            return RequestAccessToken.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Delete a device details object.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="channel">Filter to restrict to subscriptions associated with that channel.</param>
    ///<param name="deviceId">Must be set when clientId is empty, cannot be used with clientId.</param>
    ///<param name="clientId">Must be set when deviceId is empty, cannot be used with deviceId.</param>
    ///<param name="cancellationToken"></param>
    member this.DeletePushDeviceDetails
        (
            ?xAblyVersion: string,
            ?format: string,
            ?channel: string,
            ?deviceId: string,
            ?clientId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if channel.IsSome then
                      RequestPart.query ("channel", channel.Value)
                  if deviceId.IsSome then
                      RequestPart.query ("deviceId", deviceId.Value)
                  if clientId.IsSome then
                      RequestPart.query ("clientId", clientId.Value) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/push/channelSubscriptions" requestParts cancellationToken

            return DeletePushDeviceDetails.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a list of push notification subscriptions to channels.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="channel">Filter to restrict to subscriptions associated with that channel.</param>
    ///<param name="deviceId">Optional filter to restrict to devices associated with that deviceId. Cannot be used with clientId.</param>
    ///<param name="clientId">Optional filter to restrict to devices associated with that clientId. Cannot be used with deviceId.</param>
    ///<param name="limit">The maximum number of records to return.</param>
    ///<param name="cancellationToken"></param>
    member this.GetPushSubscriptionsOnChannels
        (
            ?xAblyVersion: string,
            ?format: string,
            ?channel: string,
            ?deviceId: string,
            ?clientId: string,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if channel.IsSome then
                      RequestPart.query ("channel", channel.Value)
                  if deviceId.IsSome then
                      RequestPart.query ("deviceId", deviceId.Value)
                  if clientId.IsSome then
                      RequestPart.query ("clientId", clientId.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/push/channelSubscriptions" requestParts cancellationToken

            return GetPushSubscriptionsOnChannels.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Subscribe either a single device or all devices associated with a client ID to receive push notifications from messages sent to a channel.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.SubscribePushDeviceToChannel
        (
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/push/channelSubscriptions" requestParts cancellationToken

            return SubscribePushDeviceToChannel.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Returns a paginated response of channel names.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.GetChannelsWithPushSubscribers
        (
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/push/channels" requestParts cancellationToken
            return GetChannelsWithPushSubscribers.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Unregisters devices. All their subscriptions for receiving push notifications through channels will also be deleted.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="deviceId">Optional filter to restrict to devices associated with that deviceId. Cannot be used with clientId.</param>
    ///<param name="clientId">Optional filter to restrict to devices associated with that clientId. Cannot be used with deviceId.</param>
    ///<param name="cancellationToken"></param>
    member this.UnregisterAllPushDevices
        (
            ?xAblyVersion: string,
            ?format: string,
            ?deviceId: string,
            ?clientId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if deviceId.IsSome then
                      RequestPart.query ("deviceId", deviceId.Value)
                  if clientId.IsSome then
                      RequestPart.query ("clientId", clientId.Value) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync httpClient "/push/deviceRegistrations" requestParts cancellationToken

            return UnregisterAllPushDevices.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///List of device details of devices registed for push notifications.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="deviceId">Optional filter to restrict to devices associated with that deviceId.</param>
    ///<param name="clientId">Optional filter to restrict to devices associated with that clientId.</param>
    ///<param name="limit">The maximum number of records to return.</param>
    ///<param name="cancellationToken"></param>
    member this.GetRegisteredPushDevices
        (
            ?xAblyVersion: string,
            ?format: string,
            ?deviceId: string,
            ?clientId: string,
            ?limit: int,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if deviceId.IsSome then
                      RequestPart.query ("deviceId", deviceId.Value)
                  if clientId.IsSome then
                      RequestPart.query ("clientId", clientId.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/push/deviceRegistrations" requestParts cancellationToken

            return GetRegisteredPushDevices.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Register a device’s details, including the information necessary to deliver push notifications to it. Requires "push-admin" capability.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.RegisterPushDevice
        (
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken,
            ?body: DeviceDetails
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/push/deviceRegistrations" requestParts cancellationToken

            return RegisterPushDevice.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Unregisters a single device by its device ID. All its subscriptions for receiving push notifications through channels will also be deleted.
    ///</summary>
    ///<param name="deviceId">Device's ID.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.UnregisterPushDevice
        (
            deviceId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("device_id", deviceId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/push/deviceRegistrations/{device_id}"
                    requestParts
                    cancellationToken

            return UnregisterPushDevice.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the full details of a device.
    ///</summary>
    ///<param name="deviceId">Device's ID.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.GetPushDeviceDetails
        (
            deviceId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("device_id", deviceId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/push/deviceRegistrations/{device_id}" requestParts cancellationToken

            return GetPushDeviceDetails.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Specific attributes of an existing registration can be updated. Only clientId, metadata and push.recipient are mutable.
    ///</summary>
    ///<param name="deviceId">Device's ID.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PatchPushDeviceDetails
        (
            deviceId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken,
            ?body: DeviceDetails
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("device_id", deviceId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/push/deviceRegistrations/{device_id}" requestParts cancellationToken

            return PatchPushDeviceDetails.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Device registrations can be upserted (the existing registration is replaced entirely) with a PUT operation. Only clientId, metadata and push.recipient are mutable.
    ///</summary>
    ///<param name="deviceId">Device's ID.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PutPushDeviceDetails
        (
            deviceId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken,
            ?body: DeviceDetails
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("device_id", deviceId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) =
                OpenApiHttp.putAsync httpClient "/push/deviceRegistrations/{device_id}" requestParts cancellationToken

            return PutPushDeviceDetails.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///Gets an updated device details object.
    ///</summary>
    ///<param name="deviceId">Device's ID.</param>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.UpdatePushDeviceDetails
        (
            deviceId: string,
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("device_id", deviceId)
                  if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/push/deviceRegistrations/{device_id}/resetUpdateToken"
                    requestParts
                    cancellationToken

            return UpdatePushDeviceDetails.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///A convenience endpoint to deliver a push notification payload to a single device or set of devices identified by their client identifier.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    ///<param name="body"></param>
    member this.PublishPushNotificationToDevices
        (
            ?xAblyVersion: string,
            ?format: string,
            ?cancellationToken: CancellationToken,
            ?body: PublishPushNotificationToDevicesPayload
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if body.IsSome then
                      RequestPart.jsonContent body.Value ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/push/publish" requestParts cancellationToken
            return PublishPushNotificationToDevices.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///The Ably system can be queried to obtain usage statistics for a given application, and results are provided aggregated across all channels in use in the application in the specified period. Stats may be used to track usage against account quotas.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="start"></param>
    ///<param name="limit"></param>
    ///<param name="end"></param>
    ///<param name="direction"></param>
    ///<param name="unit">Specifies the unit of aggregation in the returned results.</param>
    ///<param name="cancellationToken"></param>
    member this.GetStats
        (
            ?xAblyVersion: string,
            ?format: string,
            ?start: string,
            ?limit: int,
            ?``end``: string,
            ?direction: string,
            ?unit: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value)
                  if start.IsSome then
                      RequestPart.query ("start", start.Value)
                  if limit.IsSome then
                      RequestPart.query ("limit", limit.Value)
                  if ``end``.IsSome then
                      RequestPart.query ("end", ``end``.Value)
                  if direction.IsSome then
                      RequestPart.query ("direction", direction.Value)
                  if unit.IsSome then
                      RequestPart.query ("unit", unit.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/stats" requestParts cancellationToken
            return GetStats.DefaultResponse(Serializer.deserialize content)
        }

    ///<summary>
    ///This returns the service time in milliseconds since the epoch.
    ///</summary>
    ///<param name="xAblyVersion">The version of the API you wish to use.</param>
    ///<param name="format">The response format you would like</param>
    ///<param name="cancellationToken"></param>
    member this.GetTime(?xAblyVersion: string, ?format: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if xAblyVersion.IsSome then
                      RequestPart.header ("X-Ably-Version", xAblyVersion.Value)
                  if format.IsSome then
                      RequestPart.query ("format", format.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/time" requestParts cancellationToken
            return GetTime.DefaultResponse(Serializer.deserialize content)
        }
