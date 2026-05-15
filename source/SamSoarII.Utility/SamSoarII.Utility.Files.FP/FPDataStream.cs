using System;
using System.Text;

namespace SamSoarII.Utility.Files.FP;

public class FPDataStream : IDisposable
{
	private byte[] data;

	private int offset;

	public byte[] Data => data;

	public int Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
		}
	}

	public FPDataStream(byte[] _data)
	{
		data = _data;
		offset = 0;
	}

	public void Dispose()
	{
		data = null;
	}

	public byte Read8()
	{
		return (byte)((offset >= 0 && offset < data.Length) ? data[offset++] : 0);
	}

	public ushort ReadU16()
	{
		return (ushort)((Read8() << 8) + Read8());
	}

	public short ReadI16()
	{
		return (short)ReadU16();
	}

	public uint ReadU32()
	{
		return (uint)((ReadU16() << 16) + ReadU16());
	}

	public int ReadI32()
	{
		return (int)ReadU32();
	}

	public ulong ReadU64()
	{
		return ((ulong)ReadU32() << 32) + ReadU32();
	}

	public long ReadI64()
	{
		return (long)ReadU64();
	}

	public void ReadBytes(byte[] _datat, int start, int count)
	{
		if (offset >= 0 && offset < data.Length)
		{
			Array.Copy(data, offset, _datat, start, System.Math.Min(count, data.Length - offset));
			offset += count;
		}
	}

	public unsafe string ReadUnicode(int count)
	{
		string empty = string.Empty;
		fixed (byte* value = &data[offset])
		{
			empty = new string((sbyte*)value, 0, count * 2, Encoding.Unicode);
			offset += count * 2;
		}
		return empty;
	}
}
