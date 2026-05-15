using System;
using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Entity.User;

public interface IPolylineUserObject : INotifyPropertyChanged, IDisposable
{
	IPolylineUserFormat Format { get; set; }

	int ID { get; set; }

	string Name { get; set; }

	PolylineUserDataType DataType { get; set; }

	object Value { get; set; }

	IPolylineUserObject Clone();

	int Write(PolylineUserDataHeader header, int idx, ref int pos);

	int Read(PolylineUserDataHeader header, int idx, ref int pos);
}
