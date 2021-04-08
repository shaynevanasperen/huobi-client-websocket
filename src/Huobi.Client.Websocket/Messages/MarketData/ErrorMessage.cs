﻿using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class ErrorMessage
    {
        [JsonConstructor]
        public ErrorMessage(long timestampMs, string status, string errorCode, string message, string? reqId)
        {
            Validations.ValidateInput(status, nameof(status));
            Validations.ValidateInput(errorCode, nameof(errorCode));
            Validations.ValidateInput(message, nameof(message));
            
            TimestampMs = timestampMs;

            Status = status;
            ErrorCode = errorCode;
            Message = message;
            ReqId = reqId;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        public string Status { get; }

        [JsonProperty("err-code")]
        public string ErrorCode { get; }

        [JsonProperty("err-msg")]
        public string Message { get; }

        [JsonProperty("id")]
        public string? ReqId { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }

        internal static bool TryParse(IHuobiSerializer serializer, string input, [MaybeNullWhen(false)] out ErrorMessage response)
        {
            var result = serializer.TryDeserializeIfContains(input, "\"err-code\"", out response);
            return result && !string.IsNullOrEmpty(response?.ErrorCode);
        }
    }
}