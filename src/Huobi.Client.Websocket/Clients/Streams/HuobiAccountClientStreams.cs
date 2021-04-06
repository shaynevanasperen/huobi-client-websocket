﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.OrderUpdates;

namespace Huobi.Client.Websocket.Clients.Streams
{
    public class HuobiAccountClientStreams : HuobiClientStreamsBase
    {
        internal readonly Subject<AuthenticationResponse> AuthenticationResponseSubject = new();
        internal readonly Subject<AccountSubscribeResponse> SubscribeResponseSubject = new();

        internal readonly Subject<ConditionalOrderTriggerFailureMessage> ConditionalOrderTriggerFailureMessageSubject = new();
        internal readonly Subject<ConditionalOrderCanceledMessage> ConditionalOrderCanceledMessageSubject = new();
        internal readonly Subject<OrderSubmittedMessage> OrderSubmittedMessageSubject = new();
        internal readonly Subject<OrderTradedMessage> OrderTradedMessageSubject = new();
        internal readonly Subject<OrderCanceledMessage> OrderCanceledMessageSubject = new();

        public IObservable<AuthenticationResponse> AuthenticationResponseStream => AuthenticationResponseSubject.AsObservable();
        public IObservable<AccountSubscribeResponse> SubscribeResponseStream => SubscribeResponseSubject.AsObservable();
        
        public IObservable<ConditionalOrderTriggerFailureMessage> ConditionalOrderTriggerFailureMessageStream => ConditionalOrderTriggerFailureMessageSubject.AsObservable();
        public IObservable<ConditionalOrderCanceledMessage> ConditionalOrderCanceledMessageStream => ConditionalOrderCanceledMessageSubject.AsObservable();
        public IObservable<OrderSubmittedMessage> OrderSubmittedMessageStream => OrderSubmittedMessageSubject.AsObservable();
        public IObservable<OrderTradedMessage> OrderTradedMessageStream => OrderTradedMessageSubject.AsObservable();
        public IObservable<OrderCanceledMessage> OrderCanceledMessageStream => OrderCanceledMessageSubject.AsObservable();
    }
}