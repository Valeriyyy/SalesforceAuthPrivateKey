using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Salesforce OAuth token endpoint
// Sandboxes use https://test.salesforce.com
// Production and Dev orgs use https://login.salesforce.com
string tokenUrl = "https://login.salesforce.com";

// Salesforce Connected App credentials
string privateKeyContent = await File.ReadAllTextAsync(config.GetValue<string>("PrivateKeypath"));
string privateKey = privateKeyContent.Replace("-----BEGIN RSA PRIVATE KEY-----", string.Empty).Replace("-----END RSA PRIVATE KEY-----", string.Empty);
byte[] privateKeyBytes = Convert.FromBase64String(privateKey);

// Create RSA parameters from the byte array
var rsa = RSA.Create();
rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

// Create signing credentials, this step sets the encoding algorithm for the token. It must be RS256
var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

// Create JWT token
var tokenHandler = new JwtSecurityTokenHandler();
var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(new[]
    {
            new Claim("iss", config.GetValue<string>("ClientId")),
            new Claim("sub", config.GetValue<string>("Subject")),
            new Claim("aud", tokenUrl),
            new Claim("exp", DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds().ToString())
        }),
    SigningCredentials = signingCredentials
};

var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
var jwt = tokenHandler.WriteToken(token);

var sfAuthClient = new HttpClient
{
    BaseAddress = new Uri(tokenUrl)
};
var content = new FormUrlEncodedContent(new Dictionary<string, string>
{
    {"grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"},
    {"assertion", jwt}
});
var response = await sfAuthClient.PostAsync("/services/oauth2/token", content);
var responseContent = response.Content.ReadAsStringAsync().Result;
var authToken = JsonSerializer.Deserialize<SFAuthToken>(responseContent);


Console.WriteLine("AccessToken:" + authToken.AccessToken);
Console.WriteLine("Scope:" + authToken.Scope);
Console.WriteLine("InstanceUrl:" + authToken.InstanceUrl);
Console.WriteLine("Id:" + authToken.Id);
Console.WriteLine("TokenType:" + authToken.TokenType);