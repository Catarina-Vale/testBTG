using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CatarinaBtg.Repositories;
using CatarinaBtg.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DynamoDB client configuration
builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var region = configuration["AWS:Region"];
    var accessKeyId = configuration["AWS:AccessKeyId"];
    var secretAccessKey = configuration["AWS:SecretAccessKey"];

    var credentials = new Amazon.Runtime.BasicAWSCredentials(accessKeyId, secretAccessKey);
    var config = new AmazonDynamoDBConfig { RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region) };

    return new AmazonDynamoDBClient(credentials, config);
});

builder.Services.AddScoped<ITransactionLimitRepository, DynamoDBTransactionLimitRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Add routing before authorization
app.UseRouting();

app.UseAuthorization();

// Add Controllers before MapControllers
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();