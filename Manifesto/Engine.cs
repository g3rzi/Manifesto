using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Manifesto
{
    class Engine
    {
        public static ManifestInfo GetManifestInfo(string filepath)
        {
            ManifestInfo info = null;
            byte[] resource = ResourceManager.GetResourceFromExecutable(filepath, NativeAPI.CREATEPROCESS_MANIFEST_RESOURCE_ID, NativeAPI.RT_MANIFEST);

            if(resource != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    string xml = Encoding.UTF8.GetString(resource);
                    doc.LoadXml(xml);
                    XmlNodeList list = doc.GetElementsByTagName("requestedExecutionLevel");
                    info = new ManifestInfo();

                    if (list[0].Attributes[0].Name.Equals("level"))
                    {
                        info.Level = list[0].Attributes[0].Value;
                    }

                    if (list[0].Attributes[1].Name.Equals("uiAccess"))
                    {
                        info.uiAccess = list[0].Attributes[1].Value;
                    }

                    list = doc.GetElementsByTagName("dpiAware");
                    if (list.Count != 0)
                    {
                        info.dpiAware = list[0].InnerText;
                    }

                    list = doc.GetElementsByTagName("autoElevate");
                    if (list.Count != 0)
                    {
                        info.autoElevate = list[0].InnerText;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception with file: {0}", filepath);
                    Console.WriteLine("Message: {0}", ex.Message);
                    Console.WriteLine();
                }
            }

            return info;
        }
    }
}
