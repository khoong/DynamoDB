using System;
using DynamoDB.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DynamoDB.Services.Interfaces;
using DynamoDB.Services;

namespace DynamoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    services
                        .Configure<DynamoDBSettings>(hostingContext.Configuration.GetSection("DynamoDBClientProviderSettings"))
                        .AddTransient<IDynamoDBService, DynamoDBService>()
                        .AddHostedService<StartupApp>();
                });
    }
}
