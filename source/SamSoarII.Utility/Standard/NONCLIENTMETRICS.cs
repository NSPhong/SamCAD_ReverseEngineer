using System.Runtime.InteropServices;

namespace Standard;

internal struct NONCLIENTMETRICS
{
	public int cbSize;

	public int iBorderWidth;

	public int iScrollWidth;

	public int iScrollHeight;

	public int iCaptionWidth;

	public int iCaptionHeight;

	public Standard.LOGFONT lfCaptionFont;

	public int iSmCaptionWidth;

	public int iSmCaptionHeight;

	public Standard.LOGFONT lfSmCaptionFont;

	public int iMenuWidth;

	public int iMenuHeight;

	public Standard.LOGFONT lfMenuFont;

	public Standard.LOGFONT lfStatusFont;

	public Standard.LOGFONT lfMessageFont;

	public int iPaddedBorderWidth;

	public static Standard.NONCLIENTMETRICS VistaMetricsStruct => new Standard.NONCLIENTMETRICS
	{
		cbSize = Marshal.SizeOf(typeof(Standard.NONCLIENTMETRICS))
	};

	public static Standard.NONCLIENTMETRICS XPMetricsStruct => new Standard.NONCLIENTMETRICS
	{
		cbSize = Marshal.SizeOf(typeof(Standard.NONCLIENTMETRICS)) - 4
	};
}
