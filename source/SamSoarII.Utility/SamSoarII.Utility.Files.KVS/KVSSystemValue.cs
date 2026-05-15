using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSSystemValue
{
	public const ushort SYSCODE_ON = 2;

	public const ushort SYSCODE_OFF = 3;

	public const ushort SYSCODE_10MS = 4;

	public const ushort SYSCODE_100MS = 5;

	public const ushort SYSCODE_1S = 6;

	public const ushort SYSCODE_FIRST_ON = 7;

	public const ushort SYSCODE_FIRST_OFF = 8;

	public const ushort SYSCODE_CAL_NEG = 10;

	public const ushort SYSCODE_CAL_ZEO = 11;

	public const ushort SYSCODE_CAL_POS = 12;

	public const ushort SYSCODE_CAL_ERR = 13;

	public const ushort SYSCODE_CTH0_1US = 16;

	public const ushort SYSCODE_CTH0_10US = 17;

	public const ushort SYSCODE_CTH0_100US = 18;

	public const ushort SYSCODE_CTC0_AUTOCLEAR = 19;

	public const ushort SYSCODE_CTC0_DISABLE = 20;

	public const ushort SYSCODE_CTC0_ON = 21;

	public const ushort SYSCODE_CTC0_OFF = 22;

	public const ushort SYSCODE_CTC0_INV = 23;

	public const ushort SYSCODE_CTC1_DISABLE = 24;

	public const ushort SYSCODE_CTC1_ON = 25;

	public const ushort SYSCODE_CTC1_OFF = 26;

	public const ushort SYSCODE_CTC1_INV = 27;

	public const ushort SYSCODE_CTC1_AUTOCLEAR = 28;

	public const ushort SYSCODE_CTH0_FREQ_DOUBLE_1 = 29;

	public const ushort SYSCODE_CTH0_FREQ_DOUBLE_2 = 30;

	public const ushort SYSCODE_CTH1_1US = 32;

	public const ushort SYSCODE_CTH1_10US = 33;

	public const ushort SYSCODE_CTH1_100US = 34;

	public const ushort SYSCODE_CTC2_AUTOCLEAR = 35;

	public const ushort SYSCODE_CTC2_DISABLE = 36;

	public const ushort SYSCODE_CTC2_ON = 37;

	public const ushort SYSCODE_CTC2_OFF = 38;

	public const ushort SYSCODE_CTC2_INV = 39;

	public const ushort SYSCODE_CTC3_DISABLE = 40;

	public const ushort SYSCODE_CTC3_ON = 41;

	public const ushort SYSCODE_CTC3_OFF = 42;

	public const ushort SYSCODE_CTC3_INV = 43;

	public const ushort SYSCODE_CTC3_AUTOCLEAR = 44;

	public const ushort SYSCODE_CTH1_FREQ_DOUBLE_1 = 45;

	public const ushort SYSCODE_CTH1_FREQ_DOUBLE_2 = 46;

	public const ushort SYSCODE_DISABLE_OUTPUT = 48;

	public const ushort SYSCODE_DISABLE_INPUT = 49;

	public const ushort SYSCODE_CONST_PERIOD_ENABLE = 51;

	public const ushort SYSCODE_CONST_PERIOD_ERROR = 52;

	public const ushort SYSCODE_FREQ_COUNTER_START = 53;

	public const ushort SYSCODE_FREQ_PULSE_OUTPUT = 54;

	public const ushort SYSCODE_FREQ_PULSE_ERROR = 55;

	public const ushort SYSCODE_ELEC_DRIVER_STOP = 56;

	public const ushort SYSCODE_EMERGENCY_STOP = 57;

	public const ushort SYSCODE_ELEC_ACTION_START = 58;

	public const ushort SYSCODE_CAM_START = 62;

	public const ushort SYSCODE_CAM_ERROR = 63;

	public const ushort SYSCODE_CTH0_EXCONFIG_1 = 64;

	public const ushort SYSCODE_CTH0_EXCONFIG_2 = 65;

	public const ushort SYSCODE_INT0_POLE_1 = 66;

	public const ushort SYSCODE_INT0_POLE_2 = 67;

	public const ushort SYSCODE_INT1_POLE_1 = 68;

	public const ushort SYSCODE_INT1_POLE_2 = 69;

	public const ushort SYSCODE_CTH0_CYCLE = 70;

	public const ushort SYSCODE_CTH0_NO_B = 71;

	public const ushort SYSCODE_CTH1_EXCONFIG_1 = 72;

	public const ushort SYSCODE_CTH1_EXCONFIG_2 = 73;

	public const ushort SYSCODE_INT2_POLE_1 = 74;

	public const ushort SYSCODE_INT2_POLE_2 = 75;

	public const ushort SYSCODE_INT3_POLE_1 = 76;

	public const ushort SYSCODE_INT3_POLE_2 = 77;

	public const ushort SYSCODE_CTH1_CYCLE = 78;

	public const ushort SYSCODE_CTH1_NO_B = 79;

	public const ushort SYSCODE_D30_SW1 = 80;

	public const ushort SYSCODE_D30_SW2 = 81;

	public const ushort SYSCODE_D30_SW3 = 82;

	public const ushort SYSCODE_D30_SW4 = 83;

	public const ushort SYSCODE_D30_LP1 = 84;

	public const ushort SYSCODE_D30_LP2 = 85;

	public const ushort SYSCODE_D30_LP3 = 86;

	public const ushort SYSCODE_D30_LP4 = 87;

	public const ushort SYSCODE_D30_GREEN = 88;

	public const ushort SYSCODE_D30_RED = 89;

	public const ushort SYSCODE_D30_LANG = 90;

	public const ushort SYSCODE_D30_BEE = 91;

	public const ushort SYSCODE_D30_NP = 92;

	public const ushort SYSCODE_D30_RESET = 93;

	public const ushort SYSCODE_D30_USERINFO = 95;

	public const ushort SYSCODE_EX100_TIME = 105;

	public const ushort SYSCODE_EX200_TIME = 106;

	public const ushort SYSCODE_EX300_TIME = 107;

	public const ushort SYSCODE_EX400_TIME = 108;

	public const ushort SYSCODE_EXBREAK_CLEAR = 109;

	public const ushort SYSCODE_KL_ENABLE = 112;

	public const ushort SYSCODE_KL_VELOCITY = 113;

	public const ushort SYSCODE_KL_DATATYPE = 116;

	public const ushort SYSCODE_KL_FINAL_SETTING = 117;

	public const ushort SYSCODE_KL_BREAK_CLEAR = 118;

	public const ushort SYSCODE_KL_ERROR = 119;

	public const ushort SYSCODE_QL_ENABLE = 120;

	public const ushort SYSCODE_QL_VELOCITY = 121;

	public const ushort SYSCODE_QL_BREAK_CLEAR = 123;

	public const ushort SYSCODE_QL_ERROR = 124;

	public const ushort SYSCODE_HI_CHECK = 126;

	public const ushort SYSCODE_CAM_ACTING = 127;

	public const ushort SYSCODE_PORTA_OUTPUT = 128;

	public const ushort SYSCODE_RECEIVE = 129;

	public const ushort SYSCODE_ACQUIRE_ERROR = 130;

	public const ushort SYSCODE_RECEIVE_ERROR = 131;

	public const ushort SYSCODE_SEND_START = 132;

	public const ushort SYSCODE_BATTERY_ERROR = 140;

	public const ushort SYSCODE_UNIT_TIME = 141;

	public const ushort SYSCODE_HKEY_MULKEY_DISABLE = 142;

	public const ushort SYSCODE_HKEY_SCAN_END = 143;

	public const ushort SYSCODE_HKEY_CLEAR = 144;

	public const ushort SYSCODE_HKEY_ZONE = 145;

	public const ushort SYSCODE_PORTA_RECV_SIZE = 256;

	public const ushort SYSCODE_PORTA_RECV_DATA = 257;

	public const ushort SYSCODE_PORTA_SEND_SIZE = 258;

	public const ushort SYSCODE_PORTA_SEND_DATA = 259;

	public const ushort SYSCODE_PORTB_RECV_SIZE = 260;

	public const ushort SYSCODE_PORTB_RECV_DATA = 261;

	public const ushort SYSCODE_PORTB_SEND_SIZE = 262;

	public const ushort SYSCODE_PORTB_SEND_DATA = 263;

	public const ushort SYSCODE_CAM_OUTPUT_START = 265;

	public const ushort SYSCODE_CAM_COMPARER = 265;

	public const ushort SYSCODE_CAM_PLSNUM = 266;

	public const ushort SYSCODE_FREQ_MEASURE_PERIOD = 267;

	public const ushort SYSCODE_FREQ_MEASURE_RESULT = 268;

	public const ushort SYSCODE_CAM_DEGREE_ON = 269;

	public const ushort SYSCODE_CAM_DEGREE_OFF = 270;

	public const ushort SYSCODE_TZ_FREQ_START = 272;

	public const ushort SYSCODE_TZ_FREQ_RUN = 273;

	public const ushort SYSCODE_TZ_ACTIME = 274;

	public const ushort SYSCODE_TZ_PULSE_NUMBER = 275;

	public const ushort SYSCODE_TZ_ERROR_CODE = 276;

	public const ushort SYSCODE_QL_RECV_DATA = 288;

	public const ushort SYSCODE_QL_SEND_DATA = 289;

	public const ushort SYSCODE_QL_COMNUM = 290;

	public const ushort SYSCODE_QL_MESSAGE = 291;

	public const ushort SYSCODE_KL_RECV_ZONE = 292;

	public const ushort SYSCODE_KL_SEND_ZONE = 293;

	public const ushort SYSCODE_KL_SEND_ADDR = 294;

	public const ushort SYSCODE_KL_SEND_ADDR_NUM = 295;

	public const ushort SYSCODE_KL_RECV_ADDR = 296;

	public const ushort SYSCODE_KL_RECV_ADDR_NUM = 297;

	public const ushort SYSCODE_KL_ERROR_CODE = 298;

	public const ushort SYSCODE_KL_CONN_ADDR_NUM = 299;

	public const ushort SYSCODE_KL_CONN_MESSAGE = 300;

	public const ushort SYSCODE_CTH0_24BIT_READ = 304;

	public const ushort SYSCODE_CTH0_24BIT_WRITE = 305;

	public const ushort SYSCODE_CTH1_24BIT_READ = 306;

	public const ushort SYSCODE_CTH1_24BIT_WRITE = 307;

	public const ushort SYSCODE_CTC0_24BIT_READ = 308;

	public const ushort SYSCODE_CTC0_24BIT_WRITE = 309;

	public const ushort SYSCODE_CTC1_24BIT_READ = 310;

	public const ushort SYSCODE_CTC1_24BIT_WRITE = 311;

	public const ushort SYSCODE_CTC2_24BIT_READ = 312;

	public const ushort SYSCODE_CTC2_24BIT_WRITE = 313;

	public const ushort SYSCODE_CTC3_24BIT_READ = 314;

	public const ushort SYSCODE_CTC3_24BIT_WRITE = 315;

	public const ushort SYSCODE_CTH0_DEFAULT = 316;

	public const ushort SYSCODE_CTH1_DEFAULT = 317;

	public const ushort SYSCODE_INT0_INPUT_CATCH = 318;

	public const ushort SYSCODE_INT1_INPUT_CATCH = 319;

	public const ushort SYSCODE_INT2_INPUT_CATCH = 320;

	public const ushort SYSCODE_INT3_INPUT_CATCH = 321;

	public const ushort SYSCODE_FREQ_SETVALUE = 322;

	public const ushort SYSCODE_EXLINK_MESSAGE = 323;

	public const ushort SYSCODE_SPINNER_0_LIMIT = 324;

	public const ushort SYSCODE_SPINNER_1_LIMIT = 325;

	public const ushort SYSCODE_INPUT_TIME = 326;

	public const ushort SYSCODE_SPLIT_CONVERT = 327;

	public const ushort SYSCODE_USERINFO_ID = 328;

	public const ushort SYSCODE_PAGEX_LINEY = 336;

	public const ushort SYSCODE_PAGEX_LINEY_ELEM1 = 337;

	public const ushort SYSCODE_PAGEX_LINEY_ELEM2 = 338;

	public const ushort SYSCODE_D30_KL_CONVERT_ENABLE = 339;

	public const ushort SYSCODE_D30_DIRECT_VISIT = 340;

	public const ushort SYSCODE_CONST_PERIOD_EXCEED = 512;

	public const ushort SYSCODE_CPU_INPUT_CONST_TIME_SETTING = 513;

	public const ushort SYSCODE_SCAN_PERIOD_CLEAR = 514;

	public const ushort SYSCODE_END_PROCESS_TIME_SETTING = 515;

	public const ushort SYSCODE_DATE_TIMER_SETTING = 516;

	public const ushort SYSCODE_TIME_EXCEPTION = 517;

	public const ushort SYSCODE_VT_SENSOR_FORCE = 518;

	public const ushort SYSCODE_PROHIBIT_PASSWORD = 519;

	public const ushort SYSCODE_CONNECT_EXDEVICE = 520;

	public const ushort SYSCODE_CONNECT_EXUNIT = 521;

	public const ushort SYSCODE_ASCII_TRIMZERO = 528;

	public const ushort SYSCODE_ASCII_SKIPLABEL = 529;

	public const ushort SYSCODE_USERMSG1_SHOW = 530;

	public const ushort SYSCODE_USERMSG2_SHOW = 531;

	public const ushort SYSCODE_HAS_VISIT_WINDOW = 532;

	public const ushort SYSCODE_PROJECT_DOWNLOAD_REQUEST = 533;

	public const ushort SYSCODE_PROJECT_SAVE_REQUESET = 534;

	public const ushort SYSCODE_PROJECT_DOWNLOAD_COMPLETE = 535;

	public const ushort SYSCODE_PROJECT_DOWNLOAD_FAILED = 536;

	public const ushort SYSCODE_PROJECT_SAVE_COMPLETE = 537;

	public const ushort SYSCODE_PROJECT_SAVE_FAILED = 538;

	public const ushort SYSCODE_RECORDING = 544;

	public const ushort SYSCODE_CARD_OCCUPIED = 545;

	public const ushort SYSCODE_CARD_DETECTED = 546;

	public const ushort SYSCODE_CARD_EXISTED = 547;

	public const ushort SYSCODE_CARD_COMMANDING = 548;

	public const ushort SYSCODE_CARD_READONLY = 549;

	public const ushort SYSCODE_USER_WARNING = 560;

	public const ushort SYSCODE_WARNING_DETECT = 561;

	public const ushort SYSCODE_WARNLOG_SETTING = 562;

	public const ushort SYSCODE_WARNLOG_CLEAR = 563;

	public const ushort SYSCODE_D30_WARNINT_ENABLE = 576;

	public const ushort SYSCODE_D30_PAGE_CONVERT = 577;

	public const ushort SYSCODE_D30_PAGE_HANDLE_ENABLE = 578;

	public const ushort SYSCODE_D30_OUTPUT0_ON = 579;

	public const ushort SYSCODE_D30_OUTPUT1_ON = 580;

	public const ushort SYSCODE_D30_PAGE_CONVERT_SETTING = 581;

	public const ushort SYSCODE_D30_CURRENT_PAGE = 582;

	public const ushort SYSCODE_BIGERR_CLEAR = 592;

	public const ushort SYSCODE_ERROR_CLEAR = 593;

	public const ushort SYSCODE_BIGERR_MAX = 594;

	public const ushort SYSCODE_ERROR_MAX = 595;

	public const ushort SYSCODE_RUN_PROG = 596;

	public const ushort SYSCODE_BIGERRING = 597;

	public const ushort SYSCODE_ERRORING = 598;

	public const ushort SYSCODE_POWERON_CLEAR = 599;

	public const ushort SYSCODE_POWEROFF_CLEAR = 600;

	public const ushort SYSCODE_NOWERR_CLEAR = 601;

	public const ushort SYSCODE_CTH0_50NS = 608;

	public const ushort SYSCODE_CTH0_HAS = 609;

	public const ushort SYSCODE_CTH0_OVERFLOW = 610;

	public const ushort SYSCODE_CTH0_DIRECTION = 611;

	public const ushort SYSCODE_CTH0_OUTSET = 612;

	public const ushort SYSCODE_CTH0_MODE = 613;

	public const ushort SYSCODE_CTH0_DEFAULT_DISABLE = 614;

	public const ushort SYSCODE_CTH0_BITCOUNT_ENABLE = 615;

	public const ushort SYSCODE_CTH0_INPUT_MODE = 616;

	public const ushort SYSCODE_CTH1_50NS = 624;

	public const ushort SYSCODE_CTH1_HAS = 625;

	public const ushort SYSCODE_CTH1_OVERFLOW = 626;

	public const ushort SYSCODE_CTH1_DIRECTION = 627;

	public const ushort SYSCODE_CTH1_OUTSET = 628;

	public const ushort SYSCODE_CTH1_MODE = 629;

	public const ushort SYSCODE_CTH1_DEFAULT_DISABLE = 630;

	public const ushort SYSCODE_CTH1_BITCOUNT_ENABLE = 631;

	public const ushort SYSCODE_CTH1_INPUT_MODE = 632;

	public const ushort SYSCODE_CTH2_50NS = 640;

	public const ushort SYSCODE_CTH2_HAS = 641;

	public const ushort SYSCODE_CTH2_OVERFLOW = 642;

	public const ushort SYSCODE_CTH2_DIRECTION = 643;

	public const ushort SYSCODE_CTH2_OUTSET = 644;

	public const ushort SYSCODE_CTH2_MODE = 645;

	public const ushort SYSCODE_CTH2_DEFAULT_DISABLE = 646;

	public const ushort SYSCODE_CTH2_BITCOUNT_ENABLE = 647;

	public const ushort SYSCODE_CTH2_INPUT_MODE = 648;

	public const ushort SYSCODE_CTH2_1US = 649;

	public const ushort SYSCODE_CTH2_10US = 650;

	public const ushort SYSCODE_CTH2_100US = 651;

	public const ushort SYSCODE_CTH3_50NS = 656;

	public const ushort SYSCODE_CTH3_HAS = 657;

	public const ushort SYSCODE_CTH3_OVERFLOW = 658;

	public const ushort SYSCODE_CTH3_DIRECTION = 659;

	public const ushort SYSCODE_CTH3_OUTSET = 660;

	public const ushort SYSCODE_CTH3_MODE = 661;

	public const ushort SYSCODE_CTH3_DEFAULT_DISABLE = 662;

	public const ushort SYSCODE_CTH3_BITCOUNT_ENABLE = 663;

	public const ushort SYSCODE_CTH3_INPUT_MODE = 664;

	public const ushort SYSCODE_CTH3_1US = 665;

	public const ushort SYSCODE_CTH3_10US = 666;

	public const ushort SYSCODE_CTH3_100US = 667;

	public const ushort SYSCODE_CTC4_AUTOCLEAR = 672;

	public const ushort SYSCODE_CTC4_DISABLE = 673;

	public const ushort SYSCODE_CTC4_ON = 674;

	public const ushort SYSCODE_CTC4_OFF = 675;

	public const ushort SYSCODE_CTC4_INV = 676;

	public const ushort SYSCODE_CTC5_DISABLE = 677;

	public const ushort SYSCODE_CTC5_ON = 678;

	public const ushort SYSCODE_CTC5_OFF = 679;

	public const ushort SYSCODE_CTC5_INV = 680;

	public const ushort SYSCODE_CTC5_AUTOCLEAR = 681;

	public const ushort SYSCODE_CTC6_AUTOCLEAR = 688;

	public const ushort SYSCODE_CTC6_DISABLE = 689;

	public const ushort SYSCODE_CTC6_ON = 690;

	public const ushort SYSCODE_CTC6_OFF = 691;

	public const ushort SYSCODE_CTC6_INV = 692;

	public const ushort SYSCODE_CTC7_DISABLE = 693;

	public const ushort SYSCODE_CTC7_ON = 694;

	public const ushort SYSCODE_CTC7_OFF = 695;

	public const ushort SYSCODE_CTC7_INV = 696;

	public const ushort SYSCODE_CTC7_AUTOCLEAR = 697;

	public const ushort SYSCODE_CH0_RUNNING = 704;

	public const ushort SYSCODE_CH0_INPUT_SETTING = 705;

	public const ushort SYSCODE_CH0_HZ_RPM = 706;

	public const ushort SYSCODE_CH0_RPM = 707;

	public const ushort SYSCODE_CH0_SOURCE = 708;

	public const ushort SYSCODE_CH0_OUTPUTING = 709;

	public const ushort SYSCODE_CH0_OUTPUT_ERROR = 710;

	public const ushort SYSCODE_CH1_RUNNING = 720;

	public const ushort SYSCODE_CH1_INPUT_SETTING = 721;

	public const ushort SYSCODE_CH1_HZ_RPM = 722;

	public const ushort SYSCODE_CH1_RPM = 723;

	public const ushort SYSCODE_CH1_SOURCE = 724;

	public const ushort SYSCODE_CH1_OUTPUTING = 725;

	public const ushort SYSCODE_CH1_OUTPUT_ERROR = 726;

	public const ushort SYSCODE_CH2_RUNNING = 736;

	public const ushort SYSCODE_CH2_INPUT_SETTING = 737;

	public const ushort SYSCODE_CH2_HZ_RPM = 738;

	public const ushort SYSCODE_CH2_RPM = 739;

	public const ushort SYSCODE_CH2_SOURCE = 740;

	public const ushort SYSCODE_CH2_OUTPUTING = 741;

	public const ushort SYSCODE_CH2_OUTPUT_ERROR = 742;

	public const ushort SYSCODE_CH3_RUNNING = 752;

	public const ushort SYSCODE_CH3_INPUT_SETTING = 753;

	public const ushort SYSCODE_CH3_HZ_RPM = 754;

	public const ushort SYSCODE_CH3_RPM = 755;

	public const ushort SYSCODE_CH3_SOURCE = 756;

	public const ushort SYSCODE_CH3_OUTPUTING = 757;

	public const ushort SYSCODE_CH3_OUTPUT_ERROR = 758;

	public const ushort SYSCODE_AXI1_FORCE_STOP = 768;

	public const ushort SYSCODE_AXI1_DECELERATE = 769;

	public const ushort SYSCODE_AXI1_ERROR_CLAER = 770;

	public const ushort SYSCODE_AXI1_WARNING_CLEAR = 771;

	public const ushort SYSCODE_AXI1_MOVE_REQUEST = 772;

	public const ushort SYSCODE_AXI1_VELOCITY_REQUEST = 773;

	public const ushort SYSCODE_AXI1_TARGET_REQUEST = 774;

	public const ushort SYSCODE_AXI1_CLOCKWISE = 775;

	public const ushort SYSCODE_AXI1_CO_CLOCKWISE = 776;

	public const ushort SYSCODE_AXI1_ZERO_SENSOR = 777;

	public const ushort SYSCODE_AXI1_STOP_SENSOR = 778;

	public const ushort SYSCODE_AXI2_FORCE_STOP = 784;

	public const ushort SYSCODE_AXI2_DECELERATE = 785;

	public const ushort SYSCODE_AXI2_ERROR_CLAER = 786;

	public const ushort SYSCODE_AXI2_WARNING_CLEAR = 787;

	public const ushort SYSCODE_AXI2_MOVE_REQUEST = 788;

	public const ushort SYSCODE_AXI2_VELOCITY_REQUEST = 789;

	public const ushort SYSCODE_AXI2_TARGET_REQUEST = 790;

	public const ushort SYSCODE_AXI2_CLOCKWISE = 791;

	public const ushort SYSCODE_AXI2_CO_CLOCKWISE = 792;

	public const ushort SYSCODE_AXI2_ZERO_SENSOR = 793;

	public const ushort SYSCODE_AXI2_STOP_SENSOR = 794;

	public const ushort SYSCODE_AXI3_FORCE_STOP = 800;

	public const ushort SYSCODE_AXI3_DECELERATE = 801;

	public const ushort SYSCODE_AXI3_ERROR_CLAER = 802;

	public const ushort SYSCODE_AXI3_WARNING_CLEAR = 803;

	public const ushort SYSCODE_AXI3_MOVE_REQUEST = 804;

	public const ushort SYSCODE_AXI3_VELOCITY_REQUEST = 805;

	public const ushort SYSCODE_AXI3_TARGET_REQUEST = 806;

	public const ushort SYSCODE_AXI3_CLOCKWISE = 807;

	public const ushort SYSCODE_AXI3_CO_CLOCKWISE = 808;

	public const ushort SYSCODE_AXI3_ZERO_SENSOR = 809;

	public const ushort SYSCODE_AXI3_STOP_SENSOR = 810;

	public const ushort SYSCODE_AXI4_FORCE_STOP = 816;

	public const ushort SYSCODE_AXI4_DECELERATE = 817;

	public const ushort SYSCODE_AXI4_ERROR_CLAER = 818;

	public const ushort SYSCODE_AXI4_WARNING_CLEAR = 819;

	public const ushort SYSCODE_AXI4_MOVE_REQUEST = 820;

	public const ushort SYSCODE_AXI4_VELOCITY_REQUEST = 821;

	public const ushort SYSCODE_AXI4_TARGET_REQUEST = 822;

	public const ushort SYSCODE_AXI4_CLOCKWISE = 823;

	public const ushort SYSCODE_AXI4_CO_CLOCKWISE = 824;

	public const ushort SYSCODE_AXI4_ZERO_SENSOR = 825;

	public const ushort SYSCODE_AXI4_STOP_SENSOR = 826;

	public const ushort SYSCODE_AXI1_OUTPUTING = 832;

	public const ushort SYSCODE_AXI1_COMPLETE = 833;

	public const ushort SYSCODE_AXI1_ERROR = 834;

	public const ushort SYSCODE_AXI1_WARNING = 835;

	public const ushort SYSCODE_AXI1_RETURNING = 836;

	public const ushort SYSCODE_AXI1_RETURNED = 837;

	public const ushort SYSCODE_AXI1_EXIST = 838;

	public const ushort SYSCODE_AXI1_COMPARER = 839;

	public const ushort SYSCODE_AXI2_OUTPUTING = 848;

	public const ushort SYSCODE_AXI2_COMPLETE = 849;

	public const ushort SYSCODE_AXI2_ERROR = 850;

	public const ushort SYSCODE_AXI2_WARNING = 851;

	public const ushort SYSCODE_AXI2_RETURNING = 852;

	public const ushort SYSCODE_AXI2_RETURNED = 853;

	public const ushort SYSCODE_AXI2_EXIST = 854;

	public const ushort SYSCODE_AXI2_COMPARER = 855;

	public const ushort SYSCODE_AXI3_OUTPUTING = 864;

	public const ushort SYSCODE_AXI3_COMPLETE = 865;

	public const ushort SYSCODE_AXI3_ERROR = 866;

	public const ushort SYSCODE_AXI3_WARNING = 867;

	public const ushort SYSCODE_AXI3_RETURNING = 868;

	public const ushort SYSCODE_AXI3_RETURNED = 869;

	public const ushort SYSCODE_AXI3_EXIST = 870;

	public const ushort SYSCODE_AXI3_COMPARER = 871;

	public const ushort SYSCODE_AXI4_OUTPUTING = 880;

	public const ushort SYSCODE_AXI4_COMPLETE = 881;

	public const ushort SYSCODE_AXI4_ERROR = 882;

	public const ushort SYSCODE_AXI4_WARNING = 883;

	public const ushort SYSCODE_AXI4_RETURNING = 884;

	public const ushort SYSCODE_AXI4_RETURNED = 885;

	public const ushort SYSCODE_AXI4_EXIST = 886;

	public const ushort SYSCODE_AXI4_COMPARER = 887;

	public const ushort SYSCODE_LOG0_ENABLE = 896;

	public const ushort SYSCODE_LOG0_RUNNING = 897;

	public const ushort SYSCODE_LOG0_COMPLETE = 898;

	public const ushort SYSCODE_LOG0_50_WARNING = 899;

	public const ushort SYSCODE_LOG0_OVERFLOW = 900;

	public const ushort SYSCODE_LOG0_RUNWRITE = 901;

	public const ushort SYSCODE_LOG0_ERROR = 902;

	public const ushort SYSCODE_LOG0_NOMEMORY = 903;

	public const ushort SYSCODE_LOG0_EXCEPTION = 904;

	public const ushort SYSCODE_TRACE0_SAVEFILE = 905;

	public const ushort SYSCODE_TRACE0_COMPLETE = 906;

	public const ushort SYSCODE_LOG1_ENABLE = 912;

	public const ushort SYSCODE_LOG1_RUNNING = 913;

	public const ushort SYSCODE_LOG1_COMPLETE = 914;

	public const ushort SYSCODE_LOG1_50_WARNING = 915;

	public const ushort SYSCODE_LOG1_OVERFLOW = 916;

	public const ushort SYSCODE_LOG1_RUNWRITE = 917;

	public const ushort SYSCODE_LOG1_ERROR = 918;

	public const ushort SYSCODE_LOG1_NOMEMORY = 919;

	public const ushort SYSCODE_LOG1_EXCEPTION = 920;

	public const ushort SYSCODE_TRACE1_SAVEFILE = 921;

	public const ushort SYSCODE_TRACE1_COMPLETE = 922;

	public const ushort SYSCODE_LOG2_ENABLE = 928;

	public const ushort SYSCODE_LOG2_RUNNING = 929;

	public const ushort SYSCODE_LOG2_COMPLETE = 930;

	public const ushort SYSCODE_LOG2_50_WARNING = 931;

	public const ushort SYSCODE_LOG2_OVERFLOW = 932;

	public const ushort SYSCODE_LOG2_RUNWRITE = 933;

	public const ushort SYSCODE_LOG2_ERROR = 934;

	public const ushort SYSCODE_LOG2_NOMEMORY = 935;

	public const ushort SYSCODE_LOG2_EXCEPTION = 936;

	public const ushort SYSCODE_TRACE2_SAVEFILE = 937;

	public const ushort SYSCODE_TRACE2_COMPLETE = 938;

	public const ushort SYSCODE_LOG3_ENABLE = 944;

	public const ushort SYSCODE_LOG3_RUNNING = 945;

	public const ushort SYSCODE_LOG3_COMPLETE = 946;

	public const ushort SYSCODE_LOG3_50_WARNING = 947;

	public const ushort SYSCODE_LOG3_OVERFLOW = 948;

	public const ushort SYSCODE_LOG3_RUNWRITE = 949;

	public const ushort SYSCODE_LOG3_ERROR = 950;

	public const ushort SYSCODE_LOG3_NOMEMORY = 951;

	public const ushort SYSCODE_LOG3_EXCEPTION = 952;

	public const ushort SYSCODE_TRACE3_SAVEFILE = 953;

	public const ushort SYSCODE_TRACE3_COMPLETE = 954;

	public const ushort SYSCODE_LOG4_ENABLE = 960;

	public const ushort SYSCODE_LOG4_RUNNING = 961;

	public const ushort SYSCODE_LOG4_COMPLETE = 962;

	public const ushort SYSCODE_LOG4_50_WARNING = 963;

	public const ushort SYSCODE_LOG4_OVERFLOW = 964;

	public const ushort SYSCODE_LOG4_RUNWRITE = 965;

	public const ushort SYSCODE_LOG4_ERROR = 966;

	public const ushort SYSCODE_LOG4_NOMEMORY = 967;

	public const ushort SYSCODE_LOG4_EXCEPTION = 968;

	public const ushort SYSCODE_TRACE4_SAVEFILE = 969;

	public const ushort SYSCODE_TRACE4_COMPLETE = 970;

	public const ushort SYSCODE_LOG5_ENABLE = 976;

	public const ushort SYSCODE_LOG5_RUNNING = 977;

	public const ushort SYSCODE_LOG5_COMPLETE = 978;

	public const ushort SYSCODE_LOG5_50_WARNING = 979;

	public const ushort SYSCODE_LOG5_OVERFLOW = 980;

	public const ushort SYSCODE_LOG5_RUNWRITE = 981;

	public const ushort SYSCODE_LOG5_ERROR = 982;

	public const ushort SYSCODE_LOG5_NOMEMORY = 983;

	public const ushort SYSCODE_LOG5_EXCEPTION = 984;

	public const ushort SYSCODE_TRACE5_SAVEFILE = 985;

	public const ushort SYSCODE_TRACE5_COMPLETE = 986;

	public const ushort SYSCODE_LOG6_ENABLE = 992;

	public const ushort SYSCODE_LOG6_RUNNING = 993;

	public const ushort SYSCODE_LOG6_COMPLETE = 994;

	public const ushort SYSCODE_LOG6_50_WARNING = 995;

	public const ushort SYSCODE_LOG6_OVERFLOW = 996;

	public const ushort SYSCODE_LOG6_RUNWRITE = 997;

	public const ushort SYSCODE_LOG6_ERROR = 998;

	public const ushort SYSCODE_LOG6_NOMEMORY = 999;

	public const ushort SYSCODE_LOG6_EXCEPTION = 1000;

	public const ushort SYSCODE_TRACE6_SAVEFILE = 1001;

	public const ushort SYSCODE_TRACE6_COMPLETE = 1002;

	public const ushort SYSCODE_LOG7_ENABLE = 1008;

	public const ushort SYSCODE_LOG7_RUNNING = 1009;

	public const ushort SYSCODE_LOG7_COMPLETE = 1010;

	public const ushort SYSCODE_LOG7_50_WARNING = 1011;

	public const ushort SYSCODE_LOG7_OVERFLOW = 1012;

	public const ushort SYSCODE_LOG7_RUNWRITE = 1013;

	public const ushort SYSCODE_LOG7_ERROR = 1014;

	public const ushort SYSCODE_LOG7_NOMEMORY = 1015;

	public const ushort SYSCODE_LOG7_EXCEPTION = 1016;

	public const ushort SYSCODE_TRACE7_SAVEFILE = 1017;

	public const ushort SYSCODE_TRACE7_COMPLETE = 1018;

	public const ushort SYSCODE_LOG8_ENABLE = 1024;

	public const ushort SYSCODE_LOG8_RUNNING = 1025;

	public const ushort SYSCODE_LOG8_COMPLETE = 1026;

	public const ushort SYSCODE_LOG8_50_WARNING = 1027;

	public const ushort SYSCODE_LOG8_OVERFLOW = 1028;

	public const ushort SYSCODE_LOG8_RUNWRITE = 1029;

	public const ushort SYSCODE_LOG8_ERROR = 1030;

	public const ushort SYSCODE_LOG8_NOMEMORY = 1031;

	public const ushort SYSCODE_LOG8_EXCEPTION = 1032;

	public const ushort SYSCODE_TRACE8_SAVEFILE = 1033;

	public const ushort SYSCODE_TRACE8_COMPLETE = 1034;

	public const ushort SYSCODE_LOG9_ENABLE = 1040;

	public const ushort SYSCODE_LOG9_RUNNING = 1041;

	public const ushort SYSCODE_LOG9_COMPLETE = 1042;

	public const ushort SYSCODE_LOG9_50_WARNING = 1043;

	public const ushort SYSCODE_LOG9_OVERFLOW = 1044;

	public const ushort SYSCODE_LOG9_RUNWRITE = 1045;

	public const ushort SYSCODE_LOG9_ERROR = 1046;

	public const ushort SYSCODE_LOG9_NOMEMORY = 1047;

	public const ushort SYSCODE_LOG9_EXCEPTION = 1048;

	public const ushort SYSCODE_TRACE9_SAVEFILE = 1049;

	public const ushort SYSCODE_TRACE9_COMPLETE = 1050;

	public const ushort SYSCODE_DATE_YEAR = 1056;

	public const ushort SYSCODE_DATE_MONTH = 1057;

	public const ushort SYSCODE_DATE_DAY = 1058;

	public const ushort SYSCODE_DATE_HOUR = 1059;

	public const ushort SYSCODE_DATE_MINUTE = 1060;

	public const ushort SYSCODE_DATE_SECOND = 1061;

	public const ushort SYSCODE_DATE_WEEK = 1062;

	public const ushort SYSCODE_VOID_COUNTER = 1063;

	public const ushort SYSCODE_CPU_VERSION = 1072;

	public const ushort SYSCODE_SCAN_MEASURE = 1088;

	public const ushort SYSCODE_SCAN_SET_TIME = 1089;

	public const ushort SYSCODE_SCAN_EXCEED_TIME = 1090;

	public const ushort SYSCODE_END_MEASURE = 1091;

	public const ushort SYSCODE_SCAN_MINIMUM = 1092;

	public const ushort SYSCODE_SCAN_MAXIMUM = 1093;

	public const ushort SYSCODE_END_SET_TIME = 1094;

	public const ushort SYSCODE_END_EXCEED_TIME = 1095;

	public const ushort SYSCODE_VOLUME0 = 1104;

	public const ushort SYSCODE_VOLUME1 = 1105;

	public const ushort SYSCODE_SENSOR_COMMAND_NUMBER = 1106;

	public const ushort SYSCODE_PROJECT_PASSWORD_RETRY_SEQUENCE = 1107;

	public const ushort SYSCODE_PROJECT_PASSWORD_RETRY_TOTAL = 1108;

	public const ushort SYSCODE_LOG0_REMAIN_MEMORY = 1109;

	public const ushort SYSCODE_LOG1_REMAIN_MEMORY = 1110;

	public const ushort SYSCODE_LOG2_REMAIN_MEMORY = 1111;

	public const ushort SYSCODE_LOG3_REMAIN_MEMORY = 1112;

	public const ushort SYSCODE_LOG4_REMAIN_MEMORY = 1113;

	public const ushort SYSCODE_LOG5_REMAIN_MEMORY = 1114;

	public const ushort SYSCODE_LOG6_REMAIN_MEMORY = 1115;

	public const ushort SYSCODE_LOG7_REMAIN_MEMORY = 1116;

	public const ushort SYSCODE_LOG8_REMAIN_MEMORY = 1117;

	public const ushort SYSCODE_LOG9_REMAIN_MEMORY = 1118;

	public const ushort SYSCODE_LOG0_FILE_COUNT = 1119;

	public const ushort SYSCODE_LOG1_FILE_COUNT = 1120;

	public const ushort SYSCODE_LOG2_FILE_COUNT = 1121;

	public const ushort SYSCODE_LOG3_FILE_COUNT = 1122;

	public const ushort SYSCODE_LOG4_FILE_COUNT = 1123;

	public const ushort SYSCODE_LOG5_FILE_COUNT = 1124;

	public const ushort SYSCODE_LOG6_FILE_COUNT = 1125;

	public const ushort SYSCODE_LOG7_FILE_COUNT = 1126;

	public const ushort SYSCODE_LOG8_FILE_COUNT = 1127;

	public const ushort SYSCODE_LOG9_FILE_COUNT = 1128;

	public const ushort SYSCODE_AUTOLOAD_DIRECTORY_ID = 1136;

	public const ushort SYSCODE_RUNLOAD_DIRECTORY_ID = 1137;

	public const ushort SYSCODE_DOWNLOAD_DIRECTORY_ID = 1138;

	public const ushort SYSCODE_SAVE_DIRECTORY_ID = 1139;

	public const ushort SYSCODE_AUTOLOAD_COMPLETE_CODE = 1140;

	public const ushort SYSCODE_AUTOLOAD_DIRECTORY_ID_FIN = 1141;

	public const ushort SYSCODE_RUNLOAD_COMPLETE_CODE = 1142;

	public const ushort SYSCODE_RUNLOAD_DIRECTORY_ID_FIN = 1143;

	public const ushort SYSCODE_DOWNLOAD_COMPLETE_CODE = 1144;

	public const ushort SYSCODE_DOWNLOAD_DIRECTORY_ID_FIN = 1145;

	public const ushort SYSCODE_SAVE_COMPLETE_CODE = 1146;

	public const ushort SYSCODE_SAVE_DIRECTORY_ID_FIN = 1147;

	public const ushort SYSCODE_USERMSG_1 = 1152;

	public const ushort SYSCODE_USERMSG_2 = 1153;

	public const ushort SYSCODE_VISIT_WINDOW_DISABLE_SETTING = 1154;

	public const ushort SYSCODE_PROJECT_NAME = 1155;

	public const ushort SYSCODE_VISIT_WINDOW_SHOW_NAME = 1156;

	public const ushort SYSCODE_AW_EFFECTIVE = 1168;

	public const ushort SYSCODE_AW_ELEMENT_TYPE = 1169;

	public const ushort SYSCODE_AW_SCREEN_ID = 1170;

	public const ushort SYSCODE_AW_ELEMENT_ID = 1171;

	public const ushort SYSCODE_AW_SHOW_FORMAT = 1172;

	public const ushort SYSCODE_AW_ELEMENT_NAME = 1173;

	public const ushort SYSCODE_AW_KEY_LOCK = 1174;

	public const ushort SYSCODE_AW_LANGUAGE = 1175;

	public const ushort SYSCODE_NCE_YEAR_MONTH = 1184;

	public const ushort SYSCODE_NCE_DAY = 1185;

	public const ushort SYSCODE_NCE_HOUR = 1186;

	public const ushort SYSCODE_NCE_MINUTE = 1187;

	public const ushort SYSCODE_NCE_SECOND = 1188;

	public const ushort SYSCODE_NCE_ID = 1189;

	public const ushort SYSCODE_NCE_DATA_NUMBER = 1190;

	public const ushort SYSCODE_NCE_DETAILS = 1191;

	public const ushort SYSCODE_NNE_YEAR_MONTH = 1192;

	public const ushort SYSCODE_NNE_DAY = 1193;

	public const ushort SYSCODE_NNE_HOUR = 1194;

	public const ushort SYSCODE_NNE_MINUTE = 1195;

	public const ushort SYSCODE_NNE_SECOND = 1196;

	public const ushort SYSCODE_NNE_ID = 1197;

	public const ushort SYSCODE_NNE_DATA_NUMBER = 1198;

	public const ushort SYSCODE_NNE_DETAILS = 1199;

	public const ushort SYSCODE_EXUNIT_NUMBER = 1200;

	public const ushort SYSCODE_CARD_ERROR_CODE = 1201;

	public const ushort SYSCODE_CARD_POWER_OFF = 1202;

	public const ushort SYSCODE_CARD_RETRY_COUNT = 1203;

	public const ushort SYSCODE_CARD_CHECK_ERROR = 1204;

	public const ushort SYSCODE_CARD_OVERTIME_COUNT = 1205;

	public const ushort SYSCODE_CARD_ERROR_RESET_DELAY = 1206;

	public const ushort SYSCODE_INPUT0_INT = 1216;

	public const ushort SYSCODE_INPUT1_INT = 1217;

	public const ushort SYSCODE_INPUT2_INT = 1218;

	public const ushort SYSCODE_INPUT3_INT = 1219;

	public const ushort SYSCODE_CTC0_INT = 1220;

	public const ushort SYSCODE_CTC1_INT = 1221;

	public const ushort SYSCODE_CTC2_INT = 1222;

	public const ushort SYSCODE_CTC3_INT = 1223;

	public const ushort SYSCODE_CTC4_INT = 1224;

	public const ushort SYSCODE_CTC5_INT = 1225;

	public const ushort SYSCODE_CTC6_INT = 1226;

	public const ushort SYSCODE_CTC7_INT = 1227;

	public const ushort SYSCODE_AXI1_INT = 1228;

	public const ushort SYSCODE_AXI2_INT = 1229;

	public const ushort SYSCODE_AXI3_INT = 1230;

	public const ushort SYSCODE_AXI4_INT = 1231;

	public const ushort SYSCODE_CTH0_DIRECTION_MEASURE_TIME = 1232;

	public const ushort SYSCODE_CTH0_RESET_SETTING = 1233;

	public const ushort SYSCODE_CTH0_CYCLE_MININUM = 1234;

	public const ushort SYSCODE_CTH0_CYCLE_MAXINUM = 1235;

	public const ushort SYSCODE_CTH1_DIRECTION_MEASURE_TIME = 1236;

	public const ushort SYSCODE_CTH1_RESET_SETTING = 1237;

	public const ushort SYSCODE_CTH1_CYCLE_MININUM = 1238;

	public const ushort SYSCODE_CTH1_CYCLE_MAXINUM = 1239;

	public const ushort SYSCODE_CTH2_DIRECTION_MEASURE_TIME = 1240;

	public const ushort SYSCODE_CTH2_RESET_SETTING = 1241;

	public const ushort SYSCODE_CTH2_CYCLE_MININUM = 1242;

	public const ushort SYSCODE_CTH2_CYCLE_MAXINUM = 1243;

	public const ushort SYSCODE_CTH3_DIRECTION_MEASURE_TIME = 1244;

	public const ushort SYSCODE_CTH3_RESET_SETTING = 1245;

	public const ushort SYSCODE_CTH3_CYCLE_MININUM = 1246;

	public const ushort SYSCODE_CTH3_CYCLE_MAXINUM = 1247;

	public const ushort SYSCODE_CTH2_DEFAULT = 1264;

	public const ushort SYSCODE_CTH3_DEFAULT = 1265;

	public const ushort SYSCODE_CH0_RESULT = 1280;

	public const ushort SYSCODE_CH0_PERIOD = 1281;

	public const ushort SYSCODE_CH0_AVERAGE_TICK = 1282;

	public const ushort SYSCODE_CH0_EXSOURCE = 1283;

	public const ushort SYSCODE_CH0_FREQUENCY = 1284;

	public const ushort SYSCODE_CH0_DUTY_RATE = 1285;

	public const ushort SYSCODE_CH1_RESULT = 1296;

	public const ushort SYSCODE_CH1_PERIOD = 1297;

	public const ushort SYSCODE_CH1_AVERAGE_TICK = 1298;

	public const ushort SYSCODE_CH1_EXSOURCE = 1299;

	public const ushort SYSCODE_CH1_FREQUENCY = 1300;

	public const ushort SYSCODE_CH1_DUTY_RATE = 1301;

	public const ushort SYSCODE_CH2_RESULT = 1312;

	public const ushort SYSCODE_CH2_PERIOD = 1313;

	public const ushort SYSCODE_CH2_AVERAGE_TICK = 1314;

	public const ushort SYSCODE_CH2_EXSOURCE = 1315;

	public const ushort SYSCODE_CH2_FREQUENCY = 1316;

	public const ushort SYSCODE_CH2_DUTY_RATE = 1317;

	public const ushort SYSCODE_CH3_RESULT = 1328;

	public const ushort SYSCODE_CH3_PERIOD = 1329;

	public const ushort SYSCODE_CH3_AVERAGE_TICK = 1330;

	public const ushort SYSCODE_CH3_EXSOURCE = 1331;

	public const ushort SYSCODE_CH3_FREQUENCY = 1332;

	public const ushort SYSCODE_CH3_DUTY_RATE = 1333;

	public const ushort SYSCODE_AXI_PT_TARGET = 1344;

	public const ushort SYSCODE_AXI_PT_ACCELERATE = 1345;

	public const ushort SYSCODE_AXI_PT_DECELERATE = 1346;

	public const ushort SYSCODE_AXI_PT_VELOCITY = 1347;

	public const ushort SYSCODE_AXI_PT_ACTION_MODE = 1348;

	public const ushort SYSCODE_AXI_PT_MOVE_AFTER_STOP = 1349;

	public const ushort SYSCODE_AXI_IO_SETTING = 1350;

	public const ushort SYSCODE_AXI_SENSOR_ENABLE = 1351;

	public const ushort SYSCODE_AXI_COMPARER = 1352;

	public const ushort SYSCODE_AXI_START_VELOCITY = 1353;

	public const ushort SYSCODE_AXI_RETURN_START_VELOCITY = 1354;

	public const ushort SYSCODE_AXI_RETURN_ACCELERATE = 1355;

	public const ushort SYSCODE_AXI_RETURN_DECELERATE = 1356;

	public const ushort SYSCODE_AXI_RETURN_VELOCITY = 1357;

	public const ushort SYSCODE_AXI_RETURN_DETAIL = 1358;

	public const ushort SYSCODE_AXI_JOG_START_VELOCITY = 1359;

	public const ushort SYSCODE_AXI_JOG_ACCELERATE = 1360;

	public const ushort SYSCODE_AXI_JOG_DECELERATE = 1361;

	public const ushort SYSCODE_AXI_JOG_VELOCITY = 1362;

	public const ushort SYSCODE_AXI_START_POS_SET = 1363;

	public const ushort SYSCODE_AXI_NOW_POS_SET = 1364;

	public const ushort SYSCODE_AXI_VELOCITY_SET = 1365;

	public const ushort SYSCODE_AXI_DEST_POS_SET = 1366;

	public const ushort SYSCODE_AXI_NOW_POSITION = 1367;

	public const ushort SYSCODE_AXI_NOW_VELOCITY = 1368;

	public const ushort SYSCODE_AXI_ERROR_CODE = 1369;

	public const ushort SYSCODE_AXI_POINT_ID = 1370;

	public static readonly List<KVSSystemValue> List_PD_R;

	public static readonly List<KVSSystemValue> List_D_R;

	public static readonly List<KVSSystemValue> List_PD_DM;

	public static readonly List<KVSSystemValue> List_D_DM;

	public static readonly List<KVSSystemValue> List_N_CR;

	public static readonly List<KVSSystemValue> List_N_CM;

	private ushort syscode;

	private ushort sysid;

	private ushort code;

	private uint offset;

	private string description;

	public ushort SysCode
	{
		get
		{
			return syscode;
		}
		set
		{
			syscode = value;
		}
	}

	public ushort SysID
	{
		get
		{
			return sysid;
		}
		set
		{
			sysid = value;
		}
	}

	public ushort Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	public uint Offset
	{
		get
		{
			return offset;
		}
		set
		{
			offset = value;
		}
	}

	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}

	static KVSSystemValue()
	{
		List_PD_R = new List<KVSSystemValue>();
		List_D_R = new List<KVSSystemValue>();
		List_PD_DM = new List<KVSSystemValue>();
		List_D_DM = new List<KVSSystemValue>();
		List_N_CR = new List<KVSSystemValue>();
		List_N_CM = new List<KVSSystemValue>();
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 322u,
			SysCode = 2,
			Description = "Constant ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 323u,
			SysCode = 3,
			Description = "Always OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 324u,
			SysCode = 4,
			Description = "10ms clock pulse"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 325u,
			SysCode = 5,
			Description = "100ms clock pulse"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 326u,
			SysCode = 6,
			Description = "1-second clock pulse"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 327u,
			SysCode = 7,
			Description = "The operation starts with a scan ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 328u,
			SysCode = 8,
			Description = "The operation starts with a scan OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 329u,
			SysCode = 10,
			Description = "运算结果is负数"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 330u,
			SysCode = 11,
			Description = "运算结果is零"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 331u,
			SysCode = 12,
			Description = "运算结果is正数"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 332u,
			SysCode = 13,
			Description = "Calculation error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 336u,
			SysCode = 16,
			Description = "CTH0 has an internal 1us clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 337u,
			SysCode = 17,
			Description = "CTH0 has an internal 10us clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 338u,
			SysCode = 18,
			Description = "CTH0 has a 100us internal clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 339u,
			SysCode = 19,
			Description = "CTC0 is automatically cleared"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 340u,
			SysCode = 20,
			Description = "CTC0 output energy removal"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 341u,
			SysCode = 21,
			Description = "CTC0 outputs ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 342u,
			SysCode = 22,
			Description = "CTC0 outputs OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 343u,
			SysCode = 23,
			Description = "The output of CTC0 is inverted"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 344u,
			SysCode = 24,
			Description = "Output the energy of CTC1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 345u,
			SysCode = 25,
			Description = "CTC1 outputs ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 346u,
			SysCode = 26,
			Description = "CTC1 outputs OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 347u,
			SysCode = 27,
			Description = "The output of CTC1 is inverted"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 348u,
			SysCode = 28,
			Description = "CTC1 is automatically cleared"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 349u,
			SysCode = 29,
			Description = "CTH0 multiplier setting 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 350u,
			SysCode = 30,
			Description = "CTH0 multiplier setting 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 352u,
			SysCode = 32,
			Description = "CTH1 has an internal 1us clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 353u,
			SysCode = 33,
			Description = "The CTH1 has an internal 10us clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 354u,
			SysCode = 34,
			Description = "The CTH1 has a 100us internal clock"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 355u,
			SysCode = 35,
			Description = "CTC2 is automatically cleared"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 356u,
			SysCode = 36,
			Description = "CTC2 output energy removal"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 357u,
			SysCode = 37,
			Description = "CTC2 outputs ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 358u,
			SysCode = 38,
			Description = "CTC2 outputs OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 359u,
			SysCode = 39,
			Description = "The output of CTC2 is inverted"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 360u,
			SysCode = 40,
			Description = "CTC3 output energy removal"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 361u,
			SysCode = 41,
			Description = "CTC3 outputs ON"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 362u,
			SysCode = 42,
			Description = "CTC3 outputs OFF"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 363u,
			SysCode = 43,
			Description = "The output of CTC3 is inverted"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 364u,
			SysCode = 44,
			Description = "CTC3 is automatically cleared"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 365u,
			SysCode = 45,
			Description = "CTH1 multiplier setting 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 366u,
			SysCode = 46,
			Description = "CTH1 multiplier setting 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 368u,
			SysCode = 48,
			Description = "External output energy"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 369u,
			SysCode = 49,
			Description = "Input refresh function"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 371u,
			SysCode = 51,
			Description = "Constant scanning time enable"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 372u,
			SysCode = 52,
			Description = "Constant scan time error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 373u,
			SysCode = 53,
			Description = "The frequency counter begins"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 374u,
			SysCode = 54,
			Description = "Frequency pulse output"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 375u,
			SysCode = 55,
			Description = "Frequency pulse error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 376u,
			SysCode = 56,
			Description = "Motor-driven stop"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 377u,
			SysCode = 57,
			Description = "Emergency stop"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 378u,
			SysCode = 58,
			Description = "The motor action begins."
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 382u,
			SysCode = 62,
			Description = "CAM begins"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 383u,
			SysCode = 63,
			Description = "CAM error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 384u,
			SysCode = 64,
			Description = "CTH0 external Settings 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 385u,
			SysCode = 65,
			Description = "CTH0 External Settings 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 386u,
			SysCode = 66,
			Description = "INT0 interrupts polarity 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 387u,
			SysCode = 67,
			Description = "INT0 interrupts polarity 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 388u,
			SysCode = 68,
			Description = "INT1 interrupts polarity 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 389u,
			SysCode = 69,
			Description = "INT1 interrupts polarity 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 390u,
			SysCode = 70,
			Description = "CTH0 circular counter"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 391u,
			SysCode = 71,
			Description = "Phase CTH0 B is lost"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 392u,
			SysCode = 72,
			Description = "CTH1 External Settings 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 393u,
			SysCode = 73,
			Description = "CTH1 External Settings 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 394u,
			SysCode = 74,
			Description = "INT2 interrupts polarity 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 395u,
			SysCode = 75,
			Description = "INT2 interrupts polarity 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 396u,
			SysCode = 76,
			Description = "INT3 interrupts polarity 1"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 397u,
			SysCode = 77,
			Description = "INT3 interrupts polarity 2"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 398u,
			SysCode = 78,
			Description = "CTH1 circular counter"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 399u,
			SysCode = 79,
			Description = "Phase B of CTH1 is lost"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 425u,
			SysCode = 105,
			Description = "Extend the time constant by 100"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 426u,
			SysCode = 106,
			Description = "Extend the time constant by 200"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 427u,
			SysCode = 107,
			Description = "Extend the time constant by 300"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 428u,
			SysCode = 108,
			Description = "Extend the 400 time constant"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 429u,
			SysCode = 109,
			Description = "Expand the clearance of disconnected input"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 432u,
			SysCode = 112,
			Description = "KL is enabled"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 433u,
			SysCode = 113,
			Description = "KL communication speed"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 436u,
			SysCode = 116,
			Description = "KL communication data type"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 437u,
			SysCode = 117,
			Description = "The final setting of KL"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 438u,
			SysCode = 118,
			Description = "KL disconnection cleared"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 439u,
			SysCode = 119,
			Description = "KL setting error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 440u,
			SysCode = 120,
			Description = "QL uses enablement"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 441u,
			SysCode = 121,
			Description = "QL communication speed"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 443u,
			SysCode = 123,
			Description = "QL disconnection clear"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 444u,
			SysCode = 124,
			Description = "QL disconnection error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 446u,
			SysCode = 126,
			Description = "For high-speed input correction use"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 447u,
			SysCode = 127,
			Description = "The CAM switch is in operation"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 448u,
			SysCode = 128,
			Description = "PORTA interrupts signal output"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 449u,
			SysCode = 129,
			Description = "PORTA data reception"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 450u,
			SysCode = 130,
			Description = "PORTA data acquisition error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 451u,
			SysCode = 131,
			Description = "PORTA data reception错误"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 452u,
			SysCode = 132,
			Description = "PORTA data transmission has begun"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 453u,
			SysCode = 128,
			Description = "PORTB interrupts signal output"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 454u,
			SysCode = 129,
			Description = "PORTB data reception"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 455u,
			SysCode = 130,
			Description = "PORTB data acquisition error"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 456u,
			SysCode = 131,
			Description = "PORTB data reception错误"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 457u,
			SysCode = 132,
			Description = "PORTB data transmission has begun"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 460u,
			SysCode = 140,
			Description = "Abnormal backup battery"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 461u,
			SysCode = 141,
			Description = "Basic unit time constant"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 462u,
			SysCode = 142,
			Description = "HKEY multi-key energy removal"
		});
		List_PD_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 463u,
			SysCode = 143,
			Description = "The HKEY scan has been completed"
		});
		for (int i = 0; i < 16; i++)
		{
			List_PD_R.Add(new KVSSystemValue
			{
				Code = 0,
				Offset = (uint)(464 + i),
				SysCode = 144,
				SysID = (ushort)i,
				Description = "HKEY information storage clearing"
			});
		}
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 400u,
			SysCode = 80,
			Description = "KV-D30 custom SW1"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 401u,
			SysCode = 81,
			Description = "KV-D30 custom SW2"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 402u,
			SysCode = 82,
			Description = "KV-D30 custom SW3"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 403u,
			SysCode = 83,
			Description = "KV-D30 custom SW4"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 404u,
			SysCode = 84,
			Description = "KV-D30 Custom LP1"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 405u,
			SysCode = 85,
			Description = "KV-D30 Custom LP2"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 406u,
			SysCode = 86,
			Description = "KV-D30 Custom LP3"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 407u,
			SysCode = 87,
			Description = "KV-D30 Custom LP4"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 408u,
			SysCode = 88,
			Description = "KV-D30 green light ON"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 409u,
			SysCode = 89,
			Description = "KV-D30 red light ON"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 410u,
			SysCode = 90,
			Description = "KV-D30 display language"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 411u,
			SysCode = 91,
			Description = "KV-D30 buzzer"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 412u,
			SysCode = 92,
			Description = "KV-D30 negative/positive"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 413u,
			SysCode = 93,
			Description = "KV-D30 reset"
		});
		List_D_R.Add(new KVSSystemValue
		{
			Code = 0,
			Offset = 415u,
			SysCode = 95,
			Description = "User message display"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1000u,
			SysCode = 256,
			Description = "The number of data received by PORTA"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1100u,
			SysCode = 258,
			Description = "The number of data sent by PORTA"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1200u,
			SysCode = 260,
			Description = "The number of data received by PORTB"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1300u,
			SysCode = 262,
			Description = "The number of data sent by PORTB"
		});
		for (int j = 1; j < 100; j++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1000 + j),
				SysCode = 257,
				SysID = (ushort)(j - 1),
				Description = $"PORTA receives data {j-1}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1100 + j),
				SysCode = 259,
				SysID = (ushort)(j - 1),
				Description = $"PORTA sends data {j-1}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1200 + j),
				SysCode = 261,
				SysID = (ushort)(j - 1),
				Description = $"PORTB receives data {j-1}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1300 + j),
				SysCode = 263,
				SysID = (ushort)(j - 1),
				Description = $"PORTB sends data {j-1}"
			});
		}
		for (int k = 0; k < 32; k++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1400 + k),
				SysCode = 288,
				SysID = (ushort)k,
				Description = $"QL receives data {k}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1432 + k),
				SysCode = 289,
				SysID = (ushort)k,
				Description = $"QL sends data {k}"
			});
		}
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1564u,
			SysCode = 299,
			Description = "The number of PORTB communication addresses"
		});
		for (int l = 0; l < 8; l++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1570 + l),
				SysCode = 291,
				Description = "QL connection information"
			});
		}
		for (int m = 0; m < 64; m++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1600 + m),
				SysCode = 292,
				SysID = (ushort)m,
				Description = $"KL receiving area {m}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1700 + m),
				SysCode = 293,
				SysID = (ushort)m,
				Description = $"KL sending area {m}"
			});
		}
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1800u,
			SysCode = 294,
			Description = "KL sending address"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1801u,
			SysCode = 295,
			Description = "KL sending address数"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1802u,
			SysCode = 296,
			Description = "KL receiving address"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1803u,
			SysCode = 297,
			Description = "KL receiving address数"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1804u,
			SysCode = 298,
			Description = "KL incorrect number"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1805u,
			SysCode = 299,
			Description = "The number of KL connection addresses"
		});
		for (int n = 0; n < 16; n++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1810 + n),
				SysCode = 300,
				SysID = (ushort)n,
				Description = "KL connection information"
			});
		}
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1900u,
			SysCode = 304,
			SysID = 0,
			Description = "CTH0 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1901u,
			SysCode = 304,
			SysID = 1,
			Description = "CTH0 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1902u,
			SysCode = 306,
			SysID = 0,
			Description = "CTH1 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1903u,
			SysCode = 306,
			SysID = 1,
			Description = "CTH1 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1904u,
			SysCode = 308,
			SysID = 0,
			Description = "CTC0 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1905u,
			SysCode = 308,
			SysID = 1,
			Description = "CTC0 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1906u,
			SysCode = 310,
			SysID = 0,
			Description = "CTC1 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1907u,
			SysCode = 310,
			SysID = 1,
			Description = "CTC1 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1908u,
			SysCode = 312,
			SysID = 0,
			Description = "CTC2 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1909u,
			SysCode = 312,
			SysID = 1,
			Description = "CTC2 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1910u,
			SysCode = 314,
			SysID = 0,
			Description = "CTC3 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1911u,
			SysCode = 314,
			SysID = 1,
			Description = "CTC3 24-bit read"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1912u,
			SysCode = 304,
			SysID = 0,
			Description = "CTH0 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1913u,
			SysCode = 304,
			SysID = 1,
			Description = "CTH0 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1914u,
			SysCode = 306,
			SysID = 0,
			Description = "CTH1 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1915u,
			SysCode = 306,
			SysID = 1,
			Description = "CTH1 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1916u,
			SysCode = 308,
			SysID = 0,
			Description = "CTC0 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1917u,
			SysCode = 308,
			SysID = 1,
			Description = "CTC0 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1918u,
			SysCode = 310,
			SysID = 0,
			Description = "CTC1 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1919u,
			SysCode = 310,
			SysID = 1,
			Description = "CTC1 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1920u,
			SysCode = 312,
			SysID = 0,
			Description = "CTC2 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1921u,
			SysCode = 312,
			SysID = 1,
			Description = "CTC2 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1922u,
			SysCode = 314,
			SysID = 0,
			Description = "CTC3 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1923u,
			SysCode = 314,
			SysID = 1,
			Description = "CTC3 24-bit write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1924u,
			SysCode = 316,
			SysID = 0,
			Description = "CTH0 is preset for writing"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1925u,
			SysCode = 316,
			SysID = 1,
			Description = "CTH0 is preset for writing"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1926u,
			SysCode = 317,
			SysID = 0,
			Description = "CTH1 preset write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1927u,
			SysCode = 317,
			SysID = 1,
			Description = "CTH1 preset write"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1928u,
			SysCode = 318,
			SysID = 0,
			Description = "INT0 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1929u,
			SysCode = 318,
			SysID = 1,
			Description = "INT0 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1930u,
			SysCode = 319,
			SysID = 0,
			Description = "INT1 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1931u,
			SysCode = 319,
			SysID = 1,
			Description = "INT1 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1932u,
			SysCode = 320,
			SysID = 0,
			Description = "INT2 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1933u,
			SysCode = 320,
			SysID = 1,
			Description = "INT2 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1934u,
			SysCode = 321,
			SysID = 0,
			Description = "INT3 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1935u,
			SysCode = 321,
			SysID = 1,
			Description = "INT3 input capture"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1936u,
			SysCode = 322,
			Description = "Frequency pulse setting value"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1937u,
			SysCode = 323,
			Description = "Expand unit connection information"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1938u,
			SysCode = 324,
			Description = "Upper limit value of digital fine-tuner 0"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1939u,
			SysCode = 325,
			Description = "Upper limit value of digital fine-tuner 1"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1940u,
			SysCode = 326,
			Description = "输入when间常数设定"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1944u,
			SysCode = 327,
			Description = "Split conversion Settings"
		});
		List_PD_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1950u,
			SysCode = 328,
			Description = "User message number"
		});
		for (int num = 0; num < 5; num++)
		{
			for (int num2 = 0; num2 < 4; num2++)
			{
				List_D_DM.Add(new KVSSystemValue
				{
					Code = 6,
					Offset = (uint)(1580 + num * 4 + num2),
					SysCode = 336,
					SysID = (ushort)(num * 4 + num2),
					Description = $"Soft components on line {num2} on page {num}"
				});
				List_D_DM.Add(new KVSSystemValue
				{
					Code = 6,
					Offset = (uint)(1680 + num * 4 + num2),
					SysCode = 337,
					SysID = (ushort)(num * 4 + num2),
					Description = $"Attribute 1 on line {num2} of page {num}"
				});
				List_D_DM.Add(new KVSSystemValue
				{
					Code = 6,
					Offset = (uint)(1780 + num * 4 + num2),
					SysCode = 338,
					SysID = (ushort)(num * 4 + num2),
					Description = $"Attribute 2 on line {num2} of page {num}"
				});
			}
		}
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1676u,
			SysCode = 339,
			Description = "KV-D30 switching enable"
		});
		for (int num3 = 0; num3 < 4; num3++)
		{
			List_D_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1677 + num3),
				SysCode = 340,
				SysID = (ushort)num3,
				Description = $"Directly access {num3 + 1}"
			});
		}
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1400u,
			SysCode = 265,
			Description = "The CAM switch output relay starts"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1401u,
			SysCode = 265,
			Description = "CAM switch multi-stage comparator"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1402u,
			SysCode = 266,
			Description = "The input pulse number of the CAM switch"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1404u,
			SysCode = 267,
			Description = "Frequency measurement period"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1405u,
			SysCode = 268,
			Description = "Frequency measurement result"
		});
		for (int num4 = 0; num4 < 32; num4++)
		{
			List_D_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1406 + num4 * 2),
				SysCode = 269,
				SysID = (ushort)num4,
				Description = $"The CAM switch outputs the {num4} ON Angle"
			});
			List_D_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1407 + num4 * 2),
				SysCode = 270,
				SysID = (ushort)num4,
				Description = $"The CAM switch outputs the {num4} OFF Angle"
			});
		}
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1480u,
			SysCode = 272,
			Description = "Trapezoidal starting frequency"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1481u,
			SysCode = 273,
			Description = "Trapezoidal operating frequency"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1482u,
			SysCode = 274,
			Description = "梯形加减速when间"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1484u,
			SysCode = 275,
			SysID = 0,
			Description = "Trapezoidal output pulse number"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1485u,
			SysCode = 275,
			SysID = 1,
			Description = "Trapezoidal output pulse number"
		});
		List_D_DM.Add(new KVSSystemValue
		{
			Code = 6,
			Offset = 1486u,
			SysCode = 276,
			Description = "Trapezoidal error code"
		});
		for (int num5 = 0; num5 < 32; num5++)
		{
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1500 + num5),
				SysCode = 288,
				SysID = (ushort)num5,
				Description = $"QL received data {num5}"
			});
			List_PD_DM.Add(new KVSSystemValue
			{
				Code = 6,
				Offset = (uint)(1532 + num5),
				SysCode = 289,
				SysID = (ushort)num5,
				Description = $"QL sends data {num5}"
			});
		}
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 322u,
			SysCode = 2,
			Description = "Constant ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 323u,
			SysCode = 3,
			Description = "Always OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 324u,
			SysCode = 4,
			Description = "10ms clock pulse"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 325u,
			SysCode = 5,
			Description = "100ms clock pulse"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 326u,
			SysCode = 6,
			Description = "1-second clock pulse"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 327u,
			SysCode = 7,
			Description = "The operation starts with a scan ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 328u,
			SysCode = 8,
			Description = "The operation starts with a scan OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 330u,
			SysCode = 10,
			Description = "运算结果is负数"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 331u,
			SysCode = 11,
			Description = "运算结果is零"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 332u,
			SysCode = 12,
			Description = "运算结果is正数"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 333u,
			SysCode = 13,
			Description = "Calculation error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 368u,
			SysCode = 48,
			Description = "External output energy"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 369u,
			SysCode = 49,
			Description = "Input refresh function"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 371u,
			SysCode = 51,
			Description = "Constant scanning time enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 372u,
			SysCode = 512,
			Description = "The constant scanning cycle was exceeded by one scan"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 373u,
			SysCode = 513,
			Description = "The input time constant setting of the CPU unit"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 375u,
			SysCode = 514,
			Description = "Minimum scanning time/最大值清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 376u,
			SysCode = 515,
			Description = "END processing time setting."
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 378u,
			SysCode = 516,
			Description = "day历定when器设定"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 379u,
			SysCode = 517,
			Description = "when间异常"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 381u,
			SysCode = 518,
			Description = "The VT sensor application forcibly switches the screen"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 382u,
			SysCode = 519,
			Description = "item目密码验证禁止状态"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 337u,
			SysCode = 520,
			SysID = 0,
			Description = "Connect expansion device 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 338u,
			SysCode = 520,
			SysID = 1,
			Description = "Connect expansion device 2"
		});
		for (int num6 = 1; num6 <= 8; num6++)
		{
			List_N_CR.Add(new KVSSystemValue
			{
				Code = 1,
				Offset = (uint)(353 + num6),
				SysCode = 521,
				SysID = (ushort)(num6 - 1),
				Description = $"Connect the expansion unit {num6}"
			});
		}
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 416u,
			SysCode = 66,
			Description = "INT R0 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 417u,
			SysCode = 67,
			Description = "INT R0 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 418u,
			SysCode = 68,
			Description = "INT R1 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 419u,
			SysCode = 69,
			Description = "INT R1 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 420u,
			SysCode = 74,
			Description = "INT R2 interrupt polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 421u,
			SysCode = 75,
			Description = "INT R2 interrupt polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 422u,
			SysCode = 76,
			Description = "INT R3 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 423u,
			SysCode = 77,
			Description = "INT R3 interrupts polarity"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 462u,
			SysCode = 528,
			Description = "ASCII code conversion and zero setting"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 463u,
			SysCode = 529,
			Description = "ASCII code symbols omitted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 464u,
			SysCode = 530,
			Description = "User Message 1显示"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 465u,
			SysCode = 531,
			Description = "User Message 2 is displayed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 479u,
			SysCode = 532,
			Description = "There is an access window box"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 480u,
			SysCode = 533,
			Description = "item目下载执行请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 482u,
			SysCode = 534,
			Description = "item目保存执行请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 484u,
			SysCode = 535,
			Description = "item目下载执行完成"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 485u,
			SysCode = 536,
			Description = "item目下载执行失败"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 486u,
			SysCode = 537,
			Description = "item目保存执行完成"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 487u,
			SysCode = 538,
			Description = "item目保存执行失败"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 522u,
			SysCode = 544,
			Description = "Any record is being executed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 523u,
			SysCode = 545,
			Description = "The memory card is in use"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 524u,
			SysCode = 546,
			Description = "The memory card recognition is complete"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 525u,
			SysCode = 547,
			Description = "There is a memory card"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 526u,
			SysCode = 548,
			Description = "The memory card is executing instructions"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 527u,
			SysCode = 549,
			Description = "Memory card write protection"
		});
		for (int num7 = 0; num7 < 32; num7++)
		{
			List_N_CR.Add(new KVSSystemValue
			{
				Code = 1,
				Offset = (uint)(528 + num7),
				SysCode = 560,
				SysID = (ushort)num7,
				Description = $"User Alert {num7}"
			});
		}
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 560u,
			SysCode = 561,
			Description = "Alarm detection"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 561u,
			SysCode = 562,
			Description = "警报day志设定"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 562u,
			SysCode = 563,
			Description = "警报day志清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 574u,
			SysCode = 142,
			Description = "The HKEY command has multiple key functions removed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 575u,
			SysCode = 143,
			Description = "The HKEY command scan has been completed"
		});
		for (int num8 = 0; num8 < 16; num8++)
		{
			List_N_CR.Add(new KVSSystemValue
			{
				Code = 1,
				Offset = (uint)(576 + num8),
				SysCode = 145,
				Description = "HKEY information storage area"
			});
		}
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 592u,
			SysCode = 80,
			Description = "KV-D30 Custom Switch 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 593u,
			SysCode = 81,
			Description = "KV-D30 Custom Switch 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 594u,
			SysCode = 82,
			Description = "KV-D30 Custom Switch 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 595u,
			SysCode = 83,
			Description = "KV-D30 Custom Switch 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 596u,
			SysCode = 84,
			Description = "KV-D30 Custom Indicator Light 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 597u,
			SysCode = 85,
			Description = "KV-D30 Custom Indicator Light 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 598u,
			SysCode = 86,
			Description = "KV-D30 Custom Indicator Light 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 599u,
			SysCode = 87,
			Description = "KV-D30 Custom Indicator Light 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 600u,
			SysCode = 88,
			Description = "KV-D30 green light ON "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 601u,
			SysCode = 89,
			Description = "KV-D30 red light ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 602u,
			SysCode = 90,
			Description = "KV-D30 display language"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 603u,
			SysCode = 91,
			Description = "KV-D30 buzzer"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 604u,
			SysCode = 92,
			Description = "KV-D30 negative/positive "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 605u,
			SysCode = 576,
			Description = "KV-D30 alarm interruption enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 606u,
			SysCode = 577,
			Description = "KV-D30 page switch execution"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 607u,
			SysCode = 578,
			Description = "The KV-D30 page can be manually switched to"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 608u,
			SysCode = 579,
			Description = "External output of KV-D30:0 ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 609u,
			SysCode = 580,
			Description = "External output of KV-D30 1 ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 624u,
			SysCode = 592,
			Description = "清除严重错误day志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 625u,
			SysCode = 593,
			Description = "清除一般错误day志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 626u,
			SysCode = 594,
			Description = "最大严重错误day志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 627u,
			SysCode = 595,
			Description = "最大一般错误day志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 628u,
			SysCode = 596,
			Description = "RUN-PROG toggle switch"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 629u,
			SysCode = 597,
			Description = "A serious error is happening"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 630u,
			SysCode = 598,
			Description = "A general error is happening"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 631u,
			SysCode = 599,
			Description = "清除电源ONday志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 632u,
			SysCode = 600,
			Description = "清除电源OFFday志"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 633u,
			SysCode = 601,
			Description = "Clear the current error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 672u,
			SysCode = 608,
			Description = "CTH0 uses an internal clock of 50ns"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 673u,
			SysCode = 16,
			Description = "CTH0 uses an internal clock of 1us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 674u,
			SysCode = 17,
			Description = "CTH0 uses an internal clock of 10 microseconds"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 675u,
			SysCode = 18,
			Description = "CTH0 uses an internal clock of 100us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 677u,
			SysCode = 609,
			Description = "There is CTH0"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 678u,
			SysCode = 610,
			Description = "CTH0 overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 679u,
			SysCode = 611,
			Description = "The direction of CTH0 change"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 680u,
			SysCode = 612,
			SysID = 0,
			Description = "The setting of external preset input for CTH0"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 681u,
			SysCode = 612,
			SysID = 1,
			Description = "The setting of external preset input for CTH0"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 682u,
			SysCode = 613,
			Description = "CTH0 counter mode selection"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 683u,
			SysCode = 614,
			Description = "CTH0 preset energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 684u,
			SysCode = 615,
			Description = "The CTH0 internally allows relay count enabling"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 691u,
			SysCode = 19,
			Description = "CTC0 is automatically cleared"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 692u,
			SysCode = 20,
			Description = "CTC0 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 693u,
			SysCode = 21,
			Description = "CTC0 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 694u,
			SysCode = 22,
			Description = "CTC0 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 695u,
			SysCode = 23,
			Description = "The output of CTC0 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 696u,
			SysCode = 24,
			Description = "Output the energy of CTC1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 697u,
			SysCode = 25,
			Description = "CTC1 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 698u,
			SysCode = 26,
			Description = "CTC1 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 699u,
			SysCode = 27,
			Description = "The output of CTC1 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 700u,
			SysCode = 28,
			Description = "CTC1 is automatically cleared"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 701u,
			SysCode = 616,
			SysID = 0,
			Description = "Selection of CTH0 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 702u,
			SysCode = 616,
			SysID = 1,
			Description = "Selection of CTH0 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 703u,
			SysCode = 616,
			SysID = 2,
			Description = "Selection of CTH0 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 704u,
			SysCode = 624,
			Description = "CTH1 uses an internal clock of 50ns"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 705u,
			SysCode = 32,
			Description = "CTH1 uses an internal clock of 1us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 706u,
			SysCode = 33,
			Description = "CTH1 uses an internal clock of 10 microseconds"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 707u,
			SysCode = 34,
			Description = "CTH1 uses an internal clock of 100us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 709u,
			SysCode = 625,
			Description = "There is CTH1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 710u,
			SysCode = 626,
			Description = "CTH1 overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 711u,
			SysCode = 627,
			Description = "The direction of CTH1 change"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 712u,
			SysCode = 628,
			SysID = 0,
			Description = "The setting of external preset input for CTH1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 713u,
			SysCode = 628,
			SysID = 1,
			Description = "The setting of external preset input for CTH1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 714u,
			SysCode = 629,
			Description = "CTH1 counter mode selection"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 715u,
			SysCode = 630,
			Description = "CTH1 presets energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 716u,
			SysCode = 631,
			Description = "The CTH1 internally allows relay count enabling"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 723u,
			SysCode = 35,
			Description = "CTC2 is automatically cleared"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 724u,
			SysCode = 36,
			Description = "CTC2 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 725u,
			SysCode = 37,
			Description = "CTC2 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 726u,
			SysCode = 38,
			Description = "CTC2 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 727u,
			SysCode = 39,
			Description = "The output of CTC2 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 728u,
			SysCode = 40,
			Description = "CTC3 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 729u,
			SysCode = 41,
			Description = "CTC3 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 730u,
			SysCode = 42,
			Description = "CTC3 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 731u,
			SysCode = 43,
			Description = "The output of CTC3 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 732u,
			SysCode = 44,
			Description = "CTC3 is automatically cleared"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 733u,
			SysCode = 632,
			SysID = 0,
			Description = "Selection of CTH1 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 734u,
			SysCode = 632,
			SysID = 1,
			Description = "Selection of CTH1 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 735u,
			SysCode = 632,
			SysID = 2,
			Description = "Selection of CTH1 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 736u,
			SysCode = 640,
			Description = "CTH2 uses an internal clock of 50ns"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 737u,
			SysCode = 649,
			Description = "CTH2 uses an internal clock of 1us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 738u,
			SysCode = 650,
			Description = "CTH2 uses an internal clock of 10 microseconds"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 739u,
			SysCode = 651,
			Description = "CTH2 uses an internal clock of 100us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 741u,
			SysCode = 641,
			Description = "There is CTH2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 742u,
			SysCode = 642,
			Description = "CTH2 overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 743u,
			SysCode = 643,
			Description = "The direction of CTH2 change"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 744u,
			SysCode = 644,
			SysID = 0,
			Description = "The setting of external preset input for CTH2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 745u,
			SysCode = 644,
			SysID = 1,
			Description = "The setting of external preset input for CTH2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 746u,
			SysCode = 645,
			Description = "CTH2 counter mode selection"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 747u,
			SysCode = 646,
			Description = "CTH2 preset energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 748u,
			SysCode = 647,
			Description = "The CTH2 internally allows relay count enabling"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 755u,
			SysCode = 672,
			Description = "CTC4 automatically clears"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 756u,
			SysCode = 673,
			Description = "CTC4 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 757u,
			SysCode = 674,
			Description = "CTC4 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 758u,
			SysCode = 675,
			Description = "CTC4 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 759u,
			SysCode = 676,
			Description = "The output of CTC4 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 760u,
			SysCode = 677,
			Description = "CTC5 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 761u,
			SysCode = 678,
			Description = "CTC5 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 762u,
			SysCode = 679,
			Description = "CTC5 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 763u,
			SysCode = 680,
			Description = "The output of CTC5 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 764u,
			SysCode = 681,
			Description = "CTC5 automatically clears"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 765u,
			SysCode = 648,
			SysID = 0,
			Description = "Selection of CTH2 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 766u,
			SysCode = 648,
			SysID = 1,
			Description = "Selection of CTH2 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 767u,
			SysCode = 648,
			SysID = 2,
			Description = "Selection of CTH2 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 768u,
			SysCode = 656,
			Description = "CTH3 uses an internal clock of 50ns"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 769u,
			SysCode = 665,
			Description = "CTH3 uses an internal clock of 1us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 770u,
			SysCode = 666,
			Description = "CTH3 uses an internal clock of 10 microseconds"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 771u,
			SysCode = 667,
			Description = "CTH3 uses an internal clock of 100us"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 773u,
			SysCode = 657,
			Description = "There is CTH3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 774u,
			SysCode = 658,
			Description = "CTH3 overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 775u,
			SysCode = 659,
			Description = "The direction of CTH3 change"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 776u,
			SysCode = 660,
			SysID = 0,
			Description = "The setting of external preset inputs for CTH3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 777u,
			SysCode = 660,
			SysID = 1,
			Description = "The setting of external preset inputs for CTH3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 778u,
			SysCode = 661,
			Description = "CTH3 counter mode selection"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 779u,
			SysCode = 662,
			Description = "CTH3 preset energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 780u,
			SysCode = 663,
			Description = "The CTH3 internally allows relay count enabling"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 787u,
			SysCode = 688,
			Description = "CTC6 automatically clears"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 788u,
			SysCode = 689,
			Description = "CTC6 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 789u,
			SysCode = 690,
			Description = "CTC6 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 790u,
			SysCode = 691,
			Description = "CTC6 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 791u,
			SysCode = 692,
			Description = "The output of CTC6 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 792u,
			SysCode = 693,
			Description = "CTC7 output energy removal"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 793u,
			SysCode = 694,
			Description = "CTC7 outputs ON"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 794u,
			SysCode = 695,
			Description = "CTC7 outputs OFF"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 795u,
			SysCode = 696,
			Description = "The output of CTC7 is inverted"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 796u,
			SysCode = 697,
			Description = "CTC7 automatically clears"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 797u,
			SysCode = 664,
			SysID = 0,
			Description = "Selection of CTH3 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 798u,
			SysCode = 664,
			SysID = 1,
			Description = "Selection of CTH3 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 799u,
			SysCode = 664,
			SysID = 2,
			Description = "Selection of CTH3 count input mode"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 864u,
			SysCode = 704,
			Description = "The CH0 frequency counter is running"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 865u,
			SysCode = 705,
			SysID = 0,
			Description = "Input setting of CH0 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 866u,
			SysCode = 705,
			SysID = 1,
			Description = "Input setting of CH0 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 867u,
			SysCode = 706,
			Description = "The CH0 frequency counter switches between Hz and rpm"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 868u,
			SysCode = 707,
			Description = "CH0 frequency counter rpm measurement method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 869u,
			SysCode = 708,
			Description = "CH0 frequency counter input source switching method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 872u,
			SysCode = 709,
			Description = "The CH0 specified frequency pulse output operates"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 873u,
			SysCode = 710,
			Description = "The CH0 specified frequency pulse output is incorrect"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 880u,
			SysCode = 720,
			Description = "The CH1 frequency counter is in operation"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 881u,
			SysCode = 721,
			SysID = 0,
			Description = "Input setting of CH1 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 882u,
			SysCode = 721,
			SysID = 1,
			Description = "Input setting of CH1 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 883u,
			SysCode = 722,
			Description = "The CH1 frequency counter switches between Hz and rpm"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 884u,
			SysCode = 723,
			Description = "CH1 frequency counter rpm measurement method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 885u,
			SysCode = 724,
			Description = "CH1 frequency counter input source switching method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 888u,
			SysCode = 725,
			Description = "The CH1 specified frequency pulse output operates"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 889u,
			SysCode = 726,
			Description = "The CH1 specified frequency pulse output is incorrect"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 864u,
			SysCode = 736,
			Description = "The CH2 frequency counter is in operation"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 865u,
			SysCode = 737,
			SysID = 0,
			Description = "Input setting of CH2 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 866u,
			SysCode = 737,
			SysID = 1,
			Description = "Input setting of CH2 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 867u,
			SysCode = 738,
			Description = "The CH2 frequency counter switches between Hz and rpm"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 868u,
			SysCode = 739,
			Description = "CH2 frequency counter rpm measurement method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 869u,
			SysCode = 740,
			Description = "CH2 frequency counter input source switching method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 872u,
			SysCode = 741,
			Description = "The CH2 specified frequency pulse output operates"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 873u,
			SysCode = 742,
			Description = "The CH2 specified frequency pulse output is incorrect"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 880u,
			SysCode = 752,
			Description = "The CH3 frequency counter is running"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 881u,
			SysCode = 753,
			SysID = 0,
			Description = "Input Settings for the CH3 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 882u,
			SysCode = 753,
			SysID = 1,
			Description = "Input Settings for the CH3 frequency counter"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 883u,
			SysCode = 754,
			Description = "The CH3 frequency counter switches between Hz and rpm"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 884u,
			SysCode = 755,
			Description = "CH3 frequency counter rpm measurement method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 885u,
			SysCode = 756,
			Description = "CH3 frequency counter input source switching method"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 888u,
			SysCode = 757,
			Description = "CH3 operates with pulse output at the specified frequency"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 889u,
			SysCode = 758,
			Description = "The CH3 specified frequency pulse output is incorrect"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1280u,
			SysCode = 768,
			Description = "Axis 1 is forcibly stopped"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1281u,
			SysCode = 769,
			SysID = 0,
			Description = "Shaft 1 decelerates and stops"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1282u,
			SysCode = 770,
			SysID = 1,
			Description = "Axis 1 error清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1283u,
			SysCode = 771,
			Description = "Axis 1 Warning清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1284u,
			SysCode = 772,
			Description = "轴 1 Current coordinate更改请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1285u,
			SysCode = 773,
			Description = "Request for changing the operating speed of Axis 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1286u,
			SysCode = 774,
			Description = "Request for changing the target coordinates of Axis 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1292u,
			SysCode = 775,
			Description = "轴 1 限位开关顺when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1293u,
			SysCode = 776,
			Description = "轴 1 限位开关逆when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1294u,
			SysCode = 777,
			Description = "Input of the origin sensor for axis 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1295u,
			SysCode = 778,
			Description = "Axis 1 stops the sensor input"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1296u,
			SysCode = 784,
			Description = "Shaft 2 is forced to stop"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1297u,
			SysCode = 785,
			SysID = 0,
			Description = "Shaft 2 decelerates and stops"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1298u,
			SysCode = 786,
			SysID = 1,
			Description = "Axis 2 error清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1299u,
			SysCode = 787,
			Description = "Axis 2 Warning清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1300u,
			SysCode = 788,
			Description = "轴 2 Current coordinate更改请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1301u,
			SysCode = 789,
			Description = "Request for changing the operating speed of Axis 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1302u,
			SysCode = 790,
			Description = "Request for changing the target coordinates of Axis 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1308u,
			SysCode = 791,
			Description = "轴 2 限位开关顺when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1309u,
			SysCode = 792,
			Description = "轴 2 限位开关逆when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1310u,
			SysCode = 793,
			Description = "Input of the origin sensor for axis 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1311u,
			SysCode = 794,
			Description = "Axis 2 stops the sensor input"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1312u,
			SysCode = 800,
			Description = "Forced stop of Axis 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1313u,
			SysCode = 801,
			SysID = 0,
			Description = "Shaft 3 decelerates and stops"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1314u,
			SysCode = 802,
			SysID = 1,
			Description = "Axis 3 error清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1315u,
			SysCode = 803,
			Description = "Axis 3 Warning清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1316u,
			SysCode = 804,
			Description = "轴 3 Current coordinate更改请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1317u,
			SysCode = 805,
			Description = "Request for changing the operating speed of Axis 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1318u,
			SysCode = 806,
			Description = "Request for changing the target coordinates of Axis 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1324u,
			SysCode = 807,
			Description = "轴 3 限位开关顺when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1325u,
			SysCode = 808,
			Description = "轴 3 限位开关逆when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1326u,
			SysCode = 809,
			Description = "Input of the origin sensor for axis 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1327u,
			SysCode = 810,
			Description = "Axis 3 stops the sensor input"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1328u,
			SysCode = 816,
			Description = "Forced stop of Axis 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1329u,
			SysCode = 817,
			SysID = 0,
			Description = "Shaft 4 decelerates and stops"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1330u,
			SysCode = 818,
			SysID = 1,
			Description = "Axis 4 error清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1331u,
			SysCode = 819,
			Description = "Axis 4 Warning清除"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1332u,
			SysCode = 820,
			Description = "轴 4 Current coordinate更改请求"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1333u,
			SysCode = 821,
			Description = "Request for changing the operating speed of Axis 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1334u,
			SysCode = 822,
			Description = "Request for changing the target coordinates of Axis 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1340u,
			SysCode = 823,
			Description = "轴 4 限位开关顺when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1341u,
			SysCode = 824,
			Description = "轴 4 限位开关逆when针方向输入 "
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1342u,
			SysCode = 825,
			Description = "Axis 4 origin sensor input"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1343u,
			SysCode = 826,
			Description = "Stop the sensor input on axis 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1344u,
			SysCode = 832,
			Description = "In the pulse output of Axis 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1345u,
			SysCode = 833,
			Description = "The relay for completing the positioning of axis 1"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1346u,
			SysCode = 834,
			Description = "Axis 1 error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1347u,
			SysCode = 835,
			Description = "Axis 1 Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1348u,
			SysCode = 836,
			Description = "Axis 1 is in the process of returning to the origin"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1349u,
			SysCode = 837,
			Description = "The return to the origin of Axis 1 has been completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1358u,
			SysCode = 838,
			Description = "Axis 1 has"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1359u,
			SysCode = 839,
			Description = "Axis 1 comparator 2 matches relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1360u,
			SysCode = 848,
			Description = "In the pulse output of Axis 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1361u,
			SysCode = 849,
			Description = "The relay for completing the positioning of axis 2"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1362u,
			SysCode = 850,
			Description = "Axis 2 error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1363u,
			SysCode = 851,
			Description = "Axis 2 Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1364u,
			SysCode = 852,
			Description = "The action of returning to the origin of Axis 2 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1365u,
			SysCode = 853,
			Description = "The return to the origin of Axis 2 has been completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1374u,
			SysCode = 854,
			Description = "Axis 2 has"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1375u,
			SysCode = 855,
			Description = "Axis 2 comparator 2 matches the relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1376u,
			SysCode = 864,
			Description = "In the pulse output of Axis 3"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1377u,
			SysCode = 865,
			Description = "Axis 3 positioning completion relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1378u,
			SysCode = 866,
			Description = "Axis 3 error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1379u,
			SysCode = 867,
			Description = "Axis 3 Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1380u,
			SysCode = 868,
			Description = "The action of returning to the origin of axis 3 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1381u,
			SysCode = 869,
			Description = "The return to the origin of Axis 3 has been completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1390u,
			SysCode = 870,
			Description = "Axis 3 has"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1391u,
			SysCode = 871,
			Description = "Axis 3 comparator 2 matches the relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1392u,
			SysCode = 880,
			Description = "In the pulse output of axis 4"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1393u,
			SysCode = 881,
			Description = "Axis 4 positioning completion relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1394u,
			SysCode = 882,
			Description = "Axis 4 error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1395u,
			SysCode = 883,
			Description = "Axis 4 Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1396u,
			SysCode = 884,
			Description = "Axis 4 is in the process of returning to the origin"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1397u,
			SysCode = 885,
			Description = "The origin reset of axis 4 has been completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1406u,
			SysCode = 886,
			Description = "Axis 4 has"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1407u,
			SysCode = 887,
			Description = "Axis 4 comparator 2 matches relay"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 0u,
			SysCode = 896,
			Description = "Record/Track 0 execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 1u,
			SysCode = 897,
			Description = "Recording/Tracking 0 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 2u,
			SysCode = 898,
			Description = "Record/Track 0 file writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 3u,
			SysCode = 899,
			Description = "Record 0: Cache less than 50% warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 4u,
			SysCode = 900,
			Description = "Record 0 Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 5u,
			SysCode = 901,
			Description = "Record 0 is written during the RUN execution"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 8u,
			SysCode = 902,
			Description = "Record/Track 0 Record/Track error"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 9u,
			SysCode = 903,
			Description = "记录/跟踪0 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 10u,
			SysCode = 904,
			Description = "Record/track 0 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 11u,
			SysCode = 905,
			Description = "Tracking 0 file saving triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 12u,
			SysCode = 906,
			Description = "Tracking 0 data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 16u,
			SysCode = 912,
			Description = "Record/Track 1 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 17u,
			SysCode = 913,
			Description = "Recording/Tracking 1 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 18u,
			SysCode = 914,
			Description = "Record/Track 1 File writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 19u,
			SysCode = 915,
			Description = "Record 1: Cache less than 50% warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 20u,
			SysCode = 916,
			Description = "Record 1: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 21u,
			SysCode = 917,
			Description = "Record 1: Written in the RUN during execution"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 24u,
			SysCode = 918,
			Description = "Record/Track 1 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 25u,
			SysCode = 919,
			Description = "记录/跟踪1 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 26u,
			SysCode = 920,
			Description = "Record/Track 1 Soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 27u,
			SysCode = 921,
			Description = "Tracking 1 file saving triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 28u,
			SysCode = 922,
			Description = "Tracking 1: Data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 32u,
			SysCode = 928,
			Description = "Record/Track 2 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 33u,
			SysCode = 929,
			Description = "Record/Track 2 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 34u,
			SysCode = 930,
			Description = "Record/Track 2 file writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 35u,
			SysCode = 931,
			Description = "Record 2: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 36u,
			SysCode = 932,
			Description = "Record 2: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 37u,
			SysCode = 933,
			Description = "Record 2: Written in the RUN during execution"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 40u,
			SysCode = 934,
			Description = "Record/Track 2 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 41u,
			SysCode = 935,
			Description = "记录/跟踪2 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 42u,
			SysCode = 936,
			Description = "Record/Track 2 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 43u,
			SysCode = 937,
			Description = "Tracking 2 file saving triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 44u,
			SysCode = 938,
			Description = "Tracking 2 Data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 48u,
			SysCode = 944,
			Description = "Record/Track 3 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 49u,
			SysCode = 945,
			Description = "Recording/Tracking 3 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 50u,
			SysCode = 946,
			Description = "Record/Track 3 file writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 51u,
			SysCode = 947,
			Description = "Record 3: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 52u,
			SysCode = 948,
			Description = "Record 3: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 53u,
			SysCode = 949,
			Description = "Record 3: Written in the RUN during execution"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 56u,
			SysCode = 950,
			Description = "Record/Track 3 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 57u,
			SysCode = 951,
			Description = "记录/跟踪3 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 58u,
			SysCode = 952,
			Description = "Record/Track abnormal Settings of 3 soft components/triggers"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 59u,
			SysCode = 953,
			Description = "Tracking the saving of 3 files triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 60u,
			SysCode = 954,
			Description = "Tracking 3 data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 64u,
			SysCode = 960,
			Description = "Record/Track 4 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 65u,
			SysCode = 961,
			Description = "Recording/Tracking 4 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 66u,
			SysCode = 962,
			Description = "Record/Track 4 File writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 67u,
			SysCode = 963,
			Description = "Record 4: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 68u,
			SysCode = 964,
			Description = "Record 4: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 69u,
			SysCode = 965,
			Description = "Record 4: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 72u,
			SysCode = 966,
			Description = "Record/Track 4 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 73u,
			SysCode = 967,
			Description = "记录/跟踪4 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 74u,
			SysCode = 968,
			Description = "Record/track abnormal Settings of 4 soft components/triggers"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 75u,
			SysCode = 969,
			Description = "Tracking 4 file saving triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 76u,
			SysCode = 970,
			Description = "Tracking 4 Data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 80u,
			SysCode = 976,
			Description = "Record/Track 5 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 81u,
			SysCode = 977,
			Description = "Recording/Tracking 5 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 82u,
			SysCode = 978,
			Description = "Record/Track the completion of file 5 writing"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 83u,
			SysCode = 979,
			Description = "Record 5: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 84u,
			SysCode = 980,
			Description = "Record 5: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 85u,
			SysCode = 981,
			Description = "Record 5: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 88u,
			SysCode = 982,
			Description = "Record/Track 5 Record/track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 89u,
			SysCode = 983,
			Description = "记录/跟踪5 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 90u,
			SysCode = 984,
			Description = "Record/track 5 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 91u,
			SysCode = 985,
			Description = "Track 5 file saves trigger monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 92u,
			SysCode = 986,
			Description = "Tracking 5 data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 96u,
			SysCode = 992,
			Description = "Record/Track 6 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 97u,
			SysCode = 993,
			Description = "Recording/Tracking 6 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 98u,
			SysCode = 994,
			Description = "Record/Track 6 file writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 99u,
			SysCode = 995,
			Description = "Record 6: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 100u,
			SysCode = 996,
			Description = "Record 6: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 101u,
			SysCode = 997,
			Description = "Record 6: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 104u,
			SysCode = 998,
			Description = "Record/Track 6 Record/track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 105u,
			SysCode = 999,
			Description = "记录/跟踪6 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 106u,
			SysCode = 1000,
			Description = "Record/track 6 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 107u,
			SysCode = 1001,
			Description = "Tracking 6 file saves triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 108u,
			SysCode = 1002,
			Description = "Tracking 6 data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 112u,
			SysCode = 1008,
			Description = "Record/Track 7 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 113u,
			SysCode = 1009,
			Description = "Recording/Tracking 7 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 114u,
			SysCode = 1010,
			Description = "Record/Track 7 File writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 115u,
			SysCode = 1011,
			Description = "Record 7: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 116u,
			SysCode = 1012,
			Description = "Record 7: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 117u,
			SysCode = 1013,
			Description = "Record 7: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 120u,
			SysCode = 1014,
			Description = "Record/Track 7 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 121u,
			SysCode = 1015,
			Description = "记录/跟踪7 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 122u,
			SysCode = 1016,
			Description = "Record/Track abnormal Settings of 7 soft components/triggers"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 123u,
			SysCode = 1017,
			Description = "Tracking 7 file saving triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 124u,
			SysCode = 1018,
			Description = "Tracking 7 data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 128u,
			SysCode = 1024,
			Description = "Record/Track 8 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 129u,
			SysCode = 1025,
			Description = "Recording/Tracking 8 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 130u,
			SysCode = 1026,
			Description = "Record/Track 8 file writing completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 131u,
			SysCode = 1027,
			Description = "Record 8: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 132u,
			SysCode = 1028,
			Description = "Record 8: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 133u,
			SysCode = 1029,
			Description = "Record 8: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 136u,
			SysCode = 1030,
			Description = "Record/Track 8 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 137u,
			SysCode = 1031,
			Description = "记录/跟踪8 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 138u,
			SysCode = 1032,
			Description = "Record/track 8 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 139u,
			SysCode = 1033,
			Description = "Tracking 8 files to save triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 140u,
			SysCode = 1034,
			Description = "Tracking 8 Data capture completed"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 144u,
			SysCode = 1040,
			Description = "Record/Track 9 Execution enable"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 145u,
			SysCode = 1041,
			Description = "Recording/Tracking 9 is in progress"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 146u,
			SysCode = 1042,
			Description = "Record/Track the completion of file 9 writing"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 147u,
			SysCode = 1043,
			Description = "Record 9: Cache less than 50% Warning"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 148u,
			SysCode = 1044,
			Description = "Record 9: Cache overflow"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 149u,
			SysCode = 1045,
			Description = "Record 9: Written during RUN"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 152u,
			SysCode = 1046,
			Description = "Record/Track 9 Record/Track errors"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 153u,
			SysCode = 1047,
			Description = "记录/跟踪9 存储卡no空间的错误"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 154u,
			SysCode = 1048,
			Description = "Record/track 9 soft component/trigger setting anomalies"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 155u,
			SysCode = 1049,
			Description = "Tracking the saving of 9 files triggers monitoring"
		});
		List_N_CR.Add(new KVSSystemValue
		{
			Code = 1,
			Offset = 156u,
			SysCode = 1050,
			Description = "Tracking 9 data capture completed"
		});
		for (int num9 = 0; num9 < 80; num9++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)num9,
				SysCode = 336,
				SysID = (ushort)num9,
				Description = $"Soft components on lines {num9 & 3} on page {num9 >> 2} of KV-D30"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(200 + num9),
				SysCode = 337,
				SysID = (ushort)num9,
				Description = $"Property 1 on line {num9 & 3} of page {num9 >> 2} of KV-D30"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 400u,
			SysCode = 339,
			Description = "KV-D30 switching enable"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 401u,
			SysCode = 581,
			Description = "KV-D30 page switching Settings"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 402u,
			SysCode = 582,
			Description = "Current display page of KV-D30"
		});
		for (int num10 = 0; num10 < 6; num10++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(410 + num10),
				SysCode = 340,
				SysID = (ushort)num10,
				Description = $"KV-D30 direct access {(num10 >> 2) + 1}"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 700u,
			SysCode = 1056,
			Description = "day历定when器(years)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 701u,
			SysCode = 1057,
			Description = "day历定when器(month)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 702u,
			SysCode = 1058,
			Description = "day历定when器(day)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 703u,
			SysCode = 1059,
			Description = "day历定when器(when)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 704u,
			SysCode = 1060,
			Description = "day历定when器(points)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 705u,
			SysCode = 1061,
			Description = "day历定when器(seconds)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 706u,
			SysCode = 1062,
			Description = "Day of week (Week number)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 708u,
			SysCode = 1063,
			SysID = 0,
			Description = "Idle counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 709u,
			SysCode = 1063,
			SysID = 1,
			Description = "Idle counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 710u,
			SysCode = 1072,
			Description = "CPU version number"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 720u,
			SysCode = 1088,
			Description = "Scan time measurement value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 721u,
			SysCode = 1089,
			Description = "Constant scanning time operation set value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 722u,
			SysCode = 1090,
			Description = "The constant scanning time operation exceeded the value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 723u,
			SysCode = 1091,
			Description = "END processing time measurement value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 726u,
			SysCode = 1092,
			Description = "Minimum scanning time"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 727u,
			SysCode = 1093,
			Description = "Maximum scanning time"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 728u,
			SysCode = 1094,
			Description = "END processing time setting value."
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 729u,
			SysCode = 1095,
			Description = "The processing time of END exceeds the value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1600u,
			SysCode = 318,
			SysID = 0,
			Description = "INT R0 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1601u,
			SysCode = 318,
			SysID = 1,
			Description = "INT R0 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1602u,
			SysCode = 319,
			SysID = 0,
			Description = "INT R1 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1603u,
			SysCode = 319,
			SysID = 1,
			Description = "INT R1 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1604u,
			SysCode = 320,
			SysID = 0,
			Description = "INT R2 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1605u,
			SysCode = 320,
			SysID = 1,
			Description = "INT R2 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1606u,
			SysCode = 321,
			SysID = 0,
			Description = "INT R3 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1607u,
			SysCode = 321,
			SysID = 1,
			Description = "INT R3 input capture"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1620u,
			SysCode = 326,
			Description = "输入when间常数设定"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1630u,
			SysCode = 1104,
			SysID = 0,
			Description = "Volume0"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1631u,
			SysCode = 1104,
			SysID = 1,
			Description = "Volume0"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1632u,
			SysCode = 1105,
			SysID = 0,
			Description = "Volume1"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1633u,
			SysCode = 1105,
			SysID = 1,
			Description = "Volume1"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1650u,
			SysCode = 1106,
			Description = "The sensor sets the number of command executions"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1658u,
			SysCode = 1107,
			Description = "item目密码连续认证失败次数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1659u,
			SysCode = 1108,
			Description = "item目密码累计认证失败次数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1660u,
			SysCode = 1109,
			Description = "Record/track the remaining space of the 0-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1661u,
			SysCode = 1110,
			Description = "Record/Track the remaining space of 1 circular cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1662u,
			SysCode = 1111,
			Description = "Record/track the remaining space of the 2-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1663u,
			SysCode = 1112,
			Description = "Record/track the remaining space of the 3-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1664u,
			SysCode = 1113,
			Description = "Record/track the remaining space of the 4-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1665u,
			SysCode = 1114,
			Description = "Record/track the remaining space of the 5-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1666u,
			SysCode = 1115,
			Description = "Record/track the remaining space of the 6-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1667u,
			SysCode = 1116,
			Description = "Record/track the remaining space of the 7-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1668u,
			SysCode = 1117,
			Description = "Record/track the remaining space of the 8-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1669u,
			SysCode = 1118,
			Description = "Record/track the remaining space of the 9-ring cache"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1680u,
			SysCode = 1119,
			Description = "Record/Track 0 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1681u,
			SysCode = 1120,
			Description = "Record/Track 1 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1682u,
			SysCode = 1121,
			Description = "Record/Track 2 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1683u,
			SysCode = 1122,
			Description = "Record/Track 3 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1684u,
			SysCode = 1123,
			Description = "Record/Track 4 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1685u,
			SysCode = 1124,
			Description = "Record/Track 5 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1686u,
			SysCode = 1125,
			Description = "Record/Track 6 file save counters"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1687u,
			SysCode = 1126,
			Description = "Record/Track 7 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1688u,
			SysCode = 1127,
			Description = "Record/Track 8 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1689u,
			SysCode = 1128,
			Description = "Record/track 9 file save counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1700u,
			SysCode = 1136,
			Description = "Automatically load folder number (Request)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1701u,
			SysCode = 1137,
			Description = "RunLoad folder number (Request)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1702u,
			SysCode = 1138,
			Description = "item目下载when文件夹编号(请求)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1703u,
			SysCode = 1139,
			Description = "item目保存when文件夹编号(请求)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1710u,
			SysCode = 1140,
			Description = "Automatically load the completed code"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1711u,
			SysCode = 1141,
			Description = "Automatically load folder number (Completed)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1712u,
			SysCode = 1142,
			Description = "RunLoad completes the code"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1713u,
			SysCode = 1143,
			Description = "RunLoad folder Number (Completed)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1714u,
			SysCode = 1144,
			Description = "item目下载when完成代码"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1715u,
			SysCode = 1145,
			Description = "item目下载when文件夹编号(完成)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1716u,
			SysCode = 1146,
			Description = "item目保存when完成代码"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1717u,
			SysCode = 1147,
			Description = "item目保存when文件夹编号(完成)"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1720u,
			SysCode = 1152,
			Description = "User Message 1"
		});
		for (int num11 = 0; num11 < 17; num11++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(1721 + num11),
				SysCode = 1153,
				SysID = (ushort)num11,
				Description = $"User message 2-{num11 + 1}"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1738u,
			SysCode = 1154,
			Description = "The access window can be set"
		});
		for (int num12 = 0; num12 < 17; num12++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(1740 + num12),
				SysCode = 1155,
				SysID = (ushort)num12,
				Description = "Project Name"
			});
		}
		for (int num13 = 0; num13 < 7; num13++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(1757 + num13),
				SysCode = 1156,
				SysID = (ushort)num13,
				Description = "The access window displays the name"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1780u,
			SysCode = 1168,
			Description = "AW initial screen setting is valid"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1781u,
			SysCode = 1169,
			Description = "AW initial screen setting device type"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1782u,
			SysCode = 1170,
			SysID = 0,
			Description = "AW initial screen setting screen ID"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1783u,
			SysCode = 1170,
			SysID = 1,
			Description = "AW initial screen setting screen ID"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1784u,
			SysCode = 1171,
			SysID = 0,
			Description = "AW initial screen setting device number"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1785u,
			SysCode = 1171,
			SysID = 1,
			Description = "AW initial screen setting device number"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1786u,
			SysCode = 1172,
			Description = "AW initial screen setting display format"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1787u,
			SysCode = 1173,
			Description = "AW initial screen setting device name tag"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1788u,
			SysCode = 1174,
			Description = "AW initial screen setting key lock status"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 1789u,
			SysCode = 1175,
			Description = "AW initial screen setting display language"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2200u,
			SysCode = 1184,
			Description = "最新严重错误 years/month"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2201u,
			SysCode = 1185,
			Description = "最新严重错误 day"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2202u,
			SysCode = 1186,
			Description = "最新严重错误 when"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2203u,
			SysCode = 1187,
			Description = "最新严重错误 points"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2204u,
			SysCode = 1188,
			Description = "The latest serious error seconds"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2205u,
			SysCode = 1189,
			Description = "The latest serious misnumbering error"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2206u,
			SysCode = 1190,
			Description = "The latest number of seriously erroneous data"
		});
		for (int num14 = 0; num14 < 20; num14++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(2207 + num14),
				SysCode = 1191,
				SysID = (ushort)num14,
				Description = $"Latest serious error details {num14 + 1}"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2250u,
			SysCode = 1192,
			Description = "最新一般错误 years/month"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2251u,
			SysCode = 1193,
			Description = "最新一般错误 day"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2252u,
			SysCode = 1194,
			Description = "最新一般错误 when"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2253u,
			SysCode = 1195,
			Description = "最新一般错误 points"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2254u,
			SysCode = 1196,
			Description = "The latest general error seconds"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2255u,
			SysCode = 1197,
			Description = "The latest general error numbers"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2256u,
			SysCode = 1198,
			Description = "The latest number of general error data"
		});
		for (int num15 = 0; num15 < 20; num15++)
		{
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(2257 + num15),
				SysCode = 1199,
				SysID = (ushort)num15,
				Description = $"Latest general error details {num15 + 1}"
			});
		}
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2389u,
			SysCode = 1200,
			Description = "Expand the number of connected units"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2390u,
			SysCode = 1201,
			Description = "The memory card command uses an error code"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2391u,
			SysCode = 1202,
			Description = "The power of the memory card is OFF during access"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2392u,
			SysCode = 1203,
			Description = "The number of retries on the memory card"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2393u,
			SysCode = 1204,
			Description = "The number of errors in memory card verification"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2394u,
			SysCode = 1205,
			Description = "The number of times the memory card times out"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2395u,
			SysCode = 1206,
			Description = "Reset the delay time when the memory card malfunctions"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2400u,
			SysCode = 1216,
			Description = "Input 0 interrupt times"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2401u,
			SysCode = 1217,
			Description = "Enter 1 interrupt number"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2402u,
			SysCode = 1218,
			Description = "Enter 2 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2403u,
			SysCode = 1219,
			Description = "Enter 3 interrupt times"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2404u,
			SysCode = 1220,
			Description = "The number of CTC0 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2405u,
			SysCode = 1221,
			Description = "The number of CTC1 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2406u,
			SysCode = 1222,
			Description = "The number of CTC2 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2407u,
			SysCode = 1223,
			Description = "The number of CTC3 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2408u,
			SysCode = 1224,
			Description = "The number of CTC4 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2409u,
			SysCode = 1225,
			Description = "The number of CTC5 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2410u,
			SysCode = 1226,
			Description = "The number of CTC6 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2411u,
			SysCode = 1227,
			Description = "The number of CTC7 interrupts"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2412u,
			SysCode = 1228,
			Description = "The number of interruptions of positioning axis 1"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2413u,
			SysCode = 1229,
			Description = "The number of interruptions of positioning axis 2"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2414u,
			SysCode = 1230,
			Description = "The number of interruptions of positioning axis 3"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 2415u,
			SysCode = 1231,
			Description = "The number of interruptions of positioning axis 4"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4800u,
			SysCode = 1232,
			Description = "The direction of CTH0 change检测when间常数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4801u,
			SysCode = 1233,
			Description = "CTH0 enables reset Settings"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4802u,
			SysCode = 1234,
			SysID = 0,
			Description = "Lower limit value of CTH0 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4803u,
			SysCode = 1234,
			SysID = 1,
			Description = "Lower limit value of CTH0 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4804u,
			SysCode = 1235,
			SysID = 0,
			Description = "Upper limit value of CTH0 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4805u,
			SysCode = 1235,
			SysID = 1,
			Description = "Upper limit value of CTH0 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4806u,
			SysCode = 316,
			SysID = 0,
			Description = "CTH0 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4807u,
			SysCode = 316,
			SysID = 1,
			Description = "CTH0 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4810u,
			SysCode = 1236,
			Description = "The direction of CTH1 change检测when间常数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4811u,
			SysCode = 1237,
			Description = "CTH1 enables reset Settings"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4812u,
			SysCode = 1238,
			SysID = 0,
			Description = "Lower limit value of CTH1 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4813u,
			SysCode = 1238,
			SysID = 1,
			Description = "Lower limit value of CTH1 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4814u,
			SysCode = 1239,
			SysID = 0,
			Description = "Upper limit value of CTH1 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4815u,
			SysCode = 1239,
			SysID = 1,
			Description = "Upper limit value of CTH1 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4816u,
			SysCode = 317,
			SysID = 0,
			Description = "CTH1 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4817u,
			SysCode = 317,
			SysID = 1,
			Description = "CTH1 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4820u,
			SysCode = 1240,
			Description = "The direction of CTH2 change检测when间常数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4821u,
			SysCode = 1241,
			Description = "CTH2 enables reset Settings"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4822u,
			SysCode = 1242,
			SysID = 0,
			Description = "Lower limit value of CTH2 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4823u,
			SysCode = 1242,
			SysID = 1,
			Description = "Lower limit value of CTH2 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4824u,
			SysCode = 1243,
			SysID = 0,
			Description = "Upper limit value of CTH2 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4825u,
			SysCode = 1243,
			SysID = 1,
			Description = "Upper limit value of CTH2 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4826u,
			SysCode = 1264,
			SysID = 0,
			Description = "CTH2 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4827u,
			SysCode = 1264,
			SysID = 1,
			Description = "CTH2 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4830u,
			SysCode = 1244,
			Description = "The direction of CTH3 change检测when间常数"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4831u,
			SysCode = 1245,
			Description = "CTH3 enables reset Settings"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4832u,
			SysCode = 1246,
			SysID = 0,
			Description = "Lower limit value of CTH3 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4833u,
			SysCode = 1246,
			SysID = 1,
			Description = "Lower limit value of CTH3 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4834u,
			SysCode = 1247,
			SysID = 0,
			Description = "Upper limit value of CTH3 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4835u,
			SysCode = 1247,
			SysID = 1,
			Description = "Upper limit value of CTH3 ring counter"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4836u,
			SysCode = 1265,
			SysID = 0,
			Description = "CTH3 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4837u,
			SysCode = 1265,
			SysID = 1,
			Description = "CTH3 preset value"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4900u,
			SysCode = 1280,
			SysID = 0,
			Description = "CH0 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4901u,
			SysCode = 1280,
			SysID = 1,
			Description = "CH0 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4902u,
			SysCode = 1281,
			Description = "CH0 measurement period"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4903u,
			SysCode = 1282,
			Description = "Average number of CH0 times"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4904u,
			SysCode = 1283,
			Description = "CH0 external input source"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4906u,
			SysCode = 1284,
			SysID = 0,
			Description = "CH0 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4907u,
			SysCode = 1284,
			SysID = 1,
			Description = "CH0 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4908u,
			SysCode = 1285,
			Description = "The duty cycle of CH0 pulse output"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4910u,
			SysCode = 1296,
			SysID = 0,
			Description = "CH1 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4911u,
			SysCode = 1296,
			SysID = 1,
			Description = "CH1 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4912u,
			SysCode = 1297,
			Description = "CH1 measurement period"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4913u,
			SysCode = 1298,
			Description = "Average number of CH1 cycles"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4914u,
			SysCode = 1299,
			Description = "CH1 External input source"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4916u,
			SysCode = 1300,
			SysID = 0,
			Description = "CH1 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4917u,
			SysCode = 1300,
			SysID = 1,
			Description = "CH1 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4918u,
			SysCode = 1301,
			Description = "The duty cycle of CH1 pulse output"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4920u,
			SysCode = 1312,
			SysID = 0,
			Description = "CH2 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4921u,
			SysCode = 1312,
			SysID = 1,
			Description = "CH2 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4922u,
			SysCode = 1313,
			Description = "CH2 measurement period"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4923u,
			SysCode = 1314,
			Description = "Average number of CH2 cycles"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4924u,
			SysCode = 1315,
			Description = "CH2 external input source"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4926u,
			SysCode = 1316,
			SysID = 0,
			Description = "Set frequency for CH2 pulse output"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4927u,
			SysCode = 1316,
			SysID = 1,
			Description = "Set frequency for CH2 pulse output"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4928u,
			SysCode = 1317,
			Description = "The duty cycle of CH2 pulse output"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4930u,
			SysCode = 1328,
			SysID = 0,
			Description = "CH3 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4931u,
			SysCode = 1328,
			SysID = 1,
			Description = "CH3 measurement result"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4932u,
			SysCode = 1329,
			Description = "CH3 measurement period"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4933u,
			SysCode = 1330,
			Description = "Average number of CH3 times"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4934u,
			SysCode = 1331,
			Description = "CH3 external input source"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4936u,
			SysCode = 1332,
			SysID = 0,
			Description = "CH3 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4937u,
			SysCode = 1332,
			SysID = 1,
			Description = "CH3 pulse output set frequency"
		});
		List_N_CM.Add(new KVSSystemValue
		{
			Code = 7,
			Offset = 4938u,
			SysCode = 1333,
			Description = "The duty cycle of CH3 pulse output"
		});
		for (int num16 = 1; num16 <= 4; num16++)
		{
			for (int num17 = 1; num17 <= 20; num17++)
			{
				string text = $"Axis {num16} point {num17}";
				int num18 = (num16 - 1) * 20 + (num17 - 1);
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8000 + num18 * 10),
					SysCode = 1344,
					SysID = (ushort)(num18 * 2),
					Description = text + "Target coordinates/movement amount"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8001 + num18 * 10),
					SysCode = 1344,
					SysID = (ushort)(num18 * 2 + 1),
					Description = text + "Target coordinates/movement amount"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8002 + num18 * 10),
					SysCode = 1345,
					SysID = (ushort)num18,
					Description = text + "Acceleration"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8003 + num18 * 10),
					SysCode = 1346,
					SysID = (ushort)num18,
					Description = text + "Deceleration"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8004 + num18 * 10),
					SysCode = 1347,
					SysID = (ushort)(num18 * 2),
					Description = text + "Operating speed"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8005 + num18 * 10),
					SysCode = 1347,
					SysID = (ushort)(num18 * 2 + 1),
					Description = text + "Operating speed"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8006 + num18 * 10),
					SysCode = 1348,
					SysID = (ushort)num18,
					Description = text + "Action mode"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8007 + num18 * 10),
					SysCode = 1349,
					SysID = (ushort)num18,
					Description = text + "Stop the movement after the sensor input"
				});
			}
		}
		for (int num19 = 0; num19 < 4; num19++)
		{
			string text2 = $"Axis {num19 + 1}";
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8800 + num19 * 40),
				SysCode = 1344,
				SysID = (ushort)num19,
				Description = text2 + "I/O Settings"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8801 + num19 * 40),
				SysCode = 1351,
				SysID = (ushort)num19,
				Description = text2 + "Sensor enable"
			});
			for (int num20 = 0; num20 < 3; num20++)
			{
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8802 + num19 * 40 + num20 * 2),
					SysCode = 1352,
					SysID = (ushort)(num19 * 6 + num20 * 2),
					Description = text2 + "I/O Settings"
				});
				List_N_CM.Add(new KVSSystemValue
				{
					Code = 7,
					Offset = (uint)(8803 + num19 * 40 + num20 * 2),
					SysCode = 1352,
					SysID = (ushort)(num19 * 6 + num20 * 2 + 1),
					Description = text2 + "Sensor enable"
				});
			}
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8808 + num19 * 40),
				SysCode = 1353,
				SysID = (ushort)num19,
				Description = text2 + "Starting speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8809 + num19 * 40),
				SysCode = 1354,
				SysID = (ushort)num19,
				Description = text2 + "Return to the starting speed from the origin"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8810 + num19 * 40),
				SysCode = 1355,
				SysID = (ushort)num19,
				Description = text2 + "Acceleration rate of origin return"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8811 + num19 * 40),
				SysCode = 1356,
				SysID = (ushort)num19,
				Description = text2 + "The rate of reduction due to the return to the origin"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8812 + num19 * 40),
				SysCode = 1357,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "The original point returns to the rotational speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8813 + num19 * 40),
				SysCode = 1357,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "The original point returns to the rotational speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8814 + num19 * 40),
				SysCode = 1358,
				SysID = (ushort)num19,
				Description = text2 + "Detailed Settings for origin reset"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8815 + num19 * 40),
				SysCode = 1359,
				SysID = (ushort)num19,
				Description = text2 + "JOG starting speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8816 + num19 * 40),
				SysCode = 1360,
				SysID = (ushort)num19,
				Description = text2 + "JOG acceleration rate"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8817 + num19 * 40),
				SysCode = 1361,
				SysID = (ushort)num19,
				Description = text2 + "JOG to reduce speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8818 + num19 * 40),
				SysCode = 1362,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "JOG running speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8819 + num19 * 40),
				SysCode = 1362,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "JOG running speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8820 + num19 * 40),
				SysCode = 1363,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "The original position coordinate setting value"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8821 + num19 * 40),
				SysCode = 1363,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "The original position coordinate setting value"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8824 + num19 * 40),
				SysCode = 1364,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "Current coordinate更改设定值"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8825 + num19 * 40),
				SysCode = 1364,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "Current coordinate更改设定值"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8826 + num19 * 40),
				SysCode = 1365,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "Operating speed更改设定值"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8827 + num19 * 40),
				SysCode = 1365,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "Operating speed更改设定值"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8828 + num19 * 40),
				SysCode = 1366,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "Change the set value of the target coordinate"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8829 + num19 * 40),
				SysCode = 1366,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "Change the set value of the target coordinate"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8830 + num19 * 40),
				SysCode = 1367,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "Current coordinate"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8831 + num19 * 40),
				SysCode = 1367,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "Current coordinate"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8832 + num19 * 40),
				SysCode = 1368,
				SysID = (ushort)(num19 * 2),
				Description = text2 + "Current speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8833 + num19 * 40),
				SysCode = 1368,
				SysID = (ushort)(num19 * 2 + 1),
				Description = text2 + "Current speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8834 + num19 * 40),
				SysCode = 1369,
				SysID = (ushort)num19,
				Description = text2 + "Current speed"
			});
			List_N_CM.Add(new KVSSystemValue
			{
				Code = 7,
				Offset = (uint)(8835 + num19 * 40),
				SysCode = 1370,
				SysID = (ushort)num19,
				Description = text2 + "Current speed"
			});
		}
	}

	public KVSSystemValue Clone()
	{
		return new KVSSystemValue
		{
			Code = code,
			Offset = offset,
			Description = description,
			SysCode = syscode,
			SysID = sysid
		};
	}
}
