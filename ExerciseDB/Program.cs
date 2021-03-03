using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ExerciseDB
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("A.Avanesov - EXERCISE (2021)");

            ItemSource itemSource = null;
            if (ConfigTools.GetAppSetting("connectionType") == "XML")
            {
                itemSource = new XMLtools();
            }
            else
            {
                itemSource = new DBtools();
            }

            if (args.Length > 0 && args[0].ToUpper() == "INIT")
            {
                ListItems.items = ConfigTools.GetListItems();
                ListItems.printListItemsToConsole();
                itemSource.InitSource(ListItems.items);
            }

            try
            {
                InfoForUserOnConsole(itemSource);               // Only User info!!! Please read parameters of programm in CONFIG-file!
                Dialog(itemSource);
            }
            catch (Exception ex)
            {
                Connect.SendMessages(ex.Message);
            }
        }

        private static void Dialog(ItemSource itemSource)
        {
            ListItems.items = itemSource.GetListItems();
            ListItems.printListItemsToConsole();

            string vastusList = string.Empty;
            string vastusRaha = string.Empty;
            decimal sum = 0;
            decimal raha = 0;

            Connect.SendMessages("Items to Purchase > ");
            vastusList = Connect.RecieveMessage();
            Connect.SendMessages("Total > ");
            if (!ListItems.kogusControl(vastusList))
            {
                Connect.SendMessages("Not enough stock.");
            }
            else
            {
                Connect.SendMessages(String.Format("${0:F2}", ListItems.kogusHind(vastusList)), "\n", "Amount Paid > $");   // only example!!!
                vastusRaha = Connect.RecieveMessage();
                sum = ListItems.kogusHind(vastusList);
                raha = decimal.Parse(vastusRaha);
                Connect.SendMessages("Change > ");

                if (raha < sum)
                {
                    Connect.SendMessages("Not enough money.");
                }
                else
                {
                    Connect.SendMessages(String.Format("${0:F2}", raha - sum));
                    itemSource.updateKogus(vastusList);
                }
            }
            Connect.RecieveMessage();       // only for look results at the screen!
        }

        private static void InfoForUserOnConsole(ItemSource itemSource)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exercise: The application must calculate the smallest amount of change...");
            Console.ResetColor(); 
            Console.Write("But, SMALLEST amount = $0,01 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ALWAYS!\n");
            Console.ResetColor();
            itemSource.InfoToConsole();
        }
    }
}
