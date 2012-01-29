using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ginnay.Proxy.SocketManager
{
	public abstract class TargetConnctionGuide
	{
		public abstract SocketInfo RequireSocketInfo(ClientRequestPacket crp);
		public abstract void ReportErrorSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo);
		public abstract void ReportNotSuitableSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo);
	}
}
