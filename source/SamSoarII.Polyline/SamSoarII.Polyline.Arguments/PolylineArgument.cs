using System.ComponentModel;
using System.Text;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public class PolylineArgument : IPolylineArgument, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	public virtual IPolylineArgument Clone()
	{
		return new PolylineArgument();
	}

	public virtual LocatedFileHeader AllocHeader()
	{
		return null;
	}

	public virtual void Load(IPolylineArgument that)
	{
	}

	public virtual void Load(IFileHeader header)
	{
	}

	public virtual void Save(IFileHeader header)
	{
	}

	public virtual void Save(DownloadWriter dw)
	{
	}

	public virtual void Load(UploadReader ur)
	{
	}

	public virtual void Save(StringBuilder sb)
	{
	}

	public virtual void Load(string[] args, int start)
	{
	}
}
