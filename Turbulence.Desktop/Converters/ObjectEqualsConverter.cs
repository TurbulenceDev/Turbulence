using Avalonia.Data.Converters;
using System.Globalization;

namespace Turbulence.Desktop.Converters;

public class ObjectEqualsMultiConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count == 0) 
            return true;

        var val = values[0];
        foreach (var item in values.Skip(1)) 
        { 
            if (val != item)
                return false;
        }
        return true;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
