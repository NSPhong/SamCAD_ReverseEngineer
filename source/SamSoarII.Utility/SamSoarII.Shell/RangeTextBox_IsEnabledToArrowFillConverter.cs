using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SamSoarII.Shell;

[ValueConversion(typeof(bool), typeof(Brush))]
public class RangeTextBox_IsEnabledToArrowFillConverter : IValueConverter
{
	public static readonly Brush Brush_Enabled = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 64,
		G = 64,
		B = 64
	});

	public static readonly Brush Brush_Disabled = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 160,
		G = 160,
		B = 160
	});

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is bool && (bool)value) ? Brush_Enabled : Brush_Disabled;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value.Equals(Brush_Enabled);
	}
}
