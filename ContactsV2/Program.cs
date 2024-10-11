var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\File.csv";

Console.WriteLine("1 - Crea rubrica e aggiungi contatto");
var scelta = Console.ReadLine();
while (scelta != "1")
{
    scelta = Console.ReadLine();
}
    switch (scelta)
    {
        case "1":
            string[] array = { "Nome", "Cognome", "Email", "Telefono" };
            string separator = ", ";
            string Array = string.Join(separator, array);
            {
                bool fileExists = File.Exists(folderPath);

                using (StreamWriter sw = new StreamWriter(folderPath, true))
                {
                    if (fileExists == false)
                    {
                        sw.WriteLine(Array);
                    }
                    Console.Write("Inserisci nome: ");
                    var nome = Console.ReadLine();
                    Console.Write("Inserisci cognome: ");
                    var cognome = Console.ReadLine();
                    Console.Write("Inserisci email: ");
                    var email = Console.ReadLine();
                    Console.Write("Inserisci telefono: ");
                    var telefono = Console.ReadLine();
                    string[] Array2 = { nome, cognome, email, telefono };
                    string Array3 = string.Join(separator, Array2);
                    sw.WriteLine(Array3);
                    break;
                }
            }
    }