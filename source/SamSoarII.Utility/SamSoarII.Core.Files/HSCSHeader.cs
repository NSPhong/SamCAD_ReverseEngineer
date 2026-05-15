using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class HSCSHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public byte bInvokeMode;

	[FieldOffset(3)]
	public byte bCounterMode;

	[FieldOffset(4)]
	public int spCycle;

	[FieldOffset(8)]
	public int dwItemCount;

	[FieldOffset(12)]
	public int lpItems;

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (ushort)value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HSCS;

	public override IFileHeader Create()
	{
		return new HSCSHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spCycle = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}
