﻿using DemoUsersManagementCommand.Test.Helper;
using DemoUsersManagementCommandSide.Abstraction;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DemoUsersManagementCommand.Test.Fakers.LeaveMember
{
    public class LeaveTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory = factory.WithDefaultConfigurations(helper, services =>
        {
            services.ReplaceWithInMemoryDatabase();
        });

        [Theory]
        [InlineData("AccountId", "SubscriptionId", "MemberId", "UserId")]
        public async Task Leave_SendValidRequestWhenMemberIsJoined_MemberLeftEventSaved(
            string accountId,
            string subscriptionId,
            string memberId,
            string userId)
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var joinRequest = new JoinMemberRequest
            {
                AccountId = accountId,
                SubscriptionId = subscriptionId,
                MemberId = memberId,
                UserId = userId,
                Permissions = new Permissions
                {
                    Transfer = true,
                    PurchaseCards = false,
                    ManageDevices = false
                }
            };

            var joinResponse = await client.JoinMemberAsync(joinRequest);

            var leaveRequest = new LeaveMemberRequest
            {
                Id = joinResponse.Id,
                AccountId = accountId,
                SubscriptionId = subscriptionId,
                MemberId = memberId,
                UserId = userId,
            };

            var leaveResponse = await client.LeaveMemberAsync(leaveRequest);

            using var scope = _factory.Services.CreateScope();
            var eventStore = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var events = await eventStore.GetAllAsync(leaveResponse.Id, new CancellationToken());

            Assert.Equal(2, events.Count);
            Assert.Equal("MemberLeft", events[1].Type);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData(" ", " ", " ", " ")]
        [InlineData("ValidAccountId", "ValidSubscriptionId", "ValidMemberId", "")]
        public async Task Leave_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
           string accountId,
           string subscriptionId,
           string memberId,
           string userId)
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new LeaveMemberRequest
            {
                Id = $"{subscriptionId}_{memberId}",
                AccountId = accountId,
                SubscriptionId = subscriptionId,
                MemberId = memberId,
                UserId = userId,
            };

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.LeaveMemberAsync(request));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
        }

        [Fact]
        public async Task Leave_SendLeaveRequestToNonJoinedMember_ThrowsNotFoundRpcException()
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new LeaveMemberRequest
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                SubscriptionId = Guid.NewGuid().ToString(),
                MemberId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
            };

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.LeaveMemberAsync(request));

            Assert.Equal(StatusCode.NotFound, exception.StatusCode);
        }
    }
}
