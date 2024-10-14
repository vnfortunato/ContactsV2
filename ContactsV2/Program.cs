var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\File.csv";
var copyPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";

Console.WriteLine("1 - Aggiungi contatto");
Console.WriteLine("2 - Modifica contatto");
Console.WriteLine("3 - Elimina contatto");
Console.WriteLine("4 - Elimina rubrica");
Console.WriteLine("5 - Esporta rubrica");
Console.WriteLine("6 - Importa rubrica");
Console.WriteLine("7 - Visualizza contatti\n");
string sceltaString = Console.ReadLine();
int sceltaInt;
int.TryParse(sceltaString, out sceltaInt);
while (sceltaInt > 7 || sceltaInt < 1)
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
        var righe = File.ReadAllLines(folderPath).ToList();
        bool emailEsistente = false;
        for (int i = 0; i < righe.Count; i++)

        {
            var colonne = righe[i].Split(',');


            if (colonne[2].Trim() == email)
            {
                Console.WriteLine("\nContatto già esistente");
                emailEsistente = true;
                break;
            }
        }
        if (!emailEsistente)
        {
            Console.Write("Inserisci numero di telefono: ");
            var telefono = Console.ReadLine();
            string[] Array2 = { nome, cognome, email, telefono + "\n" };
            string Array3 = string.Join(separator, Array2);
            File.AppendAllText(folderPath, Array3);
            Console.WriteLine("\nContatto aggiunto correttamente");
        }
        break;
    case 2:
        bool emailTrovata = false;
        Console.Write("Inserisci email dell'utente da modificare: ");
        var emailSearch = Console.ReadLine();
        righe = File.ReadAllLines(folderPath).ToList();
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
                Console.WriteLine("\nContatto modificato correttamente");
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
                if (EliminazioneUtente.ToLower() == "s")
                {
                    righe.RemoveAt(i);
                    File.WriteAllLines(folderPath, righe);
                    Console.WriteLine("\nContatto eliminato correttamente");
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
        if (EliminazioneRubrica.ToLower() == "s")
        {
            Console.WriteLine("\nRubrica eliminata correttamente");
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
        var searchFileImport = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";
        Console.Write("Inserisci nome file da importare: ");
        var fileImport = searchFileImport + Console.ReadLine() + ".csv";


        righe = File.ReadAllLines(folderPath).ToList();
        var righeNuove = File.ReadAllLines(fileImport).ToList();

        for (int y = 0; y < righeNuove.Count; y++)
        {
            var colonne = righeNuove[y].Split(',');
            bool emailDuplicata = false;

            for (int i = 0; i < righe.Count; i++)
            {
                var colonne2 = righe[i].Split(',');


                if (colonne2[2].Trim() == colonne[2].Trim())
                {
                    emailDuplicata = true;
                    Console.WriteLine($"{colonne[2].Trim()} esiste già. Vuoi sostituirlo con {colonne2[0]},{colonne2[1]} ? (s/n)");
                    var rispostaUtente = Console.ReadLine();
                    if (rispostaUtente.ToLower() == "s")
                    {
                        righe.Remove(colonne[0] + colonne[1] + colonne[2] + colonne[3]);
                        righeNuove.Add(colonne[0] + colonne[1] + colonne[2] + colonne[3]);
                        File.WriteAllLines(folderPath, righeNuove);
                        break;
                    }
                    else if (rispostaUtente.ToLower() == "n")
                    {
                        Console.WriteLine("Okay.");
                        break;
                    }
                    else if (!emailDuplicata)
                    {
                        File.AppendAllLines(folderPath, righeNuove);
                        break;
                    }

                }
            }
        }
   
    break;
    case 7:

        Console.WriteLine("\n" + File.ReadAllText(folderPath)); 
        break;
}
    
    







