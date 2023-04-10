namespace agaricrawler;

internal class GameAccountSettings
{
    public string? Name { get; set; }
    public string? ApiKey { get; set; }
    public string? OutputFolder { get; set; }
    public string GetOutputFolderName(string fileDateTimeFolder) =>
        $"{OutputFolder}\\snapshot\\{Name}\\{fileDateTimeFolder}";
}

internal class GameAccountsConfiguration
{
    public static string SectionName = nameof(GameAccountsConfiguration);
    public GameAccountSettings[]? Accounts { get; set; }
}