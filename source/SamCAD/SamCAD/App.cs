using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace SamCAD;

/// <summary>
/// Lớp khởi động chính của ứng dụng SamCAD.
/// Kế thừa từ <see cref="Application"/> (WPF), đây là entry point của toàn bộ ứng dụng.
/// Vai trò trong kiến trúc:
/// - Khởi tạo <see cref="RootMain"/> (lõi nghiệp vụ) và <see cref="MainWindow"/> (giao diện).
/// - Cung cấp các thuộc tính tĩnh để các thành phần khác truy cập vào đối tượng gốc và cửa sổ chính.
/// - Đăng ký xử lý lỗi toàn cục cho cả UI thread (Dispatcher) lẫn AppDomain.
/// </summary>
public class App : Application
{
	/// <summary>
	/// Đối tượng RootMain - lõi nghiệp vụ của ứng dụng, quản lý project, lệnh vẽ và các thao tác.
	/// Được khởi tạo một lần duy nhất khi App khởi động.
	/// </summary>
	private static RootMain root;

	/// <summary>
	/// Truy cập tĩnh vào đối tượng RootMain (lõi nghiệp vụ).
	/// Các thành phần UI và service có thể gọi App.Root để thực thi lệnh nghiệp vụ.
	/// </summary>
	public static RootMain Root => root;

	/// <summary>
	/// Truy cập tĩnh vào cửa sổ chính (MainWindow).
	/// Trả về null nếu MainWindow hiện tại không phải kiểu <see cref="MainWindow"/>.
	/// Dùng pattern matching để đảm bảo type-safe khi cast.
	/// </summary>
	public static MainWindow Client => (Application.Current?.MainWindow is MainWindow) ? ((MainWindow)(Application.Current?.MainWindow)) : null;

	/// <summary>
	/// Khởi tạo các thành phần cốt lõi của ứng dụng.
	/// Thứ tự khởi tạo quan trọng: RootMain trước, sau đó MainWindow.
	/// RootMain cần tồn tại trước khi MainWindow truy cập App.Root.
	/// </summary>
	protected void Initialize()
	{
		// Khởi tạo lõi nghiệp vụ (quản lý project, lệnh, v.v.)
		root = new RootMain();
		// Tạo cửa sổ chính và gán vào Application.Current.MainWindow
		Application.Current.MainWindow = new MainWindow();
		// Hiển thị cửa sổ (nếu Client không null)
		Client?.Show();
	}

	/// <summary>
	/// Constructor của App.
	/// Đăng ký các handler xử lý exception toàn cục, sau đó gọi Initialize().
	/// Hai loại exception cần bắt:
	/// - DispatcherUnhandledException: lỗi xảy ra trên UI thread (WPF Dispatcher).
	/// - AppDomain.UnhandledException: lỗi xảy ra trên các thread khác (background thread).
	/// </summary>
	public App()
	{
		// Bắt lỗi không được xử lý trên UI thread (Dispatcher thread)
		base.DispatcherUnhandledException += App_DispatcherUnhandledException;
		// Bắt lỗi không được xử lý trên toàn bộ AppDomain (bao gồm background thread)
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		Initialize();
	}

	/// <summary>
	/// Xử lý exception không được bắt trên UI thread (Dispatcher).
	/// Hiển thị hộp thoại thông báo lỗi với thông tin StackTrace và Message.
	/// </summary>
	private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		// Hiển thị stack trace trong nội dung và message lỗi làm tiêu đề
		MessageBox.Show(e.Exception.StackTrace, e.Exception.Message);
	}

	/// <summary>
	/// Xử lý exception không được bắt ở cấp AppDomain (ví dụ: trên background thread).
	/// Hiển thị toàn bộ thông tin exception dưới dạng chuỗi.
	/// </summary>
	private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		MessageBox.Show(e.ExceptionObject.ToString());
	}

	/// <summary>
	/// Phương thức được sinh tự động bởi XAML build task (PresentationBuildTasks).
	/// Thiết lập URI khởi động cho ứng dụng WPF, trỏ tới MainWindow.xaml.
	/// Không nên gọi thủ công hoặc sửa đổi phương thức này.
	/// </summary>
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		// Đặt cửa sổ khởi động là MainWindow.xaml (đường dẫn tương đối)
		base.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
	}

	/// <summary>
	/// Điểm vào chính của chương trình (entry point).
	/// Được sinh tự động bởi XAML build task.
	/// [STAThread] bắt buộc cho ứng dụng WPF vì UI phải chạy trên Single-Threaded Apartment.
	/// </summary>
	[STAThread]
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public static void Main()
	{
		// Tạo instance App (constructor sẽ gọi Initialize() bên trong)
		App app = new App();
		// Thiết lập StartupUri từ XAML
		app.InitializeComponent();
		// Bắt đầu vòng lặp sự kiện WPF (blocking call, kết thúc khi cửa sổ đóng)
		app.Run();
	}
}
