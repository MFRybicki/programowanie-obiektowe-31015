using System;

public abstract class Pojazd
{
    private string marka;
    private string model;
    private string kolor;

    public string Marka
    {
        get => marka;
        set => marka = value;
    }

    public string Model
    {
        get => model;
        set => model = value;
    }

    public string Kolor
    {
        get => kolor;
        set => kolor = value;
    }

    public Pojazd(string marka, string model, string kolor)
    {
        Marka = marka;
        Model = model;
        Kolor = kolor;
    }

    public virtual string Opis()
    {
        return $"{Marka} {Model}, kolor: {Kolor}";
    }
}