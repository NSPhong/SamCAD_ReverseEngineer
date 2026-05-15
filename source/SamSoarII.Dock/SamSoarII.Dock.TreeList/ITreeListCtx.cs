using System.Windows;

namespace SamSoarII.Dock.TreeList;

public interface ITreeListCtx
{
	int MenuCount { get; }

	UIElement GetIcon(ITreeListItemCtx ic);

	UIElement GetMenuIcon(ITreeListItemCtx ic, int mid);

	string GetMenuHeader(ITreeListItemCtx ic, int mid);

	bool IsMenuShow(ITreeListItemCtx ic, int mid);

	bool IsMenuAble(ITreeListItemCtx ic, int mid);
}
