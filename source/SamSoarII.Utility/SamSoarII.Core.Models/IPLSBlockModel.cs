using System;
using System.Collections.Generic;
using System.ComponentModel;
using SamSoarII.Utility.DXF;

namespace SamSoarII.Core.Models;

public interface IPLSBlockModel : IModel, IDisposable, INotifyPropertyChanged
{
	string FileName { get; }

	DXFModel DXF { get; }

	IList<DXFEntity> Elements { get; }

	IList<byte> Data { get; }

	int Count { get; }

	string Name { get; set; }

	int SystemID { get; set; }

	IValueModel Velocity { get; }

	IValueModel AcTime { get; }

	IValueModel DcTime { get; }
}
