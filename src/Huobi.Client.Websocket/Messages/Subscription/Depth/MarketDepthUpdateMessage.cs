﻿using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Ticks;
using Huobi.Client.Websocket.Messages.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Subscription.Depth
{
    public class MarketDepthUpdateMessage : UpdateMessage<MarketDepthTick>
    {
        public MarketDepthUpdateMessage(string topic, long timestamp, MarketDepthTick tick)
            : base(topic, timestamp, tick)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketDepthUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"tick\"",
                    SubscriptionType.MarketDepth.ToTopicId()
                },
                out response);

            return result && response?.Tick.Timestamp > 0;
        }
    }
}