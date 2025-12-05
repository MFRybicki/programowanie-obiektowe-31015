string haslo;
do
{
    Console.Write("Podaj hasło: ");
    haslo = Console.ReadLine();
}
while (haslo != "admin123");

Console.WriteLine("Zalogowano pomyślnie!");