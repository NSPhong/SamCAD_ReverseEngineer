using System;
using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Entity.User;

public interface IPolylineUserFormat : INotifyPropertyChanged, IDisposable
{
	int ID { get; set; }

	string Name { get; set; }

	PolylineUserDataType DataType { get; set; }

	void Save(PolylineUserFormatHeader header);

	void Load(PolylineUserFormatHeader header);

	int GetSize(int count);

	void Temporary();

	void Restore();
}
