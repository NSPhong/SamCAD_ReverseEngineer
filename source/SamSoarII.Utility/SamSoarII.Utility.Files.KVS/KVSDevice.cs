using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSDevice
{
	public static readonly List<KVSDevice> List;

	private KVSPLCType plctype;

	private List<KVSValuesInfo> vsilist;

	private List<KVSSystemValue> svlist;

	public KVSPLCType PLCType
	{
		get
		{
			return plctype;
		}
		set
		{
			plctype = value;
		}
	}

	public IList<KVSValuesInfo> VSIList => vsilist;

	public IList<KVSSystemValue> SVList => svlist;

	static KVSDevice()
	{
		List = new List<KVSDevice>();
		KVSDevice kVSDevice = null;
		kVSDevice = new KVSDevice
		{
			PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_P16)
		};
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 0u,
			MaxOffset = 79u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 80u,
			MaxOffset = 159u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 160u,
			MaxOffset = 2879u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 2,
			MinOffset = 0u,
			MaxOffset = 250u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 3,
			MinOffset = 0u,
			MaxOffset = 250u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 6,
			MinOffset = 0u,
			MaxOffset = 1999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 8,
			MinOffset = 0u,
			MaxOffset = 31u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 4,
			MinOffset = 0u,
			MaxOffset = 1u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 5,
			MinOffset = 0u,
			MaxOffset = 3u
		});
		foreach (KVSSystemValue item in KVSSystemValue.List_PD_R)
		{
			kVSDevice.SVList.Add(item.Clone());
		}
		foreach (KVSSystemValue item2 in KVSSystemValue.List_PD_DM)
		{
			kVSDevice.SVList.Add(item2.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_P16)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_10);
		foreach (KVSSystemValue item3 in KVSSystemValue.List_D_R)
		{
			kVSDevice.SVList.Add(item3.Clone());
		}
		foreach (KVSSystemValue item4 in KVSSystemValue.List_D_DM)
		{
			kVSDevice.SVList.Add(item4.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_10)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_24);
		List.Add(kVSDevice);
		kVSDevice = new KVSDevice
		{
			PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_700)
		};
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 0u,
			MaxOffset = 79u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 80u,
			MaxOffset = 159u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 160u,
			MaxOffset = 9599u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 1,
			MinOffset = 0u,
			MaxOffset = 639u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 6,
			MinOffset = 0u,
			MaxOffset = 19999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 7,
			MinOffset = 0u,
			MaxOffset = 3999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 8,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 2,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 3,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 4,
			MinOffset = 0u,
			MaxOffset = 1u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 5,
			MinOffset = 0u,
			MaxOffset = 3u
		});
		foreach (KVSSystemValue item5 in KVSSystemValue.List_N_CR)
		{
			kVSDevice.SVList.Add(item5.Clone());
		}
		foreach (KVSSystemValue item6 in KVSSystemValue.List_N_CM)
		{
			kVSDevice.SVList.Add(item6.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_700)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_700_M);
		List.Add(kVSDevice);
		kVSDevice = new KVSDevice
		{
			PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_1000)
		};
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 0u,
			MaxOffset = 79u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 80u,
			MaxOffset = 159u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 160u,
			MaxOffset = 9599u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 17,
			MinOffset = 0u,
			MaxOffset = 7199u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 18,
			MinOffset = 0u,
			MaxOffset = 7199u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 1,
			MinOffset = 0u,
			MaxOffset = 639u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 6,
			MinOffset = 0u,
			MaxOffset = 40534u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 7,
			MinOffset = 0u,
			MaxOffset = 11998u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 23,
			MinOffset = 0u,
			MaxOffset = 19534u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 25,
			MinOffset = 0u,
			MaxOffset = 11766u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 8,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 2,
			MinOffset = 0u,
			MaxOffset = 999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 3,
			MinOffset = 0u,
			MaxOffset = 999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 4,
			MinOffset = 0u,
			MaxOffset = 1u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 5,
			MinOffset = 0u,
			MaxOffset = 3u
		});
		foreach (KVSSystemValue item7 in KVSSystemValue.List_N_CR)
		{
			kVSDevice.SVList.Add(item7.Clone());
		}
		foreach (KVSSystemValue item8 in KVSSystemValue.List_N_CM)
		{
			kVSDevice.SVList.Add(item8.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = new KVSDevice
		{
			PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_3000)
		};
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 0u,
			MaxOffset = 79u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 80u,
			MaxOffset = 159u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 160u,
			MaxOffset = 15999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 17,
			MinOffset = 0u,
			MaxOffset = 7999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 18,
			MinOffset = 0u,
			MaxOffset = 7999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 1,
			MinOffset = 0u,
			MaxOffset = 639u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 27,
			MinOffset = 0u,
			MaxOffset = 16383u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 6,
			MinOffset = 0u,
			MaxOffset = 40534u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 23,
			MinOffset = 0u,
			MaxOffset = 20534u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 25,
			MinOffset = 0u,
			MaxOffset = 32767u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 28,
			MinOffset = 0u,
			MaxOffset = 16383u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 8,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 2,
			MinOffset = 0u,
			MaxOffset = 2499u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 3,
			MinOffset = 0u,
			MaxOffset = 2499u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 4,
			MinOffset = 0u,
			MaxOffset = 1u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 5,
			MinOffset = 0u,
			MaxOffset = 3u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 7,
			MinOffset = 0u,
			MaxOffset = 5999u
		});
		foreach (KVSSystemValue item9 in KVSSystemValue.List_N_CR)
		{
			kVSDevice.SVList.Add(item9.Clone());
		}
		foreach (KVSSystemValue item10 in KVSSystemValue.List_N_CM)
		{
			kVSDevice.SVList.Add(item10.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_3000)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_5000);
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_5000)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_5500);
		List.Add(kVSDevice);
		kVSDevice = new KVSDevice
		{
			PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_N14)
		};
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 0u,
			MaxOffset = 79u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 80u,
			MaxOffset = 159u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 0,
			MinOffset = 160u,
			MaxOffset = 9599u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 17,
			MinOffset = 0u,
			MaxOffset = 7999u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 18,
			MinOffset = 0u,
			MaxOffset = 2879u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 27,
			MinOffset = 0u,
			MaxOffset = 8191u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 1,
			MinOffset = 0u,
			MaxOffset = 1439u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 6,
			MinOffset = 0u,
			MaxOffset = 27767u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 27,
			MinOffset = 0u,
			MaxOffset = 16383u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 8,
			MinOffset = 0u,
			MaxOffset = 511u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 2,
			MinOffset = 0u,
			MaxOffset = 411u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 3,
			MinOffset = 0u,
			MaxOffset = 205u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 4,
			MinOffset = 0u,
			MaxOffset = 3u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 5,
			MinOffset = 0u,
			MaxOffset = 7u
		});
		kVSDevice.VSIList.Add(new KVSValuesInfo
		{
			Code = 7,
			MinOffset = 0u,
			MaxOffset = 8999u
		});
		foreach (KVSSystemValue item11 in KVSSystemValue.List_N_CR)
		{
			kVSDevice.SVList.Add(item11.Clone());
		}
		foreach (KVSSystemValue item12 in KVSSystemValue.List_N_CM)
		{
			kVSDevice.SVList.Add(item12.Clone());
		}
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_N14)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_N24);
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_N14)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_N40);
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_N14)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_N60);
		List.Add(kVSDevice);
		kVSDevice = List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == Enum_KVSPLCType.KV_N14)?.Clone() ?? new KVSDevice();
		kVSDevice.PLCType = KVSPLCType.Items.FirstOrDefault((KVSPLCType pt) => pt.E == Enum_KVSPLCType.KV_NC32);
		List.Add(kVSDevice);
	}

	public KVSDevice()
	{
		vsilist = new List<KVSValuesInfo>();
		svlist = new List<KVSSystemValue>();
	}

	public override string ToString()
	{
		return plctype?.ToString() ?? "<null>";
	}

	public KVSDevice Clone()
	{
		KVSDevice kVSDevice = new KVSDevice();
		foreach (KVSValuesInfo item in vsilist)
		{
			kVSDevice.VSIList.Add(item.Clone());
		}
		foreach (KVSSystemValue item2 in svlist)
		{
			kVSDevice.SVList.Add(item2.Clone());
		}
		return kVSDevice;
	}
}
