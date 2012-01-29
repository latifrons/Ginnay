using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Ginnay.ProxySpider.ProxyProviders;
using Wimlab.Utilities.ThreadSafeCollection;

namespace Ginnay.ProxySpider
{
	public delegate void UsableProxyFoundEventHandler(object sender, UsableProxyFoundEventArgs args);

	public class UsableProxyFoundEventArgs
	{
		private ProxyInfo proxyInfo;

		public ProxyInfo ProxyInfo
		{
			get { return proxyInfo; }
			set { proxyInfo = value; }
		}
	}

	public class ProxyManager
	{
		private volatile bool canStop;
		private BackgroundWorker fetchWorker;
//		private Dictionary<string, ProxyInfo> goodProxies = new Dictionary<string, ProxyInfo>();
		private TPriorityQueue<double, ProxyInfo> goodProxiesQueue = new TPriorityQueue<double, ProxyInfo>();
		private int maxValidateThreadCount = 30;
		private Queue<ProxyInfo> pendingProxies = new Queue<ProxyInfo>();

		private List<AbstractProxyProvider> proxyProviders = new List<AbstractProxyProvider>();
		private volatile int proxyToValidateCount;
		private volatile int proxyValidatedCount;
		private ProxyValidator proxyValidator;
		private List<Thread> validateThreads;
		private BackgroundWorker validateWorker;

		public event ProgressChangedEventHandler OnDownloadProgressChanged;
		public event DownloadCompletedEventHandler OnDownloadCompleted;
		public event ProgressChangedEventHandler OnValidateProgressChanged;
		public event RunWorkerCompletedEventHandler OnValidateCompleted;
		public event UsableProxyFoundEventHandler OnUsableProxyFound;
		public event ValidateStartedEventHandler OnValidateStarted;
		public event DownloadStartedEventHandler OnDownloadStarted;
		public event PendingProxyListChangedEventHandler OnPendingProxyListChanged;
		public event ProxyRemovedEventHandler OnProxyRemoved;
		public event ProxyProviderListChangedEventHandler OnProxyProviderListChanged;

		public ProxyValidator ProxyValidator
		{
			get { return proxyValidator; }
			set { proxyValidator = value; }
		}

		public List<AbstractProxyProvider> ProxyProviders
		{
			get { return proxyProviders; }
			set { proxyProviders = value; }
		}

		public int MaxValidateThreadCount
		{
			get { return maxValidateThreadCount; }
			set { maxValidateThreadCount = value; }
		}

//		public Dictionary<string, ProxyInfo> GoodProxies
//		{
//			get { return goodProxies; }
//			set { goodProxies = value; }
//		}

		public TPriorityQueue<double, ProxyInfo> GoodProxiesQueue
		{
			get { return goodProxiesQueue; }
			set { goodProxiesQueue = value; }
		}

		public Queue<ProxyInfo> PendingProxies
		{
			get { return pendingProxies; }
			set { pendingProxies = value; }
		}

		public void StartDownloadProxies(bool startValidationAfterDownload)
		{
			lock (pendingProxies)
			{
				pendingProxies.Clear();
				goodProxiesQueue.Clear();
//				goodProxies.Clear();
			}

			if (fetchWorker != null)
			{
				if (fetchWorker.IsBusy)
				{
					return;
				}
				
			}
			fetchWorker = new BackgroundWorker();
			fetchWorker.WorkerReportsProgress = true;
			fetchWorker.WorkerSupportsCancellation = true;
			fetchWorker.DoWork += new DoWorkEventHandler(fetchWorker_DoWork);
			fetchWorker.ProgressChanged += new ProgressChangedEventHandler(fetchWorker_ProgressChanged);
			fetchWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fetchWorker_RunWorkerCompleted);

			if (startValidationAfterDownload)
			{
				fetchWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CallStartValidateProxies);
			}
			if (OnDownloadStarted != null)
			{
				OnDownloadStarted(this,new DownloadStartedEventHandlerArgs());
			}
			fetchWorker.RunWorkerAsync();
		}

		private void CallStartValidateProxies(object sender, RunWorkerCompletedEventArgs e)
		{
			StartValidateProxies();
		}

		public void StopDownloadProxies()
		{
			if (fetchWorker != null)
			{
				fetchWorker.CancelAsync();
			}
		}

		private void fetchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			fetchWorker = null;
//			int elim = this.pendingProxies.Count - 20;
//			for (int i = 0; i < elim; i++)
//			{
//				this.pendingProxies.Dequeue();
//			}
			if (OnPendingProxyListChanged != null)
			{
				OnPendingProxyListChanged(sender,new PendingProxyListChangedEventHandlerArgs());
			}
			if (OnDownloadCompleted != null)
			{
				OnDownloadCompleted(sender, new DownloadCompletedEventHandlerArgs
				                            	{
				                            		Count = this.pendingProxies.Count
				                            	});
			}
		}

		private void fetchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (OnDownloadProgressChanged != null)
			{
				OnDownloadProgressChanged(sender, e);
			}
		}

		private void fetchWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
			worker.ReportProgress(0);

			int count = proxyProviders.Count;
			int current = 0;
			foreach (AbstractProxyProvider pp in proxyProviders)
			{
				if (fetchWorker.CancellationPending)
				{
					continue;
				}
				if (!pp.Enabled)
				{
					current++;
					continue;
				}
				HashSet<ProxyInfo> pis;
				try
				{
					pis = pp.ProvideProxy();
				}
				catch (Exception)
				{
					current++;
					worker.ReportProgress(current * 100 / count, "fail");
					return;
				}
				if (pis != null)
				{
					foreach (ProxyInfo pi in pis)
					{
						PendingProxies.Enqueue(pi);
					}
				}

				current++;
				worker.ReportProgress(current * 100 / count);
			}
		}

		public void StartValidateProxies()
		{
			if (validateWorker != null)
			{
				if (validateWorker.IsBusy)
				{
					return;
				}
			}
			validateWorker = new BackgroundWorker();
			validateWorker.WorkerReportsProgress = true;
			validateWorker.WorkerSupportsCancellation = true;
			validateWorker.DoWork += new DoWorkEventHandler(validateWorker_DoWork);
			validateWorker.ProgressChanged += new ProgressChangedEventHandler(validateWorker_ProgressChanged);
			validateWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(validateWorker_RunWorkerCompleted);
			if (OnValidateStarted != null)
			{
				OnValidateStarted(this,new ValidateStartedEventHandlerArgs());
			}
			validateWorker.RunWorkerAsync();
		}
		public void StopValidateProxies()
		{
			if (validateWorker != null)
			{
				validateWorker.CancelAsync();
			}
		}

		private void validateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			validateWorker = null;
			if (OnValidateCompleted != null)
			{
				OnValidateCompleted(sender, e);
			}

		}

		private void validateWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (OnValidateProgressChanged != null)
			{
				OnValidateProgressChanged(sender, e);
			}

		}

		private void validateWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
			//move all good proxies to pending proxy and clear rtt
			ProxyInfo pi;

//			Console.WriteLine("{0} {1}",pendingProxies.Count,goodProxiesQueue.Count);

			while ((pi = DequeueFastestProxy(false)) != null)
			{
				pi.RTT = 0;
				Console.WriteLine("Good"+pi.ProxyAddress);
				pendingProxies.Enqueue(pi);
			}

			proxyToValidateCount = PendingProxies.Count;
			proxyValidatedCount = 0;

			validateThreads = new List<Thread>();
			canStop = false;
			for (int i = 0; i < MaxValidateThreadCount; i++)
			{
				Thread t = new Thread(new ThreadStart(ValidateWork));
				validateThreads.Add(t);
			}
			foreach (Thread t in validateThreads)
			{
				t.Start();
			}
			foreach (Thread t in validateThreads)
			{
				t.Join();
			}
			try
			{
				worker.ReportProgress(100);
			}
			catch (Exception) { }
		}

		private void ValidateWork()
		{
			ProxyInfo pi;
			while (!validateWorker.CancellationPending && (pi = DequeuePendingProxy()) != null)
			{
				if (proxyValidator.ValidateProxy(pi))
				{
//					Console.WriteLine("OK {0} {1} ms", pi.HttpProxy.Address.Authority, pi.RTT);
					
					//enqueue proxy
//					lock (goodProxiesQueue)
//					{
//						goodProxies[pi.HttpProxy.Address.Authority] = pi;
					EnqueueGoodProxy(pi);
//						goodProxiesQueue.Enqueue(pi.RTT, pi);	
//					}
					
					if (OnUsableProxyFound != null)
					{
						OnUsableProxyFound(this, new UsableProxyFoundEventArgs
													{
														ProxyInfo = pi
													});
					}
				}
				else
				{
//					Console.WriteLine("F {0} {1}ms", pi.HttpProxy.Address, pi.RTT);
//					EnqueuePendingProxy(pi);
				}
				proxyValidatedCount++;
				if (proxyValidatedCount > proxyToValidateCount)
				{
					throw new Exception();
				}
				validateWorker.ReportProgress(proxyValidatedCount * 100 / proxyToValidateCount, pi);
			}
		}

		private ProxyInfo DequeuePendingProxy()
		{
			ProxyInfo pi = null;
			lock (pendingProxies)
			{
				if (pendingProxies.Count > 0)
				{
					pi = pendingProxies.Dequeue();
				}
			}
			return pi;
		}

		public void CancelValidation()
		{
			canStop = true;
			foreach (Thread t in validateThreads)
			{
				t.Abort();
			}
		}

		public ProxyInfo DequeueFastestProxy(bool autoRedownload)
		{
			ProxyInfo pi = null;
			lock (goodProxiesQueue)
			{
				if (goodProxiesQueue.Count != 0)
				{
					pi = goodProxiesQueue.Dequeue();
				}
				if (autoRedownload && goodProxiesQueue.Count <= 5)
				{
					StartDownloadProxies(true);
				}
			}
			return pi;
		}
		public void EnqueueGoodProxy(ProxyInfo proxyInfo)
		{
			lock (goodProxiesQueue)
			{
				goodProxiesQueue.Enqueue(proxyInfo.RTT,proxyInfo);
			}
		}
		public void EnqueuePendingProxy(ProxyInfo proxyInfo)
		{
			lock (pendingProxies)
			{
				pendingProxies.Enqueue(proxyInfo);
			}
		}
		public void DeleteProxy(ProxyInfo proxyInfo)
		{
			if (OnProxyRemoved != null)
			{
				OnProxyRemoved(this, new ProxyRemovedEventHandlerArgs
				                     	{
				                     		ProxyInfo = proxyInfo
				                     	});
			}
		}
		public void LoadProxies(IEnumerable<ProxyInfo> proxyInfos)
		{
			lock(pendingProxies)
			{
				foreach (ProxyInfo pi in proxyInfos)
				{
					pendingProxies.Enqueue(pi);
				}
			}
			if (OnPendingProxyListChanged != null)
			{
				OnPendingProxyListChanged(this, new PendingProxyListChangedEventHandlerArgs());
			}
		}
		public void LoadProxyProviders(IEnumerable<AbstractProxyProvider> providers)
		{
			lock (proxyProviders)
			{
				foreach (AbstractProxyProvider ipp in providers)
				{
					proxyProviders.Add(ipp);
				}
			}
			if (OnProxyProviderListChanged != null)
			{
				OnProxyProviderListChanged(this,new ProxyProviderListChangedEventHandlerArgs());
			}
		}
	}

	public delegate void ProxyProviderListChangedEventHandler(object sender, ProxyProviderListChangedEventHandlerArgs args);

	public class ProxyProviderListChangedEventHandlerArgs
	{
	}

	public delegate void ProxyRemovedEventHandler(object sender, ProxyRemovedEventHandlerArgs args);

	public class ProxyRemovedEventHandlerArgs
	{
		private ProxyInfo proxyInfo;

		public ProxyInfo ProxyInfo
		{
			get { return proxyInfo; }
			set { proxyInfo = value; }
		}
	}

	public delegate void DownloadStartedEventHandler(object sender, DownloadStartedEventHandlerArgs args);

	public class DownloadStartedEventHandlerArgs
	{
	}

	public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventHandlerArgs args);

	public class DownloadCompletedEventHandlerArgs
	{
		private int count;

		public int Count
		{
			get { return count; }
			set { count = value; }
		}
	}

	public delegate void PendingProxyListChangedEventHandler(object sender, PendingProxyListChangedEventHandlerArgs args);

	public class PendingProxyListChangedEventHandlerArgs
	{
	}

	public delegate void ValidateStartedEventHandler(object sender, ValidateStartedEventHandlerArgs args);

	public class ValidateStartedEventHandlerArgs
	{
	}
}