namespace Zaliczenie;

// Dziedziczenie: Pistolet dziedziczy po Bron
public class Pistolet : Bron
{
    public Pistolet(int numer, string nazwa) : base(numer, nazwa) { }

    // Polimorfizm: konkretna implementacja metody Typ()
    public override string Typ() => "Pistolet";
    
    // Konstruktor dla deserializacji JSON
    public Pistolet() : base(0, "") { }
}