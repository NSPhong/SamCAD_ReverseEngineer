using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SamSoarII.Utility;

public static class FileHelper
{
	public const string NewFileExtension = "ssr";

	public const string OldFileExtension = "ssp";

	public const string Zip7FileExtension = "7z";

	public const string GXWFileExtension = "gxw";

	public const string KVSProjectExtension = "kpr";

	public const string KVSModuleExtension = "mod";

	public const string KVSMacroExtension = "mcr";

	public const string KVSCm1Extension = "cm1";

	public const string KVSLabelExtension = "lbl";

	public const string DeltaFileExtension = "isp";

	public const string XDPProjectExtension = "xdp";

	public const string FPXProjectExtension = "fpx";

	public const string Step7SmartExtension = "smart";

	public static string AppRootPath => Directory.GetParent(Application.ExecutablePath).FullName;

	public static string GetTempFile(string postfix)
	{
		string tempFileName;
		string text;
		while (true)
		{
			tempFileName = Path.GetTempFileName();
			text = Path.ChangeExtension(tempFileName, postfix);
			if (!File.Exists(text))
			{
				break;
			}
			File.Delete(tempFileName);
		}
		File.Move(tempFileName, text);
		return text;
	}

	public static string ChangeExtension(string file, string extension)
	{
		return Path.ChangeExtension(file, extension);
	}

	public static string GetMD5(string filename)
	{
		try
		{
			FileStream fileStream = new FileStream(filename, FileMode.Open);
			MD5 mD = new MD5CryptoServiceProvider();
			byte[] array = mD.ComputeHash(fileStream);
			fileStream.Close();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}
		catch (Exception ex)
		{
			throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
		}
	}

	public static long GetFileLength(string fullFileName)
	{
		if (File.Exists(fullFileName))
		{
			return new FileInfo(fullFileName).Length;
		}
		return 0L;
	}

	public static string GetFullFileName(string filename, string extension)
	{
		return $"{StringHelper.RemoveSystemSeparator(AppRootPath)}\\rar\\temp\\{filename}.{extension}";
	}

	public static string CompressFile(params string[] filenames)
	{
		Process process = new Process();
		string arg = $"{StringHelper.RemoveSystemSeparator(AppRootPath)}\\rar";
		string text = $"{StringHelper.RemoveSystemSeparator(AppRootPath)}\\rar\\temp\\{Path.GetFileNameWithoutExtension(filenames.FirstOrDefault())}.7z";
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append($"a -t7z -pSamSoarII \"{text}\"");
		foreach (string arg2 in filenames)
		{
			stringBuilder.Append($" \"{arg2}\"");
		}
		process.StartInfo.FileName = string.Format("{0}\\{1}", arg, "HaoZipC.exe");
		process.StartInfo.Arguments = stringBuilder.ToString();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.CreateNoWindow = true;
		process.Start();
		process.WaitForExit();
		process.Close();
		try
		{
			Process[] processesByName = Process.GetProcessesByName("HaoZipC.exe");
			foreach (Process process2 in processesByName)
			{
				process2.Kill();
			}
		}
		catch (Exception)
		{
		}
		return text;
	}

	public static void DecompressFile(string fullFileName, string outPath)
	{
		try
		{
			Process process = new Process();
			string arg = string.Format("{0}\\{1}", StringHelper.RemoveSystemSeparator(AppRootPath), "rar");
			process.StartInfo.FileName = string.Format("{0}\\{1}", arg, "HaoZipC.exe");
			process.StartInfo.Arguments = $"e -pSamSoarII \"{fullFileName}\" -o\"{outPath}\"";
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
			process.WaitForExit();
			process.Close();
			Process[] processesByName = Process.GetProcessesByName("HaoZipC.exe");
			foreach (Process process2 in processesByName)
			{
				process2.Kill();
			}
		}
		catch (Exception)
		{
			Process[] processesByName2 = Process.GetProcessesByName("HaoZipC.exe");
			foreach (Process process3 in processesByName2)
			{
				process3.Kill();
			}
		}
	}

	public static bool InvalidFileName(string fullFileName)
	{
		return fullFileName == null || fullFileName == string.Empty;
	}

	public static byte[] GetBytesByBinaryFile(string fullFileName)
	{
		List<byte> list = new List<byte>();
		BinaryReader binaryReader = new BinaryReader(new FileStream(fullFileName, FileMode.Open));
		while (binaryReader.BaseStream.CanRead)
		{
			try
			{
				list.Add(binaryReader.ReadByte());
			}
			catch (EndOfStreamException)
			{
				break;
			}
		}
		binaryReader.Close();
		return list.ToArray();
	}

	public static void GenerateBinaryFile(string fullFileName, byte[] data)
	{
		BinaryWriter binaryWriter = new BinaryWriter(new FileStream(fullFileName, FileMode.Create, FileAccess.Write));
		for (int i = 0; i < data.Length; i++)
		{
			try
			{
				binaryWriter.Write(data[i]);
			}
			catch (Exception)
			{
				break;
			}
		}
		binaryWriter.Flush();
		binaryWriter.Close();
	}

	public static void Encrypt()
	{
		string text = $"{AppRootPath}\\downg";
		string[] files = Directory.GetFiles(text);
		foreach (string text2 in files)
		{
			if (text2.EndsWith(".a") || text2.EndsWith(".ld"))
			{
				string outFile = $"{text}\\{Path.GetFileNameWithoutExtension(text2)}{Path.GetExtension(text2)}e";
				DESFileClass.EncryptFile(text2, outFile, "SamSoarII");
			}
		}
	}

	public static void Decrypt(string infile, string outfile)
	{
		DESFileClass.DecryptFile(infile, outfile, "SamSoarII");
	}

	public static Encoding GetFileEncoding(string filename)
	{
		if (!File.Exists(filename))
		{
			return Encoding.Default;
		}
		BinaryReader binaryReader = null;
		try
		{
			FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
			binaryReader = new BinaryReader(fileStream);
			byte[] array = binaryReader.ReadBytes((int)fileStream.Length);
			if (IsUTF8Bytes(array) || (array[0] == 239 && array[1] == 187))
			{
				return Encoding.UTF8;
			}
			if (array[0] == 254 && array[1] == byte.MaxValue)
			{
				return Encoding.BigEndianUnicode;
			}
			if (array[0] == byte.MaxValue && array[1] == 254)
			{
				return Encoding.Unicode;
			}
			return Encoding.Default;
		}
		catch (Exception)
		{
			return Encoding.Default;
		}
		finally
		{
			binaryReader?.Close();
		}
	}

	private static bool IsUTF8Bytes(byte[] data)
	{
		int num = 1;
		for (int i = 0; i < data.Length; i++)
		{
			byte b = data[i];
			if (num == 1)
			{
				if (b >= 128)
				{
					while (((b <<= 1) & 0x80) != 0)
					{
						num++;
					}
					if (num == 1 || num > 6)
					{
						return false;
					}
				}
			}
			else
			{
				if ((b & 0xC0) != 128)
				{
					return false;
				}
				num--;
			}
		}
		if (num > 1)
		{
			throw new Exception("Unexpected Byte format!");
		}
		return true;
	}
}
