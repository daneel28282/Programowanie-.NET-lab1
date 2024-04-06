//ININ4(hybryda)_PR1.2 73980
//--- zad 3 ------------------------------------------------------------------------------------------------------

using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/tekst.txt";

        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string content = streamReader.ReadToEnd();
                    Console.WriteLine("Zawartość pliku:");
                    Console.WriteLine(content);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik nie został znaleziony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}

//--- zad 4 ------------------------------------------------------------------------------------------------------

using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/tekst.txt";

        try
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    // wyświetl w odwrotnej kolejności
                    for (int i = line.Length - 1; i >= 0; i--)
                    {
                        Console.Write(line[i]);
                    }
                    Console.WriteLine();
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik nie został znaleziony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}

//--- zad 5 ------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Wybierz opcję:");
        Console.WriteLine("1. Zapisz dane do pliku");
        Console.WriteLine("2. Odczytaj dane z pliku");

        char choice = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (choice)
        {
            case '1':
                SaveDataToFile();
                break;
            case '2':
                ReadDataFromFile();
                break;
            default:
                Console.WriteLine("Niepoprawny wybór.");
                break;
        }
    }

    static void SaveDataToFile()
    {
        try
        {
            Console.WriteLine("Podaj imię:");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj wiek:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj adres:");
            string address = Console.ReadLine();

            UserData userData = new UserData(name, age, address);

            using (FileStream fileStream = new FileStream("userData.bin", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, userData);
            }

            Console.WriteLine("Dane zostały zapisane do pliku.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }

    static void ReadDataFromFile()
    {
        try
        {
            using (FileStream fileStream = new FileStream("userData.bin", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                UserData userData = (UserData)formatter.Deserialize(fileStream);

                Console.WriteLine("Dane odczytane z pliku:");
                Console.WriteLine($"Imię: {userData.Name}");
                Console.WriteLine($"Wiek: {userData.Age}");
                Console.WriteLine($"Adres: {userData.Address}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik nie został znaleziony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}

[Serializable]
class UserData
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }

    public UserData(string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }
}

//--- zad 6 ------------------------------------------------------------------------------------------------------
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string sourceFilePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/randomtext.txt";
            string destinationFilePath = "C:/Users/Daniel-/source/repos/dotNet_lab1/destination.txt";

			// do zad 7
            Console.WriteLine("podaj wielkość pliku w bajtach: ");
            var fileSize = int.Parse(Console.ReadLine());
            var filBuffer = new byte[fileSize];
            new Random().NextBytes(filBuffer);
            var text = System.Text.Encoding.Default.GetString(filBuffer);
				
            //File.WriteAllText("C:/Users/Daniel-/source/repos/dotNet_lab1/randomtext.txt", text);
			File.WriteAllText(sourceFilePath, text);
				// do zad 7
				
            using (FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            {
                //using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                using (FileStream destinationStream = new FileStream(destinationFilePath, FileMode.Append, FileAccess.Write))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
					
                    while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationStream.Write(buffer, 0, bytesRead);
                    }
                }
            }

            Console.WriteLine("Kopiowanie zakończone pomyślnie.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Plik źródłowy nie został znaleziony.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        }
    }
}

//--- zad 7 dodane do 6 ------------------------------------------------------------------------------------------------------
