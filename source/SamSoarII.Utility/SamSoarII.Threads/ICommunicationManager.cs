using Samkoon.Protocol;

namespace SamSoarII.Threads;

public interface ICommunicationManager : IThreadManager
{
	PortTypes PortType { get; }
}
