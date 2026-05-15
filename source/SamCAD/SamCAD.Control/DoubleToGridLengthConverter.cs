using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SamCAD.Control;

[ValueConversion(typeof(double), typeof(GridLength))]
public class DoubleToGridLengthConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return new GridLength((value is double) ? ((double)value) : 0.0, GridUnitType.Pixel);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is GridLength gridLength) ? gridLength.Value : 0.0;
	}
}
