using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace SamSoarII.Shell;

public class IPv4Box : UserControl
{
	private static readonly double IntervalFactor = 2.5;

	private static readonly double DotFactor = 0.6;

	private static readonly Typeface FontTypeface = new Typeface(new FontFamily("Microsoft Yahei"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

	private static readonly Brush TextSelectBrush = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 19,
		G = 33,
		B = 114
	});

	protected static readonly DependencyProperty IPv4Property = DependencyProperty.Register("IPv4", typeof(int), typeof(IPv4Box), new PropertyMetadata(0, OnPropertyChanged_IPv4));

	protected static readonly DependencyProperty IPMaskProperty = DependencyProperty.Register("IPMask", typeof(int), typeof(IPv4Box), new PropertyMetadata(0, OnPropertyChanged_IPMask));

	protected static readonly DependencyProperty CaretProperty = DependencyProperty.Register("Caret", typeof(int), typeof(IPv4Box), new PropertyMetadata(0, OnPropertyChanged_Caret));

	private DispatcherTimer timer;

	private bool iscaretshowed;

	public int IPv4
	{
		get
		{
			return (int)GetValue(IPv4Property);
		}
		set
		{
			SetValue(IPv4Property, value);
		}
	}

	public int IPMask
	{
		get
		{
			return (int)GetValue(IPMaskProperty);
		}
		set
		{
			SetValue(IPMaskProperty, value);
		}
	}

	public int Caret
	{
		get
		{
			return (int)GetValue(CaretProperty);
		}
		set
		{
			SetCaret(value);
		}
	}

	public event KeyEventHandler RaiseKeyDown;

	public IPv4Box()
	{
		iscaretshowed = false;
		timer = new DispatcherTimer(TimeSpan.FromMilliseconds(600.0), DispatcherPriority.Normal, OnTimer, base.Dispatcher);
		base.Cursor = Cursors.IBeam;
		base.Focusable = true;
		base.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, OnCut, CanCut));
		base.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, OnCopy, CanCopy));
		base.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPaste, CanPaste));
		base.CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, OnSelectAll, CanSelectAll));
		base.ContextMenu = new ContextMenu();
		base.ContextMenu.Items.Add(new MenuItem
		{
			Command = ApplicationCommands.Cut
		});
		base.ContextMenu.Items.Add(new MenuItem
		{
			Command = ApplicationCommands.Copy
		});
		base.ContextMenu.Items.Add(new MenuItem
		{
			Command = ApplicationCommands.Paste
		});
		base.ContextMenu.Items.Add(new MenuItem
		{
			Command = ApplicationCommands.SelectAll
		});
	}

	private static void OnPropertyChanged_IPv4(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is IPv4Box)
		{
			((IPv4Box)d).OnIPv4Changed(e);
		}
	}

	protected virtual void OnIPv4Changed(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	private static void OnPropertyChanged_IPMask(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is IPv4Box)
		{
			((IPv4Box)d).OnIPMaskChanged(e);
		}
	}

	protected virtual void OnIPMaskChanged(DependencyPropertyChangedEventArgs e)
	{
		InvalidateVisual();
	}

	protected virtual void SetCaret(int value)
	{
		int num = value >> 4;
		int num2 = value & 0xF;
		if (num < 0)
		{
			value = GetCaretMin(0);
		}
		else if (num > 3)
		{
			value = GetCaretMax(3);
		}
		else if (num2 > GetLength(num))
		{
			value = GetCaretMax(num);
		}
		SetValue(CaretProperty, value);
	}

	private static void OnPropertyChanged_Caret(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is IPv4Box)
		{
			((IPv4Box)d).OnCaretChanged(e);
		}
	}

	protected virtual void OnCaretChanged(DependencyPropertyChangedEventArgs e)
	{
		iscaretshowed = true;
		InvalidateVisual();
	}

	protected int GetNumber(int ci)
	{
		return (IPv4 >> (3 - ci) * 8) & 0xFF;
	}

	protected string GetString(int ci)
	{
		if (((IPMask >> ci) & 1) == 0)
		{
			return string.Empty;
		}
		return ((IPv4 >> (3 - ci) * 8) & 0xFF).ToString();
	}

	protected int GetLength(int ci)
	{
		return GetString(ci).Length;
	}

	protected int GetCaretMin(int ci)
	{
		return ci << 4;
	}

	protected int GetCaretMax(int ci)
	{
		return (ci << 4) + GetLength(ci);
	}

	protected void ClearSelection()
	{
		if ((IPMask & 0xF0) != 0)
		{
			int num = IPv4;
			int num2 = IPMask;
			if ((num2 & 0x10) != 0)
			{
				num = (int)((long)num & 0xFFFFFFL);
				num2 &= -18;
			}
			if ((num2 & 0x20) != 0)
			{
				num &= -16711681;
				num2 &= -35;
			}
			if ((num2 & 0x40) != 0)
			{
				num &= -65281;
				num2 &= -69;
			}
			if ((num2 & 0x80) != 0)
			{
				num &= -256;
				num2 &= -137;
			}
			IPv4 = num;
			if ((IPMask & 0x10) != 0)
			{
				Caret = GetCaretMin(0);
			}
			else if ((IPMask & 0x20) != 0)
			{
				Caret = GetCaretMin(1);
			}
			else if ((IPMask & 0x40) != 0)
			{
				Caret = GetCaretMin(2);
			}
			else if ((IPMask & 0x80) != 0)
			{
				Caret = GetCaretMin(3);
			}
			IPMask = num2;
		}
	}

	protected void Backspace()
	{
		int num = Caret >> 4;
		int num2 = Caret & 0xF;
		string text = GetString(num);
		if (num2 <= 0)
		{
			Left();
		}
		else if (text.Length == 1)
		{
			IPv4 &= ~(255 << (3 - num) * 8);
			IPMask &= ~(1 << num);
			Caret--;
		}
		else
		{
			text = ((num2 == text.Length) ? text.Substring(0, num2 - 1) : (text.Substring(0, num2 - 1) + text.Substring(num2)));
			int num3 = int.Parse(text);
			IPv4 = (IPv4 & ~(255 << (3 - num) * 8)) | (num3 << (3 - num) * 8);
			Caret--;
		}
	}

	protected void Left()
	{
		if ((IPMask & 0xF0) != 0)
		{
			if ((IPMask & 0x10) != 0)
			{
				IPMask = (IPMask & -241) | 0x80;
			}
			else if ((IPMask & 0x20) != 0)
			{
				IPMask = (IPMask & -241) | 0x10;
			}
			else if ((IPMask & 0x40) != 0)
			{
				IPMask = (IPMask & -241) | 0x20;
			}
			else if ((IPMask & 0x80) != 0)
			{
				IPMask = (IPMask & -241) | 0x40;
			}
		}
		else
		{
			int num = Caret >> 4;
			Caret--;
			int num2 = Caret >> 4;
			if (num != num2 && GetLength(num2) != 0)
			{
				IPMask |= 1 << 4 + num2;
			}
		}
	}

	protected void Right()
	{
		if ((IPMask & 0xF0) != 0)
		{
			if ((IPMask & 0x10) != 0)
			{
				IPMask = (IPMask & -241) | 0x20;
			}
			else if ((IPMask & 0x20) != 0)
			{
				IPMask = (IPMask & -241) | 0x40;
			}
			else if ((IPMask & 0x40) != 0)
			{
				IPMask = (IPMask & -241) | 0x80;
			}
			else if ((IPMask & 0x80) != 0)
			{
				IPMask = (IPMask & -241) | 0x10;
			}
		}
		else
		{
			int num = Caret & 0xF;
			int num2 = Caret >> 4;
			Caret = ((num >= GetLength(num2)) ? (num2 + 1 << 4) : (Caret + 1));
			int num3 = Caret >> 4;
			if (num2 != num3 && GetLength(num3) != 0)
			{
				IPMask |= 1 << 4 + num3;
			}
		}
	}

	protected void Copy()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < 4; i++)
		{
			if ((IPMask & (1 << 4 + i)) != 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(".");
				}
				stringBuilder.Append(GetNumber(i));
			}
		}
		DataObject data = new DataObject(stringBuilder.ToString());
		try
		{
			Clipboard.SetDataObject(data, copy: true);
		}
		catch (ExternalException)
		{
		}
	}

	protected void Paste(string text)
	{
		string[] array = text.Split(new char[2] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
		int num = Caret;
		int num2 = IPv4;
		int num3 = IPMask;
		string[] array2 = array;
		foreach (string s in array2)
		{
			int result = 0;
			int num4 = num >> 4;
			if (int.TryParse(s, out result))
			{
				num2 = (num2 & ~(255 << (3 - num4) * 8)) | ((result & 0xFF) << (3 - num4) * 8);
				num3 |= 1 << num4;
				if (num4 >= 3)
				{
					break;
				}
				num = GetCaretMin(num4 + 1);
			}
		}
		IPMask = num3;
		IPv4 = num2;
		Caret = num;
	}

	protected override void OnTextInput(TextCompositionEventArgs e)
	{
		base.OnTextInput(e);
		ClearSelection();
		if (e.Text.Length == 1)
		{
			int num = Caret >> 4;
			int num2 = Caret & 0xF;
			int number = GetNumber(num);
			int result = 0;
			if (!int.TryParse(e.Text, out result))
			{
				Caret = GetCaretMax(num);
				Right();
				return;
			}
			string text = number.ToString();
			text = ((GetLength(num) == 0) ? e.Text : ((num2 >= text.Length) ? (text + e.Text) : ((num2 <= 0) ? (e.Text + text) : (text.Substring(0, num2) + e.Text + text.Substring(num2)))));
			number = int.Parse(text);
			if (number >= 0 && number < 256)
			{
				IPMask |= 1 << num;
				IPv4 = (IPv4 & ~(255 << (3 - num) * 8)) | ((number & 0xFF) << (3 - num) * 8);
				Caret++;
			}
			else
			{
				Caret = GetCaretMax(num);
				Right();
			}
		}
		else
		{
			Paste(e.Text);
		}
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
		double actualWidth = base.ActualWidth;
		double actualHeight = base.ActualHeight;
		double num = 0.0;
		double num2 = base.FontSize * IntervalFactor;
		double num3 = base.FontSize * DotFactor;
		int num4 = Caret >> 4;
		int num5 = Caret & 0xF;
		FormattedText formattedText = null;
		ctx.DrawRectangle(Brushes.White, new Pen(Brushes.Gray, 1.0), new Rect(0.0, 0.0, actualWidth, actualHeight));
		for (int i = 0; i < 4; i++)
		{
			if (((IPMask >> 4 + i) & 1) != 0)
			{
				formattedText = new FormattedText((GetLength(i) > 0) ? GetString(i) : "0", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, base.FontSize, Brushes.White);
				ctx.DrawRectangle(TextSelectBrush, null, new Rect(num + (num2 - formattedText.Width) / 2.0, (actualHeight - formattedText.Height) / 2.0, formattedText.Width, formattedText.Height));
				if (GetLength(i) > 0)
				{
					ctx.DrawText(formattedText, new Point(num + (num2 - formattedText.Width) / 2.0, (actualHeight - formattedText.Height) / 2.0));
				}
			}
			else
			{
				formattedText = new FormattedText(GetString(i), Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, base.FontSize, Brushes.Black);
				ctx.DrawText(formattedText, new Point(num + (num2 - formattedText.Width) / 2.0, (actualHeight - formattedText.Height) / 2.0));
				if ((IPMask & 0xF0) == 0 && iscaretshowed && i == num4)
				{
					double x = num + (num2 - formattedText.Width) / 2.0 + ((formattedText.Text.Length == 0) ? 0.0 : ((double)num5 * formattedText.Width / (double)formattedText.Text.Length));
					ctx.DrawLine(new Pen(Brushes.Black, 1.0), new Point(x, 3.0), new Point(x, actualHeight - 3.0));
				}
			}
			num += num2;
			if (i < 3)
			{
				formattedText = new FormattedText(".", Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, base.FontSize, Brushes.Black);
				ctx.DrawText(formattedText, new Point(num + (num3 - formattedText.Width) / 2.0, (actualHeight - formattedText.Height) / 2.0));
				num += num3;
			}
		}
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		int num = Caret >> 4;
		int num2 = Caret & 0xF;
		switch (e.Key)
		{
		case Key.Left:
			e.Handled = true;
			Left();
			break;
		case Key.Right:
			e.Handled = true;
			Right();
			break;
		case Key.Back:
			e.Handled = true;
			if ((IPMask & 0xF0) != 0)
			{
				ClearSelection();
			}
			else
			{
				Backspace();
			}
			break;
		case Key.Return:
		case Key.Up:
		case Key.Down:
			e.Handled = true;
			this.RaiseKeyDown?.Invoke(this, e);
			break;
		}
	}

	protected override void OnMouseDown(MouseButtonEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.ChangedButton != MouseButton.Left)
		{
			return;
		}
		double actualWidth = base.ActualWidth;
		double actualHeight = base.ActualHeight;
		double num = base.FontSize * IntervalFactor;
		double num2 = base.FontSize * DotFactor;
		double num3 = e.GetPosition(this).X;
		Keyboard.Focus(this);
		CaptureMouse();
		int num4 = 0;
		num4 = 0;
		while (num4 < 4)
		{
			if (!(num3 < 0.0) && !(num3 > num))
			{
				FormattedText formattedText = new FormattedText(GetString(num4), Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, base.FontSize, Brushes.Black);
				int val = (int)((num3 - (num - formattedText.Width) / 2.0) / formattedText.Width * (double)formattedText.Text.Length);
				val = Math.Max(Math.Min(val, GetLength(num4)), 0);
				if (e.ClickCount >= 2)
				{
					IPMask |= 1 << 4 + num4;
					break;
				}
				IPMask &= -241;
				Caret = (num4 << 4) + val;
				break;
			}
			num4++;
			num3 -= num + num2;
		}
		if (num4 >= 4 && num3 >= 0.0)
		{
			if (e.ClickCount >= 2)
			{
				IPMask |= 128;
				return;
			}
			IPMask &= -241;
			Caret = GetCaretMax(3);
		}
	}

	protected override void OnMouseUp(MouseButtonEventArgs e)
	{
		base.OnMouseUp(e);
		ReleaseMouseCapture();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (!base.IsMouseCaptured)
		{
			return;
		}
		double actualWidth = base.ActualWidth;
		double actualHeight = base.ActualHeight;
		double num = base.FontSize * IntervalFactor;
		double num2 = base.FontSize * DotFactor;
		double num3 = e.GetPosition(this).X;
		int num4 = 0;
		while (num4 < 4)
		{
			if (!(num3 < 0.0) && !(num3 > num))
			{
				FormattedText formattedText = new FormattedText(GetString(num4), Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, FontTypeface, base.FontSize, Brushes.Black);
				int val = (int)((num3 - (num - formattedText.Width) / 2.0) / formattedText.Width * (double)formattedText.Text.Length);
				val = Math.Max(Math.Min(val, GetLength(num4)), 0);
				if ((IPMask & 0xF0) != 0 || Caret != (num4 << 4) + val)
				{
					IPMask |= 1 << 4 + num4;
				}
				break;
			}
			num4++;
			num3 -= num + num2;
		}
	}

	protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		base.OnGotKeyboardFocus(e);
		iscaretshowed = true;
		InvalidateVisual();
	}

	protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		base.OnLostKeyboardFocus(e);
		iscaretshowed = false;
		IPMask &= -241;
		InvalidateVisual();
	}

	private void OnTimer(object sender, EventArgs e)
	{
		if (base.IsKeyboardFocused && (IPMask & 0xF0) == 0)
		{
			iscaretshowed = !iscaretshowed;
			InvalidateVisual();
		}
	}

	private void OnCut(object sender, ExecutedRoutedEventArgs e)
	{
		Copy();
		ClearSelection();
	}

	private void OnCopy(object sender, ExecutedRoutedEventArgs e)
	{
		Copy();
	}

	private void OnPaste(object sender, ExecutedRoutedEventArgs e)
	{
		IDataObject dataObject = null;
		string text = null;
		try
		{
			dataObject = Clipboard.GetDataObject();
		}
		catch (ExternalException)
		{
			return;
		}
		try
		{
			text = dataObject.GetData(DataFormats.UnicodeText).ToString();
		}
		catch (OutOfMemoryException)
		{
			return;
		}
		ClearSelection();
		Paste(text);
	}

	private void OnSelectAll(object sender, ExecutedRoutedEventArgs e)
	{
		IPMask |= 240;
	}

	private void CanCut(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = (IPMask & 0xF0) != 0;
	}

	private void CanCopy(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = (IPMask & 0xF0) != 0;
	}

	private void CanPaste(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = true;
	}

	private void CanSelectAll(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = true;
	}
}
