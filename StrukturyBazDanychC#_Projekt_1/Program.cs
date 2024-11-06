using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StrukturyBazDanychC__Projekt_1
{
    class Program
    {
        public static int count1 = 0;
        public static int count2 = 0;
        public static void ShowMenu()
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1) Zapis z klawiatury");
            Console.WriteLine("2) Import z pliku");
            Console.WriteLine("3) Generowanie losowych rekordów do pliku o stałym rozmiarze");
            Console.WriteLine("4) Sortowanie taśmy");
            Console.WriteLine("0) Wyjście");
        }

        static void Main(string[] args)
        {
            int option = -1;
            bool sorted=true;

            SortingInformations sortingInformations = new SortingInformations();
            while (true)
            {
                ShowMenu();
                Console.Write("Wybierz opcję: ");

                string input = Console.ReadLine();

                int.TryParse(input, out option);
                    switch (option)
                    {
                        case 1:
                            GenerateFromKeyboardRecordsToFile();
                            break;
                        case 2:
                            ImportFromFileToCSV();
                            break;
                        case 3:
                            GenerateRandomRecordsDirectlyToFile();
                            break;
                        case 4:
                            Console.WriteLine("Opcja 4: Sortowanie taśmy\n");
                            CopyFileToSort(sortingInformations);
                        while (sorted)
                        {
                            splitting(sortingInformations);
                            sorted = merging(sortingInformations);
                        }
                        sortingInformations.DisplayInformations();
                        sorted = true;
                        break;
                        case 5:
                        /**Record x = new Record("xdd");
                        Record y = x;
                        Record z = new Record("lasodosadas");
                        y.Word = "sakdiask";
                        Console.WriteLine(x.Word + "   "+y.Word+ "    "+ z.Word);
                        y = z;
                        Console.WriteLine(x.Word + "   " + y.Word + "    " + z.Word);**/
                        Record x = new Record("aaa");
                        Record y = new Record("klaaa");
                        Record z = new Record("x");
                        Record p = new Record("{");
                        Console.WriteLine(x.CompareTo(y));
                        Console.WriteLine(y.CompareTo(z));
                        Console.WriteLine(z.CompareTo(x));

                        Console.WriteLine(x.CompareTo(p));
                        Console.WriteLine(y.CompareTo(p));
                        Console.WriteLine(z.CompareTo(p));

                        break;
                        case 0:
                            Console.WriteLine("Wyjście z programu.\n");
                            return;
                        default:
                            Console.WriteLine("Nieznana opcja, spróbuj ponownie.\n");
                            break;
                    }
            }
        }

        static void GenerateRandomRecordsDirectlyToFile()
        {
            Console.Write("Podaj liczbę rekordów do wygenerowania: ");
            int.TryParse(Console.ReadLine(), out int count);

            Random random = new Random();

            using (StreamWriter writer = new StreamWriter("input.csv"))
            {
                for (int i = 0; i < count; i++)
                {
                    int length = random.Next(1, 31);
                    string word = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
                                                  .Select(s => s[random.Next(s.Length)]).ToArray());

                    Record record = new Record(word);
                    writer.Write(record.ToString());
                }
            }
            Console.WriteLine($"Wygenerowano {count} rekordów i zapisano do pliku input.csv w formacie o stałym rozmiarze.");
        }

        static void GenerateFromKeyboardRecordsToFile()
        {
            Console.WriteLine("Podaj liczbę rekordów do zapisania: ");
            int.TryParse(Console.ReadLine(), out int amount);

            using (StreamWriter writer = new StreamWriter("input.csv"))
            {
                for (int i = 0; i < amount; i++)
                {
                    Console.WriteLine(i + " record:");
                    Record record = new Record(Console.ReadLine());
                    writer.Write(record.ToString());
                }
            }
            Console.WriteLine($"Wygenerowano {amount} rekordów i zapisano do pliku input.csv w formacie o stałym rozmiarze.");
        }

        static void ImportFromFileToCSV()
        {
            Console.Write("Podaj nazwę pliku do importu: ");
            string sourceFileName = Console.ReadLine();

            using (StreamReader reader = new StreamReader(sourceFileName))
            using (StreamWriter writer = new StreamWriter("input.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine($"Zawartość pliku {sourceFileName} rekordów i zapisano do pliku input.csv w formacie o stałym rozmiarze.");
        }

        static void CopyFileToSort(SortingInformations sortingInformations)
        {
            ReadBuffor TapeInput = new ReadBuffor(100, "input.csv");
            WriteBuffor TapeOutputC = new WriteBuffor(100, "TapeInputC.csv");
            //creating new file
            using (StreamWriter writer = new StreamWriter("TapeInputC.csv"));

            Record Record_TMP;

            while (true)
            {
                Record_TMP = TapeInput.NextRecord(sortingInformations);
                if (Record_TMP == null) break;

                TapeOutputC.SaveRecord(Record_TMP, sortingInformations);
            }
            TapeOutputC.saveRestValues(sortingInformations);
        }

        static void splitting(SortingInformations sortingInformations)
        {
            count1++;
            Console.WriteLine(count1);

            bool FirstValueIsSet = false;
            ReadBuffor TapeInputC = new ReadBuffor(100, "TapeInputC.csv");
            WriteBuffor TapeOutputA = new WriteBuffor(100, "TapeOutputA.csv");
            WriteBuffor TapeOutputB = new WriteBuffor(100, "TapeOutputB.csv");

            //creating new files
            using (StreamWriter writer = new StreamWriter("TapeOutputA.csv"));
            using (StreamWriter writer = new StreamWriter("TapeOutputB.csv"));

            Record Record_TMP;
            Record LastValue=null;

            WriteBuffor TapeTmp = TapeOutputA;

            while(true)
            {
                Record_TMP = TapeInputC.NextRecord(sortingInformations);
                if (!FirstValueIsSet)
                {
                    LastValue = new Record(Record_TMP.Word);
                    FirstValueIsSet = true;
                }
                if (Record_TMP == null) break;

                if (Record_TMP.CompareTo(LastValue) < 0)
                {
                    if (TapeTmp == TapeOutputB) TapeTmp = TapeOutputA;
                    else TapeTmp=TapeOutputB;
                }

                TapeTmp.SaveRecord(Record_TMP,sortingInformations);
                LastValue = new Record(Record_TMP.Word);
            }
            TapeOutputA.saveRestValues(sortingInformations);
            TapeOutputB.saveRestValues(sortingInformations);
            using (StreamWriter writer = new StreamWriter("TapeInputC.csv"));
        }

        static bool merging(SortingInformations sortingInformations)
        {
            count2++;
            Console.WriteLine(count2);
            WriteBuffor TapeOutputC = new WriteBuffor(100, "TapeInputC.csv");
            ReadBuffor TapeInputA = new ReadBuffor(100, "TapeOutputA.csv");
            ReadBuffor TapeInputB = new ReadBuffor(100, "TapeOutputB.csv");

            Record recordA = TapeInputA.NextRecord(sortingInformations);
            Record recordB = TapeInputB.NextRecord(sortingInformations);
            Record tempValueA = new Record("!");
            Record tempValueB = new Record("!");

            if (recordB == null) return false;

            while (true)
            {
                if (recordA!=null && recordB!=null)
                {
                    //sortujemy 2 tasmy az jedna sie skonczy, pozniej dokladamy reszte tasmy ktora zostala
                    while (recordA != null && recordB != null && recordA.CompareTo(tempValueA) >= 0 && recordB.CompareTo(tempValueB) >= 0)
                    {
                        if (recordA.CompareTo(recordB) > 0)
                        {
                            TapeOutputC.SaveRecord(recordB, sortingInformations);
                            tempValueB = new Record(recordB.Word);
                            recordB = TapeInputB.NextRecord(sortingInformations);
                        }
                        else
                        {
                            TapeOutputC.SaveRecord(recordA, sortingInformations);
                            tempValueA = new Record(recordA.Word);
                            recordA = TapeInputA.NextRecord(sortingInformations);
                        }
                    }
                    //tutaj zbieramy tasme B co zostala
                    if (recordB != null && recordB.CompareTo(tempValueB) >= 0)
                    {
                        while (recordB!=null && recordB.CompareTo(tempValueB) >= 0)
                        {
                            TapeOutputC.SaveRecord(recordB, sortingInformations);
                            tempValueB = new Record(recordB.Word);
                            recordB = TapeInputB.NextRecord(sortingInformations);
                        }
                        tempValueA = new Record("!");
                        tempValueB = new Record("!");
                    }
                    //tutaj zbieramy tasme A co zostala
                    else if (recordA != null && recordA.CompareTo(tempValueA) >= 0)
                    {
                        while (recordA !=null && recordA.CompareTo(tempValueA) >= 0)
                        {
                            TapeOutputC.SaveRecord(recordA, sortingInformations);
                            tempValueA = new Record(recordA.Word);
                            recordA = TapeInputA.NextRecord(sortingInformations);
                        }
                        tempValueA = new Record("!");
                        tempValueB = new Record("!");
                        
                    }

                }
                //case ze jest nieparzysta liczba serii i 1 seria nie zostaje zczytana
                else if (recordA == null && recordB!=null)
                {
                    while (recordB != null)
                    {
                        TapeOutputC.SaveRecord(recordB, sortingInformations);
                        tempValueB = new Record(recordB.Word);
                        recordB = TapeInputB.NextRecord(sortingInformations);
                    }
                }
                else if(recordB==null && recordA != null)
                {
                    while (recordA != null)
                    {
                        TapeOutputC.SaveRecord(recordA, sortingInformations);
                        tempValueA = new Record(recordA.Word);
                        recordA = TapeInputA.NextRecord(sortingInformations);
                    }
                }
                else
                {
                    break;
                }
            }
            TapeOutputC.saveRestValues(sortingInformations);
            return true;
        }
    }
}
