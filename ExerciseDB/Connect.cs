using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    internal static class Connect
    {
        public static void SendMessages(params string[] messages)
        {
            for (int i = 0; i < messages.Length; i++)
            {
                Console.Write(messages[i]);
            }
        }

        public static string RecieveMessage()
        {
            string retValue = Console.ReadLine();
            return retValue;
        }
    }
}
