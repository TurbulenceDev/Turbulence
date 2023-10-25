using Turbulence.Discord.Models.DiscordGuild;

namespace Turbulence.Discord.Models;

// TODO: some parameters like author or mentions can be given multiple times
public record SearchRequest(Guild Server, 
    string Search, 
    Snowflake? Author = null, 
    Snowflake? Mentions = null, 
    string? Contains = null, 
    Snowflake? MaxId = null,
    Snowflake? MinId = null,
    Snowflake? Channel = null,
    bool? Pinned = null,
    string? SortBy = null,
    string? SortOrder = null,
    int Offset = 0);
