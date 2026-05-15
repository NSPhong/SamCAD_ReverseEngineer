using System;
using System.IO;

namespace SamSoarII.Utility.Files.KVS;

public class KVSFileStream : IDisposable
{
	private byte[] data;

	private int index;

	public byte[] Data => data;

	public int Index
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
		}
	}

	public int Length
	{
		get
		{
			byte[] array = data;
			return (array != null) ? array.Length : 0;
		}
	}

	public bool EndOfStream => index >= Length;

	public KVSFileStream(Stream s)
	{
		data = new byte[s.Length];
		index = 0;
		s.Read(data, 0, data.Length);
	}

	public void Dispose()
	{
		data = null;
		index = -1;
	}

	public void Reset()
	{
		index = 0;
	}

	public bool Equal(byte[] temp)
	{
		if (index < 0)
		{
			return false;
		}
		if (index + temp.Length > Length)
		{
			return false;
		}
		for (int i = 0; i < temp.Length; i++)
		{
			if (data[index + i] != temp[i])
			{
				return false;
			}
		}
		return true;
	}

	public bool Previous(byte[] temp)
	{
		while (--index >= 0 && index < Length)
		{
			if (Equal(temp))
			{
				return true;
			}
		}
		return false;
	}

	public bool Next(byte[] temp)
	{
		while (++index + temp.Length <= Length && index >= 0)
		{
			if (Equal(temp))
			{
				return true;
			}
		}
		return false;
	}

	public bool First(byte[] temp)
	{
		index = -1;
		return Next(temp);
	}

	public bool Last(byte[] temp)
	{
		index = Length - temp.Length + 1;
		return Previous(temp);
	}

	public void Skip(int step)
	{
		index += step;
	}

	public byte ReadByte()
	{
		return (byte)((index >= 0 && index < Length) ? data[index++] : 0);
	}

	public ushort ReadUShort()
	{
		if (index < 0 || index + 2 > Length)
		{
			return 0;
		}
		index += 2;
		return (ushort)(data[index - 2] + (data[index - 1] << 8));
	}

	public short ReadShort()
	{
		return (short)ReadUShort();
	}

	public uint ReadUInt()
	{
		if (index < 0 || index + 4 > Length)
		{
			return 0u;
		}
		return (uint)(ReadUShort() + (ReadUShort() << 16));
	}

	public int ReadInt()
	{
		return (int)ReadUInt();
	}

	public ulong ReadULong()
	{
		if (index < 0 || index + 8 > Length)
		{
			return 0uL;
		}
		return ReadUInt() + ((ulong)ReadUInt() << 32);
	}

	public long ReadLong()
	{
		return (long)ReadULong();
	}

	public bool ReadBytes(byte[] _data, int _index, int _count)
	{
		if (index < 0 || index + _count > Length)
		{
			return false;
		}
		Array.Copy(data, index, _data, _index, _count);
		index += _count;
		return true;
	}
}
