using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class Record : IComparable<Record>
    {
        public string Word { get; set; }

        public Record(string word)
        {
            Word = word;
        }

        public int CompareTo(Record other)
        {
            return string.Compare(this.Word, other.Word, StringComparison.Ordinal);
        }
    }
}
