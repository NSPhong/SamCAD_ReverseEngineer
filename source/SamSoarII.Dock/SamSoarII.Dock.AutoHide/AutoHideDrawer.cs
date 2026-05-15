using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.AutoHide;

internal class AutoHideDrawer : DrawingVisual
{
	public static readonly RotateTransform VerticalTransform = new RotateTransform(90.0, 0.0, 0.0);

	public static readonly double Interval = 12.0;

	public static readonly double RectThickness = 8.0;

	public static readonly Brush RectBrush_MouseOut = ViewCommon.Gray;

	public static readonly Brush RectBrush_MouseOver = ViewCommon.Blue;

	public static readonly double TextInterval = 6.0;

	public static readonly double TextSize = 12.0;

	public static readonly Typeface TextTypeface = new Typeface(new FontFamily("Microsoft Yahei"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

	public static readonly Brush TextBrush_MouseOut = ViewCommon.Black;

	public static readonly Brush TextBrush_MouseOver = ViewCommon.Blue;

	private AutoHideSideBar parent;

	private List<AutoHideDrawerInfo> infos;

	public static double AllThickness => RectThickness + TextInterval + TextSize;

	internal AutoHideSideBar ViewParent => parent;

	internal IList<AutoHideDrawerInfo> Infos => infos;

	public AutoHideDrawer(AutoHideSideBar _parent)
	{
		parent = _parent;
		infos = new List<AutoHideDrawerInfo>();
		if (parent.IsVerticalOrientation)
		{
			base.Transform = VerticalTransform;
		}
	}

	internal void Update()
	{
		using DrawingContext drawingContext = RenderOpen();
		double num = (parent.IsVerticalOrientation ? (0.0 - AllThickness) : 0.0);
		double num2 = (parent.IsVerticalOrientation ? 4.0 : 10.0);
		double num3 = 0.0;
		FormattedText formattedText = null;
		Brush brush = null;
		infos.Clear();
		foreach (IDockContent item in parent.ViewChildren.Select((IDockBaseView bv) => bv.DockContent))
		{
			brush = ((parent.MouseOverItem?.DockContent == item) ? TextBrush_MouseOver : TextBrush_MouseOut);
			formattedText = new FormattedText(item.Header, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, TextTypeface, TextSize, brush);
			num3 = num2 + formattedText.Width;
			infos.Add(new AutoHideDrawerInfo(num2, num3));
			drawingContext.DrawText(formattedText, new Point(num2, num + (parent.IsButtonReverseOrder ? 0.0 : (RectThickness + TextInterval - 4.0))));
			brush = ((parent.MouseOverItem?.DockContent == item) ? RectBrush_MouseOver : RectBrush_MouseOut);
			drawingContext.DrawRectangle(brush, null, new Rect(num2, num + (parent.IsButtonReverseOrder ? (TextSize + TextInterval) : 0.0), formattedText.Width, RectThickness));
			num2 = num3 + Interval;
		}
	}
}
