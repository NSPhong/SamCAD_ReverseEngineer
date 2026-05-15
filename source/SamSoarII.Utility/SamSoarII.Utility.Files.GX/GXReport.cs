using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXReport
{
	private List<GXLadder> ladders;

	private List<GXRegister> registers;

	private List<GXInitialize> inits;

	private List<GXMonitor> monitors;

	public List<GXLadder> Ladders => ladders;

	public List<GXRegister> Registers => registers;

	public List<GXInitialize> Inits => inits;

	public List<GXMonitor> Monitors => monitors;

	public GXReport()
	{
		ladders = new List<GXLadder>();
		registers = new List<GXRegister>();
		inits = new List<GXInitialize>();
		monitors = new List<GXMonitor>();
	}
}
