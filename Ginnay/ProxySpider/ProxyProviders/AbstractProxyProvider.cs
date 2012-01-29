using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ginnay.ProxySpider.ProxyProviders
{
	public abstract class AbstractProxyProvider
	{
		private bool enabled;

		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		public string ClassName
		{
			get { return className; }
			set { className = value; }
		}

		private string className;

		public abstract HashSet<ProxyInfo> ProvideProxy();
	}
}
