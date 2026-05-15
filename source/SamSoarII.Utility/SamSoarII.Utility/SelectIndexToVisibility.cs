using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SamSoarII.Utility;

[ValueConversion(typeof(int), typeof(Visibility))]
public class SelectIndexToVisibility : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		int num = (int)value;
		if (num == 1)
		{
			return Visibility.Visible;
		}
		return Visibility.Hidden;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return -1;
	}
}
