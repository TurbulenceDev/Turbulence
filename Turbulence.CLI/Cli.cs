using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

var provider = new ServiceCollection()
    .AddSingleton<IPlatformClient, Client>()
    .BuildServiceProvider();
Ioc.Default.ConfigureServices(provider);

try
{
    // This uses dotnet user-secrets, saved in a secrets.json; can be configured through VS or CLI
    var config = new ConfigurationManager().AddUserSecrets<Client>().Build();

    if (config["token"] is not { } token)
    {
        Console.WriteLine("No token set. Use 'dotnet user-secrets set token [your token]' to set a token.");
        return;
    }

    var discord = Ioc.Default.GetService<IPlatformClient>()!;
    await discord.Start();
    discord.Ready += async (_, msg) =>
    {
        Console.WriteLine("Ready");
        if (msg.Data.Guilds.Length == 0)
        {
            Console.WriteLine("No Guilds.");
            return;
        }
        var guild = msg.Data.Guilds[0];
        var channels = (await discord.GetGuild(guild.Id)).Channels;
        var channel = channels.First(c => c.Type == ChannelType.GUILD_TEXT);
        Console.WriteLine($"Guild: {guild.Name} ({guild.Id}), Channel: {channel.Name} ({channel.Id})");
        var msgs = await discord.GetMessages(channel.Id);
        foreach (var m in msgs)
        {
            Console.WriteLine($"{m.Author.Username}: {m.Content}");
        }
    };

    // TODO: run thread
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
