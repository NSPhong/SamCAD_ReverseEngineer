using System.Collections.Generic;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineActionCollection : IPolylineAction
{
	IList<IPolylineAction> Items { get; }
}
