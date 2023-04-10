using System;
using System.Collections.Generic;
using System.Linq;

namespace agaricrawler;

internal class GuildWars2ApiConfiguration
{
    public static string SectionName = nameof(GuildWars2ApiConfiguration);
    public string? Host { get; set; }
    public string? Schema { get; set; }
    public string? Version { get; set; }
    public string[]? AccountEndpoints { get; set; }
    public bool IsValid() =>
        Host is not null
        && Schema is not null
        && Version is not null
        && AccountEndpoints is not null
        && AccountEndpoints.Any();
    public string GuildWars2ApiBaseUri => $"{Schema}://{Host}/{Version}";
    public IEnumerable<string> GetAccountEndpointsArray() =>
        AccountEndpoints?
        .Select(endpoint => $"{GuildWars2ApiBaseUri}/{endpoint}")
        ?? Array.Empty<string>();
}
