using System.Windows;
using System.Windows.Controls;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridCheckBox : CheckBox, IPolylineDataGridControl
{
	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IPolylineUserFormat), typeof(PolylineDataGridCheckBox), new PropertyMetadata(null, OnPropertyChanged_Core));

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

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridCheckBox)
		{
			((PolylineDataGridCheckBox)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void Read(string v)
	{
		bool result = base.IsChecked.HasValue && base.IsChecked.Value;
		bool.TryParse(v, out result);
		base.IsChecked = result;
	}

	public void Write()
	{
		PolylineDataGridHelper.Write(this, $"{base.IsChecked.HasValue && base.IsChecked.Value}");
	}

	public void Select()
	{
	}

	public void Unselect()
	{
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		PolylineDataGridHelper.HandlePropertyChanged(this, e);
	}

	protected override void OnChecked(RoutedEventArgs e)
	{
		base.OnChecked(e);
		Write();
	}

	protected override void OnUnchecked(RoutedEventArgs e)
	{
		base.OnUnchecked(e);
		Write();
	}
}
