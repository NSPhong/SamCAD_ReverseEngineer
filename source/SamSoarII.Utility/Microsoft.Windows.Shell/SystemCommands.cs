using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Standard;

namespace Microsoft.Windows.Shell;

public static class SystemCommands
{
	public static RoutedCommand CloseWindowCommand { get; private set; }

	public static RoutedCommand MaximizeWindowCommand { get; private set; }

	public static RoutedCommand MinimizeWindowCommand { get; private set; }

	public static RoutedCommand RestoreWindowCommand { get; private set; }

	public static RoutedCommand ShowSystemMenuCommand { get; private set; }

	static SystemCommands()
	{
		CloseWindowCommand = new RoutedCommand("CloseWindow", typeof(SystemCommands));
		MaximizeWindowCommand = new RoutedCommand("MaximizeWindow", typeof(SystemCommands));
		MinimizeWindowCommand = new RoutedCommand("MinimizeWindow", typeof(SystemCommands));
		RestoreWindowCommand = new RoutedCommand("RestoreWindow", typeof(SystemCommands));
		ShowSystemMenuCommand = new RoutedCommand("ShowSystemMenu", typeof(SystemCommands));
	}

	private static void _PostSystemCommand(Window window, Standard.SC command)
	{
		IntPtr handle = new WindowInteropHelper(window).Handle;
		if (!(handle == IntPtr.Zero) && Standard.NativeMethods.IsWindow(handle))
		{
			Standard.NativeMethods.PostMessage(handle, Standard.WM.SYSCOMMAND, new IntPtr((int)command), IntPtr.Zero);
		}
	}

	public static void CloseWindow(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		_PostSystemCommand(window, Standard.SC.CLOSE);
	}

	public static void MaximizeWindow(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		_PostSystemCommand(window, Standard.SC.MAXIMIZE);
	}

	public static void MinimizeWindow(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		_PostSystemCommand(window, Standard.SC.MINIMIZE);
	}

	public static void RestoreWindow(Window window)
	{
		Standard.Verify.IsNotNull(window, "window");
		_PostSystemCommand(window, Standard.SC.RESTORE);
	}

	public static void ShowSystemMenu(Window window, Point screenLocation)
	{
		Standard.Verify.IsNotNull(window, "window");
		ShowSystemMenuPhysicalCoordinates(window, Standard.DpiHelper.LogicalPixelsToDevice(screenLocation));
	}

	internal static void ShowSystemMenuPhysicalCoordinates(Window window, Point physicalScreenLocation)
	{
		Standard.Verify.IsNotNull(window, "window");
		IntPtr handle = new WindowInteropHelper(window).Handle;
		if (!(handle == IntPtr.Zero) && Standard.NativeMethods.IsWindow(handle))
		{
			IntPtr systemMenu = Standard.NativeMethods.GetSystemMenu(handle, bRevert: false);
			uint num = Standard.NativeMethods.TrackPopupMenuEx(systemMenu, 256u, (int)physicalScreenLocation.X, (int)physicalScreenLocation.Y, handle, IntPtr.Zero);
			if (num != 0)
			{
				Standard.NativeMethods.PostMessage(handle, Standard.WM.SYSCOMMAND, new IntPtr(num), IntPtr.Zero);
			}
		}
	}
}
