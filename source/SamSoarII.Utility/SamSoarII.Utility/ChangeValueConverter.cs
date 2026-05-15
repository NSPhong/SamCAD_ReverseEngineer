using System;
using System.Globalization;
using System.Windows.Data;

namespace SamSoarII.Utility;

[ValueConversion(typeof(double), typeof(double))]
public class ChangeValueConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = double.Parse(parameter as string);
		return (double)value + num;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = double.Parse(parameter as string);
		return (double)value - num;
	}
}
