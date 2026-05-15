#define DEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using Standard;

namespace Microsoft.Windows.Shell;

public class WindowChrome : Freezable
{
	private struct _SystemParameterBoundProperty
	{
		public string SystemParameterPropertyName { get; set; }

		public DependencyProperty DependencyProperty { get; set; }
	}

	public static readonly DependencyProperty WindowChromeProperty = DependencyProperty.RegisterAttached("WindowChrome", typeof(WindowChrome), typeof(WindowChrome), new PropertyMetadata(null, _OnChromeChanged));

	public static readonly DependencyProperty IsHitTestVisibleInChromeProperty = DependencyProperty.RegisterAttached("IsHitTestVisibleInChrome", typeof(bool), typeof(WindowChrome), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

	public static readonly DependencyProperty CaptionHeightProperty = DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowChrome), new PropertyMetadata(0.0, delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((WindowChrome)d)._OnPropertyChangedThatRequiresRepaint();
	}), (object value) => (double)value >= 0.0);

	public static readonly DependencyProperty ResizeBorderThicknessProperty = DependencyProperty.Register("ResizeBorderThickness", typeof(Thickness), typeof(WindowChrome), new PropertyMetadata(default(Thickness)), (object value) => Standard.Utility.IsThicknessNonNegative((Thickness)value));

	public static readonly DependencyProperty GlassFrameThicknessProperty = DependencyProperty.Register("GlassFrameThickness", typeof(Thickness), typeof(WindowChrome), new PropertyMetadata(default(Thickness), delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((WindowChrome)d)._OnPropertyChangedThatRequiresRepaint();
	}, (DependencyObject d, object o) => _CoerceGlassFrameThickness((Thickness)o)));

	public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WindowChrome), new PropertyMetadata(default(CornerRadius), delegate(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		((WindowChrome)d)._OnPropertyChangedThatRequiresRepaint();
	}), (object value) => Standard.Utility.IsCornerRadiusValid((CornerRadius)value));

	private static readonly List<_SystemParameterBoundProperty> _BoundProperties = new List<_SystemParameterBoundProperty>
	{
		new _SystemParameterBoundProperty
		{
			DependencyProperty = CornerRadiusProperty,
			SystemParameterPropertyName = "WindowCornerRadius"
		},
		new _SystemParameterBoundProperty
		{
			DependencyProperty = CaptionHeightProperty,
			SystemParameterPropertyName = "WindowCaptionHeight"
		},
		new _SystemParameterBoundProperty
		{
			DependencyProperty = ResizeBorderThicknessProperty,
			SystemParameterPropertyName = "WindowResizeBorderThickness"
		},
		new _SystemParameterBoundProperty
		{
			DependencyProperty = GlassFrameThicknessProperty,
			SystemParameterPropertyName = "WindowNonClientFrameThickness"
		}
	};

	public static Thickness GlassFrameCompleteThickness => new Thickness(-1.0);

	public double CaptionHeight
	{
		get
		{
			return (double)GetValue(CaptionHeightProperty);
		}
		set
		{
			SetValue(CaptionHeightProperty, value);
		}
	}

	public Thickness ResizeBorderThickness
	{
		get
		{
			return (Thickness)GetValue(ResizeBorderThicknessProperty);
		}
		set
		{
			SetValue(ResizeBorderThicknessProperty, value);
		}
	}

	public Thickness GlassFrameThickness
	{
		get
		{
			return (Thickness)GetValue(GlassFrameThicknessProperty);
		}
		set
		{
			SetValue(GlassFrameThicknessProperty, value);
		}
	}

	public CornerRadius CornerRadius
	{
		get
		{
			return (CornerRadius)GetValue(CornerRadiusProperty);
		}
		set
		{
			SetValue(CornerRadiusProperty, value);
		}
	}

	public bool ShowSystemMenu { get; set; }

	internal event EventHandler PropertyChangedThatRequiresRepaint;

	private static void _OnChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (!DesignerProperties.GetIsInDesignMode(d))
		{
			Window window = (Window)d;
			WindowChrome windowChrome = (WindowChrome)e.NewValue;
			Standard.Assert.IsNotNull(window);
			WindowChromeWorker windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(window);
			if (windowChromeWorker == null)
			{
				windowChromeWorker = new WindowChromeWorker();
				WindowChromeWorker.SetWindowChromeWorker(window, windowChromeWorker);
			}
			windowChromeWorker.SetWindowChrome(windowChrome);
		}
	}

	public static WindowChrome GetWindowChrome(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		return (WindowChrome)window.GetValue(WindowChromeProperty);
	}

	public static void SetWindowChrome(Window window, WindowChrome chrome)
	{
		Standard.Verify.IsNotNull(window, "window");
		window.SetValue(WindowChromeProperty, chrome);
	}

	public static bool GetIsHitTestVisibleInChrome(IInputElement inputElement)
	{
		Standard.Verify.IsNotNull(inputElement, "inputElement");
		if (!(inputElement is DependencyObject dependencyObject))
		{
			throw new ArgumentException("The element must be a DependencyObject", "inputElement");
		}
		return (bool)dependencyObject.GetValue(IsHitTestVisibleInChromeProperty);
	}

	public static void SetIsHitTestVisibleInChrome(IInputElement inputElement, bool hitTestVisible)
	{
		Standard.Verify.IsNotNull(inputElement, "inputElement");
		if (!(inputElement is DependencyObject dependencyObject))
		{
			throw new ArgumentException("The element must be a DependencyObject", "inputElement");
		}
		dependencyObject.SetValue(IsHitTestVisibleInChromeProperty, hitTestVisible);
	}

	private static object _CoerceGlassFrameThickness(Thickness thickness)
	{
		if (!Standard.Utility.IsThicknessNonNegative(thickness))
		{
			return GlassFrameCompleteThickness;
		}
		return thickness;
	}

	protected override Freezable CreateInstanceCore()
	{
		return new WindowChrome();
	}

	public WindowChrome()
	{
		foreach (_SystemParameterBoundProperty boundProperty in _BoundProperties)
		{
			Standard.Assert.IsNotNull(boundProperty.DependencyProperty);
			BindingOperations.SetBinding(this, boundProperty.DependencyProperty, new Binding
			{
				Source = SystemParameters2.Current,
				Path = new PropertyPath(boundProperty.SystemParameterPropertyName),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			});
		}
	}

	private void _OnPropertyChangedThatRequiresRepaint()
	{
		this.PropertyChangedThatRequiresRepaint?.Invoke(this, EventArgs.Empty);
	}
}
