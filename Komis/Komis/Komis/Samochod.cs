public class Samochod : Pojazd
{
    public Samochod(string marka, string model, string kolor)
        : base(marka, model, kolor)
    {
    }

    public override string Opis()
    {
        return "Samochód: " + base.Opis();
    }
}