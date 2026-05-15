using System;
using System.Globalization;
using System.Windows.Data;

namespace SamSoarII.Shell;

[ValueConversion(typeof(bool), typeof(double))]
public class GridPenningHorizontalScroll_BoolToOpacityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is bool && (bool)value) ? 1.0 : 0.2;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value is double && (double)value > 0.8;
	}
}
