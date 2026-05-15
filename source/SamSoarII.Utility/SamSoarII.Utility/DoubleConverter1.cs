using System;
using System.Globalization;
using System.Windows.Data;

namespace SamSoarII.Utility;

[ValueConversion(typeof(double), typeof(double))]
public class DoubleConverter1 : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = (double)value - 103.5;
		return num;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = (double)value + 75.0;
		return num;
	}
}
