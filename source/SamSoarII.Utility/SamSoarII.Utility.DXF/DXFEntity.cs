using System.Collections.Generic;
using System.Windows.Media;

namespace SamSoarII.Utility.DXF;

public class DXFEntity
{
	private DXFModel parent;

	public bool IsReverse { get; set; }

	public string Name { get; set; }

	public EntityType Type { get; set; }

	public List<DXFEntity> Entities { get; set; }

	public DXFModel Parent => parent;

	public DXFEntity(DXFModel parent)
	{
		this.parent = parent;
		IsReverse = false;
	}

	public virtual void ReadEntities()
	{
	}

	public virtual void ReadProperties()
	{
	}

	public virtual void Render(DrawingContext context, StreamGeometryContext ctx)
	{
	}
}
