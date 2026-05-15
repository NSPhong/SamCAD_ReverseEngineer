using System;
using System.ComponentModel;
using System.Windows;

namespace SamSoarII.Dock.TreeList;

public interface ITreeListItemCtx : INotifyPropertyChanged, IComparable<ITreeListItemCtx>
{
	object Header { get; }

	bool IsDirectory { get; }

	bool IsSorted { get; }

	bool IsDefaultExpand { get; }

	int MenuCount { get; }

	UIElement GetIcon();

	UIElement GetMenuIcon(int mid);

	string GetMenuHeader(int mid);

	bool IsMenuShow(int mid);

	bool IsMenuAble(int mid);

	bool CanSingleSelected();

	bool CanMultiSelectedWith(ITreeListItemCtx ctx);
}
