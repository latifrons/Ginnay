using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ginnay.Proxy
{
	public abstract class IncomingPacket
	{
		protected List<string> headers = new List<string>();
		//		protected MemoryStream body;
		protected bool contentReceived = false;
		protected bool headerReceived = false;
		protected bool headerShooted = false;


		protected MemoryStream incomingBuffer = new MemoryStream();
		protected int incomingOffset = 0;
		protected byte[] contentBuffer;

		protected int contentLength = 0;
		protected int contentReceivedLength = 0;
		protected int contentShootedLength = 0;
		protected int contentBeginOffset = 0;

		~IncomingPacket()
		{
			if (this.incomingBuffer != null)
			{
				this.incomingBuffer.Close();
				this.incomingBuffer = null;
			}
		}

		public List<string> Headers
		{
			get { return headers; }
			set { headers = value; }
		}

		//		public MemoryStream Body
		//		{
		//			get { return body; }
		//			set { body = value; }
		//		}
		public int ContentLength
		{
			get { return contentLength; }
			set { contentLength = value; }
		}

		public byte[] ContentBuffer
		{
			get { return contentBuffer; }
			set { contentBuffer = value; }
		}

		public bool HeaderReceived
		{
			get { return headerReceived; }
			set { headerReceived = value; }
		}

		public bool ContentReceived
		{
			get { return contentReceived; }
			set { contentReceived = value; }
		}

		public abstract void AppendData(byte[] buffer, int offset, int count);

		protected abstract void HandleHeader(string header);
		protected abstract void HandleBody(byte[] buffer, int offset, int count);


		public bool HeaderShooted
		{
			get { return headerShooted; }
			set { headerShooted = value; }
		}

		public abstract bool ContentShooted { get; }

		public int ContentShootedLength
		{
			get { return contentShootedLength; }
			set { contentShootedLength = value; }
		}

		public int ContentBeginOffset
		{
			get { return contentBeginOffset; }
			set { contentBeginOffset = value; }
		}

		public MemoryStream IncomingBuffer
		{
			get { return incomingBuffer; }
			set { incomingBuffer = value; }
		}
	}
}
