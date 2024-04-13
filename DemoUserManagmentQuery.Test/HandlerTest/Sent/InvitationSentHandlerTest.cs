using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagmentQuery.Test.Fakers.Cancelled;
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

namespace DemoUserManagmentQuery.Test.HandlerTest.Sent
{
    public class InvitationSentHandlerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly EventHandlerHelper _handlerHelper;

        public InvitationSentHandlerTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });

            _handlerHelper = new EventHandlerHelper(_factory.Services);
        }

        [Fact]
        public async Task InvitationSent_UserFirstInvitationSentEventHandled_PermissionAndInvitationSaved()
        {
            var @event = new InvitationSentFaker(sequence: 1).Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(@event);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var invite = await unitOfWork.Invitation.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isHandled);
            Assert.Single(invite);
            Assert.Single(permission);
            Assert.Equal(@event.AggregateId, invite.First().Id);
            Assert.Equal("Pending", invite.First().Status);
        }

        [Fact]
        public async Task InvitationSent_DuplicateInvitationSentEventHandled_DuplicateEventIgnored()
        {
            var @event = new InvitationSentFaker(sequence: 1).Generate();
            await _handlerHelper.HandleAsync(@event);

            var isHandled = await _handlerHelper.TryHandleAsync(@event);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var invite = await unitOfWork.Invitation.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isHandled);
            Assert.Single(invite);
            Assert.Single(permission);
        }

        [Fact]
        public async Task InvitationSent_InvitationSentEventSequenceNotExpectedYet_EventSetToWait()
        {
            var firstEvent = new InvitationSentFaker(sequence: 1).Generate();

            var isFirstHandled = await _handlerHelper.TryHandleAsync(firstEvent);

            var secondEvent = new InvitationSentFaker(sequence: 3)
                .RuleFor(e => e.AggregateId, firstEvent.AggregateId)
                .Generate();

            var isSecondHandled = await _handlerHelper.TryHandleAsync(secondEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var invite = await unitOfWork.Invitation.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isFirstHandled);
            Assert.False(isSecondHandled);
            Assert.Single(invite);
            Assert.Single(permission);
        }

        [Fact]
        public async Task InvitationSent_InvitationSentEventHandledAfterCanceledOrRejectedOrLeftOrRemoved_InvitationStatusUpdatedPermissionSaved()
        {
            var firstSentEvent = new InvitationSentFaker(sequence: 1).Generate();
            var cancelEvent = new InvitationCancelledFaker(firstSentEvent).Generate();

            await Task.WhenAll(
                _handlerHelper.HandleAsync(firstSentEvent),
                _handlerHelper.HandleAsync(cancelEvent)
                );

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();



            var secondSentEvent = new InvitationSentFaker(sequence: 3)
                .RuleFor(i => i.AggregateId, firstSentEvent.AggregateId)
                .Generate();

            var isSecondHandled = await _handlerHelper.TryHandleAsync(secondSentEvent);

            var inviteAfter = await unitOfWork.Invitation.GetAllAsync();
            var permissionAfter = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isSecondHandled);
            Assert.Single(inviteAfter);
            Assert.Single(permissionAfter);
            Assert.Equal(secondSentEvent.AggregateId, inviteAfter.First().Id);
            Assert.Equal("Pending", inviteAfter.First().Status);
        }

    }
}
