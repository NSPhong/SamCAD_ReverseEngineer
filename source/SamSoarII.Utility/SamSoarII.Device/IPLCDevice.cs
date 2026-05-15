using System;
using SamSoarII.Core.Models;
using SamSoarII.Utility;

namespace SamSoarII.Device;

public interface IPLCDevice : IDevice
{
	string DeviceName { get; }

	int BitNumber { get; }

	PLCType Type { get; }

	IntRange XRange { get; }

	IntRange YRange { get; }

	IntRange MRange { get; }

	IntRange CRange { get; }

	IntRange TRange { get; }

	IntRange SRange { get; }

	IntRange DRange { get; }

	IntRange CVRange { get; }

	IntRange TVRange { get; }

	IntRange AIRange { get; }

	IntRange AORange { get; }

	IntRange EAIRange { get; }

	IntRange EAORange { get; }

	IntRange VRange { get; }

	IntRange ZRange { get; }

	IntRange CV16Range { get; }

	IntRange CV32Range { get; }

	IntRange EXRange { get; }

	IntRange EYRange { get; }

	IntRange PulseRange { get; }

	IntRange ItrpRange { get; }

	IntRange SMRange { get; }

	IntRange SDRange { get; }

	bool IsMini { get; }

	IntRange GetRange(IValueModel value);

	IntRange GetRange(Enum type);
}
