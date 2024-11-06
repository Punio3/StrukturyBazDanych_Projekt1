using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class SortingInformations
    {
        public int countRead = 0;
        public int countWrite = 0;
        public int countSplitting = 0;
        public int countMerge = 0;

        public void DisplayInformations()
        {
            Console.WriteLine($"countRead: {countRead}\n" +
                               $"countWrite: {countWrite}\n" +
                               $"countRead: {countSplitting}\n" +
                               $"countRead: {countMerge}\n");
    }
    }

}
