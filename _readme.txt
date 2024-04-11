Transaction Limit API
Introduction
The Transaction Limit API is a RESTful service built to manage transaction limits for bank accounts. It provides endpoints to retrieve, add/update, and delete transaction limits associated with customer accounts.

Prerequisites
Before running the project, ensure you have the following prerequisites installed:

.NET Core SDK
Visual Studio or any preferred code editor
Additionally, contact the project owner to obtain the necessary AWS IAM credentials and keys required for the AWS DynamoDB integration.

Getting Started
Clone the repository to your local machine:
bash
Copy code
git clone <repository-url>
Navigate to the project directory:
bash
Copy code
cd TransactionLimitAPI
Open the project in your preferred code editor.

Update the AWS IAM credentials and keys in the appsettings.json file:

json
Copy code
"AWS": {
  "Region": "your-aws-region",
  "AccessKeyId": "your-access-key-id",
  "SecretAccessKey": "your-secret-access-key"
}
Replace "your-aws-region", "your-access-key-id", and "your-secret-access-key" with your AWS IAM credentials and keys obtained from the project owner.

Build and run the project:
Copy code
dotnet build
dotnet run
Once the project is running, you can access the API endpoints:
GET /api/TransactionLimit/{cpf}: Retrieves the transaction limit for the specified CPF.
POST /api/TransactionLimit: Adds or updates the transaction limit for a customer account.
DELETE /api/TransactionLimit: Removes the transaction limit for a customer account.
Usage
To use the API endpoints, send HTTP requests to the appropriate endpoint URLs using your preferred API testing tool (e.g., Postman).