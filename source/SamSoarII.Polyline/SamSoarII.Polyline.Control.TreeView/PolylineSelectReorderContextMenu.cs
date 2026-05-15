using System.Windows;
using System.Windows.Controls;

namespace SamSoarII.Polyline.Control.TreeView;

public class PolylineSelectReorderContextMenu : ContextMenu
{
	private MenuItem mi_movebefore;

	private MenuItem mi_moveafter;

	private MenuItem mi_movehome;

	private MenuItem mi_moveend;

	public event ReorderEventHandler Reorder;

	public PolylineSelectReorderContextMenu()
	{
		mi_movebefore = new MenuItem
		{
			Header = "Move to this place"
		};
		mi_moveafter = new MenuItem
		{
			Header = "After moving here"
		};
		mi_movehome = new MenuItem
		{
			Header = "Move to the top"
		};
		mi_moveend = new MenuItem
		{
			Header = "Move to the bottom"
		};
		mi_movebefore.Click += OnMenuItemClick;
		mi_moveafter.Click += OnMenuItemClick;
		mi_movehome.Click += OnMenuItemClick;
		mi_moveend.Click += OnMenuItemClick;
		base.Items.Add(mi_movebefore);
		base.Items.Add(mi_moveafter);
		base.Items.Add(mi_movehome);
		base.Items.Add(mi_moveend);
	}

	private void OnMenuItemClick(object sender, RoutedEventArgs e)
	{
		if (sender == mi_movebefore)
		{
			this.Reorder?.Invoke(this, new ReorderEventArgs(ReorderCommands.MoveBefore));
		}
		if (sender == mi_moveafter)
		{
			this.Reorder?.Invoke(this, new ReorderEventArgs(ReorderCommands.MoveAfter));
		}
		if (sender == mi_movehome)
		{
			this.Reorder?.Invoke(this, new ReorderEventArgs(ReorderCommands.MoveHome));
		}
		if (sender == mi_moveend)
		{
			this.Reorder?.Invoke(this, new ReorderEventArgs(ReorderCommands.MoveEnd));
		}
	}
}
