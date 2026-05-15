using System.IO;
using System.Runtime.InteropServices;

namespace SamCAD.Import;

public class QImageEncoder
{
	[DllImport("QImageEncoder.dll", CallingConvention = CallingConvention.Cdecl)]
	protected static extern void Encode(string filein, string fileout);

	public byte[] GetQImageData(string filein)
	{
		string text = Path.Combine(Path.GetTempPath(), "tempqim.bin");
		Encode(filein, text);
		FileStream fileStream = File.OpenRead(text);
		byte[] array = new byte[fileStream.Length];
		fileStream.Read(array, 0, array.Length);
		return array;
	}
}
