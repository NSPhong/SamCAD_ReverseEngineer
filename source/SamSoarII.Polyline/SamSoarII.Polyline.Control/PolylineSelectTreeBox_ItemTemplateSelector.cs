using System.Windows;
using System.Windows.Controls;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PolylineSelectTreeBox_ItemTemplateSelector : DataTemplateSelector
{
	public DataTemplate Group { get; set; }

	public DataTemplate Entity { get; set; }

	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		if (item is IPolylineGroup)
		{
			return Group;
		}
		return Entity;
	}
}
