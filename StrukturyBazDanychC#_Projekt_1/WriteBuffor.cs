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
        public Record[] RecordsBlock;
        public string FileToRead;
        public int bufforsize;
        public int ActualIndex;

        public WriteBuffor(int bufforSize, string FileName)
        {
            bufforsize = bufforSize;
            RecordsBlock = new Record[bufforsize];
            FileToRead = FileName;
            ActualIndex = 0;
        }

        public bool SaveRecord(Record record, EndInformations x,int option)
        {
            if (ActualIndex == bufforsize)
            {
                using (StreamWriter writer = new StreamWriter(FileToRead, append: true))
                {
                    for (ActualIndex = 0; ActualIndex < bufforsize; ActualIndex++)
                    {
                        writer.WriteLine(RecordsBlock[ActualIndex].Word);
                        if(option==1) Console.WriteLine(ActualIndex+": "+ RecordsBlock[ActualIndex].Word);

                    }
                }
                x.AmountOfWrite++;
                ActualIndex = 0;
                Array.Clear(RecordsBlock, 0, RecordsBlock.Length);
            }
            
            if (record == null) return false;
            RecordsBlock[ActualIndex]=record;
            ActualIndex++;
            return true;
        }

        public void saveRestValues(EndInformations x, int option)
        {
            using (StreamWriter writer = new StreamWriter(FileToRead, append: true))
            {
                for (int i = 0; i < ActualIndex; i++)
                {
                    writer.WriteLine(RecordsBlock[i].Word);
                    if (option == 1) Console.WriteLine(i + ": " + RecordsBlock[i].Word);

                }
            }
            x.AmountOfWrite++;
            ActualIndex = 0;                  
        }
    }
}
