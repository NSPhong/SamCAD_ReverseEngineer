using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Expands;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

/// <summary>
/// Lớp cơ sở trừu tượng cho tất cả các phần tử hình học (entity) trong SamCAD.
/// Mỗi entity đại diện cho một đoạn đường trong chuỗi polyline: đoạn thẳng (Line),
/// cung tròn (Arc), hình tròn (Circle), elip (Ellipse), B-Spline, hình chữ nhật (Rect)...
///
/// Kiến trúc chuỗi entity:
/// - Các entity được sắp xếp tuần tự trong IPolylineImage.Items[].
/// - Điểm bắt đầu (From) của entity[i] = điểm kết thúc (To) của entity[i-1].
/// - Entity đầu tiên (ID=0) lấy From từ IPolylineImage.StartPoint.
/// - Kết quả: một chuỗi đường liên tục, điểm đến điểm.
///
/// Phân loại entity:
/// - IsReal = true:  nét cắt thực sự (đầu dao hạ xuống, cắt vật liệu).
/// - IsReal = false: đường di chuyển ảo - rapid move (đầu dao nâng lên, di chuyển).
///
/// Hỗ trợ:
/// - Undo/Redo thông qua hệ thống IPolylineAction.
/// - Serialize/Deserialize ra file .sca (SamSoarII binary), .ssd (binary + ảnh), text CSV.
/// - Biến đổi hình học: Move, Mirror, Rotate, Scale, Reverse, Expand.
/// - User-defined objects (UserObjs): dữ liệu người dùng tự định nghĩa gắn kèm entity.
/// </summary>
public abstract class PolylineEntity : IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity
{
	/// <summary>Cờ đánh dấu entity đã bị dispose (giải phóng tài nguyên). Tránh dispose hai lần.</summary>
	private bool isdisposed = false;

	/// <summary>
	/// Tên tùy chỉnh của entity do người dùng đặt.
	/// Khi null, getter của Name sẽ tự gọi _GetName() để lấy tên mặc định theo loại.
	/// </summary>
	private string name;

	/// <summary>Image (trang vẽ) chứa entity này. Null nếu entity chưa thuộc image nào.</summary>
	private IPolylineImage parent;

	/// <summary>
	/// Nhóm (group) mà entity này thuộc về.
	/// Các entity cùng group được xử lý cùng nhau khi thực hiện lệnh nhóm.
	/// </summary>
	private IPolylineGroup group;

	/// <summary>
	/// Đối số (argument) chứa tham số PLC/HMI của entity này.
	/// Loại argument phụ thuộc vào ArgumentType: HMIPLINEArgument hoặc HMIBLOCKArgument.
	/// </summary>
	private IPolylineArgument argument;

	/// <summary>Loại argument đang dùng: HMIPLINE (màn hình polyline) hoặc HMIBLOCK (màn hình block).</summary>
	private ImageArgumentTypes argumenttype;

	/// <summary>
	/// Chỉ số (index) của entity trong danh sách Items của image cha.
	/// -1 nếu entity chưa được gắn vào image nào.
	/// </summary>
	private int id;

	/// <summary>Tọa độ điểm kết thúc (endpoint) của entity này.</summary>
	protected Point to;

	/// <summary>
	/// Phân loại entity: true = nét cắt thực (dao hạ xuống), false = đường di chuyển ảo (dao nâng lên).
	/// </summary>
	private bool isreal;

	/// <summary>Cờ UI: true khi con trỏ chuột đang hover trên entity này.</summary>
	private bool ismouseover;

	/// <summary>Cờ UI: true khi entity này đang được chọn trong vùng chọn.</summary>
	private bool isselected;

	/// <summary>
	/// Cờ "slot" dùng nội bộ để đánh dấu entity là vị trí giữ chỗ (placeholder).
	/// Dùng trong các thuật toán reorder và tối ưu đường đi.
	/// </summary>
	private bool isslot;

	/// <summary>
	/// Danh sách các user-defined objects gắn kèm entity này.
	/// UserObjs cho phép người dùng gắn dữ liệu tùy chỉnh (tên, mã, ghi chú) vào từng entity.
	/// </summary>
	private List<IPolylineUserObject> userobjs;

	/// <summary>True nếu entity đã bị dispose.</summary>
	public bool IsDisposed => isdisposed;

	/// <summary>ColorID cho hệ thống vẽ lưới (GridPenning). Mặc định 0 = màu tiêu chuẩn.</summary>
	int IGridPenningEntity.ColorID => 0;

	/// <summary>Loại hình học của entity này (Line, Arch, Circle, Ellipse, Rect...). Phải override trong subclass.</summary>
	public abstract PolylineType Type { get; }

	/// <summary>
	/// Tên hiển thị của entity.
	/// - Getter: Trả về tên tùy chỉnh nếu đã đặt, ngược lại gọi _GetName() để lấy tên mặc định theo loại.
	/// - Setter: Lưu tên tùy chỉnh; chuỗi rỗng được coi là null (dùng tên mặc định).
	/// </summary>
	public string Name
	{
		get { return name ?? _GetName(); }
		set
		{
			name = ((value != null && value.Length > 0) ? value : null);
			InvokePropertyChanged("Name");
		}
	}

	/// <summary>
	/// Image cha chứa entity này.
	/// Khi thay đổi, RefreshUserObjs() được gọi qua InvokePropertyChanged để đồng bộ UserObjs.
	/// </summary>
	public IPolylineImage Parent
	{
		get { return parent; }
		set { parent = value; InvokePropertyChanged("Parent"); }
	}

	/// <summary>Nhóm mà entity này thuộc về. Thay đổi sẽ trigger PropertyChanged.</summary>
	public IPolylineGroup Group
	{
		get { return group; }
		set { group = value; InvokePropertyChanged("Group"); }
	}

	/// <summary>Đối số PLC/HMI của entity. Thay đổi sẽ trigger PropertyChanged.</summary>
	public IPolylineArgument Argument
	{
		get { return argument; }
		set { argument = value; InvokePropertyChanged("Argument"); }
	}

	/// <summary>
	/// Loại argument đang dùng.
	/// Setter gọi _setArgumentType() để khởi tạo đúng loại argument object.
	/// </summary>
	public ImageArgumentTypes ArgumentType
	{
		get { return argumenttype; }
		set { _setArgumentType(value); InvokePropertyChanged("ArgumentType"); }
	}

	/// <summary>
	/// Chỉ số của entity trong Items[] của image cha.
	/// -1 nếu chưa gắn vào image.
	/// </summary>
	public int ID
	{
		get { return id; }
		set { id = value; InvokePropertyChanged("ID"); }
	}

	/// <summary>
	/// Entity liền trước trong chuỗi (ID - 1). Null nếu đây là entity đầu tiên hoặc chưa có parent.
	/// </summary>
	public IPolylineEntity Prev => (parent == null || id == 0) ? null : parent.Items[id - 1];

	/// <summary>
	/// Entity liền sau trong chuỗi (ID + 1). Null nếu đây là entity cuối cùng hoặc chưa có parent.
	/// </summary>
	public IPolylineEntity Next => (parent == null || id + 1 >= parent.Items.Count) ? null : parent.Items[id + 1];

	/// <summary>
	/// Điểm bắt đầu của entity này.
	/// - Getter: Lấy từ entity trước (Prev.To) hoặc StartPoint của image nếu là entity đầu tiên.
	/// - Setter: Thay đổi điểm kết thúc của entity trước (duy trì tính liên tục chuỗi).
	/// </summary>
	public virtual Point From
	{
		get
		{
			return (parent == null) ? new Point(0.0, 0.0)
				: ((id == 0) ? parent.StartPoint : parent.Items[id - 1].To);
		}
		set
		{
			// Chỉnh điểm kết thúc của entity trước để duy trì chuỗi liên tục
			if (parent != null && id > 0)
			{
				parent.Items[id - 1].To = value;
			}
		}
	}

	/// <summary>Điểm kết thúc của entity. Thay đổi trigger PropertyChanged.</summary>
	public virtual Point To
	{
		get { return to; }
		set { to = value; InvokePropertyChanged("To"); }
	}

	/// <summary>Vector tiếp tuyến tại điểm cuối (hướng từ From đến To).</summary>
	public virtual Vector Tangent => To - From;

	/// <summary>Vector tiếp tuyến ngược (hướng từ To về From), dùng khi cần nối ngược chiều.</summary>
	public virtual Vector TangentBack => From - To;

	/// <summary>
	/// True = nét cắt thực (đầu dao hạ, cắt vật liệu). False = đường di chuyển nhanh (rapid move).
	/// Thay đổi sẽ trigger GroupUpdate() trên image cha để cập nhật phân nhóm.
	/// </summary>
	public bool IsReal
	{
		get { return isreal; }
		set
		{
			if (isreal != value) { isreal = value; InvokePropertyChanged("IsReal"); }
		}
	}

	/// <summary>True khi con trỏ chuột đang hover. Dùng để highlight entity trong UI.</summary>
	public bool IsMouseOver
	{
		get { return ismouseover; }
		set
		{
			if (ismouseover != value) { ismouseover = value; InvokePropertyChanged("IsMouseOver"); }
		}
	}

	/// <summary>
	/// True khi entity đang được chọn.
	/// Thay đổi trigger SelectUpdate() trên image cha để đồng bộ vùng chọn.
	/// </summary>
	public bool IsSelected
	{
		get { return isselected; }
		set
		{
			if (isselected != value) { isselected = value; InvokePropertyChanged("IsSelected"); }
		}
	}

	/// <summary>True khi entity là slot giữ chỗ nội bộ (placeholder trong reorder/fill algorithms).</summary>
	public bool IsSlot
	{
		get { return isslot; }
		set
		{
			if (isslot != value) { isslot = value; InvokePropertyChanged("IsSlot"); }
		}
	}

	/// <summary>True nếu entity có tính chất đặc biệt (override trong subclass nếu cần).</summary>
	public virtual bool IsSpecial => false;

	/// <summary>
	/// Hình chữ nhật bao quanh (bounding box) của entity.
	/// Mặc định là Rect(From, To). Subclass nên override để tính chính xác hơn (vd: Arc, Circle).
	/// </summary>
	public virtual Rect Bounding => new Rect(From, To);

	/// <summary>
	/// Danh sách các điểm điều khiển (control points) của entity.
	/// Mặc định chỉ có một control point tại điểm To (index = 1).
	/// Subclass override để thêm các điểm điều khiển riêng (vd: tâm cung, bán kính...).
	/// </summary>
	public virtual IEnumerable<IPolylineControlPoint> ControlPoints
	{
		get { yield return new PolylineControlPoint(this, 1); }
	}

	/// <summary>Danh sách các đối tượng vẽ lưới (GridPenning) của entity. Mặc định chỉ có bản thân.</summary>
	public virtual IEnumerable<IGridPenningEntity> Pennings
	{
		get { yield return this; }
	}

	/// <summary>Danh sách user-defined objects gắn kèm entity này (chỉ đọc).</summary>
	public IList<IPolylineUserObject> UserObjs => userobjs;

	/// <summary>Sự kiện thông báo khi thuộc tính thay đổi (WPF data binding).</summary>
	public event PropertyChangedEventHandler PropertyChanged;

	/// <summary>
	/// Constructor mặc định: tạo entity chưa gắn vào image, dùng argument mặc định HMIPLINE.
	/// </summary>
	public PolylineEntity()
	{
		parent = null;
		id = -1;
		argument = new HMIPLINEArgument();
		userobjs = new List<IPolylineUserObject>();
		RefreshUserObjs();
	}

	/// <summary>
	/// Constructor đầy đủ: tạo entity gắn vào image cụ thể với tọa độ và loại xác định.
	/// </summary>
	/// <param name="_parent">Image cha chứa entity.</param>
	/// <param name="_id">Chỉ số trong Items[] của image.</param>
	/// <param name="_to">Tọa độ điểm kết thúc.</param>
	/// <param name="_isreal">True = nét cắt thực, False = đường rapid move.</param>
	public PolylineEntity(IPolylineImage _parent, int _id, Point _to, bool _isreal = true)
	{
		parent = _parent;
		id = _id;
		to = _to;
		isreal = _isreal;
		isslot = false;
		userobjs = new List<IPolylineUserObject>();
		RefreshUserObjs();
		if (parent != null)
		{
			ArgumentType = parent.ArgumentType;
		}
	}

	/// <summary>
	/// Giải phóng tài nguyên của entity.
	/// Xóa tham chiếu đến parent, argument, userobjs để tránh memory leak.
	/// </summary>
	public virtual void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			userobjs.Clear();
			parent = null;
			id = -1;
			argument = null;
			userobjs = null;
		}
	}

	/// <summary>
	/// Trả về tên mặc định của entity theo loại hình học.
	/// Phải được implement trong mỗi subclass (vd: "Line", "Arc", "Circle"...).
	/// </summary>
	protected abstract string _GetName();

	/// <summary>True nếu người dùng đã đặt tên tùy chỉnh cho entity này.</summary>
	public bool HasRealName() { return name != null; }

	/// <summary>
	/// Thiết lập loại argument và tạo instance argument tương ứng.
	/// HMIPLINE → HMIPLINEArgument (tham số cho màn hình HMI dạng polyline đơn giản).
	/// HMIBLOCK → HMIBLOCKArgument (tham số cho màn hình HMI dạng block).
	/// </summary>
	protected void _setArgumentType(ImageArgumentTypes value)
	{
		argumenttype = value;
		switch (argumenttype)
		{
		case ImageArgumentTypes.HMIPLINE:
			Argument = new HMIPLINEArgument();
			break;
		case ImageArgumentTypes.HMIBLOCK:
			Argument = new HMIBLOCKArgument();
			break;
		}
	}

	/// <summary>Cấp phát header trong file binary để lưu entity này. Override trong subclass nếu dùng loại header riêng.</summary>
	public virtual LocatedFileHeader AllocHeader()
	{
		return FileFormat.AllocHeader(FileHeaderTypes.PolylineEntity);
	}

	/// <summary>
	/// Lưu entity vào header binary (định dạng .sca).
	/// Ghi: tên tùy chỉnh, cờ IsReal, loại entity, loại argument và pointer tới argument header.
	/// </summary>
	public virtual void Save(PolylineEntityHeader header)
	{
		FileFormat.AllocHeaderString(header, 0, name);
		header.bIsReal = (byte)(isreal ? 1u : 0u);
		header.bEntityType = 0;
		header.bArgumentType = 0;
		header.lpArgument = -1;
		if (argument is IHMIPLINEArgument)
		{
			LocatedFileHeader locatedFileHeader = argument.AllocHeader();
			header.bArgumentType = 1;
			header.lpArgument = locatedFileHeader.LPHeader;
			argument.Save(locatedFileHeader.Header);
		}
	}

	/// <summary>
	/// Tải entity từ header binary (định dạng .sca).
	/// Đọc: tên, cờ IsReal, loại argument và tải argument tương ứng.
	/// </summary>
	public virtual void Load(PolylineEntityHeader header)
	{
		name = FileFormat.GetString(header.spName);
		if (name != null && name.Length == 0) { name = null; }
		isreal = header.bIsReal > 0;
		byte bArgumentType = header.bArgumentType;
		if (bArgumentType == 1)
		{
			// ArgumentType = 1: HMIPLINE argument
			HMIPLINEArgumentHeader header2 = (HMIPLINEArgumentHeader)FileFormat.GetHeader(header.lpArgument, FileHeaderTypes.HMIPLINEArgument);
			HMIPLINEArgument hMIPLINEArgument = new HMIPLINEArgument();
			hMIPLINEArgument.Load(header2);
			argument = hMIPLINEArgument;
		}
	}

	/// <summary>Lưu entity vào DownloadWriter (định dạng .ssd binary cho PLC). Override trong subclass.</summary>
	public virtual void Save(DownloadWriter dw) { }

	/// <summary>Tải entity từ UploadReader (định dạng nhận từ PLC). Override trong subclass.</summary>
	public virtual void Load(UploadReader ur) { }

	/// <summary>Lưu entity dưới dạng chuỗi text CSV (StringBuilder). Override trong subclass.</summary>
	public virtual void Save(StringBuilder sb) { }

	/// <summary>Tải entity từ chuỗi text CSV. Override trong subclass.</summary>
	public virtual void Load(string text) { }

	/// <summary>Lưu argument vào DownloadWriter. Gọi argument.Save(dw) nếu argument không null.</summary>
	protected void SaveArgument(DownloadWriter dw) { argument?.Save(dw); }

	/// <summary>
	/// Tải argument từ UploadReader.
	/// Khởi tạo HMIPLINEArgument mặc định nếu argument chưa có.
	/// </summary>
	protected void LoadArgument(UploadReader ur)
	{
		if (argument == null) { argument = new HMIPLINEArgument(); }
		argument.Load(ur);
	}

	/// <summary>Lưu argument dưới dạng text CSV.</summary>
	protected void SaveArgument(StringBuilder sb) { argument?.Save(sb); }

	/// <summary>Tải argument từ mảng chuỗi CSV bắt đầu từ chỉ số start.</summary>
	protected void LoadArgument(string[] args, int start)
	{
		if (argument == null) { argument = new HMIPLINEArgument(); }
		argument.Load(args, start);
	}

	// =========================================================================
	// Các phép tính hình học nội bộ (dùng chung cho subclass)
	// =========================================================================

	/// <summary>
	/// Tính điểm đối xứng của điểm p qua trục gương đi qua s với phương v.
	/// Thuật toán: dùng cross product để tính khoảng cách có dấu từ p đến trục,
	/// sau đó dịch chuyển p theo vector vuông góc 2 lần khoảng cách đó.
	/// </summary>
	/// <param name="p">Điểm cần lật gương.</param>
	/// <param name="s">Điểm nằm trên trục gương.</param>
	/// <param name="v">Vector phương của trục gương.</param>
	/// <returns>Điểm đối xứng của p qua trục.</returns>
	protected Point GetMirrorPoint(Point p, Point s, Vector v)
	{
		Vector vector = p - s;
		double num = Vector.CrossProduct(v, vector); // khoảng cách có dấu * |v|
		if (num == 0.0) { return p; } // p nằm trên trục -> không đổi
		double length = v.Length;
		double num2 = num / length; // khoảng cách thực từ p đến trục
		// Vector vuông góc với v (xoay 90 độ)
		Vector vector2 = new Vector(v.Y, 0.0 - v.X) * ((length > 0.0) ? 1 : (-1));
		vector2 *= num2 * 2.0 / vector2.Length; // dịch chuyển 2 lần khoảng cách
		return p + vector2;
	}

	/// <summary>
	/// Xoay điểm p quanh tâm s một góc a (đơn vị độ, chiều dương ngược chiều kim đồng hồ).
	/// Sử dụng ma trận xoay 2D chuẩn.
	/// </summary>
	/// <param name="p">Điểm cần xoay.</param>
	/// <param name="s">Tâm xoay.</param>
	/// <param name="a">Góc xoay tính bằng độ.</param>
	/// <returns>Điểm sau khi xoay.</returns>
	protected Point GetRotatePoint(Point p, Point s, double a)
	{
		Vector vector = p - s;
		double sinA = Math.Sin(a * Math.PI / 180.0);
		double cosA = Math.Cos(a * Math.PI / 180.0);
		// Ma trận xoay: [cosA -sinA; sinA cosA] * vector
		vector = new Vector(vector.X * cosA - vector.Y * sinA, vector.X * sinA + vector.Y * cosA);
		return s + vector;
	}

	/// <summary>Lật gương vector v qua trục phương vm.</summary>
	protected Vector GetMirrorVector(Vector v, Vector vm)
	{
		return GetMirrorPoint(new Point(v.X, v.Y), new Point(0.0, 0.0), vm) - new Point(0.0, 0.0);
	}

	/// <summary>Xoay vector v một góc a độ.</summary>
	protected Vector GetRotateVector(Vector v, double a)
	{
		return GetRotatePoint(new Point(v.X, v.Y), new Point(0.0, 0.0), a) - new Point(0.0, 0.0);
	}

	/// <summary>
	/// Scale (phóng to/thu nhỏ) điểm p quanh tâm s theo tỉ lệ xs (trục X) và ys (trục Y).
	/// </summary>
	protected Point GetScalePoint(Point p, Point s, double xs, double ys)
	{
		Vector vector = p - s;
		vector.X *= xs;
		vector.Y *= ys;
		return s + vector;
	}

	// =========================================================================
	// Các phép biến đổi hình học (trả về entity mới, không mutate entity gốc)
	// =========================================================================

	/// <summary>Tạo bản sao của entity. Phải override trong mỗi subclass.</summary>
	public virtual IPolylineEntity Clone() { return null; }

	/// <summary>Dịch chuyển entity theo vector v. Trả về entity mới (immutable).</summary>
	public virtual IPolylineEntity Move(Vector v)
	{
		IPolylineEntity polylineEntity = Clone();
		polylineEntity.To += v;
		return polylineEntity;
	}

	/// <summary>
	/// Đảo ngược chiều entity: điểm To mới = điểm From cũ.
	/// Dùng khi cần đổi chiều đường CNC.
	/// </summary>
	public virtual IPolylineEntity Reverse()
	{
		IPolylineEntity polylineEntity = Clone();
		polylineEntity.To = From;
		return polylineEntity;
	}

	/// <summary>Lật gương entity qua trục đi qua p với phương v.</summary>
	public virtual IPolylineEntity Mirror(Point p, Vector v)
	{
		IPolylineEntity polylineEntity = Clone();
		polylineEntity.To = GetMirrorPoint(To, p, v);
		return polylineEntity;
	}

	/// <summary>Xoay entity quanh tâm s một góc a độ.</summary>
	public virtual IPolylineEntity Rotate(Point s, double a)
	{
		IPolylineEntity polylineEntity = Clone();
		polylineEntity.To = GetRotatePoint(To, s, a);
		return polylineEntity;
	}

	/// <summary>Scale entity quanh tâm s theo tỉ lệ xs, ys.</summary>
	public virtual IPolylineEntity Scale(Point s, double xs, double ys)
	{
		IPolylineEntity polylineEntity = Clone();
		polylineEntity.To = GetScalePoint(To, s, xs, ys);
		return polylineEntity;
	}

	/// <summary>
	/// Tạo đối tượng IPolylineExpand (mở rộng offset) từ entity này.
	/// Mặc định trả về null. Subclass như Line, Arc cần override.
	/// </summary>
	public virtual IPolylineExpand Expand(double r) { return null; }

	/// <summary>Lấy vector tiếp tuyến tại đầu vào cho thuật toán expand offset. Override trong subclass.</summary>
	public virtual Vector? LawerFrom(double r) { return null; }

	/// <summary>Lấy vector tiếp tuyến tại đầu ra cho thuật toán expand offset. Override trong subclass.</summary>
	public virtual Vector? LawerTo(double r) { return null; }

	/// <summary>Lấy vector tiếp tuyến ngang tại điểm bắt đầu. Mặc định = To - From.</summary>
	public virtual Vector? HorizonFrom() { return To - From; }

	/// <summary>Lấy vector tiếp tuyến ngang tại điểm kết thúc. Mặc định = To - From.</summary>
	public virtual Vector? HorizonTo() { return To - From; }

	/// <summary>
	/// Tạo điểm tròn (ExpandPoint) tại đầu cuối của entity khi thực hiện expand.
	/// Mặc định tạo một vòng tròn bán kính |r| tại điểm To.
	/// IsClockwise: r > 0 = ngược chiều kim đồng hồ, r < 0 = thuận chiều.
	/// </summary>
	public virtual IPolylineExpand ExpandPoint(double r)
	{
		return new PolylineExpandCircle((PolylineImage)parent, ID)
		{
			C = To,
			R = Math.Abs(r),
			IsClockwise = (r > 0.0)
		};
	}

	/// <summary>Sửa chữa entity nếu có lỗi tọa độ hoặc dữ liệu. Override trong subclass khi cần.</summary>
	public virtual void Repair() { }

	/// <summary>
	/// Tải dữ liệu từ entity khác (clone properties, không phải tham chiếu).
	/// Sao chép: tên, argument, group, và danh sách user objects.
	/// </summary>
	public virtual void Load(IPolylineEntity that)
	{
		if (that == null) { return; }
		if (that.HasRealName()) { name = that.Name; }
		if (that.Argument != null) { argument = that.Argument.Clone(); }
		Group = that.Group;
		userobjs.Clear();
		foreach (IPolylineUserObject userObj in that.UserObjs)
		{
			userobjs.Add(userObj.Clone());
		}
	}

	/// <summary>
	/// Tính khoảng cách từ điểm p đến entity.
	/// Mặc định trả về double.MaxValue (entity không xác định khoảng cách).
	/// Override trong subclass để tính khoảng cách thực (vd: khoảng cách từ p đến đoạn thẳng).
	/// </summary>
	public virtual double GetDist(Point p) { return double.MaxValue; }

	/// <summary>Tính chiều dài của entity tính từ điểm From.</summary>
	public virtual double GetLength() { return GetLength(From); }

	/// <summary>
	/// Tính chiều dài entity từ điểm p đến To.
	/// Mặc định là khoảng cách Euclidean. Override trong subclass cho Arc/Circle.
	/// </summary>
	public virtual double GetLength(Point p) { return (To - p).Length; }

	/// <summary>
	/// Gợi ý điểm snap (hút chuột) khi xlock/ylock được bật trong editor.
	/// Mặc định trả về p không thay đổi. Override trong subclass cho snap nâng cao.
	/// </summary>
	public virtual Point ReflectInline(Point p, bool xlock, bool ylock) { return p; }

	/// <summary>
	/// Đồng bộ danh sách UserObjs với danh sách format (UserFmts) của image cha.
	/// - Nếu format của UserObj[i] khác UserFmts[i]: dispose cái cũ, tạo cái mới.
	/// - Nếu UserObjs có ít phần tử hơn UserFmts: thêm UserObj mới cho các format còn thiếu.
	/// Phương thức này được gọi khi Parent thay đổi hoặc khi UserFmts của image được cập nhật.
	/// </summary>
	public void RefreshUserObjs()
	{
		if (parent == null) { return; }
		for (int i = 0; i < userobjs.Count(); i++)
		{
			if (userobjs[i]?.Format != parent.UserFmts[i])
			{
				userobjs[i]?.Dispose();
				userobjs[i] = new PolylineUserObject(parent.UserFmts[i]);
			}
		}
		while (userobjs.Count() < parent.UserFmts.Count())
		{
			PolylineUserObject item = new PolylineUserObject(parent.UserFmts[userobjs.Count()]);
			userobjs.Add(item);
		}
	}

	/// <summary>
	/// Phát sinh sự kiện PropertyChanged và kích hoạt các cập nhật liên quan trên image cha.
	/// Logic cập nhật bổ sung:
	/// - "Parent" thay đổi → RefreshUserObjs().
	/// - "IsReal" thay đổi → parent.GroupUpdate() (phân lại nhóm real/virtual).
	/// - "IsSelected" thay đổi → parent.SelectUpdate() (cập nhật vùng chọn).
	/// Kiểm tra entity còn thuộc image cha trước khi cập nhật (tránh stale reference).
	/// </summary>
	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
		if (parent == null) { return; }

		if (propname.Equals("Parent")) { RefreshUserObjs(); }

		// Kiểm tra entity còn hợp lệ trong image cha trước khi xử lý
		if (this is IPolylineGroup)
		{
			IPolylineGroup polylineGroup = (IPolylineGroup)this;
			if (polylineGroup.GID < 0 || polylineGroup.GID >= parent.Groups.Count || parent.Groups[polylineGroup.GID] != polylineGroup)
				return;
		}
		else if (id < 0 || id >= parent.Items.Count || parent.Items[id] != this)
		{
			return;
		}

		// IsReal thay đổi: cần phân lại nhóm (group = chuỗi các entity real liên tiếp)
		if (propname.Equals("IsReal")) { parent?.GroupUpdate(); }
		// IsSelected thay đổi: cập nhật thống kê vùng chọn (SelectedStart, SelectedCount)
		if (propname.Equals("IsSelected")) { parent?.SelectUpdate(this); }
	}
}
