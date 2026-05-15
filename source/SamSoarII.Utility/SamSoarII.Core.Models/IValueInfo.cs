using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IValueInfo : INotifyPropertyChanged, IDisposable
{
	IValueManager Parent { get; }

	IValuePrototype Prototype { get; }

	string Name { get; }

	string OriginName { get; }

	int DataAddr { get; }

	int ByteCount { get; }

	IEnumerable<IValueModel> Values { get; }

	IEnumerable<ILadderUnitModel> Units { get; }

	IEnumerable<ILadderUnitModel> UsedUnits { get; }

	bool IsEmptyInfo { get; }

	bool CanRead { get; }

	bool CanWrite { get; }

	string Comment { get; set; }

	string Alias { get; set; }

	IElementInitializeModel InitModel { get; set; }

	bool IsLocked { get; set; }

	bool IsUsed { get; }

	bool IsActuallyUsed { get; }

	void Add(IValueModel value);

	void Remove(IValueModel value);

	void Add(ILadderUnitModel unit);

	void Remove(ILadderUnitModel unit);
}
