
using Azure.Messaging.ServiceBus;
using DemoUserManagementQuerySide.EventsHandler.Accepted;
using DemoUserManagementQuerySide.EventsHandler.Cancelled;
using DemoUserManagementQuerySide.EventsHandler.Rejected;
using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Left;
using DemoUserManagementQuerySide.EventsHandler.Removed;
using DemoUserManagementQuerySide.EventsHandler.Changed;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;
using System.Text;
using System.Text.Json;

namespace DemoUserManagementQuerySide.Infrastructure.ServiceBus
{
    public class MemberEventListener : IHostedService
    {
        private readonly ServiceBusSessionProcessor _sessionProcessor;
        private readonly ServiceBusProcessor _deadLetterProcessor;
        private readonly ILogger<MemberEventListener> _logger;
        private readonly IServiceProvider _serviceProvider;

        public MemberEventListener(
             MemberServiceBus memberServiceBus,
             IConfiguration configuration,
             ILogger<MemberEventListener> logger,
             IServiceProvider serviceProvider
            )
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            var options = configuration.GetSection(ServiceBusOptions.ServiceBus).Get<ServiceBusOptions>();
            _sessionProcessor = memberServiceBus.Client.CreateSessionProcessor(
                topicName: options!.TopicName,
                subscriptionName: options.SubscriptionName,
                options: new ServiceBusSessionProcessorOptions()
                {
                    AutoCompleteMessages = false,
                    PrefetchCount = 1,
                    MaxConcurrentSessions = 100,
                    MaxConcurrentCallsPerSession = 1
                });

            _deadLetterProcessor = memberServiceBus.Client.CreateProcessor(
                topicName: options.TopicName,
                subscriptionName: options.SubscriptionName,
                options: new ServiceBusProcessorOptions()
                {
                    AutoCompleteMessages = false,
                    PrefetchCount = 10,
                    MaxConcurrentCalls = 10,
                    SubQueue = SubQueue.DeadLetter,
                });

            _sessionProcessor.ProcessMessageAsync += Processor_ProcessorMessageAsync;
            _sessionProcessor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            _deadLetterProcessor.ProcessMessageAsync += DeadLetterProcessor_ProcessMessageAsync;
            _deadLetterProcessor.ProcessErrorAsync += DeadLetterProcessor_ProcessErrorAsync;
           
        }

        private async Task Processor_ProcessorMessageAsync(ProcessSessionMessageEventArgs args)
        {
            var isHandled = await TryHandleAsync(args.Message);

            if(isHandled)
            {
                await args.CompleteMessageAsync(args.Message);
            }
            else
            {
                _logger.LogWarning("Message {MessageId} not handled", args.Message.MessageId);
                await Task.Delay(5000);
                await args.AbandonMessageAsync(args.Message);
            }
        }

        private async Task<bool> TryHandleAsync(ServiceBusReceivedMessage message)
        {
            _logger.LogInformation(
                "Event {Event} Arrived, SessionId {SessionId}.",
                message.Subject,
                message.SessionId
            );

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var json = Encoding.UTF8.GetString(message.Body);

            return message.Subject switch
            {
                nameof(InvitationSent) => await mediator.Send(Deserialize<InvitationSent>(json)),
                nameof(InvitationCancelled) => await mediator.Send(Deserialize<InvitationCancelled>(json)),
                nameof(InvitationAccepted) => await mediator.Send(Deserialize<InvitationAccepted>(json)),
                nameof(InvitationRejected) => await mediator.Send(Deserialize<InvitationRejected>(json)),
                nameof(MemberJoined) => await mediator.Send(Deserialize<MemberJoined>(json)),
                nameof(MemberRemoved) => await mediator.Send(Deserialize<MemberRemoved>(json)),
                nameof(MemberLeft) => await mediator.Send(Deserialize<MemberLeft>(json)),
                nameof(PermissionChanged) => await mediator.Send(Deserialize<PermissionChanged>(json)),
                _ => await mediator.Send(Deserialize<PermissionChanged>(json)),

            };

        }
        private static T Deserialize<T>(string json)
        => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        ?? throw new InvalidOperationException("Faile to deserialize message");

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogCritical(args.Exception, "Message handler encountered an exception," +
               " Error Source:{ErrorSource}," +
               " Entity Path:{EntityPath}",
               args.ErrorSource.ToString(),
               args.EntityPath);

            return Task.CompletedTask;
        }


        private Task DeadLetterProcessor_ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogCritical(args.Exception, "Message handler encountered an exception," +
              " Error Source:{ErrorSource}," +
              " Entity Path:{EntityPath}",
              args.ErrorSource.ToString(),
              args.EntityPath);

            return Task.CompletedTask;
        }

        private async Task DeadLetterProcessor_ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var isHandled = await TryHandleAsync(args.Message);

            if (isHandled)
            {
                await args.CompleteMessageAsync(args.Message);
            }
            else
            {
                _logger.LogWarning("Message {MessageId} not handled", args.Message.MessageId);
                await Task.Delay(5000);
                await args.AbandonMessageAsync(args.Message);
            }
        }

        

       

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(
                _sessionProcessor.StartProcessingAsync(cancellationToken),
                 _deadLetterProcessor.StartProcessingAsync(cancellationToken)
                );
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(
                _sessionProcessor.CloseAsync(cancellationToken),
                _deadLetterProcessor.CloseAsync(cancellationToken)
                );
        }
    }
}
