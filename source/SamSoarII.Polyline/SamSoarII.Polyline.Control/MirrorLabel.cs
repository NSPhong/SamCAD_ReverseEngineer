using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace SamSoarII.Polyline.Control;

public class MirrorLabel : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Point), typeof(MirrorLabel), new PropertyMetadata(default(Point), OnPropertyChanged_From));

	protected static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Point), typeof(MirrorLabel), new PropertyMetadata(default(Point), OnPropertyChanged_To));

	private PolylineEditorPanel parent;

	internal RotateTransform Tran;

	private bool _contentLoaded;

	public Point From
	{
		get
		{
			return (Point)GetValue(FromProperty);
		}
		set
		{
			SetValue(FromProperty, value);
		}
	}

	public Point To
	{
		get
		{
			return (Point)GetValue(ToProperty);
		}
		set
		{
			SetValue(ToProperty, value);
		}
	}

	public MirrorLabel(PolylineEditorPanel _parent)
	{
		InitializeComponent();
		parent = _parent;
	}

	private static void OnPropertyChanged_From(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is MirrorLabel)
		{
			((MirrorLabel)d).OnFromChanged(e);
		}
	}

	protected virtual void OnFromChanged(DependencyPropertyChangedEventArgs e)
	{
		Update();
	}

	private static void OnPropertyChanged_To(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is MirrorLabel)
		{
			((MirrorLabel)d).OnToChanged(e);
		}
	}

	protected virtual void OnToChanged(DependencyPropertyChangedEventArgs e)
	{
		Update();
	}

	public void Begin()
	{
		base.Visibility = Visibility.Visible;
	}

	public void End()
	{
		base.Visibility = Visibility.Hidden;
	}

	public void Update()
	{
		Point visualPosition = parent.GetVisualPosition(From);
		Point visualPosition2 = parent.GetVisualPosition(To);
		Vector vector = visualPosition2 - visualPosition;
		Canvas.SetLeft(this, visualPosition.X);
		Canvas.SetTop(this, visualPosition.Y - 24.0);
		base.Width = vector.Length;
		Tran.Angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/mirrorlabel.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			Tran = (RotateTransform)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
