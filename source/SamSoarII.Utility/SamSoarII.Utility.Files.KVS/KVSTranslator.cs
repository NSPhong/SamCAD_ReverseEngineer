using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSTranslator : IDisposable
{
	public static readonly byte[] ZERO_ZONE;

	public const ushort CODE_PROJECT = 1;

	public const ushort CODE_MODULE = 2;

	public const ushort CODE_BASE = 4;

	public const ushort CODE_VAR = 5;

	public const ushort CODE_LADDER = 7;

	public const ushort CODE_COMMENT_LINE = 8;

	public const ushort CODE_SCRIPT = 9;

	public const ushort CODE_EXARGS = 11;

	public const ushort CODE_STRING = 12;

	public const ushort CODE_GLOBALLABEL = 29;

	public const ushort CODE_COMMENT_GLOBALLABEL = 30;

	public const ushort CODE_LOCALLABEL = 31;

	public const ushort CODE_COMMENT_LOCALLABEL = 32;

	public const ushort CODE_COMMENT_R = 160;

	public const ushort CODE_COMMENT_DM = 161;

	public const ushort CODE_COMMENT_T = 162;

	public const ushort CODE_COMMENT_C = 163;

	public const ushort CODE_COMMENT_TM = 164;

	public const ushort CODE_COMMENT_MR = 165;

	public const ushort CODE_COMMENT_LR = 166;

	public const ushort CODE_COMMENT_EM = 167;

	public const ushort CODE_COMMENT_FM = 168;

	public const ushort CODE_COMMENT_CR = 169;

	public const ushort CODE_COMMENT_CM = 170;

	public const ushort CODE_COMMENT_CTC = 171;

	public const ushort CODE_COMMENT_CTH = 172;

	public const ushort CODE_COMMENT_B = 174;

	public const ushort CODE_COMMENT_W = 175;

	public static readonly List<KVSUnitFormat> Formats;

	public static readonly KVSUnitFormat[] FormatOfCodes;

	private string filepath;

	public string FilePath => filepath;

	[DllImport("kvsdll/KvComm.dll")]
	public static extern int Kv2PxNdks(int arg1, int arg2);

	[DllImport("kvsdll/KvComm.dll")]
	public static extern int Kv2PxKdn(IntPtr pdata, int szfile, IntPtr pfile, int szdata);

	static KVSTranslator()
	{
		ZERO_ZONE = new byte[16];
		Formats = new List<KVSUnitFormat>();
		FormatOfCodes = new KVSUnitFormat[65536];
		KVSUnitFormat kVSUnitFormat = null;
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LD",
			Code = 0,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDB",
			Code = 1,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDP",
			Code = 6,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDF",
			Code = 7,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDPB",
			Code = 57350,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDFB",
			Code = 57351,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BLD",
			Code = 12,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BLDB",
			Code = 13,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "=",
			Code = 18,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "<>",
			Code = 23,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = ">",
			Code = 20,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "<",
			Code = 19,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = ">=",
			Code = 22,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "<=",
			Code = 21,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "OUT",
			Code = 36,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "OUB",
			Code = 37,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SET",
			Code = 38,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RES",
			Code = 39,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "KEEP",
			Code = 40,
			Shape = KVSUnitShape.OutputRect,
			Height = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DIFU",
			Code = 41,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DIFD",
			Code = 42,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ONDL",
			Code = 43,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "OFDL",
			Code = 44,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SHOT",
			Code = 45,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FLIK",
			Code = 46,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ALT",
			Code = 47,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BOUT",
			Code = 48,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BOUB",
			Code = 49,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BSET",
			Code = 50,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BRES",
			Code = 51,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TMR",
			Code = 52,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TMH",
			Code = 53,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TMS",
			Code = 54,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TMU",
			Code = 57356,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UDT",
			Code = 58,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "C",
			Code = 55,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "OUTC",
			Code = 57344,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ITVL",
			Code = 56,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UDC",
			Code = 57,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "END",
			Code = 60,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ENDH",
			Code = 61,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "INV",
			Code = 57347,
			Shape = KVSUnitShape.Line
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MEP",
			Code = 57348,
			Shape = KVSUnitShape.Line
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MEF",
			Code = 57349,
			Shape = KVSUnitShape.Line
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSX",
			Code = 57345,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSY",
			Code = 57346,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SFT",
			Code = 68,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MEMSW",
			Code = 69,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "STP",
			Code = 70,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "STE",
			Code = 71,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "STG",
			Code = 72,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "JMP",
			Code = 73,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ENDS",
			Code = 74,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "W-ON",
			Code = 75,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "W-OFF",
			Code = 76,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "W-UE",
			Code = 77,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "W-DE",
			Code = 78,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MC",
			Code = 79,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MCR",
			Code = 80,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALL",
			Code = 81,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SBR",
			Code = 82,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RET",
			Code = 83,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ECALL",
			Code = 57624,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FOR",
			Code = 84,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "NEXT",
			Code = 85,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BREAK",
			Code = 86,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CJ",
			Code = 57600,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "NCJ",
			Code = 57614,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "GOTO",
			Code = 57601,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LABEL",
			Code = 57602,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SCJ",
			Code = 57623,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MCALL",
			Code = 57604,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MSTRT",
			Code = 57605,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MEND",
			Code = 57606,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MDSTRT",
			Code = 57607,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MDSTOP",
			Code = 57608,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ZPUSH",
			Code = 57615,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ZPOP",
			Code = 57616,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADRSET",
			Code = 57609,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADRINC",
			Code = 57610,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADRDEC",
			Code = 57611,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADRADD",
			Code = 57612,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADRSUB",
			Code = 57613,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FRSET",
			Code = 57617,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FRSTM",
			Code = 57621,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FRLDM",
			Code = 57622,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UREAD",
			Code = 57618,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UWRIT",
			Code = 57619,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UFILL",
			Code = 57620,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MOV",
			Code = 91,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDA",
			Code = 88,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "STA",
			Code = 89,
			Shape = KVSUnitShape.InputOut
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PLDA",
			Code = 57893,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PSTA",
			Code = 57894,
			Shape = KVSUnitShape.InputOut
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TMIN",
			Code = 90,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DW",
			Code = 87,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BMOV",
			Code = 92,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FMOV",
			Code = 93,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PMOV",
			Code = 57856,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BYLMOV",
			Code = 58195,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BYBMOV",
			Code = 58196,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ADD",
			Code = 94,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SUB",
			Code = 95,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MUL",
			Code = 96,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DIV",
			Code = 97,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "INC",
			Code = 98,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DEC",
			Code = 99,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ROOT",
			Code = 100,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "POW",
			Code = 57897,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CMP",
			Code = 101,
			Shape = KVSUnitShape.InputOut
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ZCMP",
			Code = 102,
			Shape = KVSUnitShape.InputOut
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ANDA",
			Code = 103,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ORA",
			Code = 104,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "EORA",
			Code = 105,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ENRA",
			Code = 57857,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "COM",
			Code = 106,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "NEG",
			Code = 107,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SRA",
			Code = 108,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SLA",
			Code = 109,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ASRA",
			Code = 57907,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ASLA",
			Code = 57908,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RRA",
			Code = 110,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RLA",
			Code = 111,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RRNCA",
			Code = 57860,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RLNCA",
			Code = 57861,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "WSR",
			Code = 112,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "WSL",
			Code = 113,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BSR",
			Code = 57858,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BSL",
			Code = 57859,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LIMIT",
			Code = 57875,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BANDC",
			Code = 57876,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ZONE",
			Code = 57877,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "APR",
			Code = 57878,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RAMP",
			Code = 58191,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TPOUT",
			Code = 58192,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LLFLT",
			Code = 58193,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TBCD",
			Code = 114,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TBIN",
			Code = 115,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MPX",
			Code = 116,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DMX",
			Code = 117,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "GRY",
			Code = 118,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RGRY",
			Code = 119,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DISN",
			Code = 57889,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UNIN",
			Code = 57890,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DISB",
			Code = 57891,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UNIB",
			Code = 57892,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SWAP",
			Code = 124,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BSWAP",
			Code = 58197,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "XCH",
			Code = 125,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DECO",
			Code = 57862,
			Shape = KVSUnitShape.InputOut
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ENCO",
			Code = 57863,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ABS",
			Code = 57896,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CPMSET",
			Code = 58201,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CPMGET",
			Code = 58200,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FLOAT",
			Code = 126,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DFLOAT",
			Code = 57903,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "INTG",
			Code = 127,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DINTG",
			Code = 57904,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DFTOF",
			Code = 57906,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FTODF",
			Code = 57905,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DISF",
			Code = 129,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.FLOAT
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UNIF",
			Code = 130,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.FLOAT
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "EXP",
			Code = 131,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LOG",
			Code = 132,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LOG10",
			Code = 57898,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RAD",
			Code = 133,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DEG",
			Code = 134,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SIN",
			Code = 135,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "COS",
			Code = 136,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TAN",
			Code = 137,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ASIN",
			Code = 138,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ACOS",
			Code = 139,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ATAN",
			Code = 140,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ASC",
			Code = 141,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RASC",
			Code = 142,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DASC",
			Code = 143,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RDASC",
			Code = 144,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "HASC",
			Code = 57864,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RHASC",
			Code = 57865,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FASC",
			Code = 57866,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.FLOAT
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFASC",
			Code = 57867,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.FLOAT
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LEN",
			Code = 150,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SMOV",
			Code = 145,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SADD",
			Code = 146,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SRGHT",
			Code = 57868,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SLEFT",
			Code = 57869,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SMID",
			Code = 57870,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SRPLC",
			Code = 57871,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SINS",
			Code = 57873,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SDEL",
			Code = 57872,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "STRIM",
			Code = 57900,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SFIND",
			Code = 57874,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SFINDN",
			Code = 57901,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SCMP",
			Code = 147,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DISS",
			Code = 148,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "UNIS",
			Code = 149,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RCOM",
			Code = 151,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CPSASC",
			Code = 58198,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RCPSASC",
			Code = 58199,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALADD",
			Code = 57879,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALSUB",
			Code = 57880,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALMUL",
			Code = 57881,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALDIV",
			Code = 57882,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALAND",
			Code = 57883,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALOR",
			Code = 57884,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALXOR",
			Code = 57885,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALNOT",
			Code = 57886,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALSHL",
			Code = 57888,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CALSHR",
			Code = 57887,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "HKEY",
			Code = 152,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SEG",
			Code = 58112,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BCNT",
			Code = 153,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DCNT",
			Code = 154,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SER",
			Code = 155,
			Shape = KVSUnitShape.InputCalc
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DSER",
			Code = 58163,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MAX",
			Code = 156,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MIN",
			Code = 157,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "AVG",
			Code = 158,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "WSUM",
			Code = 58155,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BSUM",
			Code = 58155,
			Shape = KVSUnitShape.InputIn
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CRC",
			Code = 58114,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ZRES",
			Code = 159,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "EXT",
			Code = 162,
			Shape = KVSUnitShape.InputCalc
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BCMP",
			Code = 58115,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "BCMPI",
			Code = 58116,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SORT",
			Code = 58188,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SORTN",
			Code = 58189,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FIFOW",
			Code = 160,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FIFOR",
			Code = 161,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LIFOW",
			Code = 58147,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LIFOR",
			Code = 58117,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FWRIT",
			Code = 58118,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FINS",
			Code = 58119,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FDEL",
			Code = 58120,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16,
			IsFollowSuffix = true
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "WTIME",
			Code = 164,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SEC",
			Code = 165,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RSEC",
			Code = 166,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "AJST",
			Code = 167,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDWK",
			Code = 58121,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDWKB",
			Code = 58122,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDCAL",
			Code = 58127,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LDCALB",
			Code = 58128,
			Shape = KVSUnitShape.Input
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ARES",
			Code = 168,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "HSP",
			Code = 170,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "EI",
			Code = 172,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DI",
			Code = 171,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "DIC",
			Code = 58133,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "INT",
			Code = 173,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RETI",
			Code = 174,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CTH",
			Code = 175,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CTC",
			Code = 176,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSCTH",
			Code = 58156,
			Shape = KVSUnitShape.OutputRect
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PLSX",
			Code = 177,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PLSY",
			Code = 178,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "JOGX",
			Code = 179,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "JOGY",
			Code = 180,
			Shape = KVSUnitShape.OutputRect,
			Height = 3
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ORGX",
			Code = 181,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ORGY",
			Code = 182,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TCHX",
			Code = 183,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TCHY",
			Code = 184,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CHGSPX",
			Code = 58145,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CHGSPY",
			Code = 58146,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSPSX",
			Code = 58157,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSPSY",
			Code = 58158,
			Shape = KVSUnitShape.Output
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PSTRT",
			Code = 58207,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "JOG",
			Code = 58208,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ORG",
			Code = 58209,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TCH",
			Code = 58210,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "HOME",
			Code = 58211,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CHGSPX",
			Code = 58145,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "CHGTGT",
			Code = 58213,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSPS",
			Code = 58214,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MCMP",
			Code = 58148,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "ABSENC",
			Code = 58136,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "INCENC",
			Code = 58137,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "FCNT",
			Code = 58152,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RCNT",
			Code = 58153,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PLSOUT",
			Code = 58154,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PID",
			Code = 58138,
			Shape = KVSUnitShape.OutputRect,
			Height = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "PIDAT",
			Code = 58194,
			Shape = KVSUnitShape.OutputRect,
			Height = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LOGE",
			Code = 58140,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "LOGD",
			Code = 58141,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "TRGD",
			Code = 58162,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.INT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MWRIT",
			Code = 58142,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MREAD",
			Code = 58143,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MREAD",
			Code = 58143,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MMKDIR",
			Code = 58149,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MRMDIR",
			Code = 58160,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MDEL",
			Code = 58161,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MPRINT",
			Code = 58181,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MREADL",
			Code = 58182,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MCOPY",
			Code = 58183,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MMOV",
			Code = 58184,
			Shape = KVSUnitShape.OutputRect,
			Width = 4
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MREN",
			Code = 58185,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MFREEK",
			Code = 58186,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT32
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "MSTAT",
			Code = 58187,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.STRING
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.BOOL
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "AWNUM",
			Code = 58150,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "AWMSG",
			Code = 58151,
			Shape = KVSUnitShape.Output
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSFRC",
			Code = 58159,
			Shape = KVSUnitShape.OutputRect
		};
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SPRD",
			Code = 58202,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SPWR",
			Code = 58203,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "SSVC",
			Code = 58204,
			Shape = KVSUnitShape.OutputRect,
			Width = 3
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSCI",
			Code = 58205,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		kVSUnitFormat = new KVSUnitFormat
		{
			Name = "RFSCO",
			Code = 58206,
			Shape = KVSUnitShape.OutputRect,
			Width = 2
		};
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		kVSUnitFormat.Args.Add(new KVSArgFormat
		{
			ValueType = KVSValueType.UINT16
		});
		Formats.Add(kVSUnitFormat);
		foreach (KVSUnitFormat format in Formats)
		{
			FormatOfCodes[format.Code] = format;
		}
	}

	public KVSTranslator(string _filepath)
	{
		filepath = _filepath;
	}

	public void Dispose()
	{
	}

	public KVSProject Translate()
	{
		string directoryName = Path.GetDirectoryName(filepath);
		KVSProject kVSProject = new KVSProject();
		KVSReport kVSReport = null;
		KVSReport kVSReport2 = null;
		KVSReport kVSReport3 = null;
		List<KVSReport> list = new List<KVSReport>();
		List<KVSReport> list2 = new List<KVSReport>();
		string[] files = Directory.GetFiles(directoryName);
		foreach (string text in files)
		{
			if (text.EndsWith("kpr"))
			{
				kVSReport2 = Load(text);
			}
			else if (text.EndsWith("cm1"))
			{
				kVSReport3 = Load(text);
			}
			else if (text.EndsWith("mod"))
			{
				kVSReport = Load(text);
				list.Add(kVSReport);
			}
			else if (text.EndsWith("mcr"))
			{
				kVSReport = Load(text);
				list2.Add(kVSReport);
			}
		}
		if (kVSReport2 != null)
		{
			kVSProject.Setup(kVSReport2);
		}
		if (kVSReport3 != null)
		{
			kVSProject.Setup(kVSReport3);
		}
		foreach (KVSReport item in list)
		{
			kVSProject.SetupMod(item);
		}
		foreach (KVSReport item2 in list2)
		{
			kVSProject.SetupMcr(item2);
		}
		return kVSProject;
	}

	public KVSReport Load(string fp)
	{
		FileStream fileStream = null;
		StreamWriter streamWriter = null;
		KVSFileStream kVSFileStream = null;
		KVSLadder kVSLadder = null;
		IntPtr intPtr = IntPtr.Zero;
		KVSReport kVSReport = new KVSReport();
		KVSValueCommentList kVSValueCommentList = null;
		KVSUnknownList kVSUnknownList = null;
		ushort num = 0;
		string directoryName = Path.GetDirectoryName(fp);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fp);
		string extension = Path.GetExtension(fp);
		string path = Path.Combine(directoryName, $"{fileNameWithoutExtension}_{extension}_Decrypt.bin");
		string path2 = Path.Combine(directoryName, $"{fileNameWithoutExtension}_{extension}_Analyze.txt");
		try
		{
			fileStream = File.Open(fp, FileMode.Open);
			kVSFileStream = new KVSFileStream(fileStream);
		}
		catch (Exception)
		{
			kVSFileStream?.Dispose();
			kVSFileStream = null;
			throw;
		}
		finally
		{
			fileStream?.Close();
			fileStream = null;
		}
		try
		{
			kVSFileStream.Index = 32;
			if (!kVSFileStream.Equal(ZERO_ZONE))
			{
				intPtr = Marshal.AllocHGlobal(kVSFileStream.Length * 4);
				Marshal.Copy(kVSFileStream.Data, 0, intPtr, kVSFileStream.Length);
				int num2 = Kv2PxKdn(intPtr + 20, kVSFileStream.Length - 20, intPtr, kVSFileStream.Length - 40);
				Marshal.Copy(intPtr, kVSFileStream.Data, 0, kVSFileStream.Length);
			}
			if (!kVSFileStream.Equal(ZERO_ZONE))
			{
				kVSFileStream?.Dispose();
				kVSFileStream = null;
				return null;
			}
		}
		catch (Exception)
		{
			kVSFileStream?.Dispose();
			kVSFileStream = null;
			throw;
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			intPtr = IntPtr.Zero;
		}
		try
		{
			fileStream = File.Open(path, FileMode.Create, FileAccess.Write);
			fileStream.Write(kVSFileStream.Data, 0, kVSFileStream.Length);
		}
		catch (Exception)
		{
		}
		finally
		{
			fileStream?.Close();
			fileStream = null;
		}
		try
		{
			kVSFileStream.Index = 16;
			num = kVSFileStream.ReadUShort();
			while (!kVSFileStream.EndOfStream)
			{
				bool flag = false;
				switch (num)
				{
				case 1:
					flag = true;
					kVSReport.ProInfo = new KVSProInfoList(kVSFileStream);
					break;
				case 2:
					flag = true;
					kVSReport.ModInfo = new KVSModInfoList(kVSFileStream);
					break;
				case 4:
					flag = true;
					kVSReport.BaseInfo = new KVSBase(kVSFileStream);
					break;
				case 5:
					flag = true;
					kVSReport.VList = new KVSVarList(kVSFileStream);
					break;
				case 7:
					flag = true;
					kVSLadder = (kVSReport.Ladder = new KVSLadder(kVSFileStream));
					break;
				case 11:
					flag = true;
					kVSReport.EAList = new KVSExArgsList(kVSFileStream);
					break;
				case 12:
					flag = true;
					kVSReport.SList = new KVSStringList(kVSFileStream);
					break;
				case 8:
					flag = true;
					kVSReport.LCList = new KVSLineCommentList(kVSFileStream);
					break;
				case 9:
					flag = true;
					kVSReport.Scripts = new KVSScriptList(kVSFileStream);
					break;
				case 29:
					flag = true;
					kVSReport.GLList = new KVSGlobalLabelList(kVSFileStream);
					break;
				case 30:
					flag = true;
					kVSReport.GLCList = new KVSGlobalLabelCommentList(kVSFileStream);
					break;
				case 31:
					flag = true;
					kVSReport.LLList = new KVSLocalLabelList(kVSFileStream);
					break;
				case 32:
					flag = true;
					kVSReport.LLCList = new KVSLocalLabelCommentList(kVSFileStream);
					break;
				case 160:
				case 161:
				case 162:
				case 163:
				case 164:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
				case 170:
				case 171:
				case 172:
				case 174:
				case 175:
					flag = true;
					kVSValueCommentList = new KVSValueCommentList(kVSFileStream)
					{
						Code = num
					};
					kVSReport.VCLists.Add(kVSValueCommentList);
					break;
				default:
					kVSUnknownList = new KVSUnknownList(kVSFileStream)
					{
						Code = num
					};
					if (kVSUnknownList.Data != null)
					{
						flag = true;
						kVSReport.UKLists.Add(kVSUnknownList);
					}
					break;
				}
				if (!flag)
				{
					break;
				}
				num = kVSFileStream.ReadUShort();
			}
		}
		catch (Exception)
		{
			return kVSReport;
		}
		finally
		{
			kVSFileStream?.Dispose();
			kVSFileStream = null;
		}
		try
		{
			if (kVSLadder != null)
			{
				fileStream = File.Open(path2, FileMode.Create, FileAccess.Write);
				streamWriter = new StreamWriter(fileStream);
				for (int i = 0; i < kVSLadder.RowCount; i++)
				{
					KVSLadderLine kVSLadderLine = kVSLadder.Lines[i];
					StringBuilder stringBuilder = new StringBuilder();
					streamWriter.WriteLine("===== Y={0} Data={1} EAID={2} =====", i, stringBuilder.ToString(), kVSLadderLine.EAID);
				}
				for (int j = 0; j < kVSLadder.RowCount; j++)
				{
					KVSLadderLine kVSLadderLine2 = kVSLadder.Lines[j];
					StringBuilder stringBuilder2 = new StringBuilder();
					streamWriter.WriteLine("===== Y={0} Data={1} EAID={2} =====", j, stringBuilder2.ToString(), kVSLadderLine2.EAID);
					for (int k = 0; k < kVSLadder.ColCount; k++)
					{
						KVSUnit kVSUnit = kVSLadder.Children[k, j];
						KVSUnit kVSUnit2 = kVSLadder.VLines[k, j];
						if (kVSUnit != null)
						{
							streamWriter.Write("Y={0} X={1} ", j, k);
							streamWriter.Write("Code=0x{0:X4} Row=0x{1:X2} Flag=0x{2:X2} V={3}", kVSUnit.Code, kVSUnit.Row, kVSUnit.Flag, kVSUnit2 != null);
							KVSArg[] oArgs = kVSUnit.OArgs;
							foreach (KVSArg arg in oArgs)
							{
								streamWriter.Write($" {arg}");
							}
							streamWriter.WriteLine();
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			streamWriter?.Close();
			fileStream?.Close();
			streamWriter = null;
			fileStream = null;
		}
		return kVSReport;
	}
}
