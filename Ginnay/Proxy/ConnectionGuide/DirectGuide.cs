using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Ginnay.Proxy.SocketManager
{
	public class DirectGuide : TargetConnctionGuide
	{
		private DNSCache dnsCache;

		public DNSCache DnsCache
		{
			get { return dnsCache; }
			set { dnsCache = value; }
		}

		public override SocketInfo RequireSocketInfo(ClientRequestPacket crp)
		{
			SocketInfo si = new SocketInfo();
			si.Ips = dnsCache.GetIPAddress(crp.Host);
			if (si.Ips == null)
			{
				return null;
			}
			si.Direct = true;
			si.InUse = true;
			si.Port = crp.Port;
			si.ReUse = false;
			return si;
		}

		public override void ReportErrorSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo)
		{
			//I have nothing to do
		}

		public override void ReportNotSuitableSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo)
		{
			//I have nothing to do
		}
	}

}
