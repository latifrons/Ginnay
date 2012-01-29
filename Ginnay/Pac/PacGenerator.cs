using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Ginnay.Pac
{
	public class PacGenerator
	{
		public static string GeneratePacString(PacSetting setting)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("function FindProxyForURL(url, host) {\n");
			sb.Append("var throughMe = false;\n");
			bool hasThroughMePattern = setting.ThroughMePatterns.Count != 0;
			if (hasThroughMePattern)
			{
				sb.Append("throughMe = ");
				foreach (URLPattern up in setting.ThroughMePatterns)
				{
					//shExpMatch(host, "*.example.com")
					//sb.Append("\nshExpMatch(url,\"").Append(s).Append("\")||");
					// /s/.test(url)
					string urlFix = up.UrlPattern.Replace("/", "\\/");
					sb.Append("\n/").Append(urlFix).Append("/.test(url) ||");
				}
				sb.Remove(sb.Length - 2, 2);
				sb.Append(";\n");
			}
			
			sb.Append("if(throughMe){\n");
			sb.Append("return \"PROXY ").Append(setting.MyAddress).Append("\";\n}\n");
			sb.Append("else{\n");
			if (setting.OtherProxyAddress != null)
			{
				sb.Append("return \"PROXY ").Append(setting.OtherProxyAddress).Append("\";\n}\n");
			}
			else
			{
				sb.Append("return \"DIRECT\"").Append(";\n}\n");
			}
			

			sb.Append("}\n");


			return sb.ToString();
		}

	}
}
