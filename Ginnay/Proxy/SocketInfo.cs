using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Ginnay.Proxy.SocketManager
{
	public class SocketInfo
	{
		private Socket socket;
		private IPAddress[] ips;
		private int port;
		private bool inUse;
		private bool direct;
		private bool reUse = false;

		public Socket Socket
		{
			get { return socket; }
			set { socket = value; }
		}

		public bool InUse
		{
			get { return inUse; }
			set { inUse = value; }
		}

		public bool Direct
		{
			get { return direct; }
			set { direct = value; }
		}

		public bool ReUse
		{
			get { return reUse; }
			set { reUse = value; }
		}

		public int Port
		{
			get { return port; }
			set { port = value; }
		}

		public IPAddress[] Ips
		{
			get { return ips; }
			set { ips = value; }
		}
	}
}
