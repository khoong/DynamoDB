using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDB.Settings
{
    public class DynamoDBSettings
    {
        public bool IsLocal { get; set; }

        public string LocalEndpointUrl { get; set; }

        public string LocalAccessKeyId { get; set; }

        public string LocalSecretAccessKey { get; set; }

        public bool LocalUseHttp { get; set; }

        public string TableName { get; set; }

    }
}
