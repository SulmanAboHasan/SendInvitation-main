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

namespace DemoUsersManagementCommand.Test.Fakers.JoinMember
{
    public class JoinMemberTest(WebApplicationFactory<Program> factory, ITestOutputHelper helper) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory = factory.WithDefaultConfigurations(helper, services =>
        {
            services.ReplaceWithInMemoryDatabase();
        });

        [Fact]
        public async Task JoinMember_SendValidRequest_MemberJoinedEventSaved()
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new JoinMemberRequest
            {
                AccountId = Guid.NewGuid().ToString(),
                SubscriptionId = Guid.NewGuid().ToString(),
                MemberId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Permissions = new Permissions
                {
                    Transfer = true,
                    PurchaseCards = false,
                    ManageDevices = false
                }
            };

            var response = await client.JoinMemberAsync(request);

            using var scope = _factory.Services.CreateScope();
            var eventsStore = scope.ServiceProvider.GetRequiredService<IEventStore>();
            var events = await eventsStore.GetAllAsync(response.Id, new CancellationToken());

            Assert.Single(events);
            Assert.Equal($"{request.SubscriptionId}_{request.MemberId}", response.Id);
            Assert.Equal(1, events[0].sequence);
        }

        [Fact]
        public async Task JoinMember_SendJoinRequestWhileMemberAlreadyJoined_ThrowsAlreadyExistsRpcException()
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new JoinMemberRequest
            {
                AccountId = Guid.NewGuid().ToString(),
                SubscriptionId = Guid.NewGuid().ToString(),
                MemberId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Permissions = new Permissions
                {
                    Transfer = true,
                    PurchaseCards = false,
                    ManageDevices = false
                }
            };

            await client.JoinMemberAsync(request);

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.JoinMemberAsync(request));

            Assert.Equal(StatusCode.AlreadyExists, exception.StatusCode);
        }

        [Theory]
        [InlineData("", "", "", "")]
        [InlineData(" ", " ", " ", " ")]
        [InlineData("ValidAccountId", "ValidSubscriptionId", "ValidMemberId", "")]
        public async Task JoinMember_SendInvalidRequestData_ThrowsInvalidArgumentRpcException(
            string accountId,
            string subscriptionId,
            string memberId,
            string userId)
        {
            var client = new InvitaionMembers.InvitaionMembersClient(_factory.CreateGrpcChannel());

            var request = new JoinMemberRequest
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

            var exception = await Assert.ThrowsAsync<RpcException>(async () => await client.JoinMemberAsync(request));

            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);

        }
    }
}
