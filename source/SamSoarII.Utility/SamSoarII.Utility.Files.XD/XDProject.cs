using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.XD;

public class XDProject
{
	private string name;

	private List<XDLadder> ladders;

	private List<XDFuncBlock> funcblocks;

	private Dictionary<string, XDValue> values;

	private List<XDValueInteract> interacts;

	private XDDevice device;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public IList<XDLadder> Ladders => ladders;

	public IList<XDFuncBlock> FuncBlocks => funcblocks;

	public IDictionary<string, XDValue> Values => values;

	public IList<XDValueInteract> Interacts => interacts;

	public XDDevice Device
	{
		get
		{
			return device;
		}
		set
		{
			_SetDevice(value);
		}
	}

	public XDProject()
	{
		name = "XDPro";
		ladders = new List<XDLadder>();
		funcblocks = new List<XDFuncBlock>();
		values = new Dictionary<string, XDValue>();
		interacts = new List<XDValueInteract>();
		device = null;
	}

	protected void _SetDevice(XDDevice value)
	{
		device = value;
		if (device != null)
		{
			interacts = (from vr in device.VRs.Concat(device.Parent.VRs)
				select new XDValueInteract(vr)).ToList();
		}
	}

	public void UpdateInteract()
	{
		foreach (XDValueInteract interact in interacts)
		{
			interact.Reset();
		}
		foreach (XDLadder ladder in ladders)
		{
			foreach (XDUnit child in ladder.Children)
			{
				foreach (string arg in child.Args)
				{
					XDValue value = null;
					if (!values.TryGetValue(arg, out value) || value.IsConst())
					{
						continue;
					}
					foreach (XDValueInteract interact2 in interacts)
					{
						interact2.Add(child, value);
					}
				}
			}
		}
	}
}
