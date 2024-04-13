using DemoUsersManagementCommand.Test.Helper;
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

namespace DemoUsersManagementCommand.Test.Fakers.RemoveMember
{
    public class RemoveMemberTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory = factory.WithDefaultConfigurations(helper, services =>
        {
            services.ReplaceWithInMemoryDatabase();
        });

        [Theory]
        [InlineData("AccountId", "SubscriptionId", "MemberId", "UserId")]
        public async Task RemoveMember_SendValidRequest_MemberRemovedEventSaved(
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

            var removeRequest = new RemoveMemberRequest
            {
                Id = joinResponse.Id,
                AccountId = accountId,
                SubscriptionId = subscriptionId,
                MemberId = memberId,
                UserId = userId,
            };

            var removeResponse = await client.RemoveMemberAsync(removeRequest);

            using var scope = _factory.Services.CreateScope();
            var eventStore = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var events = await eventStore.GetAllAsync(removeResponse.Id, new CancellationToken());

            Assert.Equal(2, events.Count);
            Assert.Equal("MemberRemoved", events[1].Type);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData(" ", " ", " ", " ")]
        [InlineData("ValidAccountId", "ValidSubscriptionId", "ValidMemberId", "")]
        public async Task RemoveMember_SendInvalidRequest_ThrowsInvalidArgumentRpcException(
            string accountId,
            string subscriptionId,
            string memberId,
            string userId)
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new RemoveMemberRequest
            {
                Id = $"{subscriptionId}_{memberId}",
                AccountId = accountId,
                SubscriptionId = subscriptionId,
                MemberId = memberId,
                UserId = userId,
            };

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.RemoveMemberAsync(request));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveMember_SendRemoveRequestToNonJoinedMember_ThrowsNotFoundRpcException()
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new RemoveMemberRequest
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = Guid.NewGuid().ToString(),
                SubscriptionId = Guid.NewGuid().ToString(),
                MemberId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
            };

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.RemoveMemberAsync(request));

            Assert.Equal(StatusCode.NotFound, exception.StatusCode);
        }
    }
}
