using System;
using Microsoft.Extensions.Configuration;
using agaricrawler;
using System.Linq;
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
}
else
{
    Console.WriteLine("The following accounts have been found in the configuration:");
    var counter = 1;
    foreach (var account in gameAccountsConfiguration.Accounts)
    {
        Console.WriteLine($"{counter:000}-) {account.Name}");
    }
}