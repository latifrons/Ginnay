using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Ginnay.Proxy.SocketManager;

namespace Ginnay.Proxy
{
	public class TargetConnectionPool
	{
		protected Dictionary<string, List<SocketInfo>> socketPool = new Dictionary<string, List<SocketInfo>>();
		protected object socketLock = new object();
		protected bool canStop = false;
		private Thread daemonThread;

		public virtual bool RequestSocketReuse(ClientRequestPacket crp,out SocketInfo socketInfo)
		{
			string requestHost = crp.Host + ":" + crp.Port;
			List<SocketInfo> sockets;
			socketInfo = null;
			lock (socketLock)
			{
				if (!socketPool.TryGetValue(requestHost, out sockets))
				{
					sockets = new List<SocketInfo>(1);
					socketPool[requestHost] = sockets;
					List<SocketInfo> toRemove = new List<SocketInfo>();

					foreach (SocketInfo si in sockets)
					{
						if (!si.InUse)
						{
							if (TouchSocket(si))
							{
								socketInfo = si;
								socketInfo.InUse = true;
								break;
							}
							else
							{
								//remove
								toRemove.Add(si);
							}
						}
					}
					foreach (SocketInfo si in toRemove)
					{
						//si.Socket.Shutdown(SocketShutdown.Both);
						Console.WriteLine("Actively Removed {0}", ((IPEndPoint)si.Socket.RemoteEndPoint).Port);
						si.Socket.Close();
						sockets.Remove(si);
					}
				}
			}
			//if no connection available,return null;
			return socketInfo != null;
		}

		public virtual void ReuseSocket(ClientRequestPacket crp, SocketInfo socketInfo)
		{
			lock (socketPool)
			{
				AddSocket(crp,socketInfo);
				socketInfo.InUse = false;
			}
		}

		public virtual void RemoveSocket(ClientRequestPacket crp, SocketInfo socketInfo)
		{
			string requestHost = crp.Host + ":" + crp.Host;
			List<SocketInfo> sockets;

			lock (socketLock)
			{
				if (socketPool.TryGetValue(requestHost, out sockets))
				{
					sockets.Remove(socketInfo);
					if (sockets.Count == 0)
					{
						socketPool.Remove(requestHost);
					}
				}
			}
		}
		protected virtual void AddSocket(ClientRequestPacket crp, SocketInfo socketInfo)
		{
			string requestHost = crp.Host + ":" + crp.Port;
			List<SocketInfo> sockets;
			Debug.Assert(socketInfo.Socket != null);
			lock (socketLock)
			{
				if (!socketPool.TryGetValue(requestHost, out sockets))
				{
					sockets = new List<SocketInfo>(1);
					socketPool[requestHost] = sockets;
				}
				if (!sockets.Contains(socketInfo))
				{
					sockets.Add(socketInfo);
				}
			}
		}
		public virtual bool TouchSocket(SocketInfo socketInfo)
		{
			bool a = socketInfo.Socket.Poll(-1, SelectMode.SelectWrite);
			return a;
			//			byte[] test = new byte[1];
			//			bool blockingState = socket.Blocking;
			//			try
			//			{
			//				SocketError se;
			//				socket.Blocking = false;
			//				int a = socket.Receive(test, 0,1,SocketFlags.None);
			//				return socket.Connected;
			//			}
			//			catch (SocketException se )
			//			{
			//				if (se.NativeErrorCode.Equals(10035))
			//				{
			//					return true;
			//				}
			//				else
			//				{
			//					return false;
			//				}
			//			}
			//			finally
			//			{
			//				socket.Blocking = blockingState;
			//			}
		}
		public void StartDaemon()
		{
			daemonThread = new Thread(new ThreadStart(MonitorSockets));
			daemonThread.IsBackground = true;
			daemonThread.Start();
		}
		public void StopDaemon()
		{
			if (daemonThread != null)
			{
				daemonThread.Abort();
			}
		}

		void MonitorSockets()
		{
			while (!canStop)
			{
				lock (socketLock)
				{
					Console.WriteLine("Cleaning");

					List<SocketInfo> toRemove = new List<SocketInfo>();
					foreach (KeyValuePair<string, List<SocketInfo>> pair in socketPool)
					{
						foreach (SocketInfo si in pair.Value)
						{
							if (!si.InUse && !TouchSocket(si))
							{
								toRemove.Add(si);
							}
						}
						foreach (SocketInfo si in toRemove)
						{
							si.Socket.Close();
							Console.WriteLine("Clean Removed {0}", ((IPEndPoint)si.Socket.RemoteEndPoint).Port);
							pair.Value.Remove(si);
						}
					}
					Console.WriteLine("CleanOver");
				}
				Thread.Sleep(30000);
			}
		}
	}
}
