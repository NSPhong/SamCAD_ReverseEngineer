using System;
using System.ComponentModel;
using SamSoarII.Core.Files;

namespace SamSoarII.Polyline.Entity.User;

public class PolylineUserObject : IPolylineUserObject, INotifyPropertyChanged, IDisposable
{
	private IPolylineUserFormat format;

	private object value;

	public IPolylineUserFormat Format
	{
		get
		{
			return format;
		}
		set
		{
			if (format != null)
			{
				format.PropertyChanged -= OnFormatPropertyChanged;
			}
			format = value;
			if (format != null)
			{
				format.PropertyChanged += OnFormatPropertyChanged;
			}
			InvokeProp("Format");
			InvokeProp("Name");
			InvokeProp("DataType");
			ClearValue();
		}
	}

	public int ID
	{
		get
		{
			return format?.ID ?? (-1);
		}
		set
		{
			if (format != null)
			{
				format.ID = value;
			}
		}
	}

	public string Name
	{
		get
		{
			return format?.Name ?? "<null>";
		}
		set
		{
			if (format != null)
			{
				format.Name = value;
			}
		}
	}

	public PolylineUserDataType DataType
	{
		get
		{
			return format?.DataType ?? PolylineUserDataType.Int;
		}
		set
		{
			if (format != null)
			{
				format.DataType = value;
			}
		}
	}

	public object Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
			InvokeProp("Value");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineUserObject(IPolylineUserFormat _format)
	{
		format = _format;
		value = null;
		ClearValue();
	}

	public void Dispose()
	{
		Value = null;
		Format = null;
	}

	protected void ClearValue()
	{
		if (format != null)
		{
			switch (format.DataType)
			{
			case PolylineUserDataType.Bool:
				Value = false;
				break;
			case PolylineUserDataType.Double:
				Value = 0.0;
				break;
			case PolylineUserDataType.Int:
				Value = 0;
				break;
			case PolylineUserDataType.String:
				Value = string.Empty;
				break;
			}
		}
	}

	public IPolylineUserObject Clone()
	{
		return new PolylineUserObject(format)
		{
			Value = value
		};
	}

	public unsafe int Write(PolylineUserDataHeader header, int idx, ref int pos)
	{
		byte[] dwData = header.dwData;
		switch (DataType)
		{
		case PolylineUserDataType.Bool:
			if (!(value is bool))
			{
				return idx;
			}
			if ((bool)value)
			{
				dwData[idx] |= (byte)(1 << pos);
			}
			else
			{
				dwData[idx] &= (byte)(~(1 << pos));
			}
			if (++pos >= 8)
			{
				pos = 0;
				return idx + 1;
			}
			return idx;
		case PolylineUserDataType.Double:
			if (!(value is double))
			{
				return idx;
			}
			fixed (byte* ptr2 = &dwData[idx])
			{
				*(double*)ptr2 = (double)value;
			}
			return idx + 8;
		case PolylineUserDataType.Int:
			if (!(value is int))
			{
				return idx;
			}
			fixed (byte* ptr = &dwData[idx])
			{
				*(int*)ptr = (int)value;
			}
			return idx + 4;
		case PolylineUserDataType.String:
			FileFormat.AllocHeaderString(header, idx, value.ToString());
			return idx + 4;
		default:
			return idx;
		}
	}

	public unsafe int Read(PolylineUserDataHeader header, int idx, ref int pos)
	{
		byte[] dwData = header.dwData;
		switch (DataType)
		{
		case PolylineUserDataType.Bool:
			value = ((dwData[idx] >> pos++) & 1) != 0;
			if (pos >= 8)
			{
				pos = 0;
				return idx + 1;
			}
			return idx;
		case PolylineUserDataType.Double:
			fixed (byte* ptr2 = &dwData[idx])
			{
				value = *(double*)ptr2;
			}
			return idx + 8;
		case PolylineUserDataType.Int:
			fixed (byte* ptr3 = &dwData[idx])
			{
				value = *(int*)ptr3;
			}
			return idx + 4;
		case PolylineUserDataType.String:
			fixed (byte* ptr = &dwData[idx])
			{
				value = FileFormat.GetString(*(int*)ptr);
			}
			return idx + 4;
		default:
			return idx;
		}
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	private void OnFormatPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		InvokeProp(e.PropertyName);
		string propertyName = e.PropertyName;
		if (propertyName != null && propertyName.Equals("DataType"))
		{
			ClearValue();
		}
	}
}
