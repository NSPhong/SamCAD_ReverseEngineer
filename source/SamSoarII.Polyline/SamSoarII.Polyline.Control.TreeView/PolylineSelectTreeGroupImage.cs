using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control.TreeView;

public class PolylineSelectTreeGroupImage : UserControl, IComponentConnector
{
	protected static readonly double DefaultWidth = 60.0;

	protected static readonly double DefaultHeight = 48.0;

	protected static readonly DependencyProperty GroupProperty = DependencyProperty.Register("Group", typeof(IPolylineGroup), typeof(PolylineSelectTreeGroupImage), new PropertyMetadata(null, OnPropertyChanged_Items));

	private bool _contentLoaded;

	public IPolylineGroup Group
	{
		get
		{
			return (IPolylineGroup)GetValue(GroupProperty);
		}
		set
		{
			SetValue(GroupProperty, value);
		}
	}

	public PolylineSelectTreeGroupImage()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Items(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectTreeGroupImage)
		{
			((PolylineSelectTreeGroupImage)d).OnGroupChanged(e);
		}
	}

	protected virtual void OnGroupChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	protected Point ToVisual(Point p, Rect dim)
	{
		return new Point((p.X - dim.X) * DefaultWidth / dim.Width, DefaultHeight - (p.Y - dim.Y) * DefaultHeight / dim.Height);
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
		if (Group == null || Group.Count == 0)
		{
			return;
		}
		IPolylineImage parent = Group.Parent;
		Rect bounding = parent.Items[Group.Start].Bounding;
		Pen pen = new Pen(Brushes.White, 1.0);
		StreamGeometry streamGeometry = new StreamGeometry();
		for (int i = Group.Start + 1; i < Group.Start + Group.Count; i++)
		{
			IPolylineEntity polylineEntity = parent.Items[i];
			bounding.Union(polylineEntity.Bounding);
		}
		bounding.X -= bounding.Width * 0.05;
		bounding.Y -= bounding.Height * 0.05;
		bounding.Width += bounding.Width * 0.1;
		bounding.Height += bounding.Height * 0.1;
		if (bounding.Width / DefaultWidth > bounding.Height / DefaultHeight)
		{
			bounding.Height = bounding.Width / DefaultWidth * DefaultHeight;
		}
		else
		{
			bounding.Width = bounding.Height / DefaultHeight * DefaultWidth;
		}
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			for (int j = Group.Start; j < Group.Start + Group.Count; j++)
			{
				IPolylineEntity polylineEntity2 = parent.Items[j];
				if (j == Group.Start)
				{
					streamGeometryContext.BeginFigure(ToVisual(polylineEntity2.From, bounding), isFilled: false, isClosed: false);
				}
				if (polylineEntity2 is IPolylineLine)
				{
					IPolylineLine polylineLine = (IPolylineLine)polylineEntity2;
					streamGeometryContext.LineTo(ToVisual(polylineEntity2.To, bounding), isStroked: true, isSmoothJoin: false);
				}
				else if (polylineEntity2 is IPolylineArch)
				{
					IPolylineArch polylineArch = (IPolylineArch)polylineEntity2;
					double width = polylineArch.Radius * DefaultWidth / bounding.Width;
					double height = polylineArch.Radius * DefaultHeight / bounding.Height;
					streamGeometryContext.ArcTo(ToVisual(polylineEntity2.To, bounding), new Size(width, height), 0.0, polylineArch.IsLarge, polylineArch.IsClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
				}
				else if (polylineEntity2 is IPolylineCircle)
				{
					IPolylineCircle polylineCircle = (IPolylineCircle)polylineEntity2;
					Point center = ToVisual(polylineCircle.Center, bounding);
					double num = polylineCircle.Radius * DefaultWidth / bounding.Width;
					ctx.DrawEllipse(null, pen, center, num, num);
				}
			}
		}
		ctx.DrawGeometry(null, pen, streamGeometry);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/treeview/polylineselecttreegroupimage.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}
}
