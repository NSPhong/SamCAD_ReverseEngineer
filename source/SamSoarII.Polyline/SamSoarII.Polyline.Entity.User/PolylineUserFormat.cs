using System;
using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Entity.User;

public class PolylineUserFormat : IPolylineUserFormat, INotifyPropertyChanged, IDisposable
{
	public static string[] _DataTypes_S = new string[4] { "Integer", "Real number", "isno", "Text" };

	private int id;

	private string oldname;

	private string name;

	private PolylineUserDataType olddatatype;

	private PolylineUserDataType datatype;

	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
			InvokeProp("ID");
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			InvokeProp("Name");
		}
	}

	public PolylineUserDataType DataType
	{
		get
		{
			return datatype;
		}
		set
		{
			datatype = value;
			InvokeProp("DataType");
			InvokeProp("DataType_I");
		}
	}

	public int DataType_I
	{
		get
		{
			return (int)DataType;
		}
		set
		{
			DataType = (PolylineUserDataType)value;
		}
	}

	public string[] DataTypes_S => _DataTypes_S;

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineUserFormat(string _name, PolylineUserDataType _datatype)
	{
		name = _name;
		datatype = _datatype;
	}

	public PolylineUserFormat(PolylineUserFormatHeader _header)
	{
		Load(_header);
	}

	public void Dispose()
	{
		name = null;
	}

	public void Temporary()
	{
		oldname = name;
		olddatatype = datatype;
	}

	public void Restore()
	{
		Name = oldname;
		DataType = olddatatype;
	}

	public void Save(PolylineUserFormatHeader header)
	{
		header.bDataType = (byte)datatype;
		FileFormat.AllocHeaderString(header, 0, name);
	}

	public void Load(PolylineUserFormatHeader header)
	{
		datatype = (PolylineUserDataType)header.bDataType;
		name = FileFormat.GetString(header.spName);
	}

	public int GetSize(int count)
	{
		return datatype switch
		{
			PolylineUserDataType.Bool => (count - 1) / 8 + 1, 
			PolylineUserDataType.Int => count * 4, 
			PolylineUserDataType.Double => count * 8, 
			PolylineUserDataType.String => count * 4, 
			_ => 0, 
		};
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}
