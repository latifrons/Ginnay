using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Ginnay.Proxy.SocketManager;

namespace Ginnay.Proxy
{
	public delegate void ListenerStateChangedEventHandler(object sender, ListenerStateChangedEventArgs args);

	public delegate void ExceptionRaisedEventHandler(object sender, ExceptionRaisedEventArgs e);

	public class Listener
	{
		private TargetConnctionGuide targetConnctionGuide;
		private TargetResponseValidator targetResponseValidator;
		private TargetConnectionPool targetConnectionPool = new TargetConnectionPool();
		private volatile bool running = false;
		private object runningLock  =new object();
		private int port;
		private EventWaitHandle stateEventChanged = new EventWaitHandle(false,EventResetMode.AutoReset);
		private Socket listener;

		public TargetConnctionGuide TargetConnctionGuide
		{
			get { return targetConnctionGuide; }
			set { targetConnctionGuide = value; }
		}

		public TargetConnectionPool TargetConnectionPool
		{
			get { return targetConnectionPool; }
			set { targetConnectionPool = value; }
		}

		public bool Running
		{
			get { return running; }
			set { running = value; }
		}

		public int Port
		{
			get { return port; }
			set { port = value; }
		}

		public TargetResponseValidator TargetResponseValidator
		{
			get { return targetResponseValidator; }
			set { targetResponseValidator = value; }
		}

		public event ListenerStateChangedEventHandler OnListenerStateChanged;
		public event ExceptionRaisedEventHandler OnExceptionRaised;
		public event NewRequestEventHandler OnNewRequest;

		public void StartListener()
		{
			lock (runningLock)
			{
				if (running)
				{
					return;
				}
				
				if (targetConnctionGuide == null)
				{
					throw new Exception("Target socket manager not set");
				}
				listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				
				try
				{
					listener.Bind(new IPEndPoint(IPAddress.Any, port));
					listener.Listen(10);
					running = true;
				}
				catch (SocketException e)
				{
					if (OnExceptionRaised != null)
					{
						OnExceptionRaised(this,new ExceptionRaisedEventArgs
						                       	{
						                       		E = e,
													Explaination = "Bind error. Port in use?"
						                       	});
					}
					return;
				}
				TargetConnectionPool.StartDaemon();
				
			}
			if (OnListenerStateChanged != null)
			{
				ListenerStateChangedEventArgs args = new ListenerStateChangedEventArgs();
				OnListenerStateChanged(this, args);
			}
			while (running)
			{
				Socket clientSocket = null;
				try
				{
					clientSocket = listener.Accept();
				}
				catch { }
				if (clientSocket != null)
				{
					BiConnection client = new BiConnection(this,clientSocket);
					client.TargetConnctionGuide = TargetConnctionGuide;
					client.TargetConnectionPool = TargetConnectionPool;
					client.TargetResponseValidator = targetResponseValidator;
					client.BeginClientReceive();
				}
					
			}
			stateEventChanged.Set();
		}
		public void InvokeNewRequestEvent(NewRequestEventHandlerArgs args)
		{
			if (this.OnNewRequest != null)
			{
				this.OnNewRequest(this, args);
			}
		}
		public void StopListener()
		{
			lock (runningLock)
			{
				if (!running)
				{
					return;
				}
				targetConnectionPool.StopDaemon();
				try
				{
					listener.Close();
					running = false;
				}
				catch (SocketException e)
				{
					if (OnExceptionRaised != null)
					{
						OnExceptionRaised(this, new ExceptionRaisedEventArgs
						{
							E = e,
							Explaination = "Closing Error"
						});
					}
				}
				stateEventChanged.WaitOne();
				
			}
			if (OnListenerStateChanged != null)
			{
				ListenerStateChangedEventArgs args = new ListenerStateChangedEventArgs();
				OnListenerStateChanged(this, args);
			}
		}
	}

	public delegate void NewRequestEventHandler(object sender, NewRequestEventHandlerArgs args);

	public class NewRequestEventHandlerArgs
	{
		private string url;

		public string Url
		{
			get { return url; }
			set { url = value; }
		}
	}

	public class ListenerStateChangedEventArgs
	{
	}
	public class ExceptionRaisedEventArgs
	{
		private Exception e;
		private string explaination;

		public string Explaination
		{
			get { return explaination; }
			set { explaination = value; }
		}

		public Exception E
		{
			get { return e; }
			set { e = value; }
		}
	}
}
