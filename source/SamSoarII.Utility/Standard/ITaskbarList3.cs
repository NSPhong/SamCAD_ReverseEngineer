using System;
using System.Runtime.InteropServices;

namespace Standard;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
internal interface ITaskbarList3 : Standard.ITaskbarList2, Standard.ITaskbarList
{
	new void HrInit();

	new void AddTab(IntPtr hwnd);

	new void DeleteTab(IntPtr hwnd);

	new void ActivateTab(IntPtr hwnd);

	new void SetActiveAlt(IntPtr hwnd);

	new void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

	[PreserveSig]
	Standard.HRESULT SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);

	[PreserveSig]
	Standard.HRESULT SetProgressState(IntPtr hwnd, Standard.TBPF tbpFlags);

	[PreserveSig]
	Standard.HRESULT RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);

	[PreserveSig]
	Standard.HRESULT UnregisterTab(IntPtr hwndTab);

	[PreserveSig]
	Standard.HRESULT SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);

	[PreserveSig]
	Standard.HRESULT SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, uint dwReserved);

	[PreserveSig]
	Standard.HRESULT ThumbBarAddButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] Standard.THUMBBUTTON[] pButtons);

	[PreserveSig]
	Standard.HRESULT ThumbBarUpdateButtons(IntPtr hwnd, uint cButtons, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] Standard.THUMBBUTTON[] pButtons);

	[PreserveSig]
	Standard.HRESULT ThumbBarSetImageList(IntPtr hwnd, [MarshalAs(UnmanagedType.IUnknown)] object himl);

	[PreserveSig]
	Standard.HRESULT SetOverlayIcon(IntPtr hwnd, IntPtr hIcon, [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);

	[PreserveSig]
	Standard.HRESULT SetThumbnailTooltip(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszTip);

	[PreserveSig]
	Standard.HRESULT SetThumbnailClip(IntPtr hwnd, Standard.RefRECT prcClip);
}
