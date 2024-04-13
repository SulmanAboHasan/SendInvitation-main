using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagmentQuery.Test.Fakers.Accepted;
using DemoUserManagmentQuery.Test.Fakers.IncrementSequence;
using DemoUserManagmentQuery.Test.Fakers.Joined;
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

namespace DemoUserManagmentQuery.Test.HandlerTest.IncrementSequence
{
    public class UnknownEventHandlerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly EventHandlerHelper _handlerHelper;

        public UnknownEventHandlerTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });

            _handlerHelper = new EventHandlerHelper(_factory.Services);
        }

        [Fact]
        public async Task UnknownEvent_EventHandled_RelatedSequenceIncreases()
        {
            var sentEvent = new InvitationSentFaker(sequence: 1).Generate();
            var acceptedEvent = new InvitationAcceptedFaker(sentEvent).Generate();

            await Task.WhenAll(
                _handlerHelper.HandleAsync(sentEvent),
                _handlerHelper.HandleAsync(acceptedEvent)
                );


            var unknown = new UnknownEventFaker(sequence: 3)
                .RuleFor(e => e.AggregateId, acceptedEvent.AggregateId)
                .Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(unknown);


            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var subscribers = await unitOfWork.Subscriper.GetAllAsync();
            var invites = await unitOfWork.Invitation.GetAllAsync();
            var permissions = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isHandled);
            Assert.Single(subscribers);
            Assert.Single(invites);
            Assert.Single(permissions);
            Assert.Equal(3, subscribers.First().Sequence);
            Assert.Equal(3, invites.First().Sequence);
            Assert.Equal(3, permissions.First().Sequence);
        }

        [Fact]
        public async Task UnknownEvent_EventHandledWithNoRelatedEntity_EventSetToWait()
        {
            var unknown = new UnknownEventFaker(sequence: 3).Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(unknown);

            Assert.False(isHandled);
        }

        [Fact]
        public async Task UnknownEvent_DublicateEventHandled_DuplicateEventIgnored()
        {
            var joinedevent = new MemberJoinedFaker(sequence: 1).Generate();
            var unknown = new UnknownEventFaker(sequence: 2)
                .RuleFor(e => e.AggregateId, joinedevent.AggregateId)
                .Generate();

            await Task.WhenAll(_handlerHelper.HandleAsync(joinedevent),
                _handlerHelper.HandleAsync(unknown)
                );

            var isHandled = await _handlerHelper.TryHandleAsync(unknown);

            Assert.True(isHandled);
        }

        [Fact]
        public async Task UnknownEvent_EventSequenceNotExpectedYet_EventSetToWait()
        {
            var joinedevent = new MemberJoinedFaker(sequence: 1).Generate();
            await _handlerHelper.HandleAsync(joinedevent);

            var unknown = new UnknownEventFaker(sequence: 3)
                .RuleFor(e => e.AggregateId, joinedevent.AggregateId)
                .Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(unknown);

            Assert.False(isHandled);
        }
    }
}
