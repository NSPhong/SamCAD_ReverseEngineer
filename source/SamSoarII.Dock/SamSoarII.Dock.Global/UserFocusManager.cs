using System.Windows;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.Global;

internal class UserFocusManager
{
	private static IUserFocus focusedobject;

	public static void Focus(IUserFocus _focusedobject)
	{
		IUserFocus userFocus = focusedobject;
		focusedobject = _focusedobject;
		userFocus?.InvokeIsUserFocusedChanged();
		if (focusedobject != null)
		{
			focusedobject.InvokeIsUserFocusedChanged();
		}
		if (userFocus is DependencyObject && focusedobject is DependencyObject)
		{
			Window window = Window.GetWindow((DependencyObject)userFocus);
			Window window2 = Window.GetWindow((DependencyObject)focusedobject);
			if (window == window2)
			{
			}
		}
	}

	public static void Unfocus(IUserFocus _focusedobject)
	{
		if (focusedobject == _focusedobject)
		{
			focusedobject = null;
			_focusedobject?.InvokeIsUserFocusedChanged();
		}
	}

	public static bool IsUserFocused(IUserFocus _focusedobject)
	{
		return focusedobject == _focusedobject;
	}
}
