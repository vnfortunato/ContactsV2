string[] array = { "Nome", "Cognome", "Età", "Telefono" };
string separator = ", ";
string Array = string.Join(separator, array);
{
    bool fileExists = File.Exists(@"C:\Users\Nick\Desktop\File.csv");

    using (StreamWriter sw = new StreamWriter(@"C:\Users\Nick\Desktop\File.csv", true))
    {
        if (fileExists == false)
        {
            sw.WriteLine(Array);
        }
        var nome = Console.ReadLine();
        var cognome = Console.ReadLine();
        var eta = Console.ReadLine();
        var telefono = Console.ReadLine();
        string[] Array2 = { nome, cognome, eta, telefono };
        string Array3 = string.Join(separator, Array2);
        sw.WriteLine(Array3);
    }
}