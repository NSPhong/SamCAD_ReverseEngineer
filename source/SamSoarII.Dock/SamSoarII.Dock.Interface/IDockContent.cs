using System.ComponentModel;
using System.Windows.Media;

namespace SamSoarII.Dock.Interface;

public interface IDockContent : INotifyPropertyChanged
{
	ushort DockID { get; }

	string Header { get; }

	ImageSource Icon { get; }
}
