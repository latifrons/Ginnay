using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Ginnay.Proxy
{
	public class TargetResponsePacket:IncomingPacket
	{
		private bool keepAlive = true;
		private string transferEncoding;
		private string responseCode;
		private string contentType;
		

		public bool KeepAlive
		{
			get { return keepAlive; }
		}

		public string TransferEncoding
		{
			get { return transferEncoding; }
		}
		

		public override void AppendData(byte[] buffer, int offset, int count)
		{
			//TODO body directly forward
			IncomingBuffer.Write(buffer, offset, count);
			int len = (int)IncomingBuffer.Length - incomingOffset;
			//bytes written into the incomingBuffer but not be read
			byte[] incomingBytes = new byte[len];
			IncomingBuffer.Seek(incomingOffset, SeekOrigin.Begin);
			IncomingBuffer.Read(incomingBytes, 0, len);
			string s = Encoding.ASCII.GetString(incomingBytes);
			int i = 0;
			int sOffset = 0;
			while (!HeaderReceived && (i = s.IndexOf("\r\n", sOffset, StringComparison.Ordinal)) >= 0)
			{
				if (i - sOffset == 0 && incomingOffset > 0)
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
			if (HeaderReceived && !ContentReceived)
			{
				if (contentBeginOffset == 0)
				{
					contentBeginOffset = incomingOffset;
				}
				incomingOffset += len - i;
				HandleBody(incomingBytes, i, len - i);
			}
		}

		protected override void HandleHeader(string header)
		{
			if (header.StartsWith("HTTP"))
			{
				string[] ss = header.Split(new char[] {' '});
				if (ss.Length == 3)
				{
					responseCode = ss[1];
				}
				
			}
			else if (header.StartsWith("Connection", StringComparison.OrdinalIgnoreCase))
			{
				if (header.IndexOf("Close", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					//close
					this.keepAlive = false;
				}
				else
				{
					this.keepAlive = true;
				}
			}
			else if (header.StartsWith("Content-Length", StringComparison.OrdinalIgnoreCase))
			{
				contentLength = Int32.Parse(header.Substring(header.IndexOf(':') + 1).TrimStart());
			}
			else if (header.StartsWith("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
			{
				transferEncoding = header.Substring(header.IndexOf(':') + 1).TrimStart();
			}
			else if (header.StartsWith("Content-Type", StringComparison.OrdinalIgnoreCase))
			{
				contentType = header.Substring(header.IndexOf(':') + 1).TrimStart();
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
			
			if (transferEncoding != null && transferEncoding.Equals("chunked", StringComparison.OrdinalIgnoreCase))
			{
				//no content length
				//check if the last 5 bytes are 0 \r \n \r \n
				long bodyBufferLength = contentBuffer.Length;
				if (bodyBufferLength >= 5)
				{
					string lastFive = Encoding.ASCII.GetString(contentBuffer, (int)bodyBufferLength - 5, 5);
					if (lastFive == "0\r\n\r\n")
					{
						ContentReceived = true;
					}
				}
			}
			else
			{
				Debug.Assert(contentLength == 0 || contentReceivedLength <= contentLength);
				if (contentReceivedLength == contentLength)
				{
					ContentReceived = true;
				}
			}
		}

		public override bool ContentShooted
		{
			get
			{
				if (transferEncoding != null && transferEncoding.Equals("chunked",StringComparison.OrdinalIgnoreCase))
				{
					return contentReceived;
				}
				else
				{
					return contentShootedLength == contentLength;
				}
				
			}
		}

		public string ResponseCode
		{
			get { return responseCode; }
		}

		public string ContentType
		{
			get { return contentType; }
			set { contentType = value; }
		}
	}
}
