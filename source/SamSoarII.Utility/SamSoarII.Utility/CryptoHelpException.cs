using System;

namespace SamSoarII.Utility;

public class CryptoHelpException : ApplicationException
{
	public CryptoHelpException(string msg)
		: base(msg)
	{
	}
}
