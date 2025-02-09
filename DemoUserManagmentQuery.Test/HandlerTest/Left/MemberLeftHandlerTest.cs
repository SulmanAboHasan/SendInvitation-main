﻿using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagmentQuery.Test.Fakers.Joined;
using DemoUserManagmentQuery.Test.Fakers.Left;
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

namespace DemoUserManagmentQuery.Test.HandlerTest.Left
{
    public class MemberLeftHandlerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly EventHandlerHelper _handlerHelper;

        public MemberLeftHandlerTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper)
        {
            _factory = factory.WithDefaultConfigurations(helper, services =>
            {
                services.ReplaceWithInMemoryDatabase();
            });

            _handlerHelper = new EventHandlerHelper(_factory.Services);
        }

        [Fact]
        public async Task MemberLeft_MemberLeftEventHandled_SubscriberStatusUpdatedPermissionRemoved()
        {
            var joinedEvent = new MemberJoinedFaker(sequence: 1).Generate();
            await _handlerHelper.HandleAsync(joinedEvent);

            var leftEvent = new MemberLeftFaker(joinedEvent).Generate();
            var isHandled = await _handlerHelper.TryHandleAsync(leftEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var subscriber = await unitOfWork.Subscriper.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.True(isHandled);
            Assert.Single(subscriber);
            Assert.Equal("Left", subscriber.First().Status);
            Assert.Empty(permission);
        }

        [Fact]
        public async Task MemberLeft_MemberLeftEventHandled_WhenMemberNotJoinedYet_EventSetToWait()
        {
            var joinedEvent = new MemberJoinedFaker(sequence: 1).Generate();

            var leftEvent = new MemberLeftFaker(joinedEvent).Generate();
            var isHandled = await _handlerHelper.TryHandleAsync(leftEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var subscriber = await unitOfWork.Subscriper.GetAllAsync();

            Assert.False(isHandled);
            Assert.Empty(subscriber);
        }

        [Fact]
        public async Task MemberLeft_DuplicateMemberLeftEventHandled_DuplicateEventIgnored()
        {
            var joinedEvent = new MemberJoinedFaker(sequence: 1).Generate();
            var leftEvent = new MemberLeftFaker(joinedEvent).Generate();

            await Task.WhenAll(
            _handlerHelper.HandleAsync(joinedEvent),
            _handlerHelper.HandleAsync(leftEvent)
           );

            var isHandled = await _handlerHelper.TryHandleAsync(leftEvent);

            Assert.True(isHandled);
        }

        [Fact]
        public async Task MemberLeft_EventSequenceNotExpectedYet_EventSetToWait()
        {
            var joinedEvent = new MemberJoinedFaker(sequence: 1).Generate();
            await _handlerHelper.HandleAsync(joinedEvent);

            var leftEvent = new MemberLeftFaker(joinedEvent)
                .RuleFor(e => e.Sequence, 3)
                .Generate();

            var isHandled = await _handlerHelper.TryHandleAsync(leftEvent);

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var subscriber = await unitOfWork.Subscriper.GetAllAsync();
            var permission = await unitOfWork.Permission.GetAllAsync();

            Assert.False(isHandled);
            Assert.Single(subscriber);
            Assert.Equal("Joined", subscriber.First().Status);
            Assert.Single(permission);
        }
    }
}
