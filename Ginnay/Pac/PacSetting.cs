using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ginnay.Pac
{
	public class PacSetting
	{
//		private Dictionary<string, URLPatternRegex> throughMePatterns = new Dictionary<string, URLPatternRegex>(); 
		private List<URLPattern> throughMePatterns = new List<URLPattern>();
		private List<URLPattern> needValidatePatterns = new List<URLPattern>(); 

		private string myAddress;
		private string otherProxyAddress;

		public event ThroughMePatternsChangedEventHandler OnThroughMePatternsChanged;

		public string MyAddress
		{
			get { return myAddress; }
			set { myAddress = value; }
		}

		public string OtherProxyAddress
		{
			get { return otherProxyAddress; }
			set { otherProxyAddress = value; }
		}

		public List<URLPattern> ThroughMePatterns
		{
			get { return throughMePatterns; }
			set { throughMePatterns = value; }
		}

		public List<URLPattern> NeedValidatePatterns
		{
			get { return needValidatePatterns; }
			set { needValidatePatterns = value; }
		}

//		public Dictionary<string, URLPatternRegex> ThroughMePatterns
//		{
//			get { return throughMePatterns; }
//		}
		public void Clear()
		{
			throughMePatterns.Clear();
			needValidatePatterns.Clear();
		}

		public bool IsThroughProxy(string fullURL)
		{
			foreach (URLPattern upr in ThroughMePatterns)
			{
				if (upr.Enabled && upr.UrlPatternRegex.IsMatch(fullURL))
				{
					Console.WriteLine("OK {0}", fullURL);
					return true;
				}
			}
			Console.WriteLine("NO {0}", fullURL);
			return false;
		}
		public bool IsThroughProxy(string protocol, string host, int port, string relativePath)
		{
			string fullURL;
			if (port == 80)
			{
				fullURL = protocol + "://" + host + relativePath;
			}
			else
			{
				fullURL = protocol + "://" + host + ":" + port + relativePath;
			}
			return IsThroughProxy(fullURL);
		}
		public void LoadURLPatterns(IEnumerable<URLPattern> urlPatterns)
		{
			foreach (URLPattern up in urlPatterns)
			{
				AddURLPattern(up);
			}
			if (OnThroughMePatternsChanged!= null)
			{
				OnThroughMePatternsChanged(this,new ThroughMePatternsChangedEventHandlerArgs());
			}
		}
		public void LoadURLPattern(URLPattern urlPattern)
		{
			AddURLPattern(urlPattern);
			if (OnThroughMePatternsChanged!= null)
			{
				OnThroughMePatternsChanged(this,new ThroughMePatternsChangedEventHandlerArgs());
			}
		}
		private void AddURLPattern(URLPattern urlPattern)
		{
			throughMePatterns.Add(urlPattern);
			if (urlPattern.NeedValidation)
			{
				needValidatePatterns.Add(urlPattern);
			}
		}

		public void LoadURLPattern(string urlPattern,IEnumerable<string> necessaryKeywords, 
			IEnumerable<string> forbiddenKeywords)
		{
			URLPattern upr = new URLPattern(urlPattern);
			if (necessaryKeywords != null)
			{
				upr.NeedValidation = true;
				foreach (string s in necessaryKeywords)
				{
					upr.NecessaryKeywords.Add(s);
				}
			}
			if (forbiddenKeywords != null)
			{
				upr.NeedValidation = true;
				foreach (string s in forbiddenKeywords)
				{
					upr.ForbiddenKeywords.Add(s);
				}
			}

			LoadURLPattern(upr);
		}

		public bool IsNeedValidate(string url)
		{
			
			foreach (URLPattern up in needValidatePatterns)
			{
				if (up.Enabled && up.UrlPatternRegex.IsMatch(url))
				{
					return true;
				}
			}
			return false;
		}
		public bool ValidateHtml(string fullURL, string html)
		{
			
			foreach (URLPattern up in needValidatePatterns)
			{
				if (up.Enabled && up.UrlPatternRegex.IsMatch(fullURL))
				{
					if (html == null)
					{
						if (up.NecessaryKeywords.Count != 0)
						{
							return false;
						}
						return true;
					}
					foreach (string s in up.NecessaryKeywords)
					{
						if (!html.Contains(s))
						{
							return false;
						}
					}
					foreach (string s in up.ForbiddenKeywords)
					{
						if (html.Contains(s))
						{
							return false;
						}
					}
					return true;
				}
			}
			return true;
		}
	}

	public delegate void ThroughMePatternsChangedEventHandler(object sender, ThroughMePatternsChangedEventHandlerArgs args);

	public class ThroughMePatternsChangedEventHandlerArgs
	{
	}
}
