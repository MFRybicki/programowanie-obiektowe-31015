using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Zaliczenie;

class Program
{
    static List<Bron> bronie = new();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Logowanie();

        // ---------------- Polimorfizm przy wczytywaniu JSON ----------------
        bronie = DataManager.Wczytaj(); // Polimorfizm: JSON rozpoznaje Pistolet i Karabin dzięki [JsonDerivedType]
        if (!bronie.Any())
        {
            // jeśli plik pusty lub pierwszy raz, tworzymy startowe dane i zapisujemy je
            bronie = UtworzBronie();
            DataManager.Zapisz(bronie);
        }

        bool dziala = true;
        while (dziala)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  REZERWACJA I WYPOŻYCZENIE BRONI");
            Console.WriteLine("=================================\n");

            Console.WriteLine("1. Pokaż broń");
            Console.WriteLine("2. Pokaż wypożyczoną / zarezerwowaną broń");
            Console.WriteLine("3. Zapisz i wyjdź\n");
            Console.Write("Wybierz opcję: ");

            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1": MenuPokazBron(); break;
                case "2": MenuMojaBron(); break;
                case "3": DataManager.Zapisz(bronie); dziala = false; break;
            }
        }
    }

    // ---------------- LOGOWANIE ----------------
    static void Logowanie()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("REZERWACJA I WYPOŻYCZENIE BRONI\n");
            Console.WriteLine("Proszę zalogować się do systemu:\n");
            Console.Write("Wprowadź login: ");
            string login = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("\nBłędny login, spróbuj ponownie.");
                Pauza();
                continue;
            }

            Console.Write("\nPodaj hasło: ");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(150);
                Console.Write("*");
            }
            Pauza(800);
            break;
        }
    }

    // ---------------- DANE ----------------
    static List<Bron> UtworzBronie()
    {
        return new List<Bron>
        {
            // Dziedziczenie: tworzymy obiekty Pistolet i Karabin, które dziedziczą po Bron
            new Pistolet(1, "Glock 17"),
            new Pistolet(2, "Walther P99"),
            new Pistolet(3, "Beretta 92"),
            new Pistolet(4, "CZ Shadow"),
            new Pistolet(5, "SIG Sauer"),

            new Karabin(101, "AK-47"),
            new Karabin(102, "AR-15"),
            new Karabin(103, "G36"),
            new Karabin(104, "SCAR-L"),
            new Karabin(105, "M4A1")
        };
    }

    // ---------------- MENU: POKAŻ BROŃ ----------------
    static void MenuPokazBron()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== DOSTĘPNA BROŃ ===\n");

            foreach (var bron in bronie)
            {
                // Polimorfizm: bron.Typ() działa różnie dla Pistolet i Karabin
                string status = bron.DataRezerwacji != null ? $"zarezerwowana na {bron.DataRezerwacji:dd/MM/yyyy}" : (bron.Dostepna ? "dostępna" : "niedostępna");
                Console.WriteLine($"[{bron.NumerKatalogowy}] | {bron.Nazwa,-15} | {bron.Typ()} | {status}");
            }

            Console.WriteLine("\n1. Wypożycz broń");
            Console.WriteLine("2. Zarezerwuj broń");
            Console.WriteLine("3. Wróć do menu\n");
            Console.Write("Wybierz opcję: ");
            string wybor = Console.ReadLine();

            if (wybor == "1") WypozyczBron();
            else if (wybor == "2") ZarezerwujBron();
            else if (wybor == "3") return;
        }
    }

    // ---------------- MENU: MOJA BROŃ ----------------
    static void MenuMojaBron()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== TWOJA BROŃ ===\n");

            var mojaBron = bronie.Where(b => !b.Dostepna || b.DataRezerwacji != null).ToList();

            if (!mojaBron.Any())
            {
                Console.WriteLine("Brak wypożyczeń i rezerwacji.\n");
            }

            foreach (var bron in mojaBron)
            {
                string status = !bron.Dostepna ? "wypożyczona" : $"zarezerwowana na {bron.DataRezerwacji:dd/MM/yyyy}";
                Console.WriteLine($"[{bron.NumerKatalogowy}] | {bron.Nazwa,-15} | {status}");
            }

            Console.WriteLine("\n1. Zwróć broń");
            Console.WriteLine("2. Anuluj rezerwację");
            Console.WriteLine("3. Wróć do menu\n");
            Console.Write("Wybierz opcję: ");
            string wybor = Console.ReadLine();

            if (wybor == "1") ZwrocBron();
            else if (wybor == "2") AnulujRezerwacje();
            else if (wybor == "3") return;
        }
    }

    // ---------------- POMOCNICZE ----------------
    static Bron WybierzBron()
    {
        Console.Write("\nPodaj numer katalogowy: ");
        if (!int.TryParse(Console.ReadLine(), out int numer))
        {
            Console.WriteLine("Nieprawidłowy numer.");
            Pauza();
            return null;
        }

        var bron = bronie.FirstOrDefault(b => b.NumerKatalogowy == numer);
        if (bron == null)
        {
            Console.WriteLine("Nie znaleziono broni.");
            Pauza();
        }

        return bron;
    }

    static void Pauza(int ms = 1500) => Thread.Sleep(ms);

    // ---------------- AKCJE ----------------
    static void WypozyczBron()
    {
        var bron = WybierzBron();
        if (bron != null && bron.Dostepna)
        {
            bron.Wypozycz();
            Console.WriteLine("Broń wypożyczona.");
            DataManager.Zapisz(bronie);
        }
        else if (bron != null)
        {
            Console.WriteLine("Nie można wypożyczyć tej broni.");
        }

        Pauza();
    }

    static void ZarezerwujBron()
    {
        var bron = WybierzBron();
        if (bron == null) return;

        Console.Write("Podaj datę rezerwacji (DD/MM/RRRR): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
        {
            bron.Zarezerwuj(data);
            Console.WriteLine("Rezerwacja zapisana.");
            DataManager.Zapisz(bronie);
        }
        else
        {
            Console.WriteLine("Nieprawidłowa data.");
        }

        Pauza();
    }

    static void ZwrocBron()
    {
        var bron = WybierzBron();
        if (bron != null)
        {
            bron.Zwroc();
            Console.WriteLine("Broń zwrócona.");
            DataManager.Zapisz(bronie);
        }

        Pauza();
    }

    static void AnulujRezerwacje()
    {
        var bron = WybierzBron();
        if (bron != null && bron.DataRezerwacji != null)
        {
            bron.AnulujRezerwacje();
            Console.WriteLine("Rezerwacja anulowana.");
            DataManager.Zapisz(bronie);
        }

        Pauza();
    }
}
