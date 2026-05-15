using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7Program : S7Object
{
	private Enum_Program e;

	private int id;

	private string name;

	private List<S7Network> nets;

	private List<S7Argument> args;

	private List<int> endcodes;

	public Enum_Program E => e;

	public int ID => id;

	public string Name => name;

	public IList<S7Network> Nets => nets;

	public IList<S7Argument> Args => args;

	public IList<int> EndCodes => endcodes;

	public S7Program(S7DataStream _data, int _dataindex)
		: base(_data)
	{
		dataindex = _dataindex;
		data.Start(dataindex);
		e = (Enum_Program)data.GetB(1);
		id = data.GetW(5);
		data.Move(33);
		int _size = 0;
		name = data.GetString(0, out _size);
		data.Move(_size + 2);
		data.Move(128);
		nets = new List<S7Network>();
		int w = data.GetW(0);
		data.Move(2);
		while (w-- > 0)
		{
			S7Network s7Network = new S7Network(this, data.Index);
			nets.Add(s7Network);
			data.Start(s7Network.DataIndex + s7Network.DataCount);
		}
		args = new List<S7Argument>();
		int w2 = data.GetW(1);
		data.Move(3);
		while (w2-- > 0)
		{
			S7Argument s7Argument = new S7Argument(data, data.Index);
			args.Add(s7Argument);
			data.Start(s7Argument.DataIndex + s7Argument.DataCount);
		}
		endcodes = new List<int>();
		endcodes.Add(data.GetI(0));
		endcodes.Add(data.GetI(4));
		endcodes.Add(data.GetI(8));
		data.Move(12);
		datacount = data.Index - dataindex;
	}
}
