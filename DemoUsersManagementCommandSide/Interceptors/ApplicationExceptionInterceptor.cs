using DemoUsersManagementCommandSide.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;
using System;

namespace DemoUsersManagementCommandSide.Interceptors
{
    public class ApplicationExceptionInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (NotFoundException e)
            {
                throw new RpcException(new Status(StatusCode.NotFound, e.Message));
            }
            catch (AlreadyExistsException e)
            {
                throw new RpcException(new Status(StatusCode.AlreadyExists, e.Message));
            }
            catch (RuleViolationException e)
            {
                throw new RpcException(new Status(StatusCode.FailedPrecondition, e.Message));
            }
        }
    }
}
    
