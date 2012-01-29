using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Ginnay.ProxySpider
{
	public class ProxyValidator
	{
		public event ProxyValidateConditionChangedEventHandler OnProxyValidateConditionChanged;
		private List<ProxyValidateCondition> validateConditions = new List<ProxyValidateCondition>(1);
//		object locker = new object();

		public List<ProxyValidateCondition> ValidateConditions
		{
			get { return validateConditions; }
			set { validateConditions = value; }
		}
		public void LoadProxyValidations(IEnumerable<ProxyValidateCondition> conditions)
		{
			foreach (ProxyValidateCondition pvc in conditions)
			{
				validateConditions.Add(pvc);
			}
			if (OnProxyValidateConditionChanged != null)
			{
				OnProxyValidateConditionChanged(this,new ProxyValidateConditionChangedEventHandlerArgs());
			}
		}
		public void ReplaceProxyValidations(IEnumerable<ProxyValidateCondition> conditions)
		{
			this.validateConditions.Clear();
			LoadProxyValidations(conditions);
		}

		public bool ValidateProxy(ProxyInfo pi)
		{
			int round = ValidateConditions.Count;
			long totalTime = 0;
			foreach (ProxyValidateCondition c in ValidateConditions)
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
				request.Proxy = pi.HttpProxy;
				request.Timeout = 5000;
				long start = System.Environment.TickCount;
				HttpWebResponse resp = null;
				try
				{
					resp = (HttpWebResponse)request.GetResponse();
					long end = System.Environment.TickCount;
					totalTime += end - start;
				}
				catch (WebException e)
				{
//					lock (locker)
//					File.AppendAllText("debug.txt", pi.ProxyAddress + " " + e.Message + "\n");
					pi.RTT = -1;
					return false;
				}
				if (resp.StatusCode != HttpStatusCode.OK)
				{
//					lock (locker)
//					File.AppendAllText("debug.txt", pi.ProxyAddress + " NOT OK\n");
					pi.RTT = -1;
					return false;
				}

				string html;
				bool success = HtmlHelper.GetHtml(resp, out html);
				resp.Close();
				if (!success)
				{
					pi.RTT = -1;
//					lock (locker)
//					File.AppendAllText("debug.txt", pi.ProxyAddress + " HTML\n");
					return false;
				}
//				lock (locker)
//				File.AppendAllText("debug.txt", pi.ProxyAddress + html + "\n");
				foreach (string s in c.Keywords)
				{
					if (!html.Contains(s))
					{
//						lock (locker)
//						File.AppendAllText("debug.txt", pi.ProxyAddress + "NOT CONTAIN " + s+ "\n");
						pi.RTT = -1;
						return false;
					}
				}
				foreach (string s in c.ForbiddenKeywords)
				{
					if (html.Contains(s))
					{
//						lock (locker)
//						File.AppendAllText("debug.txt", pi.ProxyAddress + "CONTAIN " + s + "\n");
						pi.RTT = -1;
						return false;
					}
				}
			}
			pi.RTT = (int)totalTime/round;
			return true;
		}
	}

	public delegate void ProxyValidateConditionChangedEventHandler(object sender, ProxyValidateConditionChangedEventHandlerArgs args);

	public class ProxyValidateConditionChangedEventHandlerArgs
	{
	}

	public class ProxyValidateCondition
	{
		private string url;
		private List<string> keywords = new List<string>(1);
		private List<string> forbiddenKeywords = new List<string>(1);
		private string keywordsStr;
		private string forbiddenKeywordsStr;

		public string Url
		{
			get { return url; }
			set { url = value; }
		}

		public List<string> Keywords
		{
			get { return keywords; }
			set { keywords = value;
				keywordsStr = null;
			}
		}
		public string KeywordsStr
		{
			get
			{
				StringBuilder sb =new StringBuilder();
				foreach (string s in keywords)
				{
					sb.Append(s).Append(",");
				}
				if (sb.Length > 0)
				{
					return sb.ToString(0, sb.Length - 1);
				}
				else
				{
					return sb.ToString();
				}
				
			}
		}

		public List<string> ForbiddenKeywords
		{
			get { return forbiddenKeywords; }
			set { forbiddenKeywords = value;
				forbiddenKeywordsStr = null;
			}
		}
		public string ForbiddenKeywordsStr
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (string s in forbiddenKeywords)
				{
					sb.Append(s).Append(",");
				}
				if (sb.Length > 0)
				{
					return sb.ToString(0, sb.Length - 1);
				}
				else
				{
					return sb.ToString();
				}
			}
		}

		public string KeywordsString
		{
			get
			{
				if (keywordsStr == null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in keywords)
					{
						sb.Append(s).Append(',');
					}
					keywordsStr = sb.ToString(0, sb.Length - 1);
				}
				return keywordsStr;
			}
		}
		public string ForbiddenKeywordsString
		{
			get
			{
				if (forbiddenKeywordsStr == null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (string s in forbiddenKeywords)
					{
						sb.Append(s).Append(',');
					}
					forbiddenKeywordsStr = sb.ToString(0, sb.Length - 1);
				}
				return forbiddenKeywordsStr;
			}
		}
	}
}
