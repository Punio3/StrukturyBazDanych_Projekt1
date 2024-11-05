using Microsoft.VisualBasic.FileIO;
using System;

namespace StrukturyBazDanychC__Projekt_1
{
    class Program
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1) Zapis z klawiatury");
            Console.WriteLine("2) Import z pliku");
            Console.WriteLine("3) Generowanie losowych rekordów do pliku o stałym rozmiarze");
            Console.WriteLine("4) Odczyt taśmy");
            Console.WriteLine("5) Odczyt informacji o taśmie");
            Console.WriteLine("6) Sortowanie taśmy");
            Console.WriteLine("7) Sortowanie taśmy z odczytem");
            Console.WriteLine("0) Wyjście");
        }

        static void Main(string[] args)
        {
            int option = -1;

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
                            Console.WriteLine("Opcja 4: Odczyt taśmy\n");
                            // Implementacja odczytu taśmy
                            break;
                        case 5:
                            Console.WriteLine("Opcja 5: Odczyt informacji o taśmie\n");
                            // Implementacja odczytu informacji o taśmie
                            break;
                        case 6:
                            Console.WriteLine("Opcja 6: Sortowanie taśmy\n");
                            // Implementacja sortowania taśmy
                            break;
                        case 7:
                            Console.WriteLine("Opcja 7: Sortowanie taśmy z odczytem\n");
                            // Implementacja sortowania z odczytem
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
                    writer.Write(record.ToFixedSizeFormat());
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
                    writer.Write(record.ToFixedSizeFormat());
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
    }
}
