using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide;
using DemoUserManagmentQuery.Test.Fakers.EntitiesFakers;
using DemoUserManagmentQuery.Test.Helper;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DemoUserManagmentQuery.Test.QueryTests
{
    public class OwnerInvitationPendingTests(WebApplicationFactory<Program> factory, ITestOutputHelper helper) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory = factory.WithDefaultConfigurations(helper, services =>
        {
            services.ReplaceWithInMemoryDatabase();
        });

        [Fact]
        public async Task OwnerPendingInvitations_QueryExistingEntities_ReturnsOwnerPendingInvitations()
        {
            string userId = Guid.NewGuid().ToString();
            string subscriptionId = Guid.NewGuid().ToString();

            using var scope = _factory.Services.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await unitOfWork.Invitation.AddRangeAsync(
                new InvitationFaker().SameSupscriptionDiffrentUser(userId, subscriptionId, count: 3)
                , new CancellationToken());
            await unitOfWork.CommitAsync(new CancellationToken());

            var x = await unitOfWork.Invitation.GetAllAsync(i => i.Subscriptions.UserId == userId,
                includeProperties: "Subscription,User",
                1, 20);

            var client = new Members.MembersClient(_factory.CreateGrpcChannel());

            var request = new GetOwnerInvitationPendingRequest()
            {
                OwnerId = userId,
                Page = 1,
                Size = 20
            };

            var response = await client.GetOwnerInvitationPendingAsync(request);

            Assert.Equal(1, response.Page);
            Assert.Equal(20, response.PageSize);
            Assert.Equal(3, response.TotalResults);
            Assert.Equal(3, response.Invitations.Count);
        }

        [Fact]
        public async Task OwnerPendingInvitations_QueryUserWithNoInvitation_ThrowNotFoundRpcException()
        {
            var client = new Members.MembersClient(_factory.CreateGrpcChannel());

            var request = new GetOwnerInvitationPendingRequest()
            {
                OwnerId = Guid.NewGuid().ToString(),
            };

            var exception = await Assert.ThrowsAsync<RpcException>(() => client.GetOwnerInvitationPendingAsync(request).ResponseAsync);

            Assert.Equal(StatusCode.NotFound, exception.StatusCode);
            Assert.NotEmpty(exception.Status.Detail);
        }
    }
}
