using System;
using System.Security.Principal;

namespace SamCAD;

public static class StartPoint
{
	[STAThread]
	private static void Main(string[] args)
	{
		new App().Run();
	}

	private static bool CheckPrincipal()
	{
		WindowsIdentity current = WindowsIdentity.GetCurrent();
		WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
		return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
	}
}
