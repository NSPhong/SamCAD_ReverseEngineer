using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7Network : S7Object
{
	private S7Program parent;

	private Enum_S7NetworkProgramming programming;

	private int id;

	private string comment;

	private S7PreMask premask;

	private S7PreList prelist;

	private List<S7UnitBase> stls;

	private List<S7UnitBase> units;

	public S7Program Parent => parent;

	public Enum_S7NetworkProgramming Programming => programming;

	public int ID => id;

	public string Comment => comment;

	public S7PreMask PreMask => premask;

	public S7PreList PreList => prelist;

	public IList<S7UnitBase> STLs => stls;

	public IList<S7UnitBase> Units => units;

	public S7Network(S7Program _parent, int _dataindex)
		: base(_parent.Data)
	{
		parent = _parent;
		dataindex = _dataindex;
		data.Start(dataindex);
		id = data.GetW(0);
		programming = (Enum_S7NetworkProgramming)data.GetB(2);
		data.Move(9);
		int _size = 0;
		comment = data.GetString(0, out _size);
		data.Move(_size + 2);
		premask = new S7PreMask(this, data.Index);
		data.Start(premask.DataIndex + premask.DataCount);
		prelist = new S7PreList(this, data.Index);
		data.Start(prelist.DataIndex + prelist.DataCount);
		data.Move(7);
		int w = data.GetW(0);
		data.Move(2);
		stls = new List<S7UnitBase>();
		while (w-- > 0)
		{
			S7STLStmt s7STLStmt = new S7STLStmt(this, data.Index);
			stls.Add(s7STLStmt);
			data.Start(s7STLStmt.DataIndex + s7STLStmt.DataCount);
		}
		int w2 = data.GetW(1);
		data.Move(3);
		units = new List<S7UnitBase>();
		while (w2-- > 0)
		{
			S7UnitBase s7UnitBase = ((programming == Enum_S7NetworkProgramming.FBD) ? new S7FBDUnit(this, data.Index) : ((programming == Enum_S7NetworkProgramming.STL) ? ((S7UnitBase)new S7STLUnit(this, data.Index)) : ((S7UnitBase)new S7Unit(this, data.Index))));
			units.Add(s7UnitBase);
			data.Start(s7UnitBase.DataIndex + s7UnitBase.DataCount);
		}
		datacount = data.Index - dataindex;
	}
}
