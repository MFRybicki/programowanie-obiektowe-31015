namespace Zaliczenie;

// Dziedziczenie: Karabin dziedziczy po Bron
public class Karabin : Bron
{
    public Karabin(int numer, string nazwa) : base(numer, nazwa) { }

    // Polimorfizm: konkretna implementacja metody Typ()
    public override string Typ() => "Karabin";
    
    // Konstruktor dla deserializacji JSON
    public Karabin() : base(0, "") { }
}