using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SamSoarII.Polyline.Control;

[ValueConversion(typeof(Vector), typeof(Point))]
public class VectorToPointConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is Vector { X: var x }) ? new Point(x, ((Vector)value).Y) : default(Point);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is Point { X: var x }) ? new Vector(x, ((Point)value).Y) : default(Vector);
	}
}
