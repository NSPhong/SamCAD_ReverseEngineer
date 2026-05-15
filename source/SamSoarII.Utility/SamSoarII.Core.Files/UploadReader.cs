using System;
using System.IO;
using System.Text;

namespace SamSoarII.Core.Files;

public class UploadReader : IDisposable
{
	private const ushort PLC_REG_X = 0;

	private const ushort PLC_REG_Y = 10000;

	private const ushort PLC_REG_AI = 20000;

	private const ushort PLC_REG_AO = 20512;

	private const ushort PLC_REG_M = 30000;

	private const ushort PLC_REG_S = 50000;

	private const ushort PLC_REG_D = 40000;

	private const ushort PLC_REG_T = 60768;

	private const ushort PLC_REG_C = 60512;

	private const ushort PLC_REG_TV = 60256;

	private const ushort PLC_REG_CV = 60000;

	private const ushort PLC_REG_K = 16;

	private const ushort PLC_REG_H = 17;

	private const ushort PLC_REG_V = 48192;

	private const ushort PLC_REG_Z = 48200;

	private bool isdisposed = false;

	private Stream s;

	private bool isdecrypt;

	private byte deskey;

	public bool IsDisposed => isdisposed;

	public UploadReader(string _filename)
	{
		s = File.Open(_filename, FileMode.Open, FileAccess.ReadWrite);
		isdecrypt = false;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			s.Close();
		}
	}

	public void DecryptBegin(byte _deskey)
	{
		isdecrypt = true;
		deskey = _deskey;
	}

	public void DecryptEnd()
	{
		isdecrypt = false;
	}

	public void Read(byte[] data)
	{
		for (int i = 0; i < data.Length; i++)
		{
			data[i] = Read8();
		}
	}

	public byte Read8()
	{
		byte b = (byte)s.ReadByte();
		if (isdecrypt)
		{
			byte b2 = 0;
			byte b3 = 1;
			int num = 7;
			b ^= deskey;
			for (int i = 0; i < 8; i++)
			{
				b2 = ((num <= 0) ? ((byte)(b2 | (byte)((b & b3) >> -num))) : ((byte)(b2 | (byte)((b & b3) << num))));
				b3 <<= 1;
				num -= 2;
			}
			b = b2;
		}
		return b;
	}

	public short Read16()
	{
		short num = Read8();
		return (short)(num | (short)(Read8() << 8));
	}

	public ushort Read16U()
	{
		return (ushort)Read16();
	}

	public int Read32()
	{
		int num = Read16U();
		return num | (Read16U() << 16);
	}

	public uint Read32U()
	{
		return (uint)Read32();
	}

	public long Read64()
	{
		long num = Read32U();
		return num | (long)((ulong)Read32U() << 32);
	}

	public ulong Read64U()
	{
		return (ulong)Read64();
	}

	public unsafe float Read32F()
	{
		int num = Read32();
		return *(float*)(&num);
	}

	public unsafe double Read64F()
	{
		long num = Read64();
		return *(double*)(&num);
	}

	public string Read8S()
	{
		Encoding encoding = Encoding.Default;
		byte b = Read8();
		byte[] array = new byte[b];
		Read(array);
		return encoding.GetString(array);
	}

	public string Read16S()
	{
		Encoding encoding = Encoding.Default;
		ushort num = Read16U();
		byte[] array = new byte[num];
		Read(array);
		return encoding.GetString(array);
	}

	public string Read32S()
	{
		Encoding encoding = Encoding.Default;
		uint num = Read32U();
		byte[] array = new byte[num];
		Read(array);
		return encoding.GetString(array);
	}

	public string ReadI()
	{
		ushort num = Read16U();
		int num2 = Read32();
		return num switch
		{
			16 => $"K{num2}", 
			17 => $"H{num2:x}", 
			40000 => $"D{num2}", 
			30000 => $"M{num2}", 
			0 => $"X{num2}", 
			10000 => $"Y{num2}", 
			50000 => $"S{num2}", 
			48192 => $"V{num2}", 
			48200 => $"Z{num2}", 
			60512 => $"C{num2}", 
			60768 => $"T{num2}", 
			20000 => $"AI{num2}", 
			20512 => $"AO{num2}", 
			60000 => $"CV{num2}", 
			60256 => $"TV{num2}", 
			_ => string.Empty, 
		};
	}

	public unsafe string ReadF()
	{
		ushort num = Read16U();
		int num2 = Read32();
		return num switch
		{
			16 => $"K{*(float*)(&num2)}", 
			17 => $"H{num2:x}", 
			40000 => $"D{num2}", 
			30000 => $"M{num2}", 
			0 => $"X{num2}", 
			10000 => $"Y{num2}", 
			50000 => $"S{num2}", 
			48192 => $"V{num2}", 
			48200 => $"Z{num2}", 
			60512 => $"C{num2}", 
			60768 => $"T{num2}", 
			20000 => $"AI{num2}", 
			20512 => $"AO{num2}", 
			60000 => $"CV{num2}", 
			60256 => $"TV{num2}", 
			_ => string.Empty, 
		};
	}

	public void Skip(int count)
	{
		for (int i = 0; i < count; i++)
		{
			s.ReadByte();
		}
	}

	public byte[] ReadAll()
	{
		byte[] array = new byte[s.Length - s.Position];
		Read(array);
		return array;
	}
}
