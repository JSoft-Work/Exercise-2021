using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    class ConfigTools
    {
        public static List<Item> GetListItems()
        {
            Item item = null;
            List<Item> listItems = new List<Item>();

            ProductsConfigSection section = (ProductsConfigSection)ConfigurationManager.GetSection("AllProducts");

            for (int i = 0; i < section.Products.Count; i++)
            {

                item = new Item();
                item.Name = section.Products[i].ProductType;
                item.Hind = decimal.Parse(section.Products[i].Hind);
                item.Kogus = int.Parse(section.Products[i].Kogus);
                listItems.Add(item);
            }

            return listItems;
        }

        public static string GetAppSetting(string attribute)
        {
            return ConfigurationManager.AppSettings[attribute];
        }

    }
}
