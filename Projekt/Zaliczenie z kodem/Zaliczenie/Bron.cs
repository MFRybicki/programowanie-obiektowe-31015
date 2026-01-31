using System;
using System.Text.Json.Serialization;

namespace Zaliczenie;

// Polimorfizm: JSON wie, że Bron może być Pistolet lub Karabin
[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Pistolet), "Pistolet")]
[JsonDerivedType(typeof(Karabin), "Karabin")]
public abstract class Bron
{
    public int NumerKatalogowy { get; set; }       // public: dostępne dla wszystkich
    public string Nazwa { get; set; }              // public: dostępne dla wszystkich

    [JsonInclude]                                  // pozwala JSON ustawić wartość przy deserializacji
    public bool Dostepna { get; private set; } = true;    

    [JsonInclude]                                  // pozwala JSON ustawić wartość przy deserializacji
    public DateTime? DataRezerwacji { get; private set; } // Hermetyzacja: kontrola zapisu

    protected Bron(int numerKatalogowy, string nazwa)
    {
        NumerKatalogowy = numerKatalogowy;
        Nazwa = nazwa;
        Dostepna = true;
    }

    // Polimorfizm: metody działają inaczej w klasach pochodnych
    public abstract string Typ();

    public void Wypozycz()      // Hermetyzacja: kontrolujemy dostęp do zmiany stanu broni
    {
        Dostepna = false;
        DataRezerwacji = null;
    }

    public void Zwroc()          // Hermetyzacja
    {
        Dostepna = true;
    }

    public void Zarezerwuj(DateTime data)  // Hermetyzacja
    {
        Dostepna = true; // broń wciąż dostępna
        DataRezerwacji = data;
    }

    public void AnulujRezerwacje() // Hermetyzacja
    {
        DataRezerwacji = null;
    }
}