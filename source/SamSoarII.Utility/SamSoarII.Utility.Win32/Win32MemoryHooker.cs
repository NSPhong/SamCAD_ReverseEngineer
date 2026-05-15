using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Win32;

public class Win32MemoryHooker : Win32Hooker
{
	private IntPtr memptr;

	private int size;

	public IntPtr MemPtr => memptr;

	public int Size => size;

	public unsafe Win32MemoryHooker(IntPtr _entry, int _size)
		: base(_entry)
	{
		size = _size;
		memptr = Marshal.AllocHGlobal(size);
		byte[] hookCode = GetHookCode();
		hookcodesize = hookCode.Length;
		hookcodeptr = Win32Hooker.VirtualAlloc(IntPtr.Zero, hookcodesize, 4096, 64);
		Marshal.Copy(hookCode, 0, hookcodeptr, hookcodesize);
		*(int*)(void*)(hookcodeptr + 18) -= (int)hookcodeptr + 18 + 4;
	}

	public override void Dispose()
	{
		if (size != 0)
		{
			Marshal.FreeHGlobal(memptr);
			size = 0;
			memptr = IntPtr.Zero;
		}
	}

	protected byte[] GetHookCode()
	{
		List<byte> list = new List<byte>();
		IntPtr module = Win32Hooker.LoadLibrary("MSVCRT.DLL");
		IntPtr procAddress = Win32Hooker.GetProcAddress(module, "memcpy");
		list.Add(85);
		list.Add(139);
		list.Add(236);
		list.Add(131);
		list.Add(197);
		list.Add(8);
		list.Add(104);
		list.Add((byte)size);
		list.Add((byte)(size >> 8));
		list.Add((byte)(size >> 16));
		list.Add((byte)(size >> 24));
		list.Add(85);
		list.Add(104);
		list.Add((byte)(int)memptr);
		list.Add((byte)((int)memptr >> 8));
		list.Add((byte)((int)memptr >> 16));
		list.Add((byte)((int)memptr >> 24));
		list.Add(232);
		list.Add((byte)(int)procAddress);
		list.Add((byte)((int)procAddress >> 8));
		list.Add((byte)((int)procAddress >> 16));
		list.Add((byte)((int)procAddress >> 24));
		list.Add(93);
		list.Add(93);
		list.Add(93);
		list.Add(93);
		list.Add(195);
		return list.ToArray();
	}

	public unsafe int GetInt(int index)
	{
		return ((int*)(void*)memptr)[index];
	}
}
