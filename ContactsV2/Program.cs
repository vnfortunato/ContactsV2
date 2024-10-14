var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\File.csv";
var copyPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";

Console.WriteLine("1 - Aggiungi contatto");
Console.WriteLine("2 - Modifica contatto");
Console.WriteLine("3 - Elimina contatto");
Console.WriteLine("4 - Elimina rubrica");
Console.WriteLine("5 - Esporta rubrica");
Console.WriteLine("6 - Importa rubrica");
string sceltaString = Console.ReadLine();
int sceltaInt;
int.TryParse(sceltaString, out sceltaInt);
while (sceltaInt > 6 || sceltaInt < 1)
{
    sceltaString = Console.ReadLine();
    int.TryParse(sceltaString, out sceltaInt);
}
switch (sceltaInt)
{
    case 1:
        string[] array = { "Nome", "Cognome", "Email", "Telefono\n" };
        string separator = ", ";
        string Array = string.Join(separator, array);
        bool fileExists = File.Exists(folderPath);
        if (!fileExists)
        {
            File.AppendAllText(folderPath, Array);
        }
        Console.Write("Inserisci nome: ");
        var nome = Console.ReadLine();
        Console.Write("Inserisci cognome: ");
        var cognome = Console.ReadLine();
        Console.Write("Inserisci email: ");
        var email = Console.ReadLine();
        Console.Write("Inserisci numero di telefono: ");
        var telefono = Console.ReadLine();
        string[] Array2 = { nome, cognome, email, telefono + "\n" };
        string Array3 = string.Join(separator, Array2);
        File.AppendAllText(folderPath, Array3);
        break;

    case 2:
        bool emailTrovata = false;
        Console.Write("Inserisci email dell'utente da modificare: ");
        var emailSearch = Console.ReadLine();
        var righe = File.ReadAllLines(folderPath).ToList();
        for (int i = 0; i < righe.Count; i++)
        {

            var colonne = righe[i].Split(',');

            if (colonne[2].Trim() == emailSearch)
            {
                emailTrovata = true;
                Console.Write("Inserisci nuovo nome: ");
                var nuovoNome = Console.ReadLine();
                Console.Write("Inserisci nuovo cognome: ");
                var nuovoCognome = Console.ReadLine();
                Console.Write("Inserisci nuova email: ");
                var nuovaEmail = Console.ReadLine();
                Console.Write("Inserisci nuovo numero di telefono: ");
                var nuovoTelefono = Console.ReadLine();

                colonne[0] = nuovoNome;
                colonne[1] = nuovoCognome;
                colonne[2] = nuovaEmail;
                colonne[3] = nuovoTelefono;

                righe[i] = string.Join(", ", colonne);
                File.WriteAllLines(folderPath, righe);
                var righeNonVuote = righe.Where(riga => !string.IsNullOrWhiteSpace(riga)).ToList();
                File.WriteAllLines(folderPath, righeNonVuote);
                break;
            }
        }
        if (!emailTrovata)
        {
            Console.WriteLine("Email non trovata");
        }
        break;

    case 3:
        emailTrovata = false;
        Console.Write("Inserisci email dell'utente da eliminare: ");
        emailSearch = Console.ReadLine();
        righe = File.ReadAllLines(folderPath).ToList();
        for (int i = 0; i < righe.Count; i++)
        {

            var colonne = righe[i].Split(',');

            if (colonne[2].Trim() == emailSearch)
            {
                emailTrovata = true;
                Console.WriteLine($"""Sei sicuro di voler eliminare l'utente "{emailSearch}"? (s/n)""");
                var EliminazioneUtente = Console.ReadLine();
                if (EliminazioneUtente == "s")
                {
                    righe.RemoveAt(i);
                    File.WriteAllLines(folderPath, righe);
                    break;
                }
            }
        }
        if (!emailTrovata)
        {
            Console.WriteLine("Email non trovata");
        }
        break;
    case 4:
        Console.WriteLine("Sei sicuro di voler eliminare la rubrica? (s/n)");
        var EliminazioneRubrica = Console.ReadLine();
        if (EliminazioneRubrica == "s")
        {
            Console.WriteLine("Rubrica eliminata");
            File.Delete(folderPath);

        }
        else
        {
            Console.WriteLine("Operazione annullata");
        }
        break;
    case 5:
        {
            Console.WriteLine("Inserisci nome nuovo registro");
            var fileCopy = Console.ReadLine();
            File.Copy(folderPath, copyPath + fileCopy + ".csv");
        }
        break;
    case 6:
        {

        }
}



