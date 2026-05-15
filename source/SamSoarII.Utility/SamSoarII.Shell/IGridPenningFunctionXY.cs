namespace SamSoarII.Shell;

public interface IGridPenningFunctionXY : IGridPenningEntity
{
	string Name { get; }

	double XMin { get; }

	double XMax { get; }

	bool GetY(double x, ref double y);

	bool GetY(double x, ref double y, int channel);

	bool GetYString(double x, ref string y);

	bool GetYString(double x, ref string y, int channel);
}
