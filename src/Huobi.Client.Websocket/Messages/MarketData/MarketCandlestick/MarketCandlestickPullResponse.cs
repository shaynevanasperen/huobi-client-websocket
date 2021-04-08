﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick
{
    public class MarketCandlestickPullResponse : PullResponse<MarketCandlestickTick[]>
    {
        [JsonConstructor]
        public MarketCandlestickPullResponse(
            string reqId,
            string status,
            string topic,
            long timestampMs,
            MarketCandlestickTick[] data)
            : base(reqId, status, topic, timestampMs, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketCandlestickPullResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"rep\"",
                    SubscriptionType.MarketCandlestick.ToTopicId()
                },
                out response);

            return result && response?.Data.FirstOrDefault()?.Id > 0;
        }
    }
}