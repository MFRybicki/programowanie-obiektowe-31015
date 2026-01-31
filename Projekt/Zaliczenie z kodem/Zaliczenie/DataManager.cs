using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Zaliczenie;

public static class DataManager
{
    // Ścieżka do pliku dane.json w tym samym katalogu, co exe
    private static string FilePath => Path.Combine(AppContext.BaseDirectory, "dane.json");

    private static JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter()
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // ---------------- ZAPIS ----------------
    public static void Zapisz(List<Bron> bronie)
    {
        // Tworzymy katalog, jeśli go nie ma (przydatne jeśli w przyszłości zmienimy lokalizację)
        string dir = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string json = JsonSerializer.Serialize(bronie, options);
        File.WriteAllText(FilePath, json);
    }

    // ---------------- WCZYTYWANIE ----------------
    public static List<Bron> Wczytaj()
    {
        // Jeśli plik nie istnieje, tworzymy go od razu jako pusty JSON
        if (!File.Exists(FilePath))
        {
            Zapisz(new List<Bron>());
            return new List<Bron>();
        }

        try
        {
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Bron>>(json, options) ?? new List<Bron>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Wystąpił błąd przy wczytywaniu JSON: {ex.Message}");
            return new List<Bron>();
        }
    }
}