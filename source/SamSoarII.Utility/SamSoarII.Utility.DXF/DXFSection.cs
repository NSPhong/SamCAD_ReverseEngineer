using System.Collections.Generic;

namespace SamSoarII.Utility.DXF;

public class DXFSection : DXFEntity
{
	public DXFSection(string name, DXFModel parent)
		: base(parent)
	{
		base.Type = EntityType.Section;
		base.Name = name;
		base.Entities = new List<DXFEntity>();
		ReadEntities();
	}

	public override void ReadEntities()
	{
		string name = base.Name;
		string text = name;
		if (!(text == "BLOCKS"))
		{
			if (!(text == "ENTITIES"))
			{
				return;
			}
			while (true)
			{
				if (base.Parent.Reader.CurrentCode != 0)
				{
					base.Parent.Reader.MoveNext();
				}
				if (base.Parent.Reader.CurrentValue == "ENDSEC")
				{
					break;
				}
				if (base.Parent.Reader.CurrentCode == 0)
				{
					switch (base.Parent.Reader.CurrentValue)
					{
					case "LINE":
						base.Entities.Add(new DXFLine(base.Parent.Reader.CurrentValue, base.Parent));
						break;
					case "ARC":
						base.Entities.Add(new DXFArc(base.Parent.Reader.CurrentValue, base.Parent));
						break;
					case "CIRCLE":
						base.Entities.Add(new DXFCircle(base.Parent.Reader.CurrentValue, base.Parent));
						break;
					case "SPLINE":
						base.Entities.Add(new DXFSpline(base.Parent.Reader.CurrentValue, base.Parent));
						break;
					default:
						base.Parent.Reader.MoveNext();
						break;
					}
				}
			}
		}
		else
		{
			ReadBlocks();
		}
	}

	public void ReadBlocks()
	{
		do
		{
			base.Parent.Reader.MoveNext();
		}
		while (!(base.Parent.Reader.CurrentValue == "ENDSEC"));
	}
}
