using System;
using System.Globalization;
using System.Windows.Data;

namespace SamSoarII.Utility;

[ValueConversion(typeof(double), typeof(double))]
public class DoubleConverter3 : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = (double)value / 4.0;
		return num;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = (double)value * 4.0;
		return num;
	}
}
