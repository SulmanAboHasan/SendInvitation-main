using Calzolari.Grpc.AspNetCore.Validation;
using DemoUsersManagementCommandSide.Interceptors;
using DemoUsersManagementCommandSide.Validators;


namespace DemoUsersManagementCommandSide.Services
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
            services.AddValidator<SendRequestValidator>();
            services.AddValidator<CancelRequestValidator>();
            services.AddValidator<AcceptRequestValidator>();
            services.AddValidator<RejectRequestValidator>();
            services.AddValidator<DeleteRequestValidator>();
            services.AddValidator<JoinMemberRequestValidator>();
            services.AddValidator<RemoveMemberRequestValidator>();
            services.AddValidator<LeaveMemberRequestValidator>();
            services.AddValidator<ChangePermissionRequestValidator>();
        }
    }
}
