using System.Text.RegularExpressions;

var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\File.csv";
var copyPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";

Console.WriteLine("1 - Aggiungi contatto");
Console.WriteLine("2 - Modifica contatto");
Console.WriteLine("3 - Elimina contatto");
Console.WriteLine("4 - Elimina rubrica");
Console.WriteLine("5 - Esporta rubrica\n");
string sceltaString = Console.ReadLine();
int sceltaInt;
int.TryParse(sceltaString, out sceltaInt);
while (sceltaInt > 5 || sceltaInt < 1)
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
        if (fileExists == false)
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
        Console.WriteLine("Inserisci email dell'utente da modificare");
        var emailSearch = Console.ReadLine();
        var righe = File.ReadAllLines(folderPath).ToList();
        for (int i = 0; i < righe.Count; i++)
        {

            var colonne = righe[i].Split(',');

            if (colonne[2].Trim() == emailSearch)
            {
                emailTrovata = true;
                Console.Write("Inserisci nome: ");
                nome = Console.ReadLine();
                Console.Write("Inserisci cognome: ");
                cognome = Console.ReadLine();
                Console.Write("Inserisci email: ");
                email = Console.ReadLine();
                Console.Write("Inserisci numero di telefono: ");
                telefono = Console.ReadLine();

                colonne[0] = nome;
                colonne[1] = cognome;
                colonne[2] = email;
                colonne[3] = telefono;

                righe[i] = string.Join(", ", colonne);
                File.WriteAllLines(folderPath, righe);
                var righeNonVuote = righe.Where(riga => !string.IsNullOrWhiteSpace(riga)).ToList();
                File.WriteAllLines(folderPath, righeNonVuote);
                break;
            }
        }
        if (emailTrovata == false)
        {
            Console.WriteLine("Email non trovata");
        }
        break;

    case 3:
        emailTrovata = false;
        Console.WriteLine("Inserisci email dell'utente da eliminare");
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
        if (emailTrovata == false)
        {
            Console.WriteLine("Email non trovata");
        }
        break;
    case 4:
        Console.WriteLine("Sei sicuro di voler eliminare la rubrica?");
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
}



