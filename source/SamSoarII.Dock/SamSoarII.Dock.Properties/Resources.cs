using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SamSoarII.Dock.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("SamSoarII.Dock.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static string Anchorable_AutoHide => ResourceManager.GetString("Anchorable_AutoHide", resourceCulture);

	internal static string Anchorable_BtnAutoHide_Hint => ResourceManager.GetString("Anchorable_BtnAutoHide_Hint", resourceCulture);

	internal static string Anchorable_BtnClose_Hint => ResourceManager.GetString("Anchorable_BtnClose_Hint", resourceCulture);

	internal static string Anchorable_CxMenu_Hint => ResourceManager.GetString("Anchorable_CxMenu_Hint", resourceCulture);

	internal static string Anchorable_Dock => ResourceManager.GetString("Anchorable_Dock", resourceCulture);

	internal static string Anchorable_DockAsDocument => ResourceManager.GetString("Anchorable_DockAsDocument", resourceCulture);

	internal static string Anchorable_Float => ResourceManager.GetString("Anchorable_Float", resourceCulture);

	internal static string Anchorable_Hide => ResourceManager.GetString("Anchorable_Hide", resourceCulture);

	internal static string Document_Close => ResourceManager.GetString("Document_Close", resourceCulture);

	internal static string Document_CloseAllButThis => ResourceManager.GetString("Document_CloseAllButThis", resourceCulture);

	internal static string Document_CxMenu_Hint => ResourceManager.GetString("Document_CxMenu_Hint", resourceCulture);

	internal static string Document_DockAsDocument => ResourceManager.GetString("Document_DockAsDocument", resourceCulture);

	internal static string Document_Float => ResourceManager.GetString("Document_Float", resourceCulture);

	internal static string Document_FloatAll => ResourceManager.GetString("Document_FloatAll", resourceCulture);

	internal static string Document_Merge => ResourceManager.GetString("Document_Merge", resourceCulture);

	internal static string Document_MoveToNextTabGroup => ResourceManager.GetString("Document_MoveToNextTabGroup", resourceCulture);

	internal static string Document_MoveToPreviousTabGroup => ResourceManager.GetString("Document_MoveToPreviousTabGroup", resourceCulture);

	internal static string Document_NewHorizontalTabGroup => ResourceManager.GetString("Document_NewHorizontalTabGroup", resourceCulture);

	internal static string Document_NewVerticalTabGroup => ResourceManager.GetString("Document_NewVerticalTabGroup", resourceCulture);

	internal static string Document_SplitHorizontalTabGroup => ResourceManager.GetString("Document_SplitHorizontalTabGroup", resourceCulture);

	internal static string Document_SplitVerticalTabGroup => ResourceManager.GetString("Document_SplitVerticalTabGroup", resourceCulture);

	internal static string Window_Maximize => ResourceManager.GetString("Window_Maximize", resourceCulture);

	internal static string Window_Restore => ResourceManager.GetString("Window_Restore", resourceCulture);

	internal Resources()
	{
	}
}
