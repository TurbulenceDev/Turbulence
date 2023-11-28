using Avalonia.Controls.Documents;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.Globalization;
using Turbulence.Discord;
using Turbulence.Discord.Models.DiscordChannel;

namespace Turbulence.Desktop.Converters;

public class MessageContentConverter : IValueConverter
{
    private readonly IPlatformClient _client = Ioc.Default.GetService<IPlatformClient>()!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Message message)
            return null;

        var res = new InlineCollection
        {
            _client.GetMessageContent(message),
        };
        if (message.EditedTimestamp != null)
        {
            var editRun = new Run(" [Edited]");
            //TODO: tooltip?
            editRun.Classes.Add("Edit");
            res.Add(editRun);
        }
        return res;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
