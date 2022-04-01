using Newtonsoft.Json;

namespace Turbulence.API.Models.DiscordGateway
{
    public class GatewayClientState
    {
        [JsonProperty("guild_hashes", Required = Required.Always)]
        public object GuildHashes { get; set; } = null!;

        [JsonProperty("highest_last_message_id", Required = Required.Always)]
        public string highestLastMessageID { get; set; }

        [JsonProperty("read_state_version", Required = Required.Always)]
        public int ReadStateVersion { get; set; }

        [JsonProperty("user_guild_settings_version", Required = Required.Always)]
        public int UserGuildSettingsVersion { get; set; }

        [JsonProperty("user_settings_version", Required = Required.Always)]
        public int UserSettingsVersion { get; set; }
    }
}
