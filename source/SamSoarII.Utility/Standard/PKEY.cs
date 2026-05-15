using System;
using System.Runtime.InteropServices;

namespace Standard;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal struct PKEY(Guid fmtid, uint pid)
{
	private readonly Guid _fmtid = fmtid;

	private readonly uint _pid = pid;

	public static readonly Standard.PKEY Title = new Standard.PKEY(new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), 2u);

	public static readonly Standard.PKEY AppUserModel_ID = new Standard.PKEY(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 5u);

	public static readonly Standard.PKEY AppUserModel_IsDestListSeparator = new Standard.PKEY(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 6u);

	public static readonly Standard.PKEY AppUserModel_RelaunchCommand = new Standard.PKEY(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 2u);

	public static readonly Standard.PKEY AppUserModel_RelaunchDisplayNameResource = new Standard.PKEY(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 4u);

	public static readonly Standard.PKEY AppUserModel_RelaunchIconResource = new Standard.PKEY(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 3u);
}
