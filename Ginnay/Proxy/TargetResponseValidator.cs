using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ginnay.Pac;
using Ginnay.ProxySpider;

namespace Ginnay.Proxy
{
	public class TargetResponseValidator
	{
		//Regex charsetRegex = new Regex(@"charset=\s*'?""?(?<charset>.+)'?""?\s*");
		private static Regex charsetRegex = new Regex(@"charset\s*=\s*'?""?(?<charset>[\w-]+)'?""?\s*/?\s*>?");
		private PacSetting pacSetting;

		public PacSetting PacSetting
		{
			get { return pacSetting; }
			set { pacSetting = value; }
		}
		
		public bool ValidateTargetResponse(ClientRequestPacket crp, TargetResponsePacket trp)
		{
			string fullURL;
			
			if (crp.Port == 80)
			{
				fullURL = crp.Protocol + "://" + crp.Host + crp.RelativePath;
			}
			else
			{
				fullURL = crp.Protocol + "://" + crp.Host + ":" + crp.Port + crp.RelativePath;
			}
			string html = ReformHTML(trp);
			return pacSetting.ValidateHtml(fullURL, html);
		}

		private string ReformHTML(TargetResponsePacket trp)
		{
			string contentEncoding = null;
			bool isChunked = false;
			bool isGzip = false;
			foreach (string s in trp.Headers)
			{
				if (s.StartsWith("Content-Type",StringComparison.OrdinalIgnoreCase))
				{
					Match m = charsetRegex.Match(s);
					if (m.Success)
					{
						string charset = m.Groups["charset"].Value;
					}
				}
				else if (s.StartsWith("Content-Encoding",StringComparison.OrdinalIgnoreCase))
				{
					if (s.IndexOf("gzip",StringComparison.OrdinalIgnoreCase)>=0)
					{
						isGzip = true;
					}
				}
				else if (s.StartsWith("Transfer-Encoding",StringComparison.OrdinalIgnoreCase))
				{
					if (s.IndexOf("chunked",StringComparison.OrdinalIgnoreCase)>=0)
					{
						isChunked = true;
					}
				}
			}
			string html;
			byte[] content = trp.IncomingBuffer.ToArray();
			MemoryStream ms = new MemoryStream(content,trp.ContentBeginOffset,content.Length-trp.ContentBeginOffset);
			byte[] array = ms.ToArray();
			if (array.Length> 0)
			{
				HtmlHelper.GetHtml(ms.ToArray(), contentEncoding, isChunked, isGzip, out html);
				return html;
			}
			else
			{
				return null;
			}
		}

		public bool NeedValidate(ClientRequestPacket crp)
		{
			string fullURL;

			if (crp.Port == 80)
			{
				fullURL = crp.Protocol + "://" + crp.Host + crp.RelativePath;
			}
			else
			{
				fullURL = crp.Protocol + "://" + crp.Host + ":" + crp.Port + crp.RelativePath;
			}
			return pacSetting.IsNeedValidate(fullURL);
		}
	}
}
