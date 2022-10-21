using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumioApp.Converters;

internal class SecondsToTimeConverter : IValueConverter
{
     public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        TimeSpan time = TimeSpan.FromSeconds(System.Convert.ToDouble(value));

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string str;
        if (time.Hours > 0)
        {
            str = time.ToString(@"hh\:mm\:ss");
        }
        else if (time.Minutes > 9)
        {
            str = time.ToString(@"mm\:ss");
        }
        else
        {
            str = time.ToString(@"m\:ss");
        }


        return str;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
