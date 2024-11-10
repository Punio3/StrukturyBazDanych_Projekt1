using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class ReadBuffor
    {
        public Record[] RecordsBlock;
        public string FileToRead;
        public bool isEndOfFile;
        public int bufforsize;
        public int ActualIndex;
        public int IndexOfLastRecordRode;

        public ReadBuffor(int bufforSize,string FileName) 
        {
            bufforsize = bufforSize;
            isEndOfFile = false;
            IndexOfLastRecordRode = 0;
            RecordsBlock = new Record[bufforsize];
            FileToRead = FileName;
            ActualIndex = 0;
        }

        public Record NextRecord(EndInformations x)
        {
            int ActualIndexCopy = -1;
            if (ActualIndex == bufforsize || IndexOfLastRecordRode == 0)
            {
                Array.Clear(RecordsBlock, 0, RecordsBlock.Length);
                using (StreamReader reader = new StreamReader(FileToRead))
                {
                    int currentLine = 0;

                    // Pomijaj linie aż do IndexOfLastRecordRode
                    while (currentLine < IndexOfLastRecordRode && !reader.EndOfStream)
                    {
                        reader.ReadLine();
                        currentLine++;
                    }
                    string tmp;
                    ActualIndex = 0;
                    while (ActualIndex < bufforsize && !isEndOfFile)
                    {
                        tmp = reader.ReadLine();
                        if (tmp != null)
                        {
                            RecordsBlock[ActualIndex] = new Record(tmp);
                            ActualIndex++;
                        }
                        else isEndOfFile = true;                        
                    }
                }
                IndexOfLastRecordRode = IndexOfLastRecordRode + ActualIndex;
                ActualIndexCopy = ActualIndex;
                ActualIndex = 0;
                x.AmountOfRead++;
            }

            if (ActualIndexCopy == 0 && isEndOfFile) return null;
            
            ActualIndex++;
            return RecordsBlock[ActualIndex-1];

        }
    }
}
