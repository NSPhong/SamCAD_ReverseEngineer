using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Dock.Float;

internal class FloatWindowResizer : Border
{
	public static readonly double Thickness = 6.0;

	private UserControl content;

	private FloatWindow target;

	private Point start;

	private Thickness offset;

	public double ContentWidth => content.Width;

	public double ContentHeight => content.Height;

	public FloatWindow Target => target;

	public Point StartPoint => start;

	public Thickness Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
			FrameworkElement frameworkElement = (FrameworkElement)target.ViewContent;
			Canvas.SetTop(this, start.Y - offset.Top);
			Canvas.SetLeft(this, start.X - offset.Left);
			content.Height = frameworkElement.ActualHeight + offset.Bottom + offset.Top;
			content.Width = frameworkElement.ActualWidth + offset.Right + offset.Left;
		}
	}

	public FloatWindowResizer()
	{
		content = new UserControl();
		base.BorderBrush = new SolidColorBrush(new Color
		{
			A = 96,
			R = 0,
			G = 0,
			B = 0
		});
		base.BorderThickness = new Thickness(Thickness);
		base.Background = Brushes.Transparent;
		content.Background = Brushes.Transparent;
		Child = content;
	}

	public void Start(FloatWindow _target, Point _start)
	{
		if (target == null && _target.ViewContent is FrameworkElement)
		{
			target = _target;
			start = _start;
			FrameworkElement frameworkElement = (FrameworkElement)target.ViewContent;
			Offset = new Thickness(0.0);
			base.Visibility = Visibility.Visible;
		}
	}

	public void End()
	{
		if (target != null)
		{
			FrameworkElement frameworkElement = (FrameworkElement)target.ViewContent;
			target.Top -= offset.Top;
			target.Left -= offset.Left;
			target.Height = target.Height + offset.Top + offset.Bottom;
			target.Width = target.Width + offset.Left + offset.Right;
			target = null;
			base.Visibility = Visibility.Hidden;
		}
	}
}
