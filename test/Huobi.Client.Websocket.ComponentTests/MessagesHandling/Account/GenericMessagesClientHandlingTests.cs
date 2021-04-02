﻿using System;
using Huobi.Client.Websocket.Messages.Account;
using Moq;
using Xunit;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling.Account
{
    public class GenericMessagesClientHandlingTests : ClientMessagesHandlingTestsBase
    {
        [Fact]
        public void Ping_RespondsWithPong()
        {
            // Arrange
            InitializeAccountClient();
            var message = new AccountPingRequest(12345);

            // Act
            TriggerMessageReceive(message);

            // Assert
            AccountCommunicatorMock.Verify(m => m.Send(It.Is<string>(x => x.Contains("pong") && x.Contains("12345"))), Times.Once);
            VerifyMessageNotUnhandled();
        }

        [Fact]
        public void HandleResponse_AuthErrorMessage_StreamUpdated()
        {
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.AuthErrorMessageStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                });

            var message = HuobiAccountMessagesFactory.CreateAuthErrorMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void HandleResponse_Authenticated_StreamUpdated()
        {
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.AuthenticationResponseStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(!string.IsNullOrEmpty(msg.Channel));
                });

            var message = HuobiAccountMessagesFactory.CreateAuthenticationResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }

        [Fact]
        public void HandleResponse_Subscribed_StreamUpdated()
        {
            var triggered = false;
            var client = InitializeAccountClient();
            client.Streams.SubscribeResponseStream.Subscribe(
                msg =>
                {
                    triggered = true;

                    // Assert
                    Assert.NotNull(msg);
                    Assert.True(msg.Code > 0);
                    Assert.True(!string.IsNullOrEmpty(msg.Channel));
                });

            var message = HuobiAccountMessagesFactory.CreateSubscribeResponseMessage();

            // Act
            TriggerMessageReceive(message);

            // Assert
            VerifyMessageNotUnhandled();
            Assert.True(triggered);
        }
    }
}