using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.IO;

namespace ExerciseDB
{
    class XMLtools : ItemSource
    {
        private static XmlDocument xDoc;
        private static string name_of_XML = ConfigTools.GetAppSetting("name_of_XML");

        public List<Item> GetListItems()
        {
            Item item = null;
            List<Item> listItems = new List<Item>();

            OpenConnect();

            XmlElement xRoot = xDoc.DocumentElement;

            int i = 0;
            foreach (XmlNode xnode in xRoot)
            {
                item = new Item();
                item.Name = xnode.Attributes.GetNamedItem("name").Value;
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "hind")
                    {
                        item.Hind = decimal.Parse(childnode.InnerText);
                    }
                    if (childnode.Name == "kogus")
                    {
                        item.Kogus = Int32.Parse(childnode.InnerText);
                    }
                }
                listItems.Add(item);
            }

            return listItems;
        }

        private static void OpenConnect()
        {
            //name_of_XML = ConfigTools.GetAppSetting("name_of_XML");
            xDoc = new XmlDocument();
            xDoc.Load(name_of_XML);
        }

        private static void SaveAndClose()
        {
            xDoc.Save(name_of_XML);            
        }

        public void updateKogus(string strItems)
        {
            XmlElement xRoot = xDoc.DocumentElement;

            foreach (XmlNode xnode in xRoot)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (strItems.Contains(attr.Value.First()))
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "kogus")
                        {
                            childnode.InnerText = (Int32.Parse(childnode.InnerText) - 1).ToString();
                        }
                    }
                }
            }
            SaveAndClose();
        }

        public void InfoToConsole()
        {
            Console.WriteLine("---------- WORK WITH XML ---------- (A.Avanesov)\n");
        }

        public void InitSource(List<Item> listItems)
        {
            using (StreamWriter sw = new StreamWriter(name_of_XML, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?> ");
                sw.WriteLine("<items>");
                foreach (var item in listItems)
                {
                    sw.WriteLine(String.Format("  <item name=\"{0}\">", item.Name));
                    sw.WriteLine(String.Format("    <hind>{0}</hind>", item.Hind));
                    sw.WriteLine(String.Format("    <kogus>{0}</kogus>", item.Kogus));
                    sw.WriteLine(String.Format("  </item>"));
                }
                sw.WriteLine("</items>");
            }
            Console.WriteLine("OK INIT XML-file\n");
        }

    }
}
