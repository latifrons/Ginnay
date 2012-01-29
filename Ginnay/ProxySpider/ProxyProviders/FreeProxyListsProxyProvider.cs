using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Ginnay.Proxy.BLL;

namespace Ginnay.ProxySpider.ProxyProviders
{
	public class FreeProxyListsProxyProvider : AbstractProxyProvider
	{
		private string configPath = "plugin/FreeProxyListsProxyProvider/config.xml";
		private Regex matchRegex;
		private Regex ipRegex = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
		private int MAX_PROVIDE = 3000;
		private List<string> sources = new List<string>();
		List<int> portWhiteList = new List<int>(); 
		private CookieContainer cc = new CookieContainer();
		private const string DOMAIN = "www.freeproxylists.net";


		public List<string> Sources
		{
			get { return sources; }
			set { sources = value; }
		}
		public override HashSet<ProxyInfo> ProvideProxy()
		{
			Dictionary<string, ProxyInfo> proxyInfos = new Dictionary<string, ProxyInfo>();
			foreach (string src in Sources)
			{
				try
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(src);
					HtmlHelper.HttpWebRequestNormalSetup(request);
					request.CookieContainer = cc;
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();
					string html;
					if (HtmlHelper.GetHtml(response, out html))
					{
						MatchCollection mc = matchRegex.Matches(html);
						foreach (Match m in mc)
						{
							string ip = m.Groups["ip"].Value;
							ip = System.Web.HttpUtility.UrlDecode(ip);
							Match mm = ipRegex.Match(ip);
							if (mm.Success)
							{
								ip = mm.Captures[0].Value;
							}
							//<a href="http://www.freeproxylists.net/zh/118.123.248.103.html">118.123.248.103</a>
							string port = m.Groups["port"].Value;
							int porti;
							if (Int32.TryParse(port, out porti))
							{
								if (portWhiteList.Count == 0 ||
									portWhiteList.Contains(porti))
								{
									proxyInfos[ip] = new ProxyInfo
									{
										HttpProxy = new WebProxy(ip, Int32.Parse(port)),
										Location = IPLocationSearch.GetIPLocation(ip).country
									};
								}
							}
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			//return proxyInfos.GetRange(0,30);
			List<ProxyInfo> proxies = new List<ProxyInfo>(proxyInfos.Values);
			int len = proxies.Count;
			Random r = new Random();
			while (len > MAX_PROVIDE)
			{
				int random = r.Next(len);
				proxies.RemoveAt(random);
				len--;
			}
			return new HashSet<ProxyInfo>(proxies);
		}

		public FreeProxyListsProxyProvider()
		{
			ReadConfig();

		}
		public void ReadConfig()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(configPath);
			XmlNode patternNode = doc.SelectSingleNode("//Pattern");
			if (patternNode != null)
			{
				matchRegex = new Regex(patternNode.InnerText);
			}
			XmlNode cookieNode = doc.SelectSingleNode("//Cookie");
			if (cookieNode != null)
			{
				string cookie = cookieNode.InnerText;
				string[] cookies = cookie.Split(new char[] {';',' '},StringSplitOptions.RemoveEmptyEntries);
				foreach (string s in cookies)
				{
					string[] ss = s.Split(new char[] {'='});
					Cookie c = new Cookie(ss[0],ss[1],"/",DOMAIN);
					cc.Add(c);
				}
			}
			XmlNode portNode = doc.SelectSingleNode("//PortWhiteList");
			if (portNode != null)
			{
				string portstr = portNode.InnerText;
				string[] ports = portstr.Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string port in ports)
				{
					portWhiteList.Add(Int32.Parse(port));
				}
			}

			XmlNodeList nodelist = doc.SelectNodes("//URLS/URL");
			if (nodelist == null)
			{
				return;
			}

			foreach (XmlNode node in nodelist)
			{
				sources.Add(node.InnerText);
			}
		}
	}
}
