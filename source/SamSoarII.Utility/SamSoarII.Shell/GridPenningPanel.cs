using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SamSoarII.Shell;

public class GridPenningPanel : Canvas, IComponentConnector
{
	public static readonly uint PenThickness = 2u;

	public static readonly uint[] RGBA_Pens = new uint[8] { 4294967295u, 4294967040u, 4294902015u, 4278255615u, 4294901760u, 4278255360u, 4278190335u, 4286611584u };

	public static readonly int OneBoolWidth = 4;

	public static readonly int OneBoolHeight = 56;

	public static readonly int OneBoolMargin = 8;

	public static readonly int BitmapMaxWidth = 1600;

	public static readonly int BitmapMaxHeight = 900;

	public static readonly int CacheWidth = 16;

	public static readonly int CacheHeight = 16;

	public static readonly int XCacheCapacity = 200;

	public static readonly int YCacheCapacity = 100;

	private bool isdisposed = false;

	private GridPenning parent;

	private byte ba = byte.MaxValue;

	private byte br = 0;

	private byte bg = 0;

	private byte bb = 0;

	private byte gray_background = 0;

	private uint rgba_background = 4278190080u;

	private byte fa = byte.MaxValue;

	private byte fr = byte.MaxValue;

	private byte fg = byte.MaxValue;

	private byte fb = byte.MaxValue;

	private byte gray_foreground = byte.MaxValue;

	private uint rgba_foreground = uint.MaxValue;

	private Image image;

	private WriteableBitmap bitmap;

	private bool[,] isdirty;

	private bool[,] isexist;

	private byte[] buffer;

	private GridPenningPanelGlobalDrawing gbdraw;

	private bool isdrawing;

	private Rect vpzone;

	private Rect dwzone;

	internal GridPenningPanel This;

	private bool _contentLoaded;

	public static int XBlockCapacity => GridPenning.XBlockCapacity;

	public static int YBlockCapacity => GridPenning.YBlockCapacity;

	public static int BitmapStride => BitmapMaxWidth * 4;

	public bool IsDisposed => isdisposed;

	public GridPenning ViewParent => parent;

	public IGridPenningSource DrawingSource => parent?.DrawingSource;

	public double BlockWidth => parent.BlockWidth;

	public double BlockHeight => parent.BlockHeight;

	protected double XAccuracyLost => BlockWidth / 6400.0;

	protected double YAccuracyLost => BlockHeight / 6400.0;

	public uint RGBA_Background
	{
		get
		{
			return rgba_background;
		}
		set
		{
			rgba_background = value;
			ParseRGBA(rgba_background, ref ba, ref br, ref bg, ref bb, ref gray_background);
		}
	}

	public uint RGBA_Foreground
	{
		get
		{
			return rgba_foreground;
		}
		set
		{
			rgba_foreground = value;
			ParseRGBA(rgba_foreground, ref fa, ref fr, ref fg, ref fb, ref gray_foreground);
		}
	}

	public GridPenningPanel(GridPenning _parent)
	{
		InitializeComponent();
		parent = _parent;
		image = new Image
		{
			IsHitTestVisible = false
		};
		bitmap = new WriteableBitmap(BitmapMaxWidth, BitmapMaxHeight, 96.0, 96.0, PixelFormats.Bgra32, null);
		isdirty = new bool[XCacheCapacity, YCacheCapacity];
		isexist = new bool[XCacheCapacity, YCacheCapacity];
		buffer = new byte[BitmapMaxWidth * BitmapMaxHeight * 4];
		gbdraw = new GridPenningPanelGlobalDrawing(this);
		isdrawing = false;
		vpzone = default(Rect);
		dwzone = default(Rect);
		image.Source = bitmap;
		Array.Clear(isdirty, 0, isdirty.Length);
		Array.Clear(isexist, 0, isexist.Length);
		Array.Clear(buffer, 0, buffer.Length);
		Panel.SetZIndex(gbdraw, 100);
		Panel.SetZIndex(image, 200);
		base.Children.Add(gbdraw);
		base.Children.Add(image);
		parent.PropertyChanged += Parent_OnPropertyChanged;
	}

	public void Dispose()
	{
		parent.PropertyChanged -= Parent_OnPropertyChanged;
		parent = null;
	}

	protected void ParseRGBA(uint rgba, ref byte r, ref byte g, ref byte b, ref byte a, ref byte gray)
	{
		a = (byte)((rgba >> 24) & 0xFF);
		r = (byte)((rgba >> 16) & 0xFF);
		g = (byte)((rgba >> 8) & 0xFF);
		b = (byte)(rgba & 0xFF);
		gray = (byte)((uint)(r + g + b) / 3u);
	}

	protected uint ToRGBA(byte r, byte g, byte b, byte a)
	{
		return (uint)((a << 24) + (r << 16) + (g << 8) + b);
	}

	protected byte ChannelTransform(byte d1, byte d2, byte r)
	{
		if (d2 > d1)
		{
			return (byte)(d1 + (((d2 - d1) * r >>> 8) & 0xFF));
		}
		return (byte)(d2 - (((d1 - d2) * (byte)(~r) >>> 8) & 0xFF));
	}

	protected byte ChannelMix(byte d1, byte d2, byte d3, byte r)
	{
		if (d2 > d1)
		{
			return (byte)(d3 + (((d2 - d3) * r >>> 8) & 0xFF));
		}
		return (byte)(d3 - (((d1 - d3) * (byte)(~r) >>> 8) & 0xFF));
	}

	protected void GetPointAround(Point p, ref int xi, ref int yi, ref uint xl, ref uint xn, ref uint yl, ref uint yn)
	{
		xi = (int)p.X;
		yi = (int)p.Y;
		xl = (uint)(((double)(xi + 1) - p.X) * 4294967296.0);
		yl = (uint)(((double)(yi + 1) - p.Y) * 4294967296.0);
		xn = (uint)((p.X - (double)xi) * 4294967296.0);
		yn = (uint)((p.Y - (double)yi) * 4294967296.0);
	}

	protected void Swap(ref Point p1, ref Point p2)
	{
		Point point = p1;
		p1 = p2;
		p2 = point;
	}

	protected void Swap(ref double d1, ref double d2)
	{
		double num = d1;
		d1 = d2;
		d2 = num;
	}

	protected bool GetVisualX(double lx, ref double vx)
	{
		vx = (lx - vpzone.X) * base.ActualWidth / vpzone.Width;
		return lx >= vpzone.Left && lx <= vpzone.Right;
	}

	protected bool GetVisualY(double ly, ref double vy)
	{
		vy = (ly - vpzone.Y) * base.ActualHeight / vpzone.Height;
		return ly >= vpzone.Top && ly <= vpzone.Bottom;
	}

	protected bool GetVisualPoint(Point lp, ref Point vp)
	{
		if (!vpzone.Contains(lp))
		{
			return false;
		}
		vp.X = (lp.X - vpzone.X) * base.ActualWidth / vpzone.Width;
		vp.Y = (lp.Y - vpzone.Y) * base.ActualHeight / vpzone.Height;
		return true;
	}

	protected bool GetVisualRect(Rect lr, ref Rect vr)
	{
		Point vp = default(Point);
		Point vp2 = default(Point);
		if (!GetVisualPoint(lr.TopLeft, ref vp))
		{
			return false;
		}
		if (!GetVisualPoint(lr.BottomRight, ref vp2))
		{
			return false;
		}
		vr.X = vp.X;
		vr.Y = vp.Y;
		vr.Width = vp2.X - vp.X;
		vr.Height = vp2.Y - vp.Y;
		return true;
	}

	protected bool GetLogicalX(double vx, ref double lx)
	{
		if (vx < 0.0 || vx > base.ActualWidth)
		{
			return false;
		}
		lx = vpzone.X + vx * vpzone.Width / base.ActualWidth;
		return true;
	}

	protected bool GetLogicalY(double vy, ref double ly)
	{
		if (vy < 0.0 || vy > base.ActualHeight)
		{
			return false;
		}
		ly = vpzone.Y + vy * vpzone.Height / base.ActualHeight;
		return true;
	}

	protected bool GetLogicalPoint(Point vp, ref Point lp)
	{
		double lx = 0.0;
		double ly = 0.0;
		if (!GetLogicalX(vp.X, ref lx))
		{
			return false;
		}
		if (!GetLogicalY(vp.Y, ref ly))
		{
			return false;
		}
		lp.X = lx;
		lp.Y = ly;
		return true;
	}

	public Rect GetDrawingRectAll()
	{
		return new Rect(DrawingSource.XStart + parent.HorizontalOffset, DrawingSource.YStart + parent.VerticalOffset, parent.ViewportWidth, parent.ViewportHeight);
	}

	protected bool _ResizeLineInRect(ref Point start, ref Point end, Rect rect)
	{
		if (!rect.Contains(start))
		{
			double num = Math.Abs(end.X - start.X);
			double num2 = Math.Abs(end.Y - start.Y);
			if (start.X < rect.Left)
			{
				if (num < XAccuracyLost)
				{
					return false;
				}
				start += (end - start) * (rect.Left - start.X) / num;
			}
			if (start.X > rect.Right)
			{
				if (num < XAccuracyLost)
				{
					return false;
				}
				start += (end - start) * (start.X - rect.Right) / num;
			}
			if (start.Y < rect.Top)
			{
				if (num2 < YAccuracyLost)
				{
					return false;
				}
				start += (end - start) * (rect.Top - start.Y) / num2;
			}
			if (start.Y > rect.Bottom)
			{
				if (num2 < YAccuracyLost)
				{
					return false;
				}
				start += (end - start) * (start.Y - rect.Bottom) / num2;
			}
		}
		return true;
	}

	protected bool ResizeLineInRect(ref Point start, ref Point end, Rect rect)
	{
		if (!_ResizeLineInRect(ref start, ref end, rect))
		{
			return false;
		}
		Swap(ref start, ref end);
		if (!_ResizeLineInRect(ref start, ref end, rect))
		{
			return false;
		}
		Swap(ref start, ref end);
		return true;
	}

	public static bool GetXCross(IGridPenningCircle circle, double x, Rect rect, ref Point result)
	{
		double num = Math.Abs(x - circle.Center.X);
		double num2 = rect.Top + rect.Height / 2.0;
		if (num > circle.Radius)
		{
			return false;
		}
		double d = num / circle.Radius;
		double num3 = Math.Acos(d);
		if (x > circle.Center.X)
		{
			Vector v = new Vector(circle.Radius, 0.0);
			if (num2 < circle.Center.Y)
			{
				result = circle.Center + GridPenningArch.Rotate(v, num3);
			}
			else
			{
				result = circle.Center + GridPenningArch.Rotate(v, 0.0 - num3);
			}
			return true;
		}
		if (x < circle.Center.X)
		{
			Vector v2 = new Vector(0.0 - circle.Radius, 0.0);
			if (num2 < circle.Center.Y)
			{
				result = circle.Center + GridPenningArch.Rotate(v2, 0.0 - num3);
			}
			else
			{
				result = circle.Center + GridPenningArch.Rotate(v2, num3);
			}
			return true;
		}
		return false;
	}

	public static bool GetXCross(IGridPenningEllipse ellipse, double x, Rect rect, ref Point result)
	{
		return false;
	}

	public static bool GetYCross(IGridPenningCircle circle, double y, Rect rect, ref Point result)
	{
		double num = Math.Abs(y - circle.Center.Y);
		double num2 = rect.Left + rect.Width / 2.0;
		if (num > circle.Radius)
		{
			return false;
		}
		double d = num / circle.Radius;
		double num3 = Math.Acos(d);
		if (y > circle.Center.Y)
		{
			Vector v = new Vector(0.0, circle.Radius);
			if (num2 < circle.Center.X)
			{
				result = circle.Center + GridPenningArch.Rotate(v, 0.0 - num3);
			}
			else
			{
				result = circle.Center + GridPenningArch.Rotate(v, num3);
			}
			return true;
		}
		if (y < circle.Center.Y)
		{
			Vector v2 = new Vector(0.0, 0.0 - circle.Radius);
			if (num2 < circle.Center.X)
			{
				result = circle.Center + GridPenningArch.Rotate(v2, num3);
			}
			else
			{
				result = circle.Center + GridPenningArch.Rotate(v2, 0.0 - num3);
			}
			return true;
		}
		return false;
	}

	public static bool GetYCross(IGridPenningEllipse ellipse, double x, Rect rect, ref Point result)
	{
		return false;
	}

	protected bool BufferClearAll()
	{
		Array.Clear(buffer, 0, buffer.Length);
		for (int i = 0; i < XCacheCapacity; i++)
		{
			for (int j = 0; j < YCacheCapacity; j++)
			{
				isdirty[i, j] = isexist[i, j];
				isexist[i, j] = false;
			}
		}
		return true;
	}

	protected bool BufferClear(int x, int y, int width, int height)
	{
		int val = x / CacheWidth;
		int val2 = (x + width - 1) / CacheWidth;
		int val3 = y / CacheHeight;
		int val4 = (y + height - 1) / CacheHeight;
		val = Math.Max(val, 0);
		val2 = Math.Min(val2, XCacheCapacity - 1);
		val3 = Math.Max(val3, 0);
		val4 = Math.Min(val4, YCacheCapacity - 1);
		for (int i = y; i < y + height; i++)
		{
			int index = i * BitmapStride + x * 4;
			int length = width * 4;
			Array.Clear(buffer, index, length);
		}
		for (int j = val; j <= val2; j++)
		{
			for (int k = val3; k <= val4; k++)
			{
				isdirty[j, k] = isexist[j, k];
				if (j > val && j < val2 && k > val3 && k < val4)
				{
					isexist[j, k] = false;
				}
			}
		}
		return true;
	}

	protected bool BufferGet(int x, int y, ref byte r, ref byte g, ref byte b, ref byte a)
	{
		int num = y * BitmapStride + x * 4;
		b = buffer[num];
		g = buffer[num + 1];
		r = buffer[num + 2];
		a = buffer[num + 3];
		return true;
	}

	protected bool BufferSet(int x, int y, byte r, byte g, byte b, byte a)
	{
		int num = y * BitmapStride + x * 4;
		buffer[num] = b;
		buffer[num + 1] = g;
		buffer[num + 2] = r;
		buffer[num + 3] = a;
		int num2 = x / CacheWidth;
		int num3 = y / CacheHeight;
		isexist[num2, num3] = true;
		isdirty[num2, num3] = true;
		return true;
	}

	protected void BufferWrite()
	{
		Int32Rect int32Rect = new Int32Rect(0, 0, CacheWidth, CacheHeight);
		bitmap.Lock();
		for (int i = 0; i < XCacheCapacity; i++)
		{
			for (int j = 0; j < YCacheCapacity; j++)
			{
				if (isdirty[i, j])
				{
					isdirty[i, j] = false;
					int32Rect.X = i * CacheWidth;
					int32Rect.Y = j * CacheHeight;
					bitmap.WritePixels(int32Rect, buffer, BitmapStride, int32Rect.X, int32Rect.Y);
					bitmap.AddDirtyRect(int32Rect);
				}
			}
		}
		bitmap.Unlock();
	}

	protected bool OneBool_Left_ON(GridPenningBoolRangeResults rst)
	{
		switch (rst)
		{
		case GridPenningBoolRangeResults.ON:
		case GridPenningBoolRangeResults.ONtoOFF:
		case GridPenningBoolRangeResults.ZipZap:
			return true;
		default:
			return false;
		}
	}

	protected bool OneBool_Left_OFF(GridPenningBoolRangeResults rst)
	{
		if (rst == GridPenningBoolRangeResults.OFF || (uint)(rst - 3) <= 1u)
		{
			return true;
		}
		return false;
	}

	protected bool OneBool_Right_ON(GridPenningBoolRangeResults rst)
	{
		if (rst == GridPenningBoolRangeResults.ON || (uint)(rst - 3) <= 1u)
		{
			return true;
		}
		return false;
	}

	protected bool OneBool_Right_OFF(GridPenningBoolRangeResults rst)
	{
		if ((uint)(rst - 1) <= 1u || rst == GridPenningBoolRangeResults.ZipZap)
		{
			return true;
		}
		return false;
	}

	protected bool OneBool_LineDown(GridPenningBoolRangeResults oldrst, GridPenningBoolRangeResults newrst)
	{
		if (OneBool_Right_OFF(oldrst) && OneBool_Left_ON(newrst))
		{
			return true;
		}
		if (OneBool_Right_ON(oldrst) && OneBool_Left_OFF(newrst))
		{
			return true;
		}
		return false;
	}

	public void DrawingAll()
	{
		if (isdrawing)
		{
			return;
		}
		isdrawing = true;
		try
		{
			vpzone = GetDrawingRectAll();
			dwzone = vpzone;
			BufferClearAll();
			foreach (IGridPenningEntity entity in DrawingSource.GetEntities(dwzone))
			{
				DrawingEntity(entity);
			}
			BufferWrite();
			gbdraw.DrawingAll();
		}
		catch (Exception)
		{
		}
		finally
		{
			isdrawing = false;
		}
	}

	public void DrawingCover(Rect _dwzone)
	{
		if (isdrawing)
		{
			return;
		}
		isdrawing = true;
		try
		{
			vpzone = GetDrawingRectAll();
			dwzone = _dwzone;
			dwzone.Intersect(vpzone);
			foreach (IGridPenningEntity entity in DrawingSource.GetEntities(dwzone))
			{
				DrawingEntity(entity);
			}
			BufferWrite();
			gbdraw.DrawingAll();
		}
		catch (Exception)
		{
		}
		finally
		{
			isdrawing = false;
		}
	}

	public void Drawing(Rect _dwzone)
	{
		if (isdrawing)
		{
			return;
		}
		isdrawing = true;
		try
		{
			vpzone = GetDrawingRectAll();
			dwzone = _dwzone;
			dwzone.Intersect(vpzone);
			Rect vr = default(Rect);
			GetVisualRect(dwzone, ref vr);
			Int32Rect int32Rect = new Int32Rect((int)vr.X, (int)vr.Y, (int)vr.Width + 1, (int)vr.Height + 1);
			BufferClear(int32Rect.X, int32Rect.Y, int32Rect.Width, int32Rect.Height);
			foreach (IGridPenningEntity entity in DrawingSource.GetEntities(dwzone))
			{
				DrawingEntity(entity);
			}
			BufferWrite();
			gbdraw.DrawingAll();
		}
		catch (Exception)
		{
		}
		finally
		{
			isdrawing = false;
		}
	}

	protected bool DrawingEntity(IGridPenningEntity entity)
	{
		if (entity is IGridPenningFunctionXY)
		{
			IGridPenningFunctionXY funcxy = (IGridPenningFunctionXY)entity;
			DrawingXY(funcxy);
		}
		if (entity is IGridPenningFunctionYX)
		{
			IGridPenningFunctionYX funcyx = (IGridPenningFunctionYX)entity;
			DrawingYX(funcyx);
		}
		if (entity is IGridPenningBoolRuler)
		{
			IGridPenningBoolRuler ruler = (IGridPenningBoolRuler)entity;
			DrawingBool(ruler);
		}
		return true;
	}

	protected void DrawingXY(IGridPenningFunctionXY funcxy)
	{
		double vx = 0.0;
		double vx2 = 0.0;
		if (!GetVisualX(dwzone.Left, ref vx) || !GetVisualX(dwzone.Right, ref vx2))
		{
			return;
		}
		int val = (int)vx - 1;
		int val2 = (int)vx2 + 1;
		uint rgba = RGBA_Pens[funcxy.ColorID & 7];
		val = Math.Max(0, val);
		val2 = Math.Min((int)base.ActualWidth + 1, val2);
		bool flag = true;
		bool flag2 = false;
		int num = 0;
		for (int i = val; i < val2; i++)
		{
			double vx3 = i;
			double vy = 0.0;
			double lx = 0.0;
			double y = 0.0;
			if (!GetLogicalX(vx3, ref lx))
			{
				continue;
			}
			try
			{
				if (!funcxy.GetY(lx, ref y))
				{
					continue;
				}
			}
			catch (Exception)
			{
				continue;
			}
			bool visualY = GetVisualY(y, ref vy);
			if (!visualY)
			{
				vy = Math.Max(Math.Min(vy, base.ActualHeight), 0.0);
			}
			int num2 = (int)vy;
			byte yrate = (byte)((vy - (double)num2) * 256.0);
			if (visualY || flag2)
			{
				if (!flag && num2 > num)
				{
					DrawingRect(i, num, PenThickness, (uint)Math.Max(num2 - num + 1, (int)PenThickness), 0, yrate, rgba);
				}
				else if (!flag && num2 < num)
				{
					DrawingRect(i, num2, PenThickness, (uint)Math.Max(num - num2 + 1, (int)PenThickness), 0, yrate, rgba);
				}
				else
				{
					DrawingRect(i, (int)vy, PenThickness, PenThickness, 0, yrate, rgba);
				}
			}
			num = num2;
			flag2 = visualY;
			flag = false;
		}
	}

	protected void DrawingYX(IGridPenningFunctionYX funcyx)
	{
		double vy = 0.0;
		double vy2 = 0.0;
		if (!GetVisualY(dwzone.Top, ref vy) || !GetVisualY(dwzone.Bottom, ref vy2))
		{
			return;
		}
		int val = (int)vy - 1;
		int val2 = (int)vy2 + 1;
		uint rgba = RGBA_Pens[funcyx.ColorID & 7];
		val = Math.Max(0, val);
		val2 = Math.Min((int)base.ActualWidth + 1, val2);
		bool flag = true;
		bool flag2 = false;
		int num = 0;
		for (int i = val; i < val2; i++)
		{
			double vy3 = i;
			double vx = 0.0;
			double ly = 0.0;
			double x = 0.0;
			if (!GetLogicalY(vy3, ref ly))
			{
				continue;
			}
			try
			{
				if (!funcyx.GetX(ly, ref x))
				{
					continue;
				}
			}
			catch (Exception)
			{
				continue;
			}
			bool visualX = GetVisualX(x, ref vx);
			if (!visualX)
			{
				vx = Math.Max(Math.Min(vx, base.ActualWidth), 0.0);
			}
			int num2 = (int)vx;
			byte xrate = (byte)((vx - (double)num2) * 256.0);
			if (visualX || flag2)
			{
				if (!flag && num2 > num)
				{
					DrawingRect(num, i, (uint)Math.Max(num2 - num + 1, (int)PenThickness), PenThickness, xrate, 0, rgba);
				}
				else if (!flag && num2 < num)
				{
					DrawingRect(num2, i, (uint)Math.Max(num - num2 + 1, (int)PenThickness), PenThickness, xrate, 0, rgba);
				}
				else
				{
					DrawingRect((int)vx, i, PenThickness, PenThickness, xrate, 0, rgba);
				}
			}
			num = num2;
			flag2 = visualX;
			flag = false;
		}
	}

	protected void DrawingBool(IGridPenningBoolRuler ruler)
	{
		double vx = 0.0;
		double vx2 = 0.0;
		if (!GetVisualX(dwzone.Left, ref vx) || !GetVisualX(dwzone.Right, ref vx2))
		{
			return;
		}
		int val = (int)vx - 1;
		int val2 = (int)vx2 + 1;
		uint rgba = RGBA_Pens[ruler.ColorID & 7];
		val = Math.Max(0, val);
		val2 = Math.Min((int)base.ActualWidth + 1, val2);
		int num = val / OneBoolWidth;
		int num2 = val2 / OneBoolWidth;
		GridPenningBoolRangeResults gridPenningBoolRangeResults = GridPenningBoolRangeResults.OFF;
		int num3 = ruler.RulerID * (OneBoolHeight + OneBoolMargin);
		bool flag = true;
		for (int i = num; i <= num2; i++)
		{
			int num4 = i * OneBoolWidth;
			int num5 = num4 + OneBoolWidth;
			double vx3 = num4;
			double vx4 = num5;
			double lx = 0.0;
			double lx2 = 0.0;
			double sp = 0.0;
			int num6 = 0;
			GridPenningBoolRangeResults result = GridPenningBoolRangeResults.OFF;
			if (GetLogicalX(vx3, ref lx) && GetLogicalX(vx4, ref lx2) && ruler.GetMsg(lx, lx2, ref result, ref sp))
			{
				switch (result)
				{
				case GridPenningBoolRangeResults.OFF:
					DrawingRect(num4, num3, (uint)OneBoolWidth, PenThickness, 0, 0, rgba);
					break;
				case GridPenningBoolRangeResults.ON:
					DrawingRect(num4, (int)(num3 + OneBoolHeight - PenThickness), (uint)OneBoolWidth, PenThickness, 0, 0, rgba);
					break;
				case GridPenningBoolRangeResults.OFFtoON:
					num6 = (int)((sp - lx) * 4.0 / (lx2 - lx));
					DrawingRect(num4, num3, (uint)num6, PenThickness, 0, 0, rgba);
					DrawingRect(num4 + num6, (int)(num3 + OneBoolHeight - PenThickness), (uint)(OneBoolWidth - num6), PenThickness, 0, 0, rgba);
					DrawingRect(num4 + num6, num3, PenThickness, (uint)OneBoolHeight, 0, 0, rgba);
					break;
				case GridPenningBoolRangeResults.ONtoOFF:
					num6 = (int)((sp - lx) * 4.0 / (lx2 - lx));
					DrawingRect(num4, (int)(num3 + OneBoolHeight - PenThickness), (uint)num6, PenThickness, 0, 0, rgba);
					DrawingRect(num4 + num6, num3, (uint)(OneBoolWidth - num6), PenThickness, 0, 0, rgba);
					DrawingRect(num4 + num6, num3, PenThickness, (uint)OneBoolHeight, 0, 0, rgba);
					break;
				case GridPenningBoolRangeResults.ZipZap:
					DrawingRect(num4, num3, (uint)OneBoolWidth, (uint)OneBoolHeight, 0, 0, rgba);
					break;
				}
				gridPenningBoolRangeResults = result;
				flag = false;
			}
		}
	}

	protected void DrawingArch(IGridPenningCircle circle, Rect dwrect, WriteableBitmap bitmap, double startangle, double endangle)
	{
	}

	protected void DrawingArch(IGridPenningCircle circle, Rect dwrect, WriteableBitmap bitmap, bool xpositive, bool ypositive, double startangle, double endangle)
	{
	}

	protected void DrawingRect(int left, int top, uint width, uint height, byte xrate, byte yrate, uint rgba = uint.MaxValue)
	{
		byte r = 0;
		byte g = 0;
		byte b = 0;
		byte a = 0;
		byte gray = 0;
		ParseRGBA(rgba, ref r, ref g, ref b, ref a, ref gray);
		for (int i = left; i < left + width; i++)
		{
			for (int j = top; j < top + height; j++)
			{
				BufferSet(i, j, r, g, b, a);
			}
		}
	}

	protected void DrawingPoint(int x, int y, byte xrate, byte yrate)
	{
		if (x < 0 || x >= bitmap.PixelWidth || y < 0 || y >= bitmap.PixelHeight)
		{
			return;
		}
		byte r = (byte)((~xrate * (byte)(~yrate) >>> 8) & 0xFF);
		byte a = 0;
		byte r2 = 0;
		byte g = 0;
		byte b = 0;
		byte b2 = 0;
		byte b3 = 0;
		byte b4 = 0;
		byte b5 = 0;
		if (BufferGet(x, y, ref r2, ref g, ref b, ref a))
		{
			if (a == 0 && r2 == 0 && g == 0 && b == 0)
			{
				b2 = ChannelTransform(ba, fa, r);
				b3 = ChannelTransform(br, fr, r);
				b4 = ChannelTransform(bg, fg, r);
				b5 = ChannelTransform(bb, fb, r);
			}
			else
			{
				b2 = ChannelMix(ba, fa, a, r);
				b3 = ChannelMix(br, fr, r2, r);
				b4 = ChannelMix(bg, fg, g, r);
				b5 = ChannelMix(bb, fb, b, r);
			}
			BufferSet(x, y, b2, b3, b4, b5);
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		base.Clip = new RectangleGeometry(new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight));
	}

	private void Parent_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "DrawingSource":
		case "XScale":
		case "YScale":
			DrawingAll();
			break;
		}
	}

	private void Parent_OnBlockXIndexChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is int && e.NewValue is int)
		{
			int num = (int)e.OldValue;
			int num2 = (int)e.NewValue;
			DrawingAll();
		}
	}

	private void Parent_OnBlockYIndexChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is int && e.NewValue is int)
		{
			int num = (int)e.OldValue;
			int num2 = (int)e.NewValue;
			DrawingAll();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/gridpenning/gridpenningpanel.xaml", UriKind.Relative);
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
			This = (GridPenningPanel)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
