using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    static class ListItems
    {
        public static List<Item> items;

        public static void printListItemsToConsole()
        {
            foreach (var item in items)
            {
                Console.WriteLine("Name: {0}\tHind: ${1:F2}\tKogus: {2}", item.Name, item.Hind, item.Kogus);
            }
            //Console.WriteLine();
        }

        public static decimal kokkuHind()
        {
            return items.Sum(x => x.Hind);
        }

        public static decimal kogusHind(string strItems)
        {
            decimal retValue = 0;
            foreach (var item in items)
            {
                if (strItems.Contains(item.Name.First()))
                {
                    retValue += item.Hind;
                }
            }
            return retValue;
        }
        public static bool kogusControl(string strItems)
        {
            bool retValue = true;
            foreach (var item in items)
            {
                if (strItems.Contains(item.Name.First()))
                {
                    if (item.Kogus<=0)
                    {
                        retValue = false;
                        break;
                    }
                }
            }
            return retValue;
        }
    }
}
