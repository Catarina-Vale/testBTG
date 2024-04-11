using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using CatarinaBtg.Interfaces;
using CatarinaBtg.Models;

namespace CatarinaBtg.Repositories{

public class DynamoDBTransactionLimitRepository : ITransactionLimitRepository
{
    private readonly AmazonDynamoDBClient _dynamoDBClient;

    public DynamoDBTransactionLimitRepository(IConfiguration configuration)
    {
        var region = configuration["AWS:Region"];
        var accessKeyId = configuration["AWS:AccessKeyId"];
        var secretAccessKey = configuration["AWS:SecretAccessKey"];
        var credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey);
            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region)
            };

            _dynamoDBClient = new AmazonDynamoDBClient(credentials, config);
    }

    public async Task<TransactionLimit> GetTransactionLimitAsync(string cpf)
    {
        var table = Table.LoadTable(_dynamoDBClient, "catarina-vale-table");

        var document = await table.GetItemAsync(cpf);
        if (document == null)
            return null;

        return new TransactionLimit
        {
            CPF = document["CPF"],
            AgencyNumber = document["AgencyNumber"],
            AccountNumber = document["AccountNumber"],
            PIXLimit = Convert.ToDecimal(document["PIXLimit"])
        };
    }

    public async Task AddOrUpdateTransactionLimitAsync(TransactionLimit transactionLimit)
    {
        var table = Table.LoadTable(_dynamoDBClient, "catarina-vale-table");

        var document = new Document();
        document["CPF"] = transactionLimit.CPF;
        document["AgencyNumber"] = transactionLimit.AgencyNumber;
        document["AccountNumber"] = transactionLimit.AccountNumber;
        document["PIXLimit"] = transactionLimit.PIXLimit;

        await table.PutItemAsync(document);
    }

    public async Task RemoveTransactionLimitAsync(TransactionLimit transactionLimit)
    {
        var table = Table.LoadTable(_dynamoDBClient, "catarina-vale-table");

        var document = new Document();
        document["CPF"] = transactionLimit.CPF;
        document["AgencyNumber"] = transactionLimit.AgencyNumber;
        document["AccountNumber"] = transactionLimit.AccountNumber;
        document["PIXLimit"] = transactionLimit.PIXLimit;

        await table.DeleteItemAsync(document);
    }
}

}