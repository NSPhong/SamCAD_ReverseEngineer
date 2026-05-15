using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SamSoarII.Core.Files;

public class DownloadWriter : IDisposable
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

	private List<long> ps;

	private bool isencrypt;

	private byte deskey;

	public bool IsDisposed => isdisposed;

	public DownloadWriter(string _filename)
	{
		s = File.Open(_filename, FileMode.Create, FileAccess.ReadWrite);
		ps = new List<long>();
		isencrypt = false;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			s.Close();
		}
	}

	public void EncryptBegin(byte _deskey)
	{
		isencrypt = true;
		deskey = _deskey;
	}

	public void EncryptEnd()
	{
		isencrypt = false;
	}

	public void Write(byte b)
	{
		if (isencrypt)
		{
			byte b2 = 0;
			byte b3 = 1;
			int num = 7;
			for (int i = 0; i < 8; i++)
			{
				b2 = ((num <= 0) ? ((byte)(b2 | (byte)((b & b3) >> -num))) : ((byte)(b2 | (byte)((b & b3) << num))));
				b3 <<= 1;
				num -= 2;
			}
			b = b2;
			b ^= deskey;
		}
		s.WriteByte(b);
	}

	public void Write(short si)
	{
		Write((byte)(si & 0xFF));
		Write((byte)((si >> 8) & 0xFF));
	}

	public void Write(ushort usi)
	{
		Write((short)usi);
	}

	public void Write(int i)
	{
		Write((short)(i & 0xFFFF));
		Write((short)((i >> 16) & 0xFFFF));
	}

	public void Write(uint ui)
	{
		Write((int)ui);
	}

	public void Write(long l)
	{
		Write((int)(l & 0xFFFFFFFFu));
		Write((int)((l >> 32) & 0xFFFFFFFFu));
	}

	public void Write(ulong ul)
	{
		Write((long)ul);
	}

	public unsafe void Write(float f)
	{
		Write(*(int*)(&f));
	}

	public unsafe void Write(double d)
	{
		Write(*(long*)(&d));
	}

	public void Write(byte[] data)
	{
		foreach (byte b in data)
		{
			Write(b);
		}
	}

	public void Write(Stream _s)
	{
		byte[] array = new byte[_s.Length];
		_s.Position = 0L;
		_s.Read(array, 0, array.Length);
		Write(array);
	}

	public void Write8(string text)
	{
		Encoding encoding = Encoding.Default;
		byte[] bytes = encoding.GetBytes(text);
		Write((byte)bytes.Length);
		Write(bytes);
	}

	public void Write16(string text)
	{
		Encoding encoding = Encoding.Default;
		byte[] bytes = encoding.GetBytes(text);
		Write((ushort)bytes.Length);
		Write(bytes);
	}

	public void Write32(string text)
	{
		Encoding encoding = Encoding.Default;
		byte[] bytes = encoding.GetBytes(text);
		Write((uint)bytes.Length);
		Write(bytes);
	}

	public void WriteI(string text)
	{
		switch (text[0])
		{
		case 'K':
			Write((ushort)16);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'H':
			Write((ushort)17);
			try
			{
				Write(int.Parse(text.Substring(1), NumberStyles.HexNumber));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'D':
			Write((ushort)40000);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'M':
			Write((ushort)30000);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'X':
			Write((ushort)0);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'Y':
			Write((ushort)10000);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'S':
			Write((ushort)50000);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'V':
			Write((ushort)48192);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'Z':
			Write((ushort)48200);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'C':
			if (text[1] == 'V')
			{
				Write((ushort)60000);
				try
				{
					Write(int.Parse(text.Substring(2)));
					break;
				}
				catch (Exception)
				{
					Write(0);
					break;
				}
			}
			Write((ushort)60512);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'T':
			if (text[1] == 'V')
			{
				Write((ushort)60256);
				try
				{
					Write(int.Parse(text.Substring(2)));
					break;
				}
				catch (Exception)
				{
					Write(0);
					break;
				}
			}
			Write((ushort)60768);
			try
			{
				Write(int.Parse(text.Substring(1)));
				break;
			}
			catch (Exception)
			{
				Write(0);
				break;
			}
		case 'A':
			if (text[1] == 'I')
			{
				Write((ushort)20000);
				try
				{
					Write(int.Parse(text.Substring(2)));
					break;
				}
				catch (Exception)
				{
					Write(0);
					break;
				}
			}
			if (text[1] == 'O')
			{
				Write((ushort)20512);
				try
				{
					Write(int.Parse(text.Substring(2)));
					break;
				}
				catch (Exception)
				{
					Write(0);
					break;
				}
			}
			break;
		}
	}

	public void WriteF(string text)
	{
		char c = text[0];
		char c2 = c;
		if (c2 == 'K')
		{
			Write((ushort)16);
			try
			{
				Write(float.Parse(text.Substring(1)));
				return;
			}
			catch (Exception)
			{
				Write(0f);
				return;
			}
		}
		WriteI(text);
	}

	public void Skip(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Write((byte)0);
		}
	}

	public void PushL()
	{
		ps.Add(s.Position);
		Write(0);
	}

	public void PopL()
	{
		if (ps.Count() != 0)
		{
			long num = ps[ps.Count() - 1];
			long position = s.Position;
			ps.RemoveAt(ps.Count() - 1);
			s.Position = num;
			Write((int)(position - num - 4));
			s.Position = position;
		}
	}
}
