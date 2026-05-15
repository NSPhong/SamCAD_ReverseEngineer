namespace SamCAD.Control;

public struct LoadMode(Enum_LoadMode _e)
{
	private Enum_LoadMode e = _e;

	public Enum_LoadMode E => e;

	public override string ToString()
	{
		return e switch
		{
			Enum_LoadMode.None => "no", 
			Enum_LoadMode.Drill => "Drilling (circular change point", 
			_ => base.ToString(), 
		};
	}
}
