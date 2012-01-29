using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Ginnay.Pac;
using Ginnay.ProxySpider;

namespace GinnayGUI
{
	public class URLPatternParser
	{
		public static IEnumerable<URLPattern> ReadConfig(string path)
		{
			string classification = Path.GetFileNameWithoutExtension(path);
			List<URLPattern> urlPatterns = new List<URLPattern>();
			XmlDocument doc = new XmlDocument();
			doc.Load(path);
			XmlNodeList nodelist = doc.SelectNodes("//URLPattern");
			if (nodelist == null)
			{
				return urlPatterns;
			}

			foreach (XmlNode node in nodelist)
			{
				
				if (node.Attributes == null)
				{
					continue;
				}
				XmlAttribute urlX = node.Attributes["URL"];
				XmlAttribute needValidationX = node.Attributes["NeedValidation"];
				XmlAttribute necessaryKeywordsX = node.Attributes["NecessaryKeywords"];
				XmlAttribute forbiddenKeywordsX = node.Attributes["ForbiddenKeywords"];
				XmlAttribute enabledX = node.Attributes["Enabled"];
				if (needValidationX == null || necessaryKeywordsX == null || 
					forbiddenKeywordsX == null || enabledX == null)
				{
					continue;
				}
				URLPattern urlPattern = new URLPattern(urlX.Value);
				urlPattern.NeedValidation = Convert.ToBoolean(needValidationX.Value);
				urlPattern.Enabled = Convert.ToBoolean(enabledX.Value);
				string[] necessaryKeywords = necessaryKeywordsX.Value.Split(new char[] {','});
				foreach (string s in necessaryKeywords)
				{
					urlPattern.NecessaryKeywords.Add(s);
				}
				string[] forbiddenKeywords = forbiddenKeywordsX.Value.Split(new char[] { ',' });
				foreach (string s in forbiddenKeywords)
				{
					urlPattern.ForbiddenKeywords.Add(s);
				}
				urlPattern.Classification = classification;
				
				urlPatterns.Add(urlPattern);
			}
			return urlPatterns;
		}

		public static void WriteConfig(string path, IEnumerable<URLPattern> urlPatterns)
		{

			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, "yes"));
			XmlElement root = doc.CreateElement("URLPatterns");
			doc.AppendChild(root);
			foreach (URLPattern p in urlPatterns)
			{
				XmlElement node = doc.CreateElement("URLPattern");
				node.SetAttribute("Enabled", p.Enabled.ToString());
				node.SetAttribute("URL",p.UrlPattern );
				node.SetAttribute("NeedValidation", p.NeedValidation.ToString());
				node.SetAttribute("NecessaryKeywords", p.NecessaryKeywordsStr);
				node.SetAttribute("ForbiddenKeywords", p.ForbiddenKeywordsStr);

				root.AppendChild(node);
			}
			FileInfo fi = new FileInfo(path);
			fi.Directory.Create();
			FileStream fs = fi.Create();
			doc.Save(fs);
			fs.Close();
		}
	}
}
