using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using Ginnay.ProxySpider;
using Ginnay.ProxySpider.ProxyProviders;

namespace GinnayGUI
{
	public class ProxyProviderParser
	{
		public static IEnumerable<AbstractProxyProvider> ReadConfig(string path)
		{
			List<AbstractProxyProvider> proxyProviders = new List<AbstractProxyProvider>();
			XmlDocument doc = new XmlDocument();
			doc.Load(path);
			XmlNodeList nodelist = doc.SelectNodes("//ProxyProvider");
			if (nodelist == null)
			{
				return proxyProviders;
			}

			foreach (XmlNode node in nodelist)
			{

				if (node.Attributes == null)
				{
					continue;
				}
				XmlAttribute classNameX = node.Attributes["ClassName"];
				XmlAttribute filenameX = node.Attributes["FileName"];
				XmlAttribute enabledX = node.Attributes["Enabled"];
				if (classNameX == null || filenameX == null
					|| enabledX == null)
				{
					continue;
				}
				try
				{
					Assembly bin;
					if (!string.IsNullOrEmpty(filenameX.Value))
					{
						bin = Assembly.LoadFrom(filenameX.Value);
						
					}else
					{
						bin = Assembly.Load("Ginnay");
					}
					Type ppType = bin.GetType(classNameX.Value);
					AbstractProxyProvider ipp = (AbstractProxyProvider)Activator.CreateInstance(ppType);
					ipp.Enabled = Convert.ToBoolean(enabledX.Value);
					ipp.ClassName = classNameX.Value;
					proxyProviders.Add(ipp);
				}catch(Exception e)
				{
					Console.WriteLine(e.Message);	
				}
			}
			return proxyProviders;
		}
	}
}
