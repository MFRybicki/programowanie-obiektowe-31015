using System;
using System.Collections.Generic;

public class Komis
{
    private List<Pojazd> pojazdy = new List<Pojazd>();

    public void Wyswietl()
    {
        if (pojazdy.Count == 0)
        {
            Console.WriteLine("Brak pojazdów.");
            return;
        }

        for (int i = 0; i < pojazdy.Count; i++)
            Console.WriteLine($"{i}: {pojazdy[i].Opis()}");
    }

    public void Dodaj()
    {
        Console.Write("Marka: ");
        string marka = Console.ReadLine();

        Console.Write("Model: ");
        string model = Console.ReadLine();

        Console.Write("Kolor: ");
        string kolor = Console.ReadLine();

        pojazdy.Add(new Samochod(marka, model, kolor));
        Console.WriteLine("Dodano pojazd.");
    }

    public void Usun()
    {
        Wyswietl();
        Console.Write("Indeks: ");

        if (int.TryParse(Console.ReadLine(), out int i)
            && i >= 0 && i < pojazdy.Count)
        {
            pojazdy.RemoveAt(i);
            Console.WriteLine("Usunięto.");
        }
    }

    public void EdytujKolor()
    {
        Wyswietl();
        Console.Write("Indeks: ");

        if (int.TryParse(Console.ReadLine(), out int i)
            && i >= 0 && i < pojazdy.Count)
        {
            Console.Write("Nowy kolor: ");
            pojazdy[i].Kolor = Console.ReadLine();
        }
    }
}