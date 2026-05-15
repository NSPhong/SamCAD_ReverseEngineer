using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.FP;

public class FPUnitFuncOutput
{
	private int width;

	private int height;

	private List<FPUnitFuncItem> items;

	public int Width => width;

	public int Height => height;

	public IList<FPUnitFuncItem> Items => items;

	public FPUnitFuncOutput(int _width, int _height)
	{
		width = _width;
		height = _height;
		items = new List<FPUnitFuncItem>();
		while (items.Count() < width * height)
		{
			items.Add(new FPUnitFuncItem());
		}
	}

	public FPUnitFuncItem Get(int ix, int iy)
	{
		return items[iy * width + ix];
	}
}
