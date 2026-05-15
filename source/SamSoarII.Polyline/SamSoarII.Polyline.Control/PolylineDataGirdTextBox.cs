using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGirdTextBox : TextBox, IPolylineDataGridControl
{
	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IPolylineUserFormat), typeof(PolylineDataGirdTextBox), new PropertyMetadata(null, OnPropertyChanged_Core));

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
		if (d is PolylineDataGirdTextBox)
		{
			((PolylineDataGirdTextBox)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void Read(string v)
	{
		base.Text = v;
	}

	public void Write()
	{
		PolylineDataGridHelper.Write(this, base.Text);
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

	protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		base.OnGotKeyboardFocus(e);
	}
}
