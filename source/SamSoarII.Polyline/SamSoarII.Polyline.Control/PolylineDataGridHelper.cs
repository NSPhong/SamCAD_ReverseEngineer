using System.Windows;
using System.Windows.Controls;
using SamSoarII.Polyline.Entity;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridHelper
{
	public static void HandlePropertyChanged(IPolylineDataGridControl c, DependencyPropertyChangedEventArgs e)
	{
		if (e.Property == UIElement.IsVisibleProperty && e.OldValue is bool && e.NewValue is bool)
		{
			if (!(bool)e.OldValue && (bool)e.NewValue)
			{
				Add(c);
			}
			if ((bool)e.OldValue && !(bool)e.NewValue)
			{
				Remove(c);
			}
		}
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (e.OldValue is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)e.OldValue;
				polylineDataGridItem.RemoveView(c);
			}
			if (e.NewValue is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem2 = (PolylineDataGridItem)e.NewValue;
				polylineDataGridItem2.AddView(c);
				Read(c);
			}
		}
	}

	public static void Add(IPolylineDataGridControl c)
	{
		if (!(c is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)c;
		PolylineDataGridEnv polylineDataGridEnv = new PolylineDataGridEnv(c);
		while (frameworkElement != null)
		{
			if (frameworkElement is DataGridCell)
			{
				DataGridCell dataGridCell = (polylineDataGridEnv.Cell = (DataGridCell)frameworkElement);
				if (dataGridCell.Column is PolylineDataGridUserColumn)
				{
					PolylineDataGridUserColumn polylineDataGridUserColumn = (PolylineDataGridUserColumn)dataGridCell.Column;
					c.Core = polylineDataGridUserColumn.Core;
				}
			}
			frameworkElement = ((frameworkElement.Parent is FrameworkElement) ? ((FrameworkElement)frameworkElement.Parent) : ((frameworkElement.TemplatedParent is FrameworkElement) ? ((FrameworkElement)frameworkElement.TemplatedParent) : null));
		}
		Read(c);
	}

	public static void Remove(IPolylineDataGridControl c)
	{
		c.Write();
		c.Core = null;
	}

	public static void Read(IPolylineDataGridControl c)
	{
		if (c.Core == null || !(c is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)c;
		if (!(frameworkElement.DataContext is PolylineDataGridItem))
		{
			return;
		}
		PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)frameworkElement.DataContext;
		if (polylineDataGridItem == null)
		{
			return;
		}
		IPolylineEntity core = polylineDataGridItem.Core;
		if (core != null)
		{
			IPolylineUserObject polylineUserObject = core.UserObjs[c.Core.ID];
			if (polylineUserObject != null)
			{
				c.Read(polylineUserObject.Value.ToString());
			}
		}
	}

	public static void Write(IPolylineDataGridControl c, string v)
	{
		if (c.Core == null || !(c is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)c;
		if (!(frameworkElement.DataContext is PolylineDataGridItem))
		{
			return;
		}
		PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)frameworkElement.DataContext;
		if (polylineDataGridItem == null)
		{
			return;
		}
		IPolylineEntity core = polylineDataGridItem.Core;
		if (core == null)
		{
			return;
		}
		IPolylineUserObject polylineUserObject = core.UserObjs[c.Core.ID];
		if (polylineUserObject == null)
		{
			return;
		}
		switch (c.Core.DataType)
		{
		case PolylineUserDataType.Bool:
		{
			if (bool.TryParse(v, out var result2))
			{
				polylineUserObject.Value = result2;
			}
			break;
		}
		case PolylineUserDataType.Int:
		{
			if (int.TryParse(v, out var result3))
			{
				polylineUserObject.Value = result3;
			}
			break;
		}
		case PolylineUserDataType.Double:
		{
			if (double.TryParse(v, out var result))
			{
				polylineUserObject.Value = result;
			}
			break;
		}
		case PolylineUserDataType.String:
			polylineUserObject.Value = v;
			break;
		}
	}
}
