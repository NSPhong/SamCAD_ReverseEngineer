using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.Tab;

internal class DockTabBarDrawer : DrawingVisual
{
	public static readonly double TabBarHeight = 22.0;

	public static readonly double TabBarLineThickness = 1.0;

	public static readonly double ItemMargin = 5.0;

	public static readonly double ItemMinWidth = 60.0;

	public static readonly double ItemMaxWidth = 200.0;

	public static readonly Brush ItemBrush_Unselected_MouseOut = ViewCommon.White;

	public static readonly Brush ItemBrush_Unselected_MouseOver = ViewCommon.LightBlue;

	public static readonly Brush ItemBrush_Selected_Unfocused = ViewCommon.Gray;

	public static readonly Brush ItemBrush_Selected_Focused = ViewCommon.Blue;

	public static readonly Pen ItemPen_Selected = new Pen(ViewCommon.Gray, 0.75);

	public static readonly Pen ItemPen_Unselected = new Pen(ViewCommon.Gray, 0.75);

	public static readonly double TextSize = 12.0;

	public static readonly double TextMaxWidth = 160.0;

	public static readonly Typeface TextTypeface = new Typeface(new FontFamily("Microsoft Yahei"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

	public static readonly Brush TextBrush_Unselected = ViewCommon.Black;

	public static readonly Brush TextBrush_Selected = ViewCommon.White;

	public static readonly double CloseWidth = 13.0;

	public static readonly Brush CloseBrushMouseOver = new SolidColorBrush(new Color
	{
		A = 96,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	public static readonly ImageSource CloseImage = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinClose_Focused.png"));

	public static readonly double IconWidth = 16.0;

	public static readonly double IconHeight = 16.0;

	public static readonly double ExtendWidth = 13.0;

	public static readonly double ExtendButtonWidth = 8.0;

	public static readonly double ExtendButtonHeight = 4.0;

	public static readonly Brush ExtendBackgroundMouseOver = new SolidColorBrush(new Color
	{
		A = 96,
		R = 0,
		G = 122,
		B = 204
	});

	public static readonly Brush ExtendBackgroundIsOpen = ViewCommon.Blue;

	public static readonly Brush ExtendBackground = ViewCommon.White;

	public static readonly Brush ExtendForegroundMouseOver = ViewCommon.Blue;

	public static readonly Brush ExtendForegroundIsOpen = ViewCommon.White;

	public static readonly Brush ExtendForeground = ViewCommon.Black;

	private DockTabBarContainer parent;

	public DockTabBarContainer ViewParent => parent;

	public DockTab Tab => ViewParent?.ViewParent;

	public DockTabBarInfo Info => ViewParent?.Info;

	public DockTabBarDrawer(DockTabBarContainer _parent)
	{
		parent = _parent;
	}

	public void Update()
	{
		using DrawingContext drawingContext = RenderOpen();
		if (Info == null || Info.Parent == null || Info.Infos.Count == 0)
		{
			return;
		}
		try
		{
			if (Tab != null)
			{
				Brush brush = (Tab.IsUserFocused ? ItemBrush_Selected_Focused : ItemBrush_Selected_Unfocused);
				Rect rectangle = ((Tab.ItemBarAlignment == DockTab.ItemBarAlignments.Top) ? new Rect(0.0, TabBarHeight - TabBarLineThickness, parent.ActualWidth, TabBarLineThickness + 1.0) : new Rect(0.0, -1.0, parent.ActualWidth, TabBarLineThickness + 1.0));
				drawingContext.DrawRectangle(brush, null, rectangle);
			}
			for (int i = 0; i < Info.Infos.Count(); i++)
			{
				DockTabItemInfo dockTabItemInfo = Info.Infos[i];
				DockTab dockTab = dockTabItemInfo?.Parent?.Parent;
				IDockContent dockContent = dockTabItemInfo?.Container?.ViewContent?.DockContent;
				double num = 0.0;
				DockTab tab = Tab;
				if (tab != null && tab.ItemBarAlignment == DockTab.ItemBarAlignments.Bottom)
				{
					num = TabBarLineThickness;
				}
				double num2 = TabBarHeight;
				if (Tab != null)
				{
					num2 -= TabBarLineThickness;
				}
				if (i != parent.SelectedIndex)
				{
					drawingContext.DrawRectangle((i == parent.MouseOverIndex) ? ItemBrush_Unselected_MouseOver : ItemBrush_Unselected_MouseOut, ItemPen_Unselected, new Rect(dockTabItemInfo.Left, num, dockTabItemInfo.Width, num2));
				}
				else
				{
					if (dockTab != null && dockTab.ItemBarAlignment == DockTab.ItemBarAlignments.Top)
					{
						DockTab tab2 = Tab;
						drawingContext.DrawRectangle((tab2 != null && tab2.IsUserFocused) ? ItemBrush_Selected_Focused : ItemBrush_Selected_Unfocused, null, new Rect(dockTabItemInfo.Left, num, dockTabItemInfo.Width, num2 + 1.0));
					}
					else
					{
						DockTab tab3 = Tab;
						drawingContext.DrawRectangle((tab3 != null && tab3.IsUserFocused) ? ItemBrush_Selected_Focused : ItemBrush_Selected_Unfocused, null, new Rect(dockTabItemInfo.Left, num - 1.0, dockTabItemInfo.Width, num2 + 1.0));
					}
					drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Left, num), new Point(dockTabItemInfo.Left, num + num2));
					drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Right, num), new Point(dockTabItemInfo.Right, num + num2));
					DockTab tab4 = Tab;
					if (tab4 == null || tab4.ItemBarAlignment != DockTab.ItemBarAlignments.Top)
					{
						drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Left, num + num2), new Point(dockTabItemInfo.Right, num + num2));
					}
					else
					{
						drawingContext.DrawLine(ItemPen_Selected, new Point(0.0, num + num2), new Point(dockTabItemInfo.Left, num + num2));
						drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Right, num + num2), new Point(Tab.ActualWidth, num + num2));
					}
					DockTab tab5 = Tab;
					if (tab5 == null || tab5.ItemBarAlignment != DockTab.ItemBarAlignments.Bottom)
					{
						drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Left, num), new Point(dockTabItemInfo.Right, num));
					}
					else
					{
						drawingContext.DrawLine(ItemPen_Selected, new Point(0.0, num), new Point(dockTabItemInfo.Left, num));
						drawingContext.DrawLine(ItemPen_Selected, new Point(dockTabItemInfo.Right, num), new Point(Tab.ActualWidth, num));
					}
				}
				FormattedText formattedText = dockTabItemInfo.Container.HeaderText;
				if (i == parent.SelectedIndex)
				{
					formattedText = new FormattedText(formattedText.Text, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, TextTypeface, TextSize, TextBrush_Selected);
					formattedText.MaxLineCount = 1;
					formattedText.MaxTextWidth = dockTabItemInfo.Container.HeaderText.MaxTextWidth;
				}
				if (dockTab != null && dockTab.Theme == DockTab.Themes.SamSoarII)
				{
					drawingContext.DrawText(formattedText, new Point(dockTabItemInfo.Left + ItemMargin * 2.0 + IconWidth, (num2 - formattedText.Height) / 2.0));
				}
				else
				{
					drawingContext.DrawText(formattedText, new Point(dockTabItemInfo.Left + ItemMargin, (num2 - formattedText.Height) / 2.0));
				}
				if (dockContent?.Icon != null && dockTab != null && dockTab.Theme == DockTab.Themes.SamSoarII)
				{
					drawingContext.DrawImage(dockContent.Icon, new Rect(dockTabItemInfo.Left + ItemMargin, (num2 - IconHeight) / 2.0, IconWidth, IconHeight));
				}
				if (dockTabItemInfo != null && dockTabItemInfo.Parent?.Parent?.Theme == DockTab.Themes.SamSoarII && (i == parent.MouseOverIndex || i == parent.SelectedIndex))
				{
					Rect rectangle2 = new Rect(dockTabItemInfo.Right - ItemMargin - CloseWidth, (num2 - CloseWidth) / 2.0, CloseWidth, CloseWidth);
					drawingContext.DrawImage(CloseImage, rectangle2);
					if (i == parent.MouseOverIndex && parent.CloseOver)
					{
						drawingContext.DrawRectangle(CloseBrushMouseOver, null, rectangle2);
					}
				}
			}
			if (Tab != null && Tab.HiddenInfos.Count() > 0)
			{
				Brush brush2 = (parent.IsExtendOpen ? ExtendBackgroundIsOpen : (parent.ExtendOver ? ExtendBackgroundMouseOver : ExtendBackground));
				drawingContext.DrawRectangle(brush2, null, new Rect(parent.ActualWidth - ExtendWidth - 4.0, 0.0, ExtendWidth, parent.ActualHeight - TabBarLineThickness));
				brush2 = (parent.IsExtendOpen ? ExtendForegroundIsOpen : (parent.ExtendOver ? ExtendForegroundMouseOver : ExtendForeground));
				StreamGeometry streamGeometry = new StreamGeometry();
				using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
				{
					double num3 = parent.ActualHeight / 2.0 - ExtendButtonHeight / 2.0;
					double num4 = parent.ActualWidth - ExtendWidth / 2.0 - ExtendButtonWidth / 2.0 - 4.0;
					streamGeometryContext.BeginFigure(new Point(num4, num3), isFilled: true, isClosed: true);
					streamGeometryContext.LineTo(new Point(num4 + ExtendButtonWidth, num3), isStroked: true, isSmoothJoin: true);
					streamGeometryContext.LineTo(new Point(num4 + ExtendButtonWidth / 2.0, num3 + ExtendButtonHeight), isStroked: true, isSmoothJoin: true);
					streamGeometryContext.LineTo(new Point(num4, num3), isStroked: true, isSmoothJoin: true);
				}
				drawingContext.DrawGeometry(brush2, null, streamGeometry);
			}
		}
		catch (Exception)
		{
		}
	}
}
