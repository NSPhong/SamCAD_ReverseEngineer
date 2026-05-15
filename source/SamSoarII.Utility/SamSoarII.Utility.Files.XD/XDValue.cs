using System;
using System.Linq;

namespace SamSoarII.Utility.Files.XD;

public class XDValue
{
	private Enum_XDValue e;

	private string name;

	private string comment;

	private string bas;

	private int ofs;

	private string ita;

	public Enum_XDValue E => e;

	public string Name => name;

	public string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	public string Bas => bas;

	public int Ofs => ofs;

	public string Ita => ita;

	public XDValue(string _name)
	{
		name = _name;
		comment = string.Empty;
		ita = null;
		int num = 0;
		int i = 0;
		num = i;
		for (; i < name.Length && !char.IsDigit(name[i]); i++)
		{
		}
		bas = ((i > num) ? name.Substring(num, i - num) : string.Empty);
		if (!Enum.TryParse<Enum_XDValue>(bas, out e) && bas.StartsWith("H"))
		{
			bas = "H";
			e = Enum_XDValue.H;
			num = 1;
		}
		if (e == Enum_XDValue.H)
		{
			if (num < name.Length)
			{
				ofs = ValueConverter.NBase_Parse(name.Substring(num), 16);
			}
			return;
		}
		if (e == Enum_XDValue.B)
		{
			if (num < name.Length)
			{
				ofs = ValueConverter.NBase_Parse(name.Substring(num), 2);
			}
			return;
		}
		num = i;
		for (; i < name.Length && char.IsDigit(name[i]); i++)
		{
		}
		if (i > num)
		{
			if (e == Enum_XDValue.X || e == Enum_XDValue.Y)
			{
				ofs = ValueConverter.NBase_Parse(name.Substring(num, i - num), 8);
			}
			else
			{
				int.TryParse(name.Substring(num, i - num), out ofs);
			}
		}
		if (i < name.Length && name[i] == '[' && name.LastOrDefault() == ']')
		{
			ita = name.Substring(i + 1, name.Length - (i + 1) - 1);
		}
	}

	public bool IsConst()
	{
		return e == Enum_XDValue.K || e == Enum_XDValue.H;
	}

	public bool IsVar()
	{
		return !IsConst();
	}
}
