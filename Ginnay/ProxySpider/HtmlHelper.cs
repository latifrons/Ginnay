using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Ginnay.ProxySpider
{
	public class HtmlHelper
	{
		//private static Regex charsetRegex = new Regex(@"<meta.*?charset\s*=\s*(?<charset>[\w-]+)");
		private static Regex charsetRegex = new Regex(@"charset\s*=\s*'?""?(?<charset>[\w-]+)'?""?\s*/?\s*>?");
		public static byte[] DeChunk(byte[] bytes)
		{
			MemoryStream target = new MemoryStream();
			int offset = 0;
			int begin = 0;
			int stop = bytes.Length - 1;
			while (offset < stop)
			{
				if (bytes[offset] == 0xd && bytes[offset + 1] == 0xa)
				{
					string lenHex = Encoding.ASCII.GetString(bytes, begin, offset - begin);
					//lenHex = "0x" + lenHex;
					int len = Int32.Parse(lenHex, NumberStyles.HexNumber);
					offset += 2;
					begin = offset;
					//copy len to target
					target.Write(bytes, begin, len);
					offset += len;
					offset += 2;
					begin = offset;
				}
				else
				{
					offset++;
				}
			}

			return target.ToArray();
		}

		public static bool GetHtml(byte[] bytes, string contentEncoding,bool isChunked,bool isGzip, out string html)
		{
			if (isChunked)
			{
				bytes = DeChunk(bytes);
			}

			Encoding encoding = Encoding.ASCII;
			if (contentEncoding != null)
			{
				encoding = Encoding.GetEncoding(contentEncoding);
			}

			if (isGzip)
			{
				Stream decoded = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
				byte[] buffer = new byte[4096];
				
				int offset = 0;
				int len = 0;
				MemoryStream ms = new MemoryStream();
				while ((len = decoded.Read(buffer, offset, 4096))>0)
				{
					ms.Write(buffer,0,len);
				}
				decoded.Close();
				bytes = ms.ToArray();
				html = encoding.GetString(bytes);
			}
			else
			{
				html = encoding.GetString(bytes);
			}
			//check if the encoding is set in the html
			//if so, fix it
			Match m = charsetRegex.Match(html);
			if (m.Success)
			{
				//fix encoding
				string charset = m.Groups["charset"].Value;
				Encoding fixEncoding = Encoding.GetEncoding(charset);
				html = fixEncoding.GetString(bytes);
			}
			return true;
		}
		public static bool GetHtml(HttpWebResponse response, out string html)
		{
			if (response.ContentType.IndexOf("text") < 0)
			{
				html = null;
				return false;
			}
			Stream decoded;
			if (response.ContentEncoding.Equals("gzip", StringComparison.OrdinalIgnoreCase))
			{
				Stream s = response.GetResponseStream();
				s.ReadTimeout = 5000;
				decoded = new GZipStream(s, CompressionMode.Decompress);
			}
			else
			{
				decoded = response.GetResponseStream();
				decoded.ReadTimeout = 5000;
			}
			
			Encoding encoding = Encoding.GetEncoding(response.CharacterSet);
			try
			{
				html = new StreamReader(decoded, encoding).ReadToEnd();
			}
			catch (Exception)
			{
				html = null;
				return false;
			}

			//check if the encoding is set in the html
			//if so, fix it
			Match m = charsetRegex.Match(html);
			if (m.Success)
			{
				//fix encoding
				string charset = m.Groups["charset"].Value;
				Encoding fixEncoding = Encoding.GetEncoding(charset);
				html = fixEncoding.GetString(encoding.GetBytes(html));
			}
			return true;
		}

		public static void HttpWebRequestNormalSetup(HttpWebRequest request)
		{
			request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.205 Safari/534.16";
			request.Accept = @"application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";
			request.Headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip,deflate,sdch");
			request.Headers.Add(HttpRequestHeader.AcceptLanguage, @"en-US,en;q=0.8");
			request.Headers.Add(HttpRequestHeader.AcceptCharset, @"ISO-8859-1,utf-8;q=0.7,*;q=0.3");

		}
	}
}
