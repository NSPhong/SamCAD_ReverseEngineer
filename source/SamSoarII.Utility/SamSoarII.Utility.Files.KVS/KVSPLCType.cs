using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSPLCType
{
	public static readonly List<KVSPLCType> Items = (from Enum_KVSPLCType e in Enum.GetValues(typeof(Enum_KVSPLCType))
		select new KVSPLCType(e)).ToList();

	private Enum_KVSPLCType e;

	public Enum_KVSPLCType E => e;

	public KVSPLCType(Enum_KVSPLCType _e)
	{
		e = _e;
	}

	public override string ToString()
	{
		return e switch
		{
			Enum_KVSPLCType.KV_5500 => "KV-5500", 
			Enum_KVSPLCType.KV_5000 => "KV-5000", 
			Enum_KVSPLCType.KV_3000 => "KV-3000", 
			Enum_KVSPLCType.KV_1000 => "KV-1000", 
			Enum_KVSPLCType.KV_700_M => "KV-700-M+", 
			Enum_KVSPLCType.KV_700 => "KV-700", 
			Enum_KVSPLCType.KV_NC32 => "KV-NC32", 
			Enum_KVSPLCType.KV_N60 => "KV-N60", 
			Enum_KVSPLCType.KV_N40 => "KV-N40", 
			Enum_KVSPLCType.KV_N24 => "KV-N24", 
			Enum_KVSPLCType.KV_N14 => "KV-N14", 
			Enum_KVSPLCType.KV_24 => "KV-24(40)", 
			Enum_KVSPLCType.KV_10 => "KV-10(16)", 
			Enum_KVSPLCType.KV_P16 => "KV-P16", 
			_ => base.ToString(), 
		};
	}
}
