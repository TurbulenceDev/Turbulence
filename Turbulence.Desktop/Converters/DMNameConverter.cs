using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turbulence.Discord.Models;

namespace Turbulence.Desktop.Converters;

public class DMNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Snowflake[] ids)
            return null;

        //TODO: fetch names from client (cache)
        return string.Join<Snowflake>(", ", ids);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
