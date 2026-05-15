using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PolylineEditorPointView : UserControl, IPolylineControlPointView
{
	private static readonly Brush Background_MouseOver = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 82,
		G = 18,
		B = 24
	});

	private static readonly Brush Background_Nearest = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 147,
		G = 129,
		B = 132
	});

	private static readonly Brush Background_Selected = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 195,
		G = 177,
		B = 182
	});

	private static readonly Brush Stroke_MouseOver = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 192,
		G = 192,
		B = 192
	});

	private static readonly Brush Stroke_Nearest = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	private static readonly Brush Stroke_Selected = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IPolylineControlPoint), typeof(PolylineEditorPointView), new PropertyMetadata(null, OnPropertyChanged_Core));

	private Border border;

	private PolylineEditorPanel parent;

	private bool isnearest;

	public IPolylineControlPoint Core
	{
		get
		{
			return (IPolylineControlPoint)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	public PolylineEditorPanel ViewParent => parent;

	private IPolylineEntity Entity => Core?.Parent;

	public bool IsNearest
	{
		get
		{
			return isnearest;
		}
		set
		{
			isnearest = value;
			InvalidateVisual();
		}
	}

	public PolylineEditorPointView(PolylineEditorPanel _parent)
	{
		border = new Border();
		parent = _parent;
		isnearest = false;
		base.Width = 8.0;
		base.Height = 8.0;
		base.Background = Brushes.White;
		base.Content = border;
		InvalidateVisual();
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPointView)
		{
			((PolylineEditorPointView)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineControlPoint && e.OldValue != null)
		{
			IPolylineControlPoint polylineControlPoint = (IPolylineControlPoint)e.OldValue;
			polylineControlPoint.Parent.PropertyChanged -= OnEntityPropertyChanged;
			if (polylineControlPoint.View != null)
			{
				polylineControlPoint.View = null;
			}
		}
		if (e.NewValue is IPolylineControlPoint && e.NewValue != null)
		{
			IPolylineControlPoint polylineControlPoint2 = (IPolylineControlPoint)e.NewValue;
			polylineControlPoint2.Parent.PropertyChanged += OnEntityPropertyChanged;
			if (polylineControlPoint2.View != this)
			{
				polylineControlPoint2.View = this;
			}
			UpdatePosition();
		}
		base.Visibility = ((Core == null) ? Visibility.Hidden : Visibility.Visible);
	}

	public new void InvalidateVisual()
	{
		if (Core != null && Entity != null)
		{
			if (Entity.IsSelected)
			{
				base.Background = Background_Selected;
				base.BorderBrush = Stroke_Selected;
				base.BorderThickness = new Thickness(2.0);
			}
			else if (isnearest)
			{
				base.Background = Background_Nearest;
				base.BorderBrush = Stroke_Nearest;
				base.BorderThickness = new Thickness(1.0);
			}
			else if (Entity.IsMouseOver)
			{
				base.Background = Background_MouseOver;
				base.BorderBrush = Stroke_MouseOver;
				base.BorderThickness = new Thickness(1.0);
			}
		}
	}

	public void UpdatePosition()
	{
		if (Core != null)
		{
			Point visualPosition = parent.GetVisualPosition(Core.Point);
			Canvas.SetTop(this, visualPosition.Y - base.Height / 2.0);
			Canvas.SetLeft(this, visualPosition.X - base.Width / 2.0);
		}
	}

	private void OnEntityPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "IsMouseOver":
		case "IsSelected":
			InvalidateVisual();
			break;
		case "To":
			if (Core.ID == 1)
			{
				UpdatePosition();
			}
			break;
		case "Center":
			if (Core.ID == 2)
			{
				UpdatePosition();
			}
			break;
		}
	}
}
