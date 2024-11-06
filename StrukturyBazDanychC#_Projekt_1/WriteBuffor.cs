using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StrukturyBazDanychC__Projekt_1
{
    public class WriteBuffor
    {
        public Record[] Records;
        public string FileToRead;
        public int bufforsize;
        public int ActualIndex;

        public WriteBuffor(int bufforSize, string FileName)
        {
            bufforsize = bufforSize;
            Records = new Record[bufforsize];
            FileToRead = FileName;
            ActualIndex = 0;
        }

        public bool SaveRecord(Record record, SortingInformations x)
        {
            if (ActualIndex == bufforsize)
            {
                using (StreamWriter writer = new StreamWriter(FileToRead, append: true))
                {
                    for (ActualIndex = 0; ActualIndex < bufforsize; ActualIndex++)
                    {
                        writer.WriteLine(Records[ActualIndex].Word);
                        Console.WriteLine(Records[ActualIndex].Word);

                    }
                }
                x.countWrite++;
                ActualIndex = 0;
                Array.Clear(Records, 0, Records.Length);
            }
            
            if (record == null) return false;
            Records[ActualIndex]=record;
            ActualIndex++;
            return true;
        }

        public void saveRestValues(SortingInformations x)
        {
            using (StreamWriter writer = new StreamWriter(FileToRead, append: true))
            {
                for (int i = 0; i < ActualIndex; i++)
                {
                    writer.WriteLine(Records[i].Word);
                    Console.WriteLine(Records[i].Word);

                }
            }
            x.countWrite++;
            ActualIndex = 0;                  
        }
    }
}
