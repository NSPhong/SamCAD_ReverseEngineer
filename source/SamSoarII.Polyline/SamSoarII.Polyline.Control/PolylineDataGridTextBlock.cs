using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridTextBlock : Label, IPolylineDataGridControl
{
	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IPolylineUserFormat), typeof(PolylineDataGridTextBlock), new PropertyMetadata(null, OnPropertyChanged_Core));

	public IPolylineUserFormat Core
	{
		get
		{
			return (IPolylineUserFormat)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	public PolylineDataGridTextBlock()
	{
		base.Foreground = Brushes.Black;
		base.Margin = new Thickness(0.0, -5.0, 0.0, -5.0);
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridTextBlock)
		{
			((PolylineDataGridTextBlock)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void Read(string v)
	{
		base.Content = v;
	}

	public void Write()
	{
	}

	public void Select()
	{
		base.Foreground = Brushes.White;
	}

	public void Unselect()
	{
		base.Foreground = Brushes.Black;
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		PolylineDataGridHelper.HandlePropertyChanged(this, e);
	}
}
