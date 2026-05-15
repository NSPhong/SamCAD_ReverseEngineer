using System.Linq;

namespace SamSoarII.Device;

public class PLCType
{
	public static readonly PLCType FGs_16MR_A = new PLCType(EnumPLCType.FGs_16MR_A, EnumSeries.PLC);

	public static readonly PLCType FGs_16MT_A = new PLCType(EnumPLCType.FGs_16MT_A, EnumSeries.PLC);

	public static readonly PLCType FGs_32MR_A = new PLCType(EnumPLCType.FGs_32MR_A, EnumSeries.PLC);

	public static readonly PLCType FGs_32MT_A = new PLCType(EnumPLCType.FGs_32MT_A, EnumSeries.PLC);

	public static readonly PLCType FGs_48MR_A = new PLCType(EnumPLCType.FGs_48MR_A, EnumSeries.PLC);

	public static readonly PLCType FGs_48MT_A = new PLCType(EnumPLCType.FGs_48MT_A, EnumSeries.PLC);

	public static readonly PLCType FGs_64MR_A = new PLCType(EnumPLCType.FGs_64MR_A, EnumSeries.PLC);

	public static readonly PLCType FGs_64MT_A = new PLCType(EnumPLCType.FGs_64MT_A, EnumSeries.PLC);

	public static readonly PLCType FGm_64MT_A = new PLCType(EnumPLCType.FGm_64MT_A, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X8T = new PLCType(EnumPLCType.FGRB_C8X8T, EnumSeries.PLC);

	public static readonly PLCType FGRS_C8X8T = new PLCType(EnumPLCType.FGRS_C8X8T, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X6R = new PLCType(EnumPLCType.FGRB_C8X6R, EnumSeries.PLC);

	public static readonly PLCType FGRE_C8X8T = new PLCType(EnumPLCType.FGRE_C8X8T, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X8T_V1 = new PLCType(EnumPLCType.FGRB_C8X8T_V1, EnumSeries.PLC);

	public static readonly PLCType FGRS_C8X8T_V1 = new PLCType(EnumPLCType.FGRS_C8X8T_V1, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X6R_V1 = new PLCType(EnumPLCType.FGRB_C8X6R_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_16MT_AC_V1 = new PLCType(EnumPLCType.FGs_16MT_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_16MR_AC_V1 = new PLCType(EnumPLCType.FGs_16MR_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_32MT_AC_V1 = new PLCType(EnumPLCType.FGs_32MT_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_32MR_AC_V1 = new PLCType(EnumPLCType.FGs_32MR_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_64MT_AC_V1 = new PLCType(EnumPLCType.FGs_64MT_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_64MR_AC_V1 = new PLCType(EnumPLCType.FGs_64MR_AC_V1, EnumSeries.PLC);

	public static readonly PLCType FGs_16MT_AC_V3 = new PLCType(EnumPLCType.FGs_16MT_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_16MR_AC_V3 = new PLCType(EnumPLCType.FGs_16MR_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_32MT_AC_V3 = new PLCType(EnumPLCType.FGs_32MT_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_32MR_AC_V3 = new PLCType(EnumPLCType.FGs_32MR_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_48MT_AC_V3 = new PLCType(EnumPLCType.FGs_48MT_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_48MR_AC_V3 = new PLCType(EnumPLCType.FGs_48MR_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_64MT_AC_V3 = new PLCType(EnumPLCType.FGs_64MT_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_64MR_AC_V3 = new PLCType(EnumPLCType.FGs_64MR_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGs_16MT_AC_V2_5 = new PLCType(EnumPLCType.FGs_16MT_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_16MR_AC_V2_5 = new PLCType(EnumPLCType.FGs_16MR_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_32MT_AC_V2_5 = new PLCType(EnumPLCType.FGs_32MT_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_32MR_AC_V2_5 = new PLCType(EnumPLCType.FGs_32MR_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_48MT_AC_V2_5 = new PLCType(EnumPLCType.FGs_48MT_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_48MR_AC_V2_5 = new PLCType(EnumPLCType.FGs_48MR_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_64MT_AC_V2_5 = new PLCType(EnumPLCType.FGs_64MT_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGs_64MR_AC_V2_5 = new PLCType(EnumPLCType.FGs_64MR_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGm_64MT_AC_V2_5 = new PLCType(EnumPLCType.FGm_64MT_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGm_64MT_AC_V3 = new PLCType(EnumPLCType.FGm_64MT_AC_V3, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X6R_AC_V2_5 = new PLCType(EnumPLCType.FGRB_C8X6R_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGRB_C8X8T_AC_V2_5 = new PLCType(EnumPLCType.FGRB_C8X8T_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGRS_C8X8T_AC_V2_5 = new PLCType(EnumPLCType.FGRS_C8X8T_AC_V2_5, EnumSeries.PLC);

	public static readonly PLCType FGRE_C8X8T_V2 = new PLCType(EnumPLCType.FGRE_C8X8T_V2, EnumSeries.PLC);

	public static readonly PLCType FAs_20MT_AC = new PLCType(EnumPLCType.FAs_20MT_AC, EnumSeries.PLC);

	public static readonly PLCType FAs_40MT_AC = new PLCType(EnumPLCType.FAs_40MT_AC, EnumSeries.PLC);

	public static readonly PLCType FAs_22MR_AC = new PLCType(EnumPLCType.FAs_20MR_AC, EnumSeries.PLC);

	public static readonly PLCType FAs_40MR_AC = new PLCType(EnumPLCType.FAs_40MR_AC, EnumSeries.PLC);

	public static readonly PLCType FGRE_C8X8T_V2_5 = new PLCType(EnumPLCType.FGRE_C8X8T_V2_5, EnumSeries.PLC);

	public static readonly PLCType GC043_16M4AI = new PLCType(EnumPLCType.GC043_16M4AI, EnumSeries.PLC_HMI);

	public static readonly PLCType GC050_32MAI = new PLCType(EnumPLCType.GC050_32MAI, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070_24MAI = new PLCType(EnumPLCType.GC070_24MAI, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070_24MAA = new PLCType(EnumPLCType.GC070_24MAA, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070_32MAA = new PLCType(EnumPLCType.GC070_32MAA, EnumSeries.PLC_HMI);

	public static readonly PLCType GC043S_16M4AA = new PLCType(EnumPLCType.GC043S_16M4AA, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070S_32MAA = new PLCType(EnumPLCType.GC070S_32MAA, EnumSeries.PLC_HMI);

	public static readonly PLCType GC043_16M4AI_V2_5 = new PLCType(EnumPLCType.GC043_16M4AI_V2_5, EnumSeries.PLC_HMI);

	public static readonly PLCType GC050_32MAI_V2_5 = new PLCType(EnumPLCType.GC050_32MAI_V2_5, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070_24MAA_V2_5 = new PLCType(EnumPLCType.GC070_24MAA_V2_5, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070_32MAA_V2_5 = new PLCType(EnumPLCType.GC070_32MAA_V2_5, EnumSeries.PLC_HMI);

	public static readonly PLCType GC070S_32MAA_V2_5 = new PLCType(EnumPLCType.GC070S_32MAA_V2_5, EnumSeries.PLC_HMI);

	public static readonly PLCType[] List = new PLCType[18]
	{
		FGs_16MR_A, FGs_16MT_A, FGs_32MR_A, FGs_32MT_A, FGs_48MR_A, FGs_48MT_A, FGs_64MR_A, FGs_64MT_A, FGm_64MT_A, FGRB_C8X8T,
		FGRS_C8X8T, FGRB_C8X6R, FGRE_C8X8T, FGRE_C8X8T_V2, FAs_20MT_AC, FAs_40MT_AC, FAs_22MR_AC, FAs_40MR_AC
	};

	public static readonly PLCType[] List_HMI = new PLCType[12]
	{
		GC043_16M4AI, GC050_32MAI, GC070_24MAI, GC070_32MAA, GC043S_16M4AA, GC070S_32MAA, GC070_24MAA, GC043_16M4AI_V2_5, GC050_32MAI_V2_5, GC070_24MAA_V2_5,
		GC070_32MAA_V2_5, GC070S_32MAA_V2_5
	};

	private EnumPLCType type;

	private EnumSeries series;

	private string name;

	public EnumPLCType Type => type;

	public EnumSeries Series => series;

	public string Name
	{
		get
		{
			return name ?? type.ToString();
		}
		set
		{
			name = value;
		}
	}

	public static PLCType Create(EnumPLCType _type)
	{
		PLCType pLCType = List.FirstOrDefault((PLCType _ret) => _ret.Type == _type);
		if (pLCType != null)
		{
			return pLCType;
		}
		pLCType = List_HMI.FirstOrDefault((PLCType _ret) => _ret.Type == _type);
		if (pLCType != null)
		{
			return pLCType;
		}
		if (_type == EnumPLCType.FGs_16MT_AC_V1)
		{
			return FGs_16MT_AC_V1;
		}
		if (_type == EnumPLCType.FGs_32MT_AC_V1)
		{
			return FGs_32MT_AC_V1;
		}
		if (_type == EnumPLCType.FGs_64MT_AC_V1)
		{
			return FGs_64MT_AC_V1;
		}
		if (_type == EnumPLCType.FGs_16MR_AC_V1)
		{
			return FGs_16MR_AC_V1;
		}
		if (_type == EnumPLCType.FGs_32MR_AC_V1)
		{
			return FGs_32MR_AC_V1;
		}
		if (_type == EnumPLCType.FGs_64MR_AC_V1)
		{
			return FGs_64MR_AC_V1;
		}
		if (_type == EnumPLCType.FGs_16MT_AC_V3)
		{
			return FGs_16MT_AC_V3;
		}
		if (_type == EnumPLCType.FGs_32MT_AC_V3)
		{
			return FGs_32MT_AC_V3;
		}
		if (_type == EnumPLCType.FGs_48MT_AC_V3)
		{
			return FGs_48MT_AC_V3;
		}
		if (_type == EnumPLCType.FGs_64MT_AC_V3)
		{
			return FGs_64MT_AC_V3;
		}
		if (_type == EnumPLCType.FGs_16MR_AC_V3)
		{
			return FGs_16MR_AC_V3;
		}
		if (_type == EnumPLCType.FGs_32MR_AC_V3)
		{
			return FGs_32MR_AC_V3;
		}
		if (_type == EnumPLCType.FGs_48MR_AC_V3)
		{
			return FGs_48MR_AC_V3;
		}
		if (_type == EnumPLCType.FGs_64MR_AC_V3)
		{
			return FGs_64MR_AC_V3;
		}
		if (_type == EnumPLCType.FGs_16MT_AC_V2_5)
		{
			return FGs_16MT_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_32MT_AC_V2_5)
		{
			return FGs_32MT_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_48MT_AC_V2_5)
		{
			return FGs_48MT_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_64MT_AC_V2_5)
		{
			return FGs_64MT_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_16MR_AC_V2_5)
		{
			return FGs_16MR_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_32MR_AC_V2_5)
		{
			return FGs_32MR_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_48MR_AC_V2_5)
		{
			return FGs_48MR_AC_V2_5;
		}
		if (_type == EnumPLCType.FGs_64MR_AC_V2_5)
		{
			return FGs_64MR_AC_V2_5;
		}
		if (_type == EnumPLCType.FGm_64MT_AC_V2_5)
		{
			return FGm_64MT_AC_V2_5;
		}
		if (_type == EnumPLCType.FGm_64MT_AC_V3)
		{
			return FGm_64MT_AC_V3;
		}
		if (_type == EnumPLCType.FGRB_C8X6R_AC_V2_5)
		{
			return FGRB_C8X6R_AC_V2_5;
		}
		if (_type == EnumPLCType.FGRB_C8X8T_AC_V2_5)
		{
			return FGRB_C8X8T_AC_V2_5;
		}
		if (_type == EnumPLCType.FGRS_C8X8T_AC_V2_5)
		{
			return FGRS_C8X8T_AC_V2_5;
		}
		if (_type == EnumPLCType.FGRB_C8X8T_V1)
		{
			return FGRB_C8X8T_V1;
		}
		if (_type == EnumPLCType.FGRS_C8X8T_V1)
		{
			return FGRS_C8X8T_V1;
		}
		if (_type == EnumPLCType.FGRB_C8X6R_V1)
		{
			return FGRB_C8X6R_V1;
		}
		if (_type == EnumPLCType.FGRE_C8X8T_V2_5)
		{
			return FGRE_C8X8T_V2_5;
		}
		if (_type == EnumPLCType.GC043_16M4AI_V2_5)
		{
			return GC043_16M4AI_V2_5;
		}
		if (_type == EnumPLCType.GC050_32MAI_V2_5)
		{
			return GC050_32MAI_V2_5;
		}
		if (_type == EnumPLCType.GC070_24MAA_V2_5)
		{
			return GC070_24MAA_V2_5;
		}
		if (_type == EnumPLCType.GC070_32MAA_V2_5)
		{
			return GC070_32MAA_V2_5;
		}
		return null;
	}

	public PLCType(EnumPLCType _type, EnumSeries _series)
	{
		type = _type;
		series = _series;
		name = null;
	}

	public override string ToString()
	{
		return Name;
	}
}
