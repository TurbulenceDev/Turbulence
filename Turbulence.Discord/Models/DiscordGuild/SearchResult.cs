using System.Text.Json.Serialization;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordUser;

namespace Turbulence.Discord.Models.DiscordGuild;

public record SearchResult
{
    [JsonPropertyName("analytics_id")]
    public required string AnalyticsId { get; init; }

    [JsonPropertyName("doing_deep_historical_index")]
    public required bool DeepHistoricalIndex { get; init; }

    [JsonPropertyName("total_results")]
    public required int TotalResults { get; init; }

    [JsonPropertyName("threads")]
    public Channel[]? Threads { get; init; }

    [JsonPropertyName("members")]
    public User[]? Members { get; init; }

    [JsonPropertyName("messages")]
    public required Message[] Messages { get; init; }
}
