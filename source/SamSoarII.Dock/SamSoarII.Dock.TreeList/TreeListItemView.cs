using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SamSoarII.Dock.TreeList;

internal class TreeListItemView : UserControl
{
	public const double DefaultWidth = 80.0;

	public const double DefaultHeight = 18.0;

	public const double LevelWidth = 18.0;

	public const double RectMargin = 4.0;

	public const double TextMargin = 2.0;

	public const double CrossMargin = 2.0;

	public static readonly Brush LineStroke = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 64, 64, 64));

	public static readonly double LineThickness = 0.75;

	public static readonly Brush RectStroke = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 16, 16, 16));

	public static readonly Brush RectFill = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 248, 248, 248));

	public static readonly double RectThickness = 0.75;

	private TreeList parent;

	private Canvas cv_main;

	private Line ln_down;

	private Line ln_left;

	private Rectangle rn_rect;

	private Line ln_recth;

	private Line ln_rectv;

	private UIElement ui_icon;

	private UIElement ui_header;

	private TextBlock tb_header;

	public ITreeListItem Item => (base.DataContext is ITreeListItem) ? ((ITreeListItem)base.DataContext) : null;

	public TreeListItemView()
	{
		double num = 18.0;
		double num2 = 18.0;
		double num3 = num2 / 2.0;
		double num4 = num / 2.0;
		cv_main = new Canvas();
		ln_left = new Line
		{
			X1 = num4,
			X2 = num + num4,
			Y1 = num3,
			Y2 = num3
		};
		ln_down = new Line
		{
			X1 = num + num4,
			X2 = num + num4,
			Y1 = num3
		};
		rn_rect = new Rectangle
		{
			Width = num - 8.0,
			Height = num2 - 8.0
		};
		ln_recth = new Line
		{
			X1 = 6.0,
			X2 = num - 4.0 - 2.0,
			Y1 = num3,
			Y2 = num3
		};
		ln_rectv = new Line
		{
			Y1 = 6.0,
			Y2 = num2 - 4.0 - 2.0,
			X1 = num4,
			X2 = num4
		};
		ui_icon = null;
		ui_header = null;
		tb_header = new TextBlock();
		LineInitialize(ln_left, 0);
		LineInitialize(ln_down, 0);
		LineInitialize(ln_recth, 1);
		LineInitialize(ln_rectv, 1);
		RectInitialize(rn_rect, 1);
		Canvas.SetTop(rn_rect, 4.0);
		Canvas.SetLeft(rn_rect, 4.0);
		Canvas.SetTop(tb_header, 0.0);
		Canvas.SetLeft(tb_header, num * 2.0 + 2.0);
		Panel.SetZIndex(ln_left, 0);
		Panel.SetZIndex(ln_down, 0);
		Panel.SetZIndex(rn_rect, 1);
		Panel.SetZIndex(ln_recth, 2);
		Panel.SetZIndex(ln_rectv, 2);
		cv_main.Children.Add(ln_left);
		cv_main.Children.Add(ln_down);
		cv_main.Children.Add(rn_rect);
		cv_main.Children.Add(ln_recth);
		cv_main.Children.Add(ln_rectv);
		cv_main.Children.Add(tb_header);
		base.Width = num;
		base.Height = num2;
		base.Content = cv_main;
		base.HorizontalAlignment = HorizontalAlignment.Left;
		base.VerticalAlignment = VerticalAlignment.Stretch;
		base.Background = Brushes.Transparent;
	}

	protected void LineInitialize(Line ln, int type)
	{
		switch (type)
		{
		case 0:
			ln.Stroke = LineStroke;
			ln.StrokeThickness = LineThickness;
			ln.StrokeDashCap = PenLineCap.Flat;
			ln.StrokeDashOffset = 0.0;
			ln.StrokeDashArray = new DoubleCollection { 1.0, 1.0 };
			break;
		case 1:
			ln.Stroke = RectStroke;
			ln.StrokeThickness = RectThickness;
			break;
		}
	}

	protected void RectInitialize(Rectangle rn, int type)
	{
		if (type == 1)
		{
			rn.Stroke = RectStroke;
			rn.StrokeThickness = RectThickness;
			rn.Fill = RectFill;
		}
	}

	protected void ItemInstall(ITreeListItem item)
	{
		double num = 18.0;
		double num2 = 18.0;
		double num3 = num2 / 2.0;
		double num4 = num / 2.0;
		parent = item.Root;
		ui_header = ((item.Ctx.Header is UIElement) ? ((UIElement)item.Ctx.Header) : tb_header);
		ui_icon = item.Ctx.GetIcon();
		cv_main.Margin = new Thickness(num * (double)item.Level, 0.0, 0.0, 0.0);
		base.Width = num * (double)item.Level + 80.0;
		if (ui_header != tb_header)
		{
			Canvas.SetLeft(ui_header, Canvas.GetLeft(tb_header));
			Canvas.SetTop(ui_header, Canvas.GetTop(tb_header));
			cv_main.Children.Add(ui_header);
			ui_header.Visibility = Visibility.Visible;
			tb_header.Visibility = Visibility.Hidden;
		}
		else
		{
			tb_header.Text = item.Ctx.Header.ToString();
			tb_header.Visibility = Visibility.Visible;
		}
		if (ui_icon != null)
		{
			Canvas.SetLeft(ui_icon, num);
			Canvas.SetTop(ui_icon, 0.0);
			cv_main.Children.Add(ui_icon);
		}
		ln_left.Visibility = Visibility.Visible;
		if (item.IsDirectory && item.Count > 0)
		{
			rn_rect.Visibility = Visibility.Visible;
			ln_recth.Visibility = Visibility.Visible;
			ln_rectv.Visibility = (item.IsExpand ? Visibility.Hidden : Visibility.Visible);
		}
		else
		{
			rn_rect.Visibility = Visibility.Hidden;
			ln_recth.Visibility = Visibility.Hidden;
			ln_rectv.Visibility = Visibility.Hidden;
		}
		if (ui_header is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)ui_header;
			frameworkElement.SizeChanged += OnHeaderSizeChanged;
		}
		if (ui_icon is FrameworkElement)
		{
			FrameworkElement frameworkElement2 = (FrameworkElement)ui_icon;
			frameworkElement2.SizeChanged += OnIconSizeChanged;
		}
		if (item is TreeListItem)
		{
			TreeListItem treeListItem = (TreeListItem)item;
			treeListItem.PropertyChanged += OnItemPropertyChanged;
		}
		ItemSelectUpdate();
		ItemHeightUpdate();
	}

	protected void ItemUninstall(ITreeListItem item)
	{
		if (item is TreeListItem)
		{
			TreeListItem treeListItem = (TreeListItem)item;
			treeListItem.PropertyChanged -= OnItemPropertyChanged;
		}
		if (ui_icon is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)ui_icon;
			frameworkElement.SizeChanged -= OnIconSizeChanged;
		}
		if (ui_header is FrameworkElement)
		{
			FrameworkElement frameworkElement2 = (FrameworkElement)ui_header;
			frameworkElement2.SizeChanged -= OnHeaderSizeChanged;
		}
		if (ui_icon != null)
		{
			cv_main.Children.Remove(ui_icon);
		}
		if (ui_header != tb_header)
		{
			cv_main.Children.Remove(ui_header);
		}
		ln_down.Visibility = Visibility.Hidden;
		ln_left.Visibility = Visibility.Hidden;
		rn_rect.Visibility = Visibility.Hidden;
		ln_recth.Visibility = Visibility.Hidden;
		ln_rectv.Visibility = Visibility.Hidden;
		tb_header.Visibility = Visibility.Hidden;
		parent = null;
		ui_header = null;
		ui_icon = null;
	}

	protected void ItemHeaderUpdate()
	{
		if (Item != null && ui_header is TextBlock)
		{
			TextBlock textBlock = (TextBlock)ui_header;
			textBlock.Text = Item.Ctx.Header.ToString();
		}
	}

	protected void ItemSelectUpdate()
	{
		if (ui_header is TextBlock)
		{
			TextBlock textBlock = (TextBlock)ui_header;
			textBlock.Background = (Item.IsSelected ? Item.Root.SelectedBackground : null);
			textBlock.Foreground = (Item.IsSelected ? Item.Root.SelectedForeground : Brushes.Black);
		}
	}

	protected void ItemHeightUpdate()
	{
		if (Item != null)
		{
			double num = 18.0;
			double num2 = 18.0;
			double num3 = num2 / 2.0;
			double num4 = num / 2.0;
			int num5 = Item.Height - (Item.LastOrDefault()?.Height ?? 1) + 1;
			if (num5 > 1)
			{
				ln_down.Visibility = Visibility.Visible;
				ln_down.Y2 = num3 + num2 * (double)(num5 - 1);
			}
			else
			{
				ln_down.Visibility = Visibility.Hidden;
			}
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (e.OldValue is ITreeListItem)
			{
				ItemUninstall((ITreeListItem)e.OldValue);
			}
			if (e.NewValue is ITreeListItem)
			{
				ItemInstall((ITreeListItem)e.NewValue);
			}
		}
	}

	protected override void OnRender(DrawingContext ctx)
	{
	}

	private void OnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
	{
		double num = 18.0;
		double num2 = 18.0;
		UIElement element = (UIElement)sender;
		Canvas.SetTop(element, Math.Max(0.0, (num2 - e.NewSize.Height) / 2.0));
		base.Width = num * (double)Item.Level + 4.0 + e.NewSize.Width;
	}

	private void OnIconSizeChanged(object sender, SizeChangedEventArgs e)
	{
		double num = 18.0;
		double num2 = 18.0;
		UIElement element = (UIElement)sender;
		Canvas.SetTop(element, Math.Max(0.0, (num2 - e.NewSize.Height) / 2.0));
		Canvas.SetLeft(element, num + Math.Max(0.0, (num - e.NewSize.Width) / 2.0));
	}

	private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "IsSelected":
			ItemSelectUpdate();
			break;
		case "Height":
			ItemHeightUpdate();
			break;
		case "Header":
			ItemHeaderUpdate();
			break;
		}
	}

	protected override void OnMouseDown(MouseButtonEventArgs e)
	{
		base.OnMouseDown(e);
		Point position = e.GetPosition(cv_main);
		if (position.X >= 0.0 && position.X <= 18.0)
		{
			Item.Root.ExpandChange((TreeListItem)Item);
		}
		else if (Keyboard.PrimaryDevice.Modifiers == ModifierKeys.Control)
		{
			Item.Root.SelectCtrl((TreeListItem)Item);
		}
		else if (Keyboard.PrimaryDevice.Modifiers == ModifierKeys.Shift)
		{
			Item.Root.SelectShift((TreeListItem)Item);
		}
		else
		{
			Item.Root.SelectSingle((TreeListItem)Item);
		}
	}
}
