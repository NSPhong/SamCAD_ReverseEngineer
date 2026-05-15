using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

[ValueConversion(typeof(IPolylineEntity), typeof(Visibility))]
public class HasToVisibleConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((value is IPolylineEllipse && !(value is IPolylineEllipseArch)) || (value is IPolylineCircle && !(value is IPolylineArch))) ? Visibility.Collapsed : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is Visibility && (Visibility)value == Visibility.Visible) ? new PolylineCircle() : null;
	}
}
