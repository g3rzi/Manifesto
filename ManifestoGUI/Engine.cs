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
            //filepath = @"C:\windows\explorer.exe";
            //filepath = @"C:\windows\bfsvc.exe";
            //filepath = @"C:\windows\system32\dccw.exe";
            //filepath = @"C:\Windows\System32\changepk.exe"; //fails with "'asmv3' is an undeclared prefix. Line 15, position 6."

            // Add check Count >= because of filepath = @"C:\Windows\system32\wscript.exe"; out of index
            //filepath = @"C:\Windows\system32\compact.exe";
            ManifestInfo info = null;
            byte[] resource = ResourceManager.GetResourceFromExecutable(filepath, NativeAPI.CREATEPROCESS_MANIFEST_RESOURCE_ID, NativeAPI.RT_MANIFEST);

            if(resource != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    string xml = Encoding.UTF8.GetString(resource);
                    if (xml.Contains("asmv3"))
                    {
                        xml = (xml.Replace("asmv3:application", "asmv3")).Replace("asmv3:windowsSettings", "asmv3");
                    }
                    // If the XML starts with BOM we should remove the first byte
                    // https://stackoverflow.com/a/27743515
                    string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                    if(xml.StartsWith(byteOrderMarkUtf8, StringComparison.Ordinal))
                    {
                        xml = xml.Remove(0, byteOrderMarkUtf8.Length);
                    }
                    doc.LoadXml(xml);
                    XmlNodeList list = doc.GetElementsByTagName("requestedExecutionLevel");
                    
                    info = new ManifestInfo();
                    if(list.Count != 0)
                    {
                        if (list[0].Attributes.Count >= 1 && list[0].Attributes[0].Name.Equals("level"))
                        {
                            info.Level = list[0].Attributes[0].Value;
                        }

                        if (list[0].Attributes.Count >= 2 && list[0].Attributes[1].Name.Equals("uiAccess"))
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
