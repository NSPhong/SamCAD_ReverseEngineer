using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public abstract class BaseFileHeader : IFileHeader
{
	public abstract int HeaderSize { get; set; }

	public virtual int MaxHeaderSize => Marshal.SizeOf((object)this);

	public abstract FileHeaderTypes HeaderType { get; }

	public virtual void SetStrPtr(int id, int sp)
	{
	}

	public virtual void SetFreePtr(int id, int lp)
	{
	}

	public virtual void Save(IntPtr intptr)
	{
		Marshal.StructureToPtr((object)this, intptr, false);
	}

	public virtual void Load(IntPtr intptr)
	{
		Marshal.PtrToStructure(intptr, (object)this);
	}

	public virtual FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 20
		};
	}

	public abstract IFileHeader Create();
}
