using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SamSoarII.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("SamSoarII.Utility.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	public static string About => ResourceManager.GetString("About", resourceCulture);

	public static string About_SamSoarII => ResourceManager.GetString("About_SamSoarII", resourceCulture);

	public static string Absolute => ResourceManager.GetString("Absolute", resourceCulture);

	public static string AccelerateTime => ResourceManager.GetString("AccelerateTime", resourceCulture);

	public static string ACDist_Error => ResourceManager.GetString("ACDist_Error", resourceCulture);

	public static string Add => ResourceManager.GetString("Add", resourceCulture);

	public static string Add_Element => ResourceManager.GetString("Add_Element", resourceCulture);

	public static string Add_FuncBlock => ResourceManager.GetString("Add_FuncBlock", resourceCulture);

	public static string Add_Modbus_Table => ResourceManager.GetString("Add_Modbus_Table", resourceCulture);

	public static string Add_Operation_Forbidden => ResourceManager.GetString("Add_Operation_Forbidden", resourceCulture);

	public static string Add_SubRoutine => ResourceManager.GetString("Add_SubRoutine", resourceCulture);

	public static string AddNewSubRoutine => ResourceManager.GetString("AddNewSubRoutine", resourceCulture);

	public static string Address => ResourceManager.GetString("Address", resourceCulture);

	public static string Address_Cross => ResourceManager.GetString("Address_Cross", resourceCulture);

	public static string AIOAnalogMode_0_10_V => ResourceManager.GetString("AIOAnalogMode_0_10_V", resourceCulture);

	public static string AIOAnalogMode_0_5_V => ResourceManager.GetString("AIOAnalogMode_0_5_V", resourceCulture);

	public static string AIOAnalogMode_4_20_mA => ResourceManager.GetString("AIOAnalogMode_4_20_mA", resourceCulture);

	public static string AIOAnalogMode_K_Hot => ResourceManager.GetString("AIOAnalogMode_K_Hot", resourceCulture);

	public static string AIOAnalogMode_PT100 => ResourceManager.GetString("AIOAnalogMode_PT100", resourceCulture);

	public static string AIOAnalogMode_T_Hot => ResourceManager.GetString("AIOAnalogMode_T_Hot", resourceCulture);

	public static string All_Information => ResourceManager.GetString("All_Information", resourceCulture);

	public static string All_Judgments => ResourceManager.GetString("All_Judgments", resourceCulture);

	public static string All_Lifted => ResourceManager.GetString("All_Lifted", resourceCulture);

	public static string All_Networks => ResourceManager.GetString("All_Networks", resourceCulture);

	public static string All_Routine => ResourceManager.GetString("All_Routine", resourceCulture);

	public static string All_Text => ResourceManager.GetString("All_Text", resourceCulture);

	public static string All_Types => ResourceManager.GetString("All_Types", resourceCulture);

	public static string AllEdge => ResourceManager.GetString("AllEdge", resourceCulture);

	public static string Alternating_Output => ResourceManager.GetString("Alternating_Output", resourceCulture);

	public static string Annotated_Element => ResourceManager.GetString("Annotated_Element", resourceCulture);

	public static string Annotation_Edit => ResourceManager.GetString("Annotation_Edit", resourceCulture);

	public static string ARCF_Inst => ResourceManager.GetString("ARCF_Inst", resourceCulture);

	public static string ARCI_Inst => ResourceManager.GetString("ARCI_Inst", resourceCulture);

	public static string Argument => ResourceManager.GetString("Argument", resourceCulture);

	public static string ArgumentSpecial_CV => ResourceManager.GetString("ArgumentSpecial_CV", resourceCulture);

	public static string ArgumentSpecial_TV => ResourceManager.GetString("ArgumentSpecial_TV", resourceCulture);

	public static string ArgumentSpecial_Y => ResourceManager.GetString("ArgumentSpecial_Y", resourceCulture);

	public static string ATCH_CF => ResourceManager.GetString("ATCH_CF", resourceCulture);

	public static string ATCH_DE => ResourceManager.GetString("ATCH_DE", resourceCulture);

	public static string ATCH_E => ResourceManager.GetString("ATCH_E", resourceCulture);

	public static string ATCH_Inst => ResourceManager.GetString("ATCH_Inst", resourceCulture);

	public static string ATCH_TIM0 => ResourceManager.GetString("ATCH_TIM0", resourceCulture);

	public static string ATCH_TIM1 => ResourceManager.GetString("ATCH_TIM1", resourceCulture);

	public static string ATCH_UE => ResourceManager.GetString("ATCH_UE", resourceCulture);

	public static string ATCH_YF => ResourceManager.GetString("ATCH_YF", resourceCulture);

	public static string Auto_Check => ResourceManager.GetString("Auto_Check", resourceCulture);

	public static string Auto_Check_BaudrateOnly => ResourceManager.GetString("Auto_Check_BaudrateOnly", resourceCulture);

	public static string Auto_Checking => ResourceManager.GetString("Auto_Checking", resourceCulture);

	public static string AutoGenerateLine => ResourceManager.GetString("AutoGenerateLine", resourceCulture);

	public static string AutoGenerateNetwork => ResourceManager.GetString("AutoGenerateNetwork", resourceCulture);

	public static string AutoMeasure => ResourceManager.GetString("AutoMeasure", resourceCulture);

	public static string AutoMeasure_SwitchPLCToStop => ResourceManager.GetString("AutoMeasure_SwitchPLCToStop", resourceCulture);

	public static string AuxiliaryWindow => ResourceManager.GetString("AuxiliaryWindow", resourceCulture);

	public static string Average_Number_Of_Samples => ResourceManager.GetString("Average_Number_Of_Samples", resourceCulture);

	public static string Average_Number_Of_Samples_AIAO => ResourceManager.GetString("Average_Number_Of_Samples_AIAO", resourceCulture);

	public static string Batch => ResourceManager.GetString("Batch", resourceCulture);

	public static string Baud_Rate => ResourceManager.GetString("Baud_Rate", resourceCulture);

	public static string BCD_Code => ResourceManager.GetString("BCD_Code", resourceCulture);

	public static string BCD_Code_To_Integer => ResourceManager.GetString("BCD_Code_To_Integer", resourceCulture);

	public static string Bit => ResourceManager.GetString("Bit", resourceCulture);

	public static string BLOCK_Inst => ResourceManager.GetString("BLOCK_Inst", resourceCulture);

	public static string BranchOver_Error => ResourceManager.GetString("BranchOver_Error", resourceCulture);

	public static string Breakpoint_Active => ResourceManager.GetString("Breakpoint_Active", resourceCulture);

	public static string Breakpoint_List => ResourceManager.GetString("Breakpoint_List", resourceCulture);

	public static string Breakpoint_Unactive => ResourceManager.GetString("Breakpoint_Unactive", resourceCulture);

	public static string Brief_Description => ResourceManager.GetString("Brief_Description", resourceCulture);

	public static string Browse => ResourceManager.GetString("Browse", resourceCulture);

	public static string Buffer_Bit => ResourceManager.GetString("Buffer_Bit", resourceCulture);

	public static string CAD_DOWNLOAD_PACKNUM_ERROR => ResourceManager.GetString("CAD_DOWNLOAD_PACKNUM_ERROR", resourceCulture);

	public static string CAD_FLASH_ERROR => ResourceManager.GetString("CAD_FLASH_ERROR", resourceCulture);

	public static string CAD_FLASH_WR_ERROR => ResourceManager.GetString("CAD_FLASH_WR_ERROR", resourceCulture);

	public static string CAD_READ_DRAWING_ERROR => ResourceManager.GetString("CAD_READ_DRAWING_ERROR", resourceCulture);

	public static string CalendarFormatError => ResourceManager.GetString("CalendarFormatError", resourceCulture);

	public static string CALL_Inst => ResourceManager.GetString("CALL_Inst", resourceCulture);

	public static string CALLM_Inst => ResourceManager.GetString("CALLM_Inst", resourceCulture);

	public static string CAM_DATA_ERROR => ResourceManager.GetString("CAM_DATA_ERROR", resourceCulture);

	public static string CAM_MaxValue => ResourceManager.GetString("CAM_MaxValue", resourceCulture);

	public static string CAM_NumberStored => ResourceManager.GetString("CAM_NumberStored", resourceCulture);

	public static string Can_Not_Add_Element => ResourceManager.GetString("Can_Not_Add_Element", resourceCulture);

	public static string Can_Not_Stop => ResourceManager.GetString("Can_Not_Stop", resourceCulture);

	public static string Cancel => ResourceManager.GetString("Cancel", resourceCulture);

	public static string Cancel_All_Force => ResourceManager.GetString("Cancel_All_Force", resourceCulture);

	public static string Cancel_Force => ResourceManager.GetString("Cancel_Force", resourceCulture);

	public static string Cancel_Handle => ResourceManager.GetString("Cancel_Handle", resourceCulture);

	public static string Change_Mode => ResourceManager.GetString("Change_Mode", resourceCulture);

	public static string Channel => ResourceManager.GetString("Channel", resourceCulture);

	public static string Check_Code => ResourceManager.GetString("Check_Code", resourceCulture);

	public static string Check_Coil_Output => ResourceManager.GetString("Check_Coil_Output", resourceCulture);

	public static string Check_Counter => ResourceManager.GetString("Check_Counter", resourceCulture);

	public static string Check_Setting_Header => ResourceManager.GetString("Check_Setting_Header", resourceCulture);

	public static string Check_Timer => ResourceManager.GetString("Check_Timer", resourceCulture);

	public static string Checking_Params => ResourceManager.GetString("Checking_Params", resourceCulture);

	public static string Chinese => ResourceManager.GetString("Chinese", resourceCulture);

	public static string CKCMP_Inst => ResourceManager.GetString("CKCMP_Inst", resourceCulture);

	public static string CKZCP_Inst => ResourceManager.GetString("CKZCP_Inst", resourceCulture);

	public static string Clear => ResourceManager.GetString("Clear", resourceCulture);

	public static string Clear_Selected => ResourceManager.GetString("Clear_Selected", resourceCulture);

	public static string ClickToUpdate => ResourceManager.GetString("ClickToUpdate", resourceCulture);

	public static string ClockTimeFormatError => ResourceManager.GetString("ClockTimeFormatError", resourceCulture);

	public static string Close => ResourceManager.GetString("Close", resourceCulture);

	public static string Close_Current_Task => ResourceManager.GetString("Close_Current_Task", resourceCulture);

	public static string Close_Dialog => ResourceManager.GetString("Close_Dialog", resourceCulture);

	public static string Close_Proj => ResourceManager.GetString("Close_Proj", resourceCulture);

	public static string CML_Inst => ResourceManager.GetString("CML_Inst", resourceCulture);

	public static string CMLD_Inst => ResourceManager.GetString("CMLD_Inst", resourceCulture);

	public static string CMP_Inst => ResourceManager.GetString("CMP_Inst", resourceCulture);

	public static string CMPD_Inst => ResourceManager.GetString("CMPD_Inst", resourceCulture);

	public static string CMPF_Inst => ResourceManager.GetString("CMPF_Inst", resourceCulture);

	public static string Collapsed => ResourceManager.GetString("Collapsed", resourceCulture);

	public static string Collapsed_All => ResourceManager.GetString("Collapsed_All", resourceCulture);

	public static string Column_Out_Of_Range => ResourceManager.GetString("Column_Out_Of_Range", resourceCulture);

	public static string ColumnInsertAfter => ResourceManager.GetString("ColumnInsertAfter", resourceCulture);

	public static string ColumnInsertBefore => ResourceManager.GetString("ColumnInsertBefore", resourceCulture);

	public static string ColumnInsertEnd => ResourceManager.GetString("ColumnInsertEnd", resourceCulture);

	public static string Columns => ResourceManager.GetString("Columns", resourceCulture);

	public static string Columns_Of_Ladder_Setting => ResourceManager.GetString("Columns_Of_Ladder_Setting", resourceCulture);

	public static string COM_DOWNLOAD_TIMEOUT_ERROR => ResourceManager.GetString("COM_DOWNLOAD_TIMEOUT_ERROR", resourceCulture);

	public static string Combination => ResourceManager.GetString("Combination", resourceCulture);

	public static string ComboBox_NewFuncBlock => ResourceManager.GetString("ComboBox_NewFuncBlock", resourceCulture);

	public static string ComboBox_None => ResourceManager.GetString("ComboBox_None", resourceCulture);

	public static string Command_Not_Execute => ResourceManager.GetString("Command_Not_Execute", resourceCulture);

	public static string Comment => ResourceManager.GetString("Comment", resourceCulture);

	public static string Comment_Mode => ResourceManager.GetString("Comment_Mode", resourceCulture);

	public static string CommParamsDialog_More => ResourceManager.GetString("CommParamsDialog_More", resourceCulture);

	public static string CommProtocol => ResourceManager.GetString("CommProtocol", resourceCulture);

	public static string Communication_Download_Data => ResourceManager.GetString("Communication_Download_Data", resourceCulture);

	public static string Communication_Error => ResourceManager.GetString("Communication_Error", resourceCulture);

	public static string Communication_Parameter_Setting => ResourceManager.GetString("Communication_Parameter_Setting", resourceCulture);

	public static string Communication_Test => ResourceManager.GetString("Communication_Test", resourceCulture);

	public static string Communication_Upload_Data => ResourceManager.GetString("Communication_Upload_Data", resourceCulture);

	public static string CommunicationSetting => ResourceManager.GetString("CommunicationSetting", resourceCulture);

	public static string Compare_Goals => ResourceManager.GetString("Compare_Goals", resourceCulture);

	public static string Compare_Source => ResourceManager.GetString("Compare_Source", resourceCulture);

	public static string Compare_Type => ResourceManager.GetString("Compare_Type", resourceCulture);

	public static string Compile => ResourceManager.GetString("Compile", resourceCulture);

	public static string Compile_Failed => ResourceManager.GetString("Compile_Failed", resourceCulture);

	public static string Compiled_Success => ResourceManager.GetString("Compiled_Success", resourceCulture);

	public static string Compiling => ResourceManager.GetString("Compiling", resourceCulture);

	public static string Condition => ResourceManager.GetString("Condition", resourceCulture);

	public static string Condition_Add => ResourceManager.GetString("Condition_Add", resourceCulture);

	public static string Config_Applied => ResourceManager.GetString("Config_Applied", resourceCulture);

	public static string Config_Applied_NewProj => ResourceManager.GetString("Config_Applied_NewProj", resourceCulture);

	public static string Config_Buffer_Error => ResourceManager.GetString("Config_Buffer_Error", resourceCulture);

	public static string Config_Download => ResourceManager.GetString("Config_Download", resourceCulture);

	public static string Config_Override => ResourceManager.GetString("Config_Override", resourceCulture);

	public static string Config_Upload => ResourceManager.GetString("Config_Upload", resourceCulture);

	public static string Connect_Failed => ResourceManager.GetString("Connect_Failed", resourceCulture);

	public static string Connection_Mode => ResourceManager.GetString("Connection_Mode", resourceCulture);

	public static string Constant_Monitor => ResourceManager.GetString("Constant_Monitor", resourceCulture);

	public static string ContainLibFunc => ResourceManager.GetString("ContainLibFunc", resourceCulture);

	public static string Content => ResourceManager.GetString("Content", resourceCulture);

	public static string Continue_Upload => ResourceManager.GetString("Continue_Upload", resourceCulture);

	public static string ConvenienceSetting => ResourceManager.GetString("ConvenienceSetting", resourceCulture);

	public static string Convert => ResourceManager.GetString("Convert", resourceCulture);

	public static string Convert_File_Not_Exist => ResourceManager.GetString("Convert_File_Not_Exist", resourceCulture);

	public static string Coordinate => ResourceManager.GetString("Coordinate", resourceCulture);

	public static string Copy => ResourceManager.GetString("Copy", resourceCulture);

	public static string Copyright_Owner => ResourceManager.GetString("Copyright_Owner", resourceCulture);

	public static string Cos_Operation => ResourceManager.GetString("Cos_Operation", resourceCulture);

	public static string Counter => ResourceManager.GetString("Counter", resourceCulture);

	public static string CRC_Address => ResourceManager.GetString("CRC_Address", resourceCulture);

	public static string CRC_Length => ResourceManager.GetString("CRC_Length", resourceCulture);

	public static string CRC_Mode => ResourceManager.GetString("CRC_Mode", resourceCulture);

	public static string CRC_Output => ResourceManager.GetString("CRC_Output", resourceCulture);

	public static string Create_Ladder_Func => ResourceManager.GetString("Create_Ladder_Func", resourceCulture);

	public static string Create_Params_Func => ResourceManager.GetString("Create_Params_Func", resourceCulture);

	public static string CreatedByBlock => ResourceManager.GetString("CreatedByBlock", resourceCulture);

	public static string CreateSubRoutineDialog_Comment => ResourceManager.GetString("CreateSubRoutineDialog_Comment", resourceCulture);

	public static string CreateSubRoutineDialog_FBD => ResourceManager.GetString("CreateSubRoutineDialog_FBD", resourceCulture);

	public static string CreateSubRoutineDialog_LAD => ResourceManager.GetString("CreateSubRoutineDialog_LAD", resourceCulture);

	public static string CreateSubRoutineDialog_Name => ResourceManager.GetString("CreateSubRoutineDialog_Name", resourceCulture);

	public static string CreateSubRoutineDialog_NetworkCount => ResourceManager.GetString("CreateSubRoutineDialog_NetworkCount", resourceCulture);

	public static string CreateSubRoutineDialog_Title => ResourceManager.GetString("CreateSubRoutineDialog_Title", resourceCulture);

	public static string CreateSubRoutineDialog_Type => ResourceManager.GetString("CreateSubRoutineDialog_Type", resourceCulture);

	public static string CSV_Export => ResourceManager.GetString("CSV_Export", resourceCulture);

	public static string CSV_Import => ResourceManager.GetString("CSV_Import", resourceCulture);

	public static string CTD_Inst => ResourceManager.GetString("CTD_Inst", resourceCulture);

	public static string CTU_Inst => ResourceManager.GetString("CTU_Inst", resourceCulture);

	public static string CTUD_Inst => ResourceManager.GetString("CTUD_Inst", resourceCulture);

	public static string Current_Color => ResourceManager.GetString("Current_Color", resourceCulture);

	public static string Current_Routine => ResourceManager.GetString("Current_Routine", resourceCulture);

	public static string Current_Text => ResourceManager.GetString("Current_Text", resourceCulture);

	public static string Current_Value_Of_Samples => ResourceManager.GetString("Current_Value_Of_Samples", resourceCulture);

	public static string Current_Value_Of_Samples_4TC => ResourceManager.GetString("Current_Value_Of_Samples_4TC", resourceCulture);

	public static string CurrentNotLadder => ResourceManager.GetString("CurrentNotLadder", resourceCulture);

	public static string CurrentProjectCannotUpload => ResourceManager.GetString("CurrentProjectCannotUpload", resourceCulture);

	public static string Custom => ResourceManager.GetString("Custom", resourceCulture);

	public static string Cut => ResourceManager.GetString("Cut", resourceCulture);

	public static string CV32RegisterRange => ResourceManager.GetString("CV32RegisterRange", resourceCulture);

	public static string CVRegisterRange => ResourceManager.GetString("CVRegisterRange", resourceCulture);

	public static string Data_Bit => ResourceManager.GetString("Data_Bit", resourceCulture);

	public static string Data_Bits => ResourceManager.GetString("Data_Bits", resourceCulture);

	public static string Data_Type => ResourceManager.GetString("Data_Type", resourceCulture);

	public static string Data_Uplaod_Empty => ResourceManager.GetString("Data_Uplaod_Empty", resourceCulture);

	public static string Data_Uplaod_Error => ResourceManager.GetString("Data_Uplaod_Error", resourceCulture);

	public static string DataGrid_Disabled => ResourceManager.GetString("DataGrid_Disabled", resourceCulture);

	public static string DataGrid_Enabled => ResourceManager.GetString("DataGrid_Enabled", resourceCulture);

	public static string DataGrid_Invalid => ResourceManager.GetString("DataGrid_Invalid", resourceCulture);

	public static string DDRVA_Inst => ResourceManager.GetString("DDRVA_Inst", resourceCulture);

	public static string DDRVI_Inst => ResourceManager.GetString("DDRVI_Inst", resourceCulture);

	public static string Decimal => ResourceManager.GetString("Decimal", resourceCulture);

	public static string Default => ResourceManager.GetString("Default", resourceCulture);

	public static string Del => ResourceManager.GetString("Del", resourceCulture);

	public static string Delete => ResourceManager.GetString("Delete", resourceCulture);

	public static string Delete_All => ResourceManager.GetString("Delete_All", resourceCulture);

	public static string Demo_Message_Element_Comment => ResourceManager.GetString("Demo_Message_Element_Comment", resourceCulture);

	public static string DemoCanvas_DiagramComment => ResourceManager.GetString("DemoCanvas_DiagramComment", resourceCulture);

	public static string DemoCanvas_ElementComment => ResourceManager.GetString("DemoCanvas_ElementComment", resourceCulture);

	public static string DemoCanvas_EtcDiagram => ResourceManager.GetString("DemoCanvas_EtcDiagram", resourceCulture);

	public static string DemoCanvas_NetworkBrief => ResourceManager.GetString("DemoCanvas_NetworkBrief", resourceCulture);

	public static string DemoCanvas_NetworkComment => ResourceManager.GetString("DemoCanvas_NetworkComment", resourceCulture);

	public static string DESTIP_Inst => ResourceManager.GetString("DESTIP_Inst", resourceCulture);

	public static string Detailed_Description => ResourceManager.GetString("Detailed_Description", resourceCulture);

	public static string DeviceDialog_AIRange => ResourceManager.GetString("DeviceDialog_AIRange", resourceCulture);

	public static string DeviceDialog_AORange => ResourceManager.GetString("DeviceDialog_AORange", resourceCulture);

	public static string DeviceDialog_CRange => ResourceManager.GetString("DeviceDialog_CRange", resourceCulture);

	public static string DeviceDialog_CVRange => ResourceManager.GetString("DeviceDialog_CVRange", resourceCulture);

	public static string DeviceDialog_Details => ResourceManager.GetString("DeviceDialog_Details", resourceCulture);

	public static string DeviceDialog_DRange => ResourceManager.GetString("DeviceDialog_DRange", resourceCulture);

	public static string DeviceDialog_MRange => ResourceManager.GetString("DeviceDialog_MRange", resourceCulture);

	public static string DeviceDialog_SRange => ResourceManager.GetString("DeviceDialog_SRange", resourceCulture);

	public static string DeviceDialog_TRange => ResourceManager.GetString("DeviceDialog_TRange", resourceCulture);

	public static string DeviceDialog_TVRange => ResourceManager.GetString("DeviceDialog_TVRange", resourceCulture);

	public static string DeviceDialog_XRange => ResourceManager.GetString("DeviceDialog_XRange", resourceCulture);

	public static string DeviceDialog_YRange => ResourceManager.GetString("DeviceDialog_YRange", resourceCulture);

	public static string DI_Inst => ResourceManager.GetString("DI_Inst", resourceCulture);

	public static string Diagram => ResourceManager.GetString("Diagram", resourceCulture);

	public static string DiagramArgument_Comment => ResourceManager.GetString("DiagramArgument_Comment", resourceCulture);

	public static string DiagramArgument_Header => ResourceManager.GetString("DiagramArgument_Header", resourceCulture);

	public static string DiagramArgument_ID => ResourceManager.GetString("DiagramArgument_ID", resourceCulture);

	public static string DiagramArgument_IOAccess => ResourceManager.GetString("DiagramArgument_IOAccess", resourceCulture);

	public static string DiagramArgument_Name => ResourceManager.GetString("DiagramArgument_Name", resourceCulture);

	public static string DiagramArgument_ValueType => ResourceManager.GetString("DiagramArgument_ValueType", resourceCulture);

	public static string Dialog_Closing => ResourceManager.GetString("Dialog_Closing", resourceCulture);

	public static string Differential_Time => ResourceManager.GetString("Differential_Time", resourceCulture);

	public static string Digital_Quantity_Range => ResourceManager.GetString("Digital_Quantity_Range", resourceCulture);

	public static string Direction_Setting => ResourceManager.GetString("Direction_Setting", resourceCulture);

	public static string Direction_Sign => ResourceManager.GetString("Direction_Sign", resourceCulture);

	public static string DirectionOutput => ResourceManager.GetString("DirectionOutput", resourceCulture);

	public static string DisableReflictionToAddress => ResourceManager.GetString("DisableReflictionToAddress", resourceCulture);

	public static string Displays_Details => ResourceManager.GetString("Displays_Details", resourceCulture);

	public static string Divide => ResourceManager.GetString("Divide", resourceCulture);

	public static string Download => ResourceManager.GetString("Download", resourceCulture);

	public static string Download_Encryption => ResourceManager.GetString("Download_Encryption", resourceCulture);

	public static string Download_Fail => ResourceManager.GetString("Download_Fail", resourceCulture);

	public static string Download_Failed => ResourceManager.GetString("Download_Failed", resourceCulture);

	public static string Download_Size_Beyond => ResourceManager.GetString("Download_Size_Beyond", resourceCulture);

	public static string Downloading => ResourceManager.GetString("Downloading", resourceCulture);

	public static string DownloadPassword_Accept => ResourceManager.GetString("DownloadPassword_Accept", resourceCulture);

	public static string DownloadPassword_Error => ResourceManager.GetString("DownloadPassword_Error", resourceCulture);

	public static string DownloadPassword_Message => ResourceManager.GetString("DownloadPassword_Message", resourceCulture);

	public static string DownloadPassword_Title => ResourceManager.GetString("DownloadPassword_Title", resourceCulture);

	public static string DownloadPasswordNotion => ResourceManager.GetString("DownloadPasswordNotion", resourceCulture);

	public static string DPLSA_Inst => ResourceManager.GetString("DPLSA_Inst", resourceCulture);

	public static string DPLSF_Inst => ResourceManager.GetString("DPLSF_Inst", resourceCulture);

	public static string DPLSR_Inst => ResourceManager.GetString("DPLSR_Inst", resourceCulture);

	public static string DPLSRD_Inst => ResourceManager.GetString("DPLSRD_Inst", resourceCulture);

	public static string DPLSY_Inst => ResourceManager.GetString("DPLSY_Inst", resourceCulture);

	public static string DPWM_Inst => ResourceManager.GetString("DPWM_Inst", resourceCulture);

	public static string DRegisterRange => ResourceManager.GetString("DRegisterRange", resourceCulture);

	public static string DRVA_Inst => ResourceManager.GetString("DRVA_Inst", resourceCulture);

	public static string DRVI_Inst => ResourceManager.GetString("DRVI_Inst", resourceCulture);

	public static string DTCH_Inst => ResourceManager.GetString("DTCH_Inst", resourceCulture);

	public static string DVIT_AccelerateTime => ResourceManager.GetString("DVIT_AccelerateTime", resourceCulture);

	public static string DVIT_Enable => ResourceManager.GetString("DVIT_Enable", resourceCulture);

	public static string DVIT_EnableSignal => ResourceManager.GetString("DVIT_EnableSignal", resourceCulture);

	public static string DVIT_Frequency => ResourceManager.GetString("DVIT_Frequency", resourceCulture);

	public static string DVIT_InterruptEvent => ResourceManager.GetString("DVIT_InterruptEvent", resourceCulture);

	public static string DVIT_InterruptSignal => ResourceManager.GetString("DVIT_InterruptSignal", resourceCulture);

	public static string DVIT_PulseDirection => ResourceManager.GetString("DVIT_PulseDirection", resourceCulture);

	public static string DVIT_PulseNumber => ResourceManager.GetString("DVIT_PulseNumber", resourceCulture);

	public static string DVIT_PulseOutput => ResourceManager.GetString("DVIT_PulseOutput", resourceCulture);

	public static string DVITEnableSignal_AlwaysOn => ResourceManager.GetString("DVITEnableSignal_AlwaysOn", resourceCulture);

	public static string DVITEnableSignal_M => ResourceManager.GetString("DVITEnableSignal_M", resourceCulture);

	public static string DVITEnableSignal_X => ResourceManager.GetString("DVITEnableSignal_X", resourceCulture);

	public static string DVITEnableSignal_Y => ResourceManager.GetString("DVITEnableSignal_Y", resourceCulture);

	public static string DVITInterruption_M_Edge => ResourceManager.GetString("DVITInterruption_M_Edge", resourceCulture);

	public static string DVITInterruption_M_FallingEdge => ResourceManager.GetString("DVITInterruption_M_FallingEdge", resourceCulture);

	public static string DVITInterruption_M_RisingEdge => ResourceManager.GetString("DVITInterruption_M_RisingEdge", resourceCulture);

	public static string DVITInterruption_X_Edge => ResourceManager.GetString("DVITInterruption_X_Edge", resourceCulture);

	public static string DVITInterruption_X_FallingEdge => ResourceManager.GetString("DVITInterruption_X_FallingEdge", resourceCulture);

	public static string DVITInterruption_X_RisingEdge => ResourceManager.GetString("DVITInterruption_X_RisingEdge", resourceCulture);

	public static string DVITInterruption_X0_Edge => ResourceManager.GetString("DVITInterruption_X0_Edge", resourceCulture);

	public static string DVITInterruption_X0_FallingEdge => ResourceManager.GetString("DVITInterruption_X0_FallingEdge", resourceCulture);

	public static string DVITInterruption_X0_RisingEdge => ResourceManager.GetString("DVITInterruption_X0_RisingEdge", resourceCulture);

	public static string DVITInterruption_X1_Edge => ResourceManager.GetString("DVITInterruption_X1_Edge", resourceCulture);

	public static string DVITInterruption_X1_FallingEdge => ResourceManager.GetString("DVITInterruption_X1_FallingEdge", resourceCulture);

	public static string DVITInterruption_X1_RisingEdge => ResourceManager.GetString("DVITInterruption_X1_RisingEdge", resourceCulture);

	public static string DVITInterruption_X2_Edge => ResourceManager.GetString("DVITInterruption_X2_Edge", resourceCulture);

	public static string DVITInterruption_X2_FallingEdge => ResourceManager.GetString("DVITInterruption_X2_FallingEdge", resourceCulture);

	public static string DVITInterruption_X2_RisingEdge => ResourceManager.GetString("DVITInterruption_X2_RisingEdge", resourceCulture);

	public static string DVITInterruption_X3_Edge => ResourceManager.GetString("DVITInterruption_X3_Edge", resourceCulture);

	public static string DVITInterruption_X3_FallingEdge => ResourceManager.GetString("DVITInterruption_X3_FallingEdge", resourceCulture);

	public static string DVITInterruption_X3_RisingEdge => ResourceManager.GetString("DVITInterruption_X3_RisingEdge", resourceCulture);

	public static string DVITInterruption_X4_Edge => ResourceManager.GetString("DVITInterruption_X4_Edge", resourceCulture);

	public static string DVITInterruption_X4_FallingEdge => ResourceManager.GetString("DVITInterruption_X4_FallingEdge", resourceCulture);

	public static string DVITInterruption_X4_RisingEdge => ResourceManager.GetString("DVITInterruption_X4_RisingEdge", resourceCulture);

	public static string DVITInterruption_X5_Edge => ResourceManager.GetString("DVITInterruption_X5_Edge", resourceCulture);

	public static string DVITInterruption_X5_FallingEdge => ResourceManager.GetString("DVITInterruption_X5_FallingEdge", resourceCulture);

	public static string DVITInterruption_X5_RisingEdge => ResourceManager.GetString("DVITInterruption_X5_RisingEdge", resourceCulture);

	public static string DVITInterruption_Y_Edge => ResourceManager.GetString("DVITInterruption_Y_Edge", resourceCulture);

	public static string DVITInterruption_Y_FallingEdge => ResourceManager.GetString("DVITInterruption_Y_FallingEdge", resourceCulture);

	public static string DVITInterruption_Y_RisingEdge => ResourceManager.GetString("DVITInterruption_Y_RisingEdge", resourceCulture);

	public static string DWord => ResourceManager.GetString("DWord", resourceCulture);

	public static string DWord_Add => ResourceManager.GetString("DWord_Add", resourceCulture);

	public static string DWord_Add_One => ResourceManager.GetString("DWord_Add_One", resourceCulture);

	public static string DWord_And => ResourceManager.GetString("DWord_And", resourceCulture);

	public static string DWord_Divide => ResourceManager.GetString("DWord_Divide", resourceCulture);

	public static string DWord_Equal => ResourceManager.GetString("DWord_Equal", resourceCulture);

	public static string DWord_Less => ResourceManager.GetString("DWord_Less", resourceCulture);

	public static string DWord_Minus => ResourceManager.GetString("DWord_Minus", resourceCulture);

	public static string DWord_Minus_One => ResourceManager.GetString("DWord_Minus_One", resourceCulture);

	public static string DWord_Mod => ResourceManager.GetString("DWord_Mod", resourceCulture);

	public static string DWord_More => ResourceManager.GetString("DWord_More", resourceCulture);

	public static string DWord_Multiply => ResourceManager.GetString("DWord_Multiply", resourceCulture);

	public static string DWord_Not_Equal => ResourceManager.GetString("DWord_Not_Equal", resourceCulture);

	public static string DWord_Not_Less => ResourceManager.GetString("DWord_Not_Less", resourceCulture);

	public static string DWord_Not_More => ResourceManager.GetString("DWord_Not_More", resourceCulture);

	public static string DWord_Or => ResourceManager.GetString("DWord_Or", resourceCulture);

	public static string DWord_Reverse => ResourceManager.GetString("DWord_Reverse", resourceCulture);

	public static string DWord_To_Float => ResourceManager.GetString("DWord_To_Float", resourceCulture);

	public static string DWord_To_Word => ResourceManager.GetString("DWord_To_Word", resourceCulture);

	public static string DWord_XOR => ResourceManager.GetString("DWord_XOR", resourceCulture);

	public static string DXF_ACTime => ResourceManager.GetString("DXF_ACTime", resourceCulture);

	public static string DXF_DCTime => ResourceManager.GetString("DXF_DCTime", resourceCulture);

	public static string DXF_File => ResourceManager.GetString("DXF_File", resourceCulture);

	public static string DXF_Import => ResourceManager.GetString("DXF_Import", resourceCulture);

	public static string DXF_Import_Message => ResourceManager.GetString("DXF_Import_Message", resourceCulture);

	public static string DXF_Velocity => ResourceManager.GetString("DXF_Velocity", resourceCulture);

	public static string DZRN_Inst => ResourceManager.GetString("DZRN_Inst", resourceCulture);

	public static string DZRND_Inst => ResourceManager.GetString("DZRND_Inst", resourceCulture);

	public static string ECAM_Accelerate => ResourceManager.GetString("ECAM_Accelerate", resourceCulture);

	public static string ECAM_AngAc => ResourceManager.GetString("ECAM_AngAc", resourceCulture);

	public static string ECAM_AngPa => ResourceManager.GetString("ECAM_AngPa", resourceCulture);

	public static string ECAM_BindingLabel => ResourceManager.GetString("ECAM_BindingLabel", resourceCulture);

	public static string ECAM_Catch => ResourceManager.GetString("ECAM_Catch", resourceCulture);

	public static string ECAM_CatchParams => ResourceManager.GetString("ECAM_CatchParams", resourceCulture);

	public static string ECAM_Clutch => ResourceManager.GetString("ECAM_Clutch", resourceCulture);

	public static string ECAM_ColorLocLabel => ResourceManager.GetString("ECAM_ColorLocLabel", resourceCulture);

	public static string ECAM_ColorLocProtect => ResourceManager.GetString("ECAM_ColorLocProtect", resourceCulture);

	public static string ECAM_ControlAddress => ResourceManager.GetString("ECAM_ControlAddress", resourceCulture);

	public static string ECAM_CountDirection => ResourceManager.GetString("ECAM_CountDirection", resourceCulture);

	public static string ECAM_Custom => ResourceManager.GetString("ECAM_Custom", resourceCulture);

	public static string ECAM_CutLength => ResourceManager.GetString("ECAM_CutLength", resourceCulture);

	public static string ECAM_CutWithLengthSpecified => ResourceManager.GetString("ECAM_CutWithLengthSpecified", resourceCulture);

	public static string ECAM_CutWithLocationSpecified => ResourceManager.GetString("ECAM_CutWithLocationSpecified", resourceCulture);

	public static string ECAM_Decrement => ResourceManager.GetString("ECAM_Decrement", resourceCulture);

	public static string ECAM_DOnly => ResourceManager.GetString("ECAM_DOnly", resourceCulture);

	public static string ECAM_Fly => ResourceManager.GetString("ECAM_Fly", resourceCulture);

	public static string ECAM_FlyParams => ResourceManager.GetString("ECAM_FlyParams", resourceCulture);

	public static string ECAM_FollowAddress => ResourceManager.GetString("ECAM_FollowAddress", resourceCulture);

	public static string ECAM_FollowFactor => ResourceManager.GetString("ECAM_FollowFactor", resourceCulture);

	public static string ECAM_FollowPreformance => ResourceManager.GetString("ECAM_FollowPreformance", resourceCulture);

	public static string ECAM_ID => ResourceManager.GetString("ECAM_ID", resourceCulture);

	public static string ECAM_Increment => ResourceManager.GetString("ECAM_Increment", resourceCulture);

	public static string ECAM_Inst => ResourceManager.GetString("ECAM_Inst", resourceCulture);

	public static string ECAM_KnifeNumber => ResourceManager.GetString("ECAM_KnifeNumber", resourceCulture);

	public static string ECAM_LineType => ResourceManager.GetString("ECAM_LineType", resourceCulture);

	public static string ECAM_LocSetting => ResourceManager.GetString("ECAM_LocSetting", resourceCulture);

	public static string ECAM_MachineParams => ResourceManager.GetString("ECAM_MachineParams", resourceCulture);

	public static string ECAM_mm => ResourceManager.GetString("ECAM_mm", resourceCulture);

	public static string ECAM_Move => ResourceManager.GetString("ECAM_Move", resourceCulture);

	public static string ECAM_NowStatus => ResourceManager.GetString("ECAM_NowStatus", resourceCulture);

	public static string ECAM_Paragraph => ResourceManager.GetString("ECAM_Paragraph", resourceCulture);

	public static string ECAM_ParallelLabel => ResourceManager.GetString("ECAM_ParallelLabel", resourceCulture);

	public static string ECAM_ParameterMissing => ResourceManager.GetString("ECAM_ParameterMissing", resourceCulture);

	public static string ECAM_PayFactor => ResourceManager.GetString("ECAM_PayFactor", resourceCulture);

	public static string ECAM_pls => ResourceManager.GetString("ECAM_pls", resourceCulture);

	public static string ECAM_Remember => ResourceManager.GetString("ECAM_Remember", resourceCulture);

	public static string ECAM_SysRes => ResourceManager.GetString("ECAM_SysRes", resourceCulture);

	public static string ECAM_TableLength => ResourceManager.GetString("ECAM_TableLength", resourceCulture);

	public static string ECAM_V => ResourceManager.GetString("ECAM_V", resourceCulture);

	public static string ECAM_Velocity => ResourceManager.GetString("ECAM_Velocity", resourceCulture);

	public static string ECAM_WheelDiameter => ResourceManager.GetString("ECAM_WheelDiameter", resourceCulture);

	public static string ECAM_X => ResourceManager.GetString("ECAM_X", resourceCulture);

	public static string ECAM_XAcLength => ResourceManager.GetString("ECAM_XAcLength", resourceCulture);

	public static string ECAM_XCMove => ResourceManager.GetString("ECAM_XCMove", resourceCulture);

	public static string ECAM_XCPls => ResourceManager.GetString("ECAM_XCPls", resourceCulture);

	public static string ECAM_XDeLength => ResourceManager.GetString("ECAM_XDeLength", resourceCulture);

	public static string ECAM_XOMea => ResourceManager.GetString("ECAM_XOMea", resourceCulture);

	public static string ECAM_XOPay => ResourceManager.GetString("ECAM_XOPay", resourceCulture);

	public static string ECAM_XPaLength => ResourceManager.GetString("ECAM_XPaLength", resourceCulture);

	public static string ECAM_XPay => ResourceManager.GetString("ECAM_XPay", resourceCulture);

	public static string ECAM_XVolume => ResourceManager.GetString("ECAM_XVolume", resourceCulture);

	public static string ECAM_XVPay => ResourceManager.GetString("ECAM_XVPay", resourceCulture);

	public static string ECAM_Y => ResourceManager.GetString("ECAM_Y", resourceCulture);

	public static string ECAM_YAcLength => ResourceManager.GetString("ECAM_YAcLength", resourceCulture);

	public static string ECAM_YAmplify => ResourceManager.GetString("ECAM_YAmplify", resourceCulture);

	public static string ECAM_YCMove => ResourceManager.GetString("ECAM_YCMove", resourceCulture);

	public static string ECAM_YCPls => ResourceManager.GetString("ECAM_YCPls", resourceCulture);

	public static string ECAM_YLiLength => ResourceManager.GetString("ECAM_YLiLength", resourceCulture);

	public static string ECAM_YODst => ResourceManager.GetString("ECAM_YODst", resourceCulture);

	public static string ECAM_YONow => ResourceManager.GetString("ECAM_YONow", resourceCulture);

	public static string ECAM_YRange => ResourceManager.GetString("ECAM_YRange", resourceCulture);

	public static string ECAM_YShrink => ResourceManager.GetString("ECAM_YShrink", resourceCulture);

	public static string ECAM_YVolume => ResourceManager.GetString("ECAM_YVolume", resourceCulture);

	public static string Edit_Color => ResourceManager.GetString("Edit_Color", resourceCulture);

	public static string EditDiagram => ResourceManager.GetString("EditDiagram", resourceCulture);

	public static string EditInsideComment => ResourceManager.GetString("EditInsideComment", resourceCulture);

	public static string EditNetwork => ResourceManager.GetString("EditNetwork", resourceCulture);

	public static string EDRVA_Inst => ResourceManager.GetString("EDRVA_Inst", resourceCulture);

	public static string EDRVI_Inst => ResourceManager.GetString("EDRVI_Inst", resourceCulture);

	public static string Effect_Preview => ResourceManager.GetString("Effect_Preview", resourceCulture);

	public static string EHSCS_Inst => ResourceManager.GetString("EHSCS_Inst", resourceCulture);

	public static string EI_Inst => ResourceManager.GetString("EI_Inst", resourceCulture);

	public static string Element => ResourceManager.GetString("Element", resourceCulture);

	public static string Element_Address => ResourceManager.GetString("Element_Address", resourceCulture);

	public static string Element_Alias => ResourceManager.GetString("Element_Alias", resourceCulture);

	public static string Element_Comment => ResourceManager.GetString("Element_Comment", resourceCulture);

	public static string Element_Comment_D0 => ResourceManager.GetString("Element_Comment_D0", resourceCulture);

	public static string Element_Comment_D1 => ResourceManager.GetString("Element_Comment_D1", resourceCulture);

	public static string Element_Comment_K0 => ResourceManager.GetString("Element_Comment_K0", resourceCulture);

	public static string Element_Comment_K1 => ResourceManager.GetString("Element_Comment_K1", resourceCulture);

	public static string Element_Comment_K2 => ResourceManager.GetString("Element_Comment_K2", resourceCulture);

	public static string Element_Delete => ResourceManager.GetString("Element_Delete", resourceCulture);

	public static string Element_Length => ResourceManager.GetString("Element_Length", resourceCulture);

	public static string Element_Name => ResourceManager.GetString("Element_Name", resourceCulture);

	public static string Element_Parameter_Setting => ResourceManager.GetString("Element_Parameter_Setting", resourceCulture);

	public static string Element_Value_Modify => ResourceManager.GetString("Element_Value_Modify", resourceCulture);

	public static string Enable_Disable => ResourceManager.GetString("Enable_Disable", resourceCulture);

	public static string Enable_Extension => ResourceManager.GetString("Enable_Extension", resourceCulture);

	public static string Enable_Filter_Time => ResourceManager.GetString("Enable_Filter_Time", resourceCulture);

	public static string Enable_Hardware_Filter => ResourceManager.GetString("Enable_Hardware_Filter", resourceCulture);

	public static string Enabled => ResourceManager.GetString("Enabled", resourceCulture);

	public static string End_Frequency => ResourceManager.GetString("End_Frequency", resourceCulture);

	public static string English => ResourceManager.GetString("English", resourceCulture);

	public static string Ensure => ResourceManager.GetString("Ensure", resourceCulture);

	public static string Enter_Password => ResourceManager.GetString("Enter_Password", resourceCulture);

	public static string EPCS_CHECK_ID_ERROR => ResourceManager.GetString("EPCS_CHECK_ID_ERROR", resourceCulture);

	public static string EPCS_ERASE_BULK_ERROR => ResourceManager.GetString("EPCS_ERASE_BULK_ERROR", resourceCulture);

	public static string EPCS_UPDATE_PACKNUM_ERROR => ResourceManager.GetString("EPCS_UPDATE_PACKNUM_ERROR", resourceCulture);

	public static string EPCS_UPDATE_TIMEOUT_ERROR => ResourceManager.GetString("EPCS_UPDATE_TIMEOUT_ERROR", resourceCulture);

	public static string EPID_Address => ResourceManager.GetString("EPID_Address", resourceCulture);

	public static string EPID_Address1 => ResourceManager.GetString("EPID_Address1", resourceCulture);

	public static string EPID_AdjustStatus => ResourceManager.GetString("EPID_AdjustStatus", resourceCulture);

	public static string EPID_ArgStart => ResourceManager.GetString("EPID_ArgStart", resourceCulture);

	public static string EPID_Argument => ResourceManager.GetString("EPID_Argument", resourceCulture);

	public static string EPID_Argument1 => ResourceManager.GetString("EPID_Argument1", resourceCulture);

	public static string EPID_ArgumentAddressList => ResourceManager.GetString("EPID_ArgumentAddressList", resourceCulture);

	public static string EPID_ArgumentName => ResourceManager.GetString("EPID_ArgumentName", resourceCulture);

	public static string EPID_ArgumentType => ResourceManager.GetString("EPID_ArgumentType", resourceCulture);

	public static string EPID_AutoAssertStability => ResourceManager.GetString("EPID_AutoAssertStability", resourceCulture);

	public static string EPID_BaseSetting => ResourceManager.GetString("EPID_BaseSetting", resourceCulture);

	public static string EPID_BaseSetting1 => ResourceManager.GetString("EPID_BaseSetting1", resourceCulture);

	public static string EPID_CalcRange => ResourceManager.GetString("EPID_CalcRange", resourceCulture);

	public static string EPID_CalcRange_ToolTip => ResourceManager.GetString("EPID_CalcRange_ToolTip", resourceCulture);

	public static string EPID_CalcRange1 => ResourceManager.GetString("EPID_CalcRange1", resourceCulture);

	public static string EPID_CalcVolume => ResourceManager.GetString("EPID_CalcVolume", resourceCulture);

	public static string EPID_CD => ResourceManager.GetString("EPID_CD", resourceCulture);

	public static string EPID_CI => ResourceManager.GetString("EPID_CI", resourceCulture);

	public static string EPID_ControlMode => ResourceManager.GetString("EPID_ControlMode", resourceCulture);

	public static string EPID_ControlMode_ToolTip => ResourceManager.GetString("EPID_ControlMode_ToolTip", resourceCulture);

	public static string EPID_DeadZone => ResourceManager.GetString("EPID_DeadZone", resourceCulture);

	public static string EPID_DirectoryPath => ResourceManager.GetString("EPID_DirectoryPath", resourceCulture);

	public static string EPID_DZ => ResourceManager.GetString("EPID_DZ", resourceCulture);

	public static string EPID_Enable => ResourceManager.GetString("EPID_Enable", resourceCulture);

	public static string EPID_EnableSelfAdjust => ResourceManager.GetString("EPID_EnableSelfAdjust", resourceCulture);

	public static string EPID_ErrorCode => ResourceManager.GetString("EPID_ErrorCode", resourceCulture);

	public static string EPID_Finish => ResourceManager.GetString("EPID_Finish", resourceCulture);

	public static string EPID_Finish0 => ResourceManager.GetString("EPID_Finish0", resourceCulture);

	public static string EPID_Finish1 => ResourceManager.GetString("EPID_Finish1", resourceCulture);

	public static string EPID_FuzzyPID => ResourceManager.GetString("EPID_FuzzyPID", resourceCulture);

	public static string EPID_Hint => ResourceManager.GetString("EPID_Hint", resourceCulture);

	public static string EPID_Hint_AdjustStatus => ResourceManager.GetString("EPID_Hint_AdjustStatus", resourceCulture);

	public static string EPID_Hint_Helper => ResourceManager.GetString("EPID_Hint_Helper", resourceCulture);

	public static string EPID_Hint_Minute => ResourceManager.GetString("EPID_Hint_Minute", resourceCulture);

	public static string EPID_Hint_Ms => ResourceManager.GetString("EPID_Hint_Ms", resourceCulture);

	public static string EPID_Hint1 => ResourceManager.GetString("EPID_Hint1", resourceCulture);

	public static string EPID_Initialize => ResourceManager.GetString("EPID_Initialize", resourceCulture);

	public static string EPID_Initialize_Description => ResourceManager.GetString("EPID_Initialize_Description", resourceCulture);

	public static string EPID_Input => ResourceManager.GetString("EPID_Input", resourceCulture);

	public static string EPID_Introduction => ResourceManager.GetString("EPID_Introduction", resourceCulture);

	public static string EPID_Introduction0 => ResourceManager.GetString("EPID_Introduction0", resourceCulture);

	public static string EPID_Introduction1 => ResourceManager.GetString("EPID_Introduction1", resourceCulture);

	public static string EPID_KP => ResourceManager.GetString("EPID_KP", resourceCulture);

	public static string EPID_Loop => ResourceManager.GetString("EPID_Loop", resourceCulture);

	public static string EPID_Loop0 => ResourceManager.GetString("EPID_Loop0", resourceCulture);

	public static string EPID_Loop1 => ResourceManager.GetString("EPID_Loop1", resourceCulture);

	public static string EPID_LoopID => ResourceManager.GetString("EPID_LoopID", resourceCulture);

	public static string EPID_LoopIndex => ResourceManager.GetString("EPID_LoopIndex", resourceCulture);

	public static string EPID_LoopName => ResourceManager.GetString("EPID_LoopName", resourceCulture);

	public static string EPID_LoopName1 => ResourceManager.GetString("EPID_LoopName1", resourceCulture);

	public static string EPID_MaxinumDutyPercentage => ResourceManager.GetString("EPID_MaxinumDutyPercentage", resourceCulture);

	public static string EPID_Measure => ResourceManager.GetString("EPID_Measure", resourceCulture);

	public static string EPID_MeasureValue => ResourceManager.GetString("EPID_MeasureValue", resourceCulture);

	public static string EPID_MemoryAttribute => ResourceManager.GetString("EPID_MemoryAttribute", resourceCulture);

	public static string EPID_MemoryAttribute_WordsFormat => ResourceManager.GetString("EPID_MemoryAttribute_WordsFormat", resourceCulture);

	public static string EPID_MemoryAttribute0 => ResourceManager.GetString("EPID_MemoryAttribute0", resourceCulture);

	public static string EPID_MemoryAttribute1 => ResourceManager.GetString("EPID_MemoryAttribute1", resourceCulture);

	public static string EPID_MemoryAttribute2 => ResourceManager.GetString("EPID_MemoryAttribute2", resourceCulture);

	public static string EPID_MemoryDistrubute => ResourceManager.GetString("EPID_MemoryDistrubute", resourceCulture);

	public static string EPID_MininumDutyPercentage => ResourceManager.GetString("EPID_MininumDutyPercentage", resourceCulture);

	public static string EPID_Minute => ResourceManager.GetString("EPID_Minute", resourceCulture);

	public static string EPID_Mode_Custom => ResourceManager.GetString("EPID_Mode_Custom", resourceCulture);

	public static string EPID_Mode_Handle => ResourceManager.GetString("EPID_Mode_Handle", resourceCulture);

	public static string EPID_Mode_Suitilize => ResourceManager.GetString("EPID_Mode_Suitilize", resourceCulture);

	public static string EPID_Ms => ResourceManager.GetString("EPID_Ms", resourceCulture);

	public static string EPID_NegativeDirection => ResourceManager.GetString("EPID_NegativeDirection", resourceCulture);

	public static string EPID_NegativeDirection_ToolTip => ResourceManager.GetString("EPID_NegativeDirection_ToolTip", resourceCulture);

	public static string EPID_NegativeDirection1 => ResourceManager.GetString("EPID_NegativeDirection1", resourceCulture);

	public static string EPID_NextPage => ResourceManager.GetString("EPID_NextPage", resourceCulture);

	public static string EPID_NoAdaption => ResourceManager.GetString("EPID_NoAdaption", resourceCulture);

	public static string EPID_Other => ResourceManager.GetString("EPID_Other", resourceCulture);

	public static string EPID_Output => ResourceManager.GetString("EPID_Output", resourceCulture);

	public static string EPID_OutputAddress => ResourceManager.GetString("EPID_OutputAddress", resourceCulture);

	public static string EPID_OutputAddress1 => ResourceManager.GetString("EPID_OutputAddress1", resourceCulture);

	public static string EPID_OutputDirection => ResourceManager.GetString("EPID_OutputDirection", resourceCulture);

	public static string EPID_OutputDirection1 => ResourceManager.GetString("EPID_OutputDirection1", resourceCulture);

	public static string EPID_OutputMaxinum => ResourceManager.GetString("EPID_OutputMaxinum", resourceCulture);

	public static string EPID_OutputMininum => ResourceManager.GetString("EPID_OutputMininum", resourceCulture);

	public static string EPID_OutputMode => ResourceManager.GetString("EPID_OutputMode", resourceCulture);

	public static string EPID_OutputMode1 => ResourceManager.GetString("EPID_OutputMode1", resourceCulture);

	public static string EPID_OutputType_D => ResourceManager.GetString("EPID_OutputType_D", resourceCulture);

	public static string EPID_OutputType_M => ResourceManager.GetString("EPID_OutputType_M", resourceCulture);

	public static string EPID_OutputType_Y => ResourceManager.GetString("EPID_OutputType_Y", resourceCulture);

	public static string EPID_ParamsList => ResourceManager.GetString("EPID_ParamsList", resourceCulture);

	public static string EPID_ParamsSetting => ResourceManager.GetString("EPID_ParamsSetting", resourceCulture);

	public static string EPID_ParaName => ResourceManager.GetString("EPID_ParaName", resourceCulture);

	public static string EPID_ParaType => ResourceManager.GetString("EPID_ParaType", resourceCulture);

	public static string EPID_PIDConfig => ResourceManager.GetString("EPID_PIDConfig", resourceCulture);

	public static string EPID_PIDMode => ResourceManager.GetString("EPID_PIDMode", resourceCulture);

	public static string EPID_PositiveDirection => ResourceManager.GetString("EPID_PositiveDirection", resourceCulture);

	public static string EPID_PositiveDirection_ToolTip => ResourceManager.GetString("EPID_PositiveDirection_ToolTip", resourceCulture);

	public static string EPID_PositiveDirection1 => ResourceManager.GetString("EPID_PositiveDirection1", resourceCulture);

	public static string EPID_PreviousPage => ResourceManager.GetString("EPID_PreviousPage", resourceCulture);

	public static string EPID_RateBenefit => ResourceManager.GetString("EPID_RateBenefit", resourceCulture);

	public static string EPID_ReadPLC => ResourceManager.GetString("EPID_ReadPLC", resourceCulture);

	public static string EPID_Reserve => ResourceManager.GetString("EPID_Reserve", resourceCulture);

	public static string EPID_Reserved => ResourceManager.GetString("EPID_Reserved", resourceCulture);

	public static string EPID_SamplePeriod => ResourceManager.GetString("EPID_SamplePeriod", resourceCulture);

	public static string EPID_SeAdjust => ResourceManager.GetString("EPID_SeAdjust", resourceCulture);

	public static string EPID_Self => ResourceManager.GetString("EPID_Self", resourceCulture);

	public static string EPID_SelfAdaption => ResourceManager.GetString("EPID_SelfAdaption", resourceCulture);

	public static string EPID_SelfAdaptionFuzzyPIDStatus => ResourceManager.GetString("EPID_SelfAdaptionFuzzyPIDStatus", resourceCulture);

	public static string EPID_SelfAdjust => ResourceManager.GetString("EPID_SelfAdjust", resourceCulture);

	public static string EPID_SelfAdjust0 => ResourceManager.GetString("EPID_SelfAdjust0", resourceCulture);

	public static string EPID_SelfAdjustArguments => ResourceManager.GetString("EPID_SelfAdjustArguments", resourceCulture);

	public static string EPID_SelfAdjustStatus => ResourceManager.GetString("EPID_SelfAdjustStatus", resourceCulture);

	public static string EPID_SelfAdjustTimeout => ResourceManager.GetString("EPID_SelfAdjustTimeout", resourceCulture);

	public static string EPID_SeNp => ResourceManager.GetString("EPID_SeNp", resourceCulture);

	public static string EPID_SeOpen => ResourceManager.GetString("EPID_SeOpen", resourceCulture);

	public static string EPID_SeParams => ResourceManager.GetString("EPID_SeParams", resourceCulture);

	public static string EPID_SeSt => ResourceManager.GetString("EPID_SeSt", resourceCulture);

	public static string EPID_SeTimeout => ResourceManager.GetString("EPID_SeTimeout", resourceCulture);

	public static string EPID_StabilityAssertMode => ResourceManager.GetString("EPID_StabilityAssertMode", resourceCulture);

	public static string EPID_Status_Complete => ResourceManager.GetString("EPID_Status_Complete", resourceCulture);

	public static string EPID_Status_Failed => ResourceManager.GetString("EPID_Status_Failed", resourceCulture);

	public static string EPID_Status_Running => ResourceManager.GetString("EPID_Status_Running", resourceCulture);

	public static string EPID_Target => ResourceManager.GetString("EPID_Target", resourceCulture);

	public static string EPID_TargetValue => ResourceManager.GetString("EPID_TargetValue", resourceCulture);

	public static string EPID_TD => ResourceManager.GetString("EPID_TD", resourceCulture);

	public static string EPID_TD1 => ResourceManager.GetString("EPID_TD1", resourceCulture);

	public static string EPID_TI => ResourceManager.GetString("EPID_TI", resourceCulture);

	public static string EPID_TI1 => ResourceManager.GetString("EPID_TI1", resourceCulture);

	public static string EPID_TS => ResourceManager.GetString("EPID_TS", resourceCulture);

	public static string EPID_UserAssertStability => ResourceManager.GetString("EPID_UserAssertStability", resourceCulture);

	public static string EPID_V0 => ResourceManager.GetString("EPID_V0", resourceCulture);

	public static string EPID_V1 => ResourceManager.GetString("EPID_V1", resourceCulture);

	public static string EPID_Wizard => ResourceManager.GetString("EPID_Wizard", resourceCulture);

	public static string EPID_WritePLC => ResourceManager.GetString("EPID_WritePLC", resourceCulture);

	public static string EPLSR_Inst => ResourceManager.GetString("EPLSR_Inst", resourceCulture);

	public static string Error => ResourceManager.GetString("Error", resourceCulture);

	public static string Error_List => ResourceManager.GetString("Error_List", resourceCulture);

	public static string Error_VerticalLineBorder => ResourceManager.GetString("Error_VerticalLineBorder", resourceCulture);

	public static string Error_VerticalLineUnder => ResourceManager.GetString("Error_VerticalLineUnder", resourceCulture);

	public static string ErrorReportWindow_Error => ResourceManager.GetString("ErrorReportWindow_Error", resourceCulture);

	public static string ErrorReportWindow_Explanation => ResourceManager.GetString("ErrorReportWindow_Explanation", resourceCulture);

	public static string ErrorReportWindow_Warning => ResourceManager.GetString("ErrorReportWindow_Warning", resourceCulture);

	public static string Exceed_Adddress => ResourceManager.GetString("Exceed_Adddress", resourceCulture);

	public static string Execution_Number => ResourceManager.GetString("Execution_Number", resourceCulture);

	public static string ExModuleCheck_ERROR => ResourceManager.GetString("ExModuleCheck_ERROR", resourceCulture);

	public static string ExModuleCom_ERROR => ResourceManager.GetString("ExModuleCom_ERROR", resourceCulture);

	public static string ExModulePara_ERROR => ResourceManager.GetString("ExModulePara_ERROR", resourceCulture);

	public static string ExModuleReConfig_TIMEOUT_ERROR => ResourceManager.GetString("ExModuleReConfig_TIMEOUT_ERROR", resourceCulture);

	public static string EXP_Operation => ResourceManager.GetString("EXP_Operation", resourceCulture);

	public static string Expand => ResourceManager.GetString("Expand", resourceCulture);

	public static string Expand_All => ResourceManager.GetString("Expand_All", resourceCulture);

	public static string Expand_Collapsed => ResourceManager.GetString("Expand_Collapsed", resourceCulture);

	public static string Expand_Module_Settings => ResourceManager.GetString("Expand_Module_Settings", resourceCulture);

	public static string Expansion_Module_Check => ResourceManager.GetString("Expansion_Module_Check", resourceCulture);

	public static string Expansion_Module_Check_Message => ResourceManager.GetString("Expansion_Module_Check_Message", resourceCulture);

	public static string Expansion_UsedRegisters => ResourceManager.GetString("Expansion_UsedRegisters", resourceCulture);

	public static string ExpansionCheckErrorDialog_QuestAutoAdjust => ResourceManager.GetString("ExpansionCheckErrorDialog_QuestAutoAdjust", resourceCulture);

	public static string ExpansionCheckErrorDialog_QuestContinueDownload => ResourceManager.GetString("ExpansionCheckErrorDialog_QuestContinueDownload", resourceCulture);

	public static string ExpansionCheckErrorDialog_Title => ResourceManager.GetString("ExpansionCheckErrorDialog_Title", resourceCulture);

	public static string ExpansionModule_Sort => ResourceManager.GetString("ExpansionModule_Sort", resourceCulture);

	public static string ExpansionModuleUnitSettingDialog_Title => ResourceManager.GetString("ExpansionModuleUnitSettingDialog_Title", resourceCulture);

	public static string Export => ResourceManager.GetString("Export", resourceCulture);

	public static string Export_CSV => ResourceManager.GetString("Export_CSV", resourceCulture);

	public static string Export_Failed => ResourceManager.GetString("Export_Failed", resourceCulture);

	public static string ExportMonitorTable => ResourceManager.GetString("ExportMonitorTable", resourceCulture);

	public static string ExternalIO => ResourceManager.GetString("ExternalIO", resourceCulture);

	public static string FACT_Inst => ResourceManager.GetString("FACT_Inst", resourceCulture);

	public static string File => ResourceManager.GetString("File", resourceCulture);

	public static string File_Convert => ResourceManager.GetString("File_Convert", resourceCulture);

	public static string File_Converter => ResourceManager.GetString("File_Converter", resourceCulture);

	public static string File_Name => ResourceManager.GetString("File_Name", resourceCulture);

	public static string File_Name_Contanin_Illegal_Char => ResourceManager.GetString("File_Name_Contanin_Illegal_Char", resourceCulture);

	public static string File_Name_Format_Illegal => ResourceManager.GetString("File_Name_Format_Illegal", resourceCulture);

	public static string File_Name_Too_Long => ResourceManager.GetString("File_Name_Too_Long", resourceCulture);

	public static string File_Override => ResourceManager.GetString("File_Override", resourceCulture);

	public static string File_Select => ResourceManager.GetString("File_Select", resourceCulture);

	public static string File_Type_Not_Supported => ResourceManager.GetString("File_Type_Not_Supported", resourceCulture);

	public static string Filter_Mode => ResourceManager.GetString("Filter_Mode", resourceCulture);

	public static string Filter_Setting => ResourceManager.GetString("Filter_Setting", resourceCulture);

	public static string Filter_Time => ResourceManager.GetString("Filter_Time", resourceCulture);

	public static string Float_Add => ResourceManager.GetString("Float_Add", resourceCulture);

	public static string Float_Divide => ResourceManager.GetString("Float_Divide", resourceCulture);

	public static string Float_Equal => ResourceManager.GetString("Float_Equal", resourceCulture);

	public static string Float_Less_Than => ResourceManager.GetString("Float_Less_Than", resourceCulture);

	public static string Float_Minus => ResourceManager.GetString("Float_Minus", resourceCulture);

	public static string Float_More_Than => ResourceManager.GetString("Float_More_Than", resourceCulture);

	public static string Float_Multiply => ResourceManager.GetString("Float_Multiply", resourceCulture);

	public static string Float_Not_Equal => ResourceManager.GetString("Float_Not_Equal", resourceCulture);

	public static string Float_Not_Less_Than => ResourceManager.GetString("Float_Not_Less_Than", resourceCulture);

	public static string Float_Not_More_Than => ResourceManager.GetString("Float_Not_More_Than", resourceCulture);

	public static string FMOV_Inst => ResourceManager.GetString("FMOV_Inst", resourceCulture);

	public static string FMOVD_Inst => ResourceManager.GetString("FMOVD_Inst", resourceCulture);

	public static string FolderConfig_Name => ResourceManager.GetString("FolderConfig_Name", resourceCulture);

	public static string FolderConfig_Password => ResourceManager.GetString("FolderConfig_Password", resourceCulture);

	public static string FolderConfig_Title => ResourceManager.GetString("FolderConfig_Title", resourceCulture);

	public static string FolderPassword_Message => ResourceManager.GetString("FolderPassword_Message", resourceCulture);

	public static string FolderPassword_Title => ResourceManager.GetString("FolderPassword_Title", resourceCulture);

	public static string FolderPasswordAccess_Message => ResourceManager.GetString("FolderPasswordAccess_Message", resourceCulture);

	public static string FolderPasswordAccess_Title => ResourceManager.GetString("FolderPasswordAccess_Title", resourceCulture);

	public static string FOLLOW_Inst => ResourceManager.GetString("FOLLOW_Inst", resourceCulture);

	public static string FOLLOWParamsDialog_Address => ResourceManager.GetString("FOLLOWParamsDialog_Address", resourceCulture);

	public static string FOLLOWParamsDialog_Div => ResourceManager.GetString("FOLLOWParamsDialog_Div", resourceCulture);

	public static string FOLLOWParamsDialog_Mul => ResourceManager.GetString("FOLLOWParamsDialog_Mul", resourceCulture);

	public static string FOLLOWParamsDialog_Period => ResourceManager.GetString("FOLLOWParamsDialog_Period", resourceCulture);

	public static string FOLLOWParamsDialog_Profix => ResourceManager.GetString("FOLLOWParamsDialog_Profix", resourceCulture);

	public static string FOLLOWParamsDialog_Target => ResourceManager.GetString("FOLLOWParamsDialog_Target", resourceCulture);

	public static string FOLLOWParamsDialog_Title => ResourceManager.GetString("FOLLOWParamsDialog_Title", resourceCulture);

	public static string Font => ResourceManager.GetString("Font", resourceCulture);

	public static string Font_Color => ResourceManager.GetString("Font_Color", resourceCulture);

	public static string Font_Range => ResourceManager.GetString("Font_Range", resourceCulture);

	public static string Font_Size => ResourceManager.GetString("Font_Size", resourceCulture);

	public static string Font_Style => ResourceManager.GetString("Font_Style", resourceCulture);

	public static string FontSetting => ResourceManager.GetString("FontSetting", resourceCulture);

	public static string FOR_Inst => ResourceManager.GetString("FOR_Inst", resourceCulture);

	public static string Forbid_Test_Edit => ResourceManager.GetString("Forbid_Test_Edit", resourceCulture);

	public static string Force_OFF => ResourceManager.GetString("Force_OFF", resourceCulture);

	public static string Force_ON => ResourceManager.GetString("Force_ON", resourceCulture);

	public static string FPGA_CHECK_ACK_ERROR => ResourceManager.GetString("FPGA_CHECK_ACK_ERROR", resourceCulture);

	public static string FPGA_UPLOAD_TIMEOUT_ERROR => ResourceManager.GetString("FPGA_UPLOAD_TIMEOUT_ERROR", resourceCulture);

	public static string Free_Port_Communication => ResourceManager.GetString("Free_Port_Communication", resourceCulture);

	public static string Frequency_Chart => ResourceManager.GetString("Frequency_Chart", resourceCulture);

	public static string Frequency_Division_Factor => ResourceManager.GetString("Frequency_Division_Factor", resourceCulture);

	public static string Func_Changed => ResourceManager.GetString("Func_Changed", resourceCulture);

	public static string FuncBlock => ResourceManager.GetString("FuncBlock", resourceCulture);

	public static string Funcblock_Check => ResourceManager.GetString("Funcblock_Check", resourceCulture);

	public static string Funcblock_Checked => ResourceManager.GetString("Funcblock_Checked", resourceCulture);

	public static string FuncBlock_Correct => ResourceManager.GetString("FuncBlock_Correct", resourceCulture);

	public static string FuncBlock_Error => ResourceManager.GetString("FuncBlock_Error", resourceCulture);

	public static string Funcblock_Of_Function => ResourceManager.GetString("Funcblock_Of_Function", resourceCulture);

	public static string FuncBlockExporting => ResourceManager.GetString("FuncBlockExporting", resourceCulture);

	public static string FuncBlockImporting => ResourceManager.GetString("FuncBlockImporting", resourceCulture);

	public static string FuncDialog_AllFuncs => ResourceManager.GetString("FuncDialog_AllFuncs", resourceCulture);

	public static string FuncDialog_Arguments => ResourceManager.GetString("FuncDialog_Arguments", resourceCulture);

	public static string FuncDialog_Author => ResourceManager.GetString("FuncDialog_Author", resourceCulture);

	public static string FuncDialog_CreateFunc => ResourceManager.GetString("FuncDialog_CreateFunc", resourceCulture);

	public static string FuncDialog_CreateName => ResourceManager.GetString("FuncDialog_CreateName", resourceCulture);

	public static string FuncDialog_Name => ResourceManager.GetString("FuncDialog_Name", resourceCulture);

	public static string FuncDialog_RemoveFunc => ResourceManager.GetString("FuncDialog_RemoveFunc", resourceCulture);

	public static string FuncDialog_Type => ResourceManager.GetString("FuncDialog_Type", resourceCulture);

	public static string FuncDialog_Version => ResourceManager.GetString("FuncDialog_Version", resourceCulture);

	public static string Function => ResourceManager.GetString("Function", resourceCulture);

	public static string Function_Block_Correct => ResourceManager.GetString("Function_Block_Correct", resourceCulture);

	public static string Fuzzy_PID => ResourceManager.GetString("Fuzzy_PID", resourceCulture);

	public static string Gateway => ResourceManager.GetString("Gateway", resourceCulture);

	public static string Generated_Failed => ResourceManager.GetString("Generated_Failed", resourceCulture);

	public static string Generated_Path => ResourceManager.GetString("Generated_Path", resourceCulture);

	public static string Generating_Final => ResourceManager.GetString("Generating_Final", resourceCulture);

	public static string HandledAddress => ResourceManager.GetString("HandledAddress", resourceCulture);

	public static string HandleMode => ResourceManager.GetString("HandleMode", resourceCulture);

	public static string HashCode_Buffer_Error => ResourceManager.GetString("HashCode_Buffer_Error", resourceCulture);

	public static string HCNT_SETVALUE_ERROR => ResourceManager.GetString("HCNT_SETVALUE_ERROR", resourceCulture);

	public static string Help => ResourceManager.GetString("Help", resourceCulture);

	public static string HereBranchError => ResourceManager.GetString("HereBranchError", resourceCulture);

	public static string HereFusionError => ResourceManager.GetString("HereFusionError", resourceCulture);

	public static string HereOpenError => ResourceManager.GetString("HereOpenError", resourceCulture);

	public static string HereShortError => ResourceManager.GetString("HereShortError", resourceCulture);

	public static string Hide_Dashed => ResourceManager.GetString("Hide_Dashed", resourceCulture);

	public static string HMIBLOCK_Inst => ResourceManager.GetString("HMIBLOCK_Inst", resourceCulture);

	public static string HMIDataAddress => ResourceManager.GetString("HMIDataAddress", resourceCulture);

	public static string HMIPLINE_Inst => ResourceManager.GetString("HMIPLINE_Inst", resourceCulture);

	public static string HMIPLINEDialog_AutoSwitch => ResourceManager.GetString("HMIPLINEDialog_AutoSwitch", resourceCulture);

	public static string HMIPLINEDialog_ClearFile => ResourceManager.GetString("HMIPLINEDialog_ClearFile", resourceCulture);

	public static string HMIPLINEDialog_DeleteFile => ResourceManager.GetString("HMIPLINEDialog_DeleteFile", resourceCulture);

	public static string HMIPLINEDialog_DestAddress => ResourceManager.GetString("HMIPLINEDialog_DestAddress", resourceCulture);

	public static string HMIPLINEDialog_DestToFile => ResourceManager.GetString("HMIPLINEDialog_DestToFile", resourceCulture);

	public static string HMIPLINEDialog_EndIndex => ResourceManager.GetString("HMIPLINEDialog_EndIndex", resourceCulture);

	public static string HMIPLINEDialog_ExportFile => ResourceManager.GetString("HMIPLINEDialog_ExportFile", resourceCulture);

	public static string HMIPLINEDialog_FirstFile => ResourceManager.GetString("HMIPLINEDialog_FirstFile", resourceCulture);

	public static string HMIPLINEDialog_MotionPlot => ResourceManager.GetString("HMIPLINEDialog_MotionPlot", resourceCulture);

	public static string HMIPLINEDialog_NextBit => ResourceManager.GetString("HMIPLINEDialog_NextBit", resourceCulture);

	public static string HMIPLINEDialog_PlaneID => ResourceManager.GetString("HMIPLINEDialog_PlaneID", resourceCulture);

	public static string HMIPLINEDialog_PrevBit => ResourceManager.GetString("HMIPLINEDialog_PrevBit", resourceCulture);

	public static string HMIPLINEDialog_RefreshFile => ResourceManager.GetString("HMIPLINEDialog_RefreshFile", resourceCulture);

	public static string HMIPLINEDialog_SelectFile => ResourceManager.GetString("HMIPLINEDialog_SelectFile", resourceCulture);

	public static string HMIPLINEDialog_Title => ResourceManager.GetString("HMIPLINEDialog_Title", resourceCulture);

	public static string HSCS_AbsoluteCounter => ResourceManager.GetString("HSCS_AbsoluteCounter", resourceCulture);

	public static string HSCS_Add => ResourceManager.GetString("HSCS_Add", resourceCulture);

	public static string HSCS_AddNumber => ResourceManager.GetString("HSCS_AddNumber", resourceCulture);

	public static string HSCS_CallName => ResourceManager.GetString("HSCS_CallName", resourceCulture);

	public static string HSCS_Const => ResourceManager.GetString("HSCS_Const", resourceCulture);

	public static string HSCS_CounterMode => ResourceManager.GetString("HSCS_CounterMode", resourceCulture);

	public static string HSCS_CycleInvoke => ResourceManager.GetString("HSCS_CycleInvoke", resourceCulture);

	public static string HSCS_CycleInvokeCount => ResourceManager.GetString("HSCS_CycleInvokeCount", resourceCulture);

	public static string HSCS_Decrement => ResourceManager.GetString("HSCS_Decrement", resourceCulture);

	public static string HSCS_HighCounter => ResourceManager.GetString("HSCS_HighCounter", resourceCulture);

	public static string HSCS_ID => ResourceManager.GetString("HSCS_ID", resourceCulture);

	public static string HSCS_IncDec => ResourceManager.GetString("HSCS_IncDec", resourceCulture);

	public static string HSCS_Increment => ResourceManager.GetString("HSCS_Increment", resourceCulture);

	public static string HSCS_Inst => ResourceManager.GetString("HSCS_Inst", resourceCulture);

	public static string HSCS_InvokeMode => ResourceManager.GetString("HSCS_InvokeMode", resourceCulture);

	public static string HSCS_MaxValue => ResourceManager.GetString("HSCS_MaxValue", resourceCulture);

	public static string HSCS_Modify => ResourceManager.GetString("HSCS_Modify", resourceCulture);

	public static string HSCS_MoveDown => ResourceManager.GetString("HSCS_MoveDown", resourceCulture);

	public static string HSCS_MoveUp => ResourceManager.GetString("HSCS_MoveUp", resourceCulture);

	public static string HSCS_MultiAdd => ResourceManager.GetString("HSCS_MultiAdd", resourceCulture);

	public static string HSCS_Operation => ResourceManager.GetString("HSCS_Operation", resourceCulture);

	public static string HSCS_Output => ResourceManager.GetString("HSCS_Output", resourceCulture);

	public static string HSCS_RelativeCounter => ResourceManager.GetString("HSCS_RelativeCounter", resourceCulture);

	public static string HSCS_Remove => ResourceManager.GetString("HSCS_Remove", resourceCulture);

	public static string HSCS_SingleInvoke => ResourceManager.GetString("HSCS_SingleInvoke", resourceCulture);

	public static string HSCS_TableCount => ResourceManager.GetString("HSCS_TableCount", resourceCulture);

	public static string HSCS_TargetValue => ResourceManager.GetString("HSCS_TargetValue", resourceCulture);

	public static string HSCSDialog_ModifyArguments => ResourceManager.GetString("HSCSDialog_ModifyArguments", resourceCulture);

	public static string HybridLink_Error => ResourceManager.GetString("HybridLink_Error", resourceCulture);

	public static string Iap_Forbid => ResourceManager.GetString("Iap_Forbid", resourceCulture);

	public static string IAPBaudrate => ResourceManager.GetString("IAPBaudrate", resourceCulture);

	public static string IAPCheckCode => ResourceManager.GetString("IAPCheckCode", resourceCulture);

	public static string IAPDataBit => ResourceManager.GetString("IAPDataBit", resourceCulture);

	public static string IAPStopBit => ResourceManager.GetString("IAPStopBit", resourceCulture);

	public static string IgnoreCase => ResourceManager.GetString("IgnoreCase", resourceCulture);

	public static string IllegalNetworkRange => ResourceManager.GetString("IllegalNetworkRange", resourceCulture);

	public static string Import => ResourceManager.GetString("Import", resourceCulture);

	public static string Import_CSV => ResourceManager.GetString("Import_CSV", resourceCulture);

	public static string Import_Failed => ResourceManager.GetString("Import_Failed", resourceCulture);

	public static string Important => ResourceManager.GetString("Important", resourceCulture);

	public static string ImportFolder => ResourceManager.GetString("ImportFolder", resourceCulture);

	public static string IN_ACC_SLOW_ERROR => ResourceManager.GetString("IN_ACC_SLOW_ERROR", resourceCulture);

	public static string IN_BEYOND_LIMT => ResourceManager.GetString("IN_BEYOND_LIMT", resourceCulture);

	public static string IN_BOWERR_ERROR => ResourceManager.GetString("IN_BOWERR_ERROR", resourceCulture);

	public static string IN_DATA_ERROR => ResourceManager.GetString("IN_DATA_ERROR", resourceCulture);

	public static string In_Edit_Mode => ResourceManager.GetString("In_Edit_Mode", resourceCulture);

	public static string IN_ELEMENT_TYPE_ERROR => ResourceManager.GetString("IN_ELEMENT_TYPE_ERROR", resourceCulture);

	public static string IN_OVER_MAXFREQ => ResourceManager.GetString("IN_OVER_MAXFREQ", resourceCulture);

	public static string IN_POSITION_ERROR => ResourceManager.GetString("IN_POSITION_ERROR", resourceCulture);

	public static string IN_RADIUS_ERROR => ResourceManager.GetString("IN_RADIUS_ERROR", resourceCulture);

	public static string IN_SPEED_ERROR => ResourceManager.GetString("IN_SPEED_ERROR", resourceCulture);

	public static string Information => ResourceManager.GetString("Information", resourceCulture);

	public static string Initialize => ResourceManager.GetString("Initialize", resourceCulture);

	public static string Initialize_Data => ResourceManager.GetString("Initialize_Data", resourceCulture);

	public static string Input => ResourceManager.GetString("Input", resourceCulture);

	public static string Input_Empty => ResourceManager.GetString("Input_Empty", resourceCulture);

	public static string Input_Select => ResourceManager.GetString("Input_Select", resourceCulture);

	public static string Input_Value_illegal => ResourceManager.GetString("Input_Value_illegal", resourceCulture);

	public static string Insert => ResourceManager.GetString("Insert", resourceCulture);

	public static string Insert5Networks => ResourceManager.GetString("Insert5Networks", resourceCulture);

	public static string Insert5NetworksAfter => ResourceManager.GetString("Insert5NetworksAfter", resourceCulture);

	public static string Insert5NetworksBefore => ResourceManager.GetString("Insert5NetworksBefore", resourceCulture);

	public static string Insert5NetworksEnd => ResourceManager.GetString("Insert5NetworksEnd", resourceCulture);

	public static string Insert5Rows => ResourceManager.GetString("Insert5Rows", resourceCulture);

	public static string Insert5RowsAfter => ResourceManager.GetString("Insert5RowsAfter", resourceCulture);

	public static string Insert5RowsBefore => ResourceManager.GetString("Insert5RowsBefore", resourceCulture);

	public static string Insert5RowsEnd => ResourceManager.GetString("Insert5RowsEnd", resourceCulture);

	public static string Inst_ABS => ResourceManager.GetString("Inst_ABS", resourceCulture);

	public static string Inst_ABSD => ResourceManager.GetString("Inst_ABSD", resourceCulture);

	public static string Inst_ABSF => ResourceManager.GetString("Inst_ABSF", resourceCulture);

	public static string Inst_ACALL => ResourceManager.GetString("Inst_ACALL", resourceCulture);

	public static string Inst_ACALLM => ResourceManager.GetString("Inst_ACALLM", resourceCulture);

	public static string Inst_ACOS => ResourceManager.GetString("Inst_ACOS", resourceCulture);

	public static string Inst_ASIN => ResourceManager.GetString("Inst_ASIN", resourceCulture);

	public static string Inst_ATAN => ResourceManager.GetString("Inst_ATAN", resourceCulture);

	public static string Inst_Auxiliar => ResourceManager.GetString("Inst_Auxiliar", resourceCulture);

	public static string Inst_AVE => ResourceManager.GetString("Inst_AVE", resourceCulture);

	public static string Inst_AVED => ResourceManager.GetString("Inst_AVED", resourceCulture);

	public static string Inst_AVEF => ResourceManager.GetString("Inst_AVEF", resourceCulture);

	public static string Inst_BAND => ResourceManager.GetString("Inst_BAND", resourceCulture);

	public static string Inst_BANDD => ResourceManager.GetString("Inst_BANDD", resourceCulture);

	public static string Inst_BANDF => ResourceManager.GetString("Inst_BANDF", resourceCulture);

	public static string Inst_BCDD => ResourceManager.GetString("Inst_BCDD", resourceCulture);

	public static string Inst_BIND => ResourceManager.GetString("Inst_BIND", resourceCulture);

	public static string Inst_Bit => ResourceManager.GetString("Inst_Bit", resourceCulture);

	public static string Inst_BKADD => ResourceManager.GetString("Inst_BKADD", resourceCulture);

	public static string Inst_BKE => ResourceManager.GetString("Inst_BKE", resourceCulture);

	public static string Inst_BKG => ResourceManager.GetString("Inst_BKG", resourceCulture);

	public static string Inst_BKGE => ResourceManager.GetString("Inst_BKGE", resourceCulture);

	public static string Inst_BKL => ResourceManager.GetString("Inst_BKL", resourceCulture);

	public static string Inst_BKLE => ResourceManager.GetString("Inst_BKLE", resourceCulture);

	public static string Inst_BKNE => ResourceManager.GetString("Inst_BKNE", resourceCulture);

	public static string Inst_BKSUB => ResourceManager.GetString("Inst_BKSUB", resourceCulture);

	public static string Inst_BREAK => ResourceManager.GetString("Inst_BREAK", resourceCulture);

	public static string Inst_BTOW => ResourceManager.GetString("Inst_BTOW", resourceCulture);

	public static string Inst_CAM => ResourceManager.GetString("Inst_CAM", resourceCulture);

	public static string Inst_CAMA => ResourceManager.GetString("Inst_CAMA", resourceCulture);

	public static string Inst_CAMR => ResourceManager.GetString("Inst_CAMR", resourceCulture);

	public static string Inst_Communication => ResourceManager.GetString("Inst_Communication", resourceCulture);

	public static string Inst_Compare => ResourceManager.GetString("Inst_Compare", resourceCulture);

	public static string Inst_Convert => ResourceManager.GetString("Inst_Convert", resourceCulture);

	public static string Inst_Counter => ResourceManager.GetString("Inst_Counter", resourceCulture);

	public static string Inst_CRC => ResourceManager.GetString("Inst_CRC", resourceCulture);

	public static string Inst_CRCCHECK => ResourceManager.GetString("Inst_CRCCHECK", resourceCulture);

	public static string Inst_DataControl => ResourceManager.GetString("Inst_DataControl", resourceCulture);

	public static string Inst_DBKADD => ResourceManager.GetString("Inst_DBKADD", resourceCulture);

	public static string Inst_DBKE => ResourceManager.GetString("Inst_DBKE", resourceCulture);

	public static string Inst_DBKG => ResourceManager.GetString("Inst_DBKG", resourceCulture);

	public static string Inst_DBKGE => ResourceManager.GetString("Inst_DBKGE", resourceCulture);

	public static string Inst_DBKL => ResourceManager.GetString("Inst_DBKL", resourceCulture);

	public static string Inst_DBKLE => ResourceManager.GetString("Inst_DBKLE", resourceCulture);

	public static string Inst_DBKNE => ResourceManager.GetString("Inst_DBKNE", resourceCulture);

	public static string Inst_DBKSUB => ResourceManager.GetString("Inst_DBKSUB", resourceCulture);

	public static string Inst_DECO => ResourceManager.GetString("Inst_DECO", resourceCulture);

	public static string Inst_DEG => ResourceManager.GetString("Inst_DEG", resourceCulture);

	public static string Inst_DI_S => ResourceManager.GetString("Inst_DI_S", resourceCulture);

	public static string Inst_DIS => ResourceManager.GetString("Inst_DIS", resourceCulture);

	public static string Inst_DSORT2 => ResourceManager.GetString("Inst_DSORT2", resourceCulture);

	public static string Inst_EHCNT => ResourceManager.GetString("Inst_EHCNT", resourceCulture);

	public static string Inst_ENCO => ResourceManager.GetString("Inst_ENCO", resourceCulture);

	public static string Inst_EPID => ResourceManager.GetString("Inst_EPID", resourceCulture);

	public static string Inst_FloatCalculation => ResourceManager.GetString("Inst_FloatCalculation", resourceCulture);

	public static string Inst_FTOW => ResourceManager.GetString("Inst_FTOW", resourceCulture);

	public static string Inst_GBIN => ResourceManager.GetString("Inst_GBIN", resourceCulture);

	public static string Inst_GBIND => ResourceManager.GetString("Inst_GBIND", resourceCulture);

	public static string Inst_GRY => ResourceManager.GetString("Inst_GRY", resourceCulture);

	public static string Inst_GRYD => ResourceManager.GetString("Inst_GRYD", resourceCulture);

	public static string Inst_HighCount => ResourceManager.GetString("Inst_HighCount", resourceCulture);

	public static string Inst_HKY => ResourceManager.GetString("Inst_HKY", resourceCulture);

	public static string Inst_HKYD => ResourceManager.GetString("Inst_HKYD", resourceCulture);

	public static string Inst_I_S => ResourceManager.GetString("Inst_I_S", resourceCulture);

	public static string Inst_INSTR => ResourceManager.GetString("Inst_INSTR", resourceCulture);

	public static string Inst_IntegerCalculation => ResourceManager.GetString("Inst_IntegerCalculation", resourceCulture);

	public static string Inst_Interrupt => ResourceManager.GetString("Inst_Interrupt", resourceCulture);

	public static string Inst_IST => ResourceManager.GetString("Inst_IST", resourceCulture);

	public static string Inst_ISTNEXT => ResourceManager.GetString("Inst_ISTNEXT", resourceCulture);

	public static string Inst_LDCALL => ResourceManager.GetString("Inst_LDCALL", resourceCulture);

	public static string Inst_LDCALLM => ResourceManager.GetString("Inst_LDCALLM", resourceCulture);

	public static string Inst_LDCALLM1 => ResourceManager.GetString("Inst_LDCALLM1", resourceCulture);

	public static string Inst_LDDAEQ => ResourceManager.GetString("Inst_LDDAEQ", resourceCulture);

	public static string Inst_LDDAG => ResourceManager.GetString("Inst_LDDAG", resourceCulture);

	public static string Inst_LDDAGE => ResourceManager.GetString("Inst_LDDAGE", resourceCulture);

	public static string Inst_LDDAL => ResourceManager.GetString("Inst_LDDAL", resourceCulture);

	public static string Inst_LDDALE => ResourceManager.GetString("Inst_LDDALE", resourceCulture);

	public static string Inst_LDDANE => ResourceManager.GetString("Inst_LDDANE", resourceCulture);

	public static string Inst_LDDMAEQ => ResourceManager.GetString("Inst_LDDMAEQ", resourceCulture);

	public static string Inst_LDDMAG => ResourceManager.GetString("Inst_LDDMAG", resourceCulture);

	public static string Inst_LDDMAGE => ResourceManager.GetString("Inst_LDDMAGE", resourceCulture);

	public static string Inst_LDDMAL => ResourceManager.GetString("Inst_LDDMAL", resourceCulture);

	public static string Inst_LDDMALE => ResourceManager.GetString("Inst_LDDMALE", resourceCulture);

	public static string Inst_LDDMANE => ResourceManager.GetString("Inst_LDDMANE", resourceCulture);

	public static string Inst_LDDMEQ => ResourceManager.GetString("Inst_LDDMEQ", resourceCulture);

	public static string Inst_LDDMG => ResourceManager.GetString("Inst_LDDMG", resourceCulture);

	public static string Inst_LDDMGE => ResourceManager.GetString("Inst_LDDMGE", resourceCulture);

	public static string Inst_LDDML => ResourceManager.GetString("Inst_LDDML", resourceCulture);

	public static string Inst_LDDMLE => ResourceManager.GetString("Inst_LDDMLE", resourceCulture);

	public static string Inst_LDDMNE => ResourceManager.GetString("Inst_LDDMNE", resourceCulture);

	public static string Inst_LDFAEQ => ResourceManager.GetString("Inst_LDFAEQ", resourceCulture);

	public static string Inst_LDFAG => ResourceManager.GetString("Inst_LDFAG", resourceCulture);

	public static string Inst_LDFAGE => ResourceManager.GetString("Inst_LDFAGE", resourceCulture);

	public static string Inst_LDFAL => ResourceManager.GetString("Inst_LDFAL", resourceCulture);

	public static string Inst_LDFALE => ResourceManager.GetString("Inst_LDFALE", resourceCulture);

	public static string Inst_LDFANE => ResourceManager.GetString("Inst_LDFANE", resourceCulture);

	public static string Inst_LDFB => ResourceManager.GetString("Inst_LDFB", resourceCulture);

	public static string Inst_LDFMAEQ => ResourceManager.GetString("Inst_LDFMAEQ", resourceCulture);

	public static string Inst_LDFMAG => ResourceManager.GetString("Inst_LDFMAG", resourceCulture);

	public static string Inst_LDFMAGE => ResourceManager.GetString("Inst_LDFMAGE", resourceCulture);

	public static string Inst_LDFMAL => ResourceManager.GetString("Inst_LDFMAL", resourceCulture);

	public static string Inst_LDFMALE => ResourceManager.GetString("Inst_LDFMALE", resourceCulture);

	public static string Inst_LDFMANE => ResourceManager.GetString("Inst_LDFMANE", resourceCulture);

	public static string Inst_LDFMEQ => ResourceManager.GetString("Inst_LDFMEQ", resourceCulture);

	public static string Inst_LDFMG => ResourceManager.GetString("Inst_LDFMG", resourceCulture);

	public static string Inst_LDFMGE => ResourceManager.GetString("Inst_LDFMGE", resourceCulture);

	public static string Inst_LDFML => ResourceManager.GetString("Inst_LDFML", resourceCulture);

	public static string Inst_LDFMLE => ResourceManager.GetString("Inst_LDFMLE", resourceCulture);

	public static string Inst_LDFMNE => ResourceManager.GetString("Inst_LDFMNE", resourceCulture);

	public static string Inst_LDPB => ResourceManager.GetString("Inst_LDPB", resourceCulture);

	public static string Inst_LDWAEQ => ResourceManager.GetString("Inst_LDWAEQ", resourceCulture);

	public static string Inst_LDWAG => ResourceManager.GetString("Inst_LDWAG", resourceCulture);

	public static string Inst_LDWAGE => ResourceManager.GetString("Inst_LDWAGE", resourceCulture);

	public static string Inst_LDWAL => ResourceManager.GetString("Inst_LDWAL", resourceCulture);

	public static string Inst_LDWALE => ResourceManager.GetString("Inst_LDWALE", resourceCulture);

	public static string Inst_LDWANE => ResourceManager.GetString("Inst_LDWANE", resourceCulture);

	public static string Inst_LDWMAEQ => ResourceManager.GetString("Inst_LDWMAEQ", resourceCulture);

	public static string Inst_LDWMAG => ResourceManager.GetString("Inst_LDWMAG", resourceCulture);

	public static string Inst_LDWMAGE => ResourceManager.GetString("Inst_LDWMAGE", resourceCulture);

	public static string Inst_LDWMAL => ResourceManager.GetString("Inst_LDWMAL", resourceCulture);

	public static string Inst_LDWMALE => ResourceManager.GetString("Inst_LDWMALE", resourceCulture);

	public static string Inst_LDWMANE => ResourceManager.GetString("Inst_LDWMANE", resourceCulture);

	public static string Inst_LDWMEQ => ResourceManager.GetString("Inst_LDWMEQ", resourceCulture);

	public static string Inst_LDWMG => ResourceManager.GetString("Inst_LDWMG", resourceCulture);

	public static string Inst_LDWMGE => ResourceManager.GetString("Inst_LDWMGE", resourceCulture);

	public static string Inst_LDWML => ResourceManager.GetString("Inst_LDWML", resourceCulture);

	public static string Inst_LDWMLE => ResourceManager.GetString("Inst_LDWMLE", resourceCulture);

	public static string Inst_LDWMNE => ResourceManager.GetString("Inst_LDWMNE", resourceCulture);

	public static string Inst_LIMIT => ResourceManager.GetString("Inst_LIMIT", resourceCulture);

	public static string Inst_LIMITD => ResourceManager.GetString("Inst_LIMITD", resourceCulture);

	public static string Inst_LIMITF => ResourceManager.GetString("Inst_LIMITF", resourceCulture);

	public static string Inst_List => ResourceManager.GetString("Inst_List", resourceCulture);

	public static string Inst_LogicOperation => ResourceManager.GetString("Inst_LogicOperation", resourceCulture);

	public static string Inst_MAX => ResourceManager.GetString("Inst_MAX", resourceCulture);

	public static string Inst_MAXD => ResourceManager.GetString("Inst_MAXD", resourceCulture);

	public static string Inst_MAXF => ResourceManager.GetString("Inst_MAXF", resourceCulture);

	public static string Inst_MIN => ResourceManager.GetString("Inst_MIN", resourceCulture);

	public static string Inst_MIND => ResourceManager.GetString("Inst_MIND", resourceCulture);

	public static string Inst_MINF => ResourceManager.GetString("Inst_MINF", resourceCulture);

	public static string Inst_MOD => ResourceManager.GetString("Inst_MOD", resourceCulture);

	public static string Inst_MODD => ResourceManager.GetString("Inst_MODD", resourceCulture);

	public static string Inst_Move => ResourceManager.GetString("Inst_Move", resourceCulture);

	public static string Inst_ORCALL => ResourceManager.GetString("Inst_ORCALL", resourceCulture);

	public static string Inst_ORCALLM => ResourceManager.GetString("Inst_ORCALLM", resourceCulture);

	public static string Inst_PID => ResourceManager.GetString("Inst_PID", resourceCulture);

	public static string Inst_PLF => ResourceManager.GetString("Inst_PLF", resourceCulture);

	public static string Inst_PLS => ResourceManager.GetString("Inst_PLS", resourceCulture);

	public static string Inst_ProgramControl => ResourceManager.GetString("Inst_ProgramControl", resourceCulture);

	public static string Inst_Pulse => ResourceManager.GetString("Inst_Pulse", resourceCulture);

	public static string Inst_R_S => ResourceManager.GetString("Inst_R_S", resourceCulture);

	public static string Inst_RAD => ResourceManager.GetString("Inst_RAD", resourceCulture);

	public static string Inst_RCL => ResourceManager.GetString("Inst_RCL", resourceCulture);

	public static string Inst_RCLD => ResourceManager.GetString("Inst_RCLD", resourceCulture);

	public static string Inst_RCR => ResourceManager.GetString("Inst_RCR", resourceCulture);

	public static string Inst_RCRD => ResourceManager.GetString("Inst_RCRD", resourceCulture);

	public static string Inst_RealTime => ResourceManager.GetString("Inst_RealTime", resourceCulture);

	public static string Inst_REF => ResourceManager.GetString("Inst_REF", resourceCulture);

	public static string Inst_RET => ResourceManager.GetString("Inst_RET", resourceCulture);

	public static string Inst_S_DI => ResourceManager.GetString("Inst_S_DI", resourceCulture);

	public static string Inst_S_I => ResourceManager.GetString("Inst_S_I", resourceCulture);

	public static string Inst_S_R => ResourceManager.GetString("Inst_S_R", resourceCulture);

	public static string Inst_SCL => ResourceManager.GetString("Inst_SCL", resourceCulture);

	public static string Inst_SCL2 => ResourceManager.GetString("Inst_SCL2", resourceCulture);

	public static string Inst_SCL2D => ResourceManager.GetString("Inst_SCL2D", resourceCulture);

	public static string Inst_SCL2F => ResourceManager.GetString("Inst_SCL2F", resourceCulture);

	public static string Inst_SCLD => ResourceManager.GetString("Inst_SCLD", resourceCulture);

	public static string Inst_SCLF => ResourceManager.GetString("Inst_SCLF", resourceCulture);

	public static string Inst_Select => ResourceManager.GetString("Inst_Select", resourceCulture);

	public static string Inst_SER => ResourceManager.GetString("Inst_SER", resourceCulture);

	public static string Inst_SERD => ResourceManager.GetString("Inst_SERD", resourceCulture);

	public static string Inst_SERF => ResourceManager.GetString("Inst_SERF", resourceCulture);

	public static string Inst_SFRD => ResourceManager.GetString("Inst_SFRD", resourceCulture);

	public static string Inst_SFRDD => ResourceManager.GetString("Inst_SFRDD", resourceCulture);

	public static string Inst_SFRDF => ResourceManager.GetString("Inst_SFRDF", resourceCulture);

	public static string Inst_SFWR => ResourceManager.GetString("Inst_SFWR", resourceCulture);

	public static string Inst_SFWRD => ResourceManager.GetString("Inst_SFWRD", resourceCulture);

	public static string Inst_SFWRF => ResourceManager.GetString("Inst_SFWRF", resourceCulture);

	public static string Inst_Shift => ResourceManager.GetString("Inst_Shift", resourceCulture);

	public static string Inst_SORT2 => ResourceManager.GetString("Inst_SORT2", resourceCulture);

	public static string Inst_STR => ResourceManager.GetString("Inst_STR", resourceCulture);

	public static string Inst_STRADD => ResourceManager.GetString("Inst_STRADD", resourceCulture);

	public static string Inst_STRD => ResourceManager.GetString("Inst_STRD", resourceCulture);

	public static string Inst_String => ResourceManager.GetString("Inst_String", resourceCulture);

	public static string Inst_String1 => ResourceManager.GetString("Inst_String1", resourceCulture);

	public static string Inst_STRLEFT => ResourceManager.GetString("Inst_STRLEFT", resourceCulture);

	public static string Inst_STRLEN => ResourceManager.GetString("Inst_STRLEN", resourceCulture);

	public static string Inst_STRMIDR => ResourceManager.GetString("Inst_STRMIDR", resourceCulture);

	public static string Inst_STRMIDW => ResourceManager.GetString("Inst_STRMIDW", resourceCulture);

	public static string Inst_STRMOV => ResourceManager.GetString("Inst_STRMOV", resourceCulture);

	public static string Inst_STRRIGHT => ResourceManager.GetString("Inst_STRRIGHT", resourceCulture);

	public static string Inst_SUM => ResourceManager.GetString("Inst_SUM", resourceCulture);

	public static string Inst_SUMB => ResourceManager.GetString("Inst_SUMB", resourceCulture);

	public static string Inst_SUMD => ResourceManager.GetString("Inst_SUMD", resourceCulture);

	public static string Inst_SUMF => ResourceManager.GetString("Inst_SUMF", resourceCulture);

	public static string Inst_TECAM => ResourceManager.GetString("Inst_TECAM", resourceCulture);

	public static string Inst_TECAM_DATASET => ResourceManager.GetString("Inst_TECAM_DATASET", resourceCulture);

	public static string Inst_Timer => ResourceManager.GetString("Inst_Timer", resourceCulture);

	public static string Inst_TKY => ResourceManager.GetString("Inst_TKY", resourceCulture);

	public static string Inst_TKYD => ResourceManager.GetString("Inst_TKYD", resourceCulture);

	public static string Inst_UNI => ResourceManager.GetString("Inst_UNI", resourceCulture);

	public static string Inst_VAL => ResourceManager.GetString("Inst_VAL", resourceCulture);

	public static string Inst_VALD => ResourceManager.GetString("Inst_VALD", resourceCulture);

	public static string Inst_WDE => ResourceManager.GetString("Inst_WDE", resourceCulture);

	public static string Inst_WOF => ResourceManager.GetString("Inst_WOF", resourceCulture);

	public static string Inst_WON => ResourceManager.GetString("Inst_WON", resourceCulture);

	public static string Inst_WSFL => ResourceManager.GetString("Inst_WSFL", resourceCulture);

	public static string Inst_WSFR => ResourceManager.GetString("Inst_WSFR", resourceCulture);

	public static string Inst_WTOB => ResourceManager.GetString("Inst_WTOB", resourceCulture);

	public static string Inst_WTOF => ResourceManager.GetString("Inst_WTOF", resourceCulture);

	public static string Inst_WUE => ResourceManager.GetString("Inst_WUE", resourceCulture);

	public static string Inst_ZONE => ResourceManager.GetString("Inst_ZONE", resourceCulture);

	public static string Inst_ZONED => ResourceManager.GetString("Inst_ZONED", resourceCulture);

	public static string Inst_ZONEF => ResourceManager.GetString("Inst_ZONEF", resourceCulture);

	public static string Inst_ZPOP => ResourceManager.GetString("Inst_ZPOP", resourceCulture);

	public static string Inst_ZPUSH => ResourceManager.GetString("Inst_ZPUSH", resourceCulture);

	public static string InstError_ANDB => ResourceManager.GetString("InstError_ANDB", resourceCulture);

	public static string InstError_EmptyLine => ResourceManager.GetString("InstError_EmptyLine", resourceCulture);

	public static string InstError_InvalidInst => ResourceManager.GetString("InstError_InvalidInst", resourceCulture);

	public static string InstError_InvalidTail => ResourceManager.GetString("InstError_InvalidTail", resourceCulture);

	public static string InstError_LDInvalid => ResourceManager.GetString("InstError_LDInvalid", resourceCulture);

	public static string InstError_MPPEmpty => ResourceManager.GetString("InstError_MPPEmpty", resourceCulture);

	public static string InstError_MPSNotPop => ResourceManager.GetString("InstError_MPSNotPop", resourceCulture);

	public static string InstError_MRDNotPop => ResourceManager.GetString("InstError_MRDNotPop", resourceCulture);

	public static string InstError_ORB => ResourceManager.GetString("InstError_ORB", resourceCulture);

	public static string InstError_ORBRight => ResourceManager.GetString("InstError_ORBRight", resourceCulture);

	public static string InstError_ORRight => ResourceManager.GetString("InstError_ORRight", resourceCulture);

	public static string InstError_OUTNotCond => ResourceManager.GetString("InstError_OUTNotCond", resourceCulture);

	public static string InstError_POPInvalid => ResourceManager.GetString("InstError_POPInvalid", resourceCulture);

	public static string InstError_POPMustBe => ResourceManager.GetString("InstError_POPMustBe", resourceCulture);

	public static string InstError_PSHCond => ResourceManager.GetString("InstError_PSHCond", resourceCulture);

	public static string Instruction => ResourceManager.GetString("Instruction", resourceCulture);

	public static string Instruction_Input => ResourceManager.GetString("Instruction_Input", resourceCulture);

	public static string Integer_To_BCD_Code => ResourceManager.GetString("Integer_To_BCD_Code", resourceCulture);

	public static string Integration_Time => ResourceManager.GetString("Integration_Time", resourceCulture);

	public static string Interrupt_Condition => ResourceManager.GetString("Interrupt_Condition", resourceCulture);

	public static string Intra_Address => ResourceManager.GetString("Intra_Address", resourceCulture);

	public static string Intra_Cross => ResourceManager.GetString("Intra_Cross", resourceCulture);

	public static string Intra_Empty => ResourceManager.GetString("Intra_Empty", resourceCulture);

	public static string Invalid_Funcblock_Name => ResourceManager.GetString("Invalid_Funcblock_Name", resourceCulture);

	public static string Invalid_Input => ResourceManager.GetString("Invalid_Input", resourceCulture);

	public static string IOAccess => ResourceManager.GetString("IOAccess", resourceCulture);

	public static string IOAccess_ReadOnly => ResourceManager.GetString("IOAccess_ReadOnly", resourceCulture);

	public static string IOAccess_ReadWrite => ResourceManager.GetString("IOAccess_ReadWrite", resourceCulture);

	public static string IOAccess_WriteOnly => ResourceManager.GetString("IOAccess_WriteOnly", resourceCulture);

	public static string IP_Address => ResourceManager.GetString("IP_Address", resourceCulture);

	public static string IP_Address_Error => ResourceManager.GetString("IP_Address_Error", resourceCulture);

	public static string IP_Address_Tooltip => ResourceManager.GetString("IP_Address_Tooltip", resourceCulture);

	public static string is_illegal => ResourceManager.GetString("is_illegal", resourceCulture);

	public static string IsEnable_Watchdog => ResourceManager.GetString("IsEnable_Watchdog", resourceCulture);

	public static string IsUsedFixed => ResourceManager.GetString("IsUsedFixed", resourceCulture);

	public static string Item_Rename => ResourceManager.GetString("Item_Rename", resourceCulture);

	public static string JMP_Inst => ResourceManager.GetString("JMP_Inst", resourceCulture);

	public static string JOG_Inst => ResourceManager.GetString("JOG_Inst", resourceCulture);

	public static string Jump => ResourceManager.GetString("Jump", resourceCulture);

	public static string Jump_Mark => ResourceManager.GetString("Jump_Mark", resourceCulture);

	public static string Key_Combination => ResourceManager.GetString("Key_Combination", resourceCulture);

	public static string Key_Pressed => ResourceManager.GetString("Key_Pressed", resourceCulture);

	public static string Lable => ResourceManager.GetString("Lable", resourceCulture);

	public static string Ladder => ResourceManager.GetString("Ladder", resourceCulture);

	public static string Ladder_Building_Block => ResourceManager.GetString("Ladder_Building_Block", resourceCulture);

	public static string Ladder_Changed => ResourceManager.GetString("Ladder_Changed", resourceCulture);

	public static string Ladder_Check => ResourceManager.GetString("Ladder_Check", resourceCulture);

	public static string Ladder_Construct_Forbid => ResourceManager.GetString("Ladder_Construct_Forbid", resourceCulture);

	public static string Ladder_Correct => ResourceManager.GetString("Ladder_Correct", resourceCulture);

	public static string Ladder_Error => ResourceManager.GetString("Ladder_Error", resourceCulture);

	public static string Ladder_Locked => ResourceManager.GetString("Ladder_Locked", resourceCulture);

	public static string Ladder_Network => ResourceManager.GetString("Ladder_Network", resourceCulture);

	public static string Ladder_Program => ResourceManager.GetString("Ladder_Program", resourceCulture);

	public static string Ladder_Protection => ResourceManager.GetString("Ladder_Protection", resourceCulture);

	public static string Ladder_Title => ResourceManager.GetString("Ladder_Title", resourceCulture);

	public static string LadderConvertDialog_ConvertType => ResourceManager.GetString("LadderConvertDialog_ConvertType", resourceCulture);

	public static string LadderConvertDialog_Title => ResourceManager.GetString("LadderConvertDialog_Title", resourceCulture);

	public static string LadderDiagram => ResourceManager.GetString("LadderDiagram", resourceCulture);

	public static string LadderDiagram_check => ResourceManager.GetString("LadderDiagram_check", resourceCulture);

	public static string LadderDiagram_Edit_Program_Comment => ResourceManager.GetString("LadderDiagram_Edit_Program_Comment", resourceCulture);

	public static string LadderDiagram_Program_Comment => ResourceManager.GetString("LadderDiagram_Program_Comment", resourceCulture);

	public static string LadderNetwork_AddBreakpoint => ResourceManager.GetString("LadderNetwork_AddBreakpoint", resourceCulture);

	public static string LadderNetwork_Edit_Network_Comment => ResourceManager.GetString("LadderNetwork_Edit_Network_Comment", resourceCulture);

	public static string LadderNetwork_Element_Actions => ResourceManager.GetString("LadderNetwork_Element_Actions", resourceCulture);

	public static string LadderNetwork_Element_Copy => ResourceManager.GetString("LadderNetwork_Element_Copy", resourceCulture);

	public static string LadderNetwork_Element_Cut => ResourceManager.GetString("LadderNetwork_Element_Cut", resourceCulture);

	public static string LadderNetwork_Element_Delete => ResourceManager.GetString("LadderNetwork_Element_Delete", resourceCulture);

	public static string LadderNetwork_Element_LineDown => ResourceManager.GetString("LadderNetwork_Element_LineDown", resourceCulture);

	public static string LadderNetwork_Element_LineLeft => ResourceManager.GetString("LadderNetwork_Element_LineLeft", resourceCulture);

	public static string LadderNetwork_Element_LineRight => ResourceManager.GetString("LadderNetwork_Element_LineRight", resourceCulture);

	public static string LadderNetwork_Element_LineUp => ResourceManager.GetString("LadderNetwork_Element_LineUp", resourceCulture);

	public static string LadderNetwork_Element_Paste => ResourceManager.GetString("LadderNetwork_Element_Paste", resourceCulture);

	public static string LadderNetwork_Element_PushDown => ResourceManager.GetString("LadderNetwork_Element_PushDown", resourceCulture);

	public static string LadderNetwork_Element_PushLeft => ResourceManager.GetString("LadderNetwork_Element_PushLeft", resourceCulture);

	public static string LadderNetwork_Element_PushRight => ResourceManager.GetString("LadderNetwork_Element_PushRight", resourceCulture);

	public static string LadderNetwork_Element_PushUp => ResourceManager.GetString("LadderNetwork_Element_PushUp", resourceCulture);

	public static string LadderNetwork_Insert_After => ResourceManager.GetString("LadderNetwork_Insert_After", resourceCulture);

	public static string LadderNetwork_Insert_before => ResourceManager.GetString("LadderNetwork_Insert_before", resourceCulture);

	public static string LadderNetwork_Insert_End => ResourceManager.GetString("LadderNetwork_Insert_End", resourceCulture);

	public static string LadderNetwork_JumpToThis => ResourceManager.GetString("LadderNetwork_JumpToThis", resourceCulture);

	public static string LadderNetwork_Network_Copy => ResourceManager.GetString("LadderNetwork_Network_Copy", resourceCulture);

	public static string LadderNetwork_Network_Cut => ResourceManager.GetString("LadderNetwork_Network_Cut", resourceCulture);

	public static string LadderNetwork_Network_Delete => ResourceManager.GetString("LadderNetwork_Network_Delete", resourceCulture);

	public static string LadderNetwork_Network_Insert => ResourceManager.GetString("LadderNetwork_Network_Insert", resourceCulture);

	public static string LadderNetwork_Network_Merge => ResourceManager.GetString("LadderNetwork_Network_Merge", resourceCulture);

	public static string LadderNetwork_Network_Paste => ResourceManager.GetString("LadderNetwork_Network_Paste", resourceCulture);

	public static string LadderNetwork_Network_Split => ResourceManager.GetString("LadderNetwork_Network_Split", resourceCulture);

	public static string LadderNetwork_RemoveBreakpoint => ResourceManager.GetString("LadderNetwork_RemoveBreakpoint", resourceCulture);

	public static string LadderNetwork_SettingBreakpoint => ResourceManager.GetString("LadderNetwork_SettingBreakpoint", resourceCulture);

	public static string LadderNetwork_Shield_Network => ResourceManager.GetString("LadderNetwork_Shield_Network", resourceCulture);

	public static string LadderUnit_LocationError => ResourceManager.GetString("LadderUnit_LocationError", resourceCulture);

	public static string LaddreDiagram => ResourceManager.GetString("LaddreDiagram", resourceCulture);

	public static string Language_Locale_Setting => ResourceManager.GetString("Language_Locale_Setting", resourceCulture);

	public static string LBL_Inst => ResourceManager.GetString("LBL_Inst", resourceCulture);

	public static string Length => ResourceManager.GetString("Length", resourceCulture);

	public static string Library_Function => ResourceManager.GetString("Library_Function", resourceCulture);

	public static string Lifted => ResourceManager.GetString("Lifted", resourceCulture);

	public static string Line_Too_Much => ResourceManager.GetString("Line_Too_Much", resourceCulture);

	public static string LineComment => ResourceManager.GetString("LineComment", resourceCulture);

	public static string LineComment_EditAfter => ResourceManager.GetString("LineComment_EditAfter", resourceCulture);

	public static string LineComment_EditBefore => ResourceManager.GetString("LineComment_EditBefore", resourceCulture);

	public static string LineComment_InsertAfter => ResourceManager.GetString("LineComment_InsertAfter", resourceCulture);

	public static string LineComment_InsertBefore => ResourceManager.GetString("LineComment_InsertBefore", resourceCulture);

	public static string LineComment_Modify => ResourceManager.GetString("LineComment_Modify", resourceCulture);

	public static string LineComment_RemoveAfter => ResourceManager.GetString("LineComment_RemoveAfter", resourceCulture);

	public static string LineComment_RemoveBefore => ResourceManager.GetString("LineComment_RemoveBefore", resourceCulture);

	public static string LINEF_Inst => ResourceManager.GetString("LINEF_Inst", resourceCulture);

	public static string LINEI_Inst => ResourceManager.GetString("LINEI_Inst", resourceCulture);

	public static string LineNumber => ResourceManager.GetString("LineNumber", resourceCulture);

	public static string LN_Operation => ResourceManager.GetString("LN_Operation", resourceCulture);

	public static string Loading_CheckExpansion => ResourceManager.GetString("Loading_CheckExpansion", resourceCulture);

	public static string LoadInitDataFromPLC => ResourceManager.GetString("LoadInitDataFromPLC", resourceCulture);

	public static string Local_IPAddress => ResourceManager.GetString("Local_IPAddress", resourceCulture);

	public static string Local_Port => ResourceManager.GetString("Local_Port", resourceCulture);

	public static string LocalVar_Comment => ResourceManager.GetString("LocalVar_Comment", resourceCulture);

	public static string LocalVar_Diagram => ResourceManager.GetString("LocalVar_Diagram", resourceCulture);

	public static string LocalVar_Name => ResourceManager.GetString("LocalVar_Name", resourceCulture);

	public static string LocalVariable => ResourceManager.GetString("LocalVariable", resourceCulture);

	public static string Location_illegal => ResourceManager.GetString("Location_illegal", resourceCulture);

	public static string Lock => ResourceManager.GetString("Lock", resourceCulture);

	public static string LOG_Inst => ResourceManager.GetString("LOG_Inst", resourceCulture);

	public static string Lower_Limit_Of_Reference => ResourceManager.GetString("Lower_Limit_Of_Reference", resourceCulture);

	public static string Magnification => ResourceManager.GetString("Magnification", resourceCulture);

	public static string MainDiagram_ProhibitArgument => ResourceManager.GetString("MainDiagram_ProhibitArgument", resourceCulture);

	public static string MainRoutine => ResourceManager.GetString("MainRoutine", resourceCulture);

	public static string MainWindow_About => ResourceManager.GetString("MainWindow_About", resourceCulture);

	public static string MainWindow_Add_FuncBlock => ResourceManager.GetString("MainWindow_Add_FuncBlock", resourceCulture);

	public static string MainWindow_Add_Modbus_Table => ResourceManager.GetString("MainWindow_Add_Modbus_Table", resourceCulture);

	public static string MainWindow_Add_SubRoutine => ResourceManager.GetString("MainWindow_Add_SubRoutine", resourceCulture);

	public static string MainWindow_Backup_Recovery => ResourceManager.GetString("MainWindow_Backup_Recovery", resourceCulture);

	public static string MainWindow_Breakpoint => ResourceManager.GetString("MainWindow_Breakpoint", resourceCulture);

	public static string MainWindow_Breakpoint_List => ResourceManager.GetString("MainWindow_Breakpoint_List", resourceCulture);

	public static string MainWindow_By_Process => ResourceManager.GetString("MainWindow_By_Process", resourceCulture);

	public static string MainWindow_By_Sentence => ResourceManager.GetString("MainWindow_By_Sentence", resourceCulture);

	public static string MainWindow_Chinese => ResourceManager.GetString("MainWindow_Chinese", resourceCulture);

	public static string MainWindow_Close_Proj => ResourceManager.GetString("MainWindow_Close_Proj", resourceCulture);

	public static string MainWindow_Column_Delete => ResourceManager.GetString("MainWindow_Column_Delete", resourceCulture);

	public static string MainWindow_Column_Insert => ResourceManager.GetString("MainWindow_Column_Insert", resourceCulture);

	public static string MainWindow_Comment_Mode => ResourceManager.GetString("MainWindow_Comment_Mode", resourceCulture);

	public static string MainWindow_Communication_Settings => ResourceManager.GetString("MainWindow_Communication_Settings", resourceCulture);

	public static string MainWindow_Compile => ResourceManager.GetString("MainWindow_Compile", resourceCulture);

	public static string MainWindow_Copy => ResourceManager.GetString("MainWindow_Copy", resourceCulture);

	public static string MainWindow_Current_Breakpoint => ResourceManager.GetString("MainWindow_Current_Breakpoint", resourceCulture);

	public static string MainWindow_Cut => ResourceManager.GetString("MainWindow_Cut", resourceCulture);

	public static string MainWindow_Del => ResourceManager.GetString("MainWindow_Del", resourceCulture);

	public static string MainWindow_Download => ResourceManager.GetString("MainWindow_Download", resourceCulture);

	public static string MainWindow_Edit => ResourceManager.GetString("MainWindow_Edit", resourceCulture);

	public static string MainWindow_Element_List => ResourceManager.GetString("MainWindow_Element_List", resourceCulture);

	public static string MainWindow_English => ResourceManager.GetString("MainWindow_English", resourceCulture);

	public static string MainWindow_Error_List => ResourceManager.GetString("MainWindow_Error_List", resourceCulture);

	public static string MainWindow_Exit => ResourceManager.GetString("MainWindow_Exit", resourceCulture);

	public static string MainWindow_Falling_Edge_Of_Result => ResourceManager.GetString("MainWindow_Falling_Edge_Of_Result", resourceCulture);

	public static string MainWindow_Falling_Edge_Pulse => ResourceManager.GetString("MainWindow_Falling_Edge_Pulse", resourceCulture);

	public static string MainWindow_File => ResourceManager.GetString("MainWindow_File", resourceCulture);

	public static string MainWindow_File_Converter => ResourceManager.GetString("MainWindow_File_Converter", resourceCulture);

	public static string MainWindow_Funcblock_Check => ResourceManager.GetString("MainWindow_Funcblock_Check", resourceCulture);

	public static string MainWindow_Help => ResourceManager.GetString("MainWindow_Help", resourceCulture);

	public static string MainWindow_Horizontal_Line => ResourceManager.GetString("MainWindow_Horizontal_Line", resourceCulture);

	public static string MainWindow_Horizontal_Line_Delete => ResourceManager.GetString("MainWindow_Horizontal_Line_Delete", resourceCulture);

	public static string MainWindow_Immediately_Close => ResourceManager.GetString("MainWindow_Immediately_Close", resourceCulture);

	public static string MainWindow_Immediately_Open => ResourceManager.GetString("MainWindow_Immediately_Open", resourceCulture);

	public static string MainWindow_Immediately_Output_Coil => ResourceManager.GetString("MainWindow_Immediately_Output_Coil", resourceCulture);

	public static string MainWindow_Inst_Mode => ResourceManager.GetString("MainWindow_Inst_Mode", resourceCulture);

	public static string MainWindow_Jump_Out => ResourceManager.GetString("MainWindow_Jump_Out", resourceCulture);

	public static string MainWindow_Ladder_Check => ResourceManager.GetString("MainWindow_Ladder_Check", resourceCulture);

	public static string MainWindow_Ladder_Mode => ResourceManager.GetString("MainWindow_Ladder_Mode", resourceCulture);

	public static string MainWindow_Language => ResourceManager.GetString("MainWindow_Language", resourceCulture);

	public static string MainWindow_Monitor => ResourceManager.GetString("MainWindow_Monitor", resourceCulture);

	public static string MainWindow_Monitor_Key => ResourceManager.GetString("MainWindow_Monitor_Key", resourceCulture);

	public static string MainWindow_Monitor_List => ResourceManager.GetString("MainWindow_Monitor_List", resourceCulture);

	public static string MainWindow_New_Proj => ResourceManager.GetString("MainWindow_New_Proj", resourceCulture);

	public static string MainWindow_Normally_Closed => ResourceManager.GetString("MainWindow_Normally_Closed", resourceCulture);

	public static string MainWindow_Normally_Open_Contact => ResourceManager.GetString("MainWindow_Normally_Open_Contact", resourceCulture);

	public static string MainWindow_Online_Upgrade => ResourceManager.GetString("MainWindow_Online_Upgrade", resourceCulture);

	public static string MainWindow_Open_Proj => ResourceManager.GetString("MainWindow_Open_Proj", resourceCulture);

	public static string MainWindow_Option => ResourceManager.GetString("MainWindow_Option", resourceCulture);

	public static string MainWindow_Output_Coil => ResourceManager.GetString("MainWindow_Output_Coil", resourceCulture);

	public static string MainWindow_Paste => ResourceManager.GetString("MainWindow_Paste", resourceCulture);

	public static string MainWindow_PLC_Settings => ResourceManager.GetString("MainWindow_PLC_Settings", resourceCulture);

	public static string MainWindow_Proj => ResourceManager.GetString("MainWindow_Proj", resourceCulture);

	public static string MainWindow_Proj_Check => ResourceManager.GetString("MainWindow_Proj_Check", resourceCulture);

	public static string MainWindow_Project_Explorer => ResourceManager.GetString("MainWindow_Project_Explorer", resourceCulture);

	public static string MainWindow_Property_Proj => ResourceManager.GetString("MainWindow_Property_Proj", resourceCulture);

	public static string MainWindow_Pulse => ResourceManager.GetString("MainWindow_Pulse", resourceCulture);

	public static string MainWindow_PulseMonitor => ResourceManager.GetString("MainWindow_PulseMonitor", resourceCulture);

	public static string MainWindow_Recent_Proj => ResourceManager.GetString("MainWindow_Recent_Proj", resourceCulture);

	public static string MainWindow_Redo => ResourceManager.GetString("MainWindow_Redo", resourceCulture);

	public static string MainWindow_Replace => ResourceManager.GetString("MainWindow_Replace", resourceCulture);

	public static string MainWindow_Ret_Edit_Mode => ResourceManager.GetString("MainWindow_Ret_Edit_Mode", resourceCulture);

	public static string MainWindow_Reversed_Result => ResourceManager.GetString("MainWindow_Reversed_Result", resourceCulture);

	public static string MainWindow_Rising_Edge_Of_Result => ResourceManager.GetString("MainWindow_Rising_Edge_Of_Result", resourceCulture);

	public static string MainWindow_Rising_Edge_Pulse => ResourceManager.GetString("MainWindow_Rising_Edge_Pulse", resourceCulture);

	public static string MainWindow_Row_Delete => ResourceManager.GetString("MainWindow_Row_Delete", resourceCulture);

	public static string MainWindow_Row_Insert => ResourceManager.GetString("MainWindow_Row_Insert", resourceCulture);

	public static string MainWindow_Save_As_Proj => ResourceManager.GetString("MainWindow_Save_As_Proj", resourceCulture);

	public static string MainWindow_Save_Proj => ResourceManager.GetString("MainWindow_Save_Proj", resourceCulture);

	public static string MainWindow_Search => ResourceManager.GetString("MainWindow_Search", resourceCulture);

	public static string MainWindow_Select_All => ResourceManager.GetString("MainWindow_Select_All", resourceCulture);

	public static string MainWindow_Simulation => ResourceManager.GetString("MainWindow_Simulation", resourceCulture);

	public static string MainWindow_Simulation_Key => ResourceManager.GetString("MainWindow_Simulation_Key", resourceCulture);

	public static string MainWindow_Simulation_Pause => ResourceManager.GetString("MainWindow_Simulation_Pause", resourceCulture);

	public static string MainWindow_Simulation_Run => ResourceManager.GetString("MainWindow_Simulation_Run", resourceCulture);

	public static string MainWindow_Simulation_Stop => ResourceManager.GetString("MainWindow_Simulation_Stop", resourceCulture);

	public static string MainWindow_Soft_Element_Init => ResourceManager.GetString("MainWindow_Soft_Element_Init", resourceCulture);

	public static string MainWindow_Soft_Element_Manager => ResourceManager.GetString("MainWindow_Soft_Element_Manager", resourceCulture);

	public static string MainWindow_System_Settings => ResourceManager.GetString("MainWindow_System_Settings", resourceCulture);

	public static string MainWindow_Text_Mode => ResourceManager.GetString("MainWindow_Text_Mode", resourceCulture);

	public static string MainWindow_Tool => ResourceManager.GetString("MainWindow_Tool", resourceCulture);

	public static string MainWindow_Undo => ResourceManager.GetString("MainWindow_Undo", resourceCulture);

	public static string MainWindow_Upload => ResourceManager.GetString("MainWindow_Upload", resourceCulture);

	public static string MainWindow_ValueBrpo => ResourceManager.GetString("MainWindow_ValueBrpo", resourceCulture);

	public static string MainWindow_Vertical_Line => ResourceManager.GetString("MainWindow_Vertical_Line", resourceCulture);

	public static string MainWindow_Vertical_Line_Delete => ResourceManager.GetString("MainWindow_Vertical_Line_Delete", resourceCulture);

	public static string MainWindow_View => ResourceManager.GetString("MainWindow_View", resourceCulture);

	public static string MainWindow_View_Help => ResourceManager.GetString("MainWindow_View_Help", resourceCulture);

	public static string MainWindow_Zoom_In => ResourceManager.GetString("MainWindow_Zoom_In", resourceCulture);

	public static string MainWindow_Zoom_Out => ResourceManager.GetString("MainWindow_Zoom_Out", resourceCulture);

	public static string MainWindowTitle_IsMonitoring => ResourceManager.GetString("MainWindowTitle_IsMonitoring", resourceCulture);

	public static string MainWindowTitle_IsSimulating => ResourceManager.GetString("MainWindowTitle_IsSimulating", resourceCulture);

	public static string Manual => ResourceManager.GetString("Manual", resourceCulture);

	public static string Max_Number_File => ResourceManager.GetString("Max_Number_File", resourceCulture);

	public static string Max_ScanCycle => ResourceManager.GetString("Max_ScanCycle", resourceCulture);

	public static string Maximize => ResourceManager.GetString("Maximize", resourceCulture);

	public static string MBUS_Inst => ResourceManager.GetString("MBUS_Inst", resourceCulture);

	public static string MBUSTCP_Inst => ResourceManager.GetString("MBUSTCP_Inst", resourceCulture);

	public static string Memory_Access_Error => ResourceManager.GetString("Memory_Access_Error", resourceCulture);

	public static string Memory_Used => ResourceManager.GetString("Memory_Used", resourceCulture);

	public static string Menu_Search => ResourceManager.GetString("Menu_Search", resourceCulture);

	public static string MenuItem_Arguments => ResourceManager.GetString("MenuItem_Arguments", resourceCulture);

	public static string MenuItem_GenerateUsbFile => ResourceManager.GetString("MenuItem_GenerateUsbFile", resourceCulture);

	public static string MenuItem_GetPlcHardFault => ResourceManager.GetString("MenuItem_GetPlcHardFault", resourceCulture);

	public static string MenuItem_GetPlcVersion => ResourceManager.GetString("MenuItem_GetPlcVersion", resourceCulture);

	public static string MenuItem_MoreDownload => ResourceManager.GetString("MenuItem_MoreDownload", resourceCulture);

	public static string MenuItem_ProjectCompare => ResourceManager.GetString("MenuItem_ProjectCompare", resourceCulture);

	public static string MenuItem_StringMonitor => ResourceManager.GetString("MenuItem_StringMonitor", resourceCulture);

	public static string MenuItem_SwitchPLCToRun => ResourceManager.GetString("MenuItem_SwitchPLCToRun", resourceCulture);

	public static string MenuItem_SwitchPLCToStop => ResourceManager.GetString("MenuItem_SwitchPLCToStop", resourceCulture);

	public static string MenuItem_ValueLine => ResourceManager.GetString("MenuItem_ValueLine", resourceCulture);

	public static string Message_AdjustExpansion => ResourceManager.GetString("Message_AdjustExpansion", resourceCulture);

	public static string Message_Alias_Exist => ResourceManager.GetString("Message_Alias_Exist", resourceCulture);

	public static string Message_Already_Exist => ResourceManager.GetString("Message_Already_Exist", resourceCulture);

	public static string Message_AppOutOfLimit => ResourceManager.GetString("Message_AppOutOfLimit", resourceCulture);

	public static string Message_AreaOutInst => ResourceManager.GetString("Message_AreaOutInst", resourceCulture);

	public static string Message_ArgNumberError => ResourceManager.GetString("Message_ArgNumberError", resourceCulture);

	public static string Message_Bit_Required => ResourceManager.GetString("Message_Bit_Required", resourceCulture);

	public static string Message_BranchOverError => ResourceManager.GetString("Message_BranchOverError", resourceCulture);

	public static string Message_BREAK => ResourceManager.GetString("Message_BREAK", resourceCulture);

	public static string Message_CALL => ResourceManager.GetString("Message_CALL", resourceCulture);

	public static string Message_CALLMArgumentInvalid => ResourceManager.GetString("Message_CALLMArgumentInvalid", resourceCulture);

	public static string Message_CALLMNotMatch => ResourceManager.GetString("Message_CALLMNotMatch", resourceCulture);

	public static string Message_Can_Not_Be_Written => ResourceManager.GetString("Message_Can_Not_Be_Written", resourceCulture);

	public static string Message_Can_Not_CALL => ResourceManager.GetString("Message_Can_Not_CALL", resourceCulture);

	public static string Message_CannotTailAdd => ResourceManager.GetString("Message_CannotTailAdd", resourceCulture);

	public static string Message_CFunc_Not_Found => ResourceManager.GetString("Message_CFunc_Not_Found", resourceCulture);

	public static string Message_Changed => ResourceManager.GetString("Message_Changed", resourceCulture);

	public static string Message_ClickHereToUpdate => ResourceManager.GetString("Message_ClickHereToUpdate", resourceCulture);

	public static string Message_CommentCrossError => ResourceManager.GetString("Message_CommentCrossError", resourceCulture);

	public static string Message_Confirm_Save => ResourceManager.GetString("Message_Confirm_Save", resourceCulture);

	public static string Message_Converter_Error => ResourceManager.GetString("Message_Converter_Error", resourceCulture);

	public static string Message_Default => ResourceManager.GetString("Message_Default", resourceCulture);

	public static string Message_Delete => ResourceManager.GetString("Message_Delete", resourceCulture);

	public static string Message_DownloadingBinary => ResourceManager.GetString("Message_DownloadingBinary", resourceCulture);

	public static string Message_DownloadingConfig => ResourceManager.GetString("Message_DownloadingConfig", resourceCulture);

	public static string Message_DownloadingModbus => ResourceManager.GetString("Message_DownloadingModbus", resourceCulture);

	public static string Message_DownloadingPLSBlock => ResourceManager.GetString("Message_DownloadingPLSBlock", resourceCulture);

	public static string Message_DownloadingPLSTable => ResourceManager.GetString("Message_DownloadingPLSTable", resourceCulture);

	public static string Message_DownloadingProject => ResourceManager.GetString("Message_DownloadingProject", resourceCulture);

	public static string Message_DownloadPasswordNotEnsurance => ResourceManager.GetString("Message_DownloadPasswordNotEnsurance", resourceCulture);

	public static string Message_DXF_HasBeenImported => ResourceManager.GetString("Message_DXF_HasBeenImported", resourceCulture);

	public static string Message_Element_Has_Added => ResourceManager.GetString("Message_Element_Has_Added", resourceCulture);

	public static string Message_EmptyError => ResourceManager.GetString("Message_EmptyError", resourceCulture);

	public static string Message_EnsureToMeasureExpansions => ResourceManager.GetString("Message_EnsureToMeasureExpansions", resourceCulture);

	public static string Message_ExpansionModuleTypeUnmatched => ResourceManager.GetString("Message_ExpansionModuleTypeUnmatched", resourceCulture);

	public static string Message_ExpansionModuleUnabled => ResourceManager.GetString("Message_ExpansionModuleUnabled", resourceCulture);

	public static string Message_ExpansionNumberUnmatched => ResourceManager.GetString("Message_ExpansionNumberUnmatched", resourceCulture);

	public static string Message_File_Exist => ResourceManager.GetString("Message_File_Exist", resourceCulture);

	public static string Message_File_Moved => ResourceManager.GetString("Message_File_Moved", resourceCulture);

	public static string Message_File_Name => ResourceManager.GetString("Message_File_Name", resourceCulture);

	public static string Message_File_Requried => ResourceManager.GetString("Message_File_Requried", resourceCulture);

	public static string Message_Fold_File_Exist => ResourceManager.GetString("Message_Fold_File_Exist", resourceCulture);

	public static string Message_FOR => ResourceManager.GetString("Message_FOR", resourceCulture);

	public static string Message_FOR_Not_Found => ResourceManager.GetString("Message_FOR_Not_Found", resourceCulture);

	public static string Message_Func_Name_Required => ResourceManager.GetString("Message_Func_Name_Required", resourceCulture);

	public static string Message_Func_Not_Found => ResourceManager.GetString("Message_Func_Not_Found", resourceCulture);

	public static string Message_Func_Params_Num_Error => ResourceManager.GetString("Message_Func_Params_Num_Error", resourceCulture);

	public static string Message_Funcblock_Exist => ResourceManager.GetString("Message_Funcblock_Exist", resourceCulture);

	public static string Message_FusionError => ResourceManager.GetString("Message_FusionError", resourceCulture);

	public static string Message_HardFaultEmpty => ResourceManager.GetString("Message_HardFaultEmpty", resourceCulture);

	public static string Message_HardFaultNumber => ResourceManager.GetString("Message_HardFaultNumber", resourceCulture);

	public static string Message_Has_Been_Used => ResourceManager.GetString("Message_Has_Been_Used", resourceCulture);

	public static string Message_illegal_Char => ResourceManager.GetString("Message_illegal_Char", resourceCulture);

	public static string Message_Input_Empty => ResourceManager.GetString("Message_Input_Empty", resourceCulture);

	public static string Message_Input_Params_Num_Error => ResourceManager.GetString("Message_Input_Params_Num_Error", resourceCulture);

	public static string Message_InstAddInvalid => ResourceManager.GetString("Message_InstAddInvalid", resourceCulture);

	public static string Message_Instruction_Not_Exist => ResourceManager.GetString("Message_Instruction_Not_Exist", resourceCulture);

	public static string Message_Interrupt_1 => ResourceManager.GetString("Message_Interrupt_1", resourceCulture);

	public static string Message_Interrupt_2 => ResourceManager.GetString("Message_Interrupt_2", resourceCulture);

	public static string Message_Interrupt_3 => ResourceManager.GetString("Message_Interrupt_3", resourceCulture);

	public static string Message_Interrupt_4 => ResourceManager.GetString("Message_Interrupt_4", resourceCulture);

	public static string Message_Interrupt_5 => ResourceManager.GetString("Message_Interrupt_5", resourceCulture);

	public static string Message_Intra_Coil_Error => ResourceManager.GetString("Message_Intra_Coil_Error", resourceCulture);

	public static string Message_Intra_Error => ResourceManager.GetString("Message_Intra_Error", resourceCulture);

	public static string Message_Invalid_Function_Name => ResourceManager.GetString("Message_Invalid_Function_Name", resourceCulture);

	public static string Message_Is_Override => ResourceManager.GetString("Message_Is_Override", resourceCulture);

	public static string Message_Jump_Not_Found => ResourceManager.GetString("Message_Jump_Not_Found", resourceCulture);

	public static string Message_Lang => ResourceManager.GetString("Message_Lang", resourceCulture);

	public static string Message_LaterVersion => ResourceManager.GetString("Message_LaterVersion", resourceCulture);

	public static string Message_LBL => ResourceManager.GetString("Message_LBL", resourceCulture);

	public static string Message_LBL_Error => ResourceManager.GetString("Message_LBL_Error", resourceCulture);

	public static string Message_LBL_Warning => ResourceManager.GetString("Message_LBL_Warning", resourceCulture);

	public static string Message_LocalBit_Multiply => ResourceManager.GetString("Message_LocalBit_Multiply", resourceCulture);

	public static string Message_LowerVersion => ResourceManager.GetString("Message_LowerVersion", resourceCulture);

	public static string Message_LowerVersion_NoInfluence => ResourceManager.GetString("Message_LowerVersion_NoInfluence", resourceCulture);

	public static string Message_MainDiagramIsEmpty => ResourceManager.GetString("Message_MainDiagramIsEmpty", resourceCulture);

	public static string Message_Masked => ResourceManager.GetString("Message_Masked", resourceCulture);

	public static string Message_MC => ResourceManager.GetString("Message_MC", resourceCulture);

	public static string Message_MC_NotFound => ResourceManager.GetString("Message_MC_NotFound", resourceCulture);

	public static string Message_MCR_NotFound => ResourceManager.GetString("Message_MCR_NotFound", resourceCulture);

	public static string Message_Missing_Parameters => ResourceManager.GetString("Message_Missing_Parameters", resourceCulture);

	public static string Message_Modbus_Name_Requied => ResourceManager.GetString("Message_Modbus_Name_Requied", resourceCulture);

	public static string Message_Modbus_Table => ResourceManager.GetString("Message_Modbus_Table", resourceCulture);

	public static string Message_Modbus_Table_Error => ResourceManager.GetString("Message_Modbus_Table_Error", resourceCulture);

	public static string Message_MonitorPasswordNotEnsurance => ResourceManager.GetString("Message_MonitorPasswordNotEnsurance", resourceCulture);

	public static string Message_Multi_Coil => ResourceManager.GetString("Message_Multi_Coil", resourceCulture);

	public static string Message_Name_Format_illegal => ResourceManager.GetString("Message_Name_Format_illegal", resourceCulture);

	public static string Message_Name_Needed => ResourceManager.GetString("Message_Name_Needed", resourceCulture);

	public static string Message_NetDownloadSwitchStop => ResourceManager.GetString("Message_NetDownloadSwitchStop", resourceCulture);

	public static string Message_Network_Range_Error => ResourceManager.GetString("Message_Network_Range_Error", resourceCulture);

	public static string Message_NEXT => ResourceManager.GetString("Message_NEXT", resourceCulture);

	public static string Message_NEXT_Not_Found => ResourceManager.GetString("Message_NEXT_Not_Found", resourceCulture);

	public static string Message_Not_DexNumber => ResourceManager.GetString("Message_Not_DexNumber", resourceCulture);

	public static string Message_NotSupportedVersion => ResourceManager.GetString("Message_NotSupportedVersion", resourceCulture);

	public static string Message_Number_Error => ResourceManager.GetString("Message_Number_Error", resourceCulture);

	public static string Message_OldPasswordNotCurrect => ResourceManager.GetString("Message_OldPasswordNotCurrect", resourceCulture);

	public static string Message_OpenError => ResourceManager.GetString("Message_OpenError", resourceCulture);

	public static string Message_OutOfXRange => ResourceManager.GetString("Message_OutOfXRange", resourceCulture);

	public static string Message_OutOfYRange => ResourceManager.GetString("Message_OutOfYRange", resourceCulture);

	public static string Message_Over_Max_Len => ResourceManager.GetString("Message_Over_Max_Len", resourceCulture);

	public static string Message_Path => ResourceManager.GetString("Message_Path", resourceCulture);

	public static string Message_PLC_OldVersion => ResourceManager.GetString("Message_PLC_OldVersion", resourceCulture);

	public static string Message_PLCStopToDownload => ResourceManager.GetString("Message_PLCStopToDownload", resourceCulture);

	public static string Message_PLSBlock_Not_Found => ResourceManager.GetString("Message_PLSBlock_Not_Found", resourceCulture);

	public static string Message_PowerDownDetectTimeMore100Warning => ResourceManager.GetString("Message_PowerDownDetectTimeMore100Warning", resourceCulture);

	public static string Message_Project_Error => ResourceManager.GetString("Message_Project_Error", resourceCulture);

	public static string Message_Project_Loaded => ResourceManager.GetString("Message_Project_Loaded", resourceCulture);

	public static string Message_ProjectPasswordNotEnsurance => ResourceManager.GetString("Message_ProjectPasswordNotEnsurance", resourceCulture);

	public static string Message_Renamed_Error => ResourceManager.GetString("Message_Renamed_Error", resourceCulture);

	public static string Message_RET => ResourceManager.GetString("Message_RET", resourceCulture);

	public static string Message_RouteInstLeft => ResourceManager.GetString("Message_RouteInstLeft", resourceCulture);

	public static string Message_ShortError => ResourceManager.GetString("Message_ShortError", resourceCulture);

	public static string Message_ST => ResourceManager.GetString("Message_ST", resourceCulture);

	public static string Message_Stack => ResourceManager.GetString("Message_Stack", resourceCulture);

	public static string Message_STL => ResourceManager.GetString("Message_STL", resourceCulture);

	public static string Message_STL_NotMatch => ResourceManager.GetString("Message_STL_NotMatch", resourceCulture);

	public static string Message_STL_OnlyOne => ResourceManager.GetString("Message_STL_OnlyOne", resourceCulture);

	public static string Message_STL_Over => ResourceManager.GetString("Message_STL_Over", resourceCulture);

	public static string Message_STLE => ResourceManager.GetString("Message_STLE", resourceCulture);

	public static string Message_STLE_OnlyOne => ResourceManager.GetString("Message_STLE_OnlyOne", resourceCulture);

	public static string Message_STLE_Over => ResourceManager.GetString("Message_STLE_Over", resourceCulture);

	public static string Message_Subroutine_Exist => ResourceManager.GetString("Message_Subroutine_Exist", resourceCulture);

	public static string Message_Subroutine_Name_Required => ResourceManager.GetString("Message_Subroutine_Name_Required", resourceCulture);

	public static string Message_SubRoutine_Not_Found => ResourceManager.GetString("Message_SubRoutine_Not_Found", resourceCulture);

	public static string Message_SwitchStatusFailed => ResourceManager.GetString("Message_SwitchStatusFailed", resourceCulture);

	public static string Message_SwitchStatusSuccess => ResourceManager.GetString("Message_SwitchStatusSuccess", resourceCulture);

	public static string Message_Table_Exist => ResourceManager.GetString("Message_Table_Exist", resourceCulture);

	public static string Message_Table_Name_Exist => ResourceManager.GetString("Message_Table_Name_Exist", resourceCulture);

	public static string Message_Text => ResourceManager.GetString("Message_Text", resourceCulture);

	public static string Message_Tooltip => ResourceManager.GetString("Message_Tooltip", resourceCulture);

	public static string Message_UnknownError => ResourceManager.GetString("Message_UnknownError", resourceCulture);

	public static string Message_UnsupportedInstruction => ResourceManager.GetString("Message_UnsupportedInstruction", resourceCulture);

	public static string Message_Upload_Empty => ResourceManager.GetString("Message_Upload_Empty", resourceCulture);

	public static string Message_UploadPasswordNotEnsurance => ResourceManager.GetString("Message_UploadPasswordNotEnsurance", resourceCulture);

	public static string Message_Used_Error => ResourceManager.GetString("Message_Used_Error", resourceCulture);

	public static string Message_Word_Requried => ResourceManager.GetString("Message_Word_Requried", resourceCulture);

	public static string Message_WriteRealTimeFailed => ResourceManager.GetString("Message_WriteRealTimeFailed", resourceCulture);

	public static string Message_WriteRealTimeSuccess => ResourceManager.GetString("Message_WriteRealTimeSuccess", resourceCulture);

	public static string MessageBox_BranchNoChildren => ResourceManager.GetString("MessageBox_BranchNoChildren", resourceCulture);

	public static string MessageBox_Communication_Failed => ResourceManager.GetString("MessageBox_Communication_Failed", resourceCulture);

	public static string MessageBox_Communication_Success => ResourceManager.GetString("MessageBox_Communication_Success", resourceCulture);

	public static string MessageBox_Download_Successd => ResourceManager.GetString("MessageBox_Download_Successd", resourceCulture);

	public static string MessageBox_MergeNoChildren => ResourceManager.GetString("MessageBox_MergeNoChildren", resourceCulture);

	public static string MessageBox_NetworkLimit => ResourceManager.GetString("MessageBox_NetworkLimit", resourceCulture);

	public static string MessageBox_RowLimit => ResourceManager.GetString("MessageBox_RowLimit", resourceCulture);

	public static string MessageBox_SelectRoutineGenerateSource => ResourceManager.GetString("MessageBox_SelectRoutineGenerateSource", resourceCulture);

	public static string MessageBox_SFCCheckResult => ResourceManager.GetString("MessageBox_SFCCheckResult", resourceCulture);

	public static string MessageBox_SFCCheckSuccessful => ResourceManager.GetString("MessageBox_SFCCheckSuccessful", resourceCulture);

	public static string MessageBox_SFCColumnOverflow => ResourceManager.GetString("MessageBox_SFCColumnOverflow", resourceCulture);

	public static string MessageBox_SFCCompileSuccessful => ResourceManager.GetString("MessageBox_SFCCompileSuccessful", resourceCulture);

	public static string MessageBox_SFCIncreasedOrder => ResourceManager.GetString("MessageBox_SFCIncreasedOrder", resourceCulture);

	public static string MessageBox_Upload_Successd => ResourceManager.GetString("MessageBox_Upload_Successd", resourceCulture);

	public static string Minimize => ResourceManager.GetString("Minimize", resourceCulture);

	public static string Minute => ResourceManager.GetString("Minute", resourceCulture);

	public static string ModBus_Changed => ResourceManager.GetString("ModBus_Changed", resourceCulture);

	public static string Modbus_Comment => ResourceManager.GetString("Modbus_Comment", resourceCulture);

	public static string Modbus_Create_New_Table => ResourceManager.GetString("Modbus_Create_New_Table", resourceCulture);

	public static string Modbus_Current_Row_Bottom => ResourceManager.GetString("Modbus_Current_Row_Bottom", resourceCulture);

	public static string Modbus_Current_Row_Down => ResourceManager.GetString("Modbus_Current_Row_Down", resourceCulture);

	public static string Modbus_Current_Row_Top => ResourceManager.GetString("Modbus_Current_Row_Top", resourceCulture);

	public static string Modbus_Current_Row_Up => ResourceManager.GetString("Modbus_Current_Row_Up", resourceCulture);

	public static string Modbus_Current_Table_Bottom => ResourceManager.GetString("Modbus_Current_Table_Bottom", resourceCulture);

	public static string Modbus_Current_Table_Down => ResourceManager.GetString("Modbus_Current_Table_Down", resourceCulture);

	public static string Modbus_Current_Table_Top => ResourceManager.GetString("Modbus_Current_Table_Top", resourceCulture);

	public static string Modbus_Current_Table_Up => ResourceManager.GetString("Modbus_Current_Table_Up", resourceCulture);

	public static string Modbus_Delete_Table => ResourceManager.GetString("Modbus_Delete_Table", resourceCulture);

	public static string Modbus_Function_Code => ResourceManager.GetString("Modbus_Function_Code", resourceCulture);

	public static string Modbus_Item_ID => ResourceManager.GetString("Modbus_Item_ID", resourceCulture);

	public static string Modbus_Master_Register => ResourceManager.GetString("Modbus_Master_Register", resourceCulture);

	public static string Modbus_Master_Station => ResourceManager.GetString("Modbus_Master_Station", resourceCulture);

	public static string Modbus_ReadCoil => ResourceManager.GetString("Modbus_ReadCoil", resourceCulture);

	public static string Modbus_ReadDisperse => ResourceManager.GetString("Modbus_ReadDisperse", resourceCulture);

	public static string Modbus_ReadHold => ResourceManager.GetString("Modbus_ReadHold", resourceCulture);

	public static string Modbus_ReadInput => ResourceManager.GetString("Modbus_ReadInput", resourceCulture);

	public static string Modbus_Slave_Length => ResourceManager.GetString("Modbus_Slave_Length", resourceCulture);

	public static string Modbus_Slave_Register => ResourceManager.GetString("Modbus_Slave_Register", resourceCulture);

	public static string Modbus_Slave_Station_Number => ResourceManager.GetString("Modbus_Slave_Station_Number", resourceCulture);

	public static string Modbus_Slaver_Station => ResourceManager.GetString("Modbus_Slaver_Station", resourceCulture);

	public static string Modbus_Table => ResourceManager.GetString("Modbus_Table", resourceCulture);

	public static string Modbus_Table_Comment => ResourceManager.GetString("Modbus_Table_Comment", resourceCulture);

	public static string Modbus_Table_Name => ResourceManager.GetString("Modbus_Table_Name", resourceCulture);

	public static string Modbus_Table_Rename => ResourceManager.GetString("Modbus_Table_Rename", resourceCulture);

	public static string Modbus_TCP_IP_Maste_Station => ResourceManager.GetString("Modbus_TCP_IP_Maste_Station", resourceCulture);

	public static string Modbus_TCP_IP_Slave_Station => ResourceManager.GetString("Modbus_TCP_IP_Slave_Station", resourceCulture);

	public static string Modbus_WriteMultipleCoil => ResourceManager.GetString("Modbus_WriteMultipleCoil", resourceCulture);

	public static string Modbus_WriteMultipleValue => ResourceManager.GetString("Modbus_WriteMultipleValue", resourceCulture);

	public static string Modbus_WriteSingleCoil => ResourceManager.GetString("Modbus_WriteSingleCoil", resourceCulture);

	public static string Modbus_WriteSingleValue => ResourceManager.GetString("Modbus_WriteSingleValue", resourceCulture);

	public static string ModbusFrameInterval => ResourceManager.GetString("ModbusFrameInterval", resourceCulture);

	public static string ModbusTable_Buffer_Error => ResourceManager.GetString("ModbusTable_Buffer_Error", resourceCulture);

	public static string Mode => ResourceManager.GetString("Mode", resourceCulture);

	public static string ModeIndexRegister => ResourceManager.GetString("ModeIndexRegister", resourceCulture);

	public static string Modify => ResourceManager.GetString("Modify", resourceCulture);

	public static string Modify_HandleCode => ResourceManager.GetString("Modify_HandleCode", resourceCulture);

	public static string Modify_MasteRegister => ResourceManager.GetString("Modify_MasteRegister", resourceCulture);

	public static string Modify_Project => ResourceManager.GetString("Modify_Project", resourceCulture);

	public static string Modify_SlaveLength => ResourceManager.GetString("Modify_SlaveLength", resourceCulture);

	public static string Modify_SlaveNumber => ResourceManager.GetString("Modify_SlaveNumber", resourceCulture);

	public static string Modify_SlaveRegister => ResourceManager.GetString("Modify_SlaveRegister", resourceCulture);

	public static string Module_BarCode => ResourceManager.GetString("Module_BarCode", resourceCulture);

	public static string Module_Check => ResourceManager.GetString("Module_Check", resourceCulture);

	public static string Module_Current => ResourceManager.GetString("Module_Current", resourceCulture);

	public static string Module_Eight => ResourceManager.GetString("Module_Eight", resourceCulture);

	public static string Module_Five => ResourceManager.GetString("Module_Five", resourceCulture);

	public static string Module_Four => ResourceManager.GetString("Module_Four", resourceCulture);

	public static string Module_ID => ResourceManager.GetString("Module_ID", resourceCulture);

	public static string Module_Message => ResourceManager.GetString("Module_Message", resourceCulture);

	public static string Module_Number => ResourceManager.GetString("Module_Number", resourceCulture);

	public static string Module_One => ResourceManager.GetString("Module_One", resourceCulture);

	public static string Module_Seven => ResourceManager.GetString("Module_Seven", resourceCulture);

	public static string Module_Six => ResourceManager.GetString("Module_Six", resourceCulture);

	public static string Module_Three => ResourceManager.GetString("Module_Three", resourceCulture);

	public static string Module_Two => ResourceManager.GetString("Module_Two", resourceCulture);

	public static string Module_Type => ResourceManager.GetString("Module_Type", resourceCulture);

	public static string Monitor => ResourceManager.GetString("Monitor", resourceCulture);

	public static string Monitor_Changed => ResourceManager.GetString("Monitor_Changed", resourceCulture);

	public static string Monitor_Closing => ResourceManager.GetString("Monitor_Closing", resourceCulture);

	public static string Monitor_Encryption => ResourceManager.GetString("Monitor_Encryption", resourceCulture);

	public static string Monitor_LadderNotMatch => ResourceManager.GetString("Monitor_LadderNotMatch", resourceCulture);

	public static string Monitor_List => ResourceManager.GetString("Monitor_List", resourceCulture);

	public static string Monitor_Start => ResourceManager.GetString("Monitor_Start", resourceCulture);

	public static string Monitor_Stop => ResourceManager.GetString("Monitor_Stop", resourceCulture);

	public static string MonitorMenu_Cancel => ResourceManager.GetString("MonitorMenu_Cancel", resourceCulture);

	public static string MonitorMenu_CancelAll => ResourceManager.GetString("MonitorMenu_CancelAll", resourceCulture);

	public static string MonitorMenu_ForceOFF => ResourceManager.GetString("MonitorMenu_ForceOFF", resourceCulture);

	public static string MonitorMenu_ForceON => ResourceManager.GetString("MonitorMenu_ForceON", resourceCulture);

	public static string MonitorMenu_SetAll => ResourceManager.GetString("MonitorMenu_SetAll", resourceCulture);

	public static string MonitorMenu_SetOFF => ResourceManager.GetString("MonitorMenu_SetOFF", resourceCulture);

	public static string MonitorMenu_SetON => ResourceManager.GetString("MonitorMenu_SetON", resourceCulture);

	public static string MonitorMenu_SetValue => ResourceManager.GetString("MonitorMenu_SetValue", resourceCulture);

	public static string MonitorPassword_Accept => ResourceManager.GetString("MonitorPassword_Accept", resourceCulture);

	public static string MonitorPassword_Error => ResourceManager.GetString("MonitorPassword_Error", resourceCulture);

	public static string MonitorPassword_Message => ResourceManager.GetString("MonitorPassword_Message", resourceCulture);

	public static string MonitorPassword_Title => ResourceManager.GetString("MonitorPassword_Title", resourceCulture);

	public static string MonitorPasswordNotion => ResourceManager.GetString("MonitorPasswordNotion", resourceCulture);

	public static string Move_Blocks_DWord => ResourceManager.GetString("Move_Blocks_DWord", resourceCulture);

	public static string Move_Blocks_Word => ResourceManager.GetString("Move_Blocks_Word", resourceCulture);

	public static string Move_DWord => ResourceManager.GetString("Move_DWord", resourceCulture);

	public static string Move_Float => ResourceManager.GetString("Move_Float", resourceCulture);

	public static string Move_Word => ResourceManager.GetString("Move_Word", resourceCulture);

	public static string MoveDown => ResourceManager.GetString("MoveDown", resourceCulture);

	public static string MoveUp => ResourceManager.GetString("MoveUp", resourceCulture);

	public static string MRegisterRange => ResourceManager.GetString("MRegisterRange", resourceCulture);

	public static string Multiply => ResourceManager.GetString("Multiply", resourceCulture);

	public static string NagitiveLimitation => ResourceManager.GetString("NagitiveLimitation", resourceCulture);

	public static string Name => ResourceManager.GetString("Name", resourceCulture);

	public static string NEG_Inst => ResourceManager.GetString("NEG_Inst", resourceCulture);

	public static string NEGD_Inst => ResourceManager.GetString("NEGD_Inst", resourceCulture);

	public static string NEGF_Inst => ResourceManager.GetString("NEGF_Inst", resourceCulture);

	public static string Net_Port_Params_Error => ResourceManager.GetString("Net_Port_Params_Error", resourceCulture);

	public static string Net_Setting => ResourceManager.GetString("Net_Setting", resourceCulture);

	public static string NetProtocol => ResourceManager.GetString("NetProtocol", resourceCulture);

	public static string Network => ResourceManager.GetString("Network", resourceCulture);

	public static string Network_Error => ResourceManager.GetString("Network_Error", resourceCulture);

	public static string Network_Number => ResourceManager.GetString("Network_Number", resourceCulture);

	public static string Network_Number_Range => ResourceManager.GetString("Network_Number_Range", resourceCulture);

	public static string Network_Range => ResourceManager.GetString("Network_Range", resourceCulture);

	public static string Network_Structure_Error => ResourceManager.GetString("Network_Structure_Error", resourceCulture);

	public static string NetworkInsertAfter => ResourceManager.GetString("NetworkInsertAfter", resourceCulture);

	public static string NetworkInsertBefore => ResourceManager.GetString("NetworkInsertBefore", resourceCulture);

	public static string NetworkInsertEnd => ResourceManager.GetString("NetworkInsertEnd", resourceCulture);

	public static string New_Folder => ResourceManager.GetString("New_Folder", resourceCulture);

	public static string New_FuncBlock => ResourceManager.GetString("New_FuncBlock", resourceCulture);

	public static string New_Funcblock_Name => ResourceManager.GetString("New_Funcblock_Name", resourceCulture);

	public static string New_Function_Name => ResourceManager.GetString("New_Function_Name", resourceCulture);

	public static string New_Ladder_Func => ResourceManager.GetString("New_Ladder_Func", resourceCulture);

	public static string New_Proj => ResourceManager.GetString("New_Proj", resourceCulture);

	public static string New_Project => ResourceManager.GetString("New_Project", resourceCulture);

	public static string New_Project_Name => ResourceManager.GetString("New_Project_Name", resourceCulture);

	public static string New_Routine => ResourceManager.GetString("New_Routine", resourceCulture);

	public static string New_Version => ResourceManager.GetString("New_Version", resourceCulture);

	public static string NewPassword => ResourceManager.GetString("NewPassword", resourceCulture);

	public static string NewProjectDialog_BaseSetting => ResourceManager.GetString("NewProjectDialog_BaseSetting", resourceCulture);

	public static string NewProjectDialog_FileSetting => ResourceManager.GetString("NewProjectDialog_FileSetting", resourceCulture);

	public static string NEXT_Inst => ResourceManager.GetString("NEXT_Inst", resourceCulture);

	public static string NextLine_CannotDeleteBase => ResourceManager.GetString("NextLine_CannotDeleteBase", resourceCulture);

	public static string NextLine_DeleteThis => ResourceManager.GetString("NextLine_DeleteThis", resourceCulture);

	public static string NextLine_IsAccess => ResourceManager.GetString("NextLine_IsAccess", resourceCulture);

	public static string NextLine_Title => ResourceManager.GetString("NextLine_Title", resourceCulture);

	public static string NO => ResourceManager.GetString("NO", resourceCulture);

	public static string No_Information => ResourceManager.GetString("No_Information", resourceCulture);

	public static string None => ResourceManager.GetString("None", resourceCulture);

	public static string NONE_ERROR => ResourceManager.GetString("NONE_ERROR", resourceCulture);

	public static string Not_Clear_While_Download => ResourceManager.GetString("Not_Clear_While_Download", resourceCulture);

	public static string Not_Command => ResourceManager.GetString("Not_Command", resourceCulture);

	public static string Not_Compiled => ResourceManager.GetString("Not_Compiled", resourceCulture);

	public static string Not_Enabled => ResourceManager.GetString("Not_Enabled", resourceCulture);

	public static string NotClearHoldingOnDownload => ResourceManager.GetString("NotClearHoldingOnDownload", resourceCulture);

	public static string Number_Lock => ResourceManager.GetString("Number_Lock", resourceCulture);

	public static string Number_Of_Additions => ResourceManager.GetString("Number_Of_Additions", resourceCulture);

	public static string Number_Of_Pulse_Segements => ResourceManager.GetString("Number_Of_Pulse_Segements", resourceCulture);

	public static string Number_Of_Retransmission => ResourceManager.GetString("Number_Of_Retransmission", resourceCulture);

	public static string Offset_Element_Select => ResourceManager.GetString("Offset_Element_Select", resourceCulture);

	public static string OldPassword => ResourceManager.GetString("OldPassword", resourceCulture);

	public static string ON_OFF_CurrentValue => ResourceManager.GetString("ON_OFF_CurrentValue", resourceCulture);

	public static string On_Off_Value => ResourceManager.GetString("On_Off_Value", resourceCulture);

	public static string OnlyControl => ResourceManager.GetString("OnlyControl", resourceCulture);

	public static string Open_Error => ResourceManager.GetString("Open_Error", resourceCulture);

	public static string Open_Proj => ResourceManager.GetString("Open_Proj", resourceCulture);

	public static string OpenNotion_AcceptClause => ResourceManager.GetString("OpenNotion_AcceptClause", resourceCulture);

	public static string OpenNotion_Clause => ResourceManager.GetString("OpenNotion_Clause", resourceCulture);

	public static string OpenNotion_Continue => ResourceManager.GetString("OpenNotion_Continue", resourceCulture);

	public static string OpenNotion_FuncNameExist => ResourceManager.GetString("OpenNotion_FuncNameExist", resourceCulture);

	public static string OpenNotion_FuncNameInfliction => ResourceManager.GetString("OpenNotion_FuncNameInfliction", resourceCulture);

	public static string OpenNotion_HasClause => ResourceManager.GetString("OpenNotion_HasClause", resourceCulture);

	public static string OpenNotion_LaterVersion => ResourceManager.GetString("OpenNotion_LaterVersion", resourceCulture);

	public static string OpenNotion_LowerVersion => ResourceManager.GetString("OpenNotion_LowerVersion", resourceCulture);

	public static string OpenNotion_MoreInfo => ResourceManager.GetString("OpenNotion_MoreInfo", resourceCulture);

	public static string OpenNotion_OpenLaterVersion1 => ResourceManager.GetString("OpenNotion_OpenLaterVersion1", resourceCulture);

	public static string OpenNotion_OpenLowerVersion => ResourceManager.GetString("OpenNotion_OpenLowerVersion", resourceCulture);

	public static string OpenNotion_Password => ResourceManager.GetString("OpenNotion_Password", resourceCulture);

	public static string OpenNotion_PasswordAccepted => ResourceManager.GetString("OpenNotion_PasswordAccepted", resourceCulture);

	public static string OpenNotion_PasswordAssert => ResourceManager.GetString("OpenNotion_PasswordAssert", resourceCulture);

	public static string OpenNotion_PasswordInput => ResourceManager.GetString("OpenNotion_PasswordInput", resourceCulture);

	public static string OpenNotion_PasswordShow => ResourceManager.GetString("OpenNotion_PasswordShow", resourceCulture);

	public static string OpenNotion_RemoveLibrary => ResourceManager.GetString("OpenNotion_RemoveLibrary", resourceCulture);

	public static string OpenNotion_Title => ResourceManager.GetString("OpenNotion_Title", resourceCulture);

	public static string Operate => ResourceManager.GetString("Operate", resourceCulture);

	public static string OptimizeCommentSpace => ResourceManager.GetString("OptimizeCommentSpace", resourceCulture);

	public static string Option => ResourceManager.GetString("Option", resourceCulture);

	public static string Other => ResourceManager.GetString("Other", resourceCulture);

	public static string Output => ResourceManager.GetString("Output", resourceCulture);

	public static string Overflow_DecelerateStop => ResourceManager.GetString("Overflow_DecelerateStop", resourceCulture);

	public static string Overflow_NotProcess => ResourceManager.GetString("Overflow_NotProcess", resourceCulture);

	public static string OverflowProcess => ResourceManager.GetString("OverflowProcess", resourceCulture);

	public static string PageSetting => ResourceManager.GetString("PageSetting", resourceCulture);

	public static string Parameter_Description => ResourceManager.GetString("Parameter_Description", resourceCulture);

	public static string Parameter_Error => ResourceManager.GetString("Parameter_Error", resourceCulture);

	public static string Params_Func => ResourceManager.GetString("Params_Func", resourceCulture);

	public static string Parse_Error_By_Wrong_Separator => ResourceManager.GetString("Parse_Error_By_Wrong_Separator", resourceCulture);

	public static string Password => ResourceManager.GetString("Password", resourceCulture);

	public static string Password_Empty => ResourceManager.GetString("Password_Empty", resourceCulture);

	public static string Password_Ensurance => ResourceManager.GetString("Password_Ensurance", resourceCulture);

	public static string Password_Error => ResourceManager.GetString("Password_Error", resourceCulture);

	public static string Password_IsEnable => ResourceManager.GetString("Password_IsEnable", resourceCulture);

	public static string Password_Length_Error => ResourceManager.GetString("Password_Length_Error", resourceCulture);

	public static string Password_Message => ResourceManager.GetString("Password_Message", resourceCulture);

	public static string Password_Protected_Success => ResourceManager.GetString("Password_Protected_Success", resourceCulture);

	public static string Paste => ResourceManager.GetString("Paste", resourceCulture);

	public static string Path => ResourceManager.GetString("Path", resourceCulture);

	public static string PAUSE_Inst => ResourceManager.GetString("PAUSE_Inst", resourceCulture);

	public static string PID_Control_Dead_Zone => ResourceManager.GetString("PID_Control_Dead_Zone", resourceCulture);

	public static string PID_Inst => ResourceManager.GetString("PID_Inst", resourceCulture);

	public static string PID_Output => ResourceManager.GetString("PID_Output", resourceCulture);

	public static string PID_ProcessVariable => ResourceManager.GetString("PID_ProcessVariable", resourceCulture);

	public static string PIDLoop => ResourceManager.GetString("PIDLoop", resourceCulture);

	public static string PIDMode_Handle => ResourceManager.GetString("PIDMode_Handle", resourceCulture);

	public static string PlatSys => ResourceManager.GetString("PlatSys", resourceCulture);

	public static string PlatSys1 => ResourceManager.GetString("PlatSys1", resourceCulture);

	public static string PlatSys2 => ResourceManager.GetString("PlatSys2", resourceCulture);

	public static string PlatSys3 => ResourceManager.GetString("PlatSys3", resourceCulture);

	public static string PlatSys4 => ResourceManager.GetString("PlatSys4", resourceCulture);

	public static string PlatSys5 => ResourceManager.GetString("PlatSys5", resourceCulture);

	public static string PlatSysSelect => ResourceManager.GetString("PlatSysSelect", resourceCulture);

	public static string PLC_Analog_Quantity => ResourceManager.GetString("PLC_Analog_Quantity", resourceCulture);

	public static string PLC_Communication_Parameters => ResourceManager.GetString("PLC_Communication_Parameters", resourceCulture);

	public static string PLC_Device_Model => ResourceManager.GetString("PLC_Device_Model", resourceCulture);

	public static string PLC_Expansion_Module => ResourceManager.GetString("PLC_Expansion_Module", resourceCulture);

	public static string PLC_Filter_Parameters => ResourceManager.GetString("PLC_Filter_Parameters", resourceCulture);

	public static string PLC_Iap_Update => ResourceManager.GetString("PLC_Iap_Update", resourceCulture);

	public static string PLC_LadderType => ResourceManager.GetString("PLC_LadderType", resourceCulture);

	public static string PLC_Password_Setting => ResourceManager.GetString("PLC_Password_Setting", resourceCulture);

	public static string PLC_Reset => ResourceManager.GetString("PLC_Reset", resourceCulture);

	public static string PLC_Reset_Message => ResourceManager.GetString("PLC_Reset_Message", resourceCulture);

	public static string PLC_Reset_Success => ResourceManager.GetString("PLC_Reset_Success", resourceCulture);

	public static string PLC_Reseting => ResourceManager.GetString("PLC_Reseting", resourceCulture);

	public static string PLC_Retentive_Zone_Settings => ResourceManager.GetString("PLC_Retentive_Zone_Settings", resourceCulture);

	public static string PLC_Series => ResourceManager.GetString("PLC_Series", resourceCulture);

	public static string PLC_Status => ResourceManager.GetString("PLC_Status", resourceCulture);

	public static string PLC_Status_Change_Error => ResourceManager.GetString("PLC_Status_Change_Error", resourceCulture);

	public static string PLC_Status_To_Run => ResourceManager.GetString("PLC_Status_To_Run", resourceCulture);

	public static string PLC_Status_To_Stop => ResourceManager.GetString("PLC_Status_To_Stop", resourceCulture);

	public static string PLC_Type => ResourceManager.GetString("PLC_Type", resourceCulture);

	public static string PLC_Type_Not_Equal => ResourceManager.GetString("PLC_Type_Not_Equal", resourceCulture);

	public static string Please_Select_DownData => ResourceManager.GetString("Please_Select_DownData", resourceCulture);

	public static string Please_Select_Range => ResourceManager.GetString("Please_Select_Range", resourceCulture);

	public static string Please_Select_UpData => ResourceManager.GetString("Please_Select_UpData", resourceCulture);

	public static string PLS_ACCTIME_ERROR => ResourceManager.GetString("PLS_ACCTIME_ERROR", resourceCulture);

	public static string PLS_Block_Buffer_Error => ResourceManager.GetString("PLS_Block_Buffer_Error", resourceCulture);

	public static string PLS_FIRST_PART_FREQ_ERROR => ResourceManager.GetString("PLS_FIRST_PART_FREQ_ERROR", resourceCulture);

	public static string PLS_FREQ_LOW_ERROR => ResourceManager.GetString("PLS_FREQ_LOW_ERROR", resourceCulture);

	public static string PLS_ILLEGAL_ACC_ERROR => ResourceManager.GetString("PLS_ILLEGAL_ACC_ERROR", resourceCulture);

	public static string PLS_LAST_PART_ERROR => ResourceManager.GetString("PLS_LAST_PART_ERROR", resourceCulture);

	public static string PLS_PART_NUM_ERROR => ResourceManager.GetString("PLS_PART_NUM_ERROR", resourceCulture);

	public static string PLS_PART_PLSNUM_ERROR => ResourceManager.GetString("PLS_PART_PLSNUM_ERROR", resourceCulture);

	public static string PLS_Table_Buffer_Error => ResourceManager.GetString("PLS_Table_Buffer_Error", resourceCulture);

	public static string PLSA_Inst => ResourceManager.GetString("PLSA_Inst", resourceCulture);

	public static string PLSBlockDialog_Title => ResourceManager.GetString("PLSBlockDialog_Title", resourceCulture);

	public static string PLSF_Inst => ResourceManager.GetString("PLSF_Inst", resourceCulture);

	public static string PLSNEXT_Inst => ResourceManager.GetString("PLSNEXT_Inst", resourceCulture);

	public static string PlsParams_AddrOverLimit => ResourceManager.GetString("PlsParams_AddrOverLimit", resourceCulture);

	public static string PLSR_Inst => ResourceManager.GetString("PLSR_Inst", resourceCulture);

	public static string PLSRD_Inst => ResourceManager.GetString("PLSRD_Inst", resourceCulture);

	public static string PLSSTOP_Inst => ResourceManager.GetString("PLSSTOP_Inst", resourceCulture);

	public static string PLSY_Inst => ResourceManager.GetString("PLSY_Inst", resourceCulture);

	public static string PoliceTree => ResourceManager.GetString("PoliceTree", resourceCulture);

	public static string POLY_ACTime => ResourceManager.GetString("POLY_ACTime", resourceCulture);

	public static string POLY_AddArch => ResourceManager.GetString("POLY_AddArch", resourceCulture);

	public static string POLY_AddLine => ResourceManager.GetString("POLY_AddLine", resourceCulture);

	public static string POLY_ArchRadius => ResourceManager.GetString("POLY_ArchRadius", resourceCulture);

	public static string POLY_ArchType => ResourceManager.GetString("POLY_ArchType", resourceCulture);

	public static string POLY_Circle => ResourceManager.GetString("POLY_Circle", resourceCulture);

	public static string POLY_ClkDir => ResourceManager.GetString("POLY_ClkDir", resourceCulture);

	public static string POLY_Clockwise => ResourceManager.GetString("POLY_Clockwise", resourceCulture);

	public static string POLY_DCTime => ResourceManager.GetString("POLY_DCTime", resourceCulture);

	public static string POLY_InfArch => ResourceManager.GetString("POLY_InfArch", resourceCulture);

	public static string POLY_InfrArch => ResourceManager.GetString("POLY_InfrArch", resourceCulture);

	public static string POLY_InsertArch => ResourceManager.GetString("POLY_InsertArch", resourceCulture);

	public static string POLY_InsertLine => ResourceManager.GetString("POLY_InsertLine", resourceCulture);

	public static string POLY_LocMode => ResourceManager.GetString("POLY_LocMode", resourceCulture);

	public static string POLY_NotRefTo => ResourceManager.GetString("POLY_NotRefTo", resourceCulture);

	public static string POLY_OnlyControl => ResourceManager.GetString("POLY_OnlyControl", resourceCulture);

	public static string POLY_OptArch => ResourceManager.GetString("POLY_OptArch", resourceCulture);

	public static string POLY_OptInf => ResourceManager.GetString("POLY_OptInf", resourceCulture);

	public static string POLY_RefAddr => ResourceManager.GetString("POLY_RefAddr", resourceCulture);

	public static string POLY_RefMode => ResourceManager.GetString("POLY_RefMode", resourceCulture);

	public static string POLY_RefTo => ResourceManager.GetString("POLY_RefTo", resourceCulture);

	public static string POLY_ThreePoint => ResourceManager.GetString("POLY_ThreePoint", resourceCulture);

	public static string POLY_TwoPoint => ResourceManager.GetString("POLY_TwoPoint", resourceCulture);

	public static string POLY_Unclockwise => ResourceManager.GetString("POLY_Unclockwise", resourceCulture);

	public static string POLY_Velocity => ResourceManager.GetString("POLY_Velocity", resourceCulture);

	public static string POLY_XCenter => ResourceManager.GetString("POLY_XCenter", resourceCulture);

	public static string POLY_XCircle => ResourceManager.GetString("POLY_XCircle", resourceCulture);

	public static string POLY_XEnd => ResourceManager.GetString("POLY_XEnd", resourceCulture);

	public static string POLY_YCenter => ResourceManager.GetString("POLY_YCenter", resourceCulture);

	public static string POLY_YCircle => ResourceManager.GetString("POLY_YCircle", resourceCulture);

	public static string POLY_YEnd => ResourceManager.GetString("POLY_YEnd", resourceCulture);

	public static string POLYLINEF_Inst => ResourceManager.GetString("POLYLINEF_Inst", resourceCulture);

	public static string POLYLINEI_Inst => ResourceManager.GetString("POLYLINEI_Inst", resourceCulture);

	public static string PolylineSystemSettingDialog_Title => ResourceManager.GetString("PolylineSystemSettingDialog_Title", resourceCulture);

	public static string Port_Number => ResourceManager.GetString("Port_Number", resourceCulture);

	public static string Port_Number_Error => ResourceManager.GetString("Port_Number_Error", resourceCulture);

	public static string Port_Number_Tooltip => ResourceManager.GetString("Port_Number_Tooltip", resourceCulture);

	public static string Position => ResourceManager.GetString("Position", resourceCulture);

	public static string PositiveLimitation => ResourceManager.GetString("PositiveLimitation", resourceCulture);

	public static string POW_Inst => ResourceManager.GetString("POW_Inst", resourceCulture);

	public static string Power_Down_Detect_Time => ResourceManager.GetString("Power_Down_Detect_Time", resourceCulture);

	public static string Power_off_Retention_Area_Setting => ResourceManager.GetString("Power_off_Retention_Area_Setting", resourceCulture);

	public static string PowerOffHolding => ResourceManager.GetString("PowerOffHolding", resourceCulture);

	public static string PreView => ResourceManager.GetString("PreView", resourceCulture);

	public static string Preview_Color => ResourceManager.GetString("Preview_Color", resourceCulture);

	public static string Print => ResourceManager.GetString("Print", resourceCulture);

	public static string PrintPreview => ResourceManager.GetString("PrintPreview", resourceCulture);

	public static string Program => ResourceManager.GetString("Program", resourceCulture);

	public static string Program_Correct => ResourceManager.GetString("Program_Correct", resourceCulture);

	public static string Program_Empty => ResourceManager.GetString("Program_Empty", resourceCulture);

	public static string Program_Password_Protected => ResourceManager.GetString("Program_Password_Protected", resourceCulture);

	public static string Project => ResourceManager.GetString("Project", resourceCulture);

	public static string Project_Applied => ResourceManager.GetString("Project_Applied", resourceCulture);

	public static string Project_Broken => ResourceManager.GetString("Project_Broken", resourceCulture);

	public static string Project_Buffer_Error => ResourceManager.GetString("Project_Buffer_Error", resourceCulture);

	public static string Project_Changed => ResourceManager.GetString("Project_Changed", resourceCulture);

	public static string Project_Config_Changed => ResourceManager.GetString("Project_Config_Changed", resourceCulture);

	public static string Project_Download => ResourceManager.GetString("Project_Download", resourceCulture);

	public static string Project_Explorer => ResourceManager.GetString("Project_Explorer", resourceCulture);

	public static string Project_File => ResourceManager.GetString("Project_File", resourceCulture);

	public static string Project_Load => ResourceManager.GetString("Project_Load", resourceCulture);

	public static string Project_Load_Failed => ResourceManager.GetString("Project_Load_Failed", resourceCulture);

	public static string Project_Load_Success => ResourceManager.GetString("Project_Load_Success", resourceCulture);

	public static string Project_Name => ResourceManager.GetString("Project_Name", resourceCulture);

	public static string Project_Preparing => ResourceManager.GetString("Project_Preparing", resourceCulture);

	public static string Project_Saved => ResourceManager.GetString("Project_Saved", resourceCulture);

	public static string Project_Setting => ResourceManager.GetString("Project_Setting", resourceCulture);

	public static string Project_UnSaved => ResourceManager.GetString("Project_UnSaved", resourceCulture);

	public static string Project_Upload => ResourceManager.GetString("Project_Upload", resourceCulture);

	public static string ProjectCompare => ResourceManager.GetString("ProjectCompare", resourceCulture);

	public static string ProjectCompare_Range1 => ResourceManager.GetString("ProjectCompare_Range1", resourceCulture);

	public static string ProjectCompare_Range2 => ResourceManager.GetString("ProjectCompare_Range2", resourceCulture);

	public static string ProjectCompare_Value1 => ResourceManager.GetString("ProjectCompare_Value1", resourceCulture);

	public static string ProjectCompare_Value2 => ResourceManager.GetString("ProjectCompare_Value2", resourceCulture);

	public static string Property_Proj => ResourceManager.GetString("Property_Proj", resourceCulture);

	public static string Proportional_Gain => ResourceManager.GetString("Proportional_Gain", resourceCulture);

	public static string PTO_Inst => ResourceManager.GetString("PTO_Inst", resourceCulture);

	public static string PTV_Convert => ResourceManager.GetString("PTV_Convert", resourceCulture);

	public static string PTV_Folder => ResourceManager.GetString("PTV_Folder", resourceCulture);

	public static string PTV_New_Folder => ResourceManager.GetString("PTV_New_Folder", resourceCulture);

	public static string PTV_New_Funcblock => ResourceManager.GetString("PTV_New_Funcblock", resourceCulture);

	public static string PTV_New_Modbus_Table => ResourceManager.GetString("PTV_New_Modbus_Table", resourceCulture);

	public static string PTV_New_Network => ResourceManager.GetString("PTV_New_Network", resourceCulture);

	public static string PTV_New_SubRoutine => ResourceManager.GetString("PTV_New_SubRoutine", resourceCulture);

	public static string PTV_No_Function => ResourceManager.GetString("PTV_No_Function", resourceCulture);

	public static string PTV_Rename => ResourceManager.GetString("PTV_Rename", resourceCulture);

	public static string PTV_Table => ResourceManager.GetString("PTV_Table", resourceCulture);

	public static string Pulse_Alternation => ResourceManager.GetString("Pulse_Alternation", resourceCulture);

	public static string Pulse_Frequency => ResourceManager.GetString("Pulse_Frequency", resourceCulture);

	public static string Pulse_Params_Setting => ResourceManager.GetString("Pulse_Params_Setting", resourceCulture);

	public static string Pulse_Platform => ResourceManager.GetString("Pulse_Platform", resourceCulture);

	public static string Pulse_Platform_Message => ResourceManager.GetString("Pulse_Platform_Message", resourceCulture);

	public static string Pulselize => ResourceManager.GetString("Pulselize", resourceCulture);

	public static string PulseMonitor_Arguments => ResourceManager.GetString("PulseMonitor_Arguments", resourceCulture);

	public static string PulseMonitor_AutoUpdateArguments => ResourceManager.GetString("PulseMonitor_AutoUpdateArguments", resourceCulture);

	public static string PulseMonitor_ClearData => ResourceManager.GetString("PulseMonitor_ClearData", resourceCulture);

	public static string PulseMonitor_Frequency => ResourceManager.GetString("PulseMonitor_Frequency", resourceCulture);

	public static string PulseMonitor_OnlyMonitorPulse => ResourceManager.GetString("PulseMonitor_OnlyMonitorPulse", resourceCulture);

	public static string PulseMonitor_PulseDiagram => ResourceManager.GetString("PulseMonitor_PulseDiagram", resourceCulture);

	public static string PulseMonitor_PulseSelection => ResourceManager.GetString("PulseMonitor_PulseSelection", resourceCulture);

	public static string PulseMonitor_Time => ResourceManager.GetString("PulseMonitor_Time", resourceCulture);

	public static string PulseMonitor_UpdateArguments => ResourceManager.GetString("PulseMonitor_UpdateArguments", resourceCulture);

	public static string PulseOutput => ResourceManager.GetString("PulseOutput", resourceCulture);

	public static string PulseWeight => ResourceManager.GetString("PulseWeight", resourceCulture);

	public static string PWM_Inst => ResourceManager.GetString("PWM_Inst", resourceCulture);

	public static string PWMS_Inst => ResourceManager.GetString("PWMS_Inst", resourceCulture);

	public static string Quick_Add => ResourceManager.GetString("Quick_Add", resourceCulture);

	public static string Quick_Add_Element => ResourceManager.GetString("Quick_Add_Element", resourceCulture);

	public static string Range_Error => ResourceManager.GetString("Range_Error", resourceCulture);

	public static string RangeTextBox_ToLowRange => ResourceManager.GetString("RangeTextBox_ToLowRange", resourceCulture);

	public static string RangeTextBox_ToTopRange => ResourceManager.GetString("RangeTextBox_ToTopRange", resourceCulture);

	public static string Read_Bit => ResourceManager.GetString("Read_Bit", resourceCulture);

	public static string Read_DWord => ResourceManager.GetString("Read_DWord", resourceCulture);

	public static string Read_Word => ResourceManager.GetString("Read_Word", resourceCulture);

	public static string ReadOnly => ResourceManager.GetString("ReadOnly", resourceCulture);

	public static string ReadOnly_Describe => ResourceManager.GetString("ReadOnly_Describe", resourceCulture);

	public static string Ready => ResourceManager.GetString("Ready", resourceCulture);

	public static string RealTime_Check_Error => ResourceManager.GetString("RealTime_Check_Error", resourceCulture);

	public static string REALTIME_CLOCK_ERROR => ResourceManager.GetString("REALTIME_CLOCK_ERROR", resourceCulture);

	public static string RealTime_Format => ResourceManager.GetString("RealTime_Format", resourceCulture);

	public static string RealTime_Setting => ResourceManager.GetString("RealTime_Setting", resourceCulture);

	public static string RealTimeFormatError => ResourceManager.GetString("RealTimeFormatError", resourceCulture);

	public static string Redo => ResourceManager.GetString("Redo", resourceCulture);

	public static string ReflictedAddress => ResourceManager.GetString("ReflictedAddress", resourceCulture);

	public static string Register => ResourceManager.GetString("Register", resourceCulture);

	public static string Register_Range => ResourceManager.GetString("Register_Range", resourceCulture);

	public static string Relative => ResourceManager.GetString("Relative", resourceCulture);

	public static string Release => ResourceManager.GetString("Release", resourceCulture);

	public static string Release_All => ResourceManager.GetString("Release_All", resourceCulture);

	public static string Replace => ResourceManager.GetString("Replace", resourceCulture);

	public static string Replace_Result => ResourceManager.GetString("Replace_Result", resourceCulture);

	public static string ReplaceWindow_Click_Tooltip => ResourceManager.GetString("ReplaceWindow_Click_Tooltip", resourceCulture);

	public static string ReplaceWindow_Input_Tooltip => ResourceManager.GetString("ReplaceWindow_Input_Tooltip", resourceCulture);

	public static string ReplaceWindow_Table_Tooltip => ResourceManager.GetString("ReplaceWindow_Table_Tooltip", resourceCulture);

	public static string Report_All_Sources => ResourceManager.GetString("Report_All_Sources", resourceCulture);

	public static string Report_Display_Output_Source => ResourceManager.GetString("Report_Display_Output_Source", resourceCulture);

	public static string Report_Funcblock_Compilation => ResourceManager.GetString("Report_Funcblock_Compilation", resourceCulture);

	public static string Report_Generate => ResourceManager.GetString("Report_Generate", resourceCulture);

	public static string Required_File_Path => ResourceManager.GetString("Required_File_Path", resourceCulture);

	public static string Reset_Coil => ResourceManager.GetString("Reset_Coil", resourceCulture);

	public static string Reset_Immediately => ResourceManager.GetString("Reset_Immediately", resourceCulture);

	public static string Resize_Columns => ResourceManager.GetString("Resize_Columns", resourceCulture);

	public static string Resize_Colums_failed => ResourceManager.GetString("Resize_Colums_failed", resourceCulture);

	public static string Restore => ResourceManager.GetString("Restore", resourceCulture);

	public static string Ret_Edit_Mode => ResourceManager.GetString("Ret_Edit_Mode", resourceCulture);

	public static string REV_Inst => ResourceManager.GetString("REV_Inst", resourceCulture);

	public static string ROL_Inst => ResourceManager.GetString("ROL_Inst", resourceCulture);

	public static string ROLD_Inst => ResourceManager.GetString("ROLD_Inst", resourceCulture);

	public static string ROR_Inst => ResourceManager.GetString("ROR_Inst", resourceCulture);

	public static string RORD_Inst => ResourceManager.GetString("RORD_Inst", resourceCulture);

	public static string Round => ResourceManager.GetString("Round", resourceCulture);

	public static string Routine => ResourceManager.GetString("Routine", resourceCulture);

	public static string Routine_Range => ResourceManager.GetString("Routine_Range", resourceCulture);

	public static string Routine_SelectedRange => ResourceManager.GetString("Routine_SelectedRange", resourceCulture);

	public static string RowInsertAfter => ResourceManager.GetString("RowInsertAfter", resourceCulture);

	public static string RowInsertBefore => ResourceManager.GetString("RowInsertBefore", resourceCulture);

	public static string RowInsertEnd => ResourceManager.GetString("RowInsertEnd", resourceCulture);

	public static string RSEC_Inst => ResourceManager.GetString("RSEC_Inst", resourceCulture);

	public static string Run_Stop => ResourceManager.GetString("Run_Stop", resourceCulture);

	public static string Same_Name_Funcblock => ResourceManager.GetString("Same_Name_Funcblock", resourceCulture);

	public static string Same_Name_Function => ResourceManager.GetString("Same_Name_Function", resourceCulture);

	public static string Sampling_Points => ResourceManager.GetString("Sampling_Points", resourceCulture);

	public static string Sampling_Time => ResourceManager.GetString("Sampling_Time", resourceCulture);

	public static string Save_Proj => ResourceManager.GetString("Save_Proj", resourceCulture);

	public static string SB_Column => ResourceManager.GetString("SB_Column", resourceCulture);

	public static string SB_Func => ResourceManager.GetString("SB_Func", resourceCulture);

	public static string SB_Modbus => ResourceManager.GetString("SB_Modbus", resourceCulture);

	public static string SB_Network => ResourceManager.GetString("SB_Network", resourceCulture);

	public static string SB_Program => ResourceManager.GetString("SB_Program", resourceCulture);

	public static string SB_Routine => ResourceManager.GetString("SB_Routine", resourceCulture);

	public static string SB_Row => ResourceManager.GetString("SB_Row", resourceCulture);

	public static string Scale => ResourceManager.GetString("Scale", resourceCulture);

	public static string ScanCycle => ResourceManager.GetString("ScanCycle", resourceCulture);

	public static string Search => ResourceManager.GetString("Search", resourceCulture);

	public static string Second => ResourceManager.GetString("Second", resourceCulture);

	public static string Select_Element_Range => ResourceManager.GetString("Select_Element_Range", resourceCulture);

	public static string Select_Module => ResourceManager.GetString("Select_Module", resourceCulture);

	public static string Select_Option => ResourceManager.GetString("Select_Option", resourceCulture);

	public static string Self_Tuning_State => ResourceManager.GetString("Self-Tuning_State", resourceCulture);

	public static string Selfloop_Error => ResourceManager.GetString("Selfloop_Error", resourceCulture);

	public static string SEND_Inst => ResourceManager.GetString("SEND_Inst", resourceCulture);

	public static string Separator_Select => ResourceManager.GetString("Separator_Select", resourceCulture);

	public static string Serialport => ResourceManager.GetString("Serialport", resourceCulture);

	public static string SerialPort_Occupied => ResourceManager.GetString("SerialPort_Occupied", resourceCulture);

	public static string Series_Motion => ResourceManager.GetString("Series_Motion", resourceCulture);

	public static string Series_PLC => ResourceManager.GetString("Series_PLC", resourceCulture);

	public static string Series_PLC_HMI => ResourceManager.GetString("Series_PLC_HMI", resourceCulture);

	public static string Set_Coil => ResourceManager.GetString("Set_Coil", resourceCulture);

	public static string Set_Immediately => ResourceManager.GetString("Set_Immediately", resourceCulture);

	public static string Set_Value => ResourceManager.GetString("Set_Value", resourceCulture);

	public static string Setpoint_High_Limit => ResourceManager.GetString("Setpoint_High_Limit", resourceCulture);

	public static string Setting => ResourceManager.GetString("Setting", resourceCulture);

	public static string Setting_Error => ResourceManager.GetString("Setting_Error", resourceCulture);

	public static string Setting_LadderShow => ResourceManager.GetString("Setting_LadderShow", resourceCulture);

	public static string Setting_Modify => ResourceManager.GetString("Setting_Modify", resourceCulture);

	public static string SFC_Check => ResourceManager.GetString("SFC_Check", resourceCulture);

	public static string SFC_Column => ResourceManager.GetString("SFC_Column", resourceCulture);

	public static string SFC_Compile => ResourceManager.GetString("SFC_Compile", resourceCulture);

	public static string SFC_Copy => ResourceManager.GetString("SFC_Copy", resourceCulture);

	public static string SFC_CurrentRoutine => ResourceManager.GetString("SFC_CurrentRoutine", resourceCulture);

	public static string SFC_Cut => ResourceManager.GetString("SFC_Cut", resourceCulture);

	public static string SFC_Delete => ResourceManager.GetString("SFC_Delete", resourceCulture);

	public static string SFC_EditMode => ResourceManager.GetString("SFC_EditMode", resourceCulture);

	public static string SFC_EndDown => ResourceManager.GetString("SFC_EndDown", resourceCulture);

	public static string SFC_InvalidDown => ResourceManager.GetString("SFC_InvalidDown", resourceCulture);

	public static string SFC_InvalidJump => ResourceManager.GetString("SFC_InvalidJump", resourceCulture);

	public static string SFC_InvalidUp => ResourceManager.GetString("SFC_InvalidUp", resourceCulture);

	public static string SFC_JumpDown => ResourceManager.GetString("SFC_JumpDown", resourceCulture);

	public static string SFC_Location => ResourceManager.GetString("SFC_Location", resourceCulture);

	public static string SFC_LookMode => ResourceManager.GetString("SFC_LookMode", resourceCulture);

	public static string SFC_NotSelectRoutine => ResourceManager.GetString("SFC_NotSelectRoutine", resourceCulture);

	public static string SFC_OpenDown => ResourceManager.GetString("SFC_OpenDown", resourceCulture);

	public static string SFC_OpenUp => ResourceManager.GetString("SFC_OpenUp", resourceCulture);

	public static string SFC_OrMerge_OutOfRange => ResourceManager.GetString("SFC_OrMerge_OutOfRange", resourceCulture);

	public static string SFC_Paste => ResourceManager.GetString("SFC_Paste", resourceCulture);

	public static string SFC_Row => ResourceManager.GetString("SFC_Row", resourceCulture);

	public static string SFC_SelectAll => ResourceManager.GetString("SFC_SelectAll", resourceCulture);

	public static string SFC_SFCLadder => ResourceManager.GetString("SFC_SFCLadder", resourceCulture);

	public static string SFCAttribute => ResourceManager.GetString("SFCAttribute", resourceCulture);

	public static string SFCDialog_StateRegister => ResourceManager.GetString("SFCDialog_StateRegister", resourceCulture);

	public static string SFCEditor => ResourceManager.GetString("SFCEditor", resourceCulture);

	public static string SFCLadder_Changed => ResourceManager.GetString("SFCLadder_Changed", resourceCulture);

	public static string SFCLadder_Check => ResourceManager.GetString("SFCLadder_Check", resourceCulture);

	public static string SFCOutputDialog_Condition => ResourceManager.GetString("SFCOutputDialog_Condition", resourceCulture);

	public static string SFCOutputDialog_Output => ResourceManager.GetString("SFCOutputDialog_Output", resourceCulture);

	public static string SFCOutputDialog_Title => ResourceManager.GetString("SFCOutputDialog_Title", resourceCulture);

	public static string SFCType => ResourceManager.GetString("SFCType", resourceCulture);

	public static string SFCType_ConditionBranch => ResourceManager.GetString("SFCType_ConditionBranch", resourceCulture);

	public static string SFCType_ConditionMerge => ResourceManager.GetString("SFCType_ConditionMerge", resourceCulture);

	public static string SFCType_CP_Branch => ResourceManager.GetString("SFCType_CP_Branch", resourceCulture);

	public static string SFCType_CP_Merge => ResourceManager.GetString("SFCType_CP_Merge", resourceCulture);

	public static string SFCType_Empty => ResourceManager.GetString("SFCType_Empty", resourceCulture);

	public static string SFCType_End => ResourceManager.GetString("SFCType_End", resourceCulture);

	public static string SFCType_Jump => ResourceManager.GetString("SFCType_Jump", resourceCulture);

	public static string SFCType_Line => ResourceManager.GetString("SFCType_Line", resourceCulture);

	public static string SFCType_ParellelBranch => ResourceManager.GetString("SFCType_ParellelBranch", resourceCulture);

	public static string SFCType_ParellelMerge => ResourceManager.GetString("SFCType_ParellelMerge", resourceCulture);

	public static string SFCType_State => ResourceManager.GetString("SFCType_State", resourceCulture);

	public static string SFCType_Transfer => ResourceManager.GetString("SFCType_Transfer", resourceCulture);

	public static string SFCUnitDialog_Title => ResourceManager.GetString("SFCUnitDialog_Title", resourceCulture);

	public static string SHL_Inst => ResourceManager.GetString("SHL_Inst", resourceCulture);

	public static string SHLB_Inst => ResourceManager.GetString("SHLB_Inst", resourceCulture);

	public static string SHLD_Inst => ResourceManager.GetString("SHLD_Inst", resourceCulture);

	public static string Short_Error => ResourceManager.GetString("Short_Error", resourceCulture);

	public static string Show_Dashed => ResourceManager.GetString("Show_Dashed", resourceCulture);

	public static string Show_Password => ResourceManager.GetString("Show_Password", resourceCulture);

	public static string ShowAlias => ResourceManager.GetString("ShowAlias", resourceCulture);

	public static string ShowDoubleWord => ResourceManager.GetString("ShowDoubleWord", resourceCulture);

	public static string SHR_Inst => ResourceManager.GetString("SHR_Inst", resourceCulture);

	public static string SHRB_Inst => ResourceManager.GetString("SHRB_Inst", resourceCulture);

	public static string SHRD_Inst => ResourceManager.GetString("SHRD_Inst", resourceCulture);

	public static string Simulate => ResourceManager.GetString("Simulate", resourceCulture);

	public static string Simulate_Another_Running => ResourceManager.GetString("Simulate_Another_Running", resourceCulture);

	public static string Simulate_Closing => ResourceManager.GetString("Simulate_Closing", resourceCulture);

	public static string Simulate_Error => ResourceManager.GetString("Simulate_Error", resourceCulture);

	public static string Simulate_Initing => ResourceManager.GetString("Simulate_Initing", resourceCulture);

	public static string Sin_Operation => ResourceManager.GetString("Sin_Operation", resourceCulture);

	public static string Size => ResourceManager.GetString("Size", resourceCulture);

	public static string SMOV_Inst => ResourceManager.GetString("SMOV_Inst", resourceCulture);

	public static string Soft_Element_Init => ResourceManager.GetString("Soft_Element_Init", resourceCulture);

	public static string Soft_Element_Manager => ResourceManager.GetString("Soft_Element_Manager", resourceCulture);

	public static string Space => ResourceManager.GetString("Space", resourceCulture);

	public static string Special_Instruction_Error => ResourceManager.GetString("Special_Instruction_Error", resourceCulture);

	public static string Special_Judgment => ResourceManager.GetString("Special_Judgment", resourceCulture);

	public static string SPLINE_DATA_ERROR => ResourceManager.GetString("SPLINE_DATA_ERROR", resourceCulture);

	public static string SQR_Inst => ResourceManager.GetString("SQR_Inst", resourceCulture);

	public static string Sqrt_Operation => ResourceManager.GetString("Sqrt_Operation", resourceCulture);

	public static string SRegisterRange => ResourceManager.GetString("SRegisterRange", resourceCulture);

	public static string ST_Inst => ResourceManager.GetString("ST_Inst", resourceCulture);

	public static string Stack_Overflow_Error => ResourceManager.GetString("Stack_Overflow_Error", resourceCulture);

	public static string Start_Address => ResourceManager.GetString("Start_Address", resourceCulture);

	public static string Start_Frequency => ResourceManager.GetString("Start_Frequency", resourceCulture);

	public static string State_Failed => ResourceManager.GetString("State_Failed", resourceCulture);

	public static string Statement => ResourceManager.GetString("Statement", resourceCulture);

	public static string Station_Number_Setting => ResourceManager.GetString("Station_Number_Setting", resourceCulture);

	public static string Status => ResourceManager.GetString("Status", resourceCulture);

	public static string STL_Inst => ResourceManager.GetString("STL_Inst", resourceCulture);

	public static string STLE_Inst => ResourceManager.GetString("STLE_Inst", resourceCulture);

	public static string Stop_Bit => ResourceManager.GetString("Stop_Bit", resourceCulture);

	public static string Stop_Run => ResourceManager.GetString("Stop_Run", resourceCulture);

	public static string Store_Address => ResourceManager.GetString("Store_Address", resourceCulture);

	public static string StringFormat_NetCross => ResourceManager.GetString("StringFormat_NetCross", resourceCulture);

	public static string StringFormat_NetIn => ResourceManager.GetString("StringFormat_NetIn", resourceCulture);

	public static string StringFormat_Version_BarCode => ResourceManager.GetString("StringFormat_Version_BarCode", resourceCulture);

	public static string SubNetMask => ResourceManager.GetString("SubNetMask", resourceCulture);

	public static string SubRoutine => ResourceManager.GetString("SubRoutine", resourceCulture);

	public static string SubRoutine_Name => ResourceManager.GetString("SubRoutine_Name", resourceCulture);

	public static string SystemD => ResourceManager.GetString("SystemD", resourceCulture);

	public static string SystemM => ResourceManager.GetString("SystemM", resourceCulture);

	public static string SystemRegister => ResourceManager.GetString("SystemRegister", resourceCulture);

	public static string SysUnit => ResourceManager.GetString("SysUnit", resourceCulture);

	public static string TableRename => ResourceManager.GetString("TableRename", resourceCulture);

	public static string Tan_Operation => ResourceManager.GetString("Tan_Operation", resourceCulture);

	public static string Target_IPAddress => ResourceManager.GetString("Target_IPAddress", resourceCulture);

	public static string Target_Port => ResourceManager.GetString("Target_Port", resourceCulture);

	public static string TargetValue => ResourceManager.GetString("TargetValue", resourceCulture);

	public static string TBL_ATCTime => ResourceManager.GetString("TBL_ATCTime", resourceCulture);

	public static string TBL_Condition => ResourceManager.GetString("TBL_Condition", resourceCulture);

	public static string TBL_DataAddress => ResourceManager.GetString("TBL_DataAddress", resourceCulture);

	public static string TBL_EndLabel => ResourceManager.GetString("TBL_EndLabel", resourceCulture);

	public static string TBL_Frequency => ResourceManager.GetString("TBL_Frequency", resourceCulture);

	public static string TBL_ID => ResourceManager.GetString("TBL_ID", resourceCulture);

	public static string TBL_Inst => ResourceManager.GetString("TBL_Inst", resourceCulture);

	public static string TBL_JumpTo => ResourceManager.GetString("TBL_JumpTo", resourceCulture);

	public static string TBL_PulseDirection => ResourceManager.GetString("TBL_PulseDirection", resourceCulture);

	public static string TBL_PulseFinish => ResourceManager.GetString("TBL_PulseFinish", resourceCulture);

	public static string TBL_PulseNumber => ResourceManager.GetString("TBL_PulseNumber", resourceCulture);

	public static string TBL_PulseOutput => ResourceManager.GetString("TBL_PulseOutput", resourceCulture);

	public static string TBL_WaitEvent => ResourceManager.GetString("TBL_WaitEvent", resourceCulture);

	public static string TBL_WaitSignal => ResourceManager.GetString("TBL_WaitSignal", resourceCulture);

	public static string TCMP_Inst => ResourceManager.GetString("TCMP_Inst", resourceCulture);

	public static string TCPClient => ResourceManager.GetString("TCPClient", resourceCulture);

	public static string TCPServer => ResourceManager.GetString("TCPServer", resourceCulture);

	public static string TECAM_KeyPoints => ResourceManager.GetString("TECAM_KeyPoints", resourceCulture);

	public static string Text_FBDComment => ResourceManager.GetString("Text_FBDComment", resourceCulture);

	public static string Text_FBDMonitor => ResourceManager.GetString("Text_FBDMonitor", resourceCulture);

	public static string Text_FBDNetwork => ResourceManager.GetString("Text_FBDNetwork", resourceCulture);

	public static string Text_FBDTable => ResourceManager.GetString("Text_FBDTable", resourceCulture);

	public static string Text_FBDValue => ResourceManager.GetString("Text_FBDValue", resourceCulture);

	public static string Text_NewName => ResourceManager.GetString("Text_NewName", resourceCulture);

	public static string Text_NewName_CannotBeEmpty => ResourceManager.GetString("Text_NewName_CannotBeEmpty", resourceCulture);

	public static string Text_OldName => ResourceManager.GetString("Text_OldName", resourceCulture);

	public static string TextMode_Comment => ResourceManager.GetString("TextMode_Comment", resourceCulture);

	public static string TextMode_Element => ResourceManager.GetString("TextMode_Element", resourceCulture);

	public static string TextMode_Title => ResourceManager.GetString("TextMode_Title", resourceCulture);

	public static string Thermocouple => ResourceManager.GetString("Thermocouple", resourceCulture);

	public static string Timed_Save => ResourceManager.GetString("Timed_Save", resourceCulture);

	public static string Timed_Save_Settings => ResourceManager.GetString("Timed_Save_Settings", resourceCulture);

	public static string Timed_Update => ResourceManager.GetString("Timed_Update", resourceCulture);

	public static string Timed_Update_PLC => ResourceManager.GetString("Timed_Update_PLC", resourceCulture);

	public static string Timeout => ResourceManager.GetString("Timeout", resourceCulture);

	public static string Title_MultiplyModify => ResourceManager.GetString("Title_MultiplyModify", resourceCulture);

	public static string Title_SimuCommBuildDialog => ResourceManager.GetString("Title_SimuCommBuildDialog", resourceCulture);

	public static string TOF_Inst => ResourceManager.GetString("TOF_Inst", resourceCulture);

	public static string TON_Inst => ResourceManager.GetString("TON_Inst", resourceCulture);

	public static string TONR_Inst => ResourceManager.GetString("TONR_Inst", resourceCulture);

	public static string ToolTip_Brief => ResourceManager.GetString("ToolTip_Brief", resourceCulture);

	public static string Tooltip_CapslockIsOpened => ResourceManager.GetString("Tooltip_CapslockIsOpened", resourceCulture);

	public static string Tooltip_Col_Delete => ResourceManager.GetString("Tooltip_Col_Delete", resourceCulture);

	public static string Tooltip_Col_Insert => ResourceManager.GetString("Tooltip_Col_Insert", resourceCulture);

	public static string ToolTip_Comment => ResourceManager.GetString("ToolTip_Comment", resourceCulture);

	public static string Tooltip_CompareClose => ResourceManager.GetString("Tooltip_CompareClose", resourceCulture);

	public static string Tooltip_CompareOpen => ResourceManager.GetString("Tooltip_CompareOpen", resourceCulture);

	public static string Tooltip_CompareUpload => ResourceManager.GetString("Tooltip_CompareUpload", resourceCulture);

	public static string Tooltip_DeleteCol_SFC => ResourceManager.GetString("Tooltip_DeleteCol_SFC", resourceCulture);

	public static string Tooltip_DeleteRow_SFC => ResourceManager.GetString("Tooltip_DeleteRow_SFC", resourceCulture);

	public static string ToolTip_Describe => ResourceManager.GetString("ToolTip_Describe", resourceCulture);

	public static string Tooltip_ExportCSV => ResourceManager.GetString("Tooltip_ExportCSV", resourceCulture);

	public static string ToolTip_File_Select => ResourceManager.GetString("ToolTip_File_Select", resourceCulture);

	public static string Tooltip_InsertCol_SFC => ResourceManager.GetString("Tooltip_InsertCol_SFC", resourceCulture);

	public static string Tooltip_InsertRow_SFC => ResourceManager.GetString("Tooltip_InsertRow_SFC", resourceCulture);

	public static string ToolTip_Message => ResourceManager.GetString("ToolTip_Message", resourceCulture);

	public static string ToolTip_Name => ResourceManager.GetString("ToolTip_Name", resourceCulture);

	public static string Tooltip_Net_Delete => ResourceManager.GetString("Tooltip_Net_Delete", resourceCulture);

	public static string Tooltip_Net_Insert => ResourceManager.GetString("Tooltip_Net_Insert", resourceCulture);

	public static string ToolTip_Parameter => ResourceManager.GetString("ToolTip_Parameter", resourceCulture);

	public static string ToolTip_Password => ResourceManager.GetString("ToolTip_Password", resourceCulture);

	public static string ToolTip_Path => ResourceManager.GetString("ToolTip_Path", resourceCulture);

	public static string ToolTip_Replace => ResourceManager.GetString("ToolTip_Replace", resourceCulture);

	public static string Tooltip_Row_Delete => ResourceManager.GetString("Tooltip_Row_Delete", resourceCulture);

	public static string Tooltip_Row_Insert => ResourceManager.GetString("Tooltip_Row_Insert", resourceCulture);

	public static string ToolTip_Search => ResourceManager.GetString("ToolTip_Search", resourceCulture);

	public static string TransformInterval => ResourceManager.GetString("TransformInterval", resourceCulture);

	public static string TRD_Inst => ResourceManager.GetString("TRD_Inst", resourceCulture);

	public static string TRDS_Inst => ResourceManager.GetString("TRDS_Inst", resourceCulture);

	public static string Trunc => ResourceManager.GetString("Trunc", resourceCulture);

	public static string TSEC_Inst => ResourceManager.GetString("TSEC_Inst", resourceCulture);

	public static string TWR_Inst => ResourceManager.GetString("TWR_Inst", resourceCulture);

	public static string TWRS_Inst => ResourceManager.GetString("TWRS_Inst", resourceCulture);

	public static string TZCP_Inst => ResourceManager.GetString("TZCP_Inst", resourceCulture);

	public static string UDPClient => ResourceManager.GetString("UDPClient", resourceCulture);

	public static string UDPServer => ResourceManager.GetString("UDPServer", resourceCulture);

	public static string Unable => ResourceManager.GetString("Unable", resourceCulture);

	public static string Undo => ResourceManager.GetString("Undo", resourceCulture);

	public static string Unified_Mode => ResourceManager.GetString("Unified_Mode", resourceCulture);

	public static string UnitComment => ResourceManager.GetString("UnitComment", resourceCulture);

	public static string UnitComment_EditAfter => ResourceManager.GetString("UnitComment_EditAfter", resourceCulture);

	public static string UnitComment_EditBefore => ResourceManager.GetString("UnitComment_EditBefore", resourceCulture);

	public static string UnitComment_InsertAfter => ResourceManager.GetString("UnitComment_InsertAfter", resourceCulture);

	public static string UnitComment_InsertBefore => ResourceManager.GetString("UnitComment_InsertBefore", resourceCulture);

	public static string UnitComment_Modify => ResourceManager.GetString("UnitComment_Modify", resourceCulture);

	public static string UnitComment_RemoveAfter => ResourceManager.GetString("UnitComment_RemoveAfter", resourceCulture);

	public static string UnitComment_RemoveBefore => ResourceManager.GetString("UnitComment_RemoveBefore", resourceCulture);

	public static string Unknowed_Exception => ResourceManager.GetString("Unknowed_Exception", resourceCulture);

	public static string Unknown_Error => ResourceManager.GetString("Unknown_Error", resourceCulture);

	public static string Unknown_PLC_Type => ResourceManager.GetString("Unknown_PLC_Type", resourceCulture);

	public static string UnknownBarCode => ResourceManager.GetString("UnknownBarCode", resourceCulture);

	public static string Unlock_Ladder => ResourceManager.GetString("Unlock_Ladder", resourceCulture);

	public static string Unlocked_Ladder => ResourceManager.GetString("Unlocked_Ladder", resourceCulture);

	public static string Unlocked_Success => ResourceManager.GetString("Unlocked_Success", resourceCulture);

	public static string Update_Or_Not => ResourceManager.GetString("Update_Or_Not", resourceCulture);

	public static string Update_Process => ResourceManager.GetString("Update_Process", resourceCulture);

	public static string Update_Size => ResourceManager.GetString("Update_Size", resourceCulture);

	public static string Update_Whether => ResourceManager.GetString("Update_Whether", resourceCulture);

	public static string Upload => ResourceManager.GetString("Upload", resourceCulture);

	public static string Upload_Data_Error => ResourceManager.GetString("Upload_Data_Error", resourceCulture);

	public static string Upload_Encryption => ResourceManager.GetString("Upload_Encryption", resourceCulture);

	public static string Upload_Fail => ResourceManager.GetString("Upload_Fail", resourceCulture);

	public static string Uploading => ResourceManager.GetString("Uploading", resourceCulture);

	public static string UploadPassword_Accept => ResourceManager.GetString("UploadPassword_Accept", resourceCulture);

	public static string UploadPassword_Error => ResourceManager.GetString("UploadPassword_Error", resourceCulture);

	public static string UploadPassword_Message => ResourceManager.GetString("UploadPassword_Message", resourceCulture);

	public static string UploadPassword_Title => ResourceManager.GetString("UploadPassword_Title", resourceCulture);

	public static string UploadPasswordNotion => ResourceManager.GetString("UploadPasswordNotion", resourceCulture);

	public static string USB_DOWNLOAD_TIMEOUT_ERROR => ResourceManager.GetString("USB_DOWNLOAD_TIMEOUT_ERROR", resourceCulture);

	public static string USB_Relink => ResourceManager.GetString("USB_Relink", resourceCulture);

	public static string Use_Expansion_Module => ResourceManager.GetString("Use_Expansion_Module", resourceCulture);

	public static string Use_Regular_Expression => ResourceManager.GetString("Use_Regular_Expression", resourceCulture);

	public static string Used_Element => ResourceManager.GetString("Used_Element", resourceCulture);

	public static string UsedHMI => ResourceManager.GetString("UsedHMI", resourceCulture);

	public static string UseHardwareFaultSelfRestore => ResourceManager.GetString("UseHardwareFaultSelfRestore", resourceCulture);

	public static string User_Function => ResourceManager.GetString("User_Function", resourceCulture);

	public static string Valid_Range => ResourceManager.GetString("Valid_Range", resourceCulture);

	public static string ValueBrpo => ResourceManager.GetString("ValueBrpo", resourceCulture);

	public static string ValueBrpo_Change => ResourceManager.GetString("ValueBrpo_Change", resourceCulture);

	public static string ValueBrpo_DownEdge => ResourceManager.GetString("ValueBrpo_DownEdge", resourceCulture);

	public static string ValueBrpo_UpEdge => ResourceManager.GetString("ValueBrpo_UpEdge", resourceCulture);

	public static string ValueLine_Add => ResourceManager.GetString("ValueLine_Add", resourceCulture);

	public static string ValueLine_Delete => ResourceManager.GetString("ValueLine_Delete", resourceCulture);

	public static string ValueLine_Export => ResourceManager.GetString("ValueLine_Export", resourceCulture);

	public static string ValueLine_Import => ResourceManager.GetString("ValueLine_Import", resourceCulture);

	public static string ValueLine_Lock => ResourceManager.GetString("ValueLine_Lock", resourceCulture);

	public static string ValueLine_Show => ResourceManager.GetString("ValueLine_Show", resourceCulture);

	public static string ValueRange => ResourceManager.GetString("ValueRange", resourceCulture);

	public static string ValueTendency => ResourceManager.GetString("ValueTendency", resourceCulture);

	public static string Variable_Modification => ResourceManager.GetString("Variable_Modification", resourceCulture);

	public static string VariableType => ResourceManager.GetString("VariableType", resourceCulture);

	public static string Version_High => ResourceManager.GetString("Version_High", resourceCulture);

	public static string VersionMessageDialog_Title => ResourceManager.GetString("VersionMessageDialog_Title", resourceCulture);

	public static string VersionMessageDialog_Webside => ResourceManager.GetString("VersionMessageDialog_Webside", resourceCulture);

	public static string View_Forbid => ResourceManager.GetString("View_Forbid", resourceCulture);

	public static string View_Forbid_Describe => ResourceManager.GetString("View_Forbid_Describe", resourceCulture);

	public static string View_Help => ResourceManager.GetString("View_Help", resourceCulture);

	public static string Warning => ResourceManager.GetString("Warning", resourceCulture);

	public static string WATCH_DOG_TIMER_TIMEOUT_ERROR => ResourceManager.GetString("WATCH_DOG_TIMER_TIMEOUT_ERROR", resourceCulture);

	public static string WatchDog_Settings => ResourceManager.GetString("WatchDog_Settings", resourceCulture);

	public static string Watchdog_Time => ResourceManager.GetString("Watchdog_Time", resourceCulture);

	public static string WDT_Inst => ResourceManager.GetString("WDT_Inst", resourceCulture);

	public static string WeekTimeFormatError => ResourceManager.GetString("WeekTimeFormatError", resourceCulture);

	public static string WKCMP_Inst => ResourceManager.GetString("WKCMP_Inst", resourceCulture);

	public static string WKZCP_Inst => ResourceManager.GetString("WKZCP_Inst", resourceCulture);

	public static string Word => ResourceManager.GetString("Word", resourceCulture);

	public static string Word_Add => ResourceManager.GetString("Word_Add", resourceCulture);

	public static string Word_Add_One => ResourceManager.GetString("Word_Add_One", resourceCulture);

	public static string Word_And => ResourceManager.GetString("Word_And", resourceCulture);

	public static string Word_Divide => ResourceManager.GetString("Word_Divide", resourceCulture);

	public static string Word_Equal => ResourceManager.GetString("Word_Equal", resourceCulture);

	public static string Word_Less => ResourceManager.GetString("Word_Less", resourceCulture);

	public static string Word_Minus => ResourceManager.GetString("Word_Minus", resourceCulture);

	public static string Word_Minus_One => ResourceManager.GetString("Word_Minus_One", resourceCulture);

	public static string Word_Mod => ResourceManager.GetString("Word_Mod", resourceCulture);

	public static string Word_More => ResourceManager.GetString("Word_More", resourceCulture);

	public static string Word_Multiply => ResourceManager.GetString("Word_Multiply", resourceCulture);

	public static string Word_Not_Equal => ResourceManager.GetString("Word_Not_Equal", resourceCulture);

	public static string Word_Not_Less => ResourceManager.GetString("Word_Not_Less", resourceCulture);

	public static string Word_Not_More => ResourceManager.GetString("Word_Not_More", resourceCulture);

	public static string Word_Or => ResourceManager.GetString("Word_Or", resourceCulture);

	public static string Word_Reverse => ResourceManager.GetString("Word_Reverse", resourceCulture);

	public static string Word_To_DWord => ResourceManager.GetString("Word_To_DWord", resourceCulture);

	public static string Word_XOR => ResourceManager.GetString("Word_XOR", resourceCulture);

	public static string Write_Bit => ResourceManager.GetString("Write_Bit", resourceCulture);

	public static string Write_Bits => ResourceManager.GetString("Write_Bits", resourceCulture);

	public static string Write_DWord => ResourceManager.GetString("Write_DWord", resourceCulture);

	public static string Write_DWords => ResourceManager.GetString("Write_DWords", resourceCulture);

	public static string Write_In => ResourceManager.GetString("Write_In", resourceCulture);

	public static string Write_OFF => ResourceManager.GetString("Write_OFF", resourceCulture);

	public static string Write_ON => ResourceManager.GetString("Write_ON", resourceCulture);

	public static string Write_Realtime => ResourceManager.GetString("Write_Realtime", resourceCulture);

	public static string Write_Word => ResourceManager.GetString("Write_Word", resourceCulture);

	public static string Write_Words => ResourceManager.GetString("Write_Words", resourceCulture);

	public static string WriteRealTime => ResourceManager.GetString("WriteRealTime", resourceCulture);

	public static string WSFL_Inst => ResourceManager.GetString("WSFL_Inst", resourceCulture);

	public static string WSFR_Inst => ResourceManager.GetString("WSFR_Inst", resourceCulture);

	public static string XAxias => ResourceManager.GetString("XAxias", resourceCulture);

	public static string XCH_Inst => ResourceManager.GetString("XCH_Inst", resourceCulture);

	public static string XCHD_Inst => ResourceManager.GetString("XCHD_Inst", resourceCulture);

	public static string XCHF_Inst => ResourceManager.GetString("XCHF_Inst", resourceCulture);

	public static string XmlData_Size_Exceed => ResourceManager.GetString("XmlData_Size_Exceed", resourceCulture);

	public static string YAxias => ResourceManager.GetString("YAxias", resourceCulture);

	public static string YES => ResourceManager.GetString("YES", resourceCulture);

	public static string ZCP_Inst => ResourceManager.GetString("ZCP_Inst", resourceCulture);

	public static string ZCPD_Inst => ResourceManager.GetString("ZCPD_Inst", resourceCulture);

	public static string ZCPF_Inst => ResourceManager.GetString("ZCPF_Inst", resourceCulture);

	public static string Zoom_In => ResourceManager.GetString("Zoom_In", resourceCulture);

	public static string Zoom_Out => ResourceManager.GetString("Zoom_Out", resourceCulture);

	public static string ZRN_Inst => ResourceManager.GetString("ZRN_Inst", resourceCulture);

	public static string ZRND_Inst => ResourceManager.GetString("ZRND_Inst", resourceCulture);

	public static string ZRNR_Inst => ResourceManager.GetString("ZRNR_Inst", resourceCulture);

	public static string ZRNR_OriPosNegEqual => ResourceManager.GetString("ZRNR_OriPosNegEqual", resourceCulture);

	public static string ZRNRDialog_AccTime => ResourceManager.GetString("ZRNRDialog_AccTime", resourceCulture);

	public static string ZRNRDialog_AlwaysClose => ResourceManager.GetString("ZRNRDialog_AlwaysClose", resourceCulture);

	public static string ZRNRDialog_AlwaysOpen => ResourceManager.GetString("ZRNRDialog_AlwaysOpen", resourceCulture);

	public static string ZRNRDialog_CrawlVelocity => ResourceManager.GetString("ZRNRDialog_CrawlVelocity", resourceCulture);

	public static string ZRNRDialog_DefaultReturnZeroDirection => ResourceManager.GetString("ZRNRDialog_DefaultReturnZeroDirection", resourceCulture);

	public static string ZRNRDialog_InputPort => ResourceManager.GetString("ZRNRDialog_InputPort", resourceCulture);

	public static string ZRNRDialog_NearSignal => ResourceManager.GetString("ZRNRDialog_NearSignal", resourceCulture);

	public static string ZRNRDialog_NegativeDirection => ResourceManager.GetString("ZRNRDialog_NegativeDirection", resourceCulture);

	public static string ZRNRDialog_OriginRisingEdge => ResourceManager.GetString("ZRNRDialog_OriginRisingEdge", resourceCulture);

	public static string ZRNRDialog_OriginSignal => ResourceManager.GetString("ZRNRDialog_OriginSignal", resourceCulture);

	public static string ZRNRDialog_Pole => ResourceManager.GetString("ZRNRDialog_Pole", resourceCulture);

	public static string ZRNRDialog_PositiveDirection => ResourceManager.GetString("ZRNRDialog_PositiveDirection", resourceCulture);

	public static string ZRNRDialog_PulseOutputDirection => ResourceManager.GetString("ZRNRDialog_PulseOutputDirection", resourceCulture);

	public static string ZRNRDialog_PulseOutputPort => ResourceManager.GetString("ZRNRDialog_PulseOutputPort", resourceCulture);

	public static string ZRNRDialog_RegressVelocity => ResourceManager.GetString("ZRNRDialog_RegressVelocity", resourceCulture);

	public static string ZRNRDialog_ReturnZeroMode => ResourceManager.GetString("ZRNRDialog_ReturnZeroMode", resourceCulture);

	public static string ZRNRDialog_SlowTime => ResourceManager.GetString("ZRNRDialog_SlowTime", resourceCulture);

	public static string ZRNRDialog_UsedNegativeLimitationSignal => ResourceManager.GetString("ZRNRDialog_UsedNegativeLimitationSignal", resourceCulture);

	public static string ZRNRDialog_UsedPositiveLimitationSignal => ResourceManager.GetString("ZRNRDialog_UsedPositiveLimitationSignal", resourceCulture);

	public static string ZRNRDialog_UsedZDirectionSignal => ResourceManager.GetString("ZRNRDialog_UsedZDirectionSignal", resourceCulture);

	public static string ZSignal_Error => ResourceManager.GetString("ZSignal_Error", resourceCulture);

	internal Resources()
	{
	}
}
