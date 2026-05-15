using System.Windows;
using System.Windows.Controls.Primitives;

namespace SamSoarII.Shell;

public class CustomToggleButton : ToggleButton
{
	public static readonly DependencyProperty DefaultContentProperty;

	public static readonly DependencyProperty CheckedContentProperty;

	public object DefaultContent
	{
		get
		{
			return GetValue(DefaultContentProperty);
		}
		set
		{
			SetValue(DefaultContentProperty, value);
		}
	}

	public object CheckedContent
	{
		get
		{
			return GetValue(CheckedContentProperty);
		}
		set
		{
			SetValue(CheckedContentProperty, value);
		}
	}

	public event DependencyPropertyChangedEventHandler DefaultContentChanged;

	public event DependencyPropertyChangedEventHandler CheckedContentChanged;

	static CustomToggleButton()
	{
		DefaultContentProperty = DependencyProperty.Register("DefaultContent", typeof(object), typeof(CustomToggleButton), new FrameworkPropertyMetadata(null, OnDefaultContentPropertyChanged));
		CheckedContentProperty = DependencyProperty.Register("CheckedContent", typeof(object), typeof(CustomToggleButton), new FrameworkPropertyMetadata(null, OnCheckedContentPropertyChanged));
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomToggleButton), new FrameworkPropertyMetadata((object)null));
	}

	public CustomToggleButton()
	{
		base.Style = (Style)FindResource("CustomToggleButtonStyle");
	}

	protected static void OnDefaultContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CustomToggleButton)
		{
			((CustomToggleButton)sender).OnDefaultContentPropertyChanged(e);
		}
	}

	protected virtual void OnDefaultContentPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		this.DefaultContentChanged?.Invoke(this, e);
		UpdateContent();
	}

	protected static void OnCheckedContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CustomToggleButton)
		{
			((CustomToggleButton)sender).OnCheckedContentPropertyChanged(e);
		}
	}

	protected virtual void OnCheckedContentPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		this.CheckedContentChanged?.Invoke(this, e);
		UpdateContent();
	}

	protected void UpdateContent()
	{
		base.Content = ((base.IsChecked == true) ? CheckedContent : DefaultContent);
	}

	protected override void OnChecked(RoutedEventArgs e)
	{
		base.OnChecked(e);
		UpdateContent();
	}

	protected override void OnUnchecked(RoutedEventArgs e)
	{
		base.OnUnchecked(e);
		UpdateContent();
	}
}
