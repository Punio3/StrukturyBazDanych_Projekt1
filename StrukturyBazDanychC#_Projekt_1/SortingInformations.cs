using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class EndInformations
    {
        public int AmountOfRead = 0;
        public int AmountOfWrite = 0;
        public int AmountOfSplitting = 0;
        public int AmountOfMerge = 0;

        public void DisplayInformations()
        {
            Console.WriteLine($"AmountOfRead: {AmountOfRead}\n" +
                              $"AmountOfWrite: {AmountOfWrite}\n" +
                              $"AmountOfSplitting: {AmountOfSplitting}\n" +
                              $"AmountOfMerge: {AmountOfMerge}\n");
        }
        public void ResetInformations()
        {
            AmountOfRead = 0;
            AmountOfWrite = 0;
            AmountOfSplitting = 0;
            AmountOfMerge = 0;
        }
    }

}
