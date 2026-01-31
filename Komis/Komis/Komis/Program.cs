using System;

class Program
{
    static void Main()
    {
        Komis komis = new Komis();
        bool dziala = true;

        while (dziala)
        {
            Console.WriteLine("\n--- KOMIS ---");
            Console.WriteLine("1. Wyświetl");
            Console.WriteLine("2. Dodaj");
            Console.WriteLine("3. Usuń");
            Console.WriteLine("4. Zmień kolor");
            Console.WriteLine("5. Wyjście");
            Console.Write("Wybór: ");

            switch (Console.ReadLine())
            {
                case "1":
                    komis.Wyswietl();
                    break;
                case "2":
                    komis.Dodaj();
                    break;
                case "3":
                    komis.Usun();
                    break;
                case "4":
                    komis.EdytujKolor();
                    break;
                case "5":
                    dziala = false;
                    break;
            }
        }
    }
}