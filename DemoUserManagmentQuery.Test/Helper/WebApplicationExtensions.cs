﻿using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DemoUserManagmentQuery.Test.Helper
{
    public static class WebApplicationExtensions
    {
        public static WebApplicationFactory<Program> WithDefaultConfigurations(this WebApplicationFactory<Program> factory, ITestOutputHelper helper, Action<IServiceCollection>? servicesConfiguration = null) => factory
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureLogging(loggingBuilder =>
                   {
                       Log.Logger = new LoggerConfiguration()
                           .MinimumLevel.Information()
                           .WriteTo.TestOutput(helper, LogEventLevel.Information)
                           .CreateLogger();
                   });

                   if (servicesConfiguration != null)
                       builder.ConfigureTestServices(servicesConfiguration);
               });

        public static GrpcChannel CreateGrpcChannel(this WebApplicationFactory<Program> factory)
        {
            var client = factory.CreateDefaultClient();
            return GrpcChannel.ForAddress(
                client.BaseAddress ?? throw new InvalidOperationException("BaseAddress is null"),
                new GrpcChannelOptions
                {
                    HttpClient = client
                });
        }

    }
}
