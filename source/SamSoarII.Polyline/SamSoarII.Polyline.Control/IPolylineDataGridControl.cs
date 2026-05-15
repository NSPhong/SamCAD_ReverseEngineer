using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public interface IPolylineDataGridControl
{
	IPolylineUserFormat Core { get; set; }

	void Read(string v);

	void Write();

	void Select();

	void Unselect();
}
