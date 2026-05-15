using System.Windows.Input;

namespace SamSoarII.Utility;

public class KeyInputHelper
{
	public static bool IsModifier(Key key)
	{
		if (key == Key.LeftCtrl || key == Key.RightCtrl || key == Key.LeftAlt || key == Key.RightAlt || key == Key.LeftShift || key == Key.RightShift || key == Key.LWin || key == Key.RWin)
		{
			return true;
		}
		return false;
	}
}
