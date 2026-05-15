using System.Windows;
using System.Windows.Controls;

namespace SamSoarII.Polyline.Control;

public class PolylineSelectItemPanel : VirtualizingStackPanel
{
	private PolylineSelectTreeBox parent;

	public PolylineSelectItemPanel()
	{
		base.Loaded += OnLoaded;
		base.Unloaded += OnUnloaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		FrameworkElement frameworkElement = this;
		while (frameworkElement != null)
		{
			if (frameworkElement is PolylineSelectTreeBox)
			{
				parent = (PolylineSelectTreeBox)frameworkElement;
				parent.ItemsPanel = this;
			}
			if (frameworkElement.Parent is FrameworkElement)
			{
				frameworkElement = (FrameworkElement)frameworkElement.Parent;
				continue;
			}
			if (frameworkElement.TemplatedParent is FrameworkElement)
			{
				frameworkElement = (FrameworkElement)frameworkElement.TemplatedParent;
				continue;
			}
			break;
		}
	}

	private void OnUnloaded(object sender, RoutedEventArgs e)
	{
	}
}
