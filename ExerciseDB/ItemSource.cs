using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    interface ItemSource
    {
        List<Item> GetListItems();
        void updateKogus(string strItems);

        void InfoToConsole();

        void InitSource(List<Item> listItems);
    }
}
