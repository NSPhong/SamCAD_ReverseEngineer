using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Sequential)]
internal class WINDOWPLACEMENT
{
	public int length = Marshal.SizeOf(typeof(Standard.WINDOWPLACEMENT));

	public int flags;

	public Standard.SW showCmd;

	public Standard.POINT ptMinPosition;

	public Standard.POINT ptMaxPosition;

	public Standard.RECT rcNormalPosition;
}
