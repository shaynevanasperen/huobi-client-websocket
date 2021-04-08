﻿using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthTick
    {
        [JsonConstructor]
        public MarketDepthTick(decimal[][] bids, decimal[][] asks, long version, long timestampMs)
        {
            Bids = bids;
            Asks = asks;
            Version = version;

            TimestampMs = timestampMs;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public decimal[][] Bids { get; }
        public decimal[][] Asks { get; }
        public long Version { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }
    }
}