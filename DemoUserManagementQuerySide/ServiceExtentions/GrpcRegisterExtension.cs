using Calzolari.Grpc.AspNetCore.Validation;
using DemoUserManagementQuerySide.Interceptors;
using DemoUserManagementQuerySide.Validators;

namespace DemoUserManagementQuerySide.ServiceExtentions
{
    public static class GrpcRegisterExtension
    {
        public static void AddGrpcWithValidators(this IServiceCollection services)
        {
            services.AddGrpc(options =>
            {
                options.EnableMessageValidation();
                options.Interceptors.Add<ApplicationExceptionInterceptor>();
            });

            AddValidators(services);
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddGrpcValidation();
            services.AddValidator<MemberInvitationPendingValidator>();
            services.AddValidator<MemberSubscriptionsValidator>();
            services.AddValidator<OwnerInvitationPendingValidator>();
            services.AddValidator<SubscriptionValidator>();
        }
    }
}
