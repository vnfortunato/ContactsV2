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
            string[] Array2 = { nome, cognome, email, telefono + "\n"};
            string Array3 = string.Join(separator, Array2);
            File.AppendAllText(folderPath, Array3);
            break;
    case 2:
        Console.Write("Inserisci email contatto da modificare: ");
        var emailSearch = Console.ReadLine();
        using (StreamReader sr = new StreamReader(folderPath))
        {
            var fileSearch = sr.ReadToEnd();
            bool fileSearchBool = fileSearch.Contains(emailSearch);
            while (fileSearchBool == false)
            {
                Console.WriteLine("Email non trovata");
                Console.Write("Inserisci email contatto da modificare: ");
                emailSearch = Console.ReadLine();
                if (fileSearch.Contains(emailSearch))
                {
                    break;
                }
            }
            Console.Write("Inserisci nuovo nome: ");
            var newName = Console.ReadLine();
            Console.Write("Inserisci nuovo cognome: ");
            var newSurname = Console.ReadLine();
            Console.Write("Inserisci nuova email: ");
            var newEmail = Console.ReadLine();
            Console.Write("Inserisci nuovo numero di telefono: ");
            var newPhone = Console.ReadLine();

            var righe = File.ReadAllLines(folderPath).ToList();

            for (int i = 0; i < righe.Count; i++)
            {
                var colonne = righe[i].Split(',');

                if (colonne[2].Trim() == emailSearch)
                {

                    colonne[0] = newName;
                    colonne[1] = newSurname;
                    colonne[2] = newEmail;
                    colonne[3] = newPhone;

                    righe[i] = string.Join(", ", colonne);
                }
            }
            File.WriteAllLines(folderPath, righe);
        }
        break;

        case 3:
            Console.WriteLine("Inserisci email dell'utente da eliminare");
                var emailSearch2 = Console.ReadLine();
        var righe3 = File.ReadAllLines(folderPath).ToList();
        for (int i = 0; i < righe3.Count; i++) //cicla nel file
        {

            var colonne3 = righe3[i].Split(',');

            if (colonne3[2].Trim() == emailSearch2)
            {
                Console.WriteLine($"""Sei sicuro di voler eliminare l'utente "{emailSearch2}"? (s/n)""");
                var s = Console.ReadLine();
                if (s == "s")
                {
                    var righe2 = File.ReadAllLines(folderPath).ToList();

                            colonne2[0] = "";
                            colonne2[1] = "";
                            colonne2[2] = "";
                            colonne2[3] = "";

                            righe2[i] = string.Join("", colonne2);
                        }
                    }
                    File.WriteAllLines(folderPath, righe2);
                    var righeNonVuote = righe2.Where(riga2 => !string.IsNullOrWhiteSpace(riga2)).ToList();
                    File.WriteAllLines(folderPath, righeNonVuote);
                }
            }
            else
            {
                Console.WriteLine("Email non trovata");
            }
        }
        break;
    case 4:
        Console.WriteLine("Sei sicuro di voler eliminare la rubrica?");
        s = Console.ReadLine();
        if (s == "s")
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



