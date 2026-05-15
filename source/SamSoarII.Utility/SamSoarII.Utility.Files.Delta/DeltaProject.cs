using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaProject
{
	private DeltaDMLDocument document;

	private string name;

	private string dt;

	private string mt;

	private DeltaDevice device;

	private List<DeltaRoutine> routines;

	private Dictionary<string, DeltaValue> values;

	public DeltaDMLDocument Document => document;

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

	public string DT
	{
		get
		{
			return dt;
		}
		set
		{
			dt = value;
		}
	}

	public string MT
	{
		get
		{
			return mt;
		}
		set
		{
			mt = value;
		}
	}

	public DeltaDevice Device
	{
		get
		{
			return device;
		}
		set
		{
			device = value;
		}
	}

	public IList<DeltaRoutine> Routines => routines;

	public Dictionary<string, DeltaValue> Values => values;

	public DeltaProject(DeltaDMLDocument _document)
	{
		document = _document;
		routines = new List<DeltaRoutine>();
		values = new Dictionary<string, DeltaValue>();
		LoadDocument();
	}

	protected void LoadDocument()
	{
		foreach (DeltaDMLElement item in document.Items)
		{
			if (item.Name.Equals("PROJECT"))
			{
				foreach (DeltaDMLElement item2 in item.Items)
				{
					switch (item2.Name)
					{
					case "Prj_Name":
						name = item2.Value;
						break;
					case "Prj_DT":
						dt = item2.Value;
						break;
					case "Prj_MT":
						mt = item2.Value;
						break;
					}
				}
			}
			if (item.Name.Equals("POU"))
			{
				DeltaRoutine deltaRoutine = new DeltaRoutine(this);
				deltaRoutine.Load(item);
				routines.Add(deltaRoutine);
			}
			if (!item.Name.Equals("DEVICE_CMT_START") || string.IsNullOrEmpty(item.Value))
			{
				continue;
			}
			string[] array = item.Value.Split('\n');
			foreach (string text in array)
			{
				for (int j = 0; j < text.Length - 1; j++)
				{
					if (text[j] == ':' && text[j + 1] == ':')
					{
						string vname = text.Substring(0, j);
						string comment = text.Substring(j + 2);
						DeltaValue value = GetValue(vname);
						value.Comment = comment;
					}
				}
			}
		}
		if (DT == null || (device = DeltaDevice.List.FirstOrDefault((DeltaDevice _dev) => _dev.Name.Equals(DT))) == null)
		{
			return;
		}
		foreach (DeltaSystemElement value3 in device.SystemElements.Values)
		{
			DeltaValue value2 = GetValue(value3.Source);
			value2.SysInfo = value3;
		}
	}

	public DeltaValue GetValue(string vname)
	{
		DeltaValue value = null;
		if (!values.TryGetValue(vname, out value))
		{
			value = new DeltaValue(vname);
			values.Add(vname, value);
		}
		return value;
	}
}
