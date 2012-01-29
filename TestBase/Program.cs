using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBase
{
	class Program
	{
		static void Main(string[] args)
		{
			IEProxySetter.SystemProxy sp = new IEProxySetter.SystemProxy
			                               	{
			                               		IsAutoDetect = true,
												IsDirect = false,
												ProxyAddress = "7.7.7.7:303",
												PacPath = "fdsafs",
												IsPac = true,
												IsProxy = true
			                               	};
			IEProxySetter.SetConnectionProxy(sp);
			IEProxySetter.SystemProxy sp2 = IEProxySetter.GetSystemProxy();
			Console.ReadKey();
		}
	}
}
