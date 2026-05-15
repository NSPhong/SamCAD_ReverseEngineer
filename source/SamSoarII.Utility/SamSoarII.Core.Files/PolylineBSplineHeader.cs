using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class PolylineBSplineHeader : PolylineEntityHeader
{
	public int dwHeaderSize;

	public int dwPointCount;

	public int dwWeightCount;

	public int dwOffsetCount;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public float[] dwData = new float[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			dwData = new float[FloatSize];
		}
	}

	public int NonDataSize => Marshal.SizeOf(typeof(PolylineEntityHeader)) + 12;

	public int FloatSize => (HeaderSize - NonDataSize - 1) / 4 + 1;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.LadderLine;

	public override IFileHeader Create()
	{
		return new LadderLineHeader();
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(dwData, 0, intptr + NonDataSize, dwData.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		dwData = new float[FloatSize];
		Marshal.Copy(intptr + NonDataSize, dwData, 0, dwData.Length);
	}
}
