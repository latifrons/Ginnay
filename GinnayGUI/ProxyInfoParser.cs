using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Ginnay.Proxy.BLL;
using Ginnay.ProxySpider;

namespace GinnayGUI
{
	public class ProxyInfoParser
	{
		public static IEnumerable<ProxyInfo> ReadConfig(string path)
		{
			List<ProxyInfo> proxies = new List<ProxyInfo>();
			XmlDocument doc = new XmlDocument();
			doc.Load(path);
			XmlNodeList nodelist = doc.SelectNodes("//ProxyInfo");
			if (nodelist == null)
			{
				return proxies;
			}

			foreach (XmlNode node in nodelist)
			{
				ProxyInfo pi = new ProxyInfo();
				if (node.Attributes == null)
				{
					continue;
				}
				XmlAttribute addressX = node.Attributes["Address"];
				XmlAttribute latencyX = node.Attributes["Latency"];
				if (addressX == null || latencyX == null)
				{
					continue;
				}

				pi.HttpProxy = new WebProxy(addressX.Value);
				int rtt;
				if (!Int32.TryParse(latencyX.Value, out rtt))
				{
					continue;
				}
				pi.RTT = rtt;
				pi.Location = IPLocationSearch.GetIPLocation(pi.HttpProxy.Address.Host).country;
				proxies.Add(pi);
			}
			return proxies;
		}

		public static void WriteConfig(string path, IEnumerable<ProxyInfo> proxyInfos)
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, "yes"));
			XmlElement root = doc.CreateElement("ProxyInfos");
			doc.AppendChild(root);
			foreach (ProxyInfo pi in proxyInfos)
			{
				XmlElement node = doc.CreateElement("ProxyInfo");
				node.SetAttribute("Address", pi.ProxyAddress);
				node.SetAttribute("Latency", pi.RTT.ToString());
				root.AppendChild(node);
			}
			FileInfo fi = new FileInfo(path);
			fi.Directory.Create();
			FileStream fs = fi.Create();
			doc.Save(fs);
			fs.Close();
		}
	}
}
