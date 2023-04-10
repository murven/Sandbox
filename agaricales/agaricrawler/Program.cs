using System;
using Microsoft.Extensions.Configuration;
using agaricrawler;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
GameAccountsConfiguration? gameAccountsConfiguration =
    config.GetRequiredSection(GameAccountsConfiguration.SectionName)
          .Get<GameAccountsConfiguration>();
if (gameAccountsConfiguration?.Accounts is null
    || !gameAccountsConfiguration.Accounts.Any())
{
    Console.WriteLine("No game accounts were found in the configuration.");
    Console.WriteLine("Make sure there is an appsettings.json file in the same folder as agaricrawler.");
    Console.WriteLine("This file should contain a section called GameAccountsConfiguration,");
    Console.WriteLine("which in turn should include a JSON array called Accounts.");
    Console.WriteLine("Each account should have a Name, an ApiKey and an OutputFolder to save the files to.");
    return 1;
}
else
{
    Console.WriteLine("The following accounts were found in the configuration:");
    var enumeratedAccounts = gameAccountsConfiguration
        .Accounts
        .Select((account, index) => $"{index + 1:000}-) {account.Name}");
    foreach (var account in enumeratedAccounts)
    {
        Console.WriteLine(account);
    }
}
GuildWars2ApiConfiguration? guildWars2ApiConfiguration =
    config.GetRequiredSection(GuildWars2ApiConfiguration.SectionName)
          .Get<GuildWars2ApiConfiguration>();
if (!(guildWars2ApiConfiguration?.IsValid() ?? false))
{
    Console.WriteLine("No Guild Wars 2 API found in the configuration.");
    Console.WriteLine("Make sure there is an appsettings.json file in the same folder as agaricrawler.");
    Console.WriteLine("This file should contain a section called GuildWars2ApiConfiguration,");
    Console.WriteLine("the section should have a Host, Schema, Version, and a JSON Array with all the AccountEndpoints.");
    return 2;
}
Console.WriteLine("The Guild Wars 2 API Base Uri is:");
Console.WriteLine(guildWars2ApiConfiguration.GuildWars2ApiBaseUri);
Console.WriteLine("The following endpoints were found in the configuration:");
var enumeratedEndpoints = guildWars2ApiConfiguration
    .GetAccountEndpointsArray()
    .Select((accountEndpoint, index) => $"{index + 1:000}-) {accountEndpoint}");
foreach (var endpoint in enumeratedEndpoints)
{
    Console.WriteLine(endpoint);
}
var fileDateTimeFolder = DateTime.Now.ToUniversalTime().ToString("yyyyMMddTHHmmssffffZ");
Console.WriteLine("The snapshot will be saved to the following folder in the configured output folder for each account:");
Console.WriteLine(fileDateTimeFolder);
GameAccountSettings[] sanitizedAccounts =
    gameAccountsConfiguration?.Accounts
    ?? Array.Empty<GameAccountSettings>();
HttpClient guildWars2ApiClient = new()
{
    BaseAddress = new Uri(guildWars2ApiConfiguration.GuildWars2ApiBaseUri),
};
foreach (GameAccountSettings account in sanitizedAccounts)
{
    var accountOutputFolder = account.GetOutputFolderName(fileDateTimeFolder);
    Directory.CreateDirectory(accountOutputFolder);
    Console.WriteLine("=================================================");
    Console.WriteLine("Downloading data for the following account:");
    Console.WriteLine(account.Name);
    Console.WriteLine("=================================================");
    Console.WriteLine("Downloading data to the following folder:");
    Console.WriteLine(accountOutputFolder);
    guildWars2ApiClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", account.ApiKey);
    foreach (string endpoint in guildWars2ApiConfiguration.AccountEndpoints ?? Array.Empty<string>())
    {
        Console.WriteLine("Downloading data from the following endpoint:");
        Console.WriteLine(endpoint);
        using var responseMessage = await guildWars2ApiClient.GetAsync($"{guildWars2ApiConfiguration.Version}/{endpoint}");
        if (responseMessage.IsSuccessStatusCode)
        {
            var outputFileName = endpoint.Replace("/", "-");
            var outputFile = $"{accountOutputFolder}\\{outputFileName}.json";
            Console.WriteLine("Saving data to the following file:");
            Console.WriteLine(outputFile);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            await System.IO.File.WriteAllTextAsync(outputFile, responseString);
            Console.WriteLine("Data saved successfully.");
        }
        else
        {
            Console.WriteLine("Error downloading data:");
            Console.WriteLine(responseMessage.StatusCode);
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
    Console.WriteLine("=================================================");
    Console.WriteLine("All data downloaded for the following account:");
    Console.WriteLine(account.Name);
    Console.WriteLine("=================================================");
}
return 0;