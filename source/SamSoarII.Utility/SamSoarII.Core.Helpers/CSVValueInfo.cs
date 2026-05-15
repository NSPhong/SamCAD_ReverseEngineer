namespace SamSoarII.Core.Helpers;

public class CSVValueInfo : ICSVValueInfo
{
	public byte BaseAddress { get; private set; }

	public ushort Offset { get; private set; }

	public byte IntraAddress { get; private set; }

	public byte IntraOffset { get; private set; }

	public byte Flag { get; private set; }

	public byte DataType { get; private set; }

	public CSVValueInfo(byte _baseaddress, ushort _offset, byte _intraaddress, byte _intraoffset, byte _flag, byte _datatype)
	{
		BaseAddress = _baseaddress;
		Offset = _offset;
		IntraAddress = _intraaddress;
		IntraOffset = _intraoffset;
		Flag = _flag;
		DataType = _datatype;
	}
}
