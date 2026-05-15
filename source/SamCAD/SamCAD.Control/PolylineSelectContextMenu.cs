using System.Windows.Controls;
using System.Windows.Input;

namespace SamCAD.Control;

public class PolylineSelectContextMenu : ContextMenu
{
	private MenuItem mi_groupmerge;

	private MenuItem mi_groupsplit;

	private MenuItem mi_groupmove;

	private MenuItem mi_groupreverse;

	private MenuItem mi_groupmirror;

	private MenuItem mi_groupscale;

	private MenuItem mi_grouprotate;

	private MenuItem mi_groupreorder;

	private MenuItem mi_groupmatrix;

	private MenuItem mi_cut;

	private MenuItem mi_copy;

	private MenuItem mi_paste;

	private MenuItem mi_delete;

	public PolylineSelectContextMenu()
	{
		mi_groupmerge = new MenuItem
		{
			Command = UserCommands.GroupMerge
		};
		mi_groupsplit = new MenuItem
		{
			Command = UserCommands.GroupSplit
		};
		mi_groupmove = new MenuItem
		{
			Command = UserCommands.GroupMove
		};
		mi_groupreverse = new MenuItem
		{
			Command = UserCommands.GroupReverse
		};
		mi_groupmirror = new MenuItem
		{
			Command = UserCommands.GroupMirror
		};
		mi_groupscale = new MenuItem
		{
			Command = UserCommands.GroupScale
		};
		mi_grouprotate = new MenuItem
		{
			Command = UserCommands.GroupRotate
		};
		mi_groupreorder = new MenuItem
		{
			Command = UserCommands.GroupReorder
		};
		mi_groupmatrix = new MenuItem
		{
			Command = UserCommands.GroupMatrix
		};
		mi_cut = new MenuItem
		{
			Command = ApplicationCommands.Cut
		};
		mi_copy = new MenuItem
		{
			Command = ApplicationCommands.Copy
		};
		mi_paste = new MenuItem
		{
			Command = ApplicationCommands.Paste
		};
		mi_delete = new MenuItem
		{
			Command = ApplicationCommands.Delete
		};
		base.Items.Add(mi_groupmerge);
		base.Items.Add(mi_groupsplit);
		base.Items.Add(mi_groupmove);
		base.Items.Add(mi_groupreverse);
		base.Items.Add(mi_groupmirror);
		base.Items.Add(mi_groupscale);
		base.Items.Add(mi_grouprotate);
		base.Items.Add(mi_groupreorder);
		base.Items.Add(mi_groupmatrix);
		base.Items.Add(new Separator());
		base.Items.Add(mi_cut);
		base.Items.Add(mi_copy);
		base.Items.Add(mi_paste);
		base.Items.Add(mi_delete);
	}
}
