using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ginnay.Pac;
using Ginnay.Proxy;
using Ginnay.Proxy.SocketManager;
using Ginnay.ProxySpider;
using Ginnay.ProxySpider.ProxyProviders;

namespace Ginnay
{
	class Program
	{
		private static ProxyManager pm;
		private static Listener l;
		static void Main(string[] args)
		{
			v9();
			Console.ReadKey();
		}
		static void v1()
		{
			ManualResetEvent mre = new ManualResetEvent(false);
			Listener l = new Listener();
			
			l.StartListener();
			mre.WaitOne();
		}
		static void v2()
		{
			while (true)
			{
				string s = Console.ReadLine();
				WebPageProxyProvider wppp = new WebPageProxyProvider();
				wppp.Sources.Clear();
				wppp.Sources.Add(new WebPageProxySource
				{
					URL = s,
				});
				wppp.ProvideProxy();
			}
		}
		static void v3()
		{
			Regex charsetRegex = new Regex(@"<meta.*?charset\s*=\s*(?<charset>.*?)\s*?""?\s*/?>");
			string s = @"<meta http-equiv=""Content-Type"" content=text/html; charset=gb2312>";
			Match m = charsetRegex.Match(s);
			if (m.Success)
			{
				//fix encoding
				string charset = m.Groups["charset"].Value;
				Console.WriteLine(charset);
			}
		}
		static void v4()
		{
			WebPageProxyProvider wppp = new WebPageProxyProvider();
			wppp.Sources.Clear();
			
			wppp.Sources.Add(new WebPageProxySource
			{
				URL = "http://proxy.ipcn.org/proxylist.html",
				Pattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\s*):(?<port>\s*\d{1,5})"
				
			});
			wppp.ProvideProxy();
		}
		static void v5()
		{

			WebPageProxyProvider wppp = new WebPageProxyProvider();
			wppp.Sources.Add(new WebPageProxySource
			{
				URL = "http://proxy.ipcn.org/proxylist.html",
				Pattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\s*):(?<port>\s*\d{1,5})"

			});

			ProxyValidator pv = new ProxyValidator();
			ProxyValidateCondition vc = new ProxyValidateCondition();
			vc.Url = "http://www.baidu.com";
			vc.Keywords.Add("百度");
			vc.Keywords.Add("html");
			pv.ValidateConditions.Add(vc);

			pm = new ProxyManager();
			pm.ProxyProviders.Add(wppp);
			pm.ProxyValidator = pv;
			pm.ProxyValidator.ValidateConditions.Add(vc);
			
			pm.StartDownloadProxies(false);
			Thread.Sleep(10000);
			pm.CancelValidation();

		}

		private static void Validate(object sender, RunWorkerCompletedEventArgs e)
		{
			pm.StartValidateProxies();
		}

		private static void Done(object sender, RunWorkerCompletedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();

			List<ProxyInfo> goods = pm.GoodProxiesQueue.ToList();
			foreach (ProxyInfo s in goods)
			{
				sb.Append(s.HttpProxy.Address.Host).Append(" ");
				sb.Append(s.HttpProxy.Address.Port).Append(" ");
				sb.Append(s.RTT);
				sb.Append(System.Environment.NewLine);
			}
			File.WriteAllText("f:/dev/proxy.txt", sb.ToString());
			Console.WriteLine("All Done");
		}

		static void v6()
		{
			List<string> throughMe = new List<string>();
			throughMe.Add("baidu.com");
			throughMe.Add("ba.com");
			throughMe.Add("baidu.com:8080");
			PacSetting ps = new PacSetting
			                	{
			                		MyAddress = "127.0.0.1:8555",
			                		OtherProxyAddress = null
			                	};
//			ps.AddURLPatternRange(throughMe);
			Console.WriteLine(PacGenerator.GeneratePacString(ps));
		}
		static void v7()
		{
//			bool a = ProxySetter.SetPac("127.0.0.1/pac.pac");
//			bool a = ProxySetter.SetProxy("5.5.5.5:8080");
			//bool a = IEProxySetter.SetProxy("127.0.0.1:808");
//			bool a = ProxySetter.SetDirect();
			//Console.WriteLine(a);
		}
		static void v8()
		{
			ManualResetEvent mre = new ManualResetEvent(false);
			l = new Listener();
			SelectiveProxyGuide spg = new SelectiveProxyGuide();
			spg.DnsCache = new DNSCache();
			spg.PacSetting = new PacSetting();
//			spg.PacSetting.AddURLPattern(@"http://.*\.baidu\.com");
			
			pm = new ProxyManager();
			WebPageProxyProvider wppp = new WebPageProxyProvider();
			wppp.Sources.Add(new WebPageProxySource
			                 	{
			                 		URL = "http://proxy.ipcn.org/proxylist.html",
			                 		Pattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\s*):(?<port>\s*\d{1,5})"

			                 	});
			
			pm.ProxyProviders.Add(wppp);

			ProxyValidator pv = new ProxyValidator();
			ProxyValidateCondition vc = new ProxyValidateCondition();
			vc.Url = "http://www.baidu.com";
			vc.Keywords.Add("百度");
			vc.Keywords.Add("html");
			pv.ValidateConditions.Add(vc);

			pm.ProxyValidator = pv;
			
			spg.ProxyManager = pm;
			l.TargetConnctionGuide = spg;

			pm.StartDownloadProxies(false);

			l.StartListener();
			mre.WaitOne();
		}

		private static void v82(object sender, RunWorkerCompletedEventArgs e)
		{
			pm.StartValidateProxies();
		}

		private static void v83(object sender, RunWorkerCompletedEventArgs e)
		{
			Console.WriteLine("Done");
		}

		private static void v81(object sender, ProgressChangedEventArgs e)
		{
			Console.WriteLine(e.ProgressPercentage+" "+e.UserState);
		}
		private static void v9()
		{
			byte[] bs = new byte[]
			           	{
			           		0x35,0xd,0xa,0x31,0x41,0x41,0x31,0x32,0xd,0xa,
							0x31,0xd,0xa,0x3a,0xd,0xa,
							0x30,0xd,0xa,0xd,0xa
			           	};
			byte[] o = HtmlHelper.DeChunk(bs);
			foreach (byte b in o )
			{
				Console.WriteLine(b+" ");
			}
		}
	}
}
