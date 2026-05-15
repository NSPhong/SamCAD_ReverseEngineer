namespace SamSoarII.Core.Models;

public interface IFBDNetworkModel
{
	int ID { get; }

	string Comment { get; }

	int Width { get; }

	int Height { get; }

	IFBDUnitModel GetChild(int x, int y);

	IFBDUnitModel GetVLine(int x, int y);
}
