using System.ComponentModel;
using System.Windows.Media;

namespace SamSoarII.Shell.Managers;

public class ColorManager : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public static Brush Parse(string text)
	{
		string[] array = text.Split(' ');
		return new SolidColorBrush(new Color
		{
			A = byte.Parse(array[0]),
			R = byte.Parse(array[1]),
			G = byte.Parse(array[2]),
			B = byte.Parse(array[3])
		});
	}

	public static string ToString(Brush brush)
	{
		if (brush is SolidColorBrush)
		{
			SolidColorBrush solidColorBrush = (SolidColorBrush)brush;
			Color color = solidColorBrush.Color;
			return $"{color.A:d} {color.R:d} {color.G:d} {color.B:d}";
		}
		return string.Empty;
	}
}
