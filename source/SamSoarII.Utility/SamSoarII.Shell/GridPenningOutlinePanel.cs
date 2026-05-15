using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SamSoarII.Shell;

public class GridPenningOutlinePanel : UserControl
{
	public static readonly int TileWidth = 4;

	public static readonly int TileHeight = 4;

	public static readonly int TileXCapacity = 320;

	public static readonly int TileYCapacity = 192;

	public const int PXID_On = 0;

	public const int PXID_Off = 1;

	public const int PXID_VOn = 2;

	public const int PXID_VOff = 3;

	public const int PX_Length = 4;

	private GridPenning parent;

	private bool[,] tiles;

	private byte[][] pxs;

	private WriteableBitmap bitmap;

	private Image bmpimage;

	private Canvas bmpcanvas;

	private bool isshowed;

	private int viewtop;

	private int viewbottom;

	private int viewleft;

	private int viewright;

	public static int PX_ByteNum => TileWidth * TileHeight * 4;

	public static int PX_Stride => TileWidth * 4;

	public GridPenning ViewParent => parent;

	public IGridPenningSource DrawingSource => ViewParent?.DrawingSource;

	public int TileXNumber => Math.Min((int)(base.ActualWidth / (double)TileWidth) + 1, TileXCapacity);

	public int TileYNumber => Math.Min((int)(base.ActualHeight / (double)TileHeight) + 1, TileYCapacity);

	public GridPenningOutlinePanel(GridPenning _parent)
	{
		parent = _parent;
		tiles = new bool[TileXCapacity, TileYCapacity];
		bitmap = new WriteableBitmap(TileWidth * TileXCapacity, TileHeight * TileYCapacity, 96.0, 96.0, PixelFormats.Bgra32, null);
		bmpimage = new Image();
		bmpcanvas = new Canvas();
		pxs = new byte[4][];
		isshowed = false;
		bmpimage.Source = bitmap;
		bmpimage.HorizontalAlignment = HorizontalAlignment.Left;
		bmpimage.VerticalAlignment = VerticalAlignment.Top;
		bmpcanvas.Children.Add(bmpimage);
		base.Content = bmpcanvas;
		base.Visibility = Visibility.Hidden;
		for (int i = 0; i < 4; i++)
		{
			pxs[i] = new byte[PX_ByteNum];
			for (int j = 0; j < TileWidth; j++)
			{
				for (int k = 0; k < TileHeight; k++)
				{
					if (j == TileWidth - 1 || k == TileHeight - 1)
					{
						int num = i;
						int num2 = num;
						if ((uint)(num2 - 2) <= 1u)
						{
							WriteRGBA(pxs[i], k * PX_Stride + j * 4, 4282400832u);
						}
						else
						{
							WriteRGBA(pxs[i], k * PX_Stride + j * 4, 4278190080u);
						}
						continue;
					}
					switch (i)
					{
					case 2:
						WriteRGBA(pxs[i], k * PX_Stride + j * 4, uint.MaxValue);
						break;
					case 3:
						WriteRGBA(pxs[i], k * PX_Stride + j * 4, 4278190080u);
						break;
					case 0:
						WriteRGBA(pxs[i], k * PX_Stride + j * 4, 4282400832u);
						break;
					default:
						WriteRGBA(pxs[i], k * PX_Stride + j * 4, 4278190080u);
						break;
					}
				}
			}
		}
	}

	protected int GetXIndex(double x)
	{
		int num = (int)((x - DrawingSource.XStart) * (double)TileXNumber / DrawingSource.XLength);
		if (num <= -1)
		{
			num = 0;
		}
		if (num >= TileXNumber)
		{
			num = TileYNumber - 1;
		}
		return num;
	}

	protected int GetYIndex(double y)
	{
		int num = (int)((y - DrawingSource.YStart) * (double)TileYNumber / DrawingSource.YLength);
		if (num <= -1)
		{
			num = 0;
		}
		if (num >= TileYNumber)
		{
			num = TileYNumber - 1;
		}
		return num;
	}

	protected double GetXValue(int xi)
	{
		return (double)xi * DrawingSource.XLength / (double)TileXNumber + DrawingSource.XStart;
	}

	protected double GetYValue(int yi)
	{
		return (double)yi * DrawingSource.YLength / (double)TileYNumber + DrawingSource.YStart;
	}

	protected void GetViewRect(ref int _viewtop, ref int _viewbottom, ref int _viewleft, ref int _viewright)
	{
		Point p = new Point(parent.HorizontalOffset, parent.VerticalOffset);
		GetTileXY(p, ref _viewleft, ref _viewtop);
		p.X += parent.ViewportWidth;
		p.Y += parent.ViewportHeight;
		GetTileXY(p, ref _viewright, ref _viewbottom);
		_viewleft = Math.Max(_viewleft, 0);
		_viewright = Math.Min(_viewright, TileXNumber - 1);
		_viewtop = Math.Max(_viewtop, 0);
		_viewbottom = Math.Min(_viewbottom, TileYNumber - 1);
	}

	protected void WritePX(int xi, int yi, int pxid)
	{
		Int32Rect sourceRect = new Int32Rect(0, 0, TileWidth, TileHeight);
		bitmap.WritePixels(sourceRect, pxs[pxid], PX_Stride, xi * TileWidth, (TileYNumber - yi - 1) * TileHeight);
	}

	protected void WriteRGBA(byte[] px, int offset, uint rgba)
	{
		px[offset] = (byte)(rgba & 0xFF);
		px[offset + 1] = (byte)((rgba >> 8) & 0xFF);
		px[offset + 2] = (byte)((rgba >> 16) & 0xFF);
		px[offset + 3] = (byte)((rgba >> 24) & 0xFF);
	}

	protected void SwapXY(ref int x1, ref int y1, ref int x2, ref int y2)
	{
		int num = x1;
		x1 = x2;
		x2 = num;
		num = y1;
		y1 = y2;
		y2 = num;
	}

	protected void GetTileXY(Point p, ref int xi, ref int yi)
	{
		xi = (int)(p.X * (double)TileXNumber / DrawingSource.XLength);
		yi = (int)(p.Y * (double)TileYNumber / DrawingSource.YLength);
		xi = Math.Min(Math.Max(0, xi), TileXNumber - 1);
		yi = Math.Min(Math.Max(0, yi), TileYNumber - 1);
	}

	private void _DrawX(double a1, double a2, Point center, double radios, int spi)
	{
		double val = center.X + Math.Cos(a1) * radios;
		double val2 = center.X + Math.Cos(a2) * radios;
		int xIndex = GetXIndex(Math.Min(val, val2));
		int xIndex2 = GetXIndex(Math.Max(val, val2));
		for (int i = xIndex; i <= xIndex2; i++)
		{
			double xValue = GetXValue(i);
			double y = center.Y + Math.Sqrt(Math.Max(0.0, radios * radios - (xValue - center.X) * (xValue - center.X))) * (double)(((spi & 1) == 0) ? 1 : (-1));
			int yIndex = GetYIndex(y);
			tiles[i, yIndex] = true;
		}
	}

	private void _DrawY(double a1, double a2, Point center, double radios, int spi, int spj)
	{
		double val = center.Y + Math.Sin(a1) * radios;
		double val2 = center.Y + Math.Sin(a2) * radios;
		int yIndex = GetYIndex(Math.Min(val, val2));
		int yIndex2 = GetYIndex(Math.Max(val, val2));
		for (int i = yIndex; i <= yIndex2; i++)
		{
			double yValue = GetYValue(i);
			double x = center.X + Math.Sqrt(Math.Max(0.0, radios * radios - (yValue - center.Y) * (yValue - center.Y))) * (double)((((spi + spj) & 1) == 0) ? 1 : (-1));
			int xIndex = GetXIndex(x);
			tiles[xIndex, i] = true;
		}
	}

	public void DrawingAll()
	{
		for (int i = 0; i < TileXNumber; i++)
		{
			for (int j = 0; j < TileYNumber; j++)
			{
				tiles[i, j] = false;
			}
		}
		foreach (IGridPenningEntity entity in DrawingSource.GetEntities(new Rect(DrawingSource.XStart, DrawingSource.YStart, DrawingSource.XLength, DrawingSource.YLength)))
		{
			if (entity is IGridPenningLine)
			{
				IGridPenningLine gridPenningLine = (IGridPenningLine)entity;
				if (!gridPenningLine.IsReal)
				{
					continue;
				}
				Point start = gridPenningLine.Start;
				Point end = gridPenningLine.End;
				Vector vector = end - start;
				if (Math.Abs(vector.X) > Math.Abs(vector.Y))
				{
					int xIndex = GetXIndex(start.X);
					int xIndex2 = GetXIndex(end.X);
					int num = Math.Min(xIndex, xIndex2);
					int num2 = Math.Max(xIndex, xIndex2);
					for (int k = num; k <= num2; k++)
					{
						double xValue = GetXValue(k);
						double y = start.Y + (end.Y - start.Y) / (end.X - start.X) * (xValue - start.X);
						int yIndex = GetYIndex(y);
						tiles[k, yIndex] = true;
					}
				}
				else
				{
					int yIndex2 = GetYIndex(start.Y);
					int yIndex3 = GetYIndex(end.Y);
					int num3 = Math.Min(yIndex2, yIndex3);
					int num4 = Math.Max(yIndex2, yIndex3);
					for (int l = num3; l <= num4; l++)
					{
						double yValue = GetYValue(l);
						double x = start.X + (end.X - start.X) / (end.Y - start.Y) * (yValue - start.Y);
						int xIndex3 = GetXIndex(x);
						tiles[xIndex3, l] = true;
					}
				}
			}
			else if (entity is IGridPenningArch)
			{
				IGridPenningArch gridPenningArch = (IGridPenningArch)entity;
				Point center = gridPenningArch.Center;
				double radius = gridPenningArch.Radius;
				double num5 = gridPenningArch.StartAngle;
				double num6 = gridPenningArch.EndAngle;
				if (gridPenningArch.Clockwise)
				{
					double num7 = num5;
					num5 = num6;
					num6 = num7;
				}
				for (; num5 < 0.0; num5 += Math.PI * 2.0)
				{
				}
				for (; num6 < num5; num6 += Math.PI * 2.0)
				{
				}
				double num8 = num5;
				int m;
				for (m = 0; Math.PI * (double)(m + 1) < num5; m++)
				{
				}
				for (; Math.PI * (double)m < num6; m++)
				{
					double num9 = Math.PI * (double)m;
					double val = Math.PI * (double)m + Math.PI / 4.0;
					double val2 = Math.PI * (double)m + Math.PI * 3.0 / 4.0;
					double num10 = Math.PI * (double)m + Math.PI;
					double val3 = num9;
					double val4 = num10;
					val3 = Math.Max(val3, num5);
					val4 = Math.Min(val4, num6);
					double num11 = Math.Min(Math.Max(val3, num9), val);
					double num12 = Math.Min(Math.Max(val4, num9), val);
					if (num11 < num12)
					{
						_DrawY(num11, num12, center, radius, m, 0);
					}
					double num13 = Math.Min(Math.Max(val3, val), val2);
					double num14 = Math.Min(Math.Max(val4, val), val2);
					if (num13 < num14)
					{
						_DrawX(num13, num14, center, radius, m);
					}
					double num15 = Math.Min(Math.Max(val3, val2), num10);
					double num16 = Math.Min(Math.Max(val4, val2), num10);
					if (num15 < num16)
					{
						_DrawY(num15, num16, center, radius, m, 1);
					}
				}
			}
			else if (entity is IGridPenningCircle)
			{
				IGridPenningCircle gridPenningCircle = (IGridPenningCircle)entity;
				Point center2 = gridPenningCircle.Center;
				double radius2 = gridPenningCircle.Radius;
				double x2 = center2.X - radius2 / 1.414;
				double x3 = center2.X + radius2 / 1.414;
				int xIndex4 = GetXIndex(x2);
				int xIndex5 = GetXIndex(x3);
				for (int n = xIndex4; n <= xIndex5; n++)
				{
					double xValue2 = GetXValue(n);
					double num17 = Math.Sqrt(Math.Max(0.0, radius2 * radius2 - (xValue2 - center2.X) * (xValue2 - center2.X)));
					double y2 = center2.Y - num17;
					int yIndex4 = GetYIndex(y2);
					tiles[n, yIndex4] = true;
					double y3 = center2.Y + num17;
					int yIndex5 = GetYIndex(y3);
					tiles[n, yIndex5] = true;
				}
				double y4 = center2.Y - radius2 / 1.414;
				double y5 = center2.Y + radius2 / 1.414;
				int yIndex6 = GetYIndex(y4);
				int yIndex7 = GetYIndex(y5);
				for (int num18 = yIndex6; num18 < yIndex7; num18++)
				{
					double yValue2 = GetYValue(num18);
					double num19 = Math.Sqrt(Math.Max(0.0, radius2 * radius2 - (yValue2 - center2.Y) * (yValue2 - center2.Y)));
					double x4 = center2.X - num19;
					int xIndex6 = GetXIndex(x4);
					tiles[xIndex6, num18] = true;
					double x5 = center2.X + num19;
					int xIndex7 = GetXIndex(x5);
					tiles[xIndex7, num18] = true;
				}
			}
			else if (entity is IGridPenningFunctionXY)
			{
				IGridPenningFunctionXY gridPenningFunctionXY = (IGridPenningFunctionXY)entity;
				int xIndex8 = GetXIndex(gridPenningFunctionXY.XMin);
				int xIndex9 = GetXIndex(gridPenningFunctionXY.XMax);
				for (int num20 = xIndex8; num20 <= xIndex9; num20++)
				{
					double x6 = DrawingSource.XStart + (double)num20 * DrawingSource.XLength / (double)TileXNumber;
					double y6 = 0.0;
					if (gridPenningFunctionXY.GetY(x6, ref y6))
					{
						int num21 = (int)((y6 - DrawingSource.YStart) * (double)TileYNumber / DrawingSource.YLength);
						if (num21 == -1)
						{
							num21 = 0;
						}
						if (num21 == TileYNumber)
						{
							num21 = TileYNumber - 1;
						}
						if (num21 >= 0 && num21 < TileYNumber)
						{
							tiles[num20, num21] = true;
						}
					}
				}
			}
			if (!(entity is IGridPenningFunctionYX))
			{
				continue;
			}
			IGridPenningFunctionYX gridPenningFunctionYX = (IGridPenningFunctionYX)entity;
			int yIndex8 = GetYIndex(gridPenningFunctionYX.YMin);
			int yIndex9 = GetYIndex(gridPenningFunctionYX.YMax);
			for (int num22 = yIndex8; num22 <= yIndex9; num22++)
			{
				double y7 = DrawingSource.YStart + (double)num22 * DrawingSource.YLength / (double)TileYNumber;
				double x7 = 0.0;
				if (gridPenningFunctionYX.GetX(y7, ref x7))
				{
					int num23 = (int)((x7 - DrawingSource.XStart) * (double)TileXNumber / DrawingSource.XLength);
					if (num23 == -1)
					{
						num23 = 0;
					}
					if (num23 == TileYNumber)
					{
						num23 = TileYNumber - 1;
					}
					if (num23 >= 0 && num23 < TileXNumber)
					{
						tiles[num23, num22] = true;
					}
				}
			}
		}
	}

	public void DrawingCover(Rect _dwzone)
	{
		foreach (IGridPenningEntity entity in DrawingSource.GetEntities(new Rect(DrawingSource.XStart, DrawingSource.YStart, DrawingSource.XLength, DrawingSource.YLength)))
		{
			if (entity is IGridPenningLine)
			{
			}
			if (entity is IGridPenningCircle)
			{
			}
			if (entity is IGridPenningArch)
			{
			}
			if (entity is IGridPenningFunctionXY)
			{
				IGridPenningFunctionXY gridPenningFunctionXY = (IGridPenningFunctionXY)entity;
				int num = (int)((_dwzone.Left - DrawingSource.XStart) * (double)TileXNumber / DrawingSource.XLength);
				int num2 = (int)((_dwzone.Right - DrawingSource.XStart) * (double)TileXNumber / DrawingSource.XLength);
				for (int i = num; i <= num2; i++)
				{
					double x = DrawingSource.XStart + (double)i * DrawingSource.XLength / (double)TileXNumber;
					double y = 0.0;
					if (gridPenningFunctionXY.GetY(x, ref y))
					{
						int num3 = (int)((y - DrawingSource.YStart) * (double)TileYNumber / DrawingSource.YLength);
						if (num3 == -1)
						{
							num3 = 0;
						}
						if (num3 == TileYNumber)
						{
							num3 = TileYNumber - 1;
						}
						if (num3 >= 0 && num3 < TileYNumber)
						{
							tiles[i, num3] = true;
						}
					}
				}
			}
			if (!(entity is IGridPenningFunctionYX))
			{
				continue;
			}
			IGridPenningFunctionYX gridPenningFunctionYX = (IGridPenningFunctionYX)entity;
			int num4 = (int)((_dwzone.Top - DrawingSource.YStart) * (double)TileYNumber / DrawingSource.YLength);
			int num5 = (int)((_dwzone.Bottom - DrawingSource.YStart) * (double)TileYNumber / DrawingSource.YLength);
			for (int j = num4; j <= num5; j++)
			{
				double y2 = DrawingSource.YStart + (double)j * DrawingSource.YLength / (double)TileYNumber;
				double x2 = 0.0;
				if (gridPenningFunctionYX.GetX(y2, ref x2))
				{
					int num6 = (int)((x2 - DrawingSource.XStart) * (double)TileXNumber / DrawingSource.XLength);
					if (num6 == -1)
					{
						num6 = 0;
					}
					if (num6 == TileYNumber)
					{
						num6 = TileYNumber - 1;
					}
					if (num6 >= 0 && num6 < TileXNumber)
					{
						tiles[num6, j] = true;
					}
				}
			}
		}
	}

	public void Update()
	{
		int _viewleft = 0;
		int _viewright = 0;
		int _viewtop = 0;
		int _viewbottom = 0;
		GetViewRect(ref _viewtop, ref _viewbottom, ref _viewleft, ref _viewright);
		if (viewleft == _viewleft && viewright == _viewright && viewtop == _viewtop && viewbottom == _viewbottom)
		{
			return;
		}
		int num = Math.Min(viewleft, _viewleft);
		int num2 = Math.Max(viewright, _viewright);
		int num3 = Math.Min(viewtop, _viewtop);
		int num4 = Math.Max(viewbottom, _viewbottom);
		bitmap.Lock();
		for (int i = num; i <= num2; i++)
		{
			for (int j = num3; j <= num4; j++)
			{
				bool flag = i >= viewleft && i <= viewright && j >= viewtop && j <= viewbottom;
				bool flag2 = i >= _viewleft && i <= _viewright && j >= _viewtop && j <= _viewbottom;
				if (!flag && flag2)
				{
					WritePX(i, j, tiles[i, j] ? 2 : 3);
				}
				if (flag && !flag2)
				{
					WritePX(i, j, (!tiles[i, j]) ? 1 : 0);
				}
			}
		}
		int num5 = Math.Max(viewleft, _viewleft);
		int num6 = Math.Min(viewright, _viewright);
		int num7 = Math.Max(viewtop, _viewtop);
		int num8 = Math.Min(viewbottom, _viewbottom);
		bitmap.AddDirtyRect(new Int32Rect(num, num3, num5 - num, num4 - num3 + 1));
		bitmap.AddDirtyRect(new Int32Rect(num, num3, num2 - num + 1, num7 - num3));
		bitmap.AddDirtyRect(new Int32Rect(num, num8, num2 - num + 1, num4 - num8));
		bitmap.AddDirtyRect(new Int32Rect(num6, num3, num2 - num6, num4 - num3 + 1));
		bitmap.Unlock();
		viewleft = _viewleft;
		viewright = _viewright;
		viewtop = _viewtop;
		viewbottom = _viewbottom;
	}

	public void Show()
	{
		bitmap.Lock();
		GetViewRect(ref viewtop, ref viewbottom, ref viewleft, ref viewright);
		for (int i = 0; i < TileXNumber; i++)
		{
			for (int j = 0; j < TileYNumber; j++)
			{
				if (i >= viewleft && i <= viewright && j >= viewtop && j <= viewbottom)
				{
					WritePX(i, j, tiles[i, j] ? 2 : 3);
				}
				else
				{
					WritePX(i, j, (!tiles[i, j]) ? 1 : 0);
				}
			}
		}
		bitmap.Unlock();
		isshowed = true;
		base.Visibility = Visibility.Visible;
	}

	public void Hide()
	{
		isshowed = false;
		base.Visibility = Visibility.Hidden;
	}
}
