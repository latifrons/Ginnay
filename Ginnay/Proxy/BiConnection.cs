using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Ginnay.Proxy.SocketManager;

namespace Ginnay.Proxy
{
	public class BiConnection
	{
		public const int BUFFER_SIZE = 4096;
		private byte[] clientBuffer = new byte[BUFFER_SIZE];
		private byte[] targetBuffer = new byte[BUFFER_SIZE];

		private Socket clientSocket;
		private ClientRequestPacket crp;
		private Socket targetSocket;
		private SocketInfo targetSocketInfo;
		private TargetResponsePacket trp;

		private int retryTimes;

		private TargetConnectionPool targetConnectionPool;
		private TargetConnctionGuide targetConnctionGuide;
		private TargetResponseValidator targetResponseValidator;

		private Listener listener;

		public BiConnection(Listener listener, Socket clientSocket)
		{
			this.listener = listener;
			this.clientSocket = clientSocket;
		}

		public Socket ClientSocket
		{
			get { return clientSocket; }
			set { clientSocket = value; }
		}

		public TargetConnectionPool TargetConnectionPool
		{
			get { return targetConnectionPool; }
			set { targetConnectionPool = value; }
		}

		public TargetConnctionGuide TargetConnctionGuide
		{
			get { return targetConnctionGuide; }
			set { targetConnctionGuide = value; }
		}

		public TargetResponseValidator TargetResponseValidator
		{
			get { return targetResponseValidator; }
			set { targetResponseValidator = value; }
		}

		public void BeginClientReceive()
		{
			try
			{
				clientSocket.BeginReceive(clientBuffer, 0, BiConnection.BUFFER_SIZE,
					SocketFlags.None, new AsyncCallback(OnClientReceived), null);
			}
			catch (SocketException)
			{
				RetryOrDispose(true,false,false);
				return;
			}

		}
		protected void OnClientReceived(IAsyncResult ir)
		{
			int len = 0;
			try
			{
				SocketError se;
				len = clientSocket.EndReceive(ir, out se);
			}
			catch (SocketException)
			{
				//client failed.
				RetryOrDispose(true,false,false);
				return;
			}
			if (len <= 0)
			{
				RetryOrDispose(true,false,false);
				return;
			}
			if (crp == null)
			{
				crp = new ClientRequestPacket();
			}

			crp.AppendData(clientBuffer, 0, len);
			if (crp.HeaderReceived && !crp.HeaderShooted)
			{
				//shoot header;
				this.listener.InvokeNewRequestEvent(new NewRequestEventHandlerArgs
				                                    	{
				                                    		Url = crp.Headers[0]
				                                    	});
				ConnectTarget();
			}
			else if (crp.HeaderShooted && !crp.ContentShooted)
			{
				if (crp.ContentBuffer != null && crp.ContentBuffer.Length > 0)
				{
					try
					{
						targetSocket.BeginSend(crp.ContentBuffer, 0, crp.ContentBuffer.Length, SocketFlags.None,
											   new AsyncCallback(OnTargetContentSent), targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false,true,false);
						return;
					}
				}
			}
		}

		protected void ConnectTarget()
		{
//			Console.WriteLine("{0} {1} {2} {3}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				"connect", crp.Headers[0]);
			bool success = targetConnectionPool.RequestSocketReuse(crp, out targetSocketInfo);
			if (success)
			{
				targetSocket = targetSocketInfo.Socket;
				ForwardRequestHeader();
			}else
			{
				//no connection in the pool
				Debug.Assert(crp != null);
				targetSocketInfo = targetConnctionGuide.RequireSocketInfo(crp);
				if (targetSocketInfo == null)
				{
					SendBack502();
					return;
				}
				targetSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				targetSocket.ReceiveTimeout = 5000;
				targetSocket.SendTimeout = 5000;
				targetSocketInfo.Socket = targetSocket;
				try{
					IAsyncResult ar = targetSocket.BeginConnect(targetSocketInfo.Ips,targetSocketInfo.Port,new AsyncCallback(OnTargetConnected),targetSocket);
				}
				catch (SocketException)
				{
					RetryOrDispose(false,true,false);
				}
			}
		}

		private void OnTargetConnected(IAsyncResult ar)
		{
			try{
				targetSocket.EndConnect(ar);
			}catch (SocketException)
			{
				RetryOrDispose(false,true,false);
				return;
			}
			ForwardRequestHeader();
		}

		protected void ForwardRequestHeader()
		{
			string newRequest = ReBuildRequest(crp.Headers);
			byte[] newRequestBytes = Encoding.ASCII.GetBytes(newRequest);
			//			Console.WriteLine("{0} {1} {2} {3} {4} \n{5}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
			//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
			//				((IPEndPoint)socket.LocalEndPoint).Port,
			//				"out", crp.Headers[0],newRequest);

//			Console.WriteLine("{0} {1} {2} {3} {4}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				((IPEndPoint)targetSocket.LocalEndPoint).Port,
//				"out", crp.Headers[0]);
			try
			{
				targetSocket.BeginSend(newRequestBytes, 0, newRequestBytes.Length, SocketFlags.None, new AsyncCallback(OnTargetHeaderSent), targetSocket);
			}
			catch (SocketException)
			{
				RetryOrDispose(false,true,false);
				return;
			}
		}

		protected void OnTargetHeaderSent(IAsyncResult ar)
		{
			try
			{
				int len = targetSocket.EndSend(ar);
				Debug.Assert((len > 0));

			}
			catch (SocketException)
			{
				RetryOrDispose(false, true, false);
				return;
			}
			crp.HeaderShooted = true;
			//send body if available
			if (!crp.ContentShooted)
			{
				if (crp.ContentBuffer != null && crp.ContentBuffer.Length > 0)
				{
					try
					{
						targetSocket.BeginSend(crp.ContentBuffer, 0, crp.ContentBuffer.Length, SocketFlags.None,
											   new AsyncCallback(OnTargetContentSent), targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, false);
						return;
					}
				}
				else
				{
					try
					{
						clientSocket.BeginReceive(clientBuffer, 0, BiConnection.BUFFER_SIZE,
				SocketFlags.None, new AsyncCallback(OnClientReceived), null);
					}
					catch (SocketException)
					{
						RetryOrDispose(true, false, false);
						return;
					}
				}
			}
			else
			{
				//listen to target's reply
				if (crp.HeaderShooted && crp.ContentShooted)
				{
					try
					{
						targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
											  targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, false);
						return;
					}
				}
			}

		}

		protected void OnTargetContentSent(IAsyncResult ar)
		{
			Socket s = (Socket)ar.AsyncState;
			int sentLength = s.EndSend(ar);
			crp.ContentShootedLength += sentLength;
			if (crp.ContentShooted)
			{
				try
				{
					targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										  targetSocket);
				}
				catch (SocketException)
				{
					RetryOrDispose(false, true, false);
					return;
				}
			}
			else
			{
				try
				{
					clientSocket.BeginReceive(clientBuffer, 0, BiConnection.BUFFER_SIZE,
						SocketFlags.None, new AsyncCallback(OnClientReceived), null);
				}
				catch (SocketException)
				{
					RetryOrDispose(true,false,true);
					return;
				}
			}
		}

		protected void OnTargetReceived(IAsyncResult ar)
		{
			int len = 0;
			try
			{
				len = targetSocket.EndReceive(ar);
			}
			catch (SocketException)
			{
				RetryOrDispose(false, true, true);
				return;
			}
			//packet over
			//if (len <= 0 && trp != null && !trp.KeepAlive)
//			Console.WriteLine(len);
			if (len <= 0)
			{
				if (trp == null|| trp.KeepAlive)
				{
					//reconnect or kill
					RetryOrDispose(false, true, false);
					return;
				}
				else
				{
					trp.ContentReceived = true;
				}
			}
			if (trp == null)
			{
				trp = new TargetResponsePacket();
			}

			trp.AppendData(targetBuffer, 0, len);

			//shoot
			//if need validate, wait for the return of entire packet
			Console.WriteLine("{0} {1} {2}",len,trp.ContentLength,trp.IncomingBuffer.Length);
			if (targetResponseValidator.NeedValidate(crp))
			{
				Console.WriteLine("Need Validate for {0} {1} {2} len = {3}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address,len);
				if (!trp.HeaderReceived || !trp.ContentReceived)
				{
					//continue receiving
					//header not complete.
					Console.WriteLine("continue receiving for {0} {1} {2}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address);
					try
					{
						targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, false);
						return;
					}
				}
				else
				{
					//check if the response is 200ok
					if (trp.ResponseCode == "200" && trp.ContentType!= null && trp.ContentType.Contains("text"))
					{
						//validate
						if (targetResponseValidator.ValidateTargetResponse(crp, trp))
						{
							//shoot the entire packet
							Console.WriteLine("Validation OK for {0} {1} {2}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address);
							ForwardFullResponse();
						}
						else
						{
							Console.WriteLine("Validation FFFF for {0} {1} {2}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address);
							NotSuitableProxy();
						}
					}
					else
					{
						Console.WriteLine("Validation Skipped for {0} {1} {2}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address);
						ForwardFullResponse();
					}
				}
			}
			else
			{
				Console.WriteLine("No need for {0} {1} {2}", crp.Host, crp.RelativePath, ((IPEndPoint)targetSocket.RemoteEndPoint).Address);
				//directly forward the response
				if (trp.HeaderReceived && !trp.HeaderShooted)
				{
					//shoot header;
					ForwardResponse();
				}
				else if (trp.HeaderShooted)
				{
					if (trp.ContentBuffer != null && trp.ContentBuffer.Length > 0)
					{
						try
						{
							clientSocket.BeginSend(trp.ContentBuffer, 0, trp.ContentBuffer.Length, SocketFlags.None,
												   new AsyncCallback(OnClientContentSent), clientSocket);
						}
						catch (SocketException)
						{
							RetryOrDispose(true, false, true);
							return;
						}
					}
				}
				else
				{
					//header not complete.
					try
					{
						targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, true);
						return;
					}
				}
			}
		}

		private void ForwardFullResponse()
		{
			string newResponse = ReBuildResponse(trp.Headers);
			byte[] newResponseBytes = Encoding.ASCII.GetBytes(newResponse);
			//			Console.WriteLine("{0} {1} {2} {3} {4}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
			//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
			//				((IPEndPoint)targetSocket.LocalEndPoint).Port,
			//				"in ", trp.Headers[0]);
			try
			{
				clientSocket.BeginSend(newResponseBytes, 0, newResponseBytes.Length, SocketFlags.None,
					new AsyncCallback(OnClientHeaderSentFull), clientSocket);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}
		}

		private void OnClientHeaderSentFull(IAsyncResult ar)
		{
			try
			{
				clientSocket.EndSend(ar);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}
			trp.HeaderShooted = true;

			//send body if available
			byte[] content = trp.IncomingBuffer.ToArray();
			MemoryStream ms = new MemoryStream(content,trp.ContentBeginOffset,content.Length-trp.ContentBeginOffset);
			byte[] array = ms.ToArray();
			if (array.Length >0)
			{
				try
				{
					clientSocket.BeginSend(array, 0, array.Length, SocketFlags.None,
											new AsyncCallback(OnClientContentSent), clientSocket);
				}
				catch (SocketException)
				{
					RetryOrDispose(true, false, true);
					return;
				}
			}
			else
			{
				if (!trp.ContentReceived)
				{
					try
					{
						targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, true);
						return;
					}
				}
				else
				{
					Clean();
				}

			}
		}

		protected void ForwardResponse()
		{
			string newResponse = ReBuildResponse(trp.Headers);
			byte[] newResponseBytes = Encoding.ASCII.GetBytes(newResponse);
//			Console.WriteLine("{0} {1} {2} {3} {4}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				((IPEndPoint)targetSocket.LocalEndPoint).Port,
//				"in ", trp.Headers[0]);
			try
			{
				clientSocket.BeginSend(newResponseBytes, 0, newResponseBytes.Length, SocketFlags.None,
					new AsyncCallback(OnClientHeaderSent), clientSocket);
			}
			catch (SocketException)
			{
				RetryOrDispose(true,false,true);
				return;
			}
		}

		protected void OnClientHeaderSent(IAsyncResult ar)
		{
			try
			{
				clientSocket.EndSend(ar);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}
			trp.HeaderShooted = true;
			//send body if available
			if (trp.ContentBuffer != null && trp.ContentBuffer.Length > 0)
			{
				try
				{
					clientSocket.BeginSend(trp.ContentBuffer, 0, trp.ContentBuffer.Length, SocketFlags.None,
											new AsyncCallback(OnClientContentSent), clientSocket);
				}
				catch (SocketException)
				{
					RetryOrDispose(true, false, true);
					return;
				}
			}
			else
			{
				if (!trp.ContentReceived)
				{
					try
					{
						targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										targetSocket);
					}
					catch (SocketException)
					{
						RetryOrDispose(false, true, true);
						return;
					}
				}
				else
				{
					Clean();
				}

			}
		}

		protected void OnClientContentSent(IAsyncResult ar)
		{
			Socket s = (Socket)ar.AsyncState;
			try
			{
				int sentLength = s.EndSend(ar);
//				Console.WriteLine("to client:{0}", sentLength);
				trp.ContentShootedLength += sentLength;
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}

			if (trp.ContentShooted)
			{
				Clean();
			}
			else
			{
				try
				{
					targetSocket.BeginReceive(targetBuffer, 0, BUFFER_SIZE, SocketFlags.None, new AsyncCallback(OnTargetReceived),
										  targetSocket);
				}
				catch (SocketException)
				{
					RetryOrDispose(false, true, true);
					return;
				}
			}
		}
		protected void SendBack502()
		{
			string ret = "HTTP/1.1 502 Ginnay - Connection Failed\r\nContent-Type: text/html; charset=UTF-8\r\nConnection: close\r\n\r\n[Ginnay: Connection Failed]";
			byte[] retBytes = Encoding.ASCII.GetBytes(ret);
//			Console.WriteLine("{0} {1} {2} {3} {4}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				0,
//				"in ", crp.Headers[0]);
			try
			{
				clientSocket.BeginSend(retBytes, 0, retBytes.Length, SocketFlags.None, new AsyncCallback(OnClient502Sent),
									   clientSocket);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}
		}

		protected void OnClient502Sent(IAsyncResult ar)
		{
			Socket s = (Socket)ar.AsyncState;
			try
			{
				s.EndSend(ar);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, true);
				return;
			}
			RetryOrDispose(false, false, true);
		}

		protected void Clean()
		{
			if (trp.KeepAlive)
			{
				targetSocket = null;
				targetConnectionPool.ReuseSocket(crp, targetSocketInfo);
			}
			else
			{
				targetSocket = null;
				targetConnectionPool.RemoveSocket(crp, targetSocketInfo);
			}

			targetSocket = null;
			//clear both buffers
			crp = null;
			trp = null;
			try
			{
				clientSocket.BeginReceive(clientBuffer, 0, BiConnection.BUFFER_SIZE,
					SocketFlags.None, new AsyncCallback(OnClientReceived), null);
			}
			catch (SocketException)
			{
				RetryOrDispose(true, false, false);
			}
		}

		protected string ReBuildRequest(List<string> headers)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in headers)
			{
				if (targetSocketInfo.Direct && s.StartsWith("Proxy-Connection: ", StringComparison.OrdinalIgnoreCase))
				{
					sb.Append(s.Replace("Proxy-Connection", "Connection"));
				}
				else if (targetSocketInfo.Direct && s.StartsWith("GET", StringComparison.OrdinalIgnoreCase) ||
					s.StartsWith("POST", StringComparison.OrdinalIgnoreCase))
				{
					sb.Append(crp.Method).Append(' ');
					sb.Append(crp.RelativePath).Append(' ');
					sb.Append(crp.ProtocolVersion);
				}
				else
				{
					sb.Append(s);
				}
				sb.Append("\r\n");
			}
			sb.Append("\r\n");
			return sb.ToString();
		}
		protected string ReBuildResponse(List<string> headers)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string s in headers)
			{
				if (s.StartsWith("Connection:"))
				{
					sb.Append(s.Replace("Connection", "Proxy-Connection"));
				}
				else
				{
					sb.Append(s);
				}
				sb.Append("\r\n");
			}
			sb.Append("\r\n");
			return sb.ToString();
		}
		public void NotSuitableProxy()
		{
			if (targetSocket != null)
			{
				targetSocket.Close();
				targetSocket = null;
			}
			if (targetSocketInfo != null)
			{
				targetConnectionPool.RemoveSocket(crp, targetSocketInfo);
			}
			trp = null;
			targetConnctionGuide.ReportNotSuitableSocketInfo(crp, targetSocketInfo);
			targetSocket = null;
			targetSocketInfo = null;
			ConnectTarget();
		}
		public void RetryOrDispose(bool clientFault, bool targetFault, bool forceDispose)
		{
			bool disposeClient = clientFault || forceDispose;
			bool disposeTarget = targetFault || forceDispose;


//			Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} \n{7}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				clientSocket == null ? 0 : ((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				targetSocket == null ? 0 : ((IPEndPoint)targetSocket.LocalEndPoint).Port,
//				"disposed",clientFault,targetFault,forceDispose,
//				crp ==null? "-":crp.Headers[0]);

			if (disposeClient)
			{
				if (clientSocket != null)
				{
					clientSocket.Close();
				}
				if (targetSocketInfo != null)
				{
					if (disposeTarget)
					{
						if (targetSocket != null)
						{
							targetSocket.Close();
							targetConnectionPool.RemoveSocket(crp, targetSocketInfo);
							
						}
						targetSocket = null;
						targetSocketInfo = null;
					}
					else
					{
						if (targetSocket != null)
						{
							targetConnectionPool.ReuseSocket(crp, targetSocketInfo);
						}
						targetSocket = null;
						targetSocketInfo = null;
					}
				}
			}
			else if (disposeTarget)
			{
				if (targetSocket != null)
				{
					targetSocket.Close();
					targetSocket = null;
				}
				if (targetSocketInfo != null)
				{
					targetConnectionPool.RemoveSocket(crp, targetSocketInfo);
				}
				trp = null;
				if (forceDispose)
				{
					targetSocket = null;
					targetSocketInfo = null;
					SendBack502();
				}
				else if(targetSocketInfo != null && targetSocketInfo.ReUse)
				{
					targetSocket = null;
					targetSocketInfo = null;
					ConnectTarget();
				}
				else
				{
					//fresh connection fails,report proxy fails if retry times >=2
					if (retryTimes < 3)
					{
						retryTimes ++;
						targetConnctionGuide.ReportErrorSocketInfo(crp, targetSocketInfo);
						targetSocket = null;
						targetSocketInfo = null;
						ConnectTarget();
					}else
					{
						//abandom
//						Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} \n{7}", ((IPEndPoint)clientSocket.RemoteEndPoint).Port,
//				clientSocket == null ? 0 : ((IPEndPoint)clientSocket.LocalEndPoint).Port,
//				targetSocket == null ? 0 : ((IPEndPoint)targetSocket.LocalEndPoint).Port,
//				"3 times dispose", clientFault, targetFault, forceDispose,
//				crp == null ? "-" : crp.Headers[0]);
						targetSocket = null;
						targetSocketInfo = null;
						SendBack502();
					}
				}
			}
		}
	}
}
