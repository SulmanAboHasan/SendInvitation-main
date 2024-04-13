using Azure.Messaging.ServiceBus;

namespace DemoUserManagementQuerySide.Infrastructure.ServiceBus
{
    public class MemberServiceBus(IConfiguration configuration)
    {
        public ServiceBusClient Client { get; } = new ServiceBusClient(configuration.GetConnectionString("ServiceBus"));
    }
}
