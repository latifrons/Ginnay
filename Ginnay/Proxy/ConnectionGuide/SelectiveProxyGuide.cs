using System;
using System.Net;
using System.Threading;
using Ginnay.Pac;
using Ginnay.ProxySpider;

namespace Ginnay.Proxy.SocketManager
{
	public class SelectiveProxyGuide : TargetConnctionGuide
	{
		protected int MAX_ERROR_TIMES = 3;

		protected IPAddress[] currentProxyIPAddress;
		protected ProxyInfo currentProxyInfo;
		protected int currentProxyPort;
		protected DNSCache dnsCache;

		protected int errorTimes = 0;
		protected PacSetting pacSetting;
		protected ProxyManager proxyManager;
		protected ReaderWriterLockSlim rwl = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public DNSCache DnsCache
		{
			get { return dnsCache; }
			set { dnsCache = value; }
		}

		public PacSetting PacSetting
		{
			get { return pacSetting; }
			set { pacSetting = value; }
		}

		public ProxyInfo CurrentProxyInfo
		{
			get { return currentProxyInfo; }
		}

		public ProxyManager ProxyManager
		{
			get { return proxyManager; }
			set
			{
				proxyManager = value;
				proxyManager.OnUsableProxyFound += new UsableProxyFoundEventHandler(DoUsableProxyFound);
				proxyManager.OnValidateStarted += new ValidateStartedEventHandler(DoValidateStarted);
			}
		}

		private void DoValidateStarted(object sender, ValidateStartedEventHandlerArgs args)
		{
			rwl.EnterWriteLock();
			ChangeProxy(null, ProxyChangePolicy.TO_GOOD);
			rwl.ExitWriteLock();
		}

		private void DoUsableProxyFound(object sender, UsableProxyFoundEventArgs args)
		{
			rwl.EnterWriteLock();
			{
				if (currentProxyInfo == null || currentProxyInfo.RTT > args.ProxyInfo.RTT)
				{
					ProxyInfo pi = proxyManager.DequeueFastestProxy(false);
					ChangeProxy(pi, ProxyChangePolicy.TO_GOOD);
				}
			}
			rwl.ExitWriteLock();
		}

		public override SocketInfo RequireSocketInfo(ClientRequestPacket crp)
		{
			var si = new SocketInfo();
			if (pacSetting.IsThroughProxy(crp.Protocol, crp.Host, crp.Port, crp.RelativePath))
			{
				rwl.EnterUpgradeableReadLock();
				si.Ips = currentProxyIPAddress;
				si.Port = currentProxyPort;
				if (si.Ips == null)
				{
					ProxyInfo newProxy = proxyManager.DequeueFastestProxy(true);
					ChangeProxy(newProxy, ProxyChangePolicy.DELETE);
				}
				si.Ips = currentProxyIPAddress;
				si.Port = currentProxyPort;
				rwl.ExitUpgradeableReadLock();

				//through proxy
				if (si.Ips == null)
				{
					return null;
				}
				si.Direct = false;
				si.InUse = true;
			}
			else
			{
				//direct
				si.Ips = dnsCache.GetIPAddress(crp.Host);
				if (si.Ips == null)
				{
					return null;
				}
				si.Direct = true;
				si.InUse = true;
				si.Port = crp.Port;
				si.ReUse = false;
			}
			return si;
		}
		public void ChangeProxy(ProxyChangePolicy policy)
		{
			rwl.EnterWriteLock();
			ProxyInfo newProxy = proxyManager.DequeueFastestProxy(true);
			ChangeProxy(newProxy,policy);
			rwl.ExitWriteLock();
		}

		public void ChangeProxy(ProxyInfo newProxy, ProxyChangePolicy policy)
		{
			rwl.EnterWriteLock();
			errorTimes = 0;
			var args = new ProxyUsingChangedEvnetHandlerArgs();
			args.OldProxy = currentProxyInfo;
			if (currentProxyInfo != null)
			{
				switch (policy)
				{
					case ProxyChangePolicy.TO_PENDING:
						proxyManager.EnqueuePendingProxy(currentProxyInfo);
						break;
					case ProxyChangePolicy.TO_GOOD:
						proxyManager.EnqueueGoodProxy(currentProxyInfo);
						break;
					case ProxyChangePolicy.DELETE:
						proxyManager.DeleteProxy(currentProxyInfo);
						break;
					default:
						throw new ArgumentOutOfRangeException("policy");
				}
			}

			currentProxyInfo = newProxy;
			args.NewProxy = currentProxyInfo;

			if (currentProxyInfo != null)
			{
				IPAddress ip;
				if (IPAddress.TryParse(currentProxyInfo.HttpProxy.Address.Host, out ip))
				{
					currentProxyIPAddress = new IPAddress[1] {ip};
				}
				else
				{
					//host name is not an ip address
					currentProxyIPAddress = dnsCache.GetIPAddress(currentProxyInfo.HttpProxy.Address.Host);
				}
				currentProxyPort = currentProxyInfo.HttpProxy.Address.Port;
			}
			else
			{
				currentProxyIPAddress = null;
				currentProxyPort = 80;
			}
			if (OnProxyUsingChanged != null)
			{
				OnProxyUsingChanged(this, args);
			}
			rwl.ExitWriteLock();
		}

		public override void ReportErrorSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo)
		{
			rwl.EnterWriteLock();
			{
				errorTimes++;
				if (errorTimes >= MAX_ERROR_TIMES && failedSocketInfo.Ips == currentProxyIPAddress)
				{
					ProxyInfo newProxy = proxyManager.DequeueFastestProxy(true);
					ChangeProxy(newProxy, ProxyChangePolicy.TO_PENDING);
				}
			}
			rwl.ExitWriteLock();
		}

		public override void ReportNotSuitableSocketInfo(ClientRequestPacket crp, SocketInfo failedSocketInfo)
		{
			rwl.EnterWriteLock();
			{
				//change immediately
				if (failedSocketInfo.Ips == currentProxyIPAddress)
				{
					ProxyInfo newProxy = proxyManager.DequeueFastestProxy(true);
					ChangeProxy(newProxy, ProxyChangePolicy.DELETE);
				}
			}
			rwl.ExitWriteLock();
		}

		public event ProxyUsingChangedEvnetHandler OnProxyUsingChanged;
	}

	public delegate void ProxyUsingChangedEvnetHandler(object sender, ProxyUsingChangedEvnetHandlerArgs args);

	public class ProxyUsingChangedEvnetHandlerArgs
	{
		private ProxyInfo newProxy;
		private ProxyInfo oldProxy;

		public ProxyInfo OldProxy
		{
			get { return oldProxy; }
			set { oldProxy = value; }
		}

		public ProxyInfo NewProxy
		{
			get { return newProxy; }
			set { newProxy = value; }
		}
	}

	public enum ProxyChangePolicy
	{
		TO_PENDING,
		TO_GOOD,
		DELETE
	}
}