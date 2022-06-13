using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;

namespace DynamoDB.Services.Interfaces
{
    public interface IDynamoDBService
    {
        AmazonDynamoDBClient GetDynamoDBClient();
    }
}
