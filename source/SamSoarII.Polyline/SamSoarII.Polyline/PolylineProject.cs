using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity;
using SamSoarII.Utility.DXF;

namespace SamSoarII.Polyline;

/// <summary>
/// Đại diện cho một dự án (project) trong SamCAD - đơn vị lưu trữ cấp cao nhất.
/// Một project chứa một hoặc nhiều <see cref="PolylineImage"/> (trang vẽ).
///
/// Định dạng file được hỗ trợ:
/// - .dxf  : AutoCAD Drawing Exchange Format (import/export tiêu chuẩn CAD).
/// - .sca  : SamCAD native format (nhị phân có mã hóa DES, dùng FileFormat).
/// - .ssd  : SamSoarII Data (nhị phân + ảnh thumbnail JPEG cho mỗi trang).
///
/// Chế độ IsDrill:
/// - false (mặc định): chế độ vẽ HMI/polyline thông thường.
/// - true: chế độ khoan CNC (drill) - ảnh hưởng đến cách parse DXF và xử lý entity.
///
/// Giao thức mã hóa file .sca:
/// - Lưu: FileFormat.SaveReset() → AllocHeader() → Save(header) → SaveFile().
/// - Tải: FileFormat.LoadReset() → LoadFile() → GetHeader() → Load(header).
///
/// Giao thức file .ssd:
/// - Lưu: DownloadWriter với EncryptBegin/EncryptEnd (mã hóa AES key 7).
/// - Tải: UploadReader với Skip(8) + DecryptBegin(7).
/// - Nếu có ảnh thumbnail: ghi thêm imagedata stream sau dữ liệu image.
/// </summary>
public class PolylineProject : IPolylineProject, INotifyPropertyChanged
{
	/// <summary>Tên hiển thị của project. Được cập nhật tự động từ tên file khi mở file.</summary>
	private string name;

	/// <summary>Đường dẫn đầy đủ của file project hiện tại. Null nếu chưa lưu lần nào.</summary>
	private string filename;

	/// <summary>Loại argument cho toàn bộ image trong project: HMIPLINE hoặc HMIBLOCK.</summary>
	private ImageArgumentTypes argumenttype;

	/// <summary>
	/// Danh sách có thể quan sát (ObservableCollection) các trang vẽ trong project.
	/// WPF UI binding vào collection này để tự động cập nhật khi thêm/xóa image.
	/// </summary>
	private ObservableCollection<IPolylineImage> items;

	/// <summary>True nếu project đang ở chế độ khoan CNC (drill mode).</summary>
	private bool isdrill;

	/// <summary>
	/// Tên hiển thị của project. Khi set Filename, Name được tự động cập nhật từ tên file.
	/// </summary>
	public string Name
	{
		get { return name; }
		set { name = value; InvokePropertyChanged("Name"); }
	}

	/// <summary>
	/// Đường dẫn file của project.
	/// Setter tự động cập nhật Name = Path.GetFileName(filename).
	/// </summary>
	public string Filename
	{
		get { return filename; }
		set
		{
			filename = value;
			name = Path.GetFileName(filename); // tên = tên file (có extension)
			InvokePropertyChanged("Filename");
		}
	}

	/// <summary>Loại argument áp dụng cho tất cả image trong project.</summary>
	public ImageArgumentTypes ArgumentType
	{
		get { return argumenttype; }
		set { argumenttype = value; }
	}

	/// <summary>Danh sách các trang vẽ (IPolylineImage) trong project (chỉ đọc).</summary>
	public IList<IPolylineImage> Items => items;

	/// <summary>True nếu project đang ở chế độ khoan CNC.</summary>
	public bool IsDrill
	{
		get { return isdrill; }
		set { isdrill = value; }
	}

	/// <summary>Sự kiện thông báo khi thuộc tính thay đổi (WPF data binding).</summary>
	public event PropertyChangedEventHandler PropertyChanged;

	/// <summary>
	/// Constructor mặc định: tạo project HMIPLINE mới với một trang vẽ trống.
	/// </summary>
	public PolylineProject()
		: this(ImageArgumentTypes.HMIPLINE)
	{
	}

	/// <summary>
	/// Constructor tạo project mới với loại argument xác định.
	/// Automatically create an empty PolylineImage with the default name "New project" (new project).
	/// </summary>
	/// <param name="_argtype">Loại argument: HMIPLINE hoặc HMIBLOCK.</param>
	public PolylineProject(ImageArgumentTypes _argtype)
	{
		items = new ObservableCollection<IPolylineImage>();
		name = "New project"; // "Project mới"
		filename = null;
		argumenttype = _argtype;
		items.CollectionChanged += OnItemsChanged;
		// Tạo một trang vẽ trống mặc định
		items.Add(new PolylineImage { ArgumentType = argumenttype });
	}

	/// <summary>
	/// Constructor mở file: tải project từ file theo extension.
	/// Phân nhánh xử lý theo định dạng:
	/// - .dxf : Parse DXF, tạo một PolylineImage từ dữ liệu DXF.
	/// - .sca : Tải binary qua FileFormat (giải mã DES, parse header).
	/// - .ssd : Tải binary qua UploadReader (giải mã key 7, đọc từng PolylineImage).
	/// </summary>
	/// <param name="_filename">Đường dẫn file cần mở.</param>
	/// <param name="_dxfdefaultargument">
	/// Argument mặc định dùng khi import DXF (không có thông tin argument trong file DXF).
	/// Nếu là IHMIBLOCKArgument, argumenttype sẽ được đặt thành HMIBLOCK.
	/// </param>
	public PolylineProject(string _filename, IPolylineArgument _dxfdefaultargument)
	{
		items = new ObservableCollection<IPolylineImage>();
		argumenttype = ImageArgumentTypes.HMIPLINE;
		if (_dxfdefaultargument is IHMIBLOCKArgument)
		{
			argumenttype = ImageArgumentTypes.HMIBLOCK;
		}
		Filename = _filename;
		items.CollectionChanged += OnItemsChanged;

		switch (Path.GetExtension(filename))
		{
		case ".dxf":
		{
			// Import DXF: parse geometry, load vào một PolylineImage
			DXFModel dXFModel = new DXFModel();
			PolylineImage polylineImage2 = new PolylineImage { ArgumentType = argumenttype };
			dXFModel.Convert(filename); // parse file DXF thành model trung gian
			polylineImage2.Name = Path.GetFileNameWithoutExtension(filename);
			polylineImage2.Width = 0.0;
			polylineImage2.Height = 0.0;
			polylineImage2.Load(dXFModel);
			items.Add(polylineImage2);
			if (_dxfdefaultargument == null) { break; }
			// Áp dụng argument mặc định cho tất cả entity trong image
			foreach (PolylineEntity item in polylineImage2.Items)
			{
				item.Argument.Load(_dxfdefaultargument);
			}
			break;
		}
		case ".sca":
			// SCA: tải binary qua FileFormat (có mã hóa DES)
			FileFormat.LoadReset();
			if (FileFormat.LoadFile(ref filename) && FileFormat.IsSamSoarIIFormattedFile)
			{
				FileHeader fileHeader = FileFormat.FileHeader;
				PolylineProjectHeader header = (PolylineProjectHeader)FileFormat.GetHeader(
					fileHeader.lpProject, FileHeaderTypes.PolylineProject);
				Load(header);
			}
			break;
		case ".ssd":
		{
			// SSD: đọc binary qua UploadReader, bỏ qua 8 bytes header đầu,
			// giải mã với key 7, đọc số lượng image rồi tải từng image.
			using UploadReader uploadReader = new UploadReader(filename);
			uploadReader.Skip(8);           // bỏ qua magic header 8 bytes
			uploadReader.DecryptBegin(7);   // bắt đầu giải mã với key 7
			int imageCount = uploadReader.Read32(); // số lượng image trong file
			for (int i = 0; i < imageCount; i++)
			{
				IPolylineImage polylineImage = new PolylineImage { ArgumentType = argumenttype };
				polylineImage.Load(uploadReader);
				items.Add(polylineImage);
			}
			break;
		}
		}
	}

	/// <summary>
	/// Tạo project từ file theo chế độ khoan CNC (Drill mode).
	/// Giống constructor mở file nhưng đặt IsDrill = true cho project và tất cả image.
	/// Dùng khi import dữ liệu CNC drilling từ file DXF/SCA/SSD.
	/// </summary>
	/// <param name="_filename">Đường dẫn file cần mở.</param>
	/// <param name="_dxfdefaultargument">Argument mặc định cho DXF.</param>
	/// <returns>Project mới ở chế độ drill.</returns>
	public static PolylineProject CreateDrill(string _filename, IPolylineArgument _dxfdefaultargument)
	{
		PolylineProject polylineProject = new PolylineProject();
		polylineProject.filename = _filename;
		polylineProject.IsDrill = true;

		switch (Path.GetExtension(_filename))
		{
		case ".dxf":
		{
			// DXF drill: dùng DXFModel với IsDrill = true để parse lỗ khoan
			DXFModel dXFModel = new DXFModel { IsDrill = true };
			PolylineImage polylineImage2 = new PolylineImage { ArgumentType = polylineProject.ArgumentType };
			dXFModel.Convert(_filename);
			polylineImage2.Name = Path.GetFileNameWithoutExtension(_filename);
			polylineImage2.Width = 0.0;
			polylineImage2.Height = 0.0;
			polylineImage2.IsDrill = true; // đánh dấu image ở chế độ drill
			polylineImage2.Load(dXFModel);
			polylineProject.Items.Clear();
			polylineProject.Items.Add(polylineImage2);
			if (_dxfdefaultargument == null) { break; }
			foreach (PolylineEntity item in polylineImage2.Items)
			{
				item.Argument.Load(_dxfdefaultargument);
			}
			break;
		}
		case ".sca":
			FileFormat.LoadReset();
			if (FileFormat.LoadFile(ref _filename) && FileFormat.IsSamSoarIIFormattedFile)
			{
				FileHeader fileHeader = FileFormat.FileHeader;
				PolylineProjectHeader header = (PolylineProjectHeader)FileFormat.GetHeader(
					fileHeader.lpProject, FileHeaderTypes.PolylineProject);
				polylineProject.IsDrill = true;
				polylineProject.Load(header);
			}
			break;
		case ".ssd":
		{
			using (UploadReader uploadReader = new UploadReader(_filename))
			{
				uploadReader.Skip(8);
				uploadReader.DecryptBegin(7);
				polylineProject.Items.Clear();
				int num = uploadReader.Read32();
				for (int i = 0; i < num; i++)
				{
					PolylineImage polylineImage = new PolylineImage { ArgumentType = polylineProject.ArgumentType };
					polylineImage.IsDrill = true; // đánh dấu image ở chế độ drill
					polylineImage.Load(uploadReader);
					polylineProject.Items.Add(polylineImage);
				}
			}
			break;
		}
		}
		return polylineProject;
	}

	/// <summary>
	/// Lưu metadata project vào header nhị phân (.sca format).
	/// Ghi: tên project, đường dẫn file, số lượng image, loại argument,
	/// và gọi Save() cho từng PolylineImage.
	/// </summary>
	public void Save(PolylineProjectHeader header)
	{
		FileFormat.AllocHeaderString(header, 0, name);     // string 0: tên project
		FileFormat.AllocHeaderString(header, 1, filename); // string 1: đường dẫn file
		// Cấp phát header cho từng image
		LocatedFileHeader[] array = items.Select(
			(IPolylineImage i) => FileFormat.AllocHeader(FileHeaderTypes.PolylineImage)).ToArray();
		header.dwItemsCount = items.Count();
		header.lpItems = array.FirstOrDefault().LPHeader; // con trỏ đến header image đầu tiên
		header.dwArgumentType = (int)argumenttype;
		// Lưu từng image vào header tương ứng
		for (int num = 0; num < items.Count(); num++)
		{
			PolylineImageHeader header2 = (PolylineImageHeader)array[num].Header;
			items[num].Save(header2);
		}
	}

	/// <summary>
	/// Tải metadata project từ header nhị phân (.sca format).
	/// Đọc: loại argument, số lượng image và tải từng PolylineImage.
	/// </summary>
	public void Load(PolylineProjectHeader header)
	{
		argumenttype = (ImageArgumentTypes)header.dwArgumentType;
		int lpItems = header.lpItems; // offset đến header image đầu tiên
		for (int i = 0; i < header.dwItemsCount; i++)
		{
			PolylineImageHeader header2 = (PolylineImageHeader)FileFormat.GetHeader(
				lpItems, FileHeaderTypes.PolylineImage);
			PolylineImage polylineImage = new PolylineImage
			{
				ArgumentType = argumenttype,
				Width = 0.0,
				Height = 0.0,
				IsDrill = IsDrill
			};
			polylineImage.Load(header2);
			items.Add(polylineImage);
			// lpItems tự động tăng qua việc GetHeader đọc header tiếp theo
		}
	}

	/// <summary>
	/// Lưu project ra file theo đường dẫn chỉ định.
	/// - .sca: Dùng FileFormat (binary có mã hóa DES).
	/// - .ssd: Dùng DownloadWriter (binary + mã hóa key 7, không có ảnh thumbnail).
	///         Dùng SaveSSD() nếu cần ảnh thumbnail.
	/// </summary>
	public void Save(string _filename)
	{
		Filename = _filename;
		switch (Path.GetExtension(filename))
		{
		case ".sca":
		{
			// Lưu binary: reset FileFormat, tạo header project, ghi tất cả image, flush ra file
			FileFormat.SaveReset();
			FileHeader fileHeader = FileFormat.FileHeader;
			LocatedFileHeader locatedFileHeader = FileFormat.AllocHeader(FileHeaderTypes.PolylineProject);
			PolylineProjectHeader header = (PolylineProjectHeader)locatedFileHeader.Header;
			fileHeader.lpProject = locatedFileHeader.LPHeader;
			Save(header);
			FileFormat.SaveFile(_filename);
			break;
		}
		case ".ssd":
		{
			// Lưu SSD không có ảnh thumbnail (chỉ dữ liệu)
			using DownloadWriter downloadWriter = new DownloadWriter(filename);
			downloadWriter.PushL();              // ghi marker length đầu
			downloadWriter.EncryptBegin(7);      // bắt đầu mã hóa key 7
			downloadWriter.PushL();
			downloadWriter.Write(items.Count()); // số lượng image
			foreach (IPolylineImage item in items)
			{
				item.Save(downloadWriter);
			}
			downloadWriter.PopL();
			downloadWriter.EncryptEnd();         // kết thúc vùng mã hóa
			downloadWriter.PopL();
			break;
		}
		}
	}

	/// <summary>
	/// Lưu project ra file .ssd kèm ảnh thumbnail JPEG cho từng image.
	/// Được gọi sau khi RootMain đã render xong ảnh (bất đồng bộ qua DispatcherTimer).
	/// Cấu trúc file SSD: [header mã hóa] [dữ liệu image] [dữ liệu ảnh thumbnail]
	/// </summary>
	/// <param name="_filename">Đường dẫn file .ssd đích.</param>
	/// <param name="_imagedata">Stream chứa dữ liệu ảnh thumbnail đã render sẵn.</param>
	public void SaveSSD(string _filename, Stream _imagedata)
	{
		filename = _filename;
		using DownloadWriter downloadWriter = new DownloadWriter(filename);
		downloadWriter.PushL();
		downloadWriter.EncryptBegin(7);
		downloadWriter.PushL();
		downloadWriter.Write(items.Count());
		foreach (IPolylineImage item in items)
		{
			item.Save(downloadWriter);
		}
		downloadWriter.PopL();
		downloadWriter.EncryptEnd();
		downloadWriter.PopL();
		// Ghi ảnh thumbnail vào cuối file (sau dữ liệu image)
		downloadWriter.Write(_imagedata);
	}

	/// <summary>Phát sinh sự kiện PropertyChanged cho tên thuộc tính được chỉ định.</summary>
	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	/// <summary>
	/// Handler sự kiện khi danh sách Items thay đổi (thêm/xóa/reset image).
	/// Hiện tại không xử lý gì thêm (placeholder để mở rộng sau nếu cần).
	/// </summary>
	private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e) { }
}
