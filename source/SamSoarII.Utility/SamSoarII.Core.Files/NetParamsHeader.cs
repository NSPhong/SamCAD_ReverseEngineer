using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class NetParamsHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	public byte bWorkMode;

	[FieldOffset(5)]
	public byte bIsUsed;

	[FieldOffset(6)]
	public byte bReserved2;

	[FieldOffset(7)]
	public byte bReserved3;

	[FieldOffset(8)]
	public int dwIPSource;

	[FieldOffset(12)]
	public int dwIPTarget;

	[FieldOffset(16)]
	public int dwSubNetMask;

	[FieldOffset(20)]
	public int dwGateway;

	[FieldOffset(24)]
	public ushort wPortSource;

	[FieldOffset(26)]
	public ushort wPortTarget;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.NetParams;

	public override IFileHeader Create()
	{
		return new NetParamsHeader();
	}
}
