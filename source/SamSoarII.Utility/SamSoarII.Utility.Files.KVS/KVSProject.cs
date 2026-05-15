using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.KVS;

public class KVSProject
{
	protected KVSProInfo proinfo;

	protected KVSDevice device;

	protected KVSModInfoList modinfos;

	protected KVSModule mainmodule;

	protected List<KVSModule> holdmodules;

	protected List<KVSModule> initmodules;

	protected List<KVSModule> modules;

	protected List<KVSModule> periodmodules;

	protected List<KVSMacro> macros;

	protected List<KVSMacro> holdmacros;

	protected SortedDictionary<ulong, KVSValue> values;

	protected Dictionary<string, KVSGlobalLabel> labels;

	public KVSProInfo ProInfo
	{
		get
		{
			return proinfo;
		}
		set
		{
			proinfo = value;
		}
	}

	public KVSDevice Device
	{
		get
		{
			return device;
		}
		set
		{
			device = value;
			SetupDev(device);
		}
	}

	public KVSModInfoList ModInfos
	{
		get
		{
			return modinfos;
		}
		set
		{
			modinfos = value;
		}
	}

	public KVSModule MainModule
	{
		get
		{
			return mainmodule;
		}
		set
		{
			mainmodule = value;
		}
	}

	public IList<KVSModule> HoldModules => holdmodules;

	public IList<KVSModule> InitModules => initmodules;

	public IList<KVSModule> Modules => modules;

	public IList<KVSModule> PeriodModules => periodmodules;

	public IList<KVSMacro> Macros => macros;

	public IList<KVSMacro> HoldMacros => holdmacros;

	public IDictionary<ulong, KVSValue> Values => values;

	public IDictionary<string, KVSGlobalLabel> Labels => labels;

	public string Name => proinfo?.Name ?? string.Empty;

	public KVSProject()
	{
		holdmodules = new List<KVSModule>();
		initmodules = new List<KVSModule>();
		modules = new List<KVSModule>();
		periodmodules = new List<KVSModule>();
		macros = new List<KVSMacro>();
		holdmacros = new List<KVSMacro>();
		values = new SortedDictionary<ulong, KVSValue>();
		labels = new Dictionary<string, KVSGlobalLabel>();
	}

	public void Setup(KVSReport rep)
	{
		if (rep == null)
		{
			return;
		}
		if (rep.ProInfo != null)
		{
			ProInfo = rep.ProInfo.FirstOrDefault();
			Enum_KVSPLCType ept = (Enum_KVSPLCType)ProInfo.Device;
			Device = KVSDevice.List.FirstOrDefault((KVSDevice _dev) => _dev.PLCType.E == ept);
		}
		if (rep.ModInfo != null)
		{
			ModInfos = rep.ModInfo;
		}
		if (rep.VCLists != null)
		{
			foreach (KVSValueCommentList vCList in rep.VCLists)
			{
				foreach (KVSValueComment item in vCList)
				{
					ushort code = item.Code;
					uint offset = (uint)item.Offset;
					KVSValue value = GetValue(code, offset);
					value.Comment = item.Comment;
				}
			}
		}
		if (rep.GLList != null)
		{
			foreach (KVSGlobalLabel gL in rep.GLList)
			{
				ushort element = gL.Element;
				uint offset2 = gL.Offset;
				KVSValue value2 = GetValue(element, offset2);
				value2.Labels.Add(gL);
				if (!labels.ContainsKey(gL.Name))
				{
					labels.Add(gL.Name, gL);
				}
			}
		}
		if (rep.GLCList == null)
		{
			return;
		}
		foreach (KVSGlobalLabel value3 in labels.Values)
		{
			if (value3.CMID >= 0 && value3.CMID < rep.GLCList.Count())
			{
				value3.Comment = rep.GLCList[value3.CMID].Comment;
			}
		}
	}

	public void SetupMod(KVSReport rep)
	{
		if (rep != null)
		{
			Setup(rep);
			KVSModule kVSModule = new KVSModule();
			kVSModule.Setup(rep);
			AddModule(kVSModule);
		}
	}

	public void SetupMcr(KVSReport rep)
	{
		if (rep != null)
		{
			Setup(rep);
			KVSMacro kVSMacro = new KVSMacro();
			kVSMacro.Setup(rep);
			AddMacro(kVSMacro);
		}
	}

	public void SetupDev(KVSDevice dev)
	{
		if (dev == null)
		{
			return;
		}
		foreach (KVSSystemValue sV in dev.SVList)
		{
			ushort code = sV.Code;
			uint offset = sV.Offset;
			KVSValue value = GetValue(code, offset);
			value.SystemValue = sV;
		}
	}

	public KVSValue GetValue(ushort code, uint offset)
	{
		ulong key = ((ulong)code << 32) + offset;
		KVSValue value = null;
		if (!values.TryGetValue(key, out value))
		{
			value = new KVSValue
			{
				Code = code,
				Offset = offset
			};
			values.Add(key, value);
		}
		return value;
	}

	public void RegisterLadder(KVSLadder ladder)
	{
		foreach (KVSUnit child in ladder.Children)
		{
			foreach (KVSArg arg in child.Args)
			{
				ushort codeBase = arg.CodeBase;
				uint offsetBase = arg.OffsetBase;
				KVSValue value = GetValue(codeBase, offsetBase);
				value.Args.Add(arg);
				switch (child.Code)
				{
				case 52:
				case 53:
				case 54:
					if (arg.ID == 0)
					{
						KVSValue value3 = GetValue(2, offsetBase);
						value3.Args.Add(arg);
					}
					break;
				case 55:
				case 57:
					if (arg.ID == 0)
					{
						KVSValue value2 = GetValue(3, offsetBase);
						value2.Args.Add(arg);
					}
					break;
				}
			}
		}
	}

	public void AddModule(KVSModule mod)
	{
		KVSModInfo kVSModInfo = ModInfos?.FirstOrDefault((KVSModInfo _mi) => _mi.Name.Equals(mod.Name));
		if (kVSModInfo != null)
		{
			if (mod.Name.Equals("Main"))
			{
				MainModule = mod;
			}
			if (kVSModInfo.Code == 0)
			{
				holdmodules.Add(mod);
			}
			if (kVSModInfo.Code == 1)
			{
				initmodules.Add(mod);
			}
			if (kVSModInfo.Code == 2)
			{
				modules.Add(mod);
			}
			if (kVSModInfo.Code == 3)
			{
				periodmodules.Add(mod);
			}
			if (mod.Ladder != null)
			{
				RegisterLadder(mod.Ladder);
			}
		}
	}

	public void AddMacro(KVSMacro mcr)
	{
		KVSModInfo kVSModInfo = ModInfos?.FirstOrDefault((KVSModInfo _mi) => _mi.Name.Equals(mcr.Name));
		if (kVSModInfo != null)
		{
			if (kVSModInfo.Code == 5)
			{
				holdmacros.Add(mcr);
			}
			if (kVSModInfo.Code == 4)
			{
				macros.Add(mcr);
			}
			if (mcr.Ladder != null)
			{
				RegisterLadder(mcr.Ladder);
			}
		}
	}
}
