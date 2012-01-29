using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Ginnay.Pac;
using Ginnay.Proxy;
using Ginnay.Proxy.SocketManager;
using Ginnay.ProxySpider;
using Ginnay.ProxySpider.ProxyProviders;

namespace GinnayGUI
{
	static class Program
	{
		static EventWaitHandle ewh = new EventWaitHandle(false,EventResetMode.AutoReset);
		private static Instances i;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			i = new Instances();
			i.Init();
			MainForm mf = new MainForm();
			mf.Ins = i;
			mf.Ins.LoadConfigs();
			Application.Run(mf);
			Application.Exit();
		}

		private static void V3()
		{
			i  = new Instances();
			i.Init();
			i.ProxyManager.OnDownloadProgressChanged += new ProgressChangedEventHandler(P1);
			i.ProxyManager.OnValidateProgressChanged += new ProgressChangedEventHandler(P1);
			i.ProxyManager.OnDownloadCompleted += new DownloadCompletedEventHandler(P2);
//			i.StartDownloadProxies();
		}

		private static void P2(object sender, DownloadCompletedEventHandlerArgs args)
		{
			while (true)
			{
//				i.StartValidateProxies();
				Thread.Sleep(5000);
//				i.StopValidateProxies();
				Thread.Sleep(5000);
			}
			
		}

		private static void P1(object sender, ProgressChangedEventArgs e)
		{
			Console.WriteLine(e.ProgressPercentage);
		}


		private static void V1()
		{
			List<ProxyInfo> pis = new List<ProxyInfo>();
			pis.Add(new ProxyInfo
			        	{
			        		HttpProxy = new WebProxy("127.0.0.1:8888"),
							RTT = 45
			        	});
			ProxyInfoParser.WriteConfig("F:/dev/a.xml", pis);
		}
		private static void V2()
		{
			IEnumerable<ProxyInfo> pis = ProxyInfoParser.ReadConfig("F:/dev/a.xml");
		}
	}
}
