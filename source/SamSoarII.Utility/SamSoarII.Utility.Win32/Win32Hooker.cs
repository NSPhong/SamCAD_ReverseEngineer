using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Win32;

public class Win32Hooker : IDisposable
{
	public const int PAGE_NOACCESS = 1;

	public const int PAGE_READONLY = 2;

	public const int PAGE_READWRITE = 4;

	public const int PAGE_WRITECOPY = 8;

	public const int PAGE_EXECUTE = 16;

	public const int PAGE_EXECUTE_READ = 32;

	public const int PAGE_EXECUTE_READWRITE = 64;

	public const int PAGE_EXECUTE_WRITECOPY = 128;

	public const int MEM_COMMIT = 4096;

	public const int MEM_RESERVE = 8192;

	public const int MEM_DECOMMIT = 16384;

	public const int MEM_RELEASE = 32768;

	public const int MEM_FREE = 65536;

	public const int MEM_PRIVATE = 131072;

	public const int MEM_MAPPED = 262144;

	public const int MEM_RESET = 524288;

	public const int MEM_TOP_DOWN = 1048576;

	public const int MEM_WRITE_WATCH = 2097152;

	public const int MEM_PHYSICAL = 4194304;

	public const int MEM_LARGE_PAGES = 536870912;

	public const int MEM_4MB_PAGES = int.MinValue;

	protected IntPtr entry;

	protected IntPtr hookcodeptr;

	protected int hookcodesize;

	public IntPtr Entry => entry;

	public IntPtr HookCodePtr => hookcodeptr;

	public int HookCodeSize => hookcodesize;

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryW")]
	public static extern IntPtr LoadLibrary(string modulename);

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
	public static extern IntPtr GetProcAddress(IntPtr module, string entryname);

	[DllImport("kernel32.dll")]
	public static extern IntPtr VirtualAlloc(IntPtr address, int size, int amem, int apage);

	[DllImport("kernel32.dll")]
	public static extern bool VirtualFree(IntPtr address, int size, int amem);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
	public static extern bool VirtualProtect(IntPtr address, int size, int pnew, out int pold);

	public unsafe static Win32MemoryHooker HookMemory(string modulename, string entryname, int memorysize)
	{
		IntPtr intPtr = LoadLibrary(modulename);
		if (intPtr == IntPtr.Zero)
		{
			return null;
		}
		IntPtr procAddress = GetProcAddress(intPtr, entryname);
		if (procAddress == IntPtr.Zero)
		{
			return null;
		}
		int pold = 0;
		VirtualProtect(procAddress, 16, 64, out pold);
		Win32MemoryHooker win32MemoryHooker = new Win32MemoryHooker(procAddress, memorysize);
		*(int*)(void*)(procAddress + 7) = *(int*)(void*)procAddress;
		*(short*)(void*)(procAddress + 11) = *(short*)(void*)(procAddress + 4);
		*(sbyte*)(void*)procAddress = -112;
		*(sbyte*)(void*)(procAddress + 1) = -112;
		*(sbyte*)(void*)(procAddress + 2) = -24;
		*(int*)(void*)(procAddress + 3) = (int)(win32MemoryHooker.HookCodePtr - ((int)procAddress + 7));
		return win32MemoryHooker;
	}

	public unsafe static void Unhook(Win32Hooker hooker)
	{
		IntPtr intPtr = hooker.Entry;
		*(int*)(void*)intPtr = *(int*)(void*)(intPtr + 6);
		*(short*)(void*)(intPtr + 4) = *(short*)(void*)(intPtr + 10);
	}

	public Win32Hooker(IntPtr _entry)
	{
		entry = _entry;
	}

	public virtual void Dispose()
	{
		if (!(entry == IntPtr.Zero))
		{
			entry = IntPtr.Zero;
			VirtualFree(hookcodeptr, hookcodesize, 16384);
			hookcodesize = 0;
			hookcodeptr = IntPtr.Zero;
		}
	}
}
