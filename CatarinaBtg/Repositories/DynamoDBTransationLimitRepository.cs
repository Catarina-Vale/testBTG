using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using CatarinaBtg.Interfaces;
using CatarinaBtg.Models;

namespace CatarinaBtg.Repositories{

public class DynamoDBTransactionLimitRepository : ITransactionLimitRepository
{
    private readonly AmazonDynamoDBClient _dynamoDBClient;

    public DynamoDBTransactionLimitRepository(AmazonDynamoDBClient dynamoDBClient)
    {
        _dynamoDBClient = dynamoDBClient;
    }

    public async Task<TransactionLimit> GetTransactionLimitAsync(string cpf)
    {
        var table = Table.LoadTable(_dynamoDBClient, "TransactionLimits");

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
        var table = Table.LoadTable(_dynamoDBClient, "TransactionLimits");

        var document = new Document();
        document["CPF"] = transactionLimit.CPF;
        document["AgencyNumber"] = transactionLimit.AgencyNumber;
        document["AccountNumber"] = transactionLimit.AccountNumber;
        document["PIXLimit"] = transactionLimit.PIXLimit;

        await table.PutItemAsync(document);
    }

    public async Task RemoveTransactionLimitAsync(TransactionLimit transactionLimit)
    {
        var table = Table.LoadTable(_dynamoDBClient, "TransactionLimits");

        var document = new Document();
        document["CPF"] = transactionLimit.CPF;
        document["AgencyNumber"] = transactionLimit.AgencyNumber;
        document["AccountNumber"] = transactionLimit.AccountNumber;
        document["PIXLimit"] = transactionLimit.PIXLimit;

        await table.DeleteItemAsync(document);
    }
}

}