using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineBSpline : PolylineEntity, IPolylineBSpline, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	private List<Point> points;

	private List<double> offsets;

	private List<double> weights;

	private List<Point> nodes;

	public override PolylineType Type => PolylineType.BSpline;

	public IList<Point> Points => points;

	public IList<double> Offsets => offsets;

	public IList<double> Weights => weights;

	public IList<Point> Nodes => nodes;

	public override Rect Bounding
	{
		get
		{
			if (nodes.Count() == 0)
			{
				return default(Rect);
			}
			Rect r = new Rect(nodes[0].X, nodes[0].Y, 0.0, 0.0);
			for (int i = 1; i < nodes.Count(); i++)
			{
				RectAdd(ref r, nodes[i]);
			}
			return r;
		}
	}

	public override IEnumerable<IGridPenningEntity> Pennings
	{
		get
		{
			int monox = ((nodes[1].X >= nodes[0].X) ? 1 : (-1));
			int monoy = ((nodes[1].Y >= nodes[0].Y) ? 1 : (-1));
			Rect rect = new Rect(nodes[0], nodes[1]);
			Point last = nodes[1];
			int start = 0;
			for (int i = 2; i < Nodes.Count(); i++)
			{
				Point next = nodes[i];
				int _monox = ((next.X >= last.X) ? 1 : (-1));
				int _monoy = ((next.Y >= last.Y) ? 1 : (-1));
				bool _diffx = _monox != monox;
				bool _diffy = _monoy != monoy;
				if (monox != 0 && monoy != 0)
				{
					if (_diffx && _diffy)
					{
						yield return _GetSegment(start, i - start + 1, monox, monoy, rect);
						monox = _monox;
						monoy = _monoy;
						rect = new Rect(last, next);
					}
					else if (_diffx)
					{
						monox = 0;
					}
					else if (_diffy)
					{
						monoy = 0;
					}
				}
				else if (monox == 0 && monoy != 0)
				{
					if (_diffy)
					{
						yield return _GetSegment(start, i - start + 1, monox, monoy, rect);
						monox = _monox;
						monoy = _monoy;
						rect = new Rect(last, next);
					}
				}
				else if (monox != 0 && monoy == 0 && _diffx)
				{
					yield return _GetSegment(start, i - start + 1, monox, monoy, rect);
					monox = _monox;
					monoy = _monoy;
					rect = new Rect(last, next);
				}
				RectAdd(ref rect, next);
				last = next;
			}
			if (start < nodes.Count())
			{
				yield return _GetSegment(start, nodes.Count() - start, monox, monoy, rect);
			}
		}
	}

	public PolylineBSpline()
	{
		points = new List<Point>();
		offsets = new List<double>();
		weights = new List<double>();
		nodes = new List<Point>();
	}

	public PolylineBSpline(IPolylineImage _parent, int _id, Point _to)
		: base(_parent, _id, _to)
	{
		points = new List<Point>();
		offsets = new List<double>();
		weights = new List<double>();
		nodes = new List<Point>();
	}

	protected override string _GetName()
	{
		return $"{base.ID}. Spline";
	}

	protected void RectAdd(ref Rect r, Point p)
	{
		if (p.X < r.X)
		{
			r.Width += r.X - p.X;
			r.X = p.X;
		}
		if (p.Y < r.Y)
		{
			r.Height += r.Y - p.Y;
			r.Y = p.Y;
		}
		if (p.X > r.Right)
		{
			r.Width += p.X - r.Right;
		}
		if (p.Y > r.Bottom)
		{
			r.Height += p.Y - r.Bottom;
		}
	}

	private PolylineBSplineSegment _GetSegment(int start, int count, int monox, int monoy, Rect rect)
	{
		if (monox != 0 && monoy != 0)
		{
			if (rect.Width >= rect.Height)
			{
				return new PolylineBSplineSegmentXY(this, start, count, monox, monoy);
			}
			return new PolylineBSplineSegmentYX(this, start, count, monox, monoy);
		}
		if (monox == 0 && monoy != 0)
		{
			return new PolylineBSplineSegmentYX(this, start, count, monox, monoy);
		}
		if (monox != 0 && monoy == 0)
		{
			return new PolylineBSplineSegmentXY(this, start, count, monox, monoy);
		}
		return null;
	}
}
