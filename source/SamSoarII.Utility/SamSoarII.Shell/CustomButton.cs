using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SamSoarII.Shell;

public class CustomButton : Button
{
	public static readonly DependencyProperty BusyWaitTimeProperty;

	private bool isbusy = false;

	private int busycount = 0;

	private static List<CustomButton> busylist;

	private static Timer busyreseter;

	public int BusyWaitTime
	{
		get
		{
			return (int)GetValue(BusyWaitTimeProperty);
		}
		set
		{
			SetValue(BusyWaitTimeProperty, Math.Max(0, value));
		}
	}

	static CustomButton()
	{
		BusyWaitTimeProperty = DependencyProperty.Register("BusyWaitTime", typeof(int), typeof(CustomButton), new PropertyMetadata(5, OnPropertyChanged_BusyWaitTime));
		busylist = new List<CustomButton>();
		busyreseter = new Timer(_BusyResetAll, null, 0, 100);
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata((object)null));
	}

	public CustomButton()
	{
		base.Style = (Style)FindResource("CustomButtonStyle");
	}

	private static void OnPropertyChanged_BusyWaitTime(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CustomButton)
		{
			((CustomButton)d).OnBusyWaitTimeChanged(e);
		}
	}

	protected virtual void OnBusyWaitTimeChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private void _BusyReset()
	{
		if (--busycount <= 0)
		{
			isbusy = false;
		}
	}

	private static void _BusyResetAll(object obj)
	{
		if (busylist == null || busylist.Count() == 0)
		{
			return;
		}
		CustomButton[] array = busylist.ToArray();
		foreach (CustomButton customButton in array)
		{
			customButton._BusyReset();
			if (!customButton.isbusy)
			{
				busylist.Remove(customButton);
			}
		}
	}

	protected override void OnClick()
	{
		base.OnClick();
		if (!isbusy && BusyWaitTime != 0)
		{
			isbusy = true;
			busycount = BusyWaitTime;
			busylist.Add(this);
		}
	}

	protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
	{
		base.OnPreviewMouseDown(e);
		if (isbusy)
		{
			e.Handled = true;
		}
	}
}
