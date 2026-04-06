namespace rec Oanda

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open Oanda.Types
open Oanda.Http

///The full OANDA v20 REST API Specification. This specification defines how to interact with v20 Accounts, Trades, Orders, Pricing and more.
type OandaClient(httpClient: HttpClient) =
    ///<summary>
    ///Fetch candlestick data for an instrument.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="price">The Price component(s) to get candlestick data for. Can contain any combination of the characters "M" (midpoint candles) "B" (bid candles) and "A" (ask candles).</param>
    ///<param name="granularity">The granularity of the candlesticks to fetch</param>
    ///<param name="count">The number of candlesticks to return in the reponse. Count should not be specified if both the start and end parameters are provided, as the time range combined with the graularity will determine the number of candlesticks to return.</param>
    ///<param name="from">The start of the time range to fetch candlesticks for.</param>
    ///<param name="to">The end of the time range to fetch candlesticks for.</param>
    ///<param name="smooth">A flag that controls whether the candlestick is "smoothed" or not.  A smoothed candlestick uses the previous candle's close price as its open price, while an unsmoothed candlestick uses the first price from its time range as its open price.</param>
    ///<param name="includeFirst">A flag that controls whether the candlestick that is covered by the from time should be included in the results. This flag enables clients to use the timestamp of the last completed candlestick received to poll for future candlesticks but avoid receiving the previous candlestick repeatedly.</param>
    ///<param name="dailyAlignment">The hour of the day (in the specified timezone) to use for granularities that have daily alignments.</param>
    ///<param name="alignmentTimezone">The timezone to use for the dailyAlignment parameter. Candlesticks with daily alignment will be aligned to the dailyAlignment hour within the alignmentTimezone.  Note that the returned times will still be represented in UTC.</param>
    ///<param name="weeklyAlignment">The day of the week used for granularities that have weekly alignment.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentCandles
        (
            authorization: string,
            instrument: string,
            ?acceptDatetimeFormat: string,
            ?price: string,
            ?granularity: string,
            ?count: int,
            ?from: string,
            ?``to``: string,
            ?smooth: bool,
            ?includeFirst: bool,
            ?dailyAlignment: int,
            ?alignmentTimezone: string,
            ?weeklyAlignment: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if price.IsSome then
                      RequestPart.query ("price", price.Value)
                  if granularity.IsSome then
                      RequestPart.query ("granularity", granularity.Value)
                  if count.IsSome then
                      RequestPart.query ("count", count.Value)
                  if from.IsSome then
                      RequestPart.query ("from", from.Value)
                  if ``to``.IsSome then
                      RequestPart.query ("to", ``to``.Value)
                  if smooth.IsSome then
                      RequestPart.query ("smooth", smooth.Value)
                  if includeFirst.IsSome then
                      RequestPart.query ("includeFirst", includeFirst.Value)
                  if dailyAlignment.IsSome then
                      RequestPart.query ("dailyAlignment", dailyAlignment.Value)
                  if alignmentTimezone.IsSome then
                      RequestPart.query ("alignmentTimezone", alignmentTimezone.Value)
                  if weeklyAlignment.IsSome then
                      RequestPart.query ("weeklyAlignment", weeklyAlignment.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/instruments/{instrument}/candles" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstrumentCandles.OK(Serializer.deserialize content)
            | 400 -> return GetInstrumentCandles.BadRequest(Serializer.deserialize content)
            | 401 -> return GetInstrumentCandles.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetInstrumentCandles.NotFound(Serializer.deserialize content)
            | _ -> return GetInstrumentCandles.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch a price for an instrument. Accounts are not associated in any way with this endpoint.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="time">The time at which the desired price is in effect. The current price is returned if no time is provided.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentPrice
        (
            authorization: string,
            instrument: string,
            ?acceptDatetimeFormat: string,
            ?time: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if time.IsSome then
                      RequestPart.query ("time", time.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/instruments/{instrument}/price" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstrumentPrice.OK(Serializer.deserialize content)
            | 400 -> return GetInstrumentPrice.BadRequest(Serializer.deserialize content)
            | 401 -> return GetInstrumentPrice.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetInstrumentPrice.NotFound(Serializer.deserialize content)
            | _ -> return GetInstrumentPrice.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch a range of prices for an instrument. Accounts are not associated in any way with this endpoint.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="from">The start of the time range to fetch prices for.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="to">The end of the time range to fetch prices for. The current time is used if this parameter is not provided.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentPriceRange
        (
            authorization: string,
            instrument: string,
            from: string,
            ?acceptDatetimeFormat: string,
            ?``to``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  RequestPart.query ("from", from)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if ``to``.IsSome then
                      RequestPart.query ("to", ``to``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/instruments/{instrument}/price/range" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstrumentPriceRange.OK(Serializer.deserialize content)
            | 400 -> return GetInstrumentPriceRange.BadRequest(Serializer.deserialize content)
            | 401 -> return GetInstrumentPriceRange.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetInstrumentPriceRange.NotFound(Serializer.deserialize content)
            | _ -> return GetInstrumentPriceRange.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch an order book for an instrument.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="time">The time of the snapshot to fetch. If not specified, then the most recent snapshot is fetched.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentsOrderBookByInstrument
        (
            authorization: string,
            instrument: string,
            ?acceptDatetimeFormat: string,
            ?time: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if time.IsSome then
                      RequestPart.query ("time", time.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/instruments/{instrument}/orderBook" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstrumentsOrderBookByInstrument.OK(Serializer.deserialize content)
            | 400 -> return GetInstrumentsOrderBookByInstrument.BadRequest(Serializer.deserialize content)
            | 401 -> return GetInstrumentsOrderBookByInstrument.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetInstrumentsOrderBookByInstrument.NotFound(Serializer.deserialize content)
            | _ -> return GetInstrumentsOrderBookByInstrument.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch a position book for an instrument.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="time">The time of the snapshot to fetch. If not specified, then the most recent snapshot is fetched.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentsPositionBookByInstrument
        (
            authorization: string,
            instrument: string,
            ?acceptDatetimeFormat: string,
            ?time: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if time.IsSome then
                      RequestPart.query ("time", time.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/instruments/{instrument}/positionBook" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstrumentsPositionBookByInstrument.OK(Serializer.deserialize content)
            | 400 -> return GetInstrumentsPositionBookByInstrument.BadRequest(Serializer.deserialize content)
            | 401 -> return GetInstrumentsPositionBookByInstrument.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetInstrumentsPositionBookByInstrument.NotFound(Serializer.deserialize content)
            | _ -> return GetInstrumentsPositionBookByInstrument.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///List all Positions for an Account. The Positions returned are for every instrument that has had a position during the lifetime of an the Account.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="cancellationToken"></param>
    member this.ListPositions(authorization: string, accountID: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/positions" requestParts cancellationToken

            match int status with
            | 200 -> return ListPositions.OK(Serializer.deserialize content)
            | 401 -> return ListPositions.Unauthorized(Serializer.deserialize content)
            | 404 -> return ListPositions.NotFound(Serializer.deserialize content)
            | _ -> return ListPositions.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///List all open Positions for an Account. An open Position is a Position in an Account that currently has a Trade opened for it.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="cancellationToken"></param>
    member this.ListOpenPositions(authorization: string, accountID: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/openPositions" requestParts cancellationToken

            match int status with
            | 200 -> return ListOpenPositions.OK(Serializer.deserialize content)
            | 401 -> return ListOpenPositions.Unauthorized(Serializer.deserialize content)
            | 404 -> return ListOpenPositions.NotFound(Serializer.deserialize content)
            | _ -> return ListOpenPositions.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the details of a single Instrument's Position in an Account. The Position may by open or not.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="cancellationToken"></param>
    member this.GetPosition
        (
            authorization: string,
            accountID: string,
            instrument: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("instrument", instrument) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/positions/{instrument}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetPosition.OK(Serializer.deserialize content)
            | 401 -> return GetPosition.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetPosition.NotFound(Serializer.deserialize content)
            | _ -> return GetPosition.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Closeout the open Position for a specific instrument in an Account.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="closePositionBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.ClosePosition
        (
            authorization: string,
            accountID: string,
            instrument: string,
            closePositionBody: ClosePositionPayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("instrument", instrument)
                  RequestPart.jsonContent closePositionBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/positions/{instrument}/close"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return ClosePosition.OK(Serializer.deserialize content)
            | 400 -> return ClosePosition.BadRequest(Serializer.deserialize content)
            | 401 -> return ClosePosition.Unauthorized(Serializer.deserialize content)
            | 404 -> return ClosePosition.NotFound(Serializer.deserialize content)
            | _ -> return ClosePosition.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a list of Trades for an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="ids">List of Trade IDs to retrieve.</param>
    ///<param name="state">The state to filter the requested Trades by.</param>
    ///<param name="instrument">The instrument to filter the requested Trades by.</param>
    ///<param name="count">The maximum number of Trades to return.</param>
    ///<param name="beforeID">The maximum Trade ID to return. If not provided the most recent Trades in the Account are returned.</param>
    ///<param name="cancellationToken"></param>
    member this.ListTrades
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?ids: list<string>,
            ?state: string,
            ?instrument: string,
            ?count: int,
            ?beforeID: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if ids.IsSome then
                      RequestPart.query ("ids", ids.Value)
                  if state.IsSome then
                      RequestPart.query ("state", state.Value)
                  if instrument.IsSome then
                      RequestPart.query ("instrument", instrument.Value)
                  if count.IsSome then
                      RequestPart.query ("count", count.Value)
                  if beforeID.IsSome then
                      RequestPart.query ("beforeID", beforeID.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/trades" requestParts cancellationToken

            match int status with
            | 200 -> return ListTrades.OK(Serializer.deserialize content)
            | 401 -> return ListTrades.Unauthorized(Serializer.deserialize content)
            | 404 -> return ListTrades.NotFound(Serializer.deserialize content)
            | _ -> return ListTrades.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the list of open Trades for an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.ListOpenTrades
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/openTrades" requestParts cancellationToken

            match int status with
            | 200 -> return ListOpenTrades.OK(Serializer.deserialize content)
            | 401 -> return ListOpenTrades.Unauthorized(Serializer.deserialize content)
            | 404 -> return ListOpenTrades.NotFound(Serializer.deserialize content)
            | _ -> return ListOpenTrades.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the details of a specific Trade in an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="tradeSpecifier">Specifier for the Trade</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetTrade
        (
            authorization: string,
            accountID: string,
            tradeSpecifier: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("tradeSpecifier", tradeSpecifier)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/trades/{tradeSpecifier}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetTrade.OK(Serializer.deserialize content)
            | 401 -> return GetTrade.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetTrade.NotFound(Serializer.deserialize content)
            | _ -> return GetTrade.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Close (partially or fully) a specific open Trade in an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="tradeSpecifier">Specifier for the Trade</param>
    ///<param name="closeTradeBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.CloseTrade
        (
            authorization: string,
            accountID: string,
            tradeSpecifier: string,
            closeTradeBody: CloseTradePayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("tradeSpecifier", tradeSpecifier)
                  RequestPart.jsonContent closeTradeBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/trades/{tradeSpecifier}/close"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return CloseTrade.OK(Serializer.deserialize content)
            | 400 -> return CloseTrade.BadRequest(Serializer.deserialize content)
            | 401 -> return CloseTrade.Unauthorized(Serializer.deserialize content)
            | 404 -> return CloseTrade.NotFound(Serializer.deserialize content)
            | _ -> return CloseTrade.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Update the Client Extensions for a Trade. Do not add, update, or delete the Client Extensions if your account is associated with MT4.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="tradeSpecifier">Specifier for the Trade</param>
    ///<param name="setTradeClientExtensionsBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.SetTradeClientExtensions
        (
            authorization: string,
            accountID: string,
            tradeSpecifier: string,
            setTradeClientExtensionsBody: SetTradeClientExtensionsPayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("tradeSpecifier", tradeSpecifier)
                  RequestPart.jsonContent setTradeClientExtensionsBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/trades/{tradeSpecifier}/clientExtensions"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return SetTradeClientExtensions.OK(Serializer.deserialize content)
            | 400 -> return SetTradeClientExtensions.BadRequest(Serializer.deserialize content)
            | 401 -> return SetTradeClientExtensions.Unauthorized(Serializer.deserialize content)
            | 404 -> return SetTradeClientExtensions.NotFound(Serializer.deserialize content)
            | _ -> return SetTradeClientExtensions.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Create, replace and cancel a Trade's dependent Orders (Take Profit, Stop Loss and Trailing Stop Loss) through the Trade itself
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="tradeSpecifier">Specifier for the Trade</param>
    ///<param name="setTradeDependentOrdersBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.SetTradeDependentOrders
        (
            authorization: string,
            accountID: string,
            tradeSpecifier: string,
            setTradeDependentOrdersBody: SetTradeDependentOrdersPayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("tradeSpecifier", tradeSpecifier)
                  RequestPart.jsonContent setTradeDependentOrdersBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/trades/{tradeSpecifier}/orders"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return SetTradeDependentOrders.OK(Serializer.deserialize content)
            | 400 -> return SetTradeDependentOrders.BadRequest(Serializer.deserialize content)
            | 401 -> return SetTradeDependentOrders.Unauthorized(Serializer.deserialize content)
            | 404 -> return SetTradeDependentOrders.NotFound(Serializer.deserialize content)
            | _ -> return SetTradeDependentOrders.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a list of all Accounts authorized for the provided token.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="cancellationToken"></param>
    member this.ListAccounts(authorization: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/accounts" requestParts cancellationToken

            match int status with
            | 200 -> return ListAccounts.OK(Serializer.deserialize content)
            | 401 -> return ListAccounts.Unauthorized(Serializer.deserialize content)
            | _ -> return ListAccounts.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the full details for a single Account that a client has access to. Full pending Order, open Trade and open Position representations are provided.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAccount
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}" requestParts cancellationToken

            match int status with
            | 200 -> return GetAccount.OK(Serializer.deserialize content)
            | 400 -> return GetAccount.BadRequest(Serializer.deserialize content)
            | 401 -> return GetAccount.Unauthorized(Serializer.deserialize content)
            | _ -> return GetAccount.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a summary for a single Account that a client has access to.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAccountSummary
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/summary" requestParts cancellationToken

            match int status with
            | 200 -> return GetAccountSummary.OK(Serializer.deserialize content)
            | 400 -> return GetAccountSummary.BadRequest(Serializer.deserialize content)
            | 401 -> return GetAccountSummary.Unauthorized(Serializer.deserialize content)
            | _ -> return GetAccountSummary.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the list of tradeable instruments for the given Account. The list of tradeable instruments is dependent on the regulatory division that the Account is located in, thus should be the same for all Accounts owned by a single user.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="instruments">List of instruments to query specifically.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAccountInstruments
        (
            authorization: string,
            accountID: string,
            ?instruments: list<string>,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if instruments.IsSome then
                      RequestPart.query ("instruments", instruments.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/instruments" requestParts cancellationToken

            match int status with
            | 200 -> return GetAccountInstruments.OK(Serializer.deserialize content)
            | 400 -> return GetAccountInstruments.BadRequest(Serializer.deserialize content)
            | 401 -> return GetAccountInstruments.Unauthorized(Serializer.deserialize content)
            | _ -> return GetAccountInstruments.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Set the client-configurable portions of an Account.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    ///<param name="configureAccountBody"></param>
    member this.ConfigureAccount
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken,
            ?configureAccountBody: ConfigureAccountPayload
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if configureAccountBody.IsSome then
                      RequestPart.jsonContent configureAccountBody.Value ]

            let! (status, content) =
                OpenApiHttp.patchAsync httpClient "/accounts/{accountID}/configuration" requestParts cancellationToken

            match int status with
            | 200 -> return ConfigureAccount.OK(Serializer.deserialize content)
            | 400 -> return ConfigureAccount.BadRequest(Serializer.deserialize content)
            | 401 -> return ConfigureAccount.Unauthorized(Serializer.deserialize content)
            | 403 -> return ConfigureAccount.Forbidden(Serializer.deserialize content)
            | 404 -> return ConfigureAccount.NotFound(Serializer.deserialize content)
            | _ -> return ConfigureAccount.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Endpoint used to poll an Account for its current state and changes since a specified TransactionID.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="sinceTransactionID">ID of the Transaction to get Account changes since.</param>
    ///<param name="cancellationToken"></param>
    member this.GetAccountChanges
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?sinceTransactionID: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if sinceTransactionID.IsSome then
                      RequestPart.query ("sinceTransactionID", sinceTransactionID.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/changes" requestParts cancellationToken

            match int status with
            | 200 -> return GetAccountChanges.OK(Serializer.deserialize content)
            | 401 -> return GetAccountChanges.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetAccountChanges.NotFound(Serializer.deserialize content)
            | 405 -> return GetAccountChanges.MethodNotAllowed(Serializer.deserialize content)
            | _ -> return GetAccountChanges.RequestedRangeNotSatisfiable(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a list of Transactions pages that satisfy a time-based Transaction query.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="from">The starting time (inclusive) of the time range for the Transactions being queried.</param>
    ///<param name="to">The ending time (inclusive) of the time range for the Transactions being queried.</param>
    ///<param name="pageSize">The number of Transactions to include in each page of the results.</param>
    ///<param name="type">A filter for restricting the types of Transactions to retreive.</param>
    ///<param name="cancellationToken"></param>
    member this.ListTransactions
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?from: string,
            ?``to``: string,
            ?pageSize: int,
            ?``type``: list<string>,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if from.IsSome then
                      RequestPart.query ("from", from.Value)
                  if ``to``.IsSome then
                      RequestPart.query ("to", ``to``.Value)
                  if pageSize.IsSome then
                      RequestPart.query ("pageSize", pageSize.Value)
                  if ``type``.IsSome then
                      RequestPart.query ("type", ``type``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/transactions" requestParts cancellationToken

            match int status with
            | 200 -> return ListTransactions.OK(Serializer.deserialize content)
            | 400 -> return ListTransactions.BadRequest(Serializer.deserialize content)
            | 401 -> return ListTransactions.Unauthorized(Serializer.deserialize content)
            | 403 -> return ListTransactions.Forbidden(Serializer.deserialize content)
            | 404 -> return ListTransactions.NotFound(Serializer.deserialize content)
            | 405 -> return ListTransactions.MethodNotAllowed(Serializer.deserialize content)
            | _ -> return ListTransactions.RequestedRangeNotSatisfiable(Serializer.deserialize content)
        }

    ///<summary>
    ///Get the details of a single Account Transaction.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="transactionID">A Transaction ID</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetTransaction
        (
            authorization: string,
            accountID: string,
            transactionID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("transactionID", transactionID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/transactions/{transactionID}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetTransaction.OK(Serializer.deserialize content)
            | 401 -> return GetTransaction.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetTransaction.NotFound(Serializer.deserialize content)
            | _ -> return GetTransaction.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a range of Transactions for an Account based on the Transaction IDs.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="from">The starting Transacion ID (inclusive) to fetch.</param>
    ///<param name="to">The ending Transaction ID (inclusive) to fetch.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="type">The filter that restricts the types of Transactions to retreive.</param>
    ///<param name="cancellationToken"></param>
    member this.GetTransactionRange
        (
            authorization: string,
            accountID: string,
            from: string,
            ``to``: string,
            ?acceptDatetimeFormat: string,
            ?``type``: list<string>,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.query ("from", from)
                  RequestPart.query ("to", ``to``)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if ``type``.IsSome then
                      RequestPart.query ("type", ``type``.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/transactions/idrange"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetTransactionRange.OK(Serializer.deserialize content)
            | 400 -> return GetTransactionRange.BadRequest(Serializer.deserialize content)
            | 401 -> return GetTransactionRange.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetTransactionRange.NotFound(Serializer.deserialize content)
            | 405 -> return GetTransactionRange.MethodNotAllowed(Serializer.deserialize content)
            | _ -> return GetTransactionRange.RequestedRangeNotSatisfiable(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a range of Transactions for an Account starting at (but not including) a provided Transaction ID.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="id">The ID of the last Transacion fetched. This query will return all Transactions newer than the TransactionID.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetTransactionsSinceId
        (
            authorization: string,
            accountID: string,
            id: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.query ("id", id)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/transactions/sinceid"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetTransactionsSinceId.OK(Serializer.deserialize content)
            | 400 -> return GetTransactionsSinceId.BadRequest(Serializer.deserialize content)
            | 401 -> return GetTransactionsSinceId.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetTransactionsSinceId.NotFound(Serializer.deserialize content)
            | 405 -> return GetTransactionsSinceId.MethodNotAllowed(Serializer.deserialize content)
            | _ -> return GetTransactionsSinceId.RequestedRangeNotSatisfiable(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a stream of Transactions for an Account starting from when the request is made.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="cancellationToken"></param>
    member this.StreamTransactions(authorization: string, accountID: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/transactions/stream"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return StreamTransactions.OK(Serializer.deserialize content)
            | 400 -> return StreamTransactions.BadRequest(Serializer.deserialize content)
            | 401 -> return StreamTransactions.Unauthorized(Serializer.deserialize content)
            | 404 -> return StreamTransactions.NotFound(Serializer.deserialize content)
            | _ -> return StreamTransactions.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch the user information for the specified user. This endpoint is intended to be used by the user themself to obtain their own information.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="userSpecifier">The User Specifier</param>
    ///<param name="cancellationToken"></param>
    member this.GetUserInfo(authorization: string, userSpecifier: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("userSpecifier", userSpecifier) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/users/{userSpecifier}" requestParts cancellationToken

            match int status with
            | 200 -> return GetUserInfo.OK(Serializer.deserialize content)
            | 401 -> return GetUserInfo.Unauthorized(Serializer.deserialize content)
            | 403 -> return GetUserInfo.Forbidden(Serializer.deserialize content)
            | _ -> return GetUserInfo.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch the externally-available user information for the specified user. This endpoint is intended to be used by 3rd parties that have been authorized by a user to view their personal information.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="userSpecifier">The User Specifier</param>
    ///<param name="cancellationToken"></param>
    member this.GetExternalUserInfo
        (
            authorization: string,
            userSpecifier: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("userSpecifier", userSpecifier) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/users/{userSpecifier}/externalInfo" requestParts cancellationToken

            match int status with
            | 200 -> return GetExternalUserInfo.OK(Serializer.deserialize content)
            | 401 -> return GetExternalUserInfo.Unauthorized(Serializer.deserialize content)
            | 403 -> return GetExternalUserInfo.Forbidden(Serializer.deserialize content)
            | _ -> return GetExternalUserInfo.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get pricing information for a specified instrument. Accounts are not associated in any way with this endpoint.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="time">The time at which the desired price for each instrument is in effect. The current price for each instrument is returned if no time is provided.</param>
    ///<param name="cancellationToken"></param>
    member this.GetBasePrices
        (
            authorization: string,
            ?acceptDatetimeFormat: string,
            ?time: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if time.IsSome then
                      RequestPart.query ("time", time.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/pricing" requestParts cancellationToken

            match int status with
            | 200 -> return GetBasePrices.OK(Serializer.deserialize content)
            | 400 -> return GetBasePrices.BadRequest(Serializer.deserialize content)
            | 401 -> return GetBasePrices.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetBasePrices.NotFound(Serializer.deserialize content)
            | _ -> return GetBasePrices.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get pricing information for a specified range of prices. Accounts are not associated in any way with this endpoint.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="from">The start of the time range to fetch prices for.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="to">The end of the time range to fetch prices for. The current time is used if this parameter is not provided.</param>
    ///<param name="cancellationToken"></param>
    member this.GetPriceRange
        (
            authorization: string,
            instrument: string,
            from: string,
            ?acceptDatetimeFormat: string,
            ?``to``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  RequestPart.query ("from", from)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if ``to``.IsSome then
                      RequestPart.query ("to", ``to``.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/pricing/range" requestParts cancellationToken

            match int status with
            | 200 -> return GetPriceRange.OK(Serializer.deserialize content)
            | 400 -> return GetPriceRange.BadRequest(Serializer.deserialize content)
            | 401 -> return GetPriceRange.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetPriceRange.NotFound(Serializer.deserialize content)
            | _ -> return GetPriceRange.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get pricing information for a specified list of Instruments within an Account.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="instruments">List of Instruments to get pricing for.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="since">Date/Time filter to apply to the response. Only prices and home conversions (if requested) with a time later than this filter (i.e. the price has changed after the since time) will be provided, and are filtered independently.</param>
    ///<param name="includeUnitsAvailable">Flag that enables the inclusion of the unitsAvailable field in the returned Price objects.</param>
    ///<param name="includeHomeConversions">Flag that enables the inclusion of the homeConversions field in the returned response. An entry will be returned for each currency in the set of all base and quote currencies present in the requested instruments list.</param>
    ///<param name="cancellationToken"></param>
    member this.GetPrices
        (
            authorization: string,
            accountID: string,
            instruments: list<string>,
            ?acceptDatetimeFormat: string,
            ?since: string,
            ?includeUnitsAvailable: bool,
            ?includeHomeConversions: bool,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.query ("instruments", instruments)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if since.IsSome then
                      RequestPart.query ("since", since.Value)
                  if includeUnitsAvailable.IsSome then
                      RequestPart.query ("includeUnitsAvailable", includeUnitsAvailable.Value)
                  if includeHomeConversions.IsSome then
                      RequestPart.query ("includeHomeConversions", includeHomeConversions.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/pricing" requestParts cancellationToken

            match int status with
            | 200 -> return GetPrices.OK(Serializer.deserialize content)
            | 400 -> return GetPrices.BadRequest(Serializer.deserialize content)
            | 401 -> return GetPrices.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetPrices.NotFound(Serializer.deserialize content)
            | _ -> return GetPrices.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a stream of Account Prices starting from when the request is made.
    ///This pricing stream does not include every single price created for the Account, but instead will provide at most 4 prices per second (every 250 milliseconds) for each instrument being requested.
    ///If more than one price is created for an instrument during the 250 millisecond window, only the price in effect at the end of the window is sent. This means that during periods of rapid price movement, subscribers to this stream will not be sent every price.
    ///Pricing windows for different connections to the price stream are not all aligned in the same way (i.e. they are not all aligned to the top of the second). This means that during periods of rapid price movement, different subscribers may observe different prices depending on their alignment.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="instruments">List of Instruments to stream Prices for.</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="snapshot">Flag that enables/disables the sending of a pricing snapshot when initially connecting to the stream.</param>
    ///<param name="cancellationToken"></param>
    member this.StreamPricing
        (
            authorization: string,
            accountID: string,
            instruments: list<string>,
            ?acceptDatetimeFormat: string,
            ?snapshot: bool,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.query ("instruments", instruments)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if snapshot.IsSome then
                      RequestPart.query ("snapshot", snapshot.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/pricing/stream" requestParts cancellationToken

            match int status with
            | 200 -> return StreamPricing.OK(Serializer.deserialize content)
            | 400 -> return StreamPricing.BadRequest(Serializer.deserialize content)
            | 401 -> return StreamPricing.Unauthorized(Serializer.deserialize content)
            | 404 -> return StreamPricing.NotFound(Serializer.deserialize content)
            | _ -> return StreamPricing.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Fetch candlestick data for an instrument.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="instrument">Name of the Instrument</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="price">The Price component(s) to get candlestick data for. Can contain any combination of the characters "M" (midpoint candles) "B" (bid candles) and "A" (ask candles).</param>
    ///<param name="granularity">The granularity of the candlesticks to fetch</param>
    ///<param name="count">The number of candlesticks to return in the response. Count should not be specified if both the start and end parameters are provided, as the time range combined with the granularity will determine the number of candlesticks to return.</param>
    ///<param name="from">The start of the time range to fetch candlesticks for.</param>
    ///<param name="to">The end of the time range to fetch candlesticks for.</param>
    ///<param name="smooth">A flag that controls whether the candlestick is "smoothed" or not.  A smoothed candlestick uses the previous candle's close price as its open price, while an unsmoothed candlestick uses the first price from its time range as its open price.</param>
    ///<param name="includeFirst">A flag that controls whether the candlestick that is covered by the from time should be included in the results. This flag enables clients to use the timestamp of the last completed candlestick received to poll for future candlesticks but avoid receiving the previous candlestick repeatedly.</param>
    ///<param name="dailyAlignment">The hour of the day (in the specified timezone) to use for granularities that have daily alignments.</param>
    ///<param name="alignmentTimezone">The timezone to use for the dailyAlignment parameter. Candlesticks with daily alignment will be aligned to the dailyAlignment hour within the alignmentTimezone.  Note that the returned times will still be represented in UTC.</param>
    ///<param name="weeklyAlignment">The day of the week used for granularities that have weekly alignment.</param>
    ///<param name="units">The number of units used to calculate the volume-weighted average bid and ask prices in the returned candles.</param>
    ///<param name="cancellationToken"></param>
    member this.GetInstrumentCandles
        (
            authorization: string,
            instrument: string,
            ?acceptDatetimeFormat: string,
            ?price: string,
            ?granularity: string,
            ?count: int,
            ?from: string,
            ?``to``: string,
            ?smooth: bool,
            ?includeFirst: bool,
            ?dailyAlignment: int,
            ?alignmentTimezone: string,
            ?weeklyAlignment: string,
            ?units: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("instrument", instrument)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if price.IsSome then
                      RequestPart.query ("price", price.Value)
                  if granularity.IsSome then
                      RequestPart.query ("granularity", granularity.Value)
                  if count.IsSome then
                      RequestPart.query ("count", count.Value)
                  if from.IsSome then
                      RequestPart.query ("from", from.Value)
                  if ``to``.IsSome then
                      RequestPart.query ("to", ``to``.Value)
                  if smooth.IsSome then
                      RequestPart.query ("smooth", smooth.Value)
                  if includeFirst.IsSome then
                      RequestPart.query ("includeFirst", includeFirst.Value)
                  if dailyAlignment.IsSome then
                      RequestPart.query ("dailyAlignment", dailyAlignment.Value)
                  if alignmentTimezone.IsSome then
                      RequestPart.query ("alignmentTimezone", alignmentTimezone.Value)
                  if weeklyAlignment.IsSome then
                      RequestPart.query ("weeklyAlignment", weeklyAlignment.Value)
                  if units.IsSome then
                      RequestPart.query ("units", units.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/instruments/{instrument}/candles"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetGetInstrumentCandles.OK(Serializer.deserialize content)
            | 400 -> return GetGetInstrumentCandles.BadRequest(Serializer.deserialize content)
            | 401 -> return GetGetInstrumentCandles.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetGetInstrumentCandles.NotFound(Serializer.deserialize content)
            | _ -> return GetGetInstrumentCandles.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Create an Order for an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="createOrderBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.CreateOrder
        (
            authorization: string,
            accountID: string,
            createOrderBody: CreateOrderPayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.jsonContent createOrderBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/accounts/{accountID}/orders" requestParts cancellationToken

            match int status with
            | 201 -> return CreateOrder.Created(Serializer.deserialize content)
            | 400 -> return CreateOrder.BadRequest(Serializer.deserialize content)
            | 401 -> return CreateOrder.Unauthorized(Serializer.deserialize content)
            | 403 -> return CreateOrder.Forbidden(Serializer.deserialize content)
            | 404 -> return CreateOrder.NotFound(Serializer.deserialize content)
            | _ -> return CreateOrder.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get a list of Orders for an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="ids">List of Order IDs to retrieve</param>
    ///<param name="state">The state to filter the requested Orders by</param>
    ///<param name="instrument">The instrument to filter the requested orders by</param>
    ///<param name="count">The maximum number of Orders to return</param>
    ///<param name="beforeID">The maximum Order ID to return. If not provided the most recent Orders in the Account are returned</param>
    ///<param name="cancellationToken"></param>
    member this.ListOrders
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?ids: list<string>,
            ?state: string,
            ?instrument: string,
            ?count: int,
            ?beforeID: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if ids.IsSome then
                      RequestPart.query ("ids", ids.Value)
                  if state.IsSome then
                      RequestPart.query ("state", state.Value)
                  if instrument.IsSome then
                      RequestPart.query ("instrument", instrument.Value)
                  if count.IsSome then
                      RequestPart.query ("count", count.Value)
                  if beforeID.IsSome then
                      RequestPart.query ("beforeID", beforeID.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/orders" requestParts cancellationToken

            match int status with
            | 200 -> return ListOrders.OK(Serializer.deserialize content)
            | 400 -> return ListOrders.BadRequest(Serializer.deserialize content)
            | 404 -> return ListOrders.NotFound(Serializer.deserialize content)
            | _ -> return ListOrders.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///List all pending Orders in an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.ListPendingOrders
        (
            authorization: string,
            accountID: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/accounts/{accountID}/pendingOrders" requestParts cancellationToken

            match int status with
            | 200 -> return ListPendingOrders.OK(Serializer.deserialize content)
            | 401 -> return ListPendingOrders.Unauthorized(Serializer.deserialize content)
            | 404 -> return ListPendingOrders.NotFound(Serializer.deserialize content)
            | _ -> return ListPendingOrders.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Get details for a single Order in an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="orderSpecifier">The Order Specifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.GetOrder
        (
            authorization: string,
            accountID: string,
            orderSpecifier: string,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("orderSpecifier", orderSpecifier)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/accounts/{accountID}/orders/{orderSpecifier}"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetOrder.OK(Serializer.deserialize content)
            | 401 -> return GetOrder.Unauthorized(Serializer.deserialize content)
            | 404 -> return GetOrder.NotFound(Serializer.deserialize content)
            | _ -> return GetOrder.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Replace an Order in an Account by simultaneously cancelling it and creating a replacement Order
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="orderSpecifier">The Order Specifier</param>
    ///<param name="replaceOrderBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="clientRequestID">Client specified RequestID to be sent with request.</param>
    ///<param name="cancellationToken"></param>
    member this.ReplaceOrder
        (
            authorization: string,
            accountID: string,
            orderSpecifier: string,
            replaceOrderBody: ReplaceOrderPayload,
            ?acceptDatetimeFormat: string,
            ?clientRequestID: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("orderSpecifier", orderSpecifier)
                  RequestPart.jsonContent replaceOrderBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if clientRequestID.IsSome then
                      RequestPart.header ("ClientRequestID", clientRequestID.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/orders/{orderSpecifier}"
                    requestParts
                    cancellationToken

            match int status with
            | 201 -> return ReplaceOrder.Created(Serializer.deserialize content)
            | 400 -> return ReplaceOrder.BadRequest(Serializer.deserialize content)
            | 401 -> return ReplaceOrder.Unauthorized(Serializer.deserialize content)
            | 404 -> return ReplaceOrder.NotFound(Serializer.deserialize content)
            | _ -> return ReplaceOrder.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Cancel a pending Order in an Account
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="orderSpecifier">The Order Specifier</param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="clientRequestID">Client specified RequestID to be sent with request.</param>
    ///<param name="cancellationToken"></param>
    member this.CancelOrder
        (
            authorization: string,
            accountID: string,
            orderSpecifier: string,
            ?acceptDatetimeFormat: string,
            ?clientRequestID: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("orderSpecifier", orderSpecifier)
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value)
                  if clientRequestID.IsSome then
                      RequestPart.header ("ClientRequestID", clientRequestID.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/orders/{orderSpecifier}/cancel"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return CancelOrder.OK(Serializer.deserialize content)
            | 401 -> return CancelOrder.Unauthorized(Serializer.deserialize content)
            | 404 -> return CancelOrder.NotFound(Serializer.deserialize content)
            | _ -> return CancelOrder.MethodNotAllowed(Serializer.deserialize content)
        }

    ///<summary>
    ///Update the Client Extensions for an Order in an Account. Do not set, modify, or delete clientExtensions if your account is associated with MT4.
    ///</summary>
    ///<param name="authorization">The authorization bearer token previously obtained by the client</param>
    ///<param name="accountID">Account Identifier</param>
    ///<param name="orderSpecifier">The Order Specifier</param>
    ///<param name="setOrderClientExtensionsBody"></param>
    ///<param name="acceptDatetimeFormat">Format of DateTime fields in the request and response.</param>
    ///<param name="cancellationToken"></param>
    member this.SetOrderClientExtensions
        (
            authorization: string,
            accountID: string,
            orderSpecifier: string,
            setOrderClientExtensionsBody: SetOrderClientExtensionsPayload,
            ?acceptDatetimeFormat: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.header ("Authorization", authorization)
                  RequestPart.path ("accountID", accountID)
                  RequestPart.path ("orderSpecifier", orderSpecifier)
                  RequestPart.jsonContent setOrderClientExtensionsBody
                  if acceptDatetimeFormat.IsSome then
                      RequestPart.header ("Accept-Datetime-Format", acceptDatetimeFormat.Value) ]

            let! (status, content) =
                OpenApiHttp.putAsync
                    httpClient
                    "/accounts/{accountID}/orders/{orderSpecifier}/clientExtensions"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return SetOrderClientExtensions.OK(Serializer.deserialize content)
            | 400 -> return SetOrderClientExtensions.BadRequest(Serializer.deserialize content)
            | 401 -> return SetOrderClientExtensions.Unauthorized(Serializer.deserialize content)
            | 404 -> return SetOrderClientExtensions.NotFound(Serializer.deserialize content)
            | _ -> return SetOrderClientExtensions.MethodNotAllowed(Serializer.deserialize content)
        }
