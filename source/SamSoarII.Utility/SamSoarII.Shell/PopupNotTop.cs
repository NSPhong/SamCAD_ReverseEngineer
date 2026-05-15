using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace SamSoarII.Shell;

public class PopupNotTop : Popup
{
	public struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(PopupNotTop), new FrameworkPropertyMetadata(false, OnTopmostChanged));

	public bool Topmost
	{
		get
		{
			return (bool)GetValue(TopmostProperty);
		}
		set
		{
			SetValue(TopmostProperty, value);
		}
	}

	private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
	{
		(obj as PopupNotTop).UpdateWindow();
	}

	protected override void OnOpened(EventArgs e)
	{
		UpdateWindow();
	}

	private void UpdateWindow()
	{
		IntPtr handle = ((HwndSource)PresentationSource.FromDependencyObject(this)).Handle;
		if (GetWindowRect(handle, out var lpRect))
		{
			SetWindowPos(handle, Topmost ? (-1) : (-2), lpRect.Left, lpRect.Top, lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top, 0);
		}
	}

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

	[DllImport("user32")]
	private static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
}
