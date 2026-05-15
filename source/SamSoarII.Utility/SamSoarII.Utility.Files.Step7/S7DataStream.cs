using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace SamSoarII.Utility.Files.Step7;

public class S7DataStream : IDisposable
{
	private IntPtr p;

	private int size;

	private int index;

	public int Size => size;

	public int Index => index;

	public S7DataStream(IntPtr _p, int _size)
	{
		p = _p;
		size = _size;
		index = 0;
	}

	public void Dispose()
	{
		p = IntPtr.Zero;
		size = 0;
		index = 0;
	}

	public unsafe void PrintData(string fpbin)
	{
		FileStream fileStream = null;
		try
		{
			fileStream = File.Create(fpbin);
		}
		catch (IOException ex)
		{
			MessageBox.Show(ex.Message);
			return;
		}
		byte* ptr = (byte*)(void*)p;
		for (int i = 0; i < size; i++)
		{
			fileStream.WriteByte(ptr[i]);
		}
		fileStream.Close();
	}

	public unsafe void PrintText(string fptxt)
	{
		StreamWriter streamWriter = null;
		try
		{
			streamWriter = new StreamWriter(fptxt, append: false);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return;
		}
		byte* ptr = (byte*)(void*)p;
		int num = size >> 5;
		int num2 = size & 0x1F;
		for (int i = 0; i < num; i++)
		{
			streamWriter.Write("0x{0:X8}\n", i << 5);
			for (int j = 0; j < 32; j++)
			{
				streamWriter.Write(" {0:X2}", *(ptr++));
			}
			streamWriter.Write("\r\n");
		}
		if (num2 > 0)
		{
			streamWriter.Write("0x{0:X8}\n", num2 << 5);
			for (int k = 0; k < num2; k++)
			{
				streamWriter.Write(" {0:X2}", *(ptr++));
			}
			streamWriter.Write("\r\n");
		}
		streamWriter.Close();
	}

	public void Start(int _index)
	{
		index = System.Math.Min(System.Math.Max(_index, 0), size - 1);
	}

	public void Move(int _offset)
	{
		index += _offset;
		index = System.Math.Min(System.Math.Max(index, 0), size - 1);
	}

	public unsafe byte GetB(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 1 > size)
		{
			return 0;
		}
		return *(byte*)(void*)(p + index + _offset);
	}

	public unsafe short GetS(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 2 > size)
		{
			return 0;
		}
		return *(short*)(void*)(p + index + _offset);
	}

	public unsafe ushort GetW(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 2 > size)
		{
			return 0;
		}
		return *(ushort*)(void*)(p + index + _offset);
	}

	public unsafe int GetI(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 4 > size)
		{
			return 0;
		}
		return *(int*)(void*)(p + index + _offset);
	}

	public unsafe uint GetU(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 4 > size)
		{
			return 0u;
		}
		return *(uint*)(void*)(p + index + _offset);
	}

	public unsafe float GetF(int _offset)
	{
		if (index + _offset < 0 || index + _offset + 4 > size)
		{
			return 0f;
		}
		return *(float*)(void*)(p + index + _offset);
	}

	public void GetBytes(int _offset, byte[] bs)
	{
		if (index + _offset >= 0 && index + _offset + bs.Length <= size)
		{
			Marshal.Copy(p + index + _offset, bs, 0, bs.Length);
		}
	}

	public unsafe string GetString(int _offset, out int _size)
	{
		if ((_size = GetW(_offset)) == 0)
		{
			return string.Empty;
		}
		_size = System.Math.Min(_size, size - index - _offset - 2);
		_size = System.Math.Max(_size, 0);
		if (_size == 0)
		{
			return string.Empty;
		}
		return new string((sbyte*)(void*)(p + index + _offset + 2), 0, _size, Encoding.Default);
	}

	public unsafe string GetString(int _offset, int _size)
	{
		_size = System.Math.Min(_size, size - index - _offset);
		_size = System.Math.Max(_size, 0);
		if (_size == 0)
		{
			return string.Empty;
		}
		return new string((sbyte*)(void*)(p + index + _offset), 0, _size, Encoding.Default);
	}
}
