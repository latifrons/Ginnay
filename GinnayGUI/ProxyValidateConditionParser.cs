using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Ginnay.ProxySpider;

namespace GinnayGUI
{
	public class ProxyValidateConditionParser
	{
		public static IEnumerable<ProxyValidateCondition> ReadConfig(string path)
		{
			List<ProxyValidateCondition> conditions = new List<ProxyValidateCondition>();
			XmlDocument doc = new XmlDocument();
			doc.Load(path);
			XmlNodeList nodelist = doc.SelectNodes("//ProxyValidateCondition");
			if (nodelist == null)
			{
				return conditions;
			}

			foreach (XmlNode node in nodelist)
			{
				
				if (node.Attributes == null)
				{
					continue;
				}
				XmlAttribute urlX = node.Attributes["URL"];
				XmlAttribute keywordX = node.Attributes["Keywords"];
				XmlAttribute forbiddenKeywordX = node.Attributes["ForbiddenKeywords"];
				if (urlX == null || keywordX == null || forbiddenKeywordX == null)
				{
					continue;
				}
				ProxyValidateCondition pvc = new ProxyValidateCondition
				                             	{
				                             		Url = urlX.Value,
				                             	};
				pvc.Keywords.AddRange(keywordX.Value.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries));
				pvc.ForbiddenKeywords.AddRange(forbiddenKeywordX.Value.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries));
				conditions.Add(pvc);
			}
			return conditions;
		}

		public static void WriteConfig(string path, IEnumerable<ProxyValidateCondition> proxyValidateConditions)
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, "yes"));
			XmlElement root = doc.CreateElement("ProxyValidateConditions");
			doc.AppendChild(root);
			foreach (ProxyValidateCondition pvc in proxyValidateConditions)
			{
				XmlElement node = doc.CreateElement("ProxyValidateCondition");

				node.SetAttribute("URL", pvc.Url);
				node.SetAttribute("Keywords", pvc.KeywordsString);
				node.SetAttribute("ForbiddenKeywords", pvc.ForbiddenKeywordsString);
				root.AppendChild(node);
			}
			doc.Save(path);
		}
	}
}
