string filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\File.csv";
string copyPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";

Console.WriteLine("1 - Aggiungi contatto");
Console.WriteLine("2 - Modifica contatto");
Console.WriteLine("3 - Elimina contatto");
Console.WriteLine("4 - Elimina rubrica");
Console.WriteLine("5 - Esporta rubrica");
Console.WriteLine("6 - Importa rubrica");
Console.WriteLine("7 - Visualizza contatti\n");
string ?sceltaString = Console.ReadLine();
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
        //string[] array = { "Nome", "Cognome", "Email", "Telefono\n" };
        //string separator = ", ";
        //string Array = string.Join(separator, array);
        bool fileExists = File.Exists(filePath);
        if (!fileExists)
        {
            File.AppendAllText(filePath, "Nome, Cognome, Email, Telefono\n");
        }
        Console.Write("Inserisci nome: ");
        string? nome = Console.ReadLine();
        Console.Write("Inserisci cognome: ");
        string? cognome = Console.ReadLine();
        Console.Write("Inserisci email: ");
        string? email = Console.ReadLine();
        var letturaFile = File.ReadAllLines(filePath).ToList();
        bool emailEsistente = false;
        for (int i = 0; i < letturaFile.Count; i++)

        {
            var righe = letturaFile[i].Split(',');


            if (righe[2].Contains(email))
            {
                Console.WriteLine("\nContatto già esistente");
                emailEsistente = true;
                break;
            }
        }
        if (!emailEsistente)
        {
            Console.Write("Inserisci numero di telefono: ");
            string? telefonoString = Console.ReadLine();
            int telefonoInt = Convert.ToInt32(telefonoString);

            var utente = new Rubrica() { Nome = nome, Cognome = cognome, Email = email, Telefono = telefonoInt };
            string contatto = utente.Nome + ", " + utente.Cognome + ", " + utente.Email + ", " + utente.Telefono + "\n";
            File.AppendAllText(filePath, contatto);
            Console.WriteLine("\nContatto aggiunto correttamente");
        }
        break;
    case 2:
        bool emailTrovata = false;
        Console.Write("Inserisci email dell'utente da modificare: ");
        var emailSearch = Console.ReadLine();
        letturaFile = File.ReadAllLines(filePath).ToList();
        for (int i = 0; i < letturaFile.Count; i++)
        {

            string?[] colonne = letturaFile[i].Split(',');

            if (colonne[2].Trim() != emailSearch)
            {
                emailTrovata = true;
                Console.Write("Inserisci nuovo nome: ");
                string? nuovoNome = Console.ReadLine();
                Console.Write("Inserisci nuovo cognome: ");
                string? nuovoCognome = Console.ReadLine();
                Console.Write("Inserisci nuova email: ");
                string? nuovaEmail = Console.ReadLine();
                Console.Write("Inserisci nuovo numero di telefono: ");
                string? nuovoTelefono = Console.ReadLine();

                colonne[0] = nuovoNome;
                colonne[1] = nuovoCognome;
                colonne[2] = nuovaEmail;
                colonne[3] = nuovoTelefono;

                letturaFile[i] = string.Join(", ", colonne);
                File.WriteAllLines(filePath, letturaFile);
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
        letturaFile = File.ReadAllLines(filePath).ToList();
        for (int i = 0; i < letturaFile.Count; i++)
        {

            var colonne = letturaFile[i].Split(',');

            if (colonne[2].Trim() == emailSearch)
            {
                emailTrovata = true;
                Console.WriteLine($"""Sei sicuro di voler eliminare l'utente "{emailSearch}"? (s/n)""");
                string? EliminazioneUtente = Console.ReadLine();
                if (EliminazioneUtente.ToLower() == "s")
                {
                    letturaFile.RemoveAt(i);
                    File.WriteAllLines(filePath, letturaFile);
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
        string? EliminazioneRubrica = Console.ReadLine();
        if (EliminazioneRubrica.Equals("s", StringComparison.CurrentCultureIgnoreCase))
        {
            Console.WriteLine("\nRubrica eliminata correttamente");
            File.Delete(filePath);

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
            File.Copy(filePath, copyPath + fileCopy + ".csv");
        }
        break;
    case 6:
        var searchFileImport = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\";
        Console.Write("Inserisci nome file da importare: ");
        var fileImport = searchFileImport + Console.ReadLine() + ".csv";


        letturaFile = File.ReadAllLines(filePath).ToList();
        var righeNuove = File.ReadAllLines(fileImport).ToList();

        for (int y = 0; y < righeNuove.Count; y++)
        {
            var colonne = righeNuove[y].Split(',');
            bool emailDuplicata = false;

            for (int i = 0; i < letturaFile.Count; i++)
            {
                var colonne2 = letturaFile[i].Split(',');


                if (colonne2[2].Trim() == colonne[2].Trim())
                {
                    emailDuplicata = true;
                    Console.WriteLine($"{colonne[2].Trim()} esiste già. Vuoi sostituirlo con {colonne2[0]},{colonne2[1]} ? (s/n)");
                    var rispostaUtente = Console.ReadLine();
                    if (rispostaUtente.ToLower() == "s")
                    {
                        letturaFile.Remove(colonne[0] + colonne[1] + colonne[2] + colonne[3]);
                        righeNuove.Add(colonne[0] + colonne[1] + colonne[2] + colonne[3]);
                        File.WriteAllLines(filePath, righeNuove);
                        break;
                    }
                    else if (rispostaUtente.ToLower() == "n")
                    {
                        Console.WriteLine("Okay.");
                        break;
                    }
                    else if (!emailDuplicata)
                    {
                        File.AppendAllLines(filePath, righeNuove);
                        break;
                    }

                }
            }
        }
   
    break;
    case 7:

        Console.WriteLine("\n" + File.ReadAllText(filePath)); 
        break;
}
    
    







