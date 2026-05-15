using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace SamSoarII.Utility.Win32;

public class UpdateResourceHelper
{
	[StructLayout(LayoutKind.Explicit)]
	public class ICONDIRENTRY
	{
		[FieldOffset(0)]
		public byte bWidth;

		[FieldOffset(1)]
		public byte bHeight;

		[FieldOffset(2)]
		public byte bColorCount;

		[FieldOffset(3)]
		public byte bReserved;

		[FieldOffset(4)]
		public ushort wPlanes;

		[FieldOffset(6)]
		public ushort wBitCount;

		[FieldOffset(8)]
		public uint dwBytesInRes;

		[FieldOffset(12)]
		public uint dwImageOffset;
	}

	[StructLayout(LayoutKind.Explicit)]
	public class ICONDIR
	{
		[FieldOffset(0)]
		public short idReserved;

		[FieldOffset(2)]
		public short idType;

		[FieldOffset(4)]
		public short idCount;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct GRPICONDIRENTRY
	{
		[FieldOffset(0)]
		public byte bWidth;

		[FieldOffset(1)]
		public byte bHeight;

		[FieldOffset(2)]
		public byte bColorCount;

		[FieldOffset(3)]
		public byte bReserved;

		[FieldOffset(4)]
		public ushort wPlanes;

		[FieldOffset(6)]
		public ushort wBitCount;

		[FieldOffset(8)]
		public uint dwBytesInRes;

		[FieldOffset(12)]
		public short nID;
	}

	[StructLayout(LayoutKind.Explicit)]
	public class GRPICONDIR
	{
		[FieldOffset(0)]
		public short idReserved;

		[FieldOffset(2)]
		public short idType;

		[FieldOffset(4)]
		public short idCount;

		[FieldOffset(8)]
		public GRPICONDIRENTRY idEntries;
	}

	public static readonly IntPtr RT_ICON = MakeIntResource(3);

	public static readonly IntPtr RT_GROUP_ICON = MakeIntResource(14);

	public static IntPtr MakeIntResource(int i)
	{
		return (IntPtr)(long)(ushort)i;
	}

	[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr BeginUpdateResource(string filename, [MarshalAs(UnmanagedType.Bool)] bool todelete);

	[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool UpdateResource(IntPtr hd, IntPtr type, IntPtr name, ushort wlang, IntPtr pdata, int szdata);

	[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool EndUpdateResource(IntPtr hd, bool todiscard);

	public unsafe void ChangeIcon(string fnexe, string fnicon)
	{
		FileStream fileStream = null;
		ICONDIR iCONDIR = new ICONDIR();
		ICONDIRENTRY iCONDIRENTRY = new ICONDIRENTRY();
		GRPICONDIR gRPICONDIR = new GRPICONDIR();
		byte[] array2;
		try
		{
			fileStream = File.Open(fnicon, FileMode.Open, FileAccess.Read);
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fixed (byte* ptr = &array[0])
			{
				IntPtr intPtr = (IntPtr)ptr;
				Marshal.PtrToStructure(intPtr, (object)iCONDIR);
				Marshal.PtrToStructure(intPtr + 6, (object)iCONDIRENTRY);
				array2 = new byte[iCONDIRENTRY.dwBytesInRes];
				Marshal.Copy(intPtr + (int)iCONDIRENTRY.dwImageOffset, array2, 0, array2.Length);
			}
			gRPICONDIR.idCount = iCONDIR.idCount;
			gRPICONDIR.idReserved = 0;
			gRPICONDIR.idType = 1;
			gRPICONDIR.idEntries.bWidth = iCONDIRENTRY.bWidth;
			gRPICONDIR.idEntries.bHeight = iCONDIRENTRY.bHeight;
			gRPICONDIR.idEntries.bColorCount = iCONDIRENTRY.bColorCount;
			gRPICONDIR.idEntries.bReserved = iCONDIRENTRY.bReserved;
			gRPICONDIR.idEntries.wPlanes = iCONDIRENTRY.wPlanes;
			gRPICONDIR.idEntries.wBitCount = iCONDIRENTRY.wBitCount;
			gRPICONDIR.idEntries.dwBytesInRes = iCONDIRENTRY.dwBytesInRes;
			gRPICONDIR.idEntries.nID = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return;
		}
		finally
		{
			fileStream?.Close();
		}
		IntPtr intPtr2 = IntPtr.Zero;
		IntPtr intPtr3 = IntPtr.Zero;
		try
		{
			intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf((object)gRPICONDIR));
			intPtr2 = BeginUpdateResource(fnexe, todelete: false);
			Marshal.StructureToPtr((object)gRPICONDIR, intPtr3, false);
			UpdateResource(intPtr2, RT_GROUP_ICON, MakeIntResource(1), 0, intPtr3, Marshal.SizeOf((object)gRPICONDIR));
			fixed (byte* ptr2 = &array2[0])
			{
				UpdateResource(intPtr2, RT_ICON, MakeIntResource(1), 0, (IntPtr)ptr2, array2.Length);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
			return;
		}
		finally
		{
			if (intPtr3 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr3);
			}
			if (intPtr2 != IntPtr.Zero)
			{
				EndUpdateResource(intPtr2, todiscard: false);
			}
		}
		MessageBox.Show("The icon has been set successfully!");
	}
}
