using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SamSoarII.Shell;

[ValueConversion(typeof(double), typeof(Matrix))]
public class HeightToMatrixConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double offsetY = ((value is double) ? ((double)value) : 0.0);
		return new Matrix(1.0, 0.0, 0.0, -1.0, 0.0, offsetY);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (value is Matrix matrix) ? matrix.OffsetY : 0.0;
	}
}
