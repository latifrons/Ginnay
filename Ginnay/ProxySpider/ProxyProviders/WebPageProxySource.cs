using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ginnay.ProxySpider.ProxyProviders
{
	public class WebPageProxySource
	{
		private string url;
		private string pattern;
		private Regex regex;
		private List<int> portWhiteList = new List<int>(); 
		private List<string> preConditions;

		public string URL
		{
			get { return url; }
			set { url = value; }
		}

		public string Pattern
		{
			get { return pattern; }
			set { pattern = value; 
			regex = new Regex(pattern);}
		}

		public Regex Regex
		{
			get { return regex; }
		}

		public List<int> PortWhiteList
		{
			get { return portWhiteList; }
			set { portWhiteList = value; }
		}

		public List<string> PreConditions
		{
			get { return preConditions; }
			set { preConditions = value; }
		}
	}
}
