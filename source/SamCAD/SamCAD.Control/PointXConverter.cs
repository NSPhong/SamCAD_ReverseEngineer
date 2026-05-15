using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SamCAD.Control;

[ValueConversion(typeof(Point), typeof(string))]
public class PointXConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is Point) ? $"X={((Point)value).X:f2}" : string.Empty;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return 0.0;
	}
}
