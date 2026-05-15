using System.Windows.Input;

namespace SamCAD;

public abstract class UserCommands
{
	public static readonly RoutedCommand CreateImage;

	public static readonly RoutedCommand ImportImage;

	public static readonly RoutedCommand RemoveImage;

	public static readonly RoutedCommand ResizeImage;

	public static readonly RoutedCommand ShowEditor;

	public static readonly RoutedCommand ShowSelect;

	public static readonly RoutedCommand ShowArgument;

	public static readonly RoutedCommand ShowDataGrid;

	public static readonly RoutedCommand DrawVirt;

	public static readonly RoutedCommand DrawLine;

	public static readonly RoutedCommand DrawCircle;

	public static readonly RoutedCommand DrawArch;

	public static readonly RoutedCommand DrawRect;

	public static readonly RoutedCommand DrawFreeRect;

	public static readonly RoutedCommand DrawEllipse;

	public static readonly RoutedCommand DrawEllArch;

	public static readonly RoutedCommand DrawBSpline;

	public static readonly RoutedCommand GroupMerge;

	public static readonly RoutedCommand GroupSplit;

	public static readonly RoutedCommand GroupMove;

	public static readonly RoutedCommand GroupReverse;

	public static readonly RoutedCommand GroupMirror;

	public static readonly RoutedCommand GroupRotate;

	public static readonly RoutedCommand GroupScale;

	public static readonly RoutedCommand GroupReorder;

	public static readonly RoutedCommand GroupTailor;

	public static readonly RoutedCommand GroupMatrix;

	public static readonly RoutedCommand GroupExpand;

	public static readonly RoutedCommand GroupFill;

	public static readonly RoutedCommand EntityBreak;

	public static readonly RoutedCommand EntityTailor;

	public static readonly RoutedCommand EntityRound;

	public static readonly RoutedCommand EntityBevel;

	public static readonly RoutedCommand EntitySharp;

	public static readonly RoutedCommand UserBook;

	public static readonly RoutedCommand About;

	public static readonly RoutedCommand PageSetup;

	public static readonly RoutedCommand Export;

	public static readonly RoutedCommand Import;

	public static readonly RoutedCommand TableUser;

	public static readonly RoutedCommand TableModify;

	static UserCommands()
	{
		CreateImage = new RoutedUICommand("New graphic (_C)", "CreateImage", typeof(UserCommands));
		ImportImage = new RoutedUICommand("Import graphics (_I", "ImportImage", typeof(UserCommands));
		RemoveImage = new RoutedUICommand("Delete graphic (_D)", "RemoveImage", typeof(UserCommands));
		ResizeImage = new RoutedUICommand("Resize (_R", "ResizeImage", typeof(UserCommands));
		ShowEditor = new RoutedUICommand("Drawing board(_C)", "ShowEditor", typeof(UserCommands));
		ShowSelect = new RoutedUICommand("List of graphic elements(_L)", "ShowSelect", typeof(UserCommands));
		ShowArgument = new RoutedUICommand("Graphic element parameter (_A)", "ShowArgument", typeof(UserCommands));
		ShowDataGrid = new RoutedUICommand("Data Table (_T)", "ShowDataGrid", typeof(UserCommands));
		DrawVirt = new RoutedUICommand("Draw a dotted line (_V)", "DrawVirt", typeof(UserCommands));
		DrawLine = new RoutedUICommand("Draw a line (_L", "DrawLine", typeof(UserCommands));
		DrawCircle = new RoutedUICommand("Draw a circle (_C", "DrawCircle", typeof(UserCommands));
		DrawArch = new RoutedUICommand("Draw an arc (_A)", "DrawArch", typeof(UserCommands));
		DrawRect = new RoutedUICommand("Draw a rectangle (_R)", "DrawRect", typeof(UserCommands));
		DrawFreeRect = new RoutedUICommand("Draw a free rectangle (_F)", "DrawFreeRect", typeof(UserCommands));
		DrawEllipse = new RoutedUICommand("Draw an ellipse (_E)", "DrawEllipse", typeof(UserCommands));
		DrawEllArch = new RoutedUICommand("Draw an elliptical arc (_M)", "DrawEllArch", typeof(UserCommands));
		DrawBSpline = new RoutedUICommand("Draw B-spline (_B)", "DrawBSpline", typeof(UserCommands));
		GroupMerge = new RoutedUICommand("Combine and (_C", "GroupMerge", typeof(UserCommands));
		GroupSplit = new RoutedUICommand("组points割(_T)", "GroupSplit", typeof(UserCommands));
		GroupMove = new RoutedUICommand("Group Movement (_M)", "GroupMove", typeof(UserCommands));
		GroupReverse = new RoutedUICommand("Group reverse (_R", "GroupReverse", typeof(UserCommands));
		GroupMirror = new RoutedUICommand("Group mirror (_I", "GroupMirror", typeof(UserCommands));
		GroupRotate = new RoutedUICommand("Group Rotation (_O", "GroupRotate", typeof(UserCommands));
		GroupScale = new RoutedUICommand("Group Scaling (_C", "GroupScale", typeof(UserCommands));
		GroupReorder = new RoutedUICommand("Group Rearrangement (_S", "GroupReorder", typeof(UserCommands));
		GroupTailor = new RoutedUICommand("Group clipping (_L", "GroupTailor", typeof(UserCommands));
		GroupMatrix = new RoutedUICommand("Group array (_A)", "GroupMatrix", typeof(UserCommands));
		GroupExpand = new RoutedUICommand("Expansion/Contraction (_E)", "GroupExpand", typeof(UserCommands));
		GroupFill = new RoutedUICommand("Line filling (_F", "GroupFill", typeof(UserCommands));
		EntityBreak = new RoutedUICommand("Graphic element interruption (_B)", "EntityBreak", typeof(UserCommands));
		EntityTailor = new RoutedUICommand("Graphic element cropping (_T", "EntityTailor", typeof(UserCommands));
		EntityRound = new RoutedUICommand("Rounded corners of graphic elements (_R", "EntityRound", typeof(UserCommands));
		EntityBevel = new RoutedUICommand("Element skew (_V)", "EntityBevel", typeof(UserCommands));
		EntitySharp = new RoutedUICommand("Pixel sharpening (_S)", "EntitySharp", typeof(UserCommands));
		UserBook = new RoutedUICommand("User Manual (_U)", "UserBook", typeof(UserCommands));
		About = new RoutedUICommand("Regarding (_A)", "About", typeof(UserCommands));
		PageSetup = new RoutedUICommand("Page Settings (_U)", "PageSetup", typeof(UserCommands));
		Export = new RoutedUICommand("Export (_E", "Export", typeof(UserCommands));
		Import = new RoutedUICommand("Import (_I", "Import", typeof(UserCommands));
		TableUser = new RoutedUICommand("Table Customization (_U", "User table", typeof(UserCommands));
		TableModify = new RoutedUICommand("Batch modification(_M)", "Multiply modify", typeof(UserCommands));
	}
}
