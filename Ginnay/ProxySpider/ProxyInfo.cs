using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Ginnay.ProxySpider
{
	public class ProxyInfo
	{
		
		private WebProxy httpProxy;
		private int rtt;
		private string _location;

		public WebProxy HttpProxy
		{
			get { return httpProxy; }
			set { httpProxy = value; }
		}
		public string ProxyAddress
		{
			get
			{
				if (httpProxy.Address.Port == 80)
				{
					return httpProxy.Address.Authority + ":80";
				}
				else
				{
					return httpProxy.Address.Authority;
				}
			}
		}

		public int RTT
		{
			get { return rtt; }
			set { rtt = value; }
		}

		public string Location
		{
			get { return _location; }
			set { _location = value; }
		}
	}
}
