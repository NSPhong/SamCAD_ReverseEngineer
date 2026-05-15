using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SamSoarII.Shell;

public class CustomPasswordBox : UserControl
{
	private PasswordBox pwbox;

	private CapslockTooltip ttcap;

	public string Password
	{
		get
		{
			return pwbox.Password;
		}
		set
		{
			pwbox.Password = value;
		}
	}

	public int MaxLength
	{
		get
		{
			return pwbox.MaxLength;
		}
		set
		{
			pwbox.MaxLength = value;
		}
	}

	public static bool CapsLockStatus
	{
		get
		{
			byte[] array = new byte[256];
			GetKeyboardState(array);
			return array[20] == 1 || array[20] == 129;
		}
	}

	public event RoutedEventHandler PasswordChanged
	{
		add
		{
			pwbox.PasswordChanged += value;
		}
		remove
		{
			pwbox.PasswordChanged -= value;
		}
	}

	public CustomPasswordBox()
	{
		pwbox = new PasswordBox();
		ttcap = new CapslockTooltip();
		ttcap.PlacementTarget = pwbox;
		ttcap.Placement = PlacementMode.RelativePoint;
		ttcap.HorizontalOffset = 0.0;
		ttcap.VerticalOffset = 22.0;
		pwbox.PasswordChar = '▪';
		pwbox.GotKeyboardFocus += PasswordBox_OnKeyboardFocus;
		pwbox.LostKeyboardFocus += PasswordBox_OnKeyboardUnfocus;
		pwbox.KeyDown += PasswordBox_OnKeyDown;
		base.Content = pwbox;
	}

	[DllImport("user32.dll")]
	public static extern int GetKeyboardState(byte[] pbKeyState);

	private void PasswordBox_OnKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		ttcap.IsOpen = CapsLockStatus;
	}

	private void PasswordBox_OnKeyboardUnfocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		ttcap.IsOpen = false;
	}

	private void PasswordBox_OnKeyDown(object sender, KeyEventArgs e)
	{
		ttcap.IsOpen = e.Key == Key.Capital && CapsLockStatus;
	}
}
