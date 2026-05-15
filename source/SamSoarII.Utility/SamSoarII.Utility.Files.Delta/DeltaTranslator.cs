using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaTranslator : IDisposable
{
	private static int szbuff;

	private static int szunzip;

	private static IntPtr pbuff;

	private static StringBuilder sbbuff = new StringBuilder();

	private string filename;

	public string FileName => filename;

	[DllImport("deltadll/DELZIP179.DLL")]
	public static extern int DZ_UnzExec(ref DelZip179UnzipInfo info);

	protected static int DZ_Callback(IntPtr caller, ref DelZip179CallbackInfo info)
	{
		if (info.actioncode == 13)
		{
			try
			{
				if (pbuff == IntPtr.Zero)
				{
					szbuff = 4194304;
					pbuff = Marshal.AllocHGlobal(szbuff);
					info.refs[0] = (int)pbuff;
				}
				else
				{
					szunzip = info.args[2];
				}
				info.actioncode = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		return 0;
	}

	public DeltaTranslator(string _filename)
	{
		filename = _filename;
	}

	public void Dispose()
	{
		filename = null;
	}

	protected int GetInt(byte[] data, int start)
	{
		int num = 0;
		for (int num2 = System.Math.Min(start + 3, data.Length - 1); num2 >= start; num2--)
		{
			num = (num << 8) + data[num2];
		}
		return num;
	}

	protected unsafe IntPtr AllocBuffC()
	{
		IntPtr intPtr = Marshal.AllocHGlobal(12);
		IntPtr intPtr2 = Marshal.AllocHGlobal(8);
		*(int*)(void*)intPtr2 = (int)intPtr;
		*(int*)(void*)(intPtr2 + 4) = 12;
		return intPtr2;
	}

	protected unsafe void FreeBuffC(IntPtr p0)
	{
		IntPtr hglobal = (IntPtr)(uint)(*(int*)(void*)p0);
		Marshal.FreeHGlobal(hglobal);
		Marshal.FreeHGlobal(p0);
	}

	protected void FreeInfo(DelZip179UnzipInfo i)
	{
		if (i.caller != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(i.caller);
		}
		if (i.inbuffer != IntPtr.Zero)
		{
			FreeBuffC(i.inbuffer);
		}
		if (i.outbuffer != IntPtr.Zero)
		{
			FreeBuffC(i.outbuffer);
		}
		if (i.errbuffer != IntPtr.Zero)
		{
			FreeBuffC(i.errbuffer);
		}
		if (i.errbuffer1 != IntPtr.Zero)
		{
			FreeBuffC(i.errbuffer1);
		}
		if (i.errbuffer2 != IntPtr.Zero)
		{
			FreeBuffC(i.errbuffer2);
		}
		if (i.pinput != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(i.pinput);
		}
		if (i.poutput != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(i.poutput);
		}
	}

	public unsafe DeltaProject Translate()
	{
		FileStream fileStream = null;
		byte[] array = null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		DelZip179UnzipInfo info = default(DelZip179UnzipInfo);
		Window window = (Window)App.IFParent.MainWindow;
		try
		{
			fileStream = File.Open(filename, FileMode.Open);
			array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			fileStream?.Close();
		}
		num = GetInt(array, 152);
		num2 = GetInt(array, 88);
		if (num2 == 0)
		{
			num2 = array.Length;
		}
		num3 = num2 - num;
		info.hwnd = new WindowInteropHelper(window).Handle;
		info.caller = IntPtr.Zero;
		info.vermain = 179;
		info.callback = DZ_Callback;
		info.zcallbackfunc = 0;
		info.zstreamfunc = 0;
		info.verbosity = 0;
		info.inbuffer = IntPtr.Zero;
		info.iboptions = new int[4] { 0, 0, 0, 1 };
		info.outbuffer = IntPtr.Zero;
		info.oboptions = new int[4] { 0, 0, 0, 1 };
		info.errbuffer = IntPtr.Zero;
		info.errbuffer1 = IntPtr.Zero;
		info.errbuffer2 = IntPtr.Zero;
		info.eboptions = new int[2] { 0, 1 };
		info.poutput = IntPtr.Zero;
		info.szoutput = num3;
		info.outputoption = 1;
		info.pinput = IntPtr.Zero;
		info.szinput = num3;
		info.inputoption = 1;
		info.filepath = IntPtr.Zero;
		info.fileoptions = new int[8];
		info.endcode = 7;
		try
		{
			info.inbuffer = AllocBuffC();
			info.errbuffer1 = AllocBuffC();
			info.errbuffer2 = AllocBuffC();
			info.poutput = Marshal.AllocHGlobal(num3);
			info.pinput = Marshal.AllocHGlobal(num3);
			int cb = Marshal.SizeOf((object)info);
			IntPtr ptr = Marshal.AllocHGlobal(cb);
			Marshal.StructureToPtr((object)info, ptr, false);
			Marshal.Copy(array, num, info.pinput, num3);
			szbuff = 0;
			pbuff = IntPtr.Zero;
			sbbuff.Clear();
			int num4 = DZ_UnzExec(ref info);
		}
		catch (Exception ex2)
		{
			throw ex2;
		}
		finally
		{
			if (pbuff != IntPtr.Zero && szunzip > 0)
			{
				string value = ValueConverter.ToString((sbyte*)(void*)pbuff, szunzip, Encoding.Default);
				sbbuff = new StringBuilder(value);
			}
			FreeInfo(info);
			if (pbuff != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pbuff);
			}
			pbuff = IntPtr.Zero;
			szbuff = 0;
		}
		DeltaDMLDocument document = new DeltaDMLDocument(sbbuff.ToString());
		return new DeltaProject(document);
	}
}
