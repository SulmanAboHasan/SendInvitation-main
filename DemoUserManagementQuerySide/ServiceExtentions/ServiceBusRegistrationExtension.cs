using DemoUserManagementQuerySide.Infrastructure.ServiceBus;

namespace DemoUserManagementQuerySide.ServiceExtentions
{
    public static class ServiceBusRegistrationExtension
    {
        public static void AddServiceBus(this IServiceCollection services)
        {
            services.AddSingleton<MemberServiceBus>();
        }
    }
}
