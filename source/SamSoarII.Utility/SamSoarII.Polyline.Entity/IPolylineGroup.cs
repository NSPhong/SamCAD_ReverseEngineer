using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using SamSoarII.Core.Files;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public interface IPolylineGroup : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IEnumerable<IPolylineEntity>, IEnumerable
{
	int GID { get; set; }

	int Start { get; set; }

	int Count { get; set; }

	bool IsExpand { get; set; }

	IPolylineEntity this[int id] { get; }

	void Save(PolylineGroupHeader header);

	void Load(PolylineGroupHeader header);

	bool IsLeftInside();
}
