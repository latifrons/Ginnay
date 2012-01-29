using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Ginnay.Proxy
{
	public class DNSCache
	{
		private Dictionary<string,IPAddress[]> domainMapping = new Dictionary<string, IPAddress[]>();

		public Dictionary<string, IPAddress[]> DomainMapping
		{
			get { return domainMapping; }
			set { domainMapping = value; }
		}

		public IPAddress[] GetIPAddress(string domain)
		{
			IPAddress[] addrs;
			if (!domainMapping.TryGetValue(domain, out addrs))
			{
				addrs = GetFreshIPAddress(domain);
			}
			return addrs;
		}

		public IPAddress[] GetFreshIPAddress(string domain)
		{
			try
			{
				IPAddress[] addrs = Dns.GetHostAddresses(domain);
				domainMapping[domain] = addrs;
				return addrs;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
