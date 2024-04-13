using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagmentQuery.Test.Fakers.Accepted;
using DemoUserManagmentQuery.Test.Fakers.Sent;
using DemoUserManagmentQuery.Test.Helper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DemoUserManagmentQuery.Test.HandlerTest.Accepted
{
    public class InvitationAcceptedHandlerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly EventHandlerHelper _handlerHelper;

        public InvitationAcceptedHandlerTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });

            _handlerHelper = new EventHandlerHelper(_factory.Services);
        }

        [Fact]
        public async Task InvitationAccepted_EventHandledWhenPendingInvitation_InvitationStatusUpdatedSubscriberSavedPermissionSequenceUpdated()
        {
            var sentEvent = new InvitationSentFaker(sequence: 1).Generate();

            await _handlerHelper.HandleAsync(sentEvent);

            var acceptedEvent = new InvitationAcceptedFaker(sentEvent).Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(acceptedEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var invite = await unitOfWork.Invitation.GetAllAsync();
            var subscriber = await unitOfWork.Subscriper.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isHandled);
            Assert.Single(invite);
            Assert.Single(subscriber);
            Assert.Single(permission);
            Assert.Equal(invite.First().SubscriptionId, subscriber.First().SubscriptionId);
            Assert.Equal(invite.First().UserId, subscriber.First().UserId);
            Assert.Equal("Accepted", invite.First().Status);
            Assert.Equal(invite.First().Sequence, permission.First().Sequence);
        }

        [Fact]
        public async Task InvitationAccepted_InvitationAcceptedEventHandledWithNoPendingInvitation_EventSetToWait()
        {
            var sentEvent = new InvitationSentFaker(sequence: 1).Generate();

            var acceptedEvent = new InvitationAcceptedFaker(sentEvent).Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(acceptedEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var invite = await unitOfWork.Invitation.GetAllAsync();
            var subscriber = await unitOfWork.Subscriper.GetAllAsync();

            Assert.False(isHandled);
            Assert.Empty(invite);
            Assert.Empty(subscriber);
        }

        [Fact]
        public async Task InvitationAccepted_DublicateInvitationAcceptedEventHandled_DuplicateEventIgnored()
        {
            var sentEvent = new InvitationSentFaker(sequence: 1).Generate();
            var acceptedEvent = new InvitationAcceptedFaker(sentEvent).Generate();

            await Task.WhenAll(
                _handlerHelper.HandleAsync(sentEvent),
                _handlerHelper.HandleAsync(acceptedEvent)
                );

            var isHandled = await _handlerHelper.TryHandleAsync(acceptedEvent);

            Assert.True(isHandled);
        }

        [Fact]
        public async Task InvitationAccepted_EventSequenceNotExpectedYet_EventSetToWait()
        {
            var sentEvent = new InvitationSentFaker(sequence: 1).Generate();
            await _handlerHelper.HandleAsync(sentEvent);

            var acceptedEvent = new InvitationAcceptedFaker(sentEvent)
                .RuleFor(e => e.Sequence, 3).Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(acceptedEvent);

            Assert.False(isHandled);
        }
    }
}
