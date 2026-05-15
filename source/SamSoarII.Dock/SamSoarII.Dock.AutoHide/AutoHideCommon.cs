using System.Windows.Controls;

namespace SamSoarII.Dock.AutoHide;

public abstract class AutoHideCommon
{
	public enum Sides
	{
		Top,
		Bottom,
		Left,
		Right
	}

	internal static Orientation GetSideStackOrientation(Sides side)
	{
		switch (side)
		{
		case Sides.Top:
		case Sides.Bottom:
			return Orientation.Vertical;
		case Sides.Left:
		case Sides.Right:
			return Orientation.Horizontal;
		default:
			return Orientation.Vertical;
		}
	}

	internal static System.Windows.Controls.Dock GetSideWPFDock(Sides side)
	{
		return side switch
		{
			Sides.Top => System.Windows.Controls.Dock.Top, 
			Sides.Left => System.Windows.Controls.Dock.Left, 
			Sides.Bottom => System.Windows.Controls.Dock.Bottom, 
			Sides.Right => System.Windows.Controls.Dock.Right, 
			_ => System.Windows.Controls.Dock.Top, 
		};
	}
}
