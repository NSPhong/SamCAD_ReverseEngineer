using System;
using System.IO;

namespace SamSoarII.Utility.Files.GX;

public class GXFileBinary : IDisposable
{
	private bool isdisposed = false;

	private byte[] data;

	private int offset;

	private uint skipstart;

	public bool IsDisposed => isdisposed;

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

	public uint SkipStart
	{
		get
		{
			return skipstart;
		}
		set
		{
			skipstart = value;
		}
	}

	public GXFileBinary(string filepath)
	{
		FileStream fileStream = new FileStream(filepath, FileMode.Open);
		data = new byte[fileStream.Length];
		offset = 0;
		skipstart = uint.MaxValue;
		fileStream.Read(data, 0, data.Length);
		fileStream.Close();
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			data = null;
			offset = 0;
			skipstart = 0u;
		}
	}

	public bool Equal(byte[] _data, int _offset)
	{
		for (int i = 0; i < _data.Length; i++)
		{
			if (data[_offset + i] != _data[i])
			{
				return false;
			}
		}
		return true;
	}

	public bool IsLegal()
	{
		return data != null && Offset >= 0 && Offset < data.Length;
	}

	public bool ToFirst(byte[] _data)
	{
		for (int i = 0; i <= data.Length - _data.Length; i++)
		{
			if (Equal(_data, i))
			{
				Offset = i + _data.Length;
				return true;
			}
		}
		return false;
	}

	public bool ToLast(byte[] _data)
	{
		for (int num = data.Length - _data.Length; num >= 0; num--)
		{
			if (Equal(_data, num))
			{
				Offset = num - 1;
				return true;
			}
		}
		return false;
	}

	public bool ToNext(byte[] _data)
	{
		for (int i = Offset; i <= data.Length - _data.Length; i++)
		{
			if (Equal(_data, i))
			{
				Offset = i + _data.Length;
				return true;
			}
		}
		return false;
	}

	public bool ToPrev(byte[] _data)
	{
		for (int num = Offset; num >= 0; num--)
		{
			if (Equal(_data, num))
			{
				Offset = num - 1;
				return true;
			}
		}
		return false;
	}

	public bool ToPrevUnit()
	{
		return _ToPrevUnit() != 0 || _ToPrevUnit_PrevPage();
	}

	protected bool _ToPrevUnit_PrevPage()
	{
		if (!IsLegal())
		{
			return false;
		}
		if ((offset & 0x1FF) != 511)
		{
			return false;
		}
		offset -= 512;
		bool flag = _ToPrevUnit() != 0;
		if (!flag)
		{
			offset += 512;
		}
		return flag;
	}

	protected int _ToPrevUnit(int maxlen = 255)
	{
		if (!IsLegal())
		{
			return 0;
		}
		int result = 1;
		byte b = data[offset];
		if (b == 0 || b > maxlen)
		{
			return 0;
		}
		int num = offset - b + 1;
		if (num < 0 || data[num] != b)
		{
			num -= 512;
			result = 2;
			if (num < 0 || data[num] != b)
			{
				num -= 512;
				result = 3;
				if (num < 0 || data[num] != b)
				{
					return 0;
				}
			}
		}
		offset = num;
		return result;
	}

	public bool ToNextUnit()
	{
		return _ToNextUnit() != 0 || _ToNextUnit_NextPage();
	}

	protected bool _ToNextUnit_NextPage()
	{
		if (!IsLegal())
		{
			return false;
		}
		if ((offset & 0x1FF) != 0)
		{
			return false;
		}
		offset += 512;
		bool flag = _ToNextUnit() != 0;
		if (!flag)
		{
			offset -= 512;
		}
		return flag;
	}

	protected int _ToNextUnit(int maxlen = 255)
	{
		if (!IsLegal())
		{
			return 0;
		}
		int result = 1;
		byte b = data[offset];
		if (b == 0 || b > maxlen)
		{
			return 0;
		}
		int num = offset + b - 1;
		if (num >= data.Length || data[num] != b)
		{
			num += 512;
			result = 2;
			if (num >= data.Length || data[num] != b)
			{
				num += 512;
				result = 3;
				if (num >= data.Length || data[num] != b)
				{
					return 0;
				}
			}
		}
		offset = num;
		return result;
	}

	public bool GetPrevUnit(ref long hex, ref int len)
	{
		return _GetPrevUnit(ref hex, ref len) || _GetPrevUnit_PrevPage(ref hex, ref len);
	}

	protected bool _GetPrevUnit_PrevPage(ref long hex, ref int len)
	{
		if (!IsLegal())
		{
			return false;
		}
		if ((offset & 0x1FF) != 511)
		{
			return false;
		}
		offset -= 512;
		bool flag = _GetPrevUnit(ref hex, ref len);
		if (!flag)
		{
			offset += 512;
		}
		return flag;
	}

	protected bool _GetPrevUnit(ref long hex, ref int len)
	{
		hex = 0L;
		len = 0;
		int num = _ToPrevUnit(10);
		if (num == 0)
		{
			return false;
		}
		len = data[offset];
		for (int i = 0; i < len - 2; i++)
		{
			int num2 = offset + 1 + i;
			if (num == 2 && offset >> 9 != num2 >> 9)
			{
				num2 += 512;
			}
			hex <<= 8;
			hex += data[num2];
		}
		offset--;
		len -= 2;
		return true;
	}

	public byte[] GetPrevBytes()
	{
		byte[] array = null;
		array = _GetPrevBytes();
		if (array != null)
		{
			return array;
		}
		array = _GetPrevBytes_PrevPage();
		if (array != null)
		{
			return array;
		}
		return array;
	}

	protected byte[] _GetPrevBytes()
	{
		int num = _ToPrevUnit();
		if (num == 0)
		{
			return null;
		}
		int num2 = data[offset];
		byte[] array = new byte[num2 - 2];
		for (int i = 0; i < num2 - 2; i++)
		{
			int num3 = offset + 1 + i;
			if (num == 2 && offset >> 9 != num3 >> 9)
			{
				num3 += 512;
			}
			array[i] = data[num3];
		}
		offset--;
		return array;
	}

	protected byte[] _GetPrevBytes_PrevPage()
	{
		if (!IsLegal())
		{
			return null;
		}
		if ((offset & 0x1FF) != 511)
		{
			return null;
		}
		offset -= 512;
		byte[] array = _GetPrevBytes();
		if (array == null)
		{
			offset += 512;
		}
		return array;
	}

	public bool GetNextUnit(ref long hex, ref int len)
	{
		return _GetNextUnit(ref hex, ref len) || _GetNextUnit_NextPage(ref hex, ref len);
	}

	protected bool _GetNextUnit_NextPage(ref long hex, ref int len)
	{
		if (!IsLegal())
		{
			return false;
		}
		if ((offset & 0x1FF) != 0)
		{
			return false;
		}
		offset += 512;
		bool flag = _GetNextUnit(ref hex, ref len);
		if (!flag)
		{
			offset -= 512;
		}
		return flag;
	}

	protected bool _GetNextUnit(ref long hex, ref int len)
	{
		hex = 0L;
		len = 0;
		int num = _ToNextUnit(64);
		if (num == 0)
		{
			return false;
		}
		len = data[offset];
		for (int i = 0; i < len - 2; i++)
		{
			int num2 = offset - len + 2 + i;
			if (num == 2 && offset >> 9 != num2 >> 9)
			{
				num2 -= 512;
			}
			hex <<= 8;
			hex += data[num2];
		}
		offset++;
		len -= 2;
		return true;
	}

	public byte[] GetNextBytes()
	{
		byte[] array = null;
		array = _GetNextBytes();
		if (array != null)
		{
			return array;
		}
		array = _GetNextBytes_NextPage();
		if (array != null)
		{
			return array;
		}
		return array;
	}

	protected byte[] _GetNextBytes()
	{
		int num = _ToNextUnit();
		if (num == 0)
		{
			return null;
		}
		int num2 = data[offset];
		byte[] array = new byte[num2 - 2];
		for (int i = 0; i < num2 - 2; i++)
		{
			int num3 = offset - num2 + 2 + i;
			if (num == 2 && offset >> 9 != num3 >> 9)
			{
				num3 -= 512;
			}
			array[i] = data[num3];
		}
		offset++;
		return array;
	}

	protected byte[] _GetNextBytes_NextPage()
	{
		if (!IsLegal())
		{
			return null;
		}
		if ((offset & 0x1FF) != 0)
		{
			return null;
		}
		offset += 512;
		byte[] array = _GetNextBytes();
		if (array == null)
		{
			offset -= 512;
		}
		return array;
	}

	public byte Read8()
	{
		if (offset >> 9 == skipstart >> 9)
		{
			offset = (int)(skipstart + 512);
		}
		return data[offset++];
	}

	public ushort Read16()
	{
		ushort num = Read8();
		if (offset >> 9 == skipstart >> 9)
		{
			offset = (int)(skipstart + 512);
		}
		return (ushort)(num + (ushort)(Read8() << 8));
	}

	public uint Read32()
	{
		uint num = Read16();
		if (offset >> 9 == skipstart >> 9)
		{
			offset = (int)(skipstart + 512);
		}
		return num + (uint)(Read16() << 16);
	}

	public ulong Read64()
	{
		ulong num = Read32();
		if (offset >> 9 == skipstart >> 9)
		{
			offset = (int)(skipstart + 512);
		}
		return num + ((ulong)Read32() << 32);
	}

	public byte[] ReadBytes(int len)
	{
		if (offset >> 9 == skipstart >> 9)
		{
			offset = (int)(skipstart + 512);
		}
		byte[] array = new byte[len];
		if (offset + len >> 9 >= skipstart >> 9)
		{
			int num = (int)skipstart - offset;
			if (num > 0)
			{
				Array.Copy(data, offset, array, 0, num);
			}
			offset = (int)(skipstart + 512);
			if (len - num > 0)
			{
				Array.Copy(data, offset, array, num, len - num);
			}
			offset += len - num;
		}
		else
		{
			Array.Copy(data, offset, array, 0, len);
			offset += len;
		}
		return array;
	}

	public bool Read32In(uint min, uint max, ref uint ret)
	{
		int num = offset;
		ret = Read32();
		int num2 = offset;
		uint num3 = skipstart;
		if ((ret < min || ret > max) && num >> 9 != num2 >> 9)
		{
			skipstart = (uint)(num2 >> 9 << 9);
			offset = num;
			ret = Read32();
		}
		if (ret < min || ret > max)
		{
			skipstart = num3;
			offset = num;
			return false;
		}
		return true;
	}
}
