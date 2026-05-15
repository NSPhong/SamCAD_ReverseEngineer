using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Arguments;

public class ImageArgument : IImageArgument, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	public virtual LocatedFileHeader AllocHeader()
	{
		return null;
	}

	public virtual void Save(IFileHeader header)
	{
	}

	public virtual void Load(IFileHeader header)
	{
	}
}
