namespace SamSoarII.Utility.Files.Step7;

public class S7Argument : S7Object
{
	private bool isenable;

	private string name;

	private string comment;

	private Enum_S7DataType s7datatype;

	private Enum_S7BaseType s7basetype;

	private int s7offset;

	private Enum_ArgumentAccessType access;

	private Enum_ArgumentDataType datatype;

	private Enum_ArgumentDataType2 datatype2;

	public bool IsEnable => isenable;

	public string Name => name;

	public string Comment => comment;

	public Enum_S7DataType S7DataType => s7datatype;

	public Enum_S7BaseType S7BaseType => s7basetype;

	public int S7Offset => s7offset;

	public Enum_ArgumentAccessType Access => access;

	public Enum_ArgumentDataType DataType => datatype;

	public Enum_ArgumentDataType2 DataType2 => datatype2;

	public S7Argument(S7DataStream _data, int _dataindex)
		: base(_data)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		int _size = 0;
		name = data.GetString(2, out _size);
		data.Move(_size + 4);
		byte b = data.GetB(1);
		s7datatype = (Enum_S7DataType)data.GetB(4);
		s7basetype = (Enum_S7BaseType)data.GetI(5);
		s7offset = data.GetI(9);
		data.Move(22);
		if (b != 3 || s7datatype != Enum_S7DataType.None)
		{
			comment = data.GetString(0, out _size);
			data.Move(_size + 2);
			access = (Enum_ArgumentAccessType)data.GetW(0);
			data.Move(2);
			datatype = (Enum_ArgumentDataType)data.GetI(0);
			datatype2 = (Enum_ArgumentDataType2)data.GetB(4);
			isenable = data.GetW(5) > 0;
			data.Move(7);
		}
		datacount = data.Index - dataindex;
	}
}
