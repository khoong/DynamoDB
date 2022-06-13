using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DynamoDB.Services.Interfaces;
using DynamoDB.Settings;

namespace DynamoDB.Services
{
    public class DynamoDBService : IDynamoDBService
    {
        private readonly ILogger _logger;
        private readonly IOptions<DynamoDBSettings> _settings;

        public DynamoDBService(ILogger<DynamoDBService> logger, IOptions<DynamoDBSettings> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public AmazonDynamoDBClient GetDynamoDBClient()
        {
            _logger.LogInformation("Configuring Local Dynamo Client");

            _logger.LogInformation(string.Format("LocalEndpointUrl={0}   LocalAccessKeyId={1}   LocalSecretAccessKey={2}  TableName={3}", _settings.Value.LocalEndpointUrl, _settings.Value.LocalAccessKeyId, _settings.Value.LocalSecretAccessKey, _settings.Value.TableName));

            var credentials = new BasicAWSCredentials(_settings.Value.LocalAccessKeyId, _settings.Value.LocalSecretAccessKey);

            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1,
                ServiceURL = _settings.Value.LocalEndpointUrl,
                UseHttp = _settings.Value.LocalUseHttp,
            };

            return new AmazonDynamoDBClient(credentials, config);
        }
    }
}
