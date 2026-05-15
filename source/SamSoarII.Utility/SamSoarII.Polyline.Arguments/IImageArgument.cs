using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public interface IImageArgument : INotifyPropertyChanged
{
	LocatedFileHeader AllocHeader();

	void Save(IFileHeader header);

	void Load(IFileHeader header);
}
