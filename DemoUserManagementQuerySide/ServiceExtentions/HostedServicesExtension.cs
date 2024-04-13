using DemoUserManagementQuerySide.Infrastructure.ServiceBus;

namespace DemoUserManagementQuerySide.ServiceExtentions
{
    public static class HostedServicesExtension
    {
        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<MemberEventListener>();
        }
    }
}
