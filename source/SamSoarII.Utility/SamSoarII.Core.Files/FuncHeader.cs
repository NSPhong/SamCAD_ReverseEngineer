using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FuncHeader : BaseFileHeader
{
	public struct FuncArg
	{
		public byte bType;

		public int spName;
	}

	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I2)]
	public short wHeaderSize;

	[FieldOffset(2)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReturn;

	[FieldOffset(3)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bArgCount;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I4)]
	public int spName;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public FuncArg[] stArg = new FuncArg[8];

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (short)value;
		}
	}

	public override int MaxHeaderSize => 8 + 8 * Marshal.SizeOf(typeof(FuncArg));

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Func;

	public override IFileHeader Create()
	{
		return new FuncHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spName = sp;
		}
		else
		{
			stArg[id - 1].spName = sp;
		}
	}
}
