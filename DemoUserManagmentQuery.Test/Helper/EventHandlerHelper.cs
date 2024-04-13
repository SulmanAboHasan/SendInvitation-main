using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Helper
{
    public class EventHandlerHelper(IServiceProvider provider)
    {
        private readonly IServiceProvider _provider = provider;

        public async Task HandleAsync<TEvent>(TEvent @event) where TEvent : IRequest<bool>
        {
            using var scope = _provider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var isHandled = await mediator.Send(@event);

            if (!isHandled)
                throw new InvalidOperationException("Event not handled");
        }

        public Task<bool> TryHandleAsync<TEvent>(TEvent @event) where TEvent : IRequest<bool>
        {
            using var scope = _provider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            return mediator.Send(@event);
        }
    }
}
