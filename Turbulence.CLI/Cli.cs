﻿using Microsoft.Extensions.Configuration;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Turbulence.API.Discord;
using Turbulence.API.Discord.Models.DiscordChannel;
using Turbulence.API.Discord.Models.DiscordGateway;
using Turbulence.API.Discord.Models.DiscordGatewayEvents;
using Turbulence.API.Discord.Models.DiscordGuild;
using Turbulence.API.Discord.Models.DiscordUser;
using static Turbulence.API.Discord.Models.DiscordGateway.GatewayOpcode;

namespace Turbulence.CLI;

public class Cli
{
    public static string Token = string.Empty;

    private static async Task Main()
    {
        try
        {
            // This uses dotnet user-secrets, saved in a secrets.json; can be configured through VS or CLI
            var config = new ConfigurationManager().AddUserSecrets<Cli>().Build();

            if (config["token"] is not { } token)
            {
                Console.WriteLine("No token set. Use 'dotnet user-secrets set token [your token]' to set a token.");
                return;
            }
            Token = token;

            var discord = new Discord();
            await discord.Start();
            Discord.OnReadyEvent += async (sender, msg) =>
            {
                Console.WriteLine("Ready");
                if (msg.Data.Guilds.Length == 0)
                {
                    Console.WriteLine("No Guilds.");
                    return;
                }
                var guild = msg.Data.Guilds[0];
                var channels = await discord.GetGuildChannels(guild.Id);
                var channel = channels.First(c => c.Type == ChannelType.GUILD_TEXT);
                Console.WriteLine($"Guild: {guild.Name} ({guild.Id}), Channel: {channel.Name} ({channel.Id})");
                var msgs = await discord.GetMessages(channel.Id);
                foreach (var m in msgs)
                {
                    Console.WriteLine($"{m.Author.Username}: {m.Content}");
                }
            };
            

            //TODO: run thread
            while (true)
            {
                await Task.Delay(1000);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in Main(): {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine("Finished?");
    }
}