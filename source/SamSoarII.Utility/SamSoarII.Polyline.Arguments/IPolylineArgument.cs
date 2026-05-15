using System.ComponentModel;
using System.Text;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public interface IPolylineArgument : INotifyPropertyChanged
{
	IPolylineArgument Clone();

	void Load(IPolylineArgument that);

	LocatedFileHeader AllocHeader();

	void Save(IFileHeader header);

	void Load(IFileHeader header);

	void Save(DownloadWriter dw);

	void Load(UploadReader ur);

	void Save(StringBuilder sb);

	void Load(string[] args, int start);
}
