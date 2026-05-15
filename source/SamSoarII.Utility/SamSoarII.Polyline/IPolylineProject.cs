using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Polyline;

public interface IPolylineProject : INotifyPropertyChanged
{
	string Name { get; set; }

	string Filename { get; set; }

	IList<IPolylineImage> Items { get; }
}
