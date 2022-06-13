using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using DynamoDB.Services.Interfaces;
using DynamoDB.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB
{
    public class StartupApp : IHostedService
    {
        private readonly IDynamoDBService _dynamoDBService;
        private readonly ILogger<StartupApp> _logger;

        public StartupApp(ILogger<StartupApp> logger, IDynamoDBService dynamoDBService)
        {
            _logger = logger;
            _dynamoDBService = dynamoDBService;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting application....");

                var dynamoDBClient = _dynamoDBService.GetDynamoDBClient();

                var newMovie = new Movie { Title = "Title 1", Year = 2022 };


                await PutItemAsync(dynamoDBClient, newMovie, "MusicCollection");



            }
            catch (Exception e)
            {
                string logMessage = $"{e.Message}    ***{e.StackTrace}*** ";
                _logger.LogError(logMessage);
            }
            finally
            {
                _logger.LogInformation("Stopping application....");

                await Task.Delay(10, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {            
            return Task.CompletedTask;
        }

        public static async Task<bool> PutItemAsync(AmazonDynamoDBClient client, Movie newMovie, string tableName)
        {
            var item = new Dictionary<string, AttributeValue>
            {
                ["title"] = new AttributeValue { S = newMovie.Title },
                ["year"] = new AttributeValue { N = newMovie.Year.ToString() },
            };

            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item,
            };

            var response = await client.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
