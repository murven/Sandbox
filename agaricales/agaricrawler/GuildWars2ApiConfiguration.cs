namespace agaricrawler;

internal class GuildWars2ApiConfiguration
{
    public static string SectionName = nameof(GuildWars2ApiConfiguration);
    public string? Host { get; set; }
    public string? Schema { get; set; }
    public string? Version { get; set; }
    public bool IsValid() =>
        Host is not null
        && Schema is not null
        && Version is not null;
}
