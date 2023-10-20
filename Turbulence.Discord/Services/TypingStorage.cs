using CommunityToolkit.Mvvm.DependencyInjection;
using Turbulence.Discord.Models;
using Turbulence.Discord.Models.DiscordChannel;
using Turbulence.Discord.Models.DiscordGatewayEvents;

namespace Turbulence.Discord.Services;

public interface ITypingStorage
{
    public IEnumerable<Snowflake>? GetTypingUsers(Snowflake channel);
    public event EventHandler<Event<Snowflake>>? TypingStatusChanged;
}

public class TypingStorage : ITypingStorage
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;
    public Dictionary<Snowflake, Dictionary<Snowflake, CancellationTokenSource>> TypingUsers = new();
    // The timeout between a typing update and it being removed
    public const int TypingTimeout = 8;
    // Called if a channel has a change in typing users
    public event EventHandler<Event<Snowflake>>? TypingStatusChanged;

    public TypingStorage()
    {
        // listen to Typing Status and MessageCreated event
        _client.MessageCreated += _client_MessageCreated;
        _client.TypingStart += _client_TypingStart;
    }

    private void _client_TypingStart(object? sender, Event<TypingStartEvent> e)
    {
        // add user to our list
        var channel = e.Data.ChannelId;
        var user = e.Data.UserId;
        UpdateTyping(channel, user);
        TypingStatusChanged?.Invoke(this, new Event<Snowflake>(channel));
    }

    private void _client_MessageCreated(object? sender, Event<Message> e)
    {
        var channel = e.Data.ChannelId;
        var user = e.Data.Author.Id;
        // check if the user was in our typing storage
        if (!TypingUsers.TryGetValue(channel, out var users) || !users.ContainsKey(user))
            return;

        // if yes, remove them
        users[user].Cancel();
        RemoveTyping(channel, user);
    }

    public void UpdateTyping(Snowflake channel, Snowflake user)
    {
        // get the channel
        if (!TypingUsers.ContainsKey(channel))
            TypingUsers.Add(channel, new());

        var users = TypingUsers[channel];

        // if the user was already writing, cancel the remove
        //TODO: rather reuse the existing task instead of canceling?
        if (users.TryGetValue(user, out var cancel))
            cancel.Cancel(); // cancel the timeout

        // start a new remove task
        var token = new CancellationTokenSource();
        users[user] = token;
        Task.Run(() => RemoveTypingAfterTimeout(channel, user, TypingTimeout, token));
    }

    public async void RemoveTypingAfterTimeout(Snowflake channel, Snowflake user, int timeout, CancellationTokenSource token)
    {
        try
        {
            await Task.Delay(timeout * 1000, token.Token);
            RemoveTyping(channel, user);
        } 
        catch (TaskCanceledException)
        {
            // cancel culture
        }
    }

    public void RemoveTyping(Snowflake channel, Snowflake user)
    {
        TypingUsers[channel].Remove(user);
        TypingStatusChanged?.Invoke(this, new Event<Snowflake>(channel));
    }

    public IEnumerable<Snowflake>? GetTypingUsers(Snowflake channel)
    {
        if (!TypingUsers.TryGetValue(channel, out var users))
            return null;

        return users.Keys;
    }
}
