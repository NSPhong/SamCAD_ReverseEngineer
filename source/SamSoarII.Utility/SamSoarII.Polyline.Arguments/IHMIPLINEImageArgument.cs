using System.ComponentModel;

namespace SamSoarII.Polyline.Arguments;

public interface IHMIPLINEImageArgument : IImageArgument, INotifyPropertyChanged
{
	int PlaneID { get; set; }

	StartupMode StartupMode { get; set; }

	int StartupModeIndex { get; set; }
}
