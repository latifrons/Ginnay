using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ginnay.Pac
{
	public class URLPattern
	{
		private string urlPattern;
		private Regex urlPatternRegex;
		private bool needValidation;
		private List<string> necessaryKeywords = new List<string>(1);
		private List<string> forbiddenKeywords = new List<string>(1);
		private string classification;
		private bool enabled = true;
 

		public URLPattern(string urlPattern)
		{
			this.urlPattern = urlPattern;
			urlPatternRegex = new Regex(urlPattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}
		public string UrlPattern
		{
			get { return urlPattern; }
		}

		public Regex UrlPatternRegex
		{
			get { return urlPatternRegex; }
		}

		public List<string> NecessaryKeywords
		{
			get { return necessaryKeywords; }
			set { necessaryKeywords = value; }
		}

		public List<string> ForbiddenKeywords
		{
			get { return forbiddenKeywords; }
			set { forbiddenKeywords = value; }
		}

		public bool NeedValidation
		{
			get { return needValidation; }
			set { needValidation = value; }
		}

		public string Classification
		{
			get { return classification; }
			set { classification = value; }
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
		public string NecessaryKeywordsStr
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				foreach (string s in necessaryKeywords)
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

		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
	}
}
