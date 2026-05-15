using System.Windows;
using System.Windows.Media;

namespace Standard;

internal static class DpiHelper
{
	private static Matrix _transformToDevice;

	private static Matrix _transformToDip;

	static DpiHelper()
	{
		using Standard.SafeDC hdc = Standard.SafeDC.GetDesktop();
		int deviceCaps = Standard.NativeMethods.GetDeviceCaps(hdc, Standard.DeviceCap.LOGPIXELSX);
		int deviceCaps2 = Standard.NativeMethods.GetDeviceCaps(hdc, Standard.DeviceCap.LOGPIXELSY);
		_transformToDip = Matrix.Identity;
		_transformToDip.Scale(96.0 / (double)deviceCaps, 96.0 / (double)deviceCaps2);
		_transformToDevice = Matrix.Identity;
		_transformToDevice.Scale((double)deviceCaps / 96.0, (double)deviceCaps2 / 96.0);
	}

	public static Point LogicalPixelsToDevice(Point logicalPoint)
	{
		return _transformToDevice.Transform(logicalPoint);
	}

	public static Point DevicePixelsToLogical(Point devicePoint)
	{
		return _transformToDip.Transform(devicePoint);
	}

	public static Rect LogicalRectToDevice(Rect logicalRectangle)
	{
		Point point = LogicalPixelsToDevice(new Point(logicalRectangle.Left, logicalRectangle.Top));
		Point point2 = LogicalPixelsToDevice(new Point(logicalRectangle.Right, logicalRectangle.Bottom));
		return new Rect(point, point2);
	}

	public static Rect DeviceRectToLogical(Rect deviceRectangle)
	{
		Point point = DevicePixelsToLogical(new Point(deviceRectangle.Left, deviceRectangle.Top));
		Point point2 = DevicePixelsToLogical(new Point(deviceRectangle.Right, deviceRectangle.Bottom));
		return new Rect(point, point2);
	}

	public static Size LogicalSizeToDevice(Size logicalSize)
	{
		Point point = LogicalPixelsToDevice(new Point(logicalSize.Width, logicalSize.Height));
		return new Size
		{
			Width = point.X,
			Height = point.Y
		};
	}

	public static Size DeviceSizeToLogical(Size deviceSize)
	{
		Point point = DevicePixelsToLogical(new Point(deviceSize.Width, deviceSize.Height));
		return new Size(point.X, point.Y);
	}
}
