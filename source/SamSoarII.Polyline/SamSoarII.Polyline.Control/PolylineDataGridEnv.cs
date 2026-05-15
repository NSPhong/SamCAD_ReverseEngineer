using System;
using System.Windows;
using System.Windows.Controls;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridEnv : IDisposable
{
	private IPolylineDataGridControl ctrl;

	private PolylineDataGridItem item;

	private DataGridRow row;

	private DataGridCell cell;

	public IPolylineDataGridControl Ctrl => ctrl;

	public PolylineDataGridItem Item
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}

	public DataGridRow Row
	{
		get
		{
			return row;
		}
		set
		{
			if (row != null)
			{
				row.Selected -= OnRowSelected;
				row.Unselected -= OnRowUnselected;
			}
			row = value;
			if (row != null)
			{
				row.Selected += OnRowSelected;
				row.Unselected += OnRowUnselected;
				if (row.IsSelected)
				{
					ctrl?.Select();
				}
				else
				{
					ctrl?.Unselect();
				}
			}
		}
	}

	public DataGridCell Cell
	{
		get
		{
			return cell;
		}
		set
		{
			cell = value;
		}
	}

	public PolylineDataGridEnv(IPolylineDataGridControl _ctrl)
	{
		ctrl = _ctrl;
		item = null;
		row = null;
		cell = null;
	}

	public void Dispose()
	{
		Row = null;
		Cell = null;
		ctrl = null;
		item = null;
	}

	private void OnRowSelected(object sender, RoutedEventArgs e)
	{
		ctrl?.Select();
	}

	private void OnRowUnselected(object sender, RoutedEventArgs e)
	{
		ctrl?.Unselect();
	}
}
