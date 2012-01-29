using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Ginnay.Proxy
{
	public class ClientRequestPacket:IncomingPacket
	{
		private string method;
		private string host;
		private int port = 80;
		private string relativePath;
		private string protocolVersion;
		private string protocol;

		public string Method
		{
			get { return method; }
			set { method = value; }
		}

		public string Host
		{
			get { return host; }
			set { host = value; }
		}

		public int Port
		{
			get { return port; }
			set { port = value; }
		}

		public override void AppendData(byte[] buffer, int offset,int count)
		{
			//TODO Directly forward
			IncomingBuffer.Write(buffer,offset,count);
			int len =(int) IncomingBuffer.Length - incomingOffset;
			byte[] incomingBytes = new byte[len];
			IncomingBuffer.Seek(incomingOffset,SeekOrigin.Begin);
			IncomingBuffer.Read(incomingBytes, 0, len);
			string s = Encoding.ASCII.GetString(incomingBytes);
			int i = 0;
			int sOffset = 0;
			while (!HeaderReceived && (i = s.IndexOf("\r\n",sOffset,StringComparison.Ordinal)) >=0)
			{
				if (i -sOffset == 0 && incomingOffset > 0)
				{
					//head over
					i += 2;
					incomingOffset += 2;
					HeaderReceived = true;
				}
				else
				{
					string header = s.Substring(sOffset, i - sOffset);
					Headers.Add(header);
					HandleHeader(header);
					incomingOffset += i - sOffset + 2;
					sOffset = i + 2;
				}
			}
			if (HeaderReceived && !ContentReceived && method=="POST")
			{
				incomingOffset += len - i;
				HandleBody(incomingBytes, i, len - i);
			}
		}

		protected override void HandleHeader(string header)
		{
			if (header.StartsWith("GET", StringComparison.OrdinalIgnoreCase) ||
				header.StartsWith("POST", StringComparison.OrdinalIgnoreCase))
			{
				Method = "GET";
				string[] parts = header.Split(new char[] { ' ' });
				if (parts.Length == 3)
				{
					method = parts[0];
					relativePath = parts[1].Substring(parts[1].IndexOf('/', parts[1].IndexOf("//") + 2));
					protocol = parts[1].Substring(0, parts[1].IndexOf(':'));
					ProtocolVersion = parts[2];
				}
				else
				{
					Console.WriteLine("BAD REQUEST {0}", header);
				}
			}
			else if (header.StartsWith("HOST: ",StringComparison.OrdinalIgnoreCase))
			{

				host = header.Substring("HOST: ".Length);
				if (host.IndexOf(':') >= 0)
				{
					//has port
					port = Int32.Parse(host.Substring(host.IndexOf(':') + 1));
					host = host.Substring(0, host.IndexOf(':'));
				}
			}
			else if (header.StartsWith("Content-Length",StringComparison.OrdinalIgnoreCase))
			{
				contentLength = Int32.Parse(header.Substring(header.IndexOf(':') + 1).TrimStart());
			}
		}

		protected override void HandleBody(byte[] buffer, int offset, int count)
		{
			using (MemoryStream body = new MemoryStream())
			{
				body.Write(buffer, offset, count);
				contentBuffer = body.ToArray();
				contentReceivedLength += count;
			}
			Debug.Assert(contentLength == 0 || contentReceivedLength <= contentLength);
			if (contentReceivedLength == contentLength)
			{
				ContentReceived = true;
			}

		}

		public override bool ContentShooted
		{
			get { return contentShootedLength == contentLength; }
		}

		public string RelativePath
		{
			get { return relativePath; }
			set { relativePath = value; }
		}

		public string Protocol
		{
			get { return protocol; }
			set { protocol = value; }
		}

		public string ProtocolVersion
		{
			get { return protocolVersion; }
			set { protocolVersion = value; }
		}
	}
}
