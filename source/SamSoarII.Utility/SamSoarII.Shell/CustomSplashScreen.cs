using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class CustomSplashScreen : Window
{
	private ImageSource source;

	private Grid ui_grid;

	private Image ui_image;

	public ImageSource Source => source;

	public CustomSplashScreen(ImageSource _source)
	{
		base.WindowStyle = WindowStyle.None;
		base.AllowsTransparency = true;
		base.SizeToContent = SizeToContent.WidthAndHeight;
		base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		source = _source;
		ui_grid = new Grid();
		ui_image = new Image
		{
			Source = source,
			Width = source.Width,
			Height = source.Height
		};
		base.Content = ui_grid;
		ui_grid.Children.Add(ui_image);
	}
}
