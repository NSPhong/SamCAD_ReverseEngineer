namespace SamSoarII.Shell;

public interface IGridPenningBoolRuler : IGridPenningEntity
{
	string Name { get; }

	int RulerID { get; }

	double XMin { get; }

	double XMax { get; }

	bool GetY(double x, ref bool y);

	bool GetY(double x, ref bool y, int channel);

	bool GetMsg(double x1, double x2, ref GridPenningBoolRangeResults result, ref double sp);
}
