using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccess
{
	internal static class XMLConfigurations
	{
		public static string LeerCadenaDeConexion()
		{
			try
			{
				string urlArchivo = Path.Combine(Directory.GetCurrentDirectory(), "Configuration.xml");
				var data = new FileStream(urlArchivo, FileMode.Open, FileAccess.Read);
				var documento = new XmlDataDocument();
				documento.Load(data);
				XmlNodeList nodeList = documento.GetElementsByTagName("stringconnection");

				foreach (XmlNode item in nodeList)
				{
					var daata = item.SelectSingleNode("DBTasks").InnerText.ToString();
					return daata;
				}
				return null;
			}
			catch (Exception f)
			{
				return null;
			}
		}

	}
}
