using System;
using System.Windows;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PointWidgetEventArgs : EventArgs
{
	private Point oldpoint;

	private Point newpoint;

	private ChangedFlags flag;

	public Point OldPoint => oldpoint;

	public Point NewPoint => newpoint;

	public ChangedFlags Flag => flag;

	public PointWidgetEventArgs(Point _oldpoint, Point _newpoint, ChangedFlags _flag)
	{
		oldpoint = _oldpoint;
		newpoint = _newpoint;
		flag = _flag;
	}
}
