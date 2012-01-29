using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ginnay.Pac
{
	public abstract class ProxySetter
	{
		public abstract bool SetSystemProxy(SystemProxy sp);
		public abstract SystemProxy GetSystemProxy();
	}
	public class SystemProxy
	{
		private bool isDirect;
		private bool isProxy;
		private bool isPac;
		private bool isAutoDetect;
		private string proxyAddress;
		private string pacPath;

		public bool IsDirect
		{
			get { return isDirect; }
			set { isDirect = value; }
		}

		public bool IsPac
		{
			get { return isPac; }
			set { isPac = value; }
		}

		public bool IsAutoDetect
		{
			get { return isAutoDetect; }
			set { isAutoDetect = value; }
		}

		public string ProxyAddress
		{
			get { return proxyAddress; }
			set { proxyAddress = value; }
		}

		public string PacPath
		{
			get { return pacPath; }
			set { pacPath = value; }
		}

		public bool IsProxy
		{
			get { return isProxy; }
			set { isProxy = value; }
		}
	}
}
