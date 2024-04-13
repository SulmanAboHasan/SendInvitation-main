using DemoUsersManagementCommandSide.Services;
using DemoUsersManagementCommandSide.Infrastructuer.Persistence;
using DemoUsersManagementCommandSide.Infrastructuer.Persistence.DbInitializer;
using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Infrastructuer.MessageBus;
using Serilog;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;


Log.Logger = LoggerServiceBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpcWithValidators();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddSingleton(new ServiceBusClient(
    builder.Configuration.GetConnectionString("ServiceBus")));
builder.Services.AddSingleton<ServiceBusPublisher>();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvitationMemberService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

public partial class Program { }