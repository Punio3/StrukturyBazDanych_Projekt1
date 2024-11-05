using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class ReadBuffor
    {
        public int bufforsize;
        public bool isEndOfFile;
        public int IndexOfNextRecord;
        public string FileToRead;
        public Record[] Records;

        public ReadBuffor(int bufforSize) 
        {
            bufforsize = bufforSize;
            isEndOfFile = false;
            IndexOfNextRecord = 0;
            Records=new Record[bufforsize];
        }
    }
}
