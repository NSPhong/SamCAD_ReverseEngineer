using System;
using System.Windows;

namespace SamSoarII.Polyline.Control;

public class MousePositionEventArgs : EventArgs
{
	private Point logicalposition;

	private Point visualposition;

	public Point LogicalPosition => logicalposition;

	public Point VisualPosition => visualposition;

	public MousePositionEventArgs(Point _logicalposition, Point _visualposition)
	{
		logicalposition = _logicalposition;
		visualposition = _visualposition;
	}
}
