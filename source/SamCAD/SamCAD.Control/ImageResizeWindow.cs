using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class ImageResizeWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty AutoLeftProperty = DependencyProperty.Register("AutoLeft", typeof(bool), typeof(ImageResizeWindow), new PropertyMetadata(false, OnPropertyChanged_AutoLeft));

	protected static readonly DependencyProperty AutoRightProperty = DependencyProperty.Register("AutoRight", typeof(bool), typeof(ImageResizeWindow), new PropertyMetadata(false, OnPropertyChanged_AutoRight));

	protected static readonly DependencyProperty AutoTopProperty = DependencyProperty.Register("AutoTop", typeof(bool), typeof(ImageResizeWindow), new PropertyMetadata(false, OnPropertyChanged_AutoTop));

	protected static readonly DependencyProperty AutoBottomProperty = DependencyProperty.Register("AutoBottom", typeof(bool), typeof(ImageResizeWindow), new PropertyMetadata(false, OnPropertyChanged_AutoBottom));

	protected static readonly DependencyProperty VisibleAllProperty = DependencyProperty.Register("VisibleAll", typeof(bool), typeof(ImageResizeWindow), new PropertyMetadata(false, OnPropertyChanged_VisibleAll));

	private IPolylineImage image;

	public RoutedEventHandler Yes;

	public RoutedEventHandler No;

	internal ImageResizeWindow This;

	internal TextBox TX_X1;

	internal TextBox TX_X2;

	internal TextBox TX_Y1;

	internal TextBox TX_Y2;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public bool AutoLeft
	{
		get
		{
			return (bool)GetValue(AutoLeftProperty);
		}
		set
		{
			SetValue(AutoLeftProperty, value);
		}
	}

	public bool AutoRight
	{
		get
		{
			return (bool)GetValue(AutoRightProperty);
		}
		set
		{
			SetValue(AutoRightProperty, value);
		}
	}

	public bool AutoTop
	{
		get
		{
			return (bool)GetValue(AutoTopProperty);
		}
		set
		{
			SetValue(AutoTopProperty, value);
		}
	}

	public bool AutoBottom
	{
		get
		{
			return (bool)GetValue(AutoBottomProperty);
		}
		set
		{
			SetValue(AutoBottomProperty, value);
		}
	}

	public bool VisibleAll
	{
		get
		{
			return (bool)GetValue(VisibleAllProperty);
		}
		set
		{
			SetValue(VisibleAllProperty, value);
		}
	}

	public IPolylineImage Image => image;

	public Rect NewSize
	{
		get
		{
			double result = image.Left;
			double result2 = image.Top;
			double result3 = result + image.Width;
			double result4 = result2 + image.Height;
			double.TryParse(TX_X1.Text, out result);
			double.TryParse(TX_X2.Text, out result3);
			double.TryParse(TX_Y1.Text, out result2);
			double.TryParse(TX_Y2.Text, out result4);
			if (AutoLeft || AutoRight || AutoTop || AutoBottom || VisibleAll)
			{
				return App.Client.UI_Editor.GetBoundary(image, AutoLeft, AutoRight, AutoTop, AutoBottom, VisibleAll);
			}
			return new Rect(result, result2, result3 - result, result4 - result2);
		}
	}

	public ImageResizeWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_AutoLeft(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_AutoRight(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_AutoTop(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_AutoBottom(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_VisibleAll(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ImageResizeWindow)
		{
			((ImageResizeWindow)d).OnVisibleAllChanged(e);
		}
	}

	protected void OnVisibleAllChanged(DependencyPropertyChangedEventArgs e)
	{
		if (VisibleAll)
		{
			AutoLeft = true;
			AutoRight = true;
			AutoTop = true;
			AutoBottom = true;
		}
	}

	public void Begin(IPolylineImage _image)
	{
		image = _image;
		TX_X1.Text = image.Left.ToString();
		TX_X2.Text = (image.Left + image.Width).ToString();
		TX_Y1.Text = image.Top.ToString();
		TX_Y2.Text = (image.Top + image.Height).ToString();
	}

	public void End()
	{
		image = null;
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		No?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/imageresizewindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (ImageResizeWindow)target;
			break;
		case 2:
			TX_X1 = (TextBox)target;
			break;
		case 3:
			TX_X2 = (TextBox)target;
			break;
		case 4:
			TX_Y1 = (TextBox)target;
			break;
		case 5:
			TX_Y2 = (TextBox)target;
			break;
		case 6:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 7:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
