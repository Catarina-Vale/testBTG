using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using CatarinaBtg.Interfaces;
using CatarinaBtg.Models;
using CatarinaBtg.Repositories;
using Amazon.DynamoDBv2;

namespace CatarinaBtg.Tests.Repositories
{
    [TestFixture]
    public class DynamoDBTransactionLimitRepositoryTests
    {
        private DynamoDBTransactionLimitRepository _repository;
        private Mock<AmazonDynamoDBClient> _mockDynamoDBClient;
        private IConfiguration _configs;

        [SetUp]
        public void Setup()
        {
            _mockDynamoDBClient = new Mock<AmazonDynamoDBClient>();

            _repository = new DynamoDBTransactionLimitRepository(_configs);
        }

        [Test]
        public async Task GetTransactionLimitAsync_WhenDocumentExists_ReturnsTransactionLimit()
        {
            string cpf = "12345678900";
            _mockDynamoDBClient.Setup(x => x.GetItemAsync(It.IsAny<GetItemRequest>(), default))
                .ReturnsAsync((GetItemRequest request, System.Threading.CancellationToken token) =>
                {
                    if (request.Key.ContainsKey("CPF") && request.Key["CPF"].S == cpf)
                    {
                        return new GetItemResponse
                        {
                            Item = new Dictionary<string, AttributeValue>
                            {
                                ["CPF"] = new AttributeValue { S = cpf },
                                ["AgencyNumber"] = new AttributeValue { S = "123" },
                                ["AccountNumber"] = new AttributeValue { S = "123456" },
                                ["PIXLimit"] = new AttributeValue { N = "1000" }
                            }
                        };
                    }
                    else
                    {
                        return new GetItemResponse();
                    }
                });

            var result = await _repository.GetTransactionLimitAsync(cpf);

            Assert.That(result, Is.Not.Null);
            Assert.That(cpf, Is.EqualTo(result.CPF));
            Assert.That("123", Is.EqualTo(result.AgencyNumber));
            Assert.That("123456", Is.EqualTo(result.AccountNumber));
            Assert.That(1000, Is.EqualTo(result.PIXLimit));
        }

        [Test]
        public async Task GetTransactionLimitAsync_WhenDocumentDoesNotExist_ReturnsNull()
        {
            string cpf = "12345678900";
            _mockDynamoDBClient.Setup(x => x.GetItemAsync(It.IsAny<GetItemRequest>(), default))
                .ReturnsAsync(new GetItemResponse());

            var result = await _repository.GetTransactionLimitAsync(cpf);

            Assert.That(result, Is.Null);
        }
    }
}
