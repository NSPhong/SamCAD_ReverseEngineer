using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.FP;

public abstract class FPFormatGroup : FPFormat
{
	protected List<FPFormat> items;

	public IList<FPFormat> Items => items;

	protected FPFormatGroup(uint _maincode)
		: base(0L, _maincode, 0u, 0u)
	{
		items = new List<FPFormat>();
	}

	protected abstract int Compare(FPFormat f0, FPFormat f1);

	protected abstract FPFormat Create(FPFormat fmt);

	public FPFormat Set(FPFormat fmt)
	{
		for (int i = 0; i < items.Count() + 1; i++)
		{
			FPFormat fPFormat = ((i - 1 >= 0) ? items[i - 1] : null);
			FPFormat fPFormat2 = ((i < items.Count()) ? items[i] : null);
			int num = ((fPFormat == null) ? (-1) : Compare(fPFormat, fmt));
			int num2 = ((fPFormat2 == null) ? (-1) : Compare(fmt, fPFormat2));
			if (num == 0)
			{
				return (fPFormat is FPFormatGroup) ? ((FPFormatGroup)fPFormat).Set(fmt) : fPFormat;
			}
			if (num2 == 0)
			{
				return (fPFormat2 is FPFormatGroup) ? ((FPFormatGroup)fPFormat2).Set(fmt) : fPFormat2;
			}
			if (num < 0 && num2 < 0)
			{
				items.Insert(i, Create(fmt));
				return fmt;
			}
		}
		return null;
	}

	public FPFormat Get(FPFormat fmt)
	{
		int num = 0;
		int num2 = items.Count() - 1;
		while (num + 1 < num2)
		{
			int num3 = num + num2 >> 1;
			FPFormat fPFormat = items[num3];
			switch (Compare(fPFormat, fmt))
			{
			case 0:
				return (fPFormat is FPFormatGroup) ? ((FPFormatGroup)fPFormat).Get(fmt) : fPFormat;
			case -1:
				num = num3 + 1;
				break;
			case 1:
				num2 = num3 - 1;
				break;
			}
		}
		if (num > num2)
		{
			return null;
		}
		if (num == num2)
		{
			return (items[num] is FPFormatGroup) ? ((FPFormatGroup)items[num]).Get(fmt) : items[num];
		}
		if (Compare(items[num], fmt) == 0)
		{
			return (items[num] is FPFormatGroup) ? ((FPFormatGroup)items[num]).Get(fmt) : items[num];
		}
		if (Compare(items[num2], fmt) == 0)
		{
			return (items[num2] is FPFormatGroup) ? ((FPFormatGroup)items[num2]).Get(fmt) : items[num2];
		}
		return null;
	}
}
