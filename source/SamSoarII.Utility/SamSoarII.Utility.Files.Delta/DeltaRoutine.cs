using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaRoutine
{
	private DeltaProject parent;

	private string name;

	private List<DeltaNetwork> networks;

	public DeltaProject Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

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

	public IList<DeltaNetwork> Networks => networks;

	public DeltaRoutine(DeltaProject _parent)
	{
		parent = _parent;
		networks = new List<DeltaNetwork>();
	}

	public void Load(DeltaDMLElement de)
	{
		foreach (DeltaDMLElement item in de.Items)
		{
			string text = item.Name;
			string text2 = text;
			if (text2 == "P_Name")
			{
				name = item.Value;
			}
			if (item.Name.Equals("NETWORK_START"))
			{
				DeltaNetwork deltaNetwork = new DeltaNetwork(this);
				deltaNetwork.Load(item);
				networks.Add(deltaNetwork);
			}
		}
	}
}
