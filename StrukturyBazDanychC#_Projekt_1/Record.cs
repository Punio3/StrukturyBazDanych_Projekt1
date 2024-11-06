using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrukturyBazDanychC__Projekt_1
{
    public class Record : IComparable<Record>
    {
        private const int MaxLength = 30;

        public string Word { get; set; }
        public int Size => Word.Length;

        // Konstruktor
        public Record(string word)
        {
            if (word.Length > MaxLength)
                throw new ArgumentException($"Rekord nie może mieć więcej niż {MaxLength} znaków.");

            Word = word;
        }

        // Implementacja porównywania leksykograficznego dla sortowania
        public int CompareTo(Record other)
        {
            return string.Compare(this.Word, other.Word, StringComparison.Ordinal);
        }

        // Nadpisanie ToString dla czytelnego wyświetlania
        public override string ToString()
        {
            return Word + "\n";
        }
    }
}
