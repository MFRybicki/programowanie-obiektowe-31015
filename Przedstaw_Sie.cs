class Osoba
{
    public string Imie;
    public int Wiek;

    public void PrzedstawSie()
    {
        Console.WriteLine($"Jestem {Imie} i mam {Wiek} lat.");
    }
}

class Program
{
    static void Main()
    {
        Osoba osoba1 = new Osoba();
        osoba1.Imie = "Ania";
        osoba1.Wiek = 12;
        osoba1.PrzedstawSie();

        Osoba osoba2 = new Osoba();
        osoba2.Imie = "Wojtek";
        osoba2.Wiek = 12;
        osoba2.PrzedstawSie();
    }
}