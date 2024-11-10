using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StrukturyBazDanychC__Projekt_1
{
    class Program
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Author: Przemek Dębek       Numer albumu: 193378\n");
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1) Zapis z klawiatury");
            Console.WriteLine("2) Import z pliku");
            Console.WriteLine("3) Generowanie losowych rekordów do pliku o stałym rozmiarze");
            Console.WriteLine("0) Wyjście");
        }
        public static void ShowMenu2()
        {
            Console.WriteLine("Wybierz opcję sortowania:");
            Console.WriteLine("0) Sortowanie bez wyświetlania");
            Console.WriteLine("1) Sortowanie z wyświetlaniem");
        }
        static void Main(string[] args)
        {
            int option = -1;
            bool sorted=true;

            EndInformations EndInformations = new EndInformations();
            while (true)
            {
                ShowMenu();

                string input = Console.ReadLine();

                int.TryParse(input, out option);
                    switch (option)
                    {
                        case 1:
                            GenerateFromKeyboardRecordsToFile(EndInformations);
                            AllFunctionsToSort(EndInformations, sorted);
                            break;
                        case 2:
                            ImportFromFileToCSV();
                            AllFunctionsToSort(EndInformations, sorted);
                            break;
                        case 3:
                            GenerateRandomRecordsDirectlyToFile(EndInformations);
                            AllFunctionsToSort(EndInformations, sorted);
                            break;
                        case 0:
                            Console.WriteLine("Wyjście z programu.\n");
                            return;
                        default:
                            break;
                    }
            }
        }
        static void AllFunctionsToSort(EndInformations EndInformations, bool sorted)
        {
            int option = -1;
            ShowMenu2();
            int.TryParse(Console.ReadLine(), out option);
            while (sorted)
            {
                splitting(EndInformations);
                sorted = merging(EndInformations, option);
            }
            CopySortedFileToOutput(EndInformations);
            EndInformations.DisplayInformations();
            EndInformations.ResetInformations();
            sorted = true;
        }

        static void GenerateRandomRecordsDirectlyToFile(EndInformations EndInformations)
        {
            Console.Write("Podaj liczbę rekordów do wygenerowania: ");
            int.TryParse(Console.ReadLine(), out int count);

            Random random = new Random();
            WriteBuffor FileA = new WriteBuffor(100, "input.csv");

            using (StreamWriter writer = new StreamWriter("input.csv"));
            
                for (int i = 0; i < count; i++)
                {
                    int length = random.Next(1, 30);
                    string word = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
                                                  .Select(s => s[random.Next(s.Length)]).ToArray());

                    Record record = new Record(word);
                FileA.SaveRecord(record, EndInformations, 0);
                }
            FileA.saveRestValues(EndInformations, 0);
            Console.WriteLine($"Wygenerowano {count} rekordów i zapisano do pliku input.csv w formacie o stałym rozmiarze.");
        }

        static void GenerateFromKeyboardRecordsToFile(EndInformations EndInformations)
        {
            Console.WriteLine("Podaj liczbę rekordów do zapisania: ");
            int.TryParse(Console.ReadLine(), out int amount);

            WriteBuffor FileA = new WriteBuffor(100, "input.csv");

            using (StreamWriter writer = new StreamWriter("input.csv")) ;
            
                for (int i = 0; i < amount; i++)
                {
                    Console.WriteLine(i + " record:");
                    Record record = new Record(Console.ReadLine());
                FileA.SaveRecord(record, EndInformations, 0);
            }
            FileA.saveRestValues(EndInformations, 0);
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

        static void CopyFileToSort(EndInformations EndInformations)
        {
            ReadBuffor FileA = new ReadBuffor(100, "input.csv");
            WriteBuffor FileB = new WriteBuffor(100, "TapeInputC.csv");
            //creating new file
            using (StreamWriter writer = new StreamWriter("TapeInputC.csv"));

            Record Record_TMP;
            while (true)
            {
                Record_TMP = FileA.NextRecord(EndInformations);
                if (Record_TMP == null) break;
                Console.WriteLine(Record_TMP.Word);

                FileB.SaveRecord(Record_TMP, EndInformations, 0);
            }
            FileB.saveRestValues(EndInformations, 0);
        }

        static void CopySortedFileToOutput(EndInformations EndInformations)
        {
            ReadBuffor FileA = new ReadBuffor(100, "TapeOutputA.csv");
            WriteBuffor FileB = new WriteBuffor(100, "Output.csv");
            //creating new file
            using (StreamWriter writer = new StreamWriter("Output.csv"));
            Record Record_TMP;

            while (true)
            {
                Record_TMP = FileA.NextRecord(EndInformations); 
                if (Record_TMP == null) break;

                Console.WriteLine(Record_TMP.Word);

                FileB.SaveRecord(Record_TMP, EndInformations, 0);
            }
            FileB.saveRestValues(EndInformations, 0);

            if (File.Exists("TapeOutputA.csv") && File.Exists("TapeOutputB.csv") && (File.Exists("input.csv"))){

                File.Delete("TapeOutputA.csv");
                File.Delete("TapeOutputB.csv");
                File.Delete("input.csv");
            }
        }

        static void splitting(EndInformations EndInformations)
        {
            EndInformations.AmountOfSplitting++;

            bool FirstValueIsSet = false;
            ReadBuffor FileA = new ReadBuffor(100, "input.csv");
            WriteBuffor FileB = new WriteBuffor(100, "TapeOutputA.csv");
            WriteBuffor FileC = new WriteBuffor(100, "TapeOutputB.csv");

            //creating new files
            using (StreamWriter writer = new StreamWriter("TapeOutputA.csv"));
            using (StreamWriter writer = new StreamWriter("TapeOutputB.csv"));

            Record Record_TMP;
            Record LastValue=null;

            WriteBuffor TapeTmp = FileB;

            while(true)
            {
                Record_TMP = FileA.NextRecord(EndInformations);
                if (!FirstValueIsSet)
                {
                    LastValue = new Record(Record_TMP.Word);
                    FirstValueIsSet = true;
                }
                if (Record_TMP == null) break;

                if (Record_TMP.CompareTo(LastValue) < 0)
                {
                    if (TapeTmp == FileC) TapeTmp = FileB;
                    else TapeTmp= FileC;
                }

                TapeTmp.SaveRecord(Record_TMP, EndInformations, 0);
                LastValue = new Record(Record_TMP.Word);
            }
            FileB.saveRestValues(EndInformations, 0);
            FileC.saveRestValues(EndInformations, 0);
            using (StreamWriter writer = new StreamWriter("input.csv"));
        }

        static bool merging(EndInformations EndInformations, int option)
        {
            EndInformations.AmountOfMerge++;
            WriteBuffor FileA = new WriteBuffor(100, "input.csv");
            ReadBuffor FileB = new ReadBuffor(100, "TapeOutputA.csv");
            ReadBuffor FileC = new ReadBuffor(100, "TapeOutputB.csv");

            Record recordA = FileB.NextRecord(EndInformations);
            Record recordB = FileC.NextRecord(EndInformations);
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
                            FileA.SaveRecord(recordB, EndInformations, option);
                            tempValueB = new Record(recordB.Word);
                            recordB = FileC.NextRecord(EndInformations);
                        }
                        else
                        {
                            FileA.SaveRecord(recordA, EndInformations, option);
                            tempValueA = new Record(recordA.Word);
                            recordA = FileB.NextRecord(EndInformations);
                        }
                    }
                    //tutaj zbieramy tasme B co zostala
                    if (recordB != null && recordB.CompareTo(tempValueB) >= 0)
                    {
                        while (recordB!=null && recordB.CompareTo(tempValueB) >= 0)
                        {
                            FileA.SaveRecord(recordB, EndInformations, option);
                            tempValueB = new Record(recordB.Word);
                            recordB = FileC.NextRecord(EndInformations);
                        }
                        tempValueA = new Record("!");
                        tempValueB = new Record("!");
                    }
                    //tutaj zbieramy tasme A co zostala
                    else if (recordA != null && recordA.CompareTo(tempValueA) >= 0)
                    {
                        while (recordA !=null && recordA.CompareTo(tempValueA) >= 0)
                        {
                            FileA.SaveRecord(recordA, EndInformations, option);
                            tempValueA = new Record(recordA.Word);
                            recordA = FileB.NextRecord(EndInformations);
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
                        FileA.SaveRecord(recordB, EndInformations, option);
                        tempValueB = new Record(recordB.Word);
                        recordB = FileC.NextRecord(EndInformations);
                    }
                }
                else if(recordB==null && recordA != null)
                {
                    while (recordA != null)
                    {
                        FileA.SaveRecord(recordA, EndInformations, option);
                        tempValueA = new Record(recordA.Word);
                        recordA = FileB.NextRecord(EndInformations);
                    }
                }
                else
                {
                    break;
                }
            }
            FileA.saveRestValues(EndInformations, option);
            return true;
        }
    }
}
