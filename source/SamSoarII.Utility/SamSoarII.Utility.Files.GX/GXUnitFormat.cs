using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXUnitFormat
{
	private GXInstFormat inst;

	private GXArgFormat[] args;

	protected bool isinput;

	protected bool isoutput;

	protected bool isand;

	protected bool isor;

	protected bool ispulse;

	public GXInstFormat Inst => inst;

	public GXArgFormat[] Args => args;

	public bool IsInput => isinput;

	public bool IsOutput => isoutput;

	public bool IsAnd => isand;

	public bool IsOr => isor;

	public bool IsPulse => ispulse;

	public virtual bool CanPulse => true;

	public GXUnitFormat(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		inst = _inst;
		args = _args.Select((GXArgFormat a) => a.Clone()).ToArray();
		isinput = false;
		isand = false;
		isor = false;
		isoutput = false;
		ispulse = false;
	}

	protected virtual void AppendOne(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas, ref int i, ref int j, ref int k)
	{
		string empty = string.Empty;
		GXSystemRemap value = null;
		if (args[j].LastFit is GXValueFormatDot)
		{
			empty = args[j].ToStringDot(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (args[j].LastFit is GXValueFormatCom)
		{
			empty = args[j].ToStringCom(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (args[j] is GXArgFormatSTR && lens[i] == 0)
		{
			empty = ((GXArgFormatSTR)args[j]).ToString(datas[k]);
			k++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 244)
		{
			empty = args[j].ToStringV(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 240)
		{
			empty = args[j].ToStringZ(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else
		{
			empty = args[j].ToString(hexs[i], lens[i]);
		}
		GXPLCSeries current = GXPLCSeries.Current;
		if (current != null && current.Remap?.TryGetValue(empty, out value) == true)
		{
			empty = value.To ?? empty;
		}
		sb.Append(empty);
	}

	protected virtual void AppendOne_OverflowClear(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas, ref int i, ref int j, ref int k)
	{
		string empty = string.Empty;
		GXSystemRemap value = null;
		if (args[j].LastFit is GXValueFormatDot)
		{
			empty = args[j].ToStringDot_OverflowClear(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (args[j].LastFit is GXValueFormatCom)
		{
			empty = args[j].ToStringCom_OverflowClear(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (args[j] is GXArgFormatSTR && lens[i] == 0)
		{
			empty = ((GXArgFormatSTR)args[j]).ToString(datas[k]);
			k++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 244)
		{
			empty = args[j].ToStringV_OverflowClear(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 240)
		{
			empty = args[j].ToStringZ_OverflowClear(hexs[i], lens[i], hexs[i + 1], lens[i + 1]);
			i++;
		}
		else
		{
			empty = args[j].ToString_OverflowClear(hexs[i], lens[i]);
		}
		GXPLCSeries current = GXPLCSeries.Current;
		if (current != null && current.Remap?.TryGetValue(empty, out value) == true)
		{
			empty = value.To ?? empty;
		}
		sb.Append(empty);
	}

	protected virtual bool IsOverflowOne(IList<long> hexs, IList<int> lens, IList<byte[]> datas, ref int i, ref int j, ref int k)
	{
		bool result = args[j].IsOverflow(hexs[i], lens[i]);
		if (args[j].LastFit is GXValueFormatDot)
		{
			i++;
		}
		else if (args[j].LastFit is GXValueFormatCom)
		{
			i++;
		}
		else if (args[j] is GXArgFormatSTR && lens[i] == 0)
		{
			k++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 244)
		{
			i++;
		}
		else if (((hexs[i] >> 8 * (lens[i] - 1)) & 0xFF) == 240)
		{
			i++;
		}
		return result;
	}

	public virtual void AppendA(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		if (isoutput)
		{
			sb.Append("A ");
		}
		_Append(sb, hexs, lens, datas);
	}

	protected virtual void _Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		sb.Append(inst.Name);
		int i = 0;
		int j = 0;
		int k = 0;
		while (i < hexs.Count())
		{
			sb.Append(" ");
			AppendOne(sb, hexs, lens, datas, ref i, ref j, ref k);
			i++;
			j++;
		}
	}

	protected virtual void _Append_OverflowClear(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		sb.Append(inst.Name);
		int i = 0;
		int j = 0;
		int k = 0;
		while (i < hexs.Count())
		{
			sb.Append(" ");
			AppendOne_OverflowClear(sb, hexs, lens, datas, ref i, ref j, ref k);
			i++;
			j++;
		}
	}

	public virtual void Append(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		_Append(sb, hexs, lens, datas);
	}

	public virtual void Append_OverflowClear(StringBuilder sb, IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		_Append_OverflowClear(sb, hexs, lens, datas);
	}

	public virtual bool IsOverflow(IList<long> hexs, IList<int> lens, IList<byte[]> datas)
	{
		int i = 0;
		int j = 0;
		int k = 0;
		while (i < hexs.Count())
		{
			if (IsOverflowOne(hexs, lens, datas, ref i, ref j, ref k))
			{
				return true;
			}
			i++;
			j++;
		}
		return false;
	}

	public virtual GXUnitFormat Create(GXInstFormat _inst, IEnumerable<GXArgFormat> _args)
	{
		return new GXUnitFormat(_inst, _args);
	}

	public virtual GXUnitFormat Pulselize()
	{
		GXInstFormat gXInstFormat = inst.Pulselize();
		GXUnitFormat gXUnitFormat = Create(gXInstFormat, args);
		gXUnitFormat.isinput = isinput;
		gXUnitFormat.isoutput = isoutput;
		gXUnitFormat.ispulse = true;
		return gXUnitFormat;
	}

	public virtual GXUnitFormat Clone()
	{
		GXUnitFormat gXUnitFormat = Create(inst, args);
		gXUnitFormat.isinput = isinput;
		gXUnitFormat.isoutput = isoutput;
		gXUnitFormat.ispulse = ispulse;
		return gXUnitFormat;
	}
}
