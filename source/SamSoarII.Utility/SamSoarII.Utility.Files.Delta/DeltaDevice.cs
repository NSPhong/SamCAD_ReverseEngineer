using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaDevice
{
	public static readonly List<DeltaDevice> List;

	private static readonly List<DeltaRange> R_VFD_List;

	private static readonly List<DeltaSystemElement> S_VFD_List;

	private string name;

	private List<DeltaRange> ranges;

	private Dictionary<string, DeltaSystemElement> systemelements;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public IList<DeltaRange> Ranges => ranges;

	public Dictionary<string, DeltaSystemElement> SystemElements => systemelements;

	static DeltaDevice()
	{
		List = new List<DeltaDevice>();
		R_VFD_List = new List<DeltaRange>();
		S_VFD_List = new List<DeltaSystemElement>();
		DeltaDevice deltaDevice = null;
		R_VFD_List.Add(new DeltaRange("X", 0, 128));
		R_VFD_List.Add(new DeltaRange("Y", 0, 128));
		R_VFD_List.Add(new DeltaRange("M", 0, 1000));
		R_VFD_List.Add(new DeltaRange("D", 0, 2000));
		R_VFD_List.Add(new DeltaRange("T", 0, 160));
		R_VFD_List.Add(new DeltaRange("C", 0, 80));
		R_VFD_List.Add(new DeltaRange("P", 0, 64));
		S_VFD_List.Add(new DeltaSystemElement("M1000", "M8151")
		{
			Comment = "运行when常开"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1001", "M8152")
		{
			Comment = "运行when常闭"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1002", "M8150")
		{
			Comment = "The first scanning cycle is connected"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1003", null)
		{
			Comment = "The first scanning cycle is closed"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1005", null)
		{
			Comment = "Abnormal occurrence of the frequency converter"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1006", null)
		{
			Comment = "The output frequency status of the frequency converter"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1007", null)
		{
			Comment = "The operating direction of the frequency converter状态"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1011", "M8161")
		{
			Comment = "1-minute clock"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1012", "M8160")
		{
			Comment = "1s clock"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1013", "M8159")
		{
			Comment = "100ms clock"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1014", "M8158")
		{
			Comment = "10ms clock"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1015", null)
		{
			Comment = "The frequency of the frequency converter has reached the set value"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1016", null)
		{
			Comment = "There is an error in reading and writing the parameters of the frequency converter"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1017", null)
		{
			Comment = "The parameters of the frequency converter have been successfully written"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1020", "M8171")
		{
			Comment = "Zero flag"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1021", "M8170")
		{
			Comment = "Borrowing flag"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1022", "M8169")
		{
			Comment = "Carry flag"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1023", "M8172")
		{
			Comment = "The divisor is 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1025", null)
		{
			Comment = "Frequency converter operation symbol"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1026", null)
		{
			Comment = "The operating direction of the frequency converter"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1027", null)
		{
			Comment = "Inverter reset"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1031", null)
		{
			Comment = "强制设置积points量"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1032", null)
		{
			Comment = "Force lag FREQ over PID"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1034", null)
		{
			Comment = "Activate CANopen instant control"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1035", null)
		{
			Comment = "Activate the internal communication control"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1038", null)
		{
			Comment = "MI8 counting begins"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1039", null)
		{
			Comment = "Clear the MI8 count value"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1040", null)
		{
			Comment = "Hardware power supply"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1042", null)
		{
			Comment = "Quick stop"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1045", null)
		{
			Comment = "AVI values ​​ignore fine-tuning of parameters"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1046", null)
		{
			Comment = "ACI values ​​ignore fine-tuning of parameters"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1047", null)
		{
			Comment = "AUI values ​​ignore fine-tuning of parameters"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1048", null)
		{
			Comment = "Move to a new location"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1050", null)
		{
			Comment = "Relative position (0)/ Absolute position (1)"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1052", null)
		{
			Comment = "Lock the frequency"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1054", null)
		{
			Comment = "Force reset the absolute position"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1055", null)
		{
			Comment = "Search for the origin"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1056", null)
		{
			Comment = "The hardware is powered on."
		});
		S_VFD_List.Add(new DeltaSystemElement("M1058", null)
		{
			Comment = "Quick stop!"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1059", null)
		{
			Comment = "CANOpen master station setup completed"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1060", null)
		{
			Comment = "CANOpen is initializing the slave"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1061", null)
		{
			Comment = "CANOpen initial slave failed"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1063", null)
		{
			Comment = "Torque reached"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1064", null)
		{
			Comment = "The target location has arrived."
		});
		S_VFD_List.Add(new DeltaSystemElement("M1066", null)
		{
			Comment = "The reading and writing of CANOpen data have been completed"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1067", null)
		{
			Comment = "The CANOpen data reading and writing were successful"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1068", null)
		{
			Comment = "There is an error in the perpetual calendar calculation"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1070", null)
		{
			Comment = "Return to the origin completed"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1071", null)
		{
			Comment = "Error of returning to the origin"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1076", null)
		{
			Comment = "The perpetual calendar time is incorrect or the update has timed out"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1077", "M8181")
		{
			Comment = "485 read/write complete"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1078", null)
		{
			Comment = "485 read/write error"
		});
		S_VFD_List.Add(new DeltaSystemElement("M1079", null)
		{
			Comment = "485 Communication Timeout"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1001", null)
		{
			Comment = "Model system program version"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1002", null)
		{
			Comment = "Program capacity"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1003", null)
		{
			Comment = "The total sum of the contents in the program memory"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1010", "D8175")
		{
			Comment = "Current scanning time (unit: 0.1ms)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1011", null)
		{
			Comment = "最小扫描when间 (单位: 0.1ms) "
		});
		S_VFD_List.Add(new DeltaSystemElement("D1012", "D8177")
		{
			Comment = "最大扫描when间 (单位: 0.1ms)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1018", null)
		{
			Comment = "当前积points量"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1019", null)
		{
			Comment = "强制设定的积points量"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1020", null)
		{
			Comment = "Output frequency"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1021", null)
		{
			Comment = "Output current"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1022", null)
		{
			Comment = "AI AO DI DO expansion card number"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1023", null)
		{
			Comment = "Communication expansion card number"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1027", null)
		{
			Comment = "Frequency command for PID control"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1028", null)
		{
			Comment = "Corresponding value of AVI terminal (0.00 ~100.00%)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1029", null)
		{
			Comment = "Corresponding value of ACI terminal (0.00 ~100.00 %)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1030", null)
		{
			Comment = "Corresponding value of AUI terminal (0.00 ~100.00 %)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1035", null)
		{
			Comment = "The corresponding value of VR (0.0-100.00%)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1036", null)
		{
			Comment = "Inverter error code"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1037", null)
		{
			Comment = "The output frequency of the frequency converter"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1038", null)
		{
			Comment = "DC Bus voltage"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1039", null)
		{
			Comment = "Output voltage"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1040", null)
		{
			Comment = "Analog output value AFM1(0 / -100.00 to 100.00%)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1043", null)
		{
			Comment = "The Keypad displays the value"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1045", null)
		{
			Comment = "Analog output value AFM2(0 / -100.00 to 100.00%)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1050", null)
		{
			Comment = "Actual control mode"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1051", null)
		{
			Comment = "Actual location (Low word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1052", null)
		{
			Comment = "Actual location (High word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1053", null)
		{
			Comment = "Actual torque"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1054", null)
		{
			Comment = "The current count value calculated by MI8 (L Word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1055", null)
		{
			Comment = "The current count value calculated by MI8 (H Word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1056", null)
		{
			Comment = "The rotational speed corresponding to MI8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1057", null)
		{
			Comment = "The speed ratio of MI8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1058", null)
		{
			Comment = "The update speed (ms) corresponding to the rotational speed of MI8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1059", null)
		{
			Comment = "The decimal places of the rotational speed corresponding to MI8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1060", null)
		{
			Comment = "Mode Settings (0: Speed 1: Position 2: Torque 3: Return to Origin)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1061", null)
		{
			Comment = "485 COM1 Communication Time out Time (ms)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1062", null)
		{
			Comment = "Torque limit in speed mode"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1063", null)
		{
			Comment = "years"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1064", null)
		{
			Comment = "Week"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1065", null)
		{
			Comment = "month"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1066", null)
		{
			Comment = "day"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1067", null)
		{
			Comment = "when"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1068", null)
		{
			Comment = "points"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1069", null)
		{
			Comment = "seconds"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1070", null)
		{
			Comment = "CANOpen initial completed channel"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1071", null)
		{
			Comment = "CANOpen initial error channel"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1073", null)
		{
			Comment = "CANOpen disconnected channel"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1074", null)
		{
			Comment = "Main site error code"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1076", null)
		{
			Comment = "SDO error (primary index value)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1077", null)
		{
			Comment = "SDO error (sub-index value)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1078", null)
		{
			Comment = "SDO error (error code: Low word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1079", null)
		{
			Comment = "SDO error (error code High word)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1090", null)
		{
			Comment = "CANOpen synchronization cycle setting"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1091", null)
		{
			Comment = "The slave stations that need to be initialized during initialization"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1092", null)
		{
			Comment = "Start the delay before initialization"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1093", null)
		{
			Comment = "Disconnection detection time"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1094", null)
		{
			Comment = "The number of disconnection detection times"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1095", null)
		{
			Comment = "Selection of the data mapping area of PDO (Bit value: (0)D2XXX; (1)D13XX)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1096", null)
		{
			Comment = "Reserve"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1097", null)
		{
			Comment = "Instant Messaging Type Settings (PDO"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1098", null)
		{
			Comment = "Instant Messaging Type Settings (PDO)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1099", null)
		{
			Comment = "The delay time after the initialization is completed"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1100", null)
		{
			Comment = "Target frequency(不运转when为0)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1101", null)
		{
			Comment = "Target frequency"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1102", null)
		{
			Comment = "Reference frequency"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1103", null)
		{
			Comment = "Target position L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1104", null)
		{
			Comment = "Target position H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1105", null)
		{
			Comment = "Target torque"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1106", null)
		{
			Comment = "Reserve"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1107", null)
		{
			Comment = "Low Pi word"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1108", null)
		{
			Comment = "Pi High word"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1109", null)
		{
			Comment = "Random value"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1110", null)
		{
			Comment = "The number of internal node communications"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1115", null)
		{
			Comment = "Internal node synchronization cycle (ms"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1116", null)
		{
			Comment = "Internal Node errors (bit0 = Node 0, bit1 = Node 1,...) bit7 = Node 7)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1117", null)
		{
			Comment = "Internal Node online correspondence (bit0 = Node 0, bit1 = Node 1,...) bit7 = Node 7)"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1120", null)
		{
			Comment = "Control command for internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1121", null)
		{
			Comment = "The control mode of internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1122", null)
		{
			Comment = "Reference command L for internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1123", null)
		{
			Comment = "Reference command H for internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1126", null)
		{
			Comment = "The state of internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1127", null)
		{
			Comment = "The response information L of internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1128", null)
		{
			Comment = "The response information H of internal node 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1130", null)
		{
			Comment = "The control command of internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1131", null)
		{
			Comment = "The control mode of internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1132", null)
		{
			Comment = "Reference command L for internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1133", null)
		{
			Comment = "Reference command H for internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1136", null)
		{
			Comment = "The status of internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1137", null)
		{
			Comment = "The response information L of internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1138", null)
		{
			Comment = "The response information H of internal node 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1140", null)
		{
			Comment = "The control command of internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1141", null)
		{
			Comment = "The control mode of internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1142", null)
		{
			Comment = "Reference command L for internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1143", null)
		{
			Comment = "Reference command H for internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1146", null)
		{
			Comment = "The status of internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1147", null)
		{
			Comment = "The response information L of internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1148", null)
		{
			Comment = "The response information H of internal node 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1150", null)
		{
			Comment = "Control commands for internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1151", null)
		{
			Comment = "The control mode of internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1152", null)
		{
			Comment = "Reference command L for internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1153", null)
		{
			Comment = "Reference command H for internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1156", null)
		{
			Comment = "The status of internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1157", null)
		{
			Comment = "The response information L of internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1158", null)
		{
			Comment = "The response information H of internal node 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1160", null)
		{
			Comment = "The control command of internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1161", null)
		{
			Comment = "The control mode of internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1162", null)
		{
			Comment = "Reference command L for internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1163", null)
		{
			Comment = "Reference command H for internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1166", null)
		{
			Comment = "The status of internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1167", null)
		{
			Comment = "The response information L of internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1168", null)
		{
			Comment = "The response information H of internal node 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1170", null)
		{
			Comment = "The control command of internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1171", null)
		{
			Comment = "The control mode of internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1172", null)
		{
			Comment = "Reference command L for internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1173", null)
		{
			Comment = "Reference command H for internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1176", null)
		{
			Comment = "The status of internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1177", null)
		{
			Comment = "The response information L of internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1178", null)
		{
			Comment = "The response information H of internal node 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1180", null)
		{
			Comment = "Control commands for internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1181", null)
		{
			Comment = "The control mode of internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1182", null)
		{
			Comment = "Reference command L for internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1183", null)
		{
			Comment = "Reference command H for internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1186", null)
		{
			Comment = "The status of internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1187", null)
		{
			Comment = "The response information L of internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1188", null)
		{
			Comment = "The response information H of internal node 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1190", null)
		{
			Comment = "The control command of internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1191", null)
		{
			Comment = "The control mode of internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1192", null)
		{
			Comment = "Reference command L for internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1193", null)
		{
			Comment = "Reference command H for internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1196", null)
		{
			Comment = "The status of internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1197", null)
		{
			Comment = "The response information L of internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1198", null)
		{
			Comment = "The response information H of internal node 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1300", null)
		{
			Comment = "CANopen slave 1 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1301", null)
		{
			Comment = "CANopen slave 1 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1302", null)
		{
			Comment = "CANopen slave 1 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1303", null)
		{
			Comment = "CANopen slave 1 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1305", null)
		{
			Comment = "CANopen slave 1 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1306", null)
		{
			Comment = "CANopen slave 1 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1307", null)
		{
			Comment = "CANopen slave 1 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1308", null)
		{
			Comment = "CANopen slave 1 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1310", null)
		{
			Comment = "CANopen slave 1 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1311", null)
		{
			Comment = "CANopen slave 1 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1312", null)
		{
			Comment = "CANopen slave 1 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1313", null)
		{
			Comment = "CANopen slave 1 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1315", null)
		{
			Comment = "CANopen slave 1 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1316", null)
		{
			Comment = "CANopen slave 1 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1317", null)
		{
			Comment = "CANopen slave 1 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1318", null)
		{
			Comment = "CANopen slave 1 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1320", null)
		{
			Comment = "CANopen slave 1 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1321", null)
		{
			Comment = "CANopen slave 1 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1322", null)
		{
			Comment = "CANopen slave 1 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1323", null)
		{
			Comment = "CANopen slave 1 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1325", null)
		{
			Comment = "CANopen slave 1 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1326", null)
		{
			Comment = "CANopen slave 1 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1327", null)
		{
			Comment = "CANopen slave 1 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1328", null)
		{
			Comment = "CANopen slave 1 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1330", null)
		{
			Comment = "CANopen slave 1 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1331", null)
		{
			Comment = "CANopen slave 1 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1332", null)
		{
			Comment = "CANopen slave 1 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1333", null)
		{
			Comment = "CANopen slave 1 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1335", null)
		{
			Comment = "CANopen slave 1 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1336", null)
		{
			Comment = "CANopen slave 1 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1337", null)
		{
			Comment = "CANopen slave 1 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1338", null)
		{
			Comment = "CANopen slave 1 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1340", null)
		{
			Comment = "CANopen slave 2 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1341", null)
		{
			Comment = "CANopen slave 2 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1342", null)
		{
			Comment = "CANopen slave 2 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1343", null)
		{
			Comment = "CANopen slave 2 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1345", null)
		{
			Comment = "CANopen slave 2 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1346", null)
		{
			Comment = "CANopen slave 2 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1347", null)
		{
			Comment = "CANopen slave 2 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1348", null)
		{
			Comment = "CANopen slave 2 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1350", null)
		{
			Comment = "CANopen slave 2 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1351", null)
		{
			Comment = "CANopen slave 2 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1352", null)
		{
			Comment = "CANopen slave 2 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1353", null)
		{
			Comment = "CANopen slave 2 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1355", null)
		{
			Comment = "CANopen slave 2 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1356", null)
		{
			Comment = "CANopen slave 2 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1357", null)
		{
			Comment = "CANopen slave 2 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1358", null)
		{
			Comment = "CANopen slave 2 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1360", null)
		{
			Comment = "CANopen slave 2 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1361", null)
		{
			Comment = "CANopen slave 2 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1362", null)
		{
			Comment = "CANopen slave 2 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1363", null)
		{
			Comment = "CANopen slave 2 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1365", null)
		{
			Comment = "CANopen slave 2 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1366", null)
		{
			Comment = "CANopen slave 2 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1367", null)
		{
			Comment = "CANopen slave 2 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1368", null)
		{
			Comment = "CANopen slave 2 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1370", null)
		{
			Comment = "CANopen slave 2 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1371", null)
		{
			Comment = "CANopen slave 2 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1372", null)
		{
			Comment = "CANopen slave 2 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1373", null)
		{
			Comment = "CANopen slave 2 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1375", null)
		{
			Comment = "CANopen slave 2 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1376", null)
		{
			Comment = "CANopen slave 2 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1377", null)
		{
			Comment = "CANopen slave 2 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1378", null)
		{
			Comment = "CANopen slave 2 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1380", null)
		{
			Comment = "CANopen slave 3 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1381", null)
		{
			Comment = "CANopen slave 3 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1382", null)
		{
			Comment = "CANopen slave 3 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1383", null)
		{
			Comment = "CANopen slave 3 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1385", null)
		{
			Comment = "CANopen slave 3 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1386", null)
		{
			Comment = "CANopen slave 3 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1387", null)
		{
			Comment = "CANopen slave 3 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1388", null)
		{
			Comment = "CANopen slave 3 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1390", null)
		{
			Comment = "CANopen slave 3 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1391", null)
		{
			Comment = "CANopen slave 3 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1392", null)
		{
			Comment = "CANopen slave 3 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1393", null)
		{
			Comment = "CANopen slave 3 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1395", null)
		{
			Comment = "CANopen slave 3 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1396", null)
		{
			Comment = "CANopen slave 3 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1397", null)
		{
			Comment = "CANopen slave 3 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1398", null)
		{
			Comment = "CANopen slave 3 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1400", null)
		{
			Comment = "CANopen slave 3 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1401", null)
		{
			Comment = "CANopen slave 3 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1402", null)
		{
			Comment = "CANopen slave 3 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1403", null)
		{
			Comment = "CANopen slave 3 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1405", null)
		{
			Comment = "CANopen slave 3 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1406", null)
		{
			Comment = "CANopen slave 3 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1407", null)
		{
			Comment = "CANopen slave 3 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1408", null)
		{
			Comment = "CANopen slave 3 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1410", null)
		{
			Comment = "CANopen slave 3 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1411", null)
		{
			Comment = "CANopen slave 3 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1412", null)
		{
			Comment = "CANopen slave 3 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1413", null)
		{
			Comment = "CANopen slave 3 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1415", null)
		{
			Comment = "CANopen slave 3 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1416", null)
		{
			Comment = "CANopen slave 3 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1417", null)
		{
			Comment = "CANopen slave 3 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1418", null)
		{
			Comment = "CANopen slave 3 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1420", null)
		{
			Comment = "CANopen slave 4 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1421", null)
		{
			Comment = "CANopen slave 4 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1422", null)
		{
			Comment = "CANopen slave 4 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1423", null)
		{
			Comment = "CANopen slave 4 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1425", null)
		{
			Comment = "CANopen slave 4 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1426", null)
		{
			Comment = "CANopen slave 4 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1427", null)
		{
			Comment = "CANopen slave 4 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1428", null)
		{
			Comment = "CANopen slave 4 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1430", null)
		{
			Comment = "CANopen slave 4 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1431", null)
		{
			Comment = "CANopen slave 4 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1432", null)
		{
			Comment = "CANopen slave 4 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1433", null)
		{
			Comment = "CANopen slave 4 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1435", null)
		{
			Comment = "CANopen slave 4 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1436", null)
		{
			Comment = "CANopen slave 4 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1437", null)
		{
			Comment = "CANopen slave 4 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1438", null)
		{
			Comment = "CANopen slave 4 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1440", null)
		{
			Comment = "CANopen slave 4 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1441", null)
		{
			Comment = "CANopen slave 4 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1442", null)
		{
			Comment = "CANopen slave 4 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1443", null)
		{
			Comment = "CANopen slave 4 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1445", null)
		{
			Comment = "CANopen slave 4 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1446", null)
		{
			Comment = "CANopen slave 4 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1447", null)
		{
			Comment = "CANopen slave 4 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1448", null)
		{
			Comment = "CANopen slave 4 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1450", null)
		{
			Comment = "CANopen slave 4 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1451", null)
		{
			Comment = "CANopen slave 4 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1452", null)
		{
			Comment = "CANopen slave 4 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1453", null)
		{
			Comment = "CANopen slave 4 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1455", null)
		{
			Comment = "CANopen slave 4 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1456", null)
		{
			Comment = "CANopen slave 4 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1457", null)
		{
			Comment = "CANopen slave 4 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1458", null)
		{
			Comment = "CANopen slave 4 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1460", null)
		{
			Comment = "CANopen slave 5 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1461", null)
		{
			Comment = "CANopen slave 5 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1462", null)
		{
			Comment = "CANopen slave 5 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1463", null)
		{
			Comment = "CANopen slave 5 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1465", null)
		{
			Comment = "CANopen slave 5 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1466", null)
		{
			Comment = "CANopen slave 5 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1467", null)
		{
			Comment = "CANopen slave 5 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1468", null)
		{
			Comment = "CANopen slave 5 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1470", null)
		{
			Comment = "CANopen slave 5 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1471", null)
		{
			Comment = "CANopen slave 5 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1472", null)
		{
			Comment = "CANopen slave 5 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1473", null)
		{
			Comment = "CANopen slave 5 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1475", null)
		{
			Comment = "CANopen slave 5 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1476", null)
		{
			Comment = "CANopen slave 5 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1477", null)
		{
			Comment = "CANopen slave 5 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1478", null)
		{
			Comment = "CANopen slave 5 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1480", null)
		{
			Comment = "CANopen slave 5 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1481", null)
		{
			Comment = "CANopen slave 5 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1482", null)
		{
			Comment = "CANopen slave 5 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1483", null)
		{
			Comment = "CANopen slave 5 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1485", null)
		{
			Comment = "CANopen slave 5 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1486", null)
		{
			Comment = "CANopen slave 5 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1487", null)
		{
			Comment = "CANopen slave 5 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1488", null)
		{
			Comment = "CANopen slave 5 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1490", null)
		{
			Comment = "CANopen slave 5 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1491", null)
		{
			Comment = "CANopen slave 5 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1492", null)
		{
			Comment = "CANopen slave 5 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1493", null)
		{
			Comment = "CANopen slave 5 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1495", null)
		{
			Comment = "CANopen slave 5 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1496", null)
		{
			Comment = "CANopen slave 5 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1497", null)
		{
			Comment = "CANopen slave 5 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1498", null)
		{
			Comment = "CANopen slave 5 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1500", null)
		{
			Comment = "CANopen slave 6 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1501", null)
		{
			Comment = "CANopen slave 6 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1502", null)
		{
			Comment = "CANopen slave 6 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1503", null)
		{
			Comment = "CANopen slave 6 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1505", null)
		{
			Comment = "CANopen slave 6 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1506", null)
		{
			Comment = "CANopen slave 6 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1507", null)
		{
			Comment = "CANopen slave 6 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1508", null)
		{
			Comment = "CANopen slave 6 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1510", null)
		{
			Comment = "CANopen slave 6 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1511", null)
		{
			Comment = "CANopen slave 6 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1512", null)
		{
			Comment = "CANopen slave 6 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1513", null)
		{
			Comment = "CANopen slave 6 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1515", null)
		{
			Comment = "CANopen slave 6 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1516", null)
		{
			Comment = "CANopen slave 6 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1517", null)
		{
			Comment = "CANopen slave 6 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1518", null)
		{
			Comment = "CANopen slave 6 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1520", null)
		{
			Comment = "CANopen slave 6 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1521", null)
		{
			Comment = "CANopen slave 6 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1522", null)
		{
			Comment = "CANopen slave 6 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1523", null)
		{
			Comment = "CANopen slave 6 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1525", null)
		{
			Comment = "CANopen slave 6 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1526", null)
		{
			Comment = "CANopen slave 6 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1527", null)
		{
			Comment = "CANopen slave 6 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1528", null)
		{
			Comment = "CANopen slave 6 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1530", null)
		{
			Comment = "CANopen slave 6 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1531", null)
		{
			Comment = "CANopen slave 6 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1532", null)
		{
			Comment = "CANopen slave 6 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1533", null)
		{
			Comment = "CANopen slave 6 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1535", null)
		{
			Comment = "CANopen slave 6 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1536", null)
		{
			Comment = "CANopen slave 6 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1537", null)
		{
			Comment = "CANopen slave 6 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1538", null)
		{
			Comment = "CANopen slave 6 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1540", null)
		{
			Comment = "CANopen slave 7 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1541", null)
		{
			Comment = "CANopen slave 7 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1542", null)
		{
			Comment = "CANopen slave 7 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1543", null)
		{
			Comment = "CANopen slave 7 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1545", null)
		{
			Comment = "CANopen slave 7 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1546", null)
		{
			Comment = "CANopen slave station 7 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1547", null)
		{
			Comment = "CANopen slave station 7 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1548", null)
		{
			Comment = "CANopen slave station 7 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1550", null)
		{
			Comment = "CANopen slave station 7 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1551", null)
		{
			Comment = "CANopen slave station 7 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1552", null)
		{
			Comment = "CANopen slave station 7 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1553", null)
		{
			Comment = "CANopen slave station 7 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1555", null)
		{
			Comment = "CANopen slave station 7 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1556", null)
		{
			Comment = "CANopen slave station 7 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1557", null)
		{
			Comment = "CANopen slave station 7 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1558", null)
		{
			Comment = "CANopen slave station 7 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1560", null)
		{
			Comment = "CANopen slave 7 PDORX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1561", null)
		{
			Comment = "CANopen slave 7 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1562", null)
		{
			Comment = "CANopen slave 7 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1563", null)
		{
			Comment = "CANopen Slave 7 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1565", null)
		{
			Comment = "CANopen slave 7 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1566", null)
		{
			Comment = "CANopen Slave 7 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1567", null)
		{
			Comment = "CANopen slave station 7 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1568", null)
		{
			Comment = "CANopen slave station 7 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1570", null)
		{
			Comment = "CANopen slave 7 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1571", null)
		{
			Comment = "CANopen slave station 7 PDORX 3 Byte 3 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1572", null)
		{
			Comment = "CANopen slave station 7 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1573", null)
		{
			Comment = "CANopen slave station 7 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1575", null)
		{
			Comment = "CANopen slave station 7 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1576", null)
		{
			Comment = "CANopen slave 7 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1577", null)
		{
			Comment = "CANopen slave station 7 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1578", null)
		{
			Comment = "CANopen slave 7 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1580", null)
		{
			Comment = "CANopen slave 8 PDOTX 1 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1581", null)
		{
			Comment = "CANopen slave 8 PDOTX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1582", null)
		{
			Comment = "CANopen slave 8 PDOTX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1583", null)
		{
			Comment = "CANopen slave 8 PDOTX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1585", null)
		{
			Comment = "CANopen slave 8 PDOTX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1586", null)
		{
			Comment = "CANopen slave 8 PDOTX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1587", null)
		{
			Comment = "CANopen slave 8 PDOTX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1588", null)
		{
			Comment = "CANopen slave 8 PDOTX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1590", null)
		{
			Comment = "CANopen slave 8 PDOTX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1591", null)
		{
			Comment = "CANopen slave 8 PDOTX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1592", null)
		{
			Comment = "CANopen slave 8 PDOTX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1593", null)
		{
			Comment = "CANopen slave station 8 PDOTX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1595", null)
		{
			Comment = "CANopen slave 8 PDOTX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1596", null)
		{
			Comment = "CANopen slave 8 PDOTX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1597", null)
		{
			Comment = "CANopen slave 8 PDOTX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1598", null)
		{
			Comment = "CANopen slave 8 PDOTX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1600", null)
		{
			Comment = "CANopen slave 8 PDORX 1 Byte 1 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1601", null)
		{
			Comment = "CANopen slave 8 PDORX 1 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1602", null)
		{
			Comment = "CANopen slave 8 PDORX 1 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1603", null)
		{
			Comment = "CANopen slave 8 PDORX 1 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1605", null)
		{
			Comment = "CANopen slave 8 PDORX 2 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1606", null)
		{
			Comment = "CANopen slave 8 PDORX 2 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1607", null)
		{
			Comment = "CANopen slave 8 PDORX 2 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1608", null)
		{
			Comment = "CANopen slave 8 PDORX 2 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1610", null)
		{
			Comment = "CANopen slave 8 PDORX 3 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1611", null)
		{
			Comment = "CANopen slave 8 PDORX 3 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1612", null)
		{
			Comment = "CANopen slave 8 PDORX 3 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1613", null)
		{
			Comment = "CANopen slave 8 PDORX 3 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1615", null)
		{
			Comment = "CANopen slave 8 PDORX 4 Byte 1, 0"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1616", null)
		{
			Comment = "CANopen slave 8 PDORX 4 Byte 3, 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1617", null)
		{
			Comment = "CANopen slave 8 PDORX 4 Byte 5, 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D1618", null)
		{
			Comment = "CANopen slave 8 PDORX 4 Byte 7, 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2000", null)
		{
			Comment = "The station number from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2001", null)
		{
			Comment = "The types from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2002", null)
		{
			Comment = "From the manufacturer code (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2003", null)
		{
			Comment = "From the manufacturer code (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2004", null)
		{
			Comment = "From the product code (L) of the manufacturer at Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2005", null)
		{
			Comment = "The product code (H) from the manufacturer of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2006", null)
		{
			Comment = "The handling method for communication disconnection from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2007", null)
		{
			Comment = "Error code from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2008", null)
		{
			Comment = "The control word from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2009", null)
		{
			Comment = "The status word from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2010", null)
		{
			Comment = "Control mode from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2011", null)
		{
			Comment = "From the actual mode of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2012", null)
		{
			Comment = "The target speed from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2013", null)
		{
			Comment = "The actual speed from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2014", null)
		{
			Comment = "The error velocity from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2015", null)
		{
			Comment = "The acceleration time from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2016", null)
		{
			Comment = "The deceleration time from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2017", null)
		{
			Comment = "The target torque from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2018", null)
		{
			Comment = "From the actual torque of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2019", null)
		{
			Comment = "The actual current from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2020", null)
		{
			Comment = "From the target position (L) of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2021", null)
		{
			Comment = "From the target position (H) of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2022", null)
		{
			Comment = "From the actual position (L) of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2023", null)
		{
			Comment = "From the actual position (H) of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2024", null)
		{
			Comment = "Speed chart (L) from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2025", null)
		{
			Comment = "Speed chart (H) from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2026", null)
		{
			Comment = "From the MI state of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2027", null)
		{
			Comment = "From the MO Settings of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2028", null)
		{
			Comment = "From the AI1 state of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2029", null)
		{
			Comment = "Set from AI2 of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2030", null)
		{
			Comment = "Set from AI3 of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2031", null)
		{
			Comment = "From the AO1 state of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2032", null)
		{
			Comment = "Set from AO2 of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2033", null)
		{
			Comment = "Set from AO3 at Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2034", null)
		{
			Comment = "From the real-time teleportation Settings of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2035", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2036", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2037", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2038", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2039", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2040", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2041", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2042", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2043", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2044", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2045", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2046", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2047", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2048", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2049", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2050", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2051", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2052", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2053", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2054", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2055", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2056", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2057", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2058", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2059", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2060", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2061", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2062", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2063", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2064", null)
		{
			Comment = "The corresponding address 3(H) from the transmission channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2065", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2066", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2067", null)
		{
			Comment = "From the instant receiving Settings of Station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2068", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2069", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2070", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2071", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2072", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2073", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2074", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2075", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 1 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2076", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2077", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2078", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2079", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2080", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2081", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2082", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2083", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 2 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2084", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2085", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2086", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2087", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2088", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2089", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2090", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2091", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 3 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2092", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2093", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2094", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2095", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2096", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2097", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2098", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2099", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 4 of station 1"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2100", null)
		{
			Comment = "The station number from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2101", null)
		{
			Comment = "The types from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2102", null)
		{
			Comment = "From the manufacturer code (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2103", null)
		{
			Comment = "From the manufacturer code (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2104", null)
		{
			Comment = "The product code (L) from the manufacturer of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2105", null)
		{
			Comment = "The product code (H) from the manufacturer of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2106", null)
		{
			Comment = "The handling method of the error from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2107", null)
		{
			Comment = "The error code from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2108", null)
		{
			Comment = "The control word from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2109", null)
		{
			Comment = "The status word from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2110", null)
		{
			Comment = "Control mode from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2111", null)
		{
			Comment = "From the actual mode of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2112", null)
		{
			Comment = "The target speed from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2113", null)
		{
			Comment = "The actual speed from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2114", null)
		{
			Comment = "The error velocity from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2115", null)
		{
			Comment = "The acceleration time from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2116", null)
		{
			Comment = "The deceleration time from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2117", null)
		{
			Comment = "The target torque from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2118", null)
		{
			Comment = "The actual torque from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2119", null)
		{
			Comment = "The actual current from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2120", null)
		{
			Comment = "From the target position (L) of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2121", null)
		{
			Comment = "From the target position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2122", null)
		{
			Comment = "From the actual position (L) of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2123", null)
		{
			Comment = "From the actual position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2124", null)
		{
			Comment = "Speed chart (L) from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2125", null)
		{
			Comment = "Speed chart (H) from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2126", null)
		{
			Comment = "From the MI state of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2127", null)
		{
			Comment = "From the MO Settings at Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2128", null)
		{
			Comment = "From the AI1 status of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2129", null)
		{
			Comment = "Set it up from AI2 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2130", null)
		{
			Comment = "Set it up from AI3 at Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2131", null)
		{
			Comment = "From the AO1 state of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2132", null)
		{
			Comment = "Set up from AO2 at Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2133", null)
		{
			Comment = "Set up from AO3 at Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2134", null)
		{
			Comment = "From the real-time teleportation Settings of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2135", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2136", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2137", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2138", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2139", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2140", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2141", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2142", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2143", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2144", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2145", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2146", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2147", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2148", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2149", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2150", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2151", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2152", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2153", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2154", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2155", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2156", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2157", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2158", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2159", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2160", null)
		{
			Comment = "The corresponding address 1(H) from the transmission channel 4 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2161", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2162", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2163", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2164", null)
		{
			Comment = "The corresponding address 3(H) from the transmission channel 4 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2165", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2166", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2167", null)
		{
			Comment = "The instant reception Settings from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2168", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2169", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2170", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2171", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2172", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2173", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2174", null)
		{
			Comment = "The corresponding address 4(L) of the receiving channel 1 from Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2175", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 1 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2176", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2177", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2178", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2179", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2180", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2181", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2182", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2183", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 2 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2184", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2185", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2186", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2187", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2188", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2189", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of Station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2190", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2191", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2192", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2193", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2194", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2195", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2196", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2197", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2198", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2199", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 4 of station 2"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2200", null)
		{
			Comment = "The station number from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2201", null)
		{
			Comment = "The types of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2202", null)
		{
			Comment = "From the manufacturer code (L) of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2203", null)
		{
			Comment = "From the manufacturer code (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2204", null)
		{
			Comment = "From the product code (L) of the manufacturer on Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2205", null)
		{
			Comment = "The product code (H) from the manufacturer of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2206", null)
		{
			Comment = "The handling method of the error from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2207", null)
		{
			Comment = "The error code from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2208", null)
		{
			Comment = "Control words from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2209", null)
		{
			Comment = "The status word from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2210", null)
		{
			Comment = "Control mode from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2211", null)
		{
			Comment = "From the actual mode of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2212", null)
		{
			Comment = "The target speed from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2213", null)
		{
			Comment = "The actual speed from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2214", null)
		{
			Comment = "The error velocity from station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2215", null)
		{
			Comment = "The acceleration time from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2216", null)
		{
			Comment = "The deceleration time from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2217", null)
		{
			Comment = "The target torque from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2218", null)
		{
			Comment = "The actual torque from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2219", null)
		{
			Comment = "The actual current from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2220", null)
		{
			Comment = "From the target position (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2221", null)
		{
			Comment = "From the target position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2222", null)
		{
			Comment = "From the actual position (L) of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2223", null)
		{
			Comment = "From the actual position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2224", null)
		{
			Comment = "Speed chart (L) from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2225", null)
		{
			Comment = "Speed chart (H) from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2226", null)
		{
			Comment = "From the MI state of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2227", null)
		{
			Comment = "From the MO Settings of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2228", null)
		{
			Comment = "From the AI1 state of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2229", null)
		{
			Comment = "Set it up from AI2 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2230", null)
		{
			Comment = "From the AI3 Settings of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2231", null)
		{
			Comment = "From the AO1 state of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2232", null)
		{
			Comment = "Set from AO2 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2233", null)
		{
			Comment = "Set up from AO3 at Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2234", null)
		{
			Comment = "From the real-time teleportation Settings of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2235", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2236", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2237", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2238", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2239", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2240", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2241", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2242", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2243", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2244", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2245", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2246", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2247", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2248", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2249", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2250", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2251", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2252", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2253", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2254", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2255", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2256", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2257", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2258", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2259", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2260", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2261", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2262", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2263", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2264", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2265", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2266", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2267", null)
		{
			Comment = "From the instant reception Settings of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2268", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2269", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2270", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2271", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2272", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2273", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2274", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2275", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 1 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2276", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2277", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2278", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2279", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2280", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2281", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2282", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2283", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 2 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2284", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2285", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2286", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2287", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2288", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2289", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 3 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2290", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2291", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 3 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2292", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 4 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2293", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2294", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 4 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2295", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 4 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2296", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 4 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2297", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 4 of Station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2298", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 4 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2299", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 4 of station 3"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2300", null)
		{
			Comment = "The station number from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2301", null)
		{
			Comment = "The types from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2302", null)
		{
			Comment = "From the manufacturer code (L) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2303", null)
		{
			Comment = "From the manufacturer code (H) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2304", null)
		{
			Comment = "From the product code (L) of the manufacturer on Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2305", null)
		{
			Comment = "The product code (H) from the manufacturer on Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2306", null)
		{
			Comment = "The handling method of the error from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2307", null)
		{
			Comment = "The error code from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2308", null)
		{
			Comment = "Control words from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2309", null)
		{
			Comment = "The status word from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2310", null)
		{
			Comment = "Control mode from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2311", null)
		{
			Comment = "From the actual mode of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2312", null)
		{
			Comment = "The target speed from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2313", null)
		{
			Comment = "The actual speed from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2314", null)
		{
			Comment = "The error velocity from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2315", null)
		{
			Comment = "The acceleration time from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2316", null)
		{
			Comment = "The deceleration time from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2317", null)
		{
			Comment = "The target torque from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2318", null)
		{
			Comment = "The actual torque from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2319", null)
		{
			Comment = "The actual current from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2320", null)
		{
			Comment = "From the target position (L) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2321", null)
		{
			Comment = "From the target position (H) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2322", null)
		{
			Comment = "From the actual position (L) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2323", null)
		{
			Comment = "From the actual position (H) of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2324", null)
		{
			Comment = "Speed chart (L) from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2325", null)
		{
			Comment = "Speed chart (H) from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2326", null)
		{
			Comment = "From the MI state of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2327", null)
		{
			Comment = "From the MO Settings at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2328", null)
		{
			Comment = "From the AI1 state of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2329", null)
		{
			Comment = "Set it up from AI2 at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2330", null)
		{
			Comment = "Set from AI3 at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2331", null)
		{
			Comment = "From the AO1 state of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2332", null)
		{
			Comment = "Set from AO2 at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2333", null)
		{
			Comment = "Set from AO3 at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2334", null)
		{
			Comment = "From the real-time teleportation Settings of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2335", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2336", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2337", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2338", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2339", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2340", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2341", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2342", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2343", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2344", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2345", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2346", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2347", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2348", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2349", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2350", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2351", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2352", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2353", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2354", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2355", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2356", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2357", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2358", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2359", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2360", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2361", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2362", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2363", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2364", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2365", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2366", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2367", null)
		{
			Comment = "From the instant reception Settings at Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2368", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2369", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2370", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2371", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2372", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2373", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2374", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2375", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 1 of Station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2376", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2377", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2378", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2379", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2380", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2381", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2382", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2383", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 2 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2384", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2385", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2386", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2387", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2388", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2389", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2390", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2391", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2392", null)
		{
			Comment = "From the corresponding address 1(L) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2393", null)
		{
			Comment = "From the corresponding address 1(H) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2394", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2395", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2396", null)
		{
			Comment = "From the corresponding address 3(L) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2397", null)
		{
			Comment = "From the corresponding address 3(H) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2398", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2399", null)
		{
			Comment = "From the corresponding address 4(H) of the receiving channel 4 of station 4"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2400", null)
		{
			Comment = "The station number from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2401", null)
		{
			Comment = "From the types of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2402", null)
		{
			Comment = "From the manufacturer code (L) of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2403", null)
		{
			Comment = "From the manufacturer code (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2404", null)
		{
			Comment = "From the product code (L) of the manufacturer on Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2405", null)
		{
			Comment = "The product code (H) from the manufacturer of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2406", null)
		{
			Comment = "The handling method of the error from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2407", null)
		{
			Comment = "The error code from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2408", null)
		{
			Comment = "Control words from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2409", null)
		{
			Comment = "The status word from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2410", null)
		{
			Comment = "Control mode from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2411", null)
		{
			Comment = "From the actual mode of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2412", null)
		{
			Comment = "The target speed from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2413", null)
		{
			Comment = "The actual speed from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2414", null)
		{
			Comment = "The error speed from station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2415", null)
		{
			Comment = "The acceleration time from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2416", null)
		{
			Comment = "The deceleration time from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2417", null)
		{
			Comment = "The target torque from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2418", null)
		{
			Comment = "The actual torque from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2419", null)
		{
			Comment = "The actual current from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2420", null)
		{
			Comment = "From the target position (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2421", null)
		{
			Comment = "From the target position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2422", null)
		{
			Comment = "From the actual position (L) of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2423", null)
		{
			Comment = "From the actual position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2424", null)
		{
			Comment = "Speed chart (L) from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2425", null)
		{
			Comment = "Speed chart (H) from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2426", null)
		{
			Comment = "From the MI state of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2427", null)
		{
			Comment = "From the MO Settings of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2428", null)
		{
			Comment = "From the AI1 state of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2429", null)
		{
			Comment = "Set it up from AI2 at Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2430", null)
		{
			Comment = "Set it up from AI3 at Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2431", null)
		{
			Comment = "From the AO1 state of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2432", null)
		{
			Comment = "Set it up from AO2 at Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2433", null)
		{
			Comment = "Set up from AO3 at Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2434", null)
		{
			Comment = "From the real-time teleportation Settings of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2435", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2436", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2437", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2438", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2439", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2440", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2441", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2442", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2443", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2444", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2445", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2446", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2447", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2448", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2449", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2450", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2451", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2452", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2453", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2454", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2455", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2456", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2457", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2458", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2459", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2460", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2461", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2462", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2463", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2464", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2465", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2466", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2467", null)
		{
			Comment = "From the instant reception Settings of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2468", null)
		{
			Comment = "The corresponding address 1(L) of the receiving channel 1 from Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2469", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2470", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2471", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2472", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2473", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2474", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2475", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 1 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2476", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2477", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2478", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2479", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2480", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2481", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2482", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2483", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 2 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2484", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2485", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2486", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2487", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2488", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2489", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of Station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2490", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2491", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2492", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2493", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2494", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2495", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2496", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2497", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2498", null)
		{
			Comment = "From the corresponding address 4(L) of the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2499", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 4 of station 5"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2500", null)
		{
			Comment = "The station number from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2501", null)
		{
			Comment = "The types of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2502", null)
		{
			Comment = "From the manufacturer code (L) of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2503", null)
		{
			Comment = "The manufacturer code (H) from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2504", null)
		{
			Comment = "From the product code (L) of the manufacturer at Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2505", null)
		{
			Comment = "The product code (H) of the manufacturer from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2506", null)
		{
			Comment = "The handling method of the error from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2507", null)
		{
			Comment = "Error code from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2508", null)
		{
			Comment = "Control words from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2509", null)
		{
			Comment = "The status word from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2510", null)
		{
			Comment = "Control mode from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2511", null)
		{
			Comment = "From the actual mode of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2512", null)
		{
			Comment = "The target speed from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2513", null)
		{
			Comment = "The actual speed from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2514", null)
		{
			Comment = "The error speed from station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2515", null)
		{
			Comment = "The acceleration time from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2516", null)
		{
			Comment = "The deceleration time from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2517", null)
		{
			Comment = "The target torque from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2518", null)
		{
			Comment = "The actual torque from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2519", null)
		{
			Comment = "The actual current from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2520", null)
		{
			Comment = "From the target position (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2521", null)
		{
			Comment = "From the target position (H) of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2522", null)
		{
			Comment = "From the actual position (L) of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2523", null)
		{
			Comment = "From the actual position (H) of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2524", null)
		{
			Comment = "Speed chart (L) from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2525", null)
		{
			Comment = "Speed chart (H) from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2526", null)
		{
			Comment = "From the MI state of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2527", null)
		{
			Comment = "From the MO Settings of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2528", null)
		{
			Comment = "From the AI1 state of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2529", null)
		{
			Comment = "Set from AI2 at Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2530", null)
		{
			Comment = "Set it up from AI3 at Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2531", null)
		{
			Comment = "From the AO1 state of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2532", null)
		{
			Comment = "Set from AO2 at Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2533", null)
		{
			Comment = "Set from AO3 at Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2534", null)
		{
			Comment = "From the real-time teleportation Settings of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2535", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2536", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2537", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2538", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2539", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2540", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2541", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2542", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2543", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2544", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2545", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2546", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2547", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2548", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2549", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2550", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2551", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2552", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2553", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2554", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2555", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2556", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2557", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2558", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2559", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2560", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2561", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2562", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2563", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2564", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2565", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2566", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2567", null)
		{
			Comment = "The instant reception Settings from Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2568", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 1 of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2569", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 1 of Station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2570", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2571", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2572", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2573", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2574", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2575", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 1 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2576", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2577", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2578", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2579", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2580", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2581", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2582", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2583", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 2 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2584", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2585", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2586", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2587", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2588", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2589", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2590", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2591", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2592", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2593", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2594", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2595", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2596", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2597", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2598", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2599", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 4 of station 6"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2600", null)
		{
			Comment = "The station number from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2601", null)
		{
			Comment = "The types from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2602", null)
		{
			Comment = "From the manufacturer code (L) of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2603", null)
		{
			Comment = "From the manufacturer code (H) of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2604", null)
		{
			Comment = "From the product code (L) of the manufacturer on Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2605", null)
		{
			Comment = "The product code (H) of the manufacturer from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2606", null)
		{
			Comment = "The handling method of the error from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2607", null)
		{
			Comment = "The error code from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2608", null)
		{
			Comment = "Control words from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2609", null)
		{
			Comment = "The status word from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2610", null)
		{
			Comment = "Control mode from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2611", null)
		{
			Comment = "From the actual mode of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2612", null)
		{
			Comment = "The target speed from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2613", null)
		{
			Comment = "The actual speed from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2614", null)
		{
			Comment = "The error speed from station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2615", null)
		{
			Comment = "The acceleration time from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2616", null)
		{
			Comment = "The deceleration time from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2617", null)
		{
			Comment = "The target torque from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2618", null)
		{
			Comment = "The actual torque from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2619", null)
		{
			Comment = "The actual current from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2620", null)
		{
			Comment = "From the target position (L"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2621", null)
		{
			Comment = "From the target position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2622", null)
		{
			Comment = "From the actual position (L) of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2623", null)
		{
			Comment = "From the actual position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2624", null)
		{
			Comment = "Speed chart (L) from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2625", null)
		{
			Comment = "Speed chart (H) from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2626", null)
		{
			Comment = "From the MI state of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2627", null)
		{
			Comment = "From the MO Settings of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2628", null)
		{
			Comment = "From the AI1 status of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2629", null)
		{
			Comment = "Set it up from AI2 at Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2630", null)
		{
			Comment = "Set it up from AI3 at Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2631", null)
		{
			Comment = "From the AO1 status of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2632", null)
		{
			Comment = "Set up from AO2 at Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2633", null)
		{
			Comment = "Set it up from AO3 at Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2634", null)
		{
			Comment = "From the real-time teleportation Settings of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2635", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2636", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2637", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2638", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2639", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2640", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2641", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2642", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2643", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2644", null)
		{
			Comment = "The corresponding address 1(H) from the transmission channel 2 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2645", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2646", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2647", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2648", null)
		{
			Comment = "The corresponding address 3(H) from the transmission channel 2 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2649", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2650", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 2 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2651", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2652", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2653", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2654", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2655", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2656", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2657", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2658", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2659", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2660", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2661", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2662", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2663", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2664", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2665", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2666", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2667", null)
		{
			Comment = "From the instant reception Settings of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2668", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2669", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2670", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2671", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2672", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2673", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2674", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2675", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 1 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2676", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2677", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2678", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2679", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 2 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2680", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2681", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2682", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2683", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 2 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2684", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2685", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2686", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 3 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2687", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 3 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2688", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 3 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2689", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2690", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 3 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2691", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2692", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2693", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2694", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2695", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2696", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2697", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2698", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 4 of station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2699", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 4 of Station 7"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2700", null)
		{
			Comment = "The station number from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2701", null)
		{
			Comment = "The types from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2702", null)
		{
			Comment = "From the manufacturer code (L) of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2703", null)
		{
			Comment = "From the manufacturer code (H) of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2704", null)
		{
			Comment = "From the product code (L) of the manufacturer on Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2705", null)
		{
			Comment = "The product code (H) from the manufacturer of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2706", null)
		{
			Comment = "The handling method of the error from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2707", null)
		{
			Comment = "The error code from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2708", null)
		{
			Comment = "The control word from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2709", null)
		{
			Comment = "The status word from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2710", null)
		{
			Comment = "Control mode from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2711", null)
		{
			Comment = "From the actual mode of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2712", null)
		{
			Comment = "The target speed from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2713", null)
		{
			Comment = "The actual speed from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2714", null)
		{
			Comment = "The error velocity from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2715", null)
		{
			Comment = "The acceleration time from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2716", null)
		{
			Comment = "The deceleration time from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2717", null)
		{
			Comment = "The target torque from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2718", null)
		{
			Comment = "The actual torque from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2719", null)
		{
			Comment = "The actual current from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2720", null)
		{
			Comment = "From the target position (L) of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2721", null)
		{
			Comment = "From the target position (H"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2722", null)
		{
			Comment = "From the actual position (L) of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2723", null)
		{
			Comment = "From the actual position (H) of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2724", null)
		{
			Comment = "Speed chart (L) from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2725", null)
		{
			Comment = "Speed chart (H) from Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2726", null)
		{
			Comment = "From the MI state of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2727", null)
		{
			Comment = "From the MO Settings of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2728", null)
		{
			Comment = "From the AI1 state of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2729", null)
		{
			Comment = "Set it up from AI2 at Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2730", null)
		{
			Comment = "Set it up from AI3 at Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2731", null)
		{
			Comment = "From the AO1 state of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2732", null)
		{
			Comment = "Set from AO2 at Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2733", null)
		{
			Comment = "Set up from AO3 at Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2734", null)
		{
			Comment = "From the real-time teleportation Settings of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2735", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2736", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2737", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2738", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2739", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2740", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2741", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2742", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 1 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2743", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 2 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2744", null)
		{
			Comment = "The corresponding address 1(H) from the transmission channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2745", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 2 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2746", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 2 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2747", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 2 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2748", null)
		{
			Comment = "The corresponding address 3(H) from the transmission channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2749", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 2 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2750", null)
		{
			Comment = "The corresponding address 4(H) from the transmission channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2751", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2752", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2753", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2754", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2755", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2756", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2757", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2758", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 3 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2759", null)
		{
			Comment = "The corresponding address 1(L) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2760", null)
		{
			Comment = "The corresponding address 1(H) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2761", null)
		{
			Comment = "The corresponding address 2(L) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2762", null)
		{
			Comment = "The corresponding address 2(H) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2763", null)
		{
			Comment = "The corresponding address 3(L) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2764", null)
		{
			Comment = "The corresponding address 3(H) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2765", null)
		{
			Comment = "The corresponding address 4(L) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2766", null)
		{
			Comment = "The corresponding address 4(H) of the transmission channel 4 from station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2767", null)
		{
			Comment = "From the instant reception Settings of Station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2768", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2769", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2770", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2771", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2772", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2773", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2774", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2775", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 1 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2776", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2777", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2778", null)
		{
			Comment = "From the corresponding address 2(L) of the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2779", null)
		{
			Comment = "From the corresponding address 2(H) of the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2780", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2781", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2782", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2783", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 2 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2784", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2785", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2786", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2787", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2788", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2789", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2790", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2791", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 3 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2792", null)
		{
			Comment = "The corresponding address 1(L) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2793", null)
		{
			Comment = "The corresponding address 1(H) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2794", null)
		{
			Comment = "The corresponding address 2(L) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2795", null)
		{
			Comment = "The corresponding address 2(H) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2796", null)
		{
			Comment = "The corresponding address 3(L) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2797", null)
		{
			Comment = "The corresponding address 3(H) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2798", null)
		{
			Comment = "The corresponding address 4(L) from the receiving channel 4 of station 8"
		});
		S_VFD_List.Add(new DeltaSystemElement("D2799", null)
		{
			Comment = "The corresponding address 4(H) from the receiving channel 4 of station 8"
		});
		deltaDevice = new DeltaDevice("VFD E TYPE");
		foreach (DeltaRange r_VFD_ in R_VFD_List)
		{
			deltaDevice.Ranges.Add(r_VFD_);
		}
		foreach (DeltaSystemElement s_VFD_ in S_VFD_List)
		{
			deltaDevice.SystemElements.Add(s_VFD_.Source, s_VFD_);
		}
		List.Add(deltaDevice);
		List.Add(deltaDevice.Clone("VFD-C200"));
		List.Add(deltaDevice.Clone("VFD-C2000"));
		List.Add(deltaDevice.Clone("VFD-CP2000"));
	}

	public DeltaDevice(string _name)
	{
		name = _name;
		ranges = new List<DeltaRange>();
		systemelements = new Dictionary<string, DeltaSystemElement>();
	}

	public override string ToString()
	{
		return name;
	}

	public DeltaDevice Clone(string _newname)
	{
		DeltaDevice deltaDevice = new DeltaDevice(_newname);
		foreach (DeltaRange range in ranges)
		{
			deltaDevice.Ranges.Add(range);
		}
		foreach (DeltaSystemElement value in systemelements.Values)
		{
			deltaDevice.SystemElements.Add(value.Source, value);
		}
		return deltaDevice;
	}
}
