namespace SamSoarII.Utility.Files.Step7;

public class S7Object
{
	protected S7DataStream data;

	protected int dataindex;

	protected int datacount;

	public S7DataStream Data => data;

	public int DataIndex => dataindex;

	public int DataCount => datacount;

	public S7Object(S7DataStream _data)
	{
		data = _data;
		dataindex = 0;
		datacount = 0;
	}
}
