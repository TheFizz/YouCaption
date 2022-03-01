using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace YouCaptionBase.Converters
{
    public class DownloadButtonStateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[2] == null)
                return false;
            if (values[3] == null)
                return false;
            if((bool)values[3]==false)
                return false;

            if ((bool)values[2] == true)
            {
                if (values[0] != null && values[1] != null)
                    return true;
                return false;
            }
            else
            {
                if (values[0] != null)
                    return true;
                return false;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("Going back to what you had isn't supported.");
        }
    }
}

