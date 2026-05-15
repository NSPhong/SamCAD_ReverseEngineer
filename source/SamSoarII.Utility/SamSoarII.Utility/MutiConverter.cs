using System;
using System.Globalization;
using System.Windows.Data;

namespace SamSoarII.Utility;

public class MutiConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		return (double)values[0] + (double)values[1] + 20.0;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		return null;
	}
}
