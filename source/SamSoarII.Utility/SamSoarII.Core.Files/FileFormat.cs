using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using SamSoarII.Utility;

namespace SamSoarII.Core.Files;

public abstract class FileFormat
{
	public enum Flags
	{
		NONE = 0,
		TO_ENCRYPT_FILE = 1,
		TO_DOWNLOAD = 2,
		FROM_ENCRYPT_FILE = 4,
		FROM_UPLOAD = 8
	}

	private static byte[] filebytedata;

	private static IntPtr filedata;

	private static int filedatalength;

	private static FileHeader fileheader;

	private static FileHeaderInfo[] fileheaderinfos;

	private static MemoryStream freememory;

	private static readonly byte[] FileToken;

	private static readonly byte[] FileToken2;

	private static readonly byte[] FileToken3;

	private static readonly byte[] EncryptKey;

	private static readonly byte[] EncryptIV;

	private static readonly DESCryptoServiceProvider EncryptProvider;

	private static Flags flag;

	private static List<LocatedFileHeader> headers;

	private static List<LocatedFileHeaderString> hdstrings;

	private static List<LocatedFreeMemory> freemems;

	private static BinaryWriter freewriter;

	private static MemoryStream[] tstrmemory;

	private static BinaryWriter[] tstrwriter;

	private static MemoryStream fstrmemory;

	private static BinaryWriter fstrwriter;

	private static BinaryReader freereader;

	public static FileHeader FileHeader => fileheader;

	public static bool IsSamSoarIIFormattedFile
	{
		get
		{
			_ = filedata;
			if (filedatalength < 16 || fileheader == null)
			{
				return false;
			}
			return fileheader.szToken.SequenceEqual(FileToken) || fileheader.szToken.SequenceEqual(FileToken2) || fileheader.szToken.SequenceEqual(FileToken3);
		}
	}

	public static FileVersion GenerateVersion => new FileVersion
	{
		dwVersionMain = App.Version_App.Ver_Main,
		dwVersionSub = App.Version_App.Ver_Sub,
		dwVersionModify = App.Version_App.Ver_Modify
	};

	public static FileVersion FileVersion
	{
		get
		{
			if (!IsSamSoarIIFormattedFile)
			{
				return new FileVersion
				{
					dwVersionMain = App.Version_App.Ver_Main,
					dwVersionSub = App.Version_App.Ver_Sub,
					dwVersionModify = App.Version_App.Ver_Modify
				};
			}
			return fileheader.stFileVersion;
		}
	}

	public static Flags Flag => flag;

	public static BinaryWriter FreeWriter => freewriter;

	public static BinaryReader FreeReader => freereader;

	static FileFormat()
	{
		filebytedata = null;
		filedata = IntPtr.Zero;
		filedatalength = -1;
		FileToken = new byte[16]
		{
			165, 113, 43, 132, 202, 2, 239, 75, 149, 53,
			160, 156, 23, 125, 62, 248
		};
		FileToken2 = new byte[16]
		{
			140, 37, 180, 1, 147, 117, 110, 244, 186, 39,
			169, 100, 57, 210, 65, 118
		};
		FileToken3 = new byte[16]
		{
			27, 106, 77, 154, 189, 145, 52, 174, 72, 57,
			145, 187, 39, 208, 2, 57
		};
		EncryptKey = new byte[8] { 43, 161, 149, 76, 192, 215, 138, 109 };
		EncryptIV = new byte[8] { 24, 195, 73, 138, 110, 44, 240, 151 };
		EncryptProvider = new DESCryptoServiceProvider();
		fileheaderinfos = new FileHeaderInfo[121];
		for (int i = 0; i < 121; i++)
		{
			fileheaderinfos[i] = new FileHeaderInfo((FileHeaderTypes)i);
		}
	}

	public static void SaveReset(Flags _flag = Flags.TO_ENCRYPT_FILE)
	{
		flag = _flag;
		headers = new List<LocatedFileHeader>();
		hdstrings = new List<LocatedFileHeaderString>();
		freemems = new List<LocatedFreeMemory>();
		freememory = new MemoryStream();
		freewriter = new BinaryWriter(freememory);
		tstrmemory = new MemoryStream[16];
		tstrwriter = new BinaryWriter[16];
		fstrmemory = new MemoryStream();
		fstrwriter = new BinaryWriter(fstrmemory);
		fileheader = new FileHeader();
		fileheader.dwHeaderSize = fileheader.MaxHeaderSize;
		fileheader.stFileVersion = GenerateVersion;
		Array.Copy(FileToken3, fileheader.szToken, FileToken3.Length);
		headers.Add(new LocatedFileHeader(fileheader, 0));
	}

	public static LocatedFileHeader AllocHeader(FileHeaderTypes type, int headersize = -1)
	{
		LocatedFileHeader locatedFileHeader = headers.Last();
		IFileHeader fileHeader = null;
		LocatedFileHeader locatedFileHeader2 = null;
		fileHeader = fileheaderinfos[(int)type].CreateExpectedHeader(FileVersion);
		if (fileHeader == null)
		{
			return null;
		}
		fileHeader.HeaderSize = ((headersize > 0) ? headersize : fileHeader.MaxHeaderSize);
		locatedFileHeader2 = new LocatedFileHeader(fileHeader, locatedFileHeader.LPHeader + locatedFileHeader.Header.HeaderSize);
		headers.Add(locatedFileHeader2);
		return locatedFileHeader2;
	}

	public static LocatedFileHeaderString AllocHeaderString(IFileHeader header, int id, string text)
	{
		LocatedFileHeaderString locatedFileHeaderString = new LocatedFileHeaderString(header, id, text);
		hdstrings.Add(locatedFileHeaderString);
		return locatedFileHeaderString;
	}

	public static LocatedFreeMemory AllocFreeMemory(IFileHeader header, int id)
	{
		LocatedFreeMemory locatedFreeMemory = new LocatedFreeMemory(header, id, (int)freememory.Length);
		freemems.Add(locatedFreeMemory);
		return locatedFreeMemory;
	}

	public static string SaveFile(string filename)
	{
		LocatedFileHeader locatedFileHeader = headers.Last();
		LocatedStringByteData locatedStringByteData = null;
		string text = null;
		byte[] array = null;
		int num = 0;
		int num2 = 1;
		if (filedata != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(filedata);
			filedata = IntPtr.Zero;
		}
		hdstrings.Sort((LocatedFileHeaderString fs1, LocatedFileHeaderString fs2) => fs1.Text.CompareTo(fs2.Text));
		while (num < hdstrings.Count())
		{
			for (num2 = num + 1; num2 < hdstrings.Count() && hdstrings[num2].Text.Equals(hdstrings[num2 - 1].Text); num2++)
			{
			}
			text = hdstrings[num].Text;
			array = ValueConverter.GetBytes(text);
			if (array.Length == 0)
			{
				for (int num3 = num; num3 < num2; num3++)
				{
					hdstrings[num3].Data = null;
					hdstrings[num3].Header.SetStrPtr(hdstrings[num3].ID, -1);
				}
			}
			else if (array.Length < 16)
			{
				if (tstrmemory[array.Length] == null)
				{
					tstrmemory[array.Length] = new MemoryStream();
					tstrwriter[array.Length] = new BinaryWriter(tstrmemory[array.Length]);
				}
				locatedStringByteData = new LocatedStringByteData(array, (int)tstrmemory[array.Length].Length);
				for (int num4 = num; num4 < num2; num4++)
				{
					hdstrings[num4].Data = locatedStringByteData;
				}
				tstrwriter[array.Length].Write(array);
			}
			else
			{
				locatedStringByteData = new LocatedStringByteData(array, (int)fstrmemory.Length);
				for (int num5 = num; num5 < num2; num5++)
				{
					hdstrings[num5].Data = locatedStringByteData;
				}
				fstrwriter.Write(array.Length);
				fstrwriter.Write(array);
			}
			num = num2;
		}
		filedatalength = 0;
		fileheader.lpFormattedMemory = filedatalength;
		filedatalength += locatedFileHeader.LPHeader + locatedFileHeader.Header.HeaderSize;
		fileheader.lpFreeMemory = filedatalength;
		filedatalength += (int)freememory.Length;
		fileheader.lpTinyString[0] = filedatalength;
		fileheader.lpTinyString[1] = filedatalength;
		for (int num6 = 1; num6 < 16; num6++)
		{
			int num7 = (int)((tstrmemory[num6] != null) ? tstrmemory[num6].Length : 0);
			filedatalength += num7;
			if (num6 + 1 < 16)
			{
				fileheader.lpTinyString[num6 + 1] = filedatalength;
			}
			else
			{
				fileheader.lpFreeString = filedatalength;
			}
		}
		filedatalength += (int)fstrmemory.Length;
		filedatalength += FileToken3.Length;
		foreach (LocatedFileHeaderString hdstring in hdstrings)
		{
			if (hdstring.Data == null)
			{
				hdstring.Header.SetStrPtr(hdstring.ID, -1);
			}
			else if (hdstring.Data.Data.Length < 16)
			{
				hdstring.Header.SetStrPtr(hdstring.ID, fileheader.lpTinyString[hdstring.Data.Data.Length] + hdstring.Data.LocalOffset);
			}
			else
			{
				hdstring.Header.SetStrPtr(hdstring.ID, fileheader.lpFreeString + hdstring.Data.LocalOffset);
			}
		}
		foreach (LocatedFreeMemory freemem in freemems)
		{
			freemem.Header.SetFreePtr(freemem.ID, fileheader.lpFreeMemory + freemem.LocalOffset);
		}
		filedata = Marshal.AllocHGlobal(filedatalength);
		int num8 = 0;
		foreach (LocatedFileHeader header in headers)
		{
			num8 = header.LPHeader;
			header.Header.Save(filedata + num8);
		}
		num8 = fileheader.lpFreeMemory;
		array = freememory.ToArray();
		Marshal.Copy(array, 0, filedata + num8, array.Length);
		for (int num9 = 1; num9 < 16; num9++)
		{
			num8 = fileheader.lpTinyString[num9];
			if (tstrmemory[num9] != null)
			{
				array = tstrmemory[num9].ToArray();
				Marshal.Copy(array, 0, filedata + num8, array.Length);
			}
		}
		num8 = fileheader.lpFreeString;
		array = fstrmemory.ToArray();
		Marshal.Copy(array, 0, filedata + num8, array.Length);
		Marshal.Copy(FileToken3, 0, filedata + filedatalength - FileToken3.Length, FileToken3.Length);
		filebytedata = new byte[filedatalength];
		Marshal.Copy(filedata, filebytedata, 0, filedatalength);
		FileStream fileStream = null;
		string text2 = $"{FileHelper.AppRootPath}\\rar\\temp";
		string text3 = $"{FileHelper.AppRootPath}\\linker.map";
		try
		{
			if ((Flag & Flags.TO_ENCRYPT_FILE) != Flags.NONE)
			{
				filebytedata = Encrypt(filebytedata);
				filedatalength = filebytedata.Length;
			}
			else
			{
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				filename = string.Format("{0}\\{1}.{2}", text2, "project", "ssr");
			}
			string path = $"{FileHelper.AppRootPath}\\save";
			string destFileName = string.Format("{0}\\save\\{2:d4}{3:d2}{4:d2}_{5:d2}{6:d2}{7:d2}_{1}", FileHelper.AppRootPath, Path.GetFileName(filename), DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			if (File.Exists(filename))
			{
				File.Copy(filename, destFileName, overwrite: true);
			}
			fileStream = File.Create(filename);
			fileStream.Write(filebytedata, 0, filedatalength);
			fileStream.Flush();
			fileStream.Close();
			fileStream = null;
			if ((Flag & Flags.TO_DOWNLOAD) != Flags.NONE)
			{
				string text4 = FileHelper.CompressFile(filename, text3);
				File.Delete(filename);
				filename = text4;
			}
			return filename;
		}
		catch (Exception)
		{
			return null;
		}
		finally
		{
			fileStream?.Close();
		}
	}

	public static byte[] Encrypt(byte[] data)
	{
		MemoryStream memoryStream = null;
		CryptoStream cryptoStream = null;
		byte[] array = null;
		try
		{
			memoryStream = new MemoryStream();
			cryptoStream = new CryptoStream(memoryStream, EncryptProvider.CreateEncryptor(EncryptKey, EncryptIV), CryptoStreamMode.Write);
			cryptoStream.Write(data, 0, data.Length);
			cryptoStream.Close();
			cryptoStream = null;
			array = memoryStream.ToArray();
			memoryStream.Close();
			memoryStream = null;
			return array;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			cryptoStream?.Close();
			memoryStream?.Close();
		}
	}

	public static void LoadReset(Flags _flag = Flags.FROM_ENCRYPT_FILE)
	{
		flag = _flag;
		headers = new List<LocatedFileHeader>();
		hdstrings = new List<LocatedFileHeaderString>();
		freemems = new List<LocatedFreeMemory>();
	}

	public static bool LoadFile(ref string filename)
	{
		FileStream fileStream = null;
		string text = $"{FileHelper.AppRootPath}\\rar\\temp";
		try
		{
			if (filedata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(filedata);
				filedata = IntPtr.Zero;
			}
			if ((Flag & Flags.FROM_UPLOAD) != Flags.NONE)
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				FileHelper.DecompressFile(filename, text);
				File.Delete(filename);
				filename = null;
				string[] files = Directory.GetFiles(text);
				foreach (string text2 in files)
				{
					if (text2.EndsWith("ssr"))
					{
						filename = text2;
						break;
					}
				}
				if (filename == null)
				{
					return false;
				}
			}
			string path = $"{FileHelper.AppRootPath}\\save";
			string text3 = string.Format("{0}\\save\\{2:d4}{3:d2}{4:d2}_{5:d2}{6:d2}{7:d2}_{1}", FileHelper.AppRootPath, Path.GetFileName(filename), DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			File.Copy(filename, text3, overwrite: true);
			fileStream = File.OpenRead(text3);
			filedatalength = (int)fileStream.Length;
			filebytedata = new byte[filedatalength];
			fileStream.Read(filebytedata, 0, filedatalength);
			fileStream.Close();
			fileStream = null;
			if ((Flag & Flags.FROM_ENCRYPT_FILE) != Flags.NONE)
			{
				filebytedata = Decrypt(filebytedata);
				filedatalength = filebytedata.Length;
			}
			freememory = new MemoryStream(filebytedata);
			freereader = new BinaryReader(freememory);
			filedata = Marshal.AllocHGlobal(filedatalength);
			Marshal.Copy(filebytedata, 0, filedata, filedatalength);
			fileheader = (FileHeader)GetHeader(0, FileHeaderTypes.File);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
		finally
		{
			fileStream?.Close();
		}
	}

	public static IFileHeader GetHeader(int lpHeader, FileHeaderTypes type)
	{
		if (lpHeader < 0)
		{
			return null;
		}
		IFileHeader fileHeader = null;
		fileHeader = fileheaderinfos[(int)type].CreateExpectedHeader(FileVersion);
		fileHeader?.Load(filedata + lpHeader);
		return fileHeader;
	}

	public static string GetString(int sp)
	{
		int num = -1;
		int num2 = -1;
		byte[] array = null;
		if (sp < 0)
		{
			return "";
		}
		for (int i = 1; i < 16; i++)
		{
			num = fileheader.lpTinyString[i];
			num2 = ((i + 1 < 16) ? fileheader.lpTinyString[i + 1] : fileheader.lpFreeString);
			if (num2 - num > 0 && sp >= num && sp < num2)
			{
				array = new byte[i];
				Marshal.Copy(filedata + sp, array, 0, i);
			}
		}
		if (array == null && sp < filedatalength)
		{
			num = fileheader.lpFreeString;
			int num3 = Marshal.ReadInt32(filedata + sp);
			sp += 4;
			if (num3 > 0 && sp + num3 <= filedatalength)
			{
				array = new byte[num3];
				Marshal.Copy(filedata + sp, array, 0, num3);
			}
		}
		return (array != null) ? ValueConverter.ParseToString(array) : string.Empty;
	}

	public static BinaryReader GetFreeReader(int lp)
	{
		freereader.BaseStream.Position = lp;
		return freereader;
	}

	public static byte[] Decrypt(byte[] data)
	{
		MemoryStream memoryStream = null;
		CryptoStream cryptoStream = null;
		byte[] array = null;
		try
		{
			memoryStream = new MemoryStream();
			cryptoStream = new CryptoStream(memoryStream, EncryptProvider.CreateDecryptor(EncryptKey, EncryptIV), CryptoStreamMode.Write);
			cryptoStream.Write(data, 0, data.Length);
			array = memoryStream.ToArray();
			memoryStream.Close();
			memoryStream = null;
			return array;
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			memoryStream?.Close();
		}
	}
}
