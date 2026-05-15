using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXTranslator : IDisposable
{
	private static readonly byte[] Head_MonitorWindow;

	private static readonly byte[] Head_Option;

	private static readonly GXValueFormat VF_M;

	private static readonly GXValueFormat VF_S;

	private static readonly GXValueFormat VF_X;

	private static readonly GXValueFormat VF_Y;

	private static readonly GXValueFormat VF_D;

	private static readonly GXValueFormat VF_R;

	private static readonly GXValueFormat VF_T;

	private static readonly GXValueFormat VF_C;

	private static readonly GXValueFormat VF_Z;

	private static readonly GXValueFormat VF_V;

	private static readonly GXValueFormat VF_TV;

	private static readonly GXValueFormat VF_CV;

	private static readonly GXValueFormat VF_K16;

	private static readonly GXValueFormat VF_K32;

	private static readonly GXValueFormat VF_H16;

	private static readonly GXValueFormat VF_H32;

	private static readonly GXValueFormat VF_E;

	private static readonly GXValueFormat VF_STR;

	private static readonly GXValueFormat VF_P;

	private static readonly GXValueFormat VF_N;

	private static readonly GXValueFormat VF_Com_M;

	private static readonly GXValueFormat VF_Com_S;

	private static readonly GXValueFormat VF_Com_X;

	private static readonly GXValueFormat VF_Com_Y;

	private static readonly GXValueFormat VF_Com_T;

	private static readonly GXValueFormat VF_Com_C;

	private static readonly GXValueFormat VF_Dot_D;

	private static readonly GXValueFormat VF_Dot_Z;

	private static readonly GXValueFormat VF_Dot_V;

	private static readonly GXInitializeFormat IIF_D;

	private static readonly GXInitializeFormat IIF_M;

	private static readonly GXInitializeFormat IIF_S;

	private static readonly GXInitializeFormat IIF_T;

	private static readonly GXInitializeFormat IIF_C;

	private static readonly GXInitializeFormat IIF_X;

	private static readonly GXInitializeFormat IIF_Y;

	private static readonly GXValueFormat[] VF_Alls;

	private static readonly GXInitializeFormat[] IIF_Alls;

	private static readonly List<GXValueFormat>[] VF_ByHexs;

	private static readonly GXInitializeFormat[] IIF_ByHexs;

	private static readonly GXArgFormat AF_X;

	private static readonly GXArgFormat AF_Y;

	private static readonly GXArgFormat AF_S;

	private static readonly GXArgFormat AF_M;

	private static readonly GXArgFormat AF_D16;

	private static readonly GXArgFormat AF_D32;

	private static readonly GXArgFormat AF_LD;

	private static readonly GXArgFormat AF_OUT;

	private static readonly GXArgFormat AF_LDW;

	private static readonly GXArgFormat AF_LDD;

	private static readonly GXArgFormat AF_LDF;

	private static readonly GXArgFormat AF_LDS;

	private static readonly GXArgFormat AF_SRW;

	private static readonly GXArgFormat AF_SRD;

	private static readonly GXArgFormat AF_SRF;

	private static readonly GXArgFormat AF_SRS;

	private static readonly GXArgFormat AF_K16;

	private static readonly GXArgFormat AF_K32;

	private static readonly GXArgFormat AF_C;

	private static readonly GXArgFormat AF_CV;

	private static readonly GXArgFormat AF_CV32;

	private static readonly GXArgFormat AF_T;

	private static readonly GXArgFormat AF_TV;

	private static readonly GXArgFormat AF_ALL;

	private static readonly GXArgFormat AF_ALLB;

	private static readonly GXArgFormat AF_ALLW;

	private static readonly GXArgFormat AF_ALLD;

	private static readonly GXArgFormat AF_ALLF;

	private static readonly GXArgFormat AF_P;

	private static readonly GXArgFormat AF_N;

	private static readonly GXArgFormat AF_R;

	private static readonly GXUnitFormat UF_LD;

	private static readonly GXUnitFormat UF_MEP;

	private static readonly GXUnitFormat UF_MEF;

	private static readonly GXUnitFormat UF_INV;

	private static readonly GXUnitFormat UF_ANDB;

	private static readonly GXUnitFormat UF_ORB;

	private static readonly GXUnitFormat UF_MPS;

	private static readonly GXUnitFormat UF_MRD;

	private static readonly GXUnitFormat UF_MPP;

	private static readonly GXUnitFormat UF_STL;

	private static readonly GXUnitFormat UF_RET;

	private static readonly GXUnitFormat UF_SRET;

	private static readonly GXUnitFormat UF_FEND;

	private static readonly GXUnitFormat UF_END;

	private static readonly GXUnitFormat UF_CJ;

	private static readonly GXUnitFormat UF_CALL;

	private static readonly GXUnitFormat UF_LBL;

	private static readonly GXUnitFormat UF_MC;

	private static readonly GXUnitFormat UF_MCR;

	private static readonly GXUnitFormat UF_LineComment;

	private static readonly GXUnitFormat UF_OutputComment;

	private static readonly GXInstFormat IF_LineComment;

	private static readonly GXInstFormat IF_OutputComment;

	private static readonly GXInstFormat IF_LD;

	private static readonly GXInstFormat IF_LDI;

	private static readonly GXInstFormat IF_LD_2;

	private static readonly GXInstFormat IF_LDI_2;

	private static readonly GXInstFormat IF_LDP;

	private static readonly GXInstFormat IF_LDF;

	private static readonly GXInstFormat IF_OR;

	private static readonly GXInstFormat IF_OR_2;

	private static readonly GXInstFormat IF_ORI;

	private static readonly GXInstFormat IF_ORI_2;

	private static readonly GXInstFormat IF_ORP;

	private static readonly GXInstFormat IF_ORF;

	private static readonly GXInstFormat IF_AND;

	private static readonly GXInstFormat IF_AND_2;

	private static readonly GXInstFormat IF_ANDI;

	private static readonly GXInstFormat IF_ANDI_2;

	private static readonly GXInstFormat IF_ANDP;

	private static readonly GXInstFormat IF_ANDF;

	private static readonly GXInstFormat IF_MEP;

	private static readonly GXInstFormat IF_MEF;

	private static readonly GXInstFormat IF_INV;

	private static readonly GXInstFormat IF_ORB;

	private static readonly GXInstFormat IF_ANDB;

	private static readonly GXInstFormat IF_MPS;

	private static readonly GXInstFormat IF_MRD;

	private static readonly GXInstFormat IF_MPP;

	private static readonly GXInstFormat IF_OUT;

	private static readonly GXInstFormat IF_SET;

	private static readonly GXInstFormat IF_RST;

	private static readonly GXInstFormat IF_PLS;

	private static readonly GXInstFormat IF_PLF;

	private static readonly GXInstFormat IF_ALT;

	private static readonly GXInstFormat IF_FEND;

	private static readonly GXInstFormat IF_END;

	private static readonly GXInstFormat IF_Spe_OUT;

	private static readonly GXInstFormat IF_Spe_SET;

	private static readonly GXInstFormat IF_Spe_RST;

	private static readonly GXInstFormat IF_Dot_LD;

	private static readonly GXInstFormat IF_Dot_LDI;

	private static readonly GXInstFormat IF_Dot_LDP;

	private static readonly GXInstFormat IF_Dot_LDF;

	private static readonly GXInstFormat IF_Dot_OR;

	private static readonly GXInstFormat IF_Dot_ORI;

	private static readonly GXInstFormat IF_Dot_ORP;

	private static readonly GXInstFormat IF_Dot_ORF;

	private static readonly GXInstFormat IF_Dot_AND;

	private static readonly GXInstFormat IF_Dot_ANDI;

	private static readonly GXInstFormat IF_Dot_ANDP;

	private static readonly GXInstFormat IF_Dot_ANDF;

	private static readonly GXInstFormat IF_Dot_OUT;

	private static readonly GXInstFormat IF_Dot_SET;

	private static readonly GXInstFormat IF_Dot_RST;

	private static readonly GXInstFormat IF_LDWEQ;

	private static readonly GXInstFormat IF_LDWNE;

	private static readonly GXInstFormat IF_LDWG;

	private static readonly GXInstFormat IF_LDWGE;

	private static readonly GXInstFormat IF_LDWL;

	private static readonly GXInstFormat IF_LDWLE;

	private static readonly GXInstFormat IF_AWEQ;

	private static readonly GXInstFormat IF_AWNE;

	private static readonly GXInstFormat IF_AWG;

	private static readonly GXInstFormat IF_AWGE;

	private static readonly GXInstFormat IF_AWL;

	private static readonly GXInstFormat IF_AWLE;

	private static readonly GXInstFormat IF_ORWEQ;

	private static readonly GXInstFormat IF_ORWNE;

	private static readonly GXInstFormat IF_ORWG;

	private static readonly GXInstFormat IF_ORWGE;

	private static readonly GXInstFormat IF_ORWL;

	private static readonly GXInstFormat IF_ORWLE;

	private static readonly GXInstFormat IF_LDDEQ;

	private static readonly GXInstFormat IF_LDDNE;

	private static readonly GXInstFormat IF_LDDG;

	private static readonly GXInstFormat IF_LDDGE;

	private static readonly GXInstFormat IF_LDDL;

	private static readonly GXInstFormat IF_LDDLE;

	private static readonly GXInstFormat IF_ADEQ;

	private static readonly GXInstFormat IF_ADNE;

	private static readonly GXInstFormat IF_ADG;

	private static readonly GXInstFormat IF_ADGE;

	private static readonly GXInstFormat IF_ADL;

	private static readonly GXInstFormat IF_ADLE;

	private static readonly GXInstFormat IF_ORDEQ;

	private static readonly GXInstFormat IF_ORDNE;

	private static readonly GXInstFormat IF_ORDG;

	private static readonly GXInstFormat IF_ORDGE;

	private static readonly GXInstFormat IF_ORDL;

	private static readonly GXInstFormat IF_ORDLE;

	private static readonly GXInstFormat IF_CMP;

	private static readonly GXInstFormat IF_CMPD;

	private static readonly GXInstFormat IF_CMPF;

	private static readonly GXInstFormat IF_ZCP;

	private static readonly GXInstFormat IF_ZCPD;

	private static readonly GXInstFormat IF_ZCPF;

	private static readonly GXInstFormat IF_ADD;

	private static readonly GXInstFormat IF_SUB;

	private static readonly GXInstFormat IF_MUL;

	private static readonly GXInstFormat IF_DIV;

	private static readonly GXInstFormat IF_INC;

	private static readonly GXInstFormat IF_DEC;

	private static readonly GXInstFormat IF_SQR;

	private static readonly GXInstFormat IF_ADDD;

	private static readonly GXInstFormat IF_SUBD;

	private static readonly GXInstFormat IF_MULD;

	private static readonly GXInstFormat IF_DIVD;

	private static readonly GXInstFormat IF_DINC;

	private static readonly GXInstFormat IF_DDEC;

	private static readonly GXInstFormat IF_ADDF;

	private static readonly GXInstFormat IF_SUBF;

	private static readonly GXInstFormat IF_MULF;

	private static readonly GXInstFormat IF_DIVF;

	private static readonly GXInstFormat IF_MOV;

	private static readonly GXInstFormat IF_XCH;

	private static readonly GXInstFormat IF_BCD;

	private static readonly GXInstFormat IF_BIN;

	private static readonly GXInstFormat IF_CML;

	private static readonly GXInstFormat IF_FMOV;

	private static readonly GXInstFormat IF_DMOV;

	private static readonly GXInstFormat IF_XCHD;

	private static readonly GXInstFormat IF_DBCD;

	private static readonly GXInstFormat IF_DBIN;

	private static readonly GXInstFormat IF_DCML;

	private static readonly GXInstFormat IF_DFMOV;

	private static readonly GXInstFormat IF_BMOV;

	private static readonly GXInstFormat IF_SMOV;

	private static readonly GXInstFormat IF_ROR;

	private static readonly GXInstFormat IF_RCR;

	private static readonly GXInstFormat IF_ROL;

	private static readonly GXInstFormat IF_RCL;

	private static readonly GXInstFormat IF_DROR;

	private static readonly GXInstFormat IF_DRCR;

	private static readonly GXInstFormat IF_DROL;

	private static readonly GXInstFormat IF_DRCL;

	private static readonly GXInstFormat IF_SFR;

	private static readonly GXInstFormat IF_SFL;

	private static readonly GXInstFormat IF_SFTR;

	private static readonly GXInstFormat IF_SFTL;

	private static readonly GXInstFormat IF_WSTR;

	private static readonly GXInstFormat IF_WSTL;

	private static readonly GXInstFormat IF_SFRD;

	private static readonly GXInstFormat IF_SFWR;

	private static readonly GXInstFormat IF_INT;

	private static readonly GXInstFormat IF_DINT;

	private static readonly GXInstFormat IF_GRY;

	private static readonly GXInstFormat IF_DGRY;

	private static readonly GXInstFormat IF_GBIN;

	private static readonly GXInstFormat IF_DGBIN;

	private static readonly GXInstFormat IF_NEG;

	private static readonly GXInstFormat IF_NEGD;

	private static readonly GXInstFormat IF_NEGF;

	private static readonly GXInstFormat IF_TOUT;

	private static readonly GXInstFormat IF_COUT;

	private static readonly GXInstFormat IF_SWAP;

	private static readonly GXInstFormat IF_DSWAP;

	private static readonly GXInstFormat IF_SER;

	private static readonly GXInstFormat IF_DSER;

	private static readonly GXInstFormat IF_DECO;

	private static readonly GXInstFormat IF_ENCO;

	private static readonly GXInstFormat IF_SORT;

	private static readonly GXInstFormat IF_ZRST;

	private static readonly GXInstFormat IF_SUM;

	private static readonly GXInstFormat IF_DSUM;

	private static readonly GXInstFormat IF_BON;

	private static readonly GXInstFormat IF_DBON;

	private static readonly GXInstFormat IF_MEAN;

	private static readonly GXInstFormat IF_FLT;

	private static readonly GXInstFormat IF_DFLT;

	private static readonly GXInstFormat IF_FOR;

	private static readonly GXInstFormat IF_NEXT;

	private static readonly GXInstFormat IF_SRET;

	private static readonly GXInstFormat IF_DSQR;

	private static readonly GXInstFormat IF_DESQR;

	private static readonly GXInstFormat IF_DSIN;

	private static readonly GXInstFormat IF_DCOS;

	private static readonly GXInstFormat IF_DTAN;

	private static readonly GXInstFormat IF_DEXP;

	private static readonly GXInstFormat IF_TCMP;

	private static readonly GXInstFormat IF_TZCP;

	private static readonly GXInstFormat IF_TADD;

	private static readonly GXInstFormat IF_TSUB;

	private static readonly GXInstFormat IF_TRD;

	private static readonly GXInstFormat IF_TWR;

	private static readonly GXInstFormat IF_ABSD;

	private static readonly GXInstFormat IF_ARWS;

	private static readonly GXInstFormat IF_DABS;

	private static readonly GXInstFormat IF_DABSD;

	private static readonly GXInstFormat IF_DEXTR;

	private static readonly GXInstFormat IF_DHKY;

	private static readonly GXInstFormat IF_DHOUR;

	private static readonly GXInstFormat IF_DHSCR;

	private static readonly GXInstFormat IF_DHSCS;

	private static readonly GXInstFormat IF_DHSZ;

	private static readonly GXInstFormat IF_DMEAN;

	private static readonly GXInstFormat IF_DPLSR;

	private static readonly GXInstFormat IF_DPLSY;

	private static readonly GXInstFormat IF_DPLSV;

	private static readonly GXInstFormat IF_DSW;

	private static readonly GXInstFormat IF_DTKY;

	private static readonly GXInstFormat IF_EXTR;

	private static readonly GXInstFormat IF_HKY;

	private static readonly GXInstFormat IF_HOUR;

	private static readonly GXInstFormat IF_INCD;

	private static readonly GXInstFormat IF_IST;

	private static readonly GXInstFormat IF_MTR;

	private static readonly GXInstFormat IF_PLSR;

	private static readonly GXInstFormat IF_PLSY;

	private static readonly GXInstFormat IF_PWM;

	private static readonly GXInstFormat IF_RAMP;

	private static readonly GXInstFormat IF_RD3A;

	private static readonly GXInstFormat IF_ROTC;

	private static readonly GXInstFormat IF_SEGD;

	private static readonly GXInstFormat IF_SEGL;

	private static readonly GXInstFormat IF_SPD;

	private static readonly GXInstFormat IF_STMR;

	private static readonly GXInstFormat IF_TKY;

	private static readonly GXInstFormat IF_TTMR;

	private static readonly GXInstFormat IF_WDT;

	private static readonly GXInstFormat IF_WR3A;

	private static readonly GXInstFormat IF_ADPRW;

	private static readonly GXInstFormat IF_CJ;

	private static readonly GXInstFormat IF_CALL;

	private static readonly GXInstFormat IF_LBL;

	private static readonly GXInstFormat IF_STL;

	private static readonly GXInstFormat IF_RET;

	private static readonly GXInstFormat IF_LIMIT;

	private static readonly GXInstFormat IF_DLIMIT;

	private static readonly GXInstFormat IF_BAND;

	private static readonly GXInstFormat IF_DBAND;

	private static readonly GXInstFormat IF_ZONE;

	private static readonly GXInstFormat IF_DZONE;

	private static readonly GXInstFormat IF_SCL;

	private static readonly GXInstFormat IF_DSCL;

	private static readonly GXInstFormat IF_SCL2;

	private static readonly GXInstFormat IF_DSCL2;

	private static readonly GXInstFormat IF_ANS;

	private static readonly GXInstFormat IF_ANR;

	private static readonly GXInstFormat IF_REFF;

	private static readonly GXInstFormat IF_REF;

	private static readonly GXInstFormat IF_BKADD;

	private static readonly GXInstFormat IF_BKSUB;

	private static readonly GXInstFormat IF_DBKADD;

	private static readonly GXInstFormat IF_DBKSUB;

	private static readonly GXInstFormat IF_BKCMP_EQUAL;

	private static readonly GXInstFormat IF_BKCMP_NOTEQUAL;

	private static readonly GXInstFormat IF_BKCMP_LESS;

	private static readonly GXInstFormat IF_BKCMP_MORE;

	private static readonly GXInstFormat IF_BKCMP_NOTMORE;

	private static readonly GXInstFormat IF_BKCMP_NOTLESS;

	private static readonly GXInstFormat IF_DBKCMP_EQUAL;

	private static readonly GXInstFormat IF_DBKCMP_NOTEQUAL;

	private static readonly GXInstFormat IF_DBKCMP_LESS;

	private static readonly GXInstFormat IF_DBKCMP_MORE;

	private static readonly GXInstFormat IF_DBKCMP_NOTMORE;

	private static readonly GXInstFormat IF_DBKCMP_NOTLESS;

	private static readonly GXInstFormat IF_FROM;

	private static readonly GXInstFormat IF_TO;

	private static readonly GXInstFormat IF_DFROM;

	private static readonly GXInstFormat IF_DTO;

	private static readonly GXInstFormat IF_RBFM;

	private static readonly GXInstFormat IF_WBFM;

	private static readonly GXInstFormat IF_PR;

	private static readonly GXInstFormat IF_STRADD;

	private static readonly GXInstFormat IF_STRMOV;

	private static readonly GXInstFormat IF_ASC;

	private static readonly GXInstFormat IF_BINDA;

	private static readonly GXInstFormat IF_INSTR;

	private static readonly GXInstFormat IF_LEFT;

	private static readonly GXInstFormat IF_RIGHT;

	private static readonly GXInstFormat IF_LEN;

	private static readonly GXInstFormat IF_MIDR;

	private static readonly GXInstFormat IF_MIDW;

	private static readonly GXInstFormat IF_STR;

	private static readonly GXInstFormat IF_VAL;

	private static readonly GXInstFormat IF_DABIN;

	private static readonly GXInstFormat IF_DBINDA;

	private static readonly GXInstFormat IF_DESTR;

	private static readonly GXInstFormat IF_DEVAL;

	private static readonly GXInstFormat IF_DSTR;

	private static readonly GXInstFormat IF_DVAL;

	private static readonly GXInstFormat IF_MC;

	private static readonly GXInstFormat IF_MCR;

	private static readonly GXInstFormat IF_DI;

	private static readonly GXInstFormat IF_EI;

	private static readonly GXInstFormat IF_WAND;

	private static readonly GXInstFormat IF_WOR;

	private static readonly GXInstFormat IF_WXOR;

	private static readonly GXInstFormat IF_RS;

	private static readonly GXInstFormat IF_PRUN;

	private static readonly GXInstFormat IF_ASCI;

	private static readonly GXInstFormat IF_HEX;

	private static readonly GXInstFormat IF_CCD;

	private static readonly GXInstFormat IF_RS2;

	private static readonly GXInstFormat IF_PID;

	private static readonly GXInstFormat IF_ZPUSH;

	private static readonly GXInstFormat IF_ZPOP;

	private static readonly GXInstFormat IF_DEMOV;

	private static readonly GXInstFormat IF_DEBCD;

	private static readonly GXInstFormat IF_DEBIN;

	private static readonly GXInstFormat IF_DLOGE;

	private static readonly GXInstFormat IF_DLOG10;

	private static readonly GXInstFormat IF_ASIN;

	private static readonly GXInstFormat IF_ACOS;

	private static readonly GXInstFormat IF_ATAN;

	private static readonly GXInstFormat IF_DPAD;

	private static readonly GXInstFormat IF_DDEG;

	private static readonly GXInstFormat IF_DIS;

	private static readonly GXInstFormat IF_UNI;

	private static readonly GXInstFormat IF_WSUM;

	private static readonly GXInstFormat IF_WTOB;

	private static readonly GXInstFormat IF_BTOW;

	private static readonly GXInstFormat IF_SORT2;

	private static readonly GXInstFormat IF_DSZR;

	private static readonly GXInstFormat IF_DVIT;

	private static readonly GXInstFormat IF_DTBL;

	private static readonly GXInstFormat IF_ZRN;

	private static readonly GXInstFormat IF_PLSV;

	private static readonly GXInstFormat IF_DRVI;

	private static readonly GXInstFormat IF_DRVA;

	private static readonly GXInstFormat IF_HTOS;

	private static readonly GXInstFormat IF_STOH;

	private static readonly GXInstFormat IF_COMRD;

	private static readonly GXInstFormat IF_RAD;

	private static readonly GXInstFormat IF_DUTY;

	private static readonly GXInstFormat IF_CRC;

	private static readonly GXInstFormat IF_HCMOV;

	private static readonly GXInstFormat IF_FDEL;

	private static readonly GXInstFormat IF_FINS;

	private static readonly GXInstFormat IF_POP;

	private static readonly GXInstFormat IF_IVCK;

	private static readonly GXInstFormat IF_IVDR;

	private static readonly GXInstFormat IF_IVRD;

	private static readonly GXInstFormat IF_IVWR;

	private static readonly GXInstFormat IF_IVBWR;

	private static readonly GXInstFormat IF_DHSCT;

	private static readonly GXInstFormat IF_LOADR;

	private static readonly GXInstFormat IF_SAVER;

	private static readonly GXInstFormat IF_INITR;

	private static readonly GXInstFormat IF_LOGR;

	private static readonly GXInstFormat IF_RWER;

	private static readonly GXInstFormat IF_INITER;

	private static readonly GXInstFormat IF_DDRVI;

	private static readonly GXInstFormat IF_DDRVA;

	private static readonly Dictionary<long, GXUnitFormat> Formats;

	private GXFileBinary bin;

	private Dictionary<string, string> sbrenames;

	static GXTranslator()
	{
		Head_MonitorWindow = new byte[26]
		{
			91, 0, 87, 0, 97, 0, 116, 0, 99, 0,
			104, 0, 87, 0, 105, 0, 110, 0, 100, 0,
			111, 0, 119, 0, 93, 0
		};
		Head_Option = new byte[16]
		{
			91, 0, 79, 0, 112, 0, 116, 0, 105, 0,
			111, 0, 110, 0, 93, 0
		};
		VF_M = new GXValueFormatM8224("M", new byte[1] { 144 });
		VF_S = new GXValueFormatB("S", new byte[1] { 152 });
		VF_X = new GXValueFormatXY("X", new byte[1] { 156 });
		VF_Y = new GXValueFormatXY("Y", new byte[1] { 157 });
		VF_D = new GXValueFormatD8234("D", new byte[1] { 168 });
		VF_R = new GXValueFormatD("R", new byte[1] { 175 });
		VF_T = new GXValueFormatB("T", new byte[1] { 194 });
		VF_C = new GXValueFormatB("C", new byte[1] { 197 });
		VF_Z = new GXValueFormatD("Z", new byte[1] { 204 });
		VF_V = new GXValueFormatD("V", new byte[1] { 205 });
		VF_TV = new GXValueFormatD("TV", new byte[1] { 194 });
		VF_CV = new GXValueFormatD("CV", new byte[1] { 197 });
		VF_K16 = new GXValueFormatD("K", new byte[1] { 232 });
		VF_K32 = new GXValueFormatD("K", new byte[1] { 233 });
		VF_H16 = new GXValueFormatH("H", new byte[1] { 234 });
		VF_H32 = new GXValueFormatH("H", new byte[1] { 235 });
		VF_E = new GXValueFormatF("K", new byte[1] { 236 });
		VF_STR = new GXValueFormatSTR("STR", new byte[1] { 238 });
		VF_P = new GXValueFormatD("K", new byte[1] { 208 });
		VF_N = new GXValueFormatD("K", new byte[1] { 210 });
		VF_Com_M = new GXValueFormatCom("M", new byte[1] { 144 });
		VF_Com_S = new GXValueFormatCom("M", new byte[1] { 152 });
		VF_Com_X = new GXValueFormatComXY("X", new byte[1] { 156 });
		VF_Com_Y = new GXValueFormatComXY("Y", new byte[1] { 157 });
		VF_Com_T = new GXValueFormatCom("T", new byte[1] { 194 });
		VF_Com_C = new GXValueFormatCom("C", new byte[1] { 197 });
		VF_Dot_D = new GXValueFormatDot("D", new byte[1] { 168 });
		VF_Dot_Z = new GXValueFormatDot("Z", new byte[1] { 204 });
		VF_Dot_V = new GXValueFormatDot("V", new byte[1] { 205 });
		IIF_D = new GXInitializeFormat(VF_D, 68u);
		IIF_M = new GXInitializeFormat(VF_M, 77u);
		IIF_S = new GXInitializeFormat(VF_S, 83u);
		IIF_T = new GXInitializeFormat(VF_TV, 84u);
		IIF_C = new GXInitializeFormat(VF_CV, 67u);
		IIF_X = new GXInitializeFormat(VF_X, 88u);
		IIF_Y = new GXInitializeFormat(VF_Y, 89u);
		VF_Alls = new GXValueFormat[22]
		{
			VF_M, VF_S, VF_X, VF_Y, VF_D, VF_T, VF_C, VF_V, VF_Z, VF_K16,
			VF_K32, VF_H16, VF_H32, VF_Com_M, VF_Com_S, VF_Com_T, VF_Com_C, VF_Com_X, VF_Com_Y, VF_Dot_D,
			VF_Dot_V, VF_Dot_Z
		};
		IIF_Alls = new GXInitializeFormat[7] { IIF_D, IIF_M, IIF_S, IIF_T, IIF_C, IIF_X, IIF_Y };
		VF_ByHexs = new List<GXValueFormat>[256];
		IIF_ByHexs = new GXInitializeFormat[256];
		AF_X = new GXArgFormatB("X", new GXValueFormat[1] { VF_X });
		AF_Y = new GXArgFormatB("X", new GXValueFormat[1] { VF_Y });
		AF_S = new GXArgFormatB("X", new GXValueFormat[1] { VF_S });
		AF_M = new GXArgFormatB("X", new GXValueFormat[1] { VF_M });
		AF_D16 = new GXArgFormat16("D", new GXValueFormat[1] { VF_D });
		AF_D32 = new GXArgFormat32("D", new GXValueFormat[1] { VF_D });
		AF_LD = new GXArgFormatB("IN", new GXValueFormat[9] { VF_X, VF_Y, VF_S, VF_M, VF_T, VF_C, VF_Dot_D, VF_Dot_V, VF_Dot_Z });
		AF_OUT = new GXArgFormatB("OUT", new GXValueFormat[8] { VF_Y, VF_S, VF_M, VF_T, VF_C, VF_Dot_D, VF_Dot_V, VF_Dot_Z });
		AF_LDW = new GXArgFormat16("IN", new GXValueFormat[12]
		{
			VF_Com_X, VF_Com_Y, VF_Com_M, VF_Com_S, VF_D, VF_V, VF_Z, VF_K16, VF_H16, VF_CV,
			VF_TV, VF_R
		});
		AF_LDD = new GXArgFormat32("IN", new GXValueFormat[12]
		{
			VF_Com_X, VF_Com_Y, VF_Com_M, VF_Com_S, VF_D, VF_V, VF_Z, VF_K32, VF_H32, VF_CV,
			VF_TV, VF_R
		});
		AF_LDF = new GXArgFormatF("IN", new GXValueFormat[2] { VF_D, VF_E });
		AF_LDS = new GXArgFormatSTR("IN", new GXValueFormat[2] { VF_D, VF_STR });
		AF_SRW = new GXArgFormat16("OUT", new GXValueFormat[9] { VF_Com_Y, VF_Com_M, VF_Com_S, VF_D, VF_V, VF_Z, VF_CV, VF_TV, VF_R });
		AF_SRD = new GXArgFormat32("OUT", new GXValueFormat[9] { VF_Com_Y, VF_Com_M, VF_Com_S, VF_D, VF_V, VF_Z, VF_CV, VF_TV, VF_R });
		AF_SRF = new GXArgFormatF("OUT", new GXValueFormat[1] { VF_D });
		AF_SRS = new GXArgFormatSTR("IN", new GXValueFormat[1] { VF_D });
		AF_K16 = new GXArgFormat16("IN", new GXValueFormat[2] { VF_K16, VF_H16 });
		AF_K32 = new GXArgFormat32("IN", new GXValueFormat[2] { VF_K32, VF_H32 });
		AF_C = new GXArgFormatB("C", new GXValueFormat[1] { VF_C });
		AF_CV = new GXArgFormat16("CV", new GXValueFormat[1] { VF_CV });
		AF_CV32 = new GXArgFormat32("CV", new GXValueFormat[1] { VF_CV });
		AF_T = new GXArgFormatB("T", new GXValueFormat[1] { VF_T });
		AF_TV = new GXArgFormat16("TV", new GXValueFormat[1] { VF_TV });
		AF_ALL = new GXArgFormat("ALL", VF_Alls);
		AF_ALLB = new GXArgFormatB("ALL", VF_Alls);
		AF_ALLW = new GXArgFormat16("ALL", VF_Alls);
		AF_ALLD = new GXArgFormat32("ALL", VF_Alls);
		AF_ALLF = new GXArgFormatF("ALL", VF_Alls);
		AF_P = new GXArgFormat("P", new GXValueFormat[1] { VF_P });
		AF_N = new GXArgFormat("N", new GXValueFormat[1] { VF_N });
		AF_R = new GXArgFormat("R", new GXValueFormat[1] { VF_R });
		IF_LineComment = new GXInstFormat(string.Empty, new byte[2] { 128, 0 });
		IF_OutputComment = new GXInstFormat(string.Empty, new byte[2] { 130, 0 });
		IF_LD = new GXInstFormat("LD", new byte[1]);
		IF_LDI = new GXInstFormat("LDI", new byte[1] { 1 });
		IF_LD_2 = new GXInstFormat("LD", new byte[1] { 2 });
		IF_LDI_2 = new GXInstFormat("LDI", new byte[2] { 1, 2 });
		IF_LDP = new GXInstFormat("LDP", new byte[2] { 2, 2 });
		IF_LDF = new GXInstFormat("LDF", new byte[2] { 3, 2 });
		IF_OR = new GXInstFormat("OR", new byte[1] { 6 });
		IF_OR_2 = new GXInstFormat("OR", new byte[2] { 6, 2 });
		IF_ORI = new GXInstFormat("ORI", new byte[1] { 7 });
		IF_ORI_2 = new GXInstFormat("ORI", new byte[2] { 7, 2 });
		IF_ORP = new GXInstFormat("ORP", new byte[2] { 8, 2 });
		IF_ORF = new GXInstFormat("ORF", new byte[2] { 9, 2 });
		IF_AND = new GXInstFormat("AND", new byte[1] { 12 });
		IF_AND_2 = new GXInstFormat("AND", new byte[2] { 12, 2 });
		IF_ANDI = new GXInstFormat("ANDI", new byte[1] { 13 });
		IF_ANDI_2 = new GXInstFormat("ANDI", new byte[2] { 13, 2 });
		IF_ANDP = new GXInstFormat("ANDP", new byte[2] { 14, 2 });
		IF_ANDF = new GXInstFormat("ANDF", new byte[2] { 15, 2 });
		IF_MEP = new GXInstFormat("MEP", new byte[1] { 18 });
		IF_MEF = new GXInstFormat("MEF", new byte[1] { 19 });
		IF_INV = new GXInstFormat("INV", new byte[1] { 20 });
		IF_ORB = new GXInstFormat("ORB", new byte[1] { 24 });
		IF_ANDB = new GXInstFormat("ANDB", new byte[1] { 25 });
		IF_MPS = new GXInstFormat("MPS", new byte[1] { 26 });
		IF_MRD = new GXInstFormat("MRD", new byte[1] { 27 });
		IF_MPP = new GXInstFormat("MPP", new byte[1] { 28 });
		IF_OUT = new GXInstFormat("OUT", new byte[1] { 32 });
		IF_SET = new GXInstFormat("SET", new byte[1] { 35 });
		IF_RST = new GXInstFormat("RST", new byte[1] { 36 });
		IF_PLS = new GXInstFormat("PLS", new byte[2] { 37, 2 });
		IF_PLF = new GXInstFormat("PLF", new byte[2] { 38, 2 });
		IF_ALT = new GXInstFormat("ALT", new byte[3] { 99, 3, 15 });
		IF_FEND = new GXInstFormat("RET", new byte[1] { 51 });
		IF_END = new GXInstFormat("END", new byte[1] { 52 });
		IF_Spe_OUT = new GXInstFormat("OUT", new byte[2] { 32, 2 });
		IF_Spe_SET = new GXInstFormat("SET", new byte[2] { 35, 2 });
		IF_Spe_RST = new GXInstFormat("RST", new byte[2] { 36, 2 });
		IF_Dot_LD = new GXInstFormat("LD", new byte[2] { 0, 3 });
		IF_Dot_LDI = new GXInstFormat("LDI", new byte[2] { 1, 3 });
		IF_Dot_LDP = new GXInstFormat("LDP", new byte[2] { 2, 3 });
		IF_Dot_LDF = new GXInstFormat("LDF", new byte[2] { 3, 3 });
		IF_Dot_OR = new GXInstFormat("OR", new byte[2] { 6, 3 });
		IF_Dot_ORI = new GXInstFormat("ORI", new byte[2] { 7, 3 });
		IF_Dot_ORP = new GXInstFormat("ORP", new byte[2] { 8, 3 });
		IF_Dot_ORF = new GXInstFormat("ORF", new byte[2] { 9, 3 });
		IF_Dot_AND = new GXInstFormat("AND", new byte[2] { 12, 3 });
		IF_Dot_ANDI = new GXInstFormat("ANDI", new byte[2] { 13, 3 });
		IF_Dot_ANDP = new GXInstFormat("ANDP", new byte[2] { 14, 3 });
		IF_Dot_ANDF = new GXInstFormat("ANDF", new byte[2] { 15, 3 });
		IF_Dot_OUT = new GXInstFormat("OUT", new byte[2] { 32, 3 });
		IF_Dot_SET = new GXInstFormat("SET", new byte[2] { 35, 3 });
		IF_Dot_RST = new GXInstFormat("RST", new byte[2] { 36, 3 });
		IF_LDWEQ = new GXInstFormat("LDW=", new byte[4] { 64, 5, 0, 16 });
		IF_LDWNE = new GXInstFormat("LDW<>", new byte[4] { 64, 5, 1, 16 });
		IF_LDWG = new GXInstFormat("LDW>", new byte[4] { 64, 5, 2, 16 });
		IF_LDWGE = new GXInstFormat("LDW>=", new byte[4] { 64, 5, 3, 16 });
		IF_LDWL = new GXInstFormat("LDW<", new byte[4] { 64, 5, 4, 16 });
		IF_LDWLE = new GXInstFormat("LDW<=", new byte[4] { 64, 5, 5, 16 });
		IF_AWEQ = new GXInstFormat("AW=", new byte[4] { 64, 5, 0, 17 });
		IF_AWNE = new GXInstFormat("AW<>", new byte[4] { 64, 5, 1, 17 });
		IF_AWG = new GXInstFormat("AW>", new byte[4] { 64, 5, 2, 17 });
		IF_AWGE = new GXInstFormat("AW>=", new byte[4] { 64, 5, 3, 17 });
		IF_AWL = new GXInstFormat("AW<", new byte[4] { 64, 5, 4, 17 });
		IF_AWLE = new GXInstFormat("AW<=", new byte[4] { 64, 5, 5, 17 });
		IF_ORWEQ = new GXInstFormat("ORW=", new byte[4] { 64, 5, 0, 18 });
		IF_ORWNE = new GXInstFormat("ORW<>", new byte[4] { 64, 5, 1, 18 });
		IF_ORWG = new GXInstFormat("ORW>", new byte[4] { 64, 5, 2, 18 });
		IF_ORWGE = new GXInstFormat("ORW>=", new byte[4] { 64, 5, 3, 18 });
		IF_ORWL = new GXInstFormat("ORW<", new byte[4] { 64, 5, 4, 18 });
		IF_ORWLE = new GXInstFormat("ORW<=", new byte[4] { 64, 5, 5, 18 });
		IF_LDDEQ = new GXInstFormat("LDD=", new byte[4] { 64, 9, 6, 16 });
		IF_LDDNE = new GXInstFormat("LDD<>", new byte[4] { 64, 9, 7, 16 });
		IF_LDDG = new GXInstFormat("LDD>", new byte[4] { 64, 9, 8, 16 });
		IF_LDDGE = new GXInstFormat("LDD>=", new byte[4] { 64, 9, 9, 16 });
		IF_LDDL = new GXInstFormat("LDD<", new byte[4] { 64, 9, 10, 16 });
		IF_LDDLE = new GXInstFormat("LDD<=", new byte[4] { 64, 9, 11, 16 });
		IF_ADEQ = new GXInstFormat("AD=", new byte[4] { 64, 9, 6, 17 });
		IF_ADNE = new GXInstFormat("AD<>", new byte[4] { 64, 9, 7, 17 });
		IF_ADG = new GXInstFormat("AD>", new byte[4] { 64, 9, 8, 17 });
		IF_ADGE = new GXInstFormat("AD>=", new byte[4] { 64, 9, 9, 17 });
		IF_ADL = new GXInstFormat("AD<", new byte[4] { 64, 9, 10, 17 });
		IF_ADLE = new GXInstFormat("AD<=", new byte[4] { 64, 9, 11, 17 });
		IF_ORDEQ = new GXInstFormat("ORD=", new byte[4] { 64, 9, 6, 18 });
		IF_ORDNE = new GXInstFormat("ORD<>", new byte[4] { 64, 9, 7, 18 });
		IF_ORDG = new GXInstFormat("ORD>", new byte[4] { 64, 9, 8, 18 });
		IF_ORDGE = new GXInstFormat("ORD>=", new byte[4] { 64, 9, 9, 18 });
		IF_ORDL = new GXInstFormat("ORD<", new byte[4] { 64, 9, 10, 18 });
		IF_ORDLE = new GXInstFormat("ORD<=", new byte[4] { 64, 9, 11, 18 });
		IF_CMP = new GXInstFormat("CMP", new byte[3] { 72, 7, 6 });
		IF_CMPD = new GXInstFormat("CMPD", new byte[3] { 72, 13, 7 });
		IF_CMPF = new GXInstFormat("CMPF", new byte[3] { 72, 13, 10 });
		IF_ZCP = new GXInstFormat("ZCP", new byte[3] { 72, 9, 8 });
		IF_ZCPD = new GXInstFormat("ZCPD", new byte[3] { 72, 17, 9 });
		IF_ZCPF = new GXInstFormat("ZCPF", new byte[3] { 72, 17, 11 });
		IF_ADD = new GXInstFormat("ADD", new byte[3] { 73, 7, 40 });
		IF_SUB = new GXInstFormat("SUB", new byte[3] { 73, 7, 42 });
		IF_MUL = new GXInstFormat("MUL", new byte[3] { 73, 7, 44 });
		IF_DIV = new GXInstFormat("DIV", new byte[3] { 73, 7, 46 });
		IF_INC = new GXInstFormat("INC", new byte[3] { 74, 3, 0 });
		IF_DEC = new GXInstFormat("DEC", new byte[3] { 74, 3, 2 });
		IF_SQR = new GXInstFormat("SQR", new byte[3] { 90, 5, 8 });
		IF_ADDD = new GXInstFormat("ADDD", new byte[3] { 73, 13, 41 });
		IF_SUBD = new GXInstFormat("SUBD", new byte[3] { 73, 13, 43 });
		IF_MULD = new GXInstFormat("MULD", new byte[3] { 73, 13, 45 });
		IF_DIVD = new GXInstFormat("DIVD", new byte[3] { 73, 13, 47 });
		IF_DINC = new GXInstFormat("INCD", new byte[3] { 74, 3, 1 });
		IF_DDEC = new GXInstFormat("DECD", new byte[3] { 74, 3, 3 });
		IF_ADDF = new GXInstFormat("ADDF", new byte[3] { 73, 13, 48 });
		IF_SUBF = new GXInstFormat("SUBF", new byte[3] { 73, 13, 49 });
		IF_MULF = new GXInstFormat("MULF", new byte[3] { 73, 13, 50 });
		IF_DIVF = new GXInstFormat("DIVF", new byte[3] { 73, 13, 51 });
		IF_MOV = new GXInstFormat("MOV", new byte[3] { 76, 5, 0 });
		IF_XCH = new GXInstFormat("XCH", new byte[3] { 76, 5, 8 });
		IF_BCD = new GXInstFormat("BCD", new byte[3] { 75, 5, 0 });
		IF_BIN = new GXInstFormat("BIN", new byte[3] { 75, 5, 2 });
		IF_CML = new GXInstFormat("INVW", new byte[3] { 76, 5, 4 });
		IF_FMOV = new GXInstFormat("FMOV", new byte[3] { 76, 7, 7 });
		IF_DMOV = new GXInstFormat("MOVD", new byte[3] { 76, 9, 1 });
		IF_XCHD = new GXInstFormat("XCHD", new byte[3] { 76, 9, 9 });
		IF_DBCD = new GXInstFormat("BCDD", new byte[3] { 75, 9, 1 });
		IF_DBIN = new GXInstFormat("BIND", new byte[3] { 75, 9, 3 });
		IF_DCML = new GXInstFormat("INVD", new byte[3] { 76, 9, 5 });
		IF_DFMOV = new GXInstFormat("FMOVD", new byte[3] { 76, 13, 14 });
		IF_BMOV = new GXInstFormat("MVBLK", new byte[3] { 76, 7, 6 });
		IF_SMOV = new GXInstFormat("SMOV", new byte[3] { 76, 11, 12 });
		IF_ROR = new GXInstFormat("ROR", new byte[3] { 80, 5, 0 });
		IF_RCR = new GXInstFormat("RCR", new byte[3] { 80, 5, 1 });
		IF_ROL = new GXInstFormat("ROL", new byte[3] { 80, 5, 2 });
		IF_RCL = new GXInstFormat("RCL", new byte[3] { 80, 5, 3 });
		IF_DROR = new GXInstFormat("RORD", new byte[3] { 80, 9, 4 });
		IF_DRCR = new GXInstFormat("RCRD", new byte[3] { 80, 9, 5 });
		IF_DROL = new GXInstFormat("ROLD", new byte[3] { 80, 9, 6 });
		IF_DRCL = new GXInstFormat("RCLD", new byte[3] { 80, 9, 7 });
		IF_SFR = new GXInstFormat("SHL", new byte[3] { 81, 5, 0 });
		IF_SFL = new GXInstFormat("SHR", new byte[3] { 81, 5, 1 });
		IF_SFTR = new GXInstFormat("SHRB", new byte[3] { 81, 9, 6 });
		IF_SFTL = new GXInstFormat("SHLB", new byte[3] { 81, 9, 7 });
		IF_WSTR = new GXInstFormat("SHRBLK", new byte[3] { 81, 9, 8 });
		IF_WSTL = new GXInstFormat("SHLBLK", new byte[3] { 81, 9, 9 });
		IF_SFRD = new GXInstFormat("SFRD", new byte[3] { 81, 7, 11 });
		IF_SFWR = new GXInstFormat("SFWR", new byte[3] { 81, 7, 10 });
		IF_INT = new GXInstFormat("FTOW", new byte[3] { 75, 5, 4 });
		IF_DINT = new GXInstFormat("TRUNC", new byte[3] { 75, 9, 5 });
		IF_GRY = new GXInstFormat("GRY", new byte[3] { 75, 5, 10 });
		IF_DGRY = new GXInstFormat("GRYD", new byte[3] { 75, 9, 11 });
		IF_GBIN = new GXInstFormat("GBIN", new byte[3] { 75, 5, 12 });
		IF_DGBIN = new GXInstFormat("GBIND", new byte[3] { 75, 9, 13 });
		IF_NEG = new GXInstFormat("NEGW", new byte[3] { 75, 3, 14 });
		IF_NEGD = new GXInstFormat("NEGD", new byte[3] { 75, 5, 15 });
		IF_NEGF = new GXInstFormat("NEGF", new byte[3] { 75, 5, 21 });
		IF_TOUT = new GXInstFormat("TON", new byte[2] { 33, 3 });
		IF_COUT = new GXInstFormat("CTU", new byte[2] { 33, 3 });
		IF_SWAP = new GXInstFormat("SWAP", new byte[3] { 76, 3, 11 });
		IF_DSWAP = new GXInstFormat("DSWAP", new byte[3] { 76, 3, 13 });
		IF_SER = new GXInstFormat("SER", new byte[3] { 83, 9, 0 });
		IF_DSER = new GXInstFormat("SERD", new byte[3] { 83, 17, 1 });
		IF_DECO = new GXInstFormat("DECO", new byte[3] { 83, 7, 4 });
		IF_ENCO = new GXInstFormat("ENCO", new byte[3] { 83, 7, 5 });
		IF_SORT = new GXInstFormat("SORT", new byte[3] { 83, 11, 17 });
		IF_ZRST = new GXInstFormat("RST", new byte[3] { 83, 5, 25 });
		IF_SUM = new GXInstFormat("SUM", new byte[3] { 83, 5, 26 });
		IF_DSUM = new GXInstFormat("SUMD", new byte[3] { 83, 7, 27 });
		IF_BON = new GXInstFormat("BON", new byte[3] { 83, 7, 28 });
		IF_DBON = new GXInstFormat("BOND", new byte[3] { 83, 7, 29 });
		IF_MEAN = new GXInstFormat("AVE", new byte[3] { 83, 7, 30 });
		IF_FLT = new GXInstFormat("WTOF", new byte[3] { 83, 5, 34 });
		IF_DFLT = new GXInstFormat("DTOF", new byte[3] { 83, 9, 35 });
		IF_FOR = new GXInstFormat("FOR", new byte[3] { 106, 3, 0 });
		IF_NEXT = new GXInstFormat("NEXT", new byte[3] { 106, 1, 1 });
		IF_SRET = new GXInstFormat("SRET", new byte[3] { 106, 1, 7 });
		IF_DSQR = new GXInstFormat("SQRD", new byte[3] { 90, 9, 21 });
		IF_DESQR = new GXInstFormat("SQR", new byte[3] { 90, 9, 22 });
		IF_DSIN = new GXInstFormat("SIN", new byte[3] { 90, 9, 23 });
		IF_DCOS = new GXInstFormat("COS", new byte[3] { 90, 9, 24 });
		IF_DTAN = new GXInstFormat("TAN", new byte[3] { 90, 9, 25 });
		IF_DEXP = new GXInstFormat("EXP", new byte[3] { 90, 9, 26 });
		IF_TCMP = new GXInstFormat("TCMP", new byte[3] { 93, 11, 6 });
		IF_TZCP = new GXInstFormat("TZCP", new byte[3] { 93, 9, 7 });
		IF_TADD = new GXInstFormat("CKADD", new byte[3] { 93, 7, 8 });
		IF_TSUB = new GXInstFormat("CKSUB", new byte[3] { 93, 7, 9 });
		IF_TRD = new GXInstFormat("TRD", new byte[3] { 93, 3, 10 });
		IF_TWR = new GXInstFormat("TWR", new byte[3] { 93, 3, 11 });
		IF_ABSD = new GXInstFormat("ABSD", new byte[3] { 99, 9, 12 });
		IF_ARWS = new GXInstFormat("ARWS", new byte[3] { 95, 9, 9 });
		IF_DABS = new GXInstFormat("DABS", new byte[3] { 99, 13, 21 });
		IF_DABSD = new GXInstFormat("DABSD", new byte[3] { 99, 17, 13 });
		IF_DEXTR = new GXInstFormat("DEXTR", new byte[3] { 99, 17, 31 });
		IF_DHKY = new GXInstFormat("DHKY", new byte[3] { 95, 17, 5 });
		IF_DHOUR = new GXInstFormat("DHOUR", new byte[3] { 93, 13, 13 });
		IF_DHSCR = new GXInstFormat("DHSCR", new byte[3] { 78, 13, 6 });
		IF_DHSCS = new GXInstFormat("DHSCS", new byte[3] { 78, 13, 4 });
		IF_DHSZ = new GXInstFormat("DHSZ", new byte[3] { 78, 17, 8 });
		IF_DMEAN = new GXInstFormat("DMEAN", new byte[3] { 83, 13, 31 });
		IF_DPLSR = new GXInstFormat("PLSR", new byte[3] { 99, 17, 19 });
		IF_DPLSY = new GXInstFormat("PLSY", new byte[3] { 99, 13, 10 });
		IF_DPLSV = new GXInstFormat("PLSV", new byte[3] { 99, 13, 25 });
		IF_DSW = new GXInstFormat("DSW", new byte[3] { 95, 9, 6 });
		IF_DTKY = new GXInstFormat("DTKY", new byte[3] { 95, 13, 3 });
		IF_EXTR = new GXInstFormat("DTKY", new byte[3] { 99, 9, 30 });
		IF_HKY = new GXInstFormat("HKY", new byte[3] { 95, 9, 4 });
		IF_HOUR = new GXInstFormat("HOUR", new byte[3] { 93, 7, 12 });
		IF_INCD = new GXInstFormat("INCD", new byte[3] { 99, 9, 14 });
		IF_IST = new GXInstFormat("IST", new byte[3] { 99, 7, 11 });
		IF_MTR = new GXInstFormat("MTR", new byte[3] { 99, 9, 9 });
		IF_PLSR = new GXInstFormat("PLSR", new byte[3] { 99, 9, 17 });
		IF_PLSY = new GXInstFormat("PLSY", new byte[3] { 99, 7, 7 });
		IF_PWM = new GXInstFormat("PWM", new byte[3] { 99, 7, 8 });
		IF_RAMP = new GXInstFormat("RAMP", new byte[3] { 99, 9, 16 });
		IF_RD3A = new GXInstFormat("RD3A", new byte[3] { 95, 7, 27 });
		IF_ROTC = new GXInstFormat("ROTC", new byte[3] { 99, 9, 4 });
		IF_SEGD = new GXInstFormat("SEGD", new byte[3] { 95, 5, 7 });
		IF_SEGL = new GXInstFormat("SEGL", new byte[3] { 95, 7, 8 });
		IF_SPD = new GXInstFormat("SPD", new byte[3] { 99, 7, 6 });
		IF_STMR = new GXInstFormat("STMR", new byte[3] { 99, 7, 3 });
		IF_TKY = new GXInstFormat("TKY", new byte[3] { 95, 7, 2 });
		IF_TTMR = new GXInstFormat("TTMR", new byte[3] { 99, 5, 2 });
		IF_WDT = new GXInstFormat("WDT", new byte[3] { 97, 1, 0 });
		IF_WR3A = new GXInstFormat("WR3A", new byte[3] { 95, 7, 28 });
		IF_ADPRW = new GXInstFormat("ADPRW", new byte[3] { 95, 11, 36 });
		IF_CJ = new GXInstFormat("CJ", new byte[3] { 77, 3, 0 });
		IF_CALL = new GXInstFormat("CALL", new byte[3] { 84, 3, 1 });
		IF_LBL = new GXInstFormat("LBL", new byte[1] { 60 });
		IF_STL = new GXInstFormat("STL", new byte[3] { 108, 1, 35 });
		IF_RET = new GXInstFormat("STLE", new byte[3] { 108, 1, 36 });
		IF_LIMIT = new GXInstFormat("LIMIT", new byte[3] { 91, 9, 0 });
		IF_DLIMIT = new GXInstFormat("LIMITD", new byte[3] { 91, 17, 1 });
		IF_BAND = new GXInstFormat("BAND", new byte[3] { 91, 9, 2 });
		IF_DBAND = new GXInstFormat("BANDD", new byte[3] { 91, 17, 3 });
		IF_ZONE = new GXInstFormat("ZONE", new byte[3] { 91, 9, 4 });
		IF_DZONE = new GXInstFormat("ZONED", new byte[3] { 91, 17, 5 });
		IF_SCL = new GXInstFormat("SCL", new byte[3] { 99, 7, 36 });
		IF_DSCL = new GXInstFormat("SCLD", new byte[3] { 99, 13, 37 });
		IF_SCL2 = new GXInstFormat("SCL2", new byte[3] { 99, 7, 42 });
		IF_DSCL2 = new GXInstFormat("SCL2D", new byte[3] { 99, 13, 43 });
		IF_ANS = new GXInstFormat("ANS", new byte[3] { 83, 7, 32 });
		IF_ANR = new GXInstFormat("ANR", new byte[3] { 83, 1, 33 });
		IF_REFF = new GXInstFormat("REFF", new byte[3] { 78, 3, 2 });
		IF_REF = new GXInstFormat("REF", new byte[3] { 78, 5, 1 });
		IF_BKADD = new GXInstFormat("BK+", new byte[3] { 73, 9, 38 });
		IF_BKSUB = new GXInstFormat("BK-", new byte[3] { 73, 9, 39 });
		IF_DBKADD = new GXInstFormat("DBK+", new byte[3] { 73, 17, 52 });
		IF_DBKSUB = new GXInstFormat("DBK-", new byte[3] { 73, 17, 53 });
		IF_BKCMP_EQUAL = new GXInstFormat("BK=", new byte[3] { 72, 9, 0 });
		IF_BKCMP_NOTEQUAL = new GXInstFormat("BK<>", new byte[3] { 72, 9, 1 });
		IF_BKCMP_LESS = new GXInstFormat("BK<", new byte[3] { 72, 9, 4 });
		IF_BKCMP_MORE = new GXInstFormat("BK>", new byte[3] { 72, 9, 2 });
		IF_BKCMP_NOTMORE = new GXInstFormat("BK<=", new byte[3] { 72, 9, 5 });
		IF_BKCMP_NOTLESS = new GXInstFormat("BK>=", new byte[3] { 72, 9, 3 });
		IF_DBKCMP_EQUAL = new GXInstFormat("DBK=", new byte[3] { 72, 17, 12 });
		IF_DBKCMP_NOTEQUAL = new GXInstFormat("DBK<>", new byte[3] { 72, 17, 13 });
		IF_DBKCMP_LESS = new GXInstFormat("DBK<", new byte[3] { 72, 17, 16 });
		IF_DBKCMP_MORE = new GXInstFormat("DBK>", new byte[3] { 72, 17, 14 });
		IF_DBKCMP_NOTMORE = new GXInstFormat("DBK<=", new byte[3] { 72, 17, 17 });
		IF_DBKCMP_NOTLESS = new GXInstFormat("DBK>=", new byte[3] { 72, 17, 15 });
		IF_FROM = new GXInstFormat("FROM", new byte[3] { 86, 9, 0 });
		IF_TO = new GXInstFormat("TO", new byte[3] { 86, 9, 2 });
		IF_DFROM = new GXInstFormat("DFROM", new byte[3] { 86, 17, 8 });
		IF_DTO = new GXInstFormat("DTO", new byte[3] { 86, 17, 3 });
		IF_RBFM = new GXInstFormat("RBFM", new byte[3] { 86, 11, 9 });
		IF_WBFM = new GXInstFormat("WBFM", new byte[3] { 86, 11, 10 });
		IF_PR = new GXInstFormat("PR", new byte[3] { 87, 5, 0 });
		IF_STRADD = new GXInstFormat("$+", new byte[3] { 73, 7, 35 });
		IF_STRMOV = new GXInstFormat("$MOV", new byte[3] { 76, 5, 3 });
		IF_ASC = new GXInstFormat("ASC", new byte[3] { 83, 11, 24 });
		IF_BINDA = new GXInstFormat("BINDA", new byte[3] { 89, 5, 0 });
		IF_INSTR = new GXInstFormat("INSTR", new byte[3] { 89, 9, 26 });
		IF_LEFT = new GXInstFormat("LEFT", new byte[3] { 89, 7, 23 });
		IF_RIGHT = new GXInstFormat("RIGHT", new byte[3] { 89, 7, 22 });
		IF_LEN = new GXInstFormat("LEN", new byte[3] { 89, 5, 13 });
		IF_MIDR = new GXInstFormat("MIDR", new byte[3] { 89, 7, 24 });
		IF_MIDW = new GXInstFormat("MIDW", new byte[3] { 89, 7, 25 });
		IF_STR = new GXInstFormat("STR", new byte[3] { 89, 7, 14 });
		IF_VAL = new GXInstFormat("VAL", new byte[3] { 89, 7, 16 });
		IF_DABIN = new GXInstFormat("DABIN", new byte[3] { 89, 5, 6 });
		IF_DBINDA = new GXInstFormat("DBINDA", new byte[3] { 89, 9, 1 });
		IF_DESTR = new GXInstFormat("DESTR", new byte[3] { 89, 13, 30 });
		IF_DEVAL = new GXInstFormat("DEVAL", new byte[3] { 89, 9, 31 });
		IF_DSTR = new GXInstFormat("DSTR", new byte[3] { 89, 13, 15 });
		IF_DVAL = new GXInstFormat("DVAL", new byte[3] { 89, 13, 17 });
		IF_MC = new GXInstFormat("MC", new byte[2] { 44, 3 });
		IF_MCR = new GXInstFormat("MCR", new byte[2] { 48, 2 });
		IF_DI = new GXInstFormat("DI", new byte[3] { 105, 1, 0 });
		IF_EI = new GXInstFormat("EI", new byte[3] { 105, 1, 1 });
		IF_WAND = new GXInstFormat("ANDW", new byte[3] { 79, 7, 1 });
		IF_WOR = new GXInstFormat("ORW", new byte[3] { 79, 7, 3 });
		IF_WXOR = new GXInstFormat("XORW", new byte[3] { 79, 7, 5 });
		IF_RS = new GXInstFormat("RS", new byte[3] { 95, 9, 10 });
		IF_PRUN = new GXInstFormat("PRUN", new byte[3] { 95, 5, 11 });
		IF_ASCI = new GXInstFormat("ASCI", new byte[3] { 89, 7, 29 });
		IF_HEX = new GXInstFormat("HEX", new byte[3] { 89, 7, 21 });
		IF_CCD = new GXInstFormat("CCD", new byte[3] { 95, 7, 13 });
		IF_RS2 = new GXInstFormat("RS2", new byte[3] { 95, 11, 29 });
		IF_PID = new GXInstFormat("PID", new byte[3] { 94, 9, 7 });
		IF_ZPUSH = new GXInstFormat("ZPUSH", new byte[3] { 97, 3, 10 });
		IF_ZPOP = new GXInstFormat("ZPOP", new byte[3] { 97, 3, 11 });
		IF_DEMOV = new GXInstFormat("MOVD", new byte[3] { 76, 9, 15 });
		IF_DEBCD = new GXInstFormat("DEBCD", new byte[3] { 75, 9, 19 });
		IF_DEBIN = new GXInstFormat("DEBIN", new byte[3] { 75, 9, 20 });
		IF_DLOGE = new GXInstFormat("DLOGE", new byte[3] { 90, 9, 27 });
		IF_DLOG10 = new GXInstFormat("DLOG10", new byte[3] { 90, 9, 28 });
		IF_ASIN = new GXInstFormat("ASIN", new byte[3] { 90, 9, 29 });
		IF_ACOS = new GXInstFormat("ACOS", new byte[3] { 90, 9, 30 });
		IF_ATAN = new GXInstFormat("ATAN", new byte[3] { 90, 9, 31 });
		IF_DPAD = new GXInstFormat("DPAD", new byte[3] { 90, 9, 32 });
		IF_DDEG = new GXInstFormat("DDEG", new byte[3] { 90, 9, 33 });
		IF_DIS = new GXInstFormat("DIS", new byte[3] { 83, 7, 7 });
		IF_UNI = new GXInstFormat("UNI", new byte[3] { 83, 7, 8 });
		IF_WSUM = new GXInstFormat("WSUM", new byte[3] { 83, 7, 19 });
		IF_WTOB = new GXInstFormat("WTOB", new byte[3] { 83, 7, 11 });
		IF_BTOW = new GXInstFormat("BTOW", new byte[3] { 83, 7, 12 });
		IF_SORT2 = new GXInstFormat("SORT2", new byte[3] { 83, 11, 36 });
		IF_DSZR = new GXInstFormat("DSZR", new byte[3] { 99, 9, 32 });
		IF_DVIT = new GXInstFormat("DVIT", new byte[3] { 99, 9, 33 });
		IF_DTBL = new GXInstFormat("DTBL", new byte[3] { 99, 17, 47 });
		IF_ZRN = new GXInstFormat("DTBL", new byte[3] { 99, 9, 22 });
		IF_PLSV = new GXInstFormat("PLSV", new byte[3] { 99, 7, 24 });
		IF_DRVI = new GXInstFormat("DRVI", new byte[3] { 99, 9, 26 });
		IF_DRVA = new GXInstFormat("DRVA", new byte[3] { 99, 9, 28 });
		IF_HTOS = new GXInstFormat("HTOS", new byte[3] { 93, 5, 14 });
		IF_STOH = new GXInstFormat("STOH", new byte[3] { 93, 5, 16 });
		IF_COMRD = new GXInstFormat("COMRD", new byte[3] { 89, 5, 12 });
		IF_RAD = new GXInstFormat("RAD", new byte[3] { 90, 3, 11 });
		IF_DUTY = new GXInstFormat("DUTY", new byte[3] { 97, 7, 1 });
		IF_CRC = new GXInstFormat("CRC", new byte[3] { 95, 7, 30 });
		IF_HCMOV = new GXInstFormat("HCMOV", new byte[3] { 78, 13, 9 });
		IF_FDEL = new GXInstFormat("FDEL", new byte[3] { 85, 7, 4 });
		IF_FINS = new GXInstFormat("FINS", new byte[3] { 85, 7, 3 });
		IF_POP = new GXInstFormat("POP", new byte[3] { 99, 7, 35 });
		IF_IVCK = new GXInstFormat("IVCK", new byte[3] { 95, 9, 31 });
		IF_IVDR = new GXInstFormat("IVDR", new byte[3] { 95, 9, 32 });
		IF_IVRD = new GXInstFormat("IVRD", new byte[3] { 95, 9, 33 });
		IF_IVWR = new GXInstFormat("IVWR", new byte[3] { 95, 9, 34 });
		IF_IVBWR = new GXInstFormat("IVWR", new byte[3] { 95, 9, 35 });
		IF_DHSCT = new GXInstFormat("DHSCT", new byte[3] { 78, 21, 10 });
		IF_LOADR = new GXInstFormat("LOADR", new byte[3] { 99, 5, 38 });
		IF_SAVER = new GXInstFormat("SAVER", new byte[3] { 99, 7, 39 });
		IF_INITR = new GXInstFormat("INITR", new byte[3] { 99, 5, 40 });
		IF_LOGR = new GXInstFormat("LOGR", new byte[3] { 99, 11, 41 });
		IF_RWER = new GXInstFormat("RWER", new byte[3] { 99, 5, 44 });
		IF_INITER = new GXInstFormat("INITER", new byte[3] { 99, 5, 45 });
		IF_DDRVI = new GXInstFormat("DRVI", new byte[3] { 99, 17, 27 });
		IF_DDRVA = new GXInstFormat("DRVA", new byte[3] { 99, 17, 29 });
		try
		{
			VF_ByHexs[144] = new List<GXValueFormat> { VF_M };
			VF_ByHexs[152] = new List<GXValueFormat> { VF_S };
			VF_ByHexs[156] = new List<GXValueFormat> { VF_X };
			VF_ByHexs[157] = new List<GXValueFormat> { VF_Y };
			VF_ByHexs[168] = new List<GXValueFormat> { VF_D };
			VF_ByHexs[194] = new List<GXValueFormat> { VF_T, VF_TV };
			VF_ByHexs[197] = new List<GXValueFormat> { VF_C, VF_CV };
			VF_ByHexs[204] = new List<GXValueFormat> { VF_Z };
			VF_ByHexs[205] = new List<GXValueFormat> { VF_V };
			VF_ByHexs[232] = new List<GXValueFormat> { VF_K16 };
			VF_ByHexs[233] = new List<GXValueFormat> { VF_H16 };
			VF_ByHexs[208] = new List<GXValueFormat> { VF_P };
			IIF_ByHexs[68] = IIF_D;
			IIF_ByHexs[77] = IIF_M;
			IIF_ByHexs[83] = IIF_S;
			IIF_ByHexs[84] = IIF_T;
			IIF_ByHexs[67] = IIF_C;
			IIF_ByHexs[88] = IIF_X;
			IIF_ByHexs[89] = IIF_Y;
			Formats = new Dictionary<long, GXUnitFormat>();
			GXArgFormat[] array = null;
			array = new GXArgFormat[0];
			UF_LineComment = new GXUnitFormat(IF_LineComment, array);
			UF_OutputComment = new GXUnitFormat(IF_OutputComment, array);
			array = new GXArgFormat[1] { AF_LD };
			UF_LD = new GXInputFormat(IF_LD, array);
			Formats.Add(0L, UF_LD);
			Formats.Add(1L, new GXInputFormat(IF_LDI, array));
			Formats.Add(2L, new GXInputFormat(IF_LD_2, array));
			Formats.Add(258L, new GXInputFormat(IF_LDI_2, array));
			Formats.Add(514L, new GXInputFormat(IF_LDP, array));
			Formats.Add(770L, new GXInputFormat(IF_LDF, array));
			Formats.Add(6L, new GXInputFormat(IF_OR, array));
			Formats.Add(1538L, new GXInputFormat(IF_OR_2, array));
			Formats.Add(7L, new GXInputFormat(IF_ORI, array));
			Formats.Add(1794L, new GXInputFormat(IF_ORI_2, array));
			Formats.Add(2050L, new GXInputFormat(IF_ORP, array));
			Formats.Add(2306L, new GXInputFormat(IF_ORF, array));
			Formats.Add(12L, new GXInputFormat(IF_AND, array));
			Formats.Add(3074L, new GXInputFormat(IF_AND_2, array));
			Formats.Add(13L, new GXInputFormat(IF_ANDI, array));
			Formats.Add(3330L, new GXInputFormat(IF_ANDI_2, array));
			Formats.Add(3586L, new GXInputFormat(IF_ANDP, array));
			Formats.Add(3842L, new GXInputFormat(IF_ANDF, array));
			Formats.Add(3L, new GXInputFormat(IF_Dot_LD, array));
			Formats.Add(259L, new GXInputFormat(IF_Dot_LDI, array));
			Formats.Add(515L, new GXInputFormat(IF_Dot_LDP, array));
			Formats.Add(771L, new GXInputFormat(IF_Dot_LDF, array));
			Formats.Add(1539L, new GXInputFormat(IF_Dot_OR, array));
			Formats.Add(1795L, new GXInputFormat(IF_Dot_ORI, array));
			Formats.Add(2051L, new GXInputFormat(IF_Dot_ORP, array));
			Formats.Add(2307L, new GXInputFormat(IF_Dot_ORF, array));
			Formats.Add(3075L, new GXInputFormat(IF_Dot_AND, array));
			Formats.Add(3331L, new GXInputFormat(IF_Dot_ANDI, array));
			Formats.Add(3587L, new GXInputFormat(IF_Dot_ANDP, array));
			Formats.Add(3843L, new GXInputFormat(IF_Dot_ANDF, array));
			array = new GXArgFormat[0];
			UF_MEP = new GXUnitFormat(IF_MEP, array);
			Formats.Add(18L, UF_MEP);
			UF_MEF = new GXUnitFormat(IF_MEF, array);
			Formats.Add(19L, UF_MEF);
			UF_INV = new GXUnitFormat(IF_INV, array);
			Formats.Add(20L, UF_INV);
			UF_ORB = new GXUnitFormat(IF_ORB, array);
			Formats.Add(24L, UF_ORB);
			UF_ANDB = new GXUnitFormat(IF_ANDB, array);
			Formats.Add(25L, UF_ANDB);
			UF_MPS = new GXMPSFormat(IF_MPS, array);
			Formats.Add(26L, UF_MPS);
			UF_MRD = new GXMRDFormat(IF_MRD, array);
			Formats.Add(27L, UF_MRD);
			UF_MPP = new GXMPPFormat(IF_MPP, array);
			Formats.Add(28L, UF_MPP);
			UF_FEND = new GXFENDFormat(IF_FEND, array);
			Formats.Add(51L, UF_FEND);
			UF_END = new GXUnitFormat(IF_END, array);
			Formats.Add(52L, UF_END);
			array = new GXArgFormat[1] { AF_OUT };
			Formats.Add(32L, new GXOutputFormat(IF_OUT, array));
			Formats.Add(35L, new GXSetRstFormat(IF_SET, array));
			Formats.Add(36L, new GXSetRstFormat(IF_RST, array));
			Formats.Add(8195L, new GXOutputFormat(IF_Dot_OUT, array));
			Formats.Add(8963L, new GXSetRstFormat(IF_Dot_SET, array));
			Formats.Add(9219L, new GXSetRstFormat(IF_Dot_RST, new GXArgFormat[1] { AF_ALL }));
			Formats.Add(6488847L, new GXALTFormat(IF_ALT, array));
			Formats.Add(9474L, new GXOutputFormat(IF_PLS, array));
			Formats.Add(9730L, new GXOutputFormat(IF_PLF, array));
			Formats.Add(8194L, new GXOutputFormat(IF_Spe_OUT, array));
			Formats.Add(8962L, new GXSetRstFormat(IF_Spe_SET, array));
			Formats.Add(9218L, new GXSetRstFormat(IF_Spe_RST, array));
			array = new GXArgFormat[2] { AF_LDW, AF_LDW };
			Formats.Add(1074069520L, new GXCmp16Format(IF_LDWEQ, array));
			Formats.Add(1074069776L, new GXCmp16Format(IF_LDWNE, array));
			Formats.Add(1074070032L, new GXCmp16Format(IF_LDWG, array));
			Formats.Add(1074070288L, new GXCmp16Format(IF_LDWGE, array));
			Formats.Add(1074070544L, new GXCmp16Format(IF_LDWL, array));
			Formats.Add(1074070800L, new GXCmp16Format(IF_LDWLE, array));
			Formats.Add(1074069521L, new GXCmp16Format(IF_AWEQ, array));
			Formats.Add(1074069777L, new GXCmp16Format(IF_AWNE, array));
			Formats.Add(1074070033L, new GXCmp16Format(IF_AWG, array));
			Formats.Add(1074070289L, new GXCmp16Format(IF_AWGE, array));
			Formats.Add(1074070545L, new GXCmp16Format(IF_AWL, array));
			Formats.Add(1074070801L, new GXCmp16Format(IF_AWLE, array));
			Formats.Add(1074069522L, new GXCmp16Format(IF_ORWEQ, array));
			Formats.Add(1074069778L, new GXCmp16Format(IF_ORWNE, array));
			Formats.Add(1074070034L, new GXCmp16Format(IF_ORWG, array));
			Formats.Add(1074070290L, new GXCmp16Format(IF_ORWGE, array));
			Formats.Add(1074070546L, new GXCmp16Format(IF_ORWL, array));
			Formats.Add(1074070802L, new GXCmp16Format(IF_ORWLE, array));
			array = new GXArgFormat[2] { AF_LDD, AF_LDD };
			Formats.Add(1074333200L, new GXCmp32Format(IF_LDDEQ, array));
			Formats.Add(1074333456L, new GXCmp32Format(IF_LDDNE, array));
			Formats.Add(1074333712L, new GXCmp32Format(IF_LDDG, array));
			Formats.Add(1074333968L, new GXCmp32Format(IF_LDDGE, array));
			Formats.Add(1074334224L, new GXCmp32Format(IF_LDDL, array));
			Formats.Add(1074334480L, new GXCmp32Format(IF_LDDLE, array));
			Formats.Add(1074333201L, new GXCmp32Format(IF_ADEQ, array));
			Formats.Add(1074333457L, new GXCmp32Format(IF_ADNE, array));
			Formats.Add(1074333713L, new GXCmp32Format(IF_ADG, array));
			Formats.Add(1074333969L, new GXCmp32Format(IF_ADGE, array));
			Formats.Add(1074334225L, new GXCmp32Format(IF_ADL, array));
			Formats.Add(1074334481L, new GXCmp32Format(IF_ADLE, array));
			Formats.Add(1074333202L, new GXCmp32Format(IF_ORDEQ, array));
			Formats.Add(1074333458L, new GXCmp32Format(IF_ORDNE, array));
			Formats.Add(1074333714L, new GXCmp32Format(IF_ORDG, array));
			Formats.Add(1074333970L, new GXCmp32Format(IF_ORDGE, array));
			Formats.Add(1074334226L, new GXCmp32Format(IF_ORDL, array));
			Formats.Add(1074334482L, new GXCmp32Format(IF_ORDLE, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(4785960L, new GXCal3Format(IF_ADD, array));
			Formats.Add(4785962L, new GXCal3Format(IF_SUB, array));
			Formats.Add(4785964L, new GXCal3Format(IF_MUL, array));
			Formats.Add(4785966L, new GXCal3Format(IF_DIV, array));
			Formats.Add(5179137L, new GXCal3Format(IF_WAND, array));
			Formats.Add(5179139L, new GXCal3Format(IF_WOR, array));
			Formats.Add(5179141L, new GXCal3Format(IF_WXOR, array));
			array = new GXArgFormat[1] { AF_SRW };
			Formats.Add(4850432L, new GXSelf16Format(IF_INC, array));
			Formats.Add(4850434L, new GXSelf16Format(IF_DEC, array));
			Formats.Add(4915982L, new GXSelf16Format(IF_NEG, array));
			array = new GXArgFormat[2] { AF_LDW, AF_SRW };
			Formats.Add(4982016L, new GXCal2Format(IF_MOV, array));
			Formats.Add(4916480L, new GXCal2Format(IF_BCD, array));
			Formats.Add(4916482L, new GXCal2Format(IF_BIN, array));
			Formats.Add(4982020L, new GXCal2Format(IF_CML, array));
			Formats.Add(4916490L, new GXCal2Format(IF_GRY, array));
			Formats.Add(4916492L, new GXCal2Format(IF_GBIN, array));
			Formats.Add(5440794L, new GXUnusedFormat(IF_SUM, array));
			Formats.Add(6227207L, new GXUnusedFormat(IF_SEGD, array));
			Formats.Add(5899528L, new GXCal3Format(IF_SQR, array));
			array = new GXArgFormat[1] { AF_LDW };
			Formats.Add(4981515L, new GXCal2Format(IF_SWAP, array));
			array = new GXArgFormat[2] { AF_SRW, AF_SRW };
			Formats.Add(4982024L, new GXCal2Format(IF_XCH, array));
			array = new GXArgFormat[3] { AF_LDD, AF_LDD, AF_SRD };
			Formats.Add(4787497L, new GXCal3Format_32(IF_ADDD, array));
			Formats.Add(4787499L, new GXCal3Format_32(IF_SUBD, array));
			Formats.Add(4787501L, new GXCal3Format_32(IF_MULD, array));
			Formats.Add(4787503L, new GXCal3Format_32(IF_DIVD, array));
			array = new GXArgFormat[1] { AF_SRD };
			Formats.Add(4850945L, new GXSelf32Format(IF_DINC, array));
			Formats.Add(4850947L, new GXSelf32Format(IF_DDEC, array));
			Formats.Add(4916495L, new GXSelf32Format(IF_NEGD, array));
			array = new GXArgFormat[1] { AF_SRF };
			Formats.Add(4916501L, new GXSelfF32Format(IF_NEGF, array));
			array = new GXArgFormat[2] { AF_LDD, AF_SRD };
			Formats.Add(4983041L, new GXCal2Format_32(IF_DMOV, array));
			Formats.Add(4917505L, new GXCal2Format_32(IF_DBCD, array));
			Formats.Add(4917507L, new GXCal2Format_32(IF_DBIN, array));
			Formats.Add(4983045L, new GXCal2Format_32(IF_DCML, array));
			Formats.Add(4917515L, new GXCal2Format_32(IF_DGRY, array));
			Formats.Add(4917517L, new GXCal2Format_32(IF_DGBIN, array));
			Formats.Add(4981517L, new GXCal2Format_32(IF_DSWAP, array));
			Formats.Add(5441307L, new GXCal2Format_32(IF_DSUM, array));
			Formats.Add(5900565L, new GXCal2Format_32(IF_DSQR, array));
			array = new GXArgFormat[2] { AF_LDF, AF_SRF };
			Formats.Add(4983055L, new GXCal2Format_F(IF_DEMOV, array));
			Formats.Add(4917523L, new GXUnusedFormat(IF_DEBCD, array));
			Formats.Add(4917524L, new GXUnusedFormat(IF_DEBIN, array));
			Formats.Add(5900566L, new GXCal2Format_F(IF_DESQR, array));
			Formats.Add(5900567L, new GXCal2Format_F(IF_DSIN, array));
			Formats.Add(5900568L, new GXCal2Format_F(IF_DCOS, array));
			Formats.Add(5900569L, new GXCal2Format_F(IF_DTAN, array));
			Formats.Add(5900570L, new GXCal2Format_F(IF_DEXP, array));
			Formats.Add(5900571L, new GXUnusedFormat(IF_DLOGE, array));
			Formats.Add(5900572L, new GXUnusedFormat(IF_DLOG10, array));
			Formats.Add(5900573L, new GXUnusedFormat(IF_ASIN, array));
			Formats.Add(5900574L, new GXUnusedFormat(IF_ACOS, array));
			Formats.Add(5900575L, new GXUnusedFormat(IF_ATAN, array));
			Formats.Add(5900576L, new GXUnusedFormat(IF_DPAD, array));
			Formats.Add(5900577L, new GXUnusedFormat(IF_DDEG, array));
			array = new GXArgFormat[2] { AF_SRD, AF_SRD };
			Formats.Add(4983049L, new GXCal2Format_32(IF_XCHD, array));
			array = new GXArgFormat[3] { AF_LDF, AF_LDF, AF_SRF };
			Formats.Add(4787504L, new GXCal3Format_F(IF_ADDF, array));
			Formats.Add(4787505L, new GXCal3Format_F(IF_SUBF, array));
			Formats.Add(4787506L, new GXCal3Format_F(IF_MULF, array));
			Formats.Add(4787507L, new GXCal3Format_F(IF_DIVF, array));
			array = new GXArgFormat[3] { AF_LDW, AF_SRW, AF_K16 };
			Formats.Add(4982535L, new GXBlk16Format(IF_FMOV, array));
			array = new GXArgFormat[3] { AF_LDD, AF_SRD, AF_K32 };
			Formats.Add(4984078L, new GXBlk16Format(IF_DFMOV, array));
			array = new GXArgFormat[3] { AF_LDW, AF_SRW, AF_K16 };
			Formats.Add(4982534L, new GXBlkBFormat(IF_BMOV, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(4983564L, new GXSMOVFormat(IF_SMOV, array));
			array = new GXArgFormat[2] { AF_SRW, AF_LDW };
			Formats.Add(5309696L, new GXSh16Format(IF_SFR, array));
			Formats.Add(5309697L, new GXSh16Format(IF_SFL, array));
			Formats.Add(5244160L, new GXSh16Format(IF_ROR, array));
			Formats.Add(5244161L, new GXSh16Format(IF_RCR, array));
			Formats.Add(5244162L, new GXSh16Format(IF_ROL, array));
			Formats.Add(5244163L, new GXSh16Format(IF_RCL, array));
			array = new GXArgFormat[2] { AF_SRD, AF_LDD };
			Formats.Add(5245188L, new GXSh32Format(IF_DROR, array));
			Formats.Add(5245189L, new GXSh32Format(IF_DRCR, array));
			Formats.Add(5245190L, new GXSh32Format(IF_DROL, array));
			Formats.Add(5245191L, new GXSh32Format(IF_DRCL, array));
			array = new GXArgFormat[4] { AF_LD, AF_OUT, AF_LDW, AF_LDW };
			Formats.Add(5310726L, new GXShbFormat(IF_SFTR, array));
			Formats.Add(5310727L, new GXShbFormat(IF_SFTL, array));
			array = new GXArgFormat[4] { AF_LDD, AF_SRD, AF_LDW, AF_LDW };
			Formats.Add(5310728L, new GXWSTRFormat(IF_WSTR, array));
			Formats.Add(5310729L, new GXWSTLFormat(IF_WSTL, array));
			array = new GXArgFormat[3] { AF_SRW, AF_SRW, AF_LDW };
			Formats.Add(5310219L, new GXSFRDFormat(IF_SFRD, array));
			Formats.Add(5310218L, new GXSFWRFormat(IF_SFWR, array));
			array = new GXArgFormat[3] { AF_LDW, AF_OUT, AF_LDW };
			Formats.Add(5441308L, new GXBONFormat(IF_BON, array));
			array = new GXArgFormat[3] { AF_LDD, AF_OUT, AF_LDD };
			Formats.Add(5441309L, new GXDBONFormat(IF_DBON, array));
			array = new GXArgFormat[3] { AF_ALLW, AF_ALLB, AF_LDW };
			Formats.Add(5441284L, new GXDECOFormat(IF_DECO, array));
			array = new GXArgFormat[3] { AF_ALLB, AF_ALLW, AF_LDW };
			Formats.Add(5441285L, new GXENCOFormat(IF_ENCO, array));
			array = new GXArgFormat[2] { AF_LDW, AF_SRF };
			Formats.Add(5440802L, new GXOutputFormat(IF_FLT, array));
			array = new GXArgFormat[2] { AF_LDD, AF_SRF };
			Formats.Add(5441827L, new GXOutputFormat(IF_DFLT, array));
			array = new GXArgFormat[2] { AF_LDF, AF_SRW };
			Formats.Add(4916484L, new GXOutputFormat(IF_INT, array));
			array = new GXArgFormat[2] { AF_LDF, AF_SRD };
			Formats.Add(4917509L, new GXOutputFormat(IF_DINT, array));
			array = new GXArgFormat[2] { AF_OUT, AF_LDW };
			Formats.Add(8451L, new GXTOUTFormat(IF_TOUT, array));
			array = new GXArgFormat[3] { AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(5441310L, new GXMEANFormat(IF_MEAN, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(5441792L, new GXSERFormat(IF_SER, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_SRD, AF_LDD };
			Formats.Add(5443841L, new GXSERFormat(IF_DSER, array));
			array = new GXArgFormat[2] { AF_ALL, AF_ALL };
			Formats.Add(5440793L, new GXZRSTFormat(IF_ZRST, array));
			array = new GXArgFormat[1] { AF_LDW };
			Formats.Add(6947584L, new GXFORFormat(IF_FOR, array));
			array = new GXArgFormat[0];
			Formats.Add(6947073L, new GXNEXTFormat(IF_NEXT, array));
			array = new GXArgFormat[1] { AF_SRW };
			Formats.Add(6095626L, new GXTRDFormat(IF_TRD, array));
			array = new GXArgFormat[1] { AF_LDW };
			Formats.Add(6095627L, new GXTWRFormat(IF_TWR, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(6096648L, new GXTCal2Format(IF_TADD, array));
			Formats.Add(6096649L, new GXTCal2Format(IF_TSUB, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(6097670L, new GXUnusedFormat(IF_TCMP, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(6097159L, new GXTZCPFormat(IF_TZCP, array));
			array = new GXArgFormat[4] { AF_LDW, AF_CV, AF_LD, AF_K16 };
			Formats.Add(6490380L, new GXUnusedFormat(IF_ABSD, array));
			Formats.Add(6490382L, new GXUnusedFormat(IF_INCD, array));
			array = new GXArgFormat[4] { AF_LD, AF_LDW, AF_Y, AF_K16 };
			Formats.Add(6228233L, new GXUnusedFormat(IF_ARWS, array));
			array = new GXArgFormat[3] { AF_LD, AF_OUT, AF_SRD };
			Formats.Add(6491413L, new GXUnusedFormat(IF_DABS, array));
			array = new GXArgFormat[4] { AF_LDD, AF_CV32, AF_OUT, AF_K32 };
			Formats.Add(6492429L, new GXUnusedFormat(IF_DABSD, array));
			array = new GXArgFormat[4] { AF_K32, AF_LDD, AF_LDD, AF_LDD };
			Formats.Add(6492447L, new GXUnusedFormat(IF_DEXTR, array));
			array = new GXArgFormat[4] { AF_X, AF_Y, AF_SRD, AF_OUT };
			Formats.Add(6230277L, new GXUnusedFormat(IF_DHKY, array));
			array = new GXArgFormat[3] { AF_LDD, AF_D32, AF_OUT };
			Formats.Add(6098189L, new GXUnusedFormat(IF_DHOUR, array));
			array = new GXArgFormat[3] { AF_LDD, AF_CV32, AF_OUT };
			Formats.Add(5115142L, new GXUnusedFormat(IF_DHSCR, array));
			Formats.Add(5115140L, new GXUnusedFormat(IF_DHSCS, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_CV32, AF_OUT };
			Formats.Add(5116168L, new GXUnusedFormat(IF_DHSZ, array));
			array = new GXArgFormat[3] { AF_LDD, AF_SRD, AF_LDD };
			Formats.Add(5442847L, new GXUnusedFormat(IF_DMEAN, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_LDD, AF_Y };
			Formats.Add(6492435L, new GXUnusedFormat(IF_DPLSR, array));
			array = new GXArgFormat[3] { AF_LDD, AF_LDD, AF_Y };
			Formats.Add(6491402L, new GXUnusedFormat(IF_DPLSY, array));
			array = new GXArgFormat[3] { AF_LDD, AF_Y, AF_OUT };
			Formats.Add(6491417L, new GXUnusedFormat(IF_DPLSV, array));
			array = new GXArgFormat[4] { AF_X, AF_Y, AF_SRD, AF_K16 };
			Formats.Add(6228230L, new GXUnusedFormat(IF_DSW, array));
			array = new GXArgFormat[3] { AF_LD, AF_SRD, AF_OUT };
			Formats.Add(6229251L, new GXUnusedFormat(IF_DTKY, array));
			array = new GXArgFormat[4] { AF_K16, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(6490398L, new GXUnusedFormat(IF_EXTR, array));
			array = new GXArgFormat[4] { AF_X, AF_Y, AF_SRW, AF_OUT };
			Formats.Add(6228228L, new GXUnusedFormat(IF_HKY, array));
			array = new GXArgFormat[3] { AF_LDW, AF_D16, AF_OUT };
			Formats.Add(6096652L, new GXUnusedFormat(IF_HOUR, array));
			array = new GXArgFormat[3] { AF_LD, AF_S, AF_S };
			Formats.Add(6489867L, new GXISTFormat(IF_IST, array));
			array = new GXArgFormat[4] { AF_X, AF_Y, AF_OUT, AF_K16 };
			Formats.Add(6490377L, new GXUnusedFormat(IF_MTR, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_Y };
			Formats.Add(6490385L, new GXUnusedFormat(IF_PLSR, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_Y };
			Formats.Add(6489863L, new GXUnusedFormat(IF_PLSY, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_Y };
			Formats.Add(6489864L, new GXUnusedFormat(IF_PWM, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(6490384L, new GXUnusedFormat(IF_RAMP, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(6227739L, new GXUnusedFormat(IF_RD3A, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(6490372L, new GXUnusedFormat(IF_ROTC, array));
			array = new GXArgFormat[3] { AF_LDW, AF_Y, AF_K16 };
			Formats.Add(6227720L, new GXUnusedFormat(IF_SEGL, array));
			array = new GXArgFormat[3] { AF_X, AF_LDW, AF_LDW };
			Formats.Add(6489862L, new GXUnusedFormat(IF_SPD, array));
			array = new GXArgFormat[3] { AF_TV, AF_LDW, AF_OUT };
			Formats.Add(6489859L, new GXUnusedFormat(IF_STMR, array));
			array = new GXArgFormat[3] { AF_LD, AF_SRW, AF_OUT };
			Formats.Add(6227714L, new GXUnusedFormat(IF_TKY, array));
			array = new GXArgFormat[2] { AF_D16, AF_LDW };
			Formats.Add(6489346L, new GXUnusedFormat(IF_TTMR, array));
			array = new GXArgFormat[0];
			Formats.Add(6357248L, new GXUnusedFormat(IF_WDT, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(6227740L, new GXUnusedFormat(IF_WR3A, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(6228772L, new GXUnusedFormat(IF_ADPRW, array));
			array = new GXArgFormat[1] { AF_P };
			UF_CJ = new GXCJFormat(IF_CJ, array);
			Formats.Add(5047040L, UF_CJ);
			UF_CALL = new GXCALLFormat(IF_CALL, array);
			Formats.Add(5505793L, UF_CALL);
			UF_LBL = new GXLBLFormat(IF_LBL, array);
			Formats.Add(60L, UF_LBL);
			array = new GXArgFormat[1] { AF_S };
			UF_STL = new GXSTLFormat(IF_STL, array);
			Formats.Add(7078179L, UF_STL);
			array = new GXArgFormat[0];
			UF_RET = new GXRETFormat(IF_RET, array);
			Formats.Add(7078180L, UF_RET);
			UF_SRET = new GXSRETFormat(IF_SRET, array);
			Formats.Add(6947079L, UF_SRET);
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(4720390L, new GXCMPFormat(IF_CMP, array));
			array = new GXArgFormat[3] { AF_LDD, AF_LDD, AF_OUT };
			Formats.Add(4721927L, new GXCMPDFormat(IF_CMPD, array));
			array = new GXArgFormat[3] { AF_LDF, AF_LDF, AF_OUT };
			Formats.Add(4721930L, new GXCMPFFormat(IF_CMPF, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(4720904L, new GXZCPFormat(IF_ZCP, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_LDD, AF_OUT };
			Formats.Add(4722953L, new GXZCPDFormat(IF_ZCPD, array));
			array = new GXArgFormat[4] { AF_LDF, AF_LDF, AF_LDF, AF_OUT };
			Formats.Add(4722955L, new GXZCPFFormat(IF_ZCPF, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(5966080L, new GXLIMITFormat(IF_LIMIT, array));
			Formats.Add(5966082L, new GXBANDFormat(IF_BAND, array));
			Formats.Add(5966084L, new GXZONEFormat(IF_ZONE, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_LDD, AF_SRD };
			Formats.Add(5968129L, new GXLIMITDFormat(IF_DLIMIT, array));
			Formats.Add(5968131L, new GXBANDDFormat(IF_DBAND, array));
			Formats.Add(5968133L, new GXZONEDFormat(IF_DZONE, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_SRW };
			Formats.Add(6489892L, new GXSCLFormat(IF_SCL, array));
			Formats.Add(6489898L, new GXSCL2Format(IF_DSCL, array));
			array = new GXArgFormat[3] { AF_LDD, AF_LDD, AF_SRD };
			Formats.Add(6491429L, new GXSCLDFormat(IF_SCL2, array));
			Formats.Add(6491435L, new GXSCL2DFormat(IF_DSCL2, array));
			array = new GXArgFormat[2] { AF_N, AF_LD };
			UF_MC = new GXMCFormat(IF_MC, array);
			Formats.Add(11267L, UF_MC);
			array = new GXArgFormat[1] { AF_N };
			UF_MCR = new GXMCRFormat(IF_MCR, array);
			Formats.Add(12290L, UF_MCR);
			array = new GXArgFormat[0];
			Formats.Add(5439777L, new GXANRFormat(IF_ANR, array));
			array = new GXArgFormat[3] { AF_TV, AF_LDW, AF_S };
			Formats.Add(5441312L, new GXANSFormat(IF_ANS, array));
			array = new GXArgFormat[2] { AF_LD, AF_LDW };
			Formats.Add(5113089L, new GXREFFormat(IF_REF, array));
			array = new GXArgFormat[1] { AF_LDW };
			Formats.Add(5112578L, new GXUnusedFormat(IF_REFF, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(4786470L, new GXBKCal3Format(IF_BKADD, array));
			Formats.Add(4786471L, new GXBKCal3Format(IF_BKSUB, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_SRD, AF_LDD };
			Formats.Add(4788532L, new GXBKDCal3Format(IF_DBKADD, array));
			Formats.Add(4788533L, new GXBKDCal3Format(IF_DBKSUB, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_OUT, AF_LDW };
			Formats.Add(4720896L, new GXBKCmpFormat(IF_BKCMP_EQUAL, array));
			Formats.Add(4720897L, new GXBKCmpFormat(IF_BKCMP_NOTEQUAL, array));
			Formats.Add(4720900L, new GXBKCmpFormat(IF_BKCMP_LESS, array));
			Formats.Add(4720898L, new GXBKCmpFormat(IF_BKCMP_MORE, array));
			Formats.Add(4720901L, new GXBKCmpFormat(IF_BKCMP_NOTMORE, array));
			Formats.Add(4720899L, new GXBKCmpFormat(IF_BKCMP_NOTLESS, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_OUT, AF_LDD };
			Formats.Add(4722956L, new GXBKCmpFormat(IF_DBKCMP_EQUAL, array));
			Formats.Add(4722957L, new GXBKCmpFormat(IF_DBKCMP_NOTEQUAL, array));
			Formats.Add(4722960L, new GXBKCmpFormat(IF_DBKCMP_LESS, array));
			Formats.Add(4722958L, new GXBKCmpFormat(IF_DBKCMP_MORE, array));
			Formats.Add(4722961L, new GXBKCmpFormat(IF_DBKCMP_NOTMORE, array));
			Formats.Add(4722959L, new GXBKCmpFormat(IF_DBKCMP_NOTLESS, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(5638400L, new GXUnusedFormat(IF_FROM, array));
			Formats.Add(5638402L, new GXUnusedFormat(IF_TO, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_SRD, AF_LDD };
			Formats.Add(5640456L, new GXUnusedFormat(IF_DFROM, array));
			Formats.Add(5640451L, new GXUnusedFormat(IF_DTO, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(5638921L, new GXUnusedFormat(IF_RBFM, array));
			Formats.Add(5638922L, new GXUnusedFormat(IF_WBFM, array));
			array = new GXArgFormat[2] { AF_LDW, AF_OUT };
			Formats.Add(5702912L, new GXUnusedFormat(IF_PR, array));
			array = new GXArgFormat[3] { AF_LDS, AF_LDS, AF_SRS };
			Formats.Add(4785955L, new GXUnusedFormat(IF_STRADD, array));
			array = new GXArgFormat[2] { AF_LDS, AF_SRS };
			Formats.Add(4982019L, new GXUnusedFormat(IF_STRMOV, array));
			array = new GXArgFormat[2] { AF_LDS, AF_SRW };
			Formats.Add(5442328L, new GXUnusedFormat(IF_ASC, array));
			array = new GXArgFormat[2] { AF_LDW, AF_SRS };
			Formats.Add(5833984L, new GXUnusedFormat(IF_BINDA, array));
			array = new GXArgFormat[2] { AF_LDS, AF_SRW };
			Formats.Add(5833990L, new GXUnusedFormat(IF_DABIN, array));
			array = new GXArgFormat[2] { AF_LDD, AF_SRS };
			Formats.Add(5835009L, new GXUnusedFormat(IF_DBINDA, array));
			array = new GXArgFormat[4] { AF_LDS, AF_LDS, AF_SRW, AF_LDW };
			Formats.Add(5835034L, new GXUnusedFormat(IF_INSTR, array));
			array = new GXArgFormat[3] { AF_LDS, AF_LDS, AF_LDW };
			Formats.Add(5834519L, new GXUnusedFormat(IF_LEFT, array));
			Formats.Add(5834518L, new GXUnusedFormat(IF_RIGHT, array));
			array = new GXArgFormat[2] { AF_LDS, AF_SRW };
			Formats.Add(5833997L, new GXUnusedFormat(IF_LEN, array));
			array = new GXArgFormat[3] { AF_LDS, AF_SRS, AF_LDW };
			Formats.Add(5834520L, new GXUnusedFormat(IF_MIDR, array));
			Formats.Add(5834521L, new GXUnusedFormat(IF_MIDW, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_LDS };
			Formats.Add(5834510L, new GXUnusedFormat(IF_STR, array));
			array = new GXArgFormat[3] { AF_LDS, AF_LDW, AF_LDW };
			Formats.Add(5834512L, new GXUnusedFormat(IF_VAL, array));
			array = new GXArgFormat[3] { AF_LDF, AF_LDW, AF_SRS };
			Formats.Add(5836062L, new GXUnusedFormat(IF_DESTR, array));
			array = new GXArgFormat[2] { AF_LDS, AF_SRF };
			Formats.Add(5835039L, new GXUnusedFormat(IF_DEVAL, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDD, AF_LDS };
			Formats.Add(5836047L, new GXUnusedFormat(IF_DSTR, array));
			array = new GXArgFormat[3] { AF_LDS, AF_LDW, AF_LDD };
			Formats.Add(5836049L, new GXUnusedFormat(IF_DVAL, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(5442321L, new GXUnusedFormat(IF_SORT, array));
			array = new GXArgFormat[0];
			Formats.Add(6881536L, new GXDIFormat(IF_DI, array));
			Formats.Add(6881537L, new GXDIFormat(IF_EI, array));
			array = new GXArgFormat[4] { AF_SRW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(6228234L, new GXUnusedFormat(IF_RS, array));
			array = new GXArgFormat[2] { AF_LDW, AF_SRW };
			Formats.Add(6227211L, new GXUnusedFormat(IF_PRUN, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(5834525L, new GXUnusedFormat(IF_ASCI, array));
			Formats.Add(5834517L, new GXUnusedFormat(IF_HEX, array));
			Formats.Add(6227725L, new GXUnusedFormat(IF_CCD, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(6228765L, new GXUnusedFormat(IF_RS2, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(6162695L, new GXUnusedFormat(IF_PID, array));
			array = new GXArgFormat[1] { AF_LDW };
			Formats.Add(6357770L, new GXUnusedFormat(IF_ZPUSH, array));
			Formats.Add(6357771L, new GXUnusedFormat(IF_ZPOP, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(5441287L, new GXUnusedFormat(IF_DIS, array));
			Formats.Add(5441288L, new GXUnusedFormat(IF_UNI, array));
			Formats.Add(5441299L, new GXUnusedFormat(IF_WSUM, array));
			Formats.Add(5441291L, new GXUnusedFormat(IF_WTOB, array));
			Formats.Add(5441292L, new GXUnusedFormat(IF_BTOW, array));
			array = new GXArgFormat[5] { AF_LDW, AF_LDW, AF_LDW, AF_LDW, AF_LDW };
			Formats.Add(5442340L, new GXUnusedFormat(IF_SORT2, array));
			array = new GXArgFormat[4] { AF_LD, AF_LD, AF_OUT, AF_OUT };
			Formats.Add(6490400L, new GXUnusedFormat(IF_DSZR, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_OUT, AF_OUT };
			Formats.Add(6490401L, new GXUnusedFormat(IF_DVIT, array));
			Formats.Add(6490390L, new GXUnusedFormat(IF_ZRN, array));
			Formats.Add(6490394L, new GXUnusedFormat(IF_DRVI, array));
			Formats.Add(6490396L, new GXUnusedFormat(IF_DRVA, array));
			array = new GXArgFormat[2] { AF_OUT, AF_LDW };
			Formats.Add(6492463L, new GXUnusedFormat(IF_DTBL, array));
			array = new GXArgFormat[3] { AF_LDW, AF_OUT, AF_OUT };
			Formats.Add(6489880L, new GXUnusedFormat(IF_PLSV, array));
			array = new GXArgFormat[2] { AF_LDW, AF_SRW };
			Formats.Add(6096142L, new GXUnusedFormat(IF_HTOS, array));
			Formats.Add(6096144L, new GXUnusedFormat(IF_STOH, array));
			array = new GXArgFormat[2] { AF_ALL, AF_SRS };
			Formats.Add(5833996L, new GXUnusedFormat(IF_COMRD, array));
			array = new GXArgFormat[1] { AF_SRW };
			Formats.Add(5899019L, new GXUnusedFormat(IF_RAD, array));
			array = new GXArgFormat[3] { AF_LDW, AF_LDW, AF_OUT };
			Formats.Add(6358785L, new GXUnusedFormat(IF_DUTY, array));
			array = new GXArgFormat[3] { AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(6227742L, new GXUnusedFormat(IF_CRC, array));
			array = new GXArgFormat[3] { AF_LDD, AF_SRD, AF_LDD };
			Formats.Add(5115145L, new GXUnusedFormat(IF_HCMOV, array));
			array = new GXArgFormat[3] { AF_SRW, AF_SRW, AF_LDW };
			Formats.Add(5572356L, new GXUnusedFormat(IF_FDEL, array));
			array = new GXArgFormat[3] { AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(5572355L, new GXUnusedFormat(IF_FINS, array));
			array = new GXArgFormat[3] { AF_SRW, AF_SRW, AF_LDW };
			Formats.Add(6489891L, new GXUnusedFormat(IF_POP, array));
			array = new GXArgFormat[4] { AF_LDW, AF_LDW, AF_SRW, AF_LDW };
			Formats.Add(6228255L, new GXUnusedFormat(IF_IVCK, array));
			Formats.Add(6228256L, new GXUnusedFormat(IF_IVDR, array));
			Formats.Add(6228257L, new GXUnusedFormat(IF_IVRD, array));
			Formats.Add(6228258L, new GXUnusedFormat(IF_IVWR, array));
			Formats.Add(6228259L, new GXUnusedFormat(IF_IVBWR, array));
			array = new GXArgFormat[5] { AF_LDD, AF_LDD, AF_CV32, AF_OUT, AF_LDD };
			Formats.Add(5117194L, new GXUnusedFormat(IF_DHSCT, array));
			array = new GXArgFormat[2] { AF_SRW, AF_LDW };
			Formats.Add(6489382L, new GXUnusedFormat(IF_LOADR, array));
			Formats.Add(6489384L, new GXUnusedFormat(IF_INITR, array));
			Formats.Add(6489388L, new GXUnusedFormat(IF_RWER, array));
			Formats.Add(6489389L, new GXUnusedFormat(IF_INITER, array));
			array = new GXArgFormat[3] { AF_SRW, AF_LDW, AF_LDW };
			Formats.Add(6489895L, new GXUnusedFormat(IF_SAVER, array));
			array = new GXArgFormat[5] { AF_SRW, AF_LDW, AF_SRW, AF_LDW, AF_SRW };
			Formats.Add(6490921L, new GXUnusedFormat(IF_LOGR, array));
			array = new GXArgFormat[4] { AF_LDD, AF_LDD, AF_Y, AF_Y };
			Formats.Add(6492443L, new GXDDRVIFormat(IF_DDRVI, array));
			Formats.Add(6492445L, new GXDDRVAFormat(IF_DDRVA, array));
			KeyValuePair<long, GXUnitFormat>[] array2 = Formats.ToArray();
			for (int i = 0; i < array2.Length; i++)
			{
				KeyValuePair<long, GXUnitFormat> keyValuePair = array2[i];
				long key = keyValuePair.Key;
				GXUnitFormat value = keyValuePair.Value;
				key = (key << 8) | 2;
				if (value.CanPulse && !Formats.ContainsKey(key))
				{
					value = value.Pulselize();
					Formats.Add(key, value);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public GXTranslator(string filepath)
	{
		bin = new GXFileBinary(filepath);
		sbrenames = new Dictionary<string, string>();
	}

	public void Dispose()
	{
		bin?.Dispose();
		bin = null;
		sbrenames = null;
	}

	protected void InsertPulse(List<GXUnitModel> olds, List<GXUnitModel> news, int start, int count, ref GXUnitModel pre, bool ispulse)
	{
		if (!(pre?.Format is GXMStackFormat))
		{
			pre = new GXUnitModel(UF_MPS, new long[0], new int[0], new byte[0][]);
			news.Add(pre);
		}
		else if (pre?.Format is GXMPPFormat)
		{
			pre.Format = UF_MRD;
		}
		if (ispulse)
		{
			news.Add(new GXUnitModel(UF_MEP, new long[0], new int[0], new byte[0][]));
		}
		for (int i = start; i < start + count; i++)
		{
			news.Add(olds[i]);
		}
		pre = new GXUnitModel(UF_MPP, new long[0], new int[0], new byte[0][]);
		news.Add(pre);
	}

	protected void Pulse(List<GXUnitModel> olds, List<GXUnitModel> news, int start, int count)
	{
		GXUnitModel pre = news.LastOrDefault();
		int num = start;
		bool isPulse = olds[start].Format.IsPulse;
		if (pre.Format.Inst == IF_STL || pre.Format.Inst == IF_LBL)
		{
			pre = new GXUnitModel(UF_LD, new long[1] { 9492255L }, new int[1] { 3 }, new byte[0][]);
			news.Add(pre);
			if (start + count < olds.Count() && olds[start + count].Format.Inst == IF_MPS)
			{
				pre = new GXUnitModel(UF_MPS, new long[0], new int[0], new byte[0][]);
				news.Add(pre);
				olds[start + count].Format = UF_MRD;
			}
		}
		for (int i = start + 1; i < start + count; i++)
		{
			GXUnitModel gXUnitModel = olds[i];
			GXUnitFormat format = gXUnitModel.Format;
			if (format.IsPulse != isPulse)
			{
				InsertPulse(olds, news, num, i - num, ref pre, isPulse);
				num = i;
				isPulse = format.IsPulse;
			}
		}
		if (isPulse)
		{
			news.Add(new GXUnitModel(UF_MEP, new long[0], new int[0], new byte[0][]));
		}
		for (int j = num; j < start + count; j++)
		{
			news.Add(olds[j]);
		}
	}

	public unsafe GXReport Translate()
	{
		byte[] data = new byte[3] { 3, 52, 3 };
		StringBuilder stringBuilder = new StringBuilder();
		List<int> list = new List<int>();
		List<long> list2 = new List<long>();
		List<byte[]> list3 = new List<byte[]>();
		List<GXUnitModel> list4 = new List<GXUnitModel>();
		GXReport gXReport = new GXReport();
		sbrenames.Clear();
		if (!bin.ToFirst(new byte[2] { 32, 10 }))
		{
			return gXReport;
		}
		List<ushort> list5 = new List<ushort>();
		List<uint> list6 = new List<uint>();
		ushort num = bin.Read16();
		for (int i = 0; i < num; i++)
		{
			ushort item = bin.Read16();
			uint num2 = bin.Read32();
			uint num3 = bin.Read32();
			for (uint num4 = 0u; num4 < num3; num4++)
			{
				list5.Add(item);
				list6.Add(num2 + num4);
			}
		}
		for (int j = 0; j < list5.Count(); j++)
		{
			bin.Offset += 4;
			uint num5 = bin.Read32();
			if (num5 > 128)
			{
				break;
			}
			char[] array = new char[num5 - 1];
			for (int k = 0; k < num5 - 1; k++)
			{
				array[k] = (char)bin.Read16();
			}
			bin.Offset += 2;
			string text = new string(array);
			List<GXValueFormat> list7 = VF_ByHexs[(byte)list5[j]];
			foreach (GXValueFormat item6 in list7)
			{
				string text2 = $"{item6.Name}{((item6 is GXValueFormatXY) ? ((uint)ValueConverter.IntToDex((int)list6[j])) : list6[j])}";
				if (item6 == VF_P)
				{
					if (!sbrenames.ContainsKey(text2))
					{
						sbrenames.Add(text2, text);
					}
				}
				else
				{
					gXReport.Registers.Add(new GXRegister(text2, text));
				}
			}
		}
		if (bin.ToLast(data))
		{
			do
			{
				if (!bin.ToPrevUnit())
				{
					continue;
				}
				bin.Offset--;
				while (bin.ToPrevUnit())
				{
					bin.Offset--;
				}
				bin.Offset++;
				if (!bin.IsLegal())
				{
					break;
				}
				int offset = bin.Offset;
				int len = 0;
				int len2 = 0;
				long hex = 0L;
				long hex2 = 0L;
				GXUnitFormat value = null;
				while (true)
				{
					if (bin.Offset >= 491874)
					{
					}
					bool nextUnit = bin.GetNextUnit(ref hex, ref len);
					if (!nextUnit)
					{
						break;
					}
					bool flag = nextUnit && Formats.TryGetValue(hex, out value);
					byte[] array2 = (nextUnit ? null : bin.GetNextBytes());
					if (flag)
					{
						list.Clear();
						list2.Clear();
						list3.Clear();
						int num6 = 0;
						while (num6 < value.Args.Length)
						{
							bool nextUnit2 = bin.GetNextUnit(ref hex, ref len);
							byte[] array3 = ((!nextUnit2) ? bin.GetNextBytes() : null);
							bool flag2 = false;
							if (nextUnit2)
							{
								long num7 = (hex >> 8 * (len - 1)) & 0xFF;
								long num8 = num7;
								long num9 = num8 - 240;
								if ((ulong)num9 > 4uL)
								{
									goto IL_060b;
								}
								switch ((int)num9)
								{
								case 1:
									break;
								case 2:
									goto IL_04c7;
								case 4:
									goto IL_0535;
								case 0:
									goto IL_05a3;
								default:
									goto IL_060b;
								}
								if (bin.GetNextUnit(ref hex2, ref len2) && value.Args[num6].CheckCom(hex, len, hex2, len2))
								{
									list.Add(len);
									list2.Add(hex);
									list.Add(len2);
									list2.Add(hex2);
									flag2 = true;
								}
							}
							else if (array3 != null && array3.Length > 1)
							{
								if (value.Args[num6] is GXArgFormatSTR)
								{
									GXArgFormatSTR gXArgFormatSTR = (GXArgFormatSTR)value.Args[num6];
									if (gXArgFormatSTR.Check(array3))
									{
										flag2 = true;
									}
								}
								if (flag2)
								{
									list.Add(0);
									list2.Add(0L);
									list3.Add(array3);
								}
							}
							goto IL_06d0;
							IL_0535:
							if (bin.GetNextUnit(ref hex2, ref len2) && value.Args[num6].CheckV(hex, len, hex2, len2))
							{
								list.Add(len);
								list2.Add(hex);
								list.Add(len2);
								list2.Add(hex2);
								flag2 = true;
							}
							goto IL_06d0;
							IL_04c7:
							if (bin.GetNextUnit(ref hex2, ref len2) && value.Args[num6].CheckDot(hex, len, hex2, len2))
							{
								list.Add(len);
								list2.Add(hex);
								list.Add(len2);
								list2.Add(hex2);
								flag2 = true;
							}
							goto IL_06d0;
							IL_06d0:
							if (!flag2)
							{
								break;
							}
							num6++;
							continue;
							IL_060b:
							if (value.Args[num6].Check(hex, len))
							{
								list.Add(len);
								list2.Add(hex);
								if (array2 != null)
								{
									list3.Add(array3);
								}
								flag2 = true;
							}
							goto IL_06d0;
							IL_05a3:
							if (bin.GetNextUnit(ref hex2, ref len2) && value.Args[num6].CheckZ(hex, len, hex2, len2))
							{
								list.Add(len);
								list2.Add(hex);
								list.Add(len2);
								list2.Add(hex2);
								flag2 = true;
							}
							goto IL_06d0;
						}
						if (num6 >= value.Args.Length)
						{
							GXUnitModel item2 = new GXUnitModel(value, list2, list, list3);
							list4.Add(item2);
						}
					}
					else if (array2 != null && array2.Length > 2 && array2[0] == 128 && array2[1] == 0)
					{
						GXLineComment gXLineComment = new GXLineComment(UF_LineComment, new long[0], new int[0], new byte[0][]);
						fixed (byte* value2 = array2)
						{
							gXLineComment.Comment = new string((sbyte*)value2, 2, array2.Length - 2, Encoding.Default);
						}
						list4.Add(gXLineComment);
					}
					else if (array2 != null && array2.Length > 2 && array2[0] == 130 && array2[1] == 0)
					{
						GXOutputComment gXOutputComment = new GXOutputComment(UF_OutputComment, new long[0], new int[0], new byte[0][]);
						fixed (byte* value3 = array2)
						{
							gXOutputComment.Comment = new string((sbyte*)value3, 2, array2.Length - 2, Encoding.Default);
						}
						list4.Add(gXOutputComment);
					}
					else
					{
						TempDebugger.WriteLineWithDate($"Unknown len={len} hex=0x{hex:x}");
					}
				}
				bin.Offset = offset;
				if (list4.Count() > 0)
				{
					break;
				}
			}
			while (bin.ToPrev(data));
		}
		List<GXUnitModel> list8 = new List<GXUnitModel>();
		int num10 = -1;
		for (int l = 0; l < list4.Count(); l++)
		{
			GXUnitModel gXUnitModel = list4[l];
			GXUnitFormat format = gXUnitModel.Format;
			if (format.IsOutput && num10 < 0)
			{
				num10 = l;
			}
			if (!format.IsOutput && num10 >= 0)
			{
				Pulse(list4, list8, num10, l - num10);
				num10 = -1;
			}
			if (!format.IsOutput)
			{
				list8.Add(gXUnitModel);
			}
		}
		if (num10 >= 0)
		{
			Pulse(list4, list8, num10, list4.Count() - num10);
		}
		list4 = list8;
		GXLadder gXLadder = new GXLadder("Main");
		string item3 = string.Empty;
		bool flag3 = false;
		foreach (GXUnitModel item7 in list4)
		{
			if (item7.Format.Inst == IF_LBL && gXLadder == null)
			{
				gXLadder = new GXLadder(item7.Format.Args[0].ToString(item7.Hexs[0], item7.Lens[0]));
				if (sbrenames.ContainsKey(gXLadder.Name))
				{
					gXLadder.Name = sbrenames[gXLadder.Name];
				}
				item3 = string.Empty;
			}
			else
			{
				if (gXLadder == null)
				{
					continue;
				}
				if (item7 is GXLineComment)
				{
					GXLineComment gXLineComment2 = (GXLineComment)item7;
					if (stringBuilder.Length > 0)
					{
						gXLadder.Code.Add(stringBuilder.ToString());
						gXLadder.Brief.Add(item3);
					}
					stringBuilder.Clear();
					item3 = gXLineComment2.Comment;
					continue;
				}
				if (item7 is GXOutputComment)
				{
					GXOutputComment gXOutputComment2 = (GXOutputComment)item7;
					continue;
				}
				if (item7.Format.Inst == IF_FEND || item7.Format.Inst == IF_END || item7.Format.Inst == IF_SRET || (item7.Format.Inst == IF_RET && !flag3))
				{
					gXLadder.Code.Add(stringBuilder.ToString());
					gXLadder.Brief.Add(item3);
					gXReport.Ladders.Add(gXLadder);
					gXLadder = null;
					stringBuilder.Clear();
					continue;
				}
				if (item7.Format.Inst == IF_STL && flag3)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("STLE");
				}
				if (item7.Format.Inst == IF_STL)
				{
					flag3 = true;
				}
				if (item7.Format.Inst == IF_RET && flag3)
				{
					flag3 = false;
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				if (item7.Format.Inst == IF_CALL)
				{
					string key = item7.Format.Args[0].ToString(item7.Hexs[0], item7.Lens[0]);
					if (sbrenames.ContainsKey(key))
					{
						stringBuilder.Append("CALL " + sbrenames[key]);
						continue;
					}
				}
				item7.Append(stringBuilder);
			}
		}
		if (gXLadder != null)
		{
			if (stringBuilder.Length > 0)
			{
				gXLadder.Code.Add(stringBuilder.ToString());
				gXLadder.Brief.Add(item3);
			}
			gXReport.Ladders.Add(gXLadder);
		}
		for (int m = 0; m < bin.Data.Length - 7; m++)
		{
			if (bin.Data[m] == 0 || bin.Data[m + 1] != 0 || bin.Data[m + 2] != 0 || bin.Data[m + 3] != 0 || bin.Data[m + 4] != 0 || bin.Data[m + 5] != 0 || bin.Data[m + 6] != 0 || bin.Data[m + 7] != 0)
			{
				continue;
			}
			GXInitializeFormat gXInitializeFormat = IIF_ByHexs[bin.Data[m]];
			if (gXInitializeFormat == null)
			{
				continue;
			}
			bin.Offset = m + 8;
			uint ret = 0u;
			if (!bin.Read32In(1u, (uint)((bin.Data.Length - bin.Offset) / 12), ref ret))
			{
				continue;
			}
			int offset2 = bin.Offset;
			int offset3 = bin.Offset;
			for (int n = 0; n < ret; n++)
			{
				uint ret2 = 0u;
				uint ret3 = 0u;
				uint ret4 = 0u;
				uint ret5 = 0u;
				uint ret6 = 0u;
				uint ret7 = 0u;
				bool flag4 = true;
				if (flag4)
				{
					flag4 &= bin.Read32In(0u, 10000u, ref ret2);
					offset3 = bin.Offset;
				}
				if (flag4)
				{
					flag4 &= bin.Read32In(ret2, 10000u, ref ret3);
					offset3 = bin.Offset;
				}
				if (flag4)
				{
					flag4 &= bin.Read32In(0u, 1024u, ref ret4);
					offset3 = bin.Offset;
				}
				if (flag4)
				{
					flag4 &= bin.Read32In(0u, 16u, ref ret5);
					offset3 = bin.Offset;
				}
				if (flag4)
				{
					bin.Offset += (int)ret5;
					flag4 &= bin.Read32In(ret5, ret5, ref ret6);
					offset3 = bin.Offset;
				}
				if (flag4)
				{
					bin.Offset += (int)ret6;
					flag4 &= bin.Read32In(ret5, ret5, ref ret7);
					offset3 = bin.Offset;
				}
				if (flag4 && n < ret - 1)
				{
					bin.Offset += (int)(ret7 * 2);
					flag4 &= bin.Read32In(0u, 10000u, ref ret2);
					offset3 = bin.Offset;
				}
				if (!flag4 && offset2 >> 9 < offset3 >> 9)
				{
					flag4 = true;
					bin.Offset = offset2;
					bin.SkipStart = (uint)((offset2 >> 9 << 9) + 512);
					if (flag4)
					{
						flag4 &= bin.Read32In(0u, 10000u, ref ret2);
					}
					if (flag4)
					{
						flag4 &= bin.Read32In(ret2, 10000u, ref ret3);
					}
					if (flag4)
					{
						flag4 &= bin.Read32In(0u, 1024u, ref ret4);
					}
					if (flag4)
					{
						flag4 &= bin.Read32In(0u, 16u, ref ret5);
						bin.Offset += (int)ret5;
					}
					if (flag4)
					{
						flag4 &= bin.Read32In(ret5, ret5, ref ret6);
						bin.Offset += (int)ret6;
					}
					if (flag4)
					{
						flag4 &= bin.Read32In(ret5, ret5, ref ret7);
					}
				}
				if (!flag4)
				{
					bin.SkipStart = uint.MaxValue;
					break;
				}
				bin.Offset = offset2;
				ret2 = bin.Read32();
				ret3 = bin.Read32();
				ret4 = bin.Read32();
				ret5 = bin.Read32();
				byte[] array4 = bin.ReadBytes((int)ret5);
				ret6 = bin.Read32();
				byte[] array5 = bin.ReadBytes((int)ret6);
				ret7 = bin.Read32();
				for (int num11 = 0; num11 < ret7; num11 += (((array4[num11] & 0xF) == 2) ? 2 : (((array4[num11] & 0xF) != 3) ? 1 : 4)))
				{
					byte b = 1;
					byte b2 = array5[num11];
					ulong num12 = 0uL;
					switch (array4[num11] & 0xF)
					{
					case 2:
						b = 4;
						num12 = bin.Read32();
						break;
					case 3:
						b = 8;
						num12 = bin.Read64();
						break;
					default:
						b = (byte)((b2 == 98) ? 1u : 2u);
						num12 = bin.Read16();
						break;
					}
					uint num13 = (uint)(ret2 + num11);
					long num14 = ((num13 & 0xFF) << 24) | ((num13 & 0xFF00) << 8) | ((num13 & 0xFF0000) >> 8) | ((num13 & 0xFF000000u) >> 24);
					string name = gXInitializeFormat.Register.ToString(num14, 5);
					GXInitialize item4 = new GXInitialize(name, b, b2, num12);
					gXReport.Inits.Add(item4);
				}
				offset2 = bin.Offset;
				offset3 = bin.Offset;
			}
		}
		if (bin.ToLast(Head_MonitorWindow))
		{
			int offset4 = bin.Offset;
			int num15 = (bin.ToNext(Head_Option) ? bin.Offset : bin.Data.Length);
			int num16 = (num15 - offset4) / 2;
			char[] array6 = new char[num16];
			bin.Offset = offset4 + 1;
			for (int num17 = 0; num17 < num16; num17++)
			{
				array6[num17] = (char)bin.Read16();
			}
			string s = new string(array6);
			StringReader stringReader = new StringReader(s);
			GXMonitor gXMonitor = null;
			for (string text3 = stringReader.ReadLine(); text3 != null; text3 = stringReader.ReadLine())
			{
				text3 = text3.Trim();
				if (text3.Length != 0)
				{
					if (text3.Last() == ':')
					{
						gXMonitor = new GXMonitor(text3.Substring(0, text3.Length - 1));
						gXReport.Monitors.Add(gXMonitor);
					}
					else if (gXMonitor != null)
					{
						string[] array7 = text3.Split(new char[2] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
						if (array7.Length >= 3)
						{
							string text4 = array7[0];
							string text5 = array7[1];
							string text6 = array7[2];
							int num18 = -1;
							if (text4.Length != 0 && text5.Length != 0 && text6.Length != 0)
							{
								if (char.IsDigit(text4.Last()))
								{
									int num19 = text4.Length - 1;
									while (num19 > 0 && char.IsDigit(text4[num19]))
									{
										num19--;
									}
									num18 = int.Parse(text4.Substring(num19 + 1));
									text4 = text4.Substring(0, num19 + 1);
								}
								GXMonitorArgument item5 = new GXMonitorArgument(text4, text5, text6);
								if (num18 >= 0)
								{
									while (gXMonitor.Items.Count() <= num18)
									{
										gXMonitor.Items.Add(new GXMonitorItem(gXMonitor.Items.Count()));
									}
									gXMonitor.Items[num18].Args.Add(item5);
								}
								else
								{
									gXMonitor.Args.Add(item5);
								}
							}
						}
					}
				}
			}
		}
		return gXReport;
	}
}
