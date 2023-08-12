# SalesforceAuthPrivateKey

# Descripton

### How to get a salesforce auth token using a connected app private key

The purpose of this project is to show how you can retrieve a working salesforce authentication token for server-to-server integration as outlined in the official documenation [OAuth 2.0 JWT Bearer Flow for Server-to-Server Integration
](https://help.salesforce.com/s/articleView?id=sf.remoteaccess_oauth_jwt_flow.htm&type=5). The documentation is fairly simple but the code example is in java. This project should help people using C# to integrate with SF to server-to-server integration.

This code uses the server.key file that is generated from when you generate a Self-Signed Certificate for use with a Salesforce Connected App. Since the server.key file is related to the certificate that you uploaded to Salesforce, you are able to use it to generate a JWT token and have Salesforce provide you with its own valid api token.

# Setup

Before you get started this project assumes that you have already created a connected app and added
a self-signed certificate.
If not, you can follow the directions here [Generage Self-Signed Certificate](https://developer.salesforce.com/docs/atlas.en-us.sfdx_dev.meta/sfdx_dev/sfdx_dev_auth_key_and_cert.htm) to get yourself started.

Once you have a certificate and have created a connected app, you can start the setup for running this project.

First retrieve the project with
`git clone https://github.com/Valeriyyy/SalesforceAuthPrivateKey.git`

You will need an `appsettings.json` file to hold sensitive information so it is not hard coded into the code itself.

It will need the following properties and look like this:

```
{
  "ClientId": "SF Connected App Consumer Key/ClientId",
  "Subject": "Your Salesforce Username",
  "PrivateKeypath": "Path to the private key"
}
```

# Execution

After you have created the necessary appsettings file, you can build and run the project with

```
dotnet restore
dotnet build
dotnet run
```

Once the code has ran, you should have a valid SFAuthToken object with an AccessToken that can be used with the SF rest api. 