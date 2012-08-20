using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Ginnay.Pac;
using Ginnay.Proxy;
using Ginnay.Proxy.SocketManager;
using Ginnay.ProxySpider;
using Ginnay.ProxySpider.ProxyProviders;

namespace GinnayGUI
{
	public class Instances
	{
		public const int VER = 120820;
		public const string VERS = "0.23";


		private string proxyInfoPath = "config/proxyinfo.xml";
		private string proxyValidateConditionPath = "config/proxyvalidate.xml";
		private string urlPatternFolderPath = "pattern/";
		private string proxyProviderPath = "config/proxyprovider.xml";
		Regex r = new Regex(@"Latest Version: Ginnay_(?<vers>[\w-t.]+)_Build_(?<ver>\d+)");
		private string pacFilePath;

		private SystemProxy backup;
		#region fields

		private DNSCache dnsCache;
		private Listener listener;
		private Thread listenerThread;
		private PacSetting pacSetting;
		private ProxyManager proxyManager;
		private SelectiveProxyGuide selectiveProxyGuide;
		private TargetResponseValidator targetResponseValidator;
		private TargetConnectionPool targetConnectionPool;
		private ProxySetter IEProxySetter;
		

		public PacSetting PacSetting
		{
			get { return pacSetting; }
			set { pacSetting = value; }
		}

		public DNSCache DnsCache
		{
			get { return dnsCache; }
			set { dnsCache = value; }
		}

		public Listener Listener
		{
			get { return listener; }
			set { listener = value; }
		}

		public TargetConnectionPool TargetConnectionPool
		{
			get { return targetConnectionPool; }
			set { targetConnectionPool = value; }
		}

		public ProxyManager ProxyManager
		{
			get { return proxyManager; }
			set { proxyManager = value; }
		}

		public SelectiveProxyGuide SelectiveProxyGuide
		{
			get { return selectiveProxyGuide; }
			set { selectiveProxyGuide = value; }
		}

		public TargetResponseValidator TargetResponseValidator
		{
			get { return targetResponseValidator; }
			set { targetResponseValidator = value; }
		}

		public ProxySetter IeProxySetter
		{
			get { return IEProxySetter; }
			set { IEProxySetter = value; }
		}

		public string PacFilePath
		{
			get { return pacFilePath; }
			set { pacFilePath = value; }
		}

		#endregion

		public void Init()
		{
			pacSetting = new PacSetting();
//			URLPattern up = new URLPattern("http://.*.baidu.com/.*");
//			//up.NecessaryKeyword.Add("百度");
//			up.ForbiddenKeywords.Add("五星体育");
//			up.NeedValidation = true;
//			pacSetting.LoadURLPattern(up);
//			PacSettingParser.ReadConfig("./Config/pac.xml",pacSetting);

			dnsCache = new DNSCache();

			targetConnectionPool = new TargetConnectionPool();
			targetResponseValidator = new TargetResponseValidator();
			targetResponseValidator.PacSetting = pacSetting;

			

			//targetConnectionPool.StartDaemon();


			proxyManager = new ProxyManager();
//			var wppp = new WebPageProxyProvider();
//			wppp.Sources.Add(new WebPageProxySource
//			                 	{
//			                 		URL = "http://proxy.ipcn.org/proxylist.html",
//			                 		Pattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\s*):(?<port>\s*\d{1,5})"
//			                 	});
//			proxyManager.ProxyProviders.Add(wppp);

			proxyManager.ProxyValidator = new ProxyValidator();
			//load validate conditions
//			var vc = new ProxyValidateCondition();
//			vc.Url = "http://www.baidu.com";
//			vc.Keywords.Add("百度");
//			vc.Keywords.Add("html");
//			proxyManager.ProxyValidator.ValidateConditions.Add(vc);

//			proxyValidator.ValidateConditions.AddRange(ProxyValidateConditionParser.ReadConfig("./Config/validate.xml"));

			selectiveProxyGuide = new SelectiveProxyGuide();
			selectiveProxyGuide.DnsCache = dnsCache;
			selectiveProxyGuide.PacSetting = pacSetting;
			selectiveProxyGuide.ProxyManager = proxyManager;

			listener = new Listener();
			listener.TargetConnectionPool = targetConnectionPool;
			listener.TargetConnctionGuide = selectiveProxyGuide;
			listener.TargetResponseValidator = targetResponseValidator;
			

			proxyManager.OnValidateCompleted += new RunWorkerCompletedEventHandler(DoValidateCompleted);

			IEProxySetter = new IEProxySetter();
			pacFilePath = Path.GetFullPath("my.pac");
		}
		
		public bool LoadConfigs()
		{
			bool success = true;
			if (File.Exists(proxyInfoPath))
			{
				try
				{
					IEnumerable<ProxyInfo> pis = ProxyInfoParser.ReadConfig(proxyInfoPath);
					proxyManager.LoadProxies(pis);
				}catch
				{
					success = false;
				}
			}
			if (File.Exists(proxyValidateConditionPath))
			{
				try
				{
					IEnumerable<ProxyValidateCondition> pvcs = ProxyValidateConditionParser.ReadConfig(proxyValidateConditionPath);
					proxyManager.ProxyValidator.LoadProxyValidations(pvcs);
				}
				catch
				{
					success = false;
				}
			}
			if (File.Exists(proxyProviderPath))
			{
				try
				{
					IEnumerable<AbstractProxyProvider> pvcs = ProxyProviderParser.ReadConfig(proxyProviderPath);
					proxyManager.LoadProxyProviders(pvcs);
				}
				catch
				{
					success = false;
				}
			}
			success &= LoadURLPatterns();
			return success;
		}
		public bool LoadURLPatterns()
		{
			bool success = true;
			try
			{
				if (!Directory.Exists(urlPatternFolderPath))
				{
					Directory.CreateDirectory(urlPatternFolderPath);
				}
			}
			catch
			{
				throw;
			}
			string[] configs = Directory.GetFiles(urlPatternFolderPath, "*.xml");
			foreach (string file in configs)
			{
				try
				{
					IEnumerable<URLPattern> urlPatterns = URLPatternParser.ReadConfig(file);
					pacSetting.LoadURLPatterns(urlPatterns);
				}
				catch
				{
					success = false;
				}
			}
			return true;
		}
		public bool SaveURLPatterns()
		{
			bool success = true;
			Dictionary<string,List<URLPattern>> configs = new Dictionary<string, List<URLPattern>>();
			foreach (URLPattern up in pacSetting.ThroughMePatterns)
			{
				List<URLPattern> ps;
				if (!configs.TryGetValue(up.Classification, out ps))
				{
					ps = new List<URLPattern>();
					configs[up.Classification] = ps;
				}
				ps.Add(up);
			}
			string[] files = Directory.GetFiles(urlPatternFolderPath);
			foreach (string s in files)
			{
				File.Delete(s);
			}
			foreach (KeyValuePair<string, List<URLPattern>> pairs in configs)
			{
				try
				{
					string path = Path.Combine(urlPatternFolderPath, pairs.Key);
					path += ".xml";
					URLPatternParser.WriteConfig(path, pairs.Value);
				}
				catch
				{
					success = false;
				}
			}
			success &= UpdateIEPac();
			return success;
		}
		public bool SaveProxyValidateConditionConfig()
		{
			try
			{
				ProxyValidateConditionParser.WriteConfig(proxyValidateConditionPath, proxyManager.ProxyValidator.ValidateConditions);
			}catch
			{
				return false;
			}
			return true;
		}

		private void DoValidateCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			List<ProxyInfo> pis = proxyManager.GoodProxiesQueue.ToList();
			ProxyInfo current = selectiveProxyGuide.CurrentProxyInfo;
			if (current != null)
			{
				pis.Insert(0,current);
			}
			ProxyInfoParser.WriteConfig(proxyInfoPath, pis);
		}

		public void StartListenerThread(int port)
		{
			pacSetting.MyAddress = "127.0.0.1:" + port;
			listener.Port = port;
			listenerThread = new Thread(new ThreadStart(listener.StartListener));

			listenerThread.Start();
			SetIEPac();
		}

		public void StopListenerThread()
		{
			if (listenerThread != null && listenerThread.IsAlive)
			{
				listener.StopListener();
				listenerThread.Abort();
				listenerThread.Join();
			}
			CancelIEPac();
		}
		public bool SetIEPac()
		{
			UpdateIEPac();
			if (IEProxySetter != null)
			{
				if (backup == null)
				{
					backup = IeProxySetter.GetSystemProxy();
				}
				return IeProxySetter.SetSystemProxy(new SystemProxy
				                                    	{
				                                    		IsAutoDetect = false,
				                                    		IsDirect = false,
				                                    		IsPac = true,
				                                    		IsProxy = false,
				                                    		PacPath = pacFilePath,
				                                    		ProxyAddress = null
				                                    	});
				//return IEProxySetter.SetPac(PacFilePath);
			}
			return true;
		}

		public bool CancelIEPac()
		{
			if (IEProxySetter != null)
			{
				if (backup != null)
				{
					bool success = IeProxySetter.SetSystemProxy(backup);
					backup = null;
					return success;
				}
				else
				{
					return IeProxySetter.SetSystemProxy(new SystemProxy
					                                    	{
					                                    		IsAutoDetect = false,
					                                    		IsDirect = true,
					                                    		IsPac = false,
					                                    		IsProxy = false
					                                    	});
				}
			}
			return true;
		}
		public bool UpdateIEPac()
		{
			string pacStr = PacGenerator.GeneratePacString(pacSetting);
			try
			{
				File.WriteAllText(PacFilePath, pacStr);
				
			}
			catch
			{
				return false;
			}
			return true;
		}
		public void CheckUpdate(out int ver, out string vers)
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://github.com/latifrons/Ginnay/wiki/Versions");
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				string html;
				HtmlHelper.GetHtml(response, out html);
				Match m = r.Match(html);
				if (m.Success)
				{
					ver = Int32.Parse(m.Groups["ver"].Value);
					vers = m.Groups["vers"].Value;
				}
				else
				{
					ver = 0;
					vers = "";
				}
				response.Close();
			}
			catch
			{
				ver = 0;
				vers = "";
			}
			
		}
	}
}