//ININ4(hybryda)_PR1.2 73980

using System;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        ManagerZadan manager = new ManagerZadan();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("===== Panel Zarządzania =====");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Usuń zadanie");
            Console.WriteLine("3. Edytuj zadanie");
            Console.WriteLine("4. Oznacz zadanie jako ukończone/nieukończone");
            Console.WriteLine("5. Sortuj zadania");
            Console.WriteLine("6. Wyświetl zadania");
            Console.WriteLine("7. Zapisz listę zadań do pliku");
            Console.WriteLine("8. Wczytaj listę zadań z pliku");
            Console.WriteLine("9. Wyjście");
            Console.Write("\nWybierz opcję: ");

            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    manager.DodajZadanie();
                    break;
                case '2':
                    manager.UsunZadanie();
                    break;
                case '3':
                    manager.EdytujZadanie();
                    break;
                case '4':
                    manager.OznaczZadanie();
                    break;
                case '5':
                    manager.SortujZadania();
                    break;
                case '6':
                    manager.WyswietlZadania();
                    break;
                case '7':
                    manager.ZapiszDoPliku();
                    break;
                case '8':
                    manager.WczytajZPliku();
                    break;
                case '9':
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\n");
                    Console.WriteLine("Niepoprawny wybór.\n");
                    break;
            }

            Console.WriteLine();
        }
    }
}

public class Zadanie
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    public DateTime DataZakonczenia { get; set; }
    public bool CzyWykonane { get; set; }

    public Zadanie() { }

    public Zadanie(int id, string nazwa, string opis, DateTime dataZakonczenia, bool czyWykonane)
    {
        Id = id;
        Nazwa = nazwa;
        Opis = opis;
        DataZakonczenia = dataZakonczenia;
        CzyWykonane = czyWykonane;
    }

    public override string ToString()
    {
        //return $"Id: {Id}, Nazwa: {Nazwa}, Opis: {Opis}, Data Zakonczenia: {DataZakonczenia.ToLongDateString()}, Czy Wykonane: {CzyWykonane}";
        return $"Id: {Id}, Nazwa: {Nazwa}, Opis: {Opis}, Data Zakonczenia: {DataZakonczenia.ToShortDateString()}, Czy Wykonane: {CzyWykonane}";
    }
}

class ManagerZadan
{
    private readonly string filePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/";
    private DateTime endDate;
    //string filePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/zadania.xml";
    private List<Zadanie> listaZadan = new List<Zadanie>();

    public void DodajZadanie()
    {
        Console.WriteLine("Dodawanie nowego zadania:");
        Console.Write("Podaj nazwę zadania: ");
        string nazwa = Console.ReadLine();
        Console.Write("Podaj opis zadania: ");
        string opis = Console.ReadLine();
        //Console.Write("Podaj datę zakończenia zadania (RRRR-MM-DD): ");
        //DateTime dataZakonczenia = DateTime.Parse(Console.ReadLine());
    
        ReadDate();

        int id = listaZadan.Count > 0 ? listaZadan.Max(z => z.Id) + 1 : 1;
        Zadanie noweZadanie = new Zadanie(id, nazwa, opis, endDate, false);
        listaZadan.Add(noweZadanie);

        Console.WriteLine("Zadanie zostało dodane.");
    }
    

    public void ReadDate()
    {
        var dFormat = new[] { "yyyy.MM.dd", "yyyy/MM/dd", "yyyy-MM-dd", "yyyyMMdd" };
        bool validate = true;
        while (validate)
        {
            Console.Write("Podaj datę zakończenia zadania (yyyy.MM.dd, yyyy/MM/dd, yyyy-MM-dd, yyyyMMdd): ");
            string rDate = Console.ReadLine();

            DateTime d;
            if (DateTime.TryParseExact(rDate, dFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out d))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Poprawny format daty");
                validate = false;
                //dataZakonczenia = DateTime.Parse(readAddMeeting);
                endDate = DateTime.ParseExact(rDate, dFormat, null);

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błędny format daty: \"{0}\"", rDate);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
      
    }

    public void UsunZadanie()
    {
        if (listaZadan.Count == 0)
        {
            Console.WriteLine("Lista zadań jest pusta.");
            return;
        }

        WyswietlZadania();
        Console.Write("Podaj numer zadania do usunięcia: ");
        int numerZadania = int.Parse(Console.ReadLine());

        if (numerZadania >= 1 && numerZadania <= listaZadan.Count)
        {
            listaZadan.RemoveAt(numerZadania - 1);
            Console.WriteLine("Zadanie zostało usunięte.");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer zadania.");
        }
    }

    public void EdytujZadanie()
    {
        if (listaZadan.Count == 0)
        {
            Console.WriteLine("Lista zadań jest pusta.");
            return;
        }

        WyswietlZadania();
        Console.Write("Podaj numer zadania do edycji: ");
        int numerZadania = int.Parse(Console.ReadLine());

        if (numerZadania >= 1 && numerZadania <= listaZadan.Count)
        {
            Zadanie zadanie = listaZadan[numerZadania - 1];

            Console.WriteLine("Edycja zadania:");
            Console.Write("Nowa nazwa zadania (enter jeśli bez zmian): ");
            string nazwa = Console.ReadLine();
            if (!string.IsNullOrEmpty(nazwa))
                zadanie.Nazwa = nazwa;

            Console.Write("Nowy opis zadania (enter jeśli bez zmian): ");
            string opis = Console.ReadLine();
            if (!string.IsNullOrEmpty(opis))
                zadanie.Opis = opis;

            Console.Write("Nowa data zakończenia zadania (enter jeśli bez zmian dowolny znak jeśli zmiana): ");
            //string dataZakonczeniaString = Console.ReadLine();
            string zmiana = Console.ReadLine();

            if (!string.IsNullOrEmpty(zmiana))
            {
                ReadDate();
                //DateTime dataZakonczenia = DateTime.Parse(dataZakonczeniaString);
                zadanie.DataZakonczenia = endDate;
            }

            Console.WriteLine("Zadanie zostało zaktualizowane.");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer zadania.");
        }
    }

    public void OznaczZadanie()
    {
        if (listaZadan.Count == 0)
        {
            Console.WriteLine("Lista zadań jest pusta.");
            return;
        }

        WyswietlZadania();
        Console.Write("Podaj numer zadania do oznaczenia (ukończone/nieukończone): ");
        int numerZadania = int.Parse(Console.ReadLine());

        if (numerZadania >= 1 && numerZadania <= listaZadan.Count)
        {
            Zadanie zadanie = listaZadan[numerZadania - 1];
            zadanie.CzyWykonane = !zadanie.CzyWykonane;

            Console.WriteLine("Zadanie zostało oznaczone.");
        }
        else
        {
            Console.WriteLine("Niepoprawny numer zadania.");
        }
    }

    public void SortujZadania()
    {
        if (listaZadan.Count == 0)
        {
            Console.WriteLine("Lista zadań jest pusta.");
            return;
        }

        Console.WriteLine("Wybierz kryterium sortowania:");
        Console.WriteLine("1. Nazwa");
        Console.WriteLine("2. Data zakończenia");
        Console.Write("Wybierz opcję: ");
        char choice = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (choice)
        {
            case '1':
                listaZadan = listaZadan.OrderBy(z => z.Nazwa).ToList();
                Console.WriteLine("Zadania zostały posortowane po nazwie.");
                break;
            case '2':
                listaZadan = listaZadan.OrderBy(z => z.DataZakonczenia).ToList();
                Console.WriteLine("Zadania zostały posortowane po dacie zakończenia.");
                break;
            default:
                Console.WriteLine("Niepoprawny wybór.");
                break;
        }
    }

    public void WyswietlZadania()
    {
        if (listaZadan.Count == 0)
        {
            Console.WriteLine("\n Lista zadań jest pusta.");
            return;
        }

        Console.WriteLine("\n Lista zadań:");
        for (int i = 0; i < listaZadan.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {listaZadan[i]}");
        }
    }

    public void ZapiszDoPliku()
    {
        try
        {
            Console.Write("Podaj nazwę pliku do zapisu: ");
            string nazwaPliku = $"{Console.ReadLine()}.xml";
            string fileFull = filePath+nazwaPliku;

            using (FileStream fileStream = new FileStream(fileFull, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
                serializer.Serialize(fileStream, listaZadan);
            }

            Console.WriteLine("Lista zadań została zapisana do pliku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas zapisu do pliku: {ex.Message}");
        }
    }

    public void WczytajZPliku()
    {
        try
        {
            Console.Write("Podaj nazwę pliku do wczytania: ");
            string nazwaPliku = $"{Console.ReadLine()}.xml";
            string fileFull = filePath + nazwaPliku;

            using (FileStream fileStream = new FileStream(fileFull, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Zadanie>));
                listaZadan = (List<Zadanie>)serializer.Deserialize(fileStream);
            }

            Console.WriteLine("Lista zadań została wczytana z pliku.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik z listą zadań nie istnieje.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd podczas wczytywania z pliku: {ex.Message}");
        }
    }
}
