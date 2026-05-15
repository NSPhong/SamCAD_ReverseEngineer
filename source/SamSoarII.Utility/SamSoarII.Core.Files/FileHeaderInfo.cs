using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Core.Files;

public class FileHeaderInfo
{
	private List<IFileHeader> items;

	public FileHeaderTypes HeaderType => (items.Count() > 0) ? items.First().HeaderType : FileHeaderTypes.Null;

	public FileHeaderInfo(FileHeaderTypes type)
	{
		items = new List<IFileHeader>();
		switch (type)
		{
		case FileHeaderTypes.TypeLP:
			items.Add(new TypeHeader());
			break;
		case FileHeaderTypes.FileHeaderEnumerable:
			items.Add(new FileHeaderEnumerable());
			break;
		case FileHeaderTypes.File:
			items.Add(new FileHeader());
			break;
		case FileHeaderTypes.Project:
			items.Add(new ProjectHeader());
			items.Add(new ProjectHeader_Version_2_2_30());
			items.Add(new ProjectHeader_Version_2_2_46());
			items.Add(new ProjectHeader_Version_2_2_50());
			items.Add(new ProjectHeader_Version_2_2_56());
			items.Add(new ProjectHeader_Version_2_3_9());
			items.Add(new ProjectHeader_Version_2_3_11());
			items.Add(new ProjectHeader_Version_2_3_12());
			break;
		case FileHeaderTypes.LadderDiagram:
			items.Add(new LadderDiagramHeader());
			items.Add(new LadderDiagramHeader_Version_2_2_40());
			items.Add(new LadderDiagramHeader_Version_2_3_9());
			items.Add(new LadderDiagramHeader_Version_2_3_12());
			items.Add(new LadderDiagramHeader_Version_2_3_14());
			items.Add(new LadderDiagramHeader_Version_2_3_23());
			break;
		case FileHeaderTypes.LadderNetwork:
			items.Add(new LadderNetworkHeader());
			items.Add(new LadderNetworkHeader_Version_2_3_14());
			break;
		case FileHeaderTypes.InstructionNetwork:
			items.Add(new InstructionNetworkHeader());
			break;
		case FileHeaderTypes.LadderUnit:
			items.Add(new LadderUnitHeader());
			break;
		case FileHeaderTypes.LadderLine:
			items.Add(new LadderLineHeader());
			break;
		case FileHeaderTypes.FuncBlock:
			items.Add(new FuncBlockHeader());
			items.Add(new FuncBlockHeader_Version_2_2_41());
			items.Add(new FuncBlockHeader_Version_2_2_47());
			break;
		case FileHeaderTypes.Func:
			items.Add(new FuncHeader());
			break;
		case FileHeaderTypes.Modbus:
			items.Add(new ModbusHeader());
			items.Add(new ModbusHeader_Version_2_2_47());
			break;
		case FileHeaderTypes.ModbusItem:
			items.Add(new ModbusItemHeader());
			items.Add(new ModbusItemHeader_Version_2_3_24());
			break;
		case FileHeaderTypes.ValueManager:
			items.Add(new ValueManagerHeader());
			break;
		case FileHeaderTypes.Value:
			items.Add(new ValueHeader());
			break;
		case FileHeaderTypes.Monitor:
			items.Add(new MonitorHeader());
			break;
		case FileHeaderTypes.MonitorTable:
			items.Add(new MonitorTableHeader());
			break;
		case FileHeaderTypes.MonitorItem:
			items.Add(new MonitorItemHeader());
			break;
		case FileHeaderTypes.PLSBlock:
			items.Add(new PLSBlockHeader());
			break;
		case FileHeaderTypes.PolylineSystem:
			items.Add(new PolylineSystemHeader());
			break;
		case FileHeaderTypes.PolylineAxis:
			items.Add(new PolylineAxisHeader());
			break;
		case FileHeaderTypes.ValueBrpo:
			items.Add(new ValueBrpoHeader());
			break;
		case FileHeaderTypes.ValueBrpoElement:
			items.Add(new ValueBrpoElementHeader());
			break;
		case FileHeaderTypes.SFCLadder:
			items.Add(new SFCLadderHeader());
			break;
		case FileHeaderTypes.SFCUnit:
			items.Add(new SFCUnitHeader());
			items.Add(new SFCUnitHeader_Version_2_2_59());
			items.Add(new SFCUnitHeader_Version_2_3_2());
			items.Add(new SFCUnitHeader_Version_2_3_4());
			break;
		case FileHeaderTypes.SFCBranch:
			items.Add(new SFCBranchHeader());
			break;
		case FileHeaderTypes.SFCAndBranch:
			items.Add(new SFCAndBranchHeader());
			break;
		case FileHeaderTypes.SFCOrBranch:
			items.Add(new SFCOrBranchHeader());
			items.Add(new SFCOrBranchHeader_Version_2_3_2());
			break;
		case FileHeaderTypes.SFCMerge:
			items.Add(new SFCMergeHeader());
			break;
		case FileHeaderTypes.SFCAndMerge:
			items.Add(new SFCAndMergeHeader());
			break;
		case FileHeaderTypes.SFCOrMerge:
			items.Add(new SFCOrMergeHeader());
			items.Add(new SFCOrMergeHeader_Version_2_3_2());
			break;
		case FileHeaderTypes.POLYLINEI:
			items.Add(new POLYLINEIHeader());
			break;
		case FileHeaderTypes.POLYLINEF:
			items.Add(new POLYLINEFHeader());
			break;
		case FileHeaderTypes.IntLine:
			items.Add(new IntLineHeader());
			break;
		case FileHeaderTypes.IntArch:
			items.Add(new IntArchHeader());
			break;
		case FileHeaderTypes.FloatLine:
			items.Add(new FloatLineHeader());
			break;
		case FileHeaderTypes.FloatArch:
			items.Add(new FloatArchHeader());
			break;
		case FileHeaderTypes.LINEI:
			items.Add(new LINEIHeader());
			break;
		case FileHeaderTypes.LINEF:
			items.Add(new LINEFHeader());
			break;
		case FileHeaderTypes.ARCI:
			items.Add(new ARCIHeader());
			break;
		case FileHeaderTypes.ARCF:
			items.Add(new ARCFHeader());
			break;
		case FileHeaderTypes.TBL:
			items.Add(new TBLHeader());
			break;
		case FileHeaderTypes.TBLElement:
			items.Add(new TBLElementHeader());
			break;
		case FileHeaderTypes.ZRNR:
			items.Add(new ZRNRHeader());
			items.Add(new ZRNRHeader_2_2_66());
			break;
		case FileHeaderTypes.CAM:
			items.Add(new CAMHeader());
			items.Add(new CAMHeader_2_3_19());
			break;
		case FileHeaderTypes.CAMElement:
			items.Add(new CAMElementHeader());
			break;
		case FileHeaderTypes.ProjectParams:
			items.Add(new ProjectParamsHeader());
			items.Add(new ProjectParamsHeader_Version_2_3_6());
			items.Add(new ProjectParamsHeader_Version_2_3_7());
			items.Add(new ProjectParamsHeader_Version_2_3_11());
			items.Add(new ProjectParamsHeader_Version_2_3_15());
			break;
		case FileHeaderTypes.NetParams:
			items.Add(new NetParamsHeader());
			break;
		case FileHeaderTypes.ComParams:
			items.Add(new CommunicationParamsHeader());
			items.Add(new CommParamsHeader_Version_2_3_6());
			break;
		case FileHeaderTypes.UsbParams:
			items.Add(new USBParamsHeader());
			break;
		case FileHeaderTypes.PasswordParams:
			items.Add(new PasswordHeader());
			items.Add(new PasswordHeader_Version_2_3_11());
			break;
		case FileHeaderTypes.OtherParams:
			items.Add(new OtherParamsHeader());
			break;
		case FileHeaderTypes.HoldingParams:
			items.Add(new HoldingSectionHeader());
			break;
		case FileHeaderTypes.AnalogParams:
			items.Add(new AnalogHeader());
			break;
		case FileHeaderTypes.ExpansionParams:
			items.Add(new ExpansionHeader());
			items.Add(new ExpansionHeader_Version_2_2_38());
			break;
		case FileHeaderTypes.ExpansionUnitParams:
			items.Add(new ExpansionUnitHeader());
			break;
		case FileHeaderTypes.ExpansionUnitAIParams:
			items.Add(new ExpansionUnitAIHeader());
			items.Add(new ExpansionUnitAIHeader_2_2_66());
			items.Add(new ExpansionUnitAIHeader_2_3_14());
			items.Add(new ExpansionUnitAIHeader_2_3_16());
			break;
		case FileHeaderTypes.ExpansionUnitAOParams:
			items.Add(new ExpansionUnitAOHeader());
			items.Add(new ExpansionUnitAOHeader_2_3_16());
			break;
		case FileHeaderTypes.FilterParams:
			items.Add(new FilterHeader());
			break;
		case FileHeaderTypes.FileExtension:
			items.Add(new FileExtensionHeader());
			break;
		case FileHeaderTypes.Folder:
			items.Add(new FolderHeader_Version_2_2_46());
			items.Add(new FolderHeader_Version_2_3_22());
			break;
		case FileHeaderTypes.FolderItem:
			items.Add(new FolderItemHeader());
			break;
		case FileHeaderTypes.ValueLine:
			items.Add(new ValueLineHeader());
			break;
		case FileHeaderTypes.ValueStory:
			items.Add(new ValueStoryHeader());
			break;
		case FileHeaderTypes.ValueBoard:
			items.Add(new ValueBoardHeader());
			break;
		case FileHeaderTypes.HSCS:
			items.Add(new HSCSHeader());
			break;
		case FileHeaderTypes.HSCSItem:
			items.Add(new HSCSItemHeader());
			break;
		case FileHeaderTypes.ECAM:
			items.Add(new ECAMHeader());
			items.Add(new ECAMHeader_Version_2_3_6());
			break;
		case FileHeaderTypes.ECAMItem:
			items.Add(new ECAMItemHeader());
			break;
		case FileHeaderTypes.PolylineProject:
			items.Add(new PolylineProjectHeader());
			break;
		case FileHeaderTypes.PolylineImage:
			items.Add(new PolylineImageHeader());
			break;
		case FileHeaderTypes.PolylineEntity:
			items.Add(new PolylineEntityHeader());
			break;
		case FileHeaderTypes.PolylineLine:
			items.Add(new PolylineLineHeader());
			break;
		case FileHeaderTypes.PolylineCircle:
			items.Add(new PolylineCircleHeader());
			break;
		case FileHeaderTypes.PolylineArch:
			items.Add(new PolylineArchHeader());
			break;
		case FileHeaderTypes.PolylineEllipse:
			items.Add(new PolylineEllipseHeader());
			break;
		case FileHeaderTypes.PolylineEllipseArch:
			items.Add(new PolylineEllipseArchHeader());
			break;
		case FileHeaderTypes.PolylineBSpline:
			items.Add(new PolylineBSplineHeader());
			break;
		case FileHeaderTypes.PolylinePolygon:
			break;
		case FileHeaderTypes.PolylineRect:
			break;
		case FileHeaderTypes.PolylineGroup:
			items.Add(new PolylineGroupHeader());
			break;
		case FileHeaderTypes.HMIPLINEArgument:
			items.Add(new HMIPLINEArgumentHeader());
			break;
		case FileHeaderTypes.HMIPLINEImageArgument:
			items.Add(new HMIPLINEImageArgumentHeader());
			break;
		case FileHeaderTypes.HMIPLINELadderArgument:
			items.Add(new HMIPLINELadderArgumentHeader());
			break;
		case FileHeaderTypes.SFCOutput:
			items.Add(new SFCOutputHeader());
			break;
		case FileHeaderTypes.SFCStateExtend:
			items.Add(new SFCStateExtendHeader());
			break;
		case FileHeaderTypes.SFCTransformExtend:
			items.Add(new SFCTransformExtendHeader());
			break;
		case FileHeaderTypes.PolylineUserData:
			items.Add(new PolylineUserDataHeader());
			break;
		case FileHeaderTypes.PolylineUserFormat:
			items.Add(new PolylineUserFormatHeader());
			break;
		case FileHeaderTypes.StringMonitorCore:
			items.Add(new StringMonitorCoreHeader());
			break;
		case FileHeaderTypes.StringMonitorElement:
			items.Add(new StringMonitorElementHeader());
			break;
		case FileHeaderTypes.LadderDiagramArgument:
			items.Add(new LadderDiagramArgumentHeader());
			break;
		case FileHeaderTypes.CALLTable:
			items.Add(new CALLTableHeader());
			break;
		case FileHeaderTypes.LadderArgumentUsedTable:
			items.Add(new LadderArgumentUsedTableHeader());
			break;
		case FileHeaderTypes.HSCSTable:
			items.Add(new HSCSTableHeader());
			break;
		case FileHeaderTypes.AIOAnalogParams:
			items.Add(new AIOAnalogParamsHeader());
			break;
		case FileHeaderTypes.AIOAnalogInputParams:
			items.Add(new AIOAnalogInputParamsHeader());
			items.Add(new AIOAnalogInputParamsHeader_2_3_17());
			break;
		case FileHeaderTypes.AIOAnalogOutputParams:
			items.Add(new AIOAnalogOutputParamsHeader());
			items.Add(new AIOAnalogOutputParamsHeader_2_3_17());
			break;
		case FileHeaderTypes.FBDDiagram:
			items.Add(new FBDDiagramHeader());
			break;
		case FileHeaderTypes.FBDNetwork:
			items.Add(new FBDNetworkHeader());
			break;
		case FileHeaderTypes.FBDUnit:
			items.Add(new FBDUnitHeader());
			break;
		case FileHeaderTypes.FBDBaseValue:
			items.Add(new FBDBaseValueHeader());
			break;
		case FileHeaderTypes.EPID:
			items.Add(new EPIDHeader());
			break;
		case FileHeaderTypes.EPIDItem:
			items.Add(new EPIDItemHeader());
			items.Add(new EPIDItemHeader_Version_2_3_15());
			items.Add(new EPIDItemHeader_2_3_22());
			break;
		case FileHeaderTypes.NetworkCommentList:
			items.Add(new NetworkCommnetListHeader());
			break;
		case FileHeaderTypes.PreNetList:
			items.Add(new PreNetListHeader());
			break;
		case FileHeaderTypes.ExtendLineList:
			items.Add(new ExtendLineListHeader());
			break;
		case FileHeaderTypes.TECAMParams_Fly:
			items.Add(new TECAMFlyParamsHeader());
			break;
		case FileHeaderTypes.TECAMParams_Follow:
			items.Add(new TECAMFollowParamsHeader());
			break;
		case FileHeaderTypes.TECAMParams_Free:
			items.Add(new TECAMFreeParamsHeader());
			break;
		case FileHeaderTypes.TECAMParams_Free_KeyPoint:
			items.Add(new TECAMFreeKeyPointHeader());
			break;
		case FileHeaderTypes.TECAMParams_Mid:
			items.Add(new TECAMMidParamsHeader());
			break;
		case FileHeaderTypes.TECAMParams_Mid_MidPoint:
			items.Add(new TECAMMidPointHeader());
			break;
		case FileHeaderTypes.TECAMAttributeParams:
			items.Add(new TECAMAttributeHeader());
			break;
		case FileHeaderTypes.DVITTable:
			items.Add(new DVITTableHeader());
			break;
		case FileHeaderTypes.QuickTreeViewInfo:
		case FileHeaderTypes.PolylineImportCore:
		case FileHeaderTypes.PolylineImportColumn:
		case FileHeaderTypes.PolylineExportCore:
		case FileHeaderTypes.PolylineExportColumn:
			break;
		}
	}

	public IFileHeader CreateExpectedHeader(FileVersion version)
	{
		for (int num = items.Count() - 1; num >= 0; num--)
		{
			if (items[num].MinorSupportedVersion().CompareTo(version) <= 0)
			{
				return items[num].Create();
			}
		}
		return null;
	}
}
