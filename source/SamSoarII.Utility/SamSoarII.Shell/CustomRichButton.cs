using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class CustomRichButton : Button
{
	public static readonly DependencyProperty NormalContentProperty = DependencyProperty.Register("NormalContent", typeof(object), typeof(CustomRichButton), new FrameworkPropertyMetadata(null, OnNormalContentPropertyChanged));

	public static readonly DependencyProperty MouseOverContentProperty = DependencyProperty.Register("MouseOverContent", typeof(object), typeof(CustomRichButton), new FrameworkPropertyMetadata(null, OnMouseOverContentPropertyChanged));

	public static readonly DependencyProperty MouseDownContentProperty = DependencyProperty.Register("MouseDownContent", typeof(object), typeof(CustomRichButton), new FrameworkPropertyMetadata(null, OnMouseDownContentPropertyChanged));

	public static readonly DependencyProperty DisabledContentProperty = DependencyProperty.Register("DisabledContent", typeof(object), typeof(CustomRichButton), new FrameworkPropertyMetadata(null, OnDisabledContentPropertyChanged));

	public static readonly DependencyProperty UnderlineContentProperty = DependencyProperty.Register("UnderlineContent", typeof(object), typeof(CustomRichButton), new FrameworkPropertyMetadata(null, OnUnderlineContentPropertyChanged));

	public static readonly DependencyProperty IsFadeableProperty = DependencyProperty.Register("IsFadeable", typeof(bool), typeof(CustomRichButton), new FrameworkPropertyMetadata(true, OnUnderlineContentPropertyChanged));

	public object NormalContent
	{
		get
		{
			return GetValue(NormalContentProperty);
		}
		set
		{
			SetValue(NormalContentProperty, value);
		}
	}

	public object MouseOverContent
	{
		get
		{
			return GetValue(MouseOverContentProperty);
		}
		set
		{
			SetValue(MouseOverContentProperty, value);
		}
	}

	public object MouseDownContent
	{
		get
		{
			return GetValue(MouseDownContentProperty);
		}
		set
		{
			SetValue(MouseDownContentProperty, value);
		}
	}

	public object DisabledContent
	{
		get
		{
			return GetValue(DisabledContentProperty);
		}
		set
		{
			SetValue(DisabledContentProperty, value);
		}
	}

	public object UnderlineContent
	{
		get
		{
			return GetValue(UnderlineContentProperty);
		}
		set
		{
			SetValue(UnderlineContentProperty, value);
		}
	}

	public bool IsFadeable
	{
		get
		{
			return (bool)GetValue(IsFadeableProperty);
		}
		set
		{
			SetValue(IsFadeableProperty, value);
		}
	}

	public event DependencyPropertyChangedEventHandler NormalContentChanged;

	public CustomRichButton()
	{
		base.Background = Brushes.Transparent;
	}

	protected static void OnNormalContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is CustomRichButton)
		{
			CustomRichButton customRichButton = (CustomRichButton)sender;
			customRichButton.OnNormalContentPropertyChanged(e);
		}
	}

	protected virtual void OnNormalContentPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		this.NormalContentChanged?.Invoke(this, e);
	}

	protected static void OnMouseOverContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
	}

	protected static void OnMouseDownContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
	}

	protected static void OnDisabledContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
	}

	protected static void OnUnderlineContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
	}

	protected void UpdateContent()
	{
		if (!base.IsEnabled)
		{
			base.Content = ((DisabledContent != null) ? DisabledContent : NormalContent);
		}
		else if (base.IsPressed)
		{
			base.Content = ((MouseDownContent != null) ? MouseDownContent : NormalContent);
		}
		else if (base.IsMouseOver)
		{
			base.Content = ((MouseOverContent != null) ? MouseOverContent : NormalContent);
		}
		else
		{
			base.Content = NormalContent;
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == UIElement.IsMouseOverProperty)
		{
			UpdateContent();
		}
		if (e.Property == ButtonBase.IsPressedProperty)
		{
			UpdateContent();
		}
		if (e.Property == UIElement.IsEnabledProperty)
		{
			UpdateContent();
		}
		if (e.Property == NormalContentProperty)
		{
			UpdateContent();
		}
		if (e.Property == MouseOverContentProperty)
		{
			UpdateContent();
		}
		if (e.Property == MouseDownContentProperty)
		{
			UpdateContent();
		}
		if (e.Property == DisabledContentProperty)
		{
			UpdateContent();
		}
	}
}
