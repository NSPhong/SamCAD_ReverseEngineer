using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SamSoarII.Shell;

public class CustomItemsPanel : Panel
{
	private CustomItemBox parent;

	private DataTemplate itemtemplate;

	private ScrollBar sb_h;

	private ScrollBar sb_v;

	private bool sb_v_changing = false;

	private bool sb_h_changing = false;

	private ItemCollection lits;

	private List<FrameworkElement> vits;

	private int vstart;

	private int vcount;

	private double vheight;

	private Rect vwrect;

	private Rect exrect;

	private bool showuser_executing = false;

	public CustomItemBox ViewParent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public DataTemplate ItemTemplate
	{
		get
		{
			return itemtemplate;
		}
		set
		{
			itemtemplate = value;
		}
	}

	public bool CanHorizontalScroll => exrect.Width > base.ActualWidth;

	public bool CanVerticalScroll => vstart > 0 || vstart + vcount < lits.Count;

	public double ViewportPixelWidth => base.ActualWidth - (CanVerticalScroll ? sb_v.ActualWidth : 0.0);

	public double ViewportPixelHeight => base.ActualHeight - (CanHorizontalScroll ? sb_h.ActualHeight : 0.0);

	public double HorizontalOffset => vwrect.X;

	public double VerticalOffset => vwrect.Y;

	public double ViewportWidth => vwrect.Width;

	public double ViewportHeight => vwrect.Height;

	public double ExtendWidth => exrect.Width;

	public double ExtendHeight => exrect.Height;

	public CustomItemsPanel()
	{
		sb_h = new ScrollBar
		{
			Orientation = Orientation.Horizontal,
			Cursor = Cursors.Arrow
		};
		sb_v = new ScrollBar
		{
			Orientation = Orientation.Vertical,
			Cursor = Cursors.Arrow
		};
		lits = null;
		vits = new List<FrameworkElement>();
		vstart = 0;
		vcount = 0;
		vheight = 0.0;
		vwrect = default(Rect);
		exrect = default(Rect);
		sb_v.ValueChanged += OnVerticalScrollChanged;
		sb_h.ValueChanged += OnHorizontalScrollChanged;
		Panel.SetZIndex(sb_v, 1000);
		Panel.SetZIndex(sb_h, 1000);
		base.Children.Add(sb_v);
		base.Children.Add(sb_h);
		base.VerticalAlignment = VerticalAlignment.Stretch;
		base.HorizontalAlignment = HorizontalAlignment.Stretch;
	}

	protected override Size MeasureOverride(Size availableSize)
	{
		sb_v.Measure(availableSize);
		sb_h.Measure(availableSize);
		for (int i = 0; i < vcount; i++)
		{
			vits[i].Measure(availableSize);
		}
		return availableSize;
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		sb_v.Arrange(new Rect(finalSize.Width - sb_v.DesiredSize.Width, 0.0, sb_v.DesiredSize.Width, finalSize.Height - sb_h.DesiredSize.Height));
		sb_h.Arrange(new Rect(0.0, finalSize.Height - sb_h.DesiredSize.Height, finalSize.Width - sb_v.DesiredSize.Width, sb_h.DesiredSize.Height));
		double num = 0.0;
		for (int i = 0; i < vcount; i++)
		{
			vits[i].Arrange(new Rect(0.0 - vwrect.X, num, vits[i].Width, vits[i].Height));
			num += vits[i].Height;
		}
		for (int j = vcount; j < vits.Count(); j++)
		{
			vits[j].Arrange(new Rect(0.0, 0.0, 0.0, 0.0));
		}
		return finalSize;
	}

	public void SetHorizontalOffset(double _offset)
	{
		if (exrect.Width - vwrect.Width != 0.0)
		{
			_offset = Math.Max(_offset, 0.0);
			_offset = Math.Min(_offset, exrect.Width - vwrect.Width);
			sb_h.Value = _offset / (exrect.Width - vwrect.Width);
		}
	}

	public void SetVerticalOffset(double _offset)
	{
		int num = (int)_offset;
		if (num > vstart)
		{
			ShowBack(num - vstart);
			ShowUser();
		}
		else if (num < vstart)
		{
			ShowFount(vstart - num);
			ShowUser();
		}
	}

	public void LineUp()
	{
		ShowFount(1);
		ShowUser();
	}

	public void LineDown()
	{
		ShowBack(1);
		ShowUser();
	}

	public void LineLeft()
	{
		double value = sb_h.Value;
		value -= 0.05;
		value = Math.Max(value, 0.0);
		sb_h.Value = value;
	}

	public void LineRight()
	{
		double value = sb_h.Value;
		value += 0.05;
		value = Math.Min(value, 1.0);
		sb_h.Value = value;
	}

	public void PageUp()
	{
		ShowFount(vcount);
		ShowUser();
	}

	public void PageDown()
	{
		ShowBack(vcount);
		ShowUser();
	}

	protected void ShowClear()
	{
		foreach (FrameworkElement vit in vits)
		{
			vit.DataContext = null;
		}
		vcount = 0;
		vheight = 0.0;
	}

	protected void ShowUser()
	{
		if (showuser_executing)
		{
			return;
		}
		showuser_executing = true;
		vwrect.Y = vstart;
		vwrect.Width = ViewportPixelWidth;
		vwrect.Height = vcount;
		exrect.X = 0.0;
		exrect.Y = 0.0;
		exrect.Width = 0.0;
		exrect.Height = lits.Count;
		for (int i = 0; i < vcount; i++)
		{
			if (vits[i].Width > exrect.Width)
			{
				exrect.Width = vits[i].Width;
			}
		}
		if (!sb_h_changing)
		{
			sb_h.Value = ((exrect.Width - vwrect.Width > 0.0) ? (vwrect.X / (exrect.Width - vwrect.Width)) : 0.0);
			sb_h.ViewportSize = ((exrect.Width - vwrect.Width > 0.0) ? (vwrect.Width / (exrect.Width - vwrect.Width)) : 0.0);
			sb_h.Visibility = ((!CanHorizontalScroll) ? Visibility.Hidden : Visibility.Visible);
		}
		if (!sb_v_changing)
		{
			sb_v.Value = ((exrect.Height - vwrect.Height > 0.0) ? (vwrect.Y / (exrect.Height - vwrect.Height)) : 0.0);
			sb_v.ViewportSize = ((exrect.Height - vwrect.Height > 0.0) ? (vwrect.Height / (exrect.Height - vwrect.Height)) : 0.0);
			sb_v.Visibility = ((!CanVerticalScroll) ? Visibility.Hidden : Visibility.Visible);
		}
		InvalidateMeasure();
		InvalidateArrange();
		showuser_executing = false;
	}

	protected void ShowFount(int _start, int _count)
	{
		List<FrameworkElement> list = new List<FrameworkElement>();
		for (int i = 0; i < _count; i++)
		{
			if (vcount <= 0)
			{
				break;
			}
			FrameworkElement frameworkElement = null;
			if (vcount < vits.Count())
			{
				frameworkElement = vits.LastOrDefault();
				vits.RemoveAt(vits.Count() - 1);
			}
			else
			{
				DependencyObject dependencyObject = itemtemplate?.LoadContent();
				frameworkElement = ((dependencyObject is FrameworkElement) ? ((FrameworkElement)dependencyObject) : new UserControl
				{
					Width = 0.0,
					Height = 12.0
				});
				base.Children.Add(frameworkElement);
			}
			frameworkElement.DataContext = lits[_start + i];
			vheight += frameworkElement.Height;
			list.Add(frameworkElement);
			while (vcount > 0 && vheight - vits[vcount - 1].Height >= ViewportPixelHeight)
			{
				vheight -= vits[vcount - 1].Height;
				vits[vcount-- - 1].DataContext = null;
			}
		}
		vstart = _start;
		vcount += list.Count();
		vits.InsertRange(0, list);
	}

	protected void ShowBack(int _start, int _count)
	{
		int num = vcount;
		int num2 = 0;
		int num3 = 0;
		int num4 = _count - 1;
		while (num4 >= 0 && num2 < vcount)
		{
			FrameworkElement frameworkElement = null;
			if (vcount >= vits.Count())
			{
				if (num3 < num2)
				{
					frameworkElement = vits[num3];
					num3++;
				}
				else
				{
					DependencyObject dependencyObject = itemtemplate?.LoadContent();
					frameworkElement = ((dependencyObject is FrameworkElement) ? ((FrameworkElement)dependencyObject) : new UserControl
					{
						Width = 0.0,
						Height = 12.0
					});
					base.Children.Add(frameworkElement);
				}
				vits.Add(frameworkElement);
				vcount++;
			}
			else
			{
				frameworkElement = vits[vcount];
				vcount++;
			}
			frameworkElement.DataContext = lits[_start + num4];
			vheight += frameworkElement.Height;
			while (num2 < vcount && vheight - vits[num2].Height >= ViewportPixelHeight)
			{
				vheight -= vits[num2].Height;
				vits[num2++].DataContext = null;
			}
			num4--;
		}
		for (int i = num3; i < num2; i++)
		{
			vits.Add(vits[i]);
		}
		vits.Reverse(num, vcount - num);
		vits.RemoveRange(0, num2);
		vcount -= num2;
		vstart = _start + _count - vcount;
	}

	protected void ShowFount(int _count)
	{
		int val = vstart - _count;
		val = Math.Max(val, 0);
		_count = Math.Min(_count, vstart - val);
		ShowFount(val, _count);
	}

	protected void ShowBack(int _count)
	{
		int num = vstart + vcount;
		_count = Math.Min(_count, lits.Count - num);
		ShowBack(num, _count);
	}

	protected void ShowFount()
	{
		List<FrameworkElement> list = new List<FrameworkElement>();
		while (vstart > 0 && vheight < ViewportPixelHeight)
		{
			FrameworkElement frameworkElement = null;
			if (vits.Count() > vcount)
			{
				frameworkElement = vits.LastOrDefault();
				vits.RemoveAt(vits.Count() - 1);
			}
			else
			{
				DependencyObject dependencyObject = itemtemplate?.LoadContent();
				frameworkElement = ((dependencyObject is FrameworkElement) ? ((FrameworkElement)dependencyObject) : new UserControl
				{
					Width = 0.0,
					Height = 12.0
				});
				base.Children.Add(frameworkElement);
			}
			frameworkElement.DataContext = lits[--vstart];
			vheight += frameworkElement.Height;
			list.Add(frameworkElement);
		}
		vcount += list.Count();
		vits.InsertRange(0, list);
	}

	protected void ShowBack()
	{
		while (vstart + vcount < lits.Count && vheight < ViewportPixelHeight)
		{
			if (vits.Count() <= vcount)
			{
				DependencyObject dependencyObject = itemtemplate?.LoadContent();
				FrameworkElement frameworkElement = ((dependencyObject is FrameworkElement) ? ((FrameworkElement)dependencyObject) : new UserControl
				{
					Width = 0.0,
					Height = 12.0
				});
				base.Children.Add(frameworkElement);
				vits.Add(frameworkElement);
			}
			FrameworkElement frameworkElement2 = vits[vcount];
			frameworkElement2.DataContext = lits[vstart + vcount++];
			vheight += frameworkElement2.Height;
		}
	}

	public void InvalidateLogicItems(ItemCollection _lits)
	{
		lits = _lits;
		InvalidateVisualItems();
	}

	protected void InvalidateVisualItems()
	{
		vstart = Math.Max(vstart, 0);
		vstart = Math.Min(vstart, lits.Count - 1);
		ShowClear();
		ShowBack();
		ShowFount();
		ShowUser();
	}

	private void OnVerticalScrollChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	{
		sb_v_changing = true;
		int num = (int)(e.NewValue * (exrect.Height - vwrect.Height));
		if (num < vstart)
		{
			ShowFount(vstart - num);
			ShowUser();
		}
		else if (num > vstart)
		{
			ShowBack(num - vstart);
			ShowUser();
		}
		sb_v_changing = false;
	}

	private void OnHorizontalScrollChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	{
		sb_h_changing = true;
		vwrect.X = e.NewValue * (exrect.Width - vwrect.Width);
		InvalidateMeasure();
		InvalidateArrange();
		sb_h_changing = false;
	}
}
