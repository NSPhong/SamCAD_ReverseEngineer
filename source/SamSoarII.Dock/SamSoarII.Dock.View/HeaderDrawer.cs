using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View;

internal class HeaderDrawer : DrawingVisual
{
	public static readonly double HeaderHeight = 21.0;

	public static readonly Brush Background_Unfocused = ViewCommon.White;

	public static readonly Brush Background_Focused = ViewCommon.Blue;

	public static readonly double FontLeft = 6.0;

	public static readonly double FontSize = 12.0;

	public static readonly Typeface FontTypeface = new Typeface(new FontFamily("Microsoft Yahei"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

	public static readonly Brush FontColor_Unfoused = ViewCommon.Black;

	public static readonly Brush FontColor_Focused = ViewCommon.White;

	public static readonly double RectMargin = 10.0;

	public static readonly double RectHeight = 6.0;

	public static readonly double ImagWidth = 5.0;

	public static readonly double ImagHeight = 6.0;

	public static readonly ImageSource RectBrushImage_Unfocused = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/TitleTexture.png"));

	public static readonly ImageSource RectBrushImage_Focused = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/TitleTexture_Focused.png"));

	public static readonly Brush RectBrush_Unfocused = new ImageBrush(RectBrushImage_Unfocused)
	{
		TileMode = TileMode.Tile,
		Stretch = Stretch.Uniform,
		ViewportUnits = BrushMappingMode.Absolute,
		Viewport = new Rect(0.0, 0.0, 5.0, 6.0),
		Transform = new ScaleTransform
		{
			ScaleX = 0.75,
			ScaleY = 0.75
		}
	};

	public static readonly Brush RectBrush_Focused = new ImageBrush(RectBrushImage_Focused)
	{
		TileMode = TileMode.Tile,
		Stretch = Stretch.Uniform,
		ViewportUnits = BrushMappingMode.Absolute,
		Viewport = new Rect(0.0, 0.0, 5.0, 6.0),
		Transform = new ScaleTransform
		{
			ScaleX = 0.75,
			ScaleY = 0.75
		}
	};

	public static readonly double ButtMargin = 3.0;

	public static readonly double ButtHeight = 13.0;

	public static readonly double ButtWidth = 13.0;

	public static readonly ImageSource[] ButtImages_Unfocused = new ImageSource[8]
	{
		null,
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMenu.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinAutoHide.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinDock.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMaximize.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMaximize.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinRestore.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinClose.png"))
	};

	public static readonly ImageSource[] ButtImages_Focused = new ImageSource[8]
	{
		null,
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMenu_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinAutoHide_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinDock_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMaximize_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinMaximize_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinRestore_Focused.png")),
		new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Dock;component/Resources/Images/PinClose_Focused.png"))
	};

	public static readonly Brush ButtMouseOverBrush = new SolidColorBrush(new Color
	{
		A = 96,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	private HeaderContainer parent;

	private double buttonstart;

	public static double ButtAllWidth => ButtWidth + ButtMargin * 2.0;

	public static double ButtAllHeight => ButtHeight + ButtMargin * 2.0;

	public HeaderContainer ViewParent => parent;

	public IDockBaseView BaseView => ViewParent?.ViewParent;

	public double ButtonStart => buttonstart;

	public HeaderDrawer(HeaderContainer _parent)
	{
		parent = _parent;
	}

	public void Update()
	{
		using DrawingContext drawingContext = RenderOpen();
		IDockContent dockContent = BaseView?.DockContent;
		if (dockContent == null || BaseView.HeaderType == ViewCommon.HeaderTypes.Null)
		{
			return;
		}
		FormattedText formattedText = null;
		double num = 0.0;
		double actualWidth = parent.ActualWidth;
		double headerHeight = HeaderHeight;
		bool isUserFocused = BaseView.IsUserFocused;
		drawingContext.DrawRectangle(isUserFocused ? Background_Focused : Background_Unfocused, null, isUserFocused ? new Rect(-1.0, -1.0, parent.ActualWidth + 2.0, headerHeight + 1.0) : new Rect(0.0, 0.0, parent.ActualWidth, headerHeight));
		actualWidth -= 3.0 * (ButtMargin * 2.0 + ButtWidth);
		actualWidth -= FontLeft;
		if (actualWidth > 0.0)
		{
			formattedText = new FormattedText(dockContent.Header, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, FontSize, isUserFocused ? FontColor_Focused : FontColor_Unfoused);
			formattedText.MaxTextWidth = actualWidth;
			formattedText.MaxLineCount = 1;
			actualWidth -= formattedText.Width;
			drawingContext.DrawText(formattedText, new Point(num + FontLeft, (headerHeight - formattedText.Height) / 2.0));
			num += FontLeft + formattedText.Width;
		}
		actualWidth -= 2.0 * RectMargin;
		if (actualWidth > 0.0)
		{
			for (double num2 = num + RectMargin; num2 < num + actualWidth + RectMargin; num2 += 3.75)
			{
				drawingContext.DrawImage(isUserFocused ? RectBrushImage_Focused : RectBrushImage_Unfocused, new Rect(num2, (headerHeight - RectHeight) / 2.0, 3.75, 4.5));
			}
			num += RectMargin * 2.0 + actualWidth;
		}
		buttonstart = num;
		ViewCommon.HeaderTypes headerType = BaseView.HeaderType;
		for (int i = 0; i < 3; i++)
		{
			ViewCommon.HeaderButtonTypes headerButtonType = ViewCommon.GetHeaderButtonType(headerType, i);
			ImageSource imageSource = (isUserFocused ? ButtImages_Focused[(int)headerButtonType] : ButtImages_Unfocused[(int)headerButtonType]);
			drawingContext.DrawImage(imageSource, new Rect(num + ButtMargin, (headerHeight - ButtHeight) / 2.0, ButtWidth, ButtHeight));
			if (parent.MouseOverIndex == i)
			{
				drawingContext.DrawRectangle(ButtMouseOverBrush, null, new Rect(num, (headerHeight - ButtAllHeight) / 2.0, ButtAllWidth, ButtAllHeight));
			}
			num += ButtAllWidth;
		}
	}
}
