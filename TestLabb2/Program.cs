using TestLabb2;

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("==========================================");
        Console.WriteLine("|      Villkommen till Samers butik      |");
        Console.WriteLine("==========================================");
        Console.WriteLine(" English or Svenska");
        var Language_list = new List<string>()
        {
            "1-  English",
            "2-  Svenska ",
        };

        Console.WriteLine("Choose a number :");
        foreach (var språk in Language_list)
        {
            Console.WriteLine(språk);

        }

        string opt = Console.ReadLine();

        if (opt == "1")
        {
            Console.WriteLine("Sorry English Language does not work right now, please try again later !");
        }
        else if (opt == "2")
        {
            Console.WriteLine(" Snyggt ");
            Console.WriteLine("_______________________________");

        }
        Console.WriteLine(" Välj ett alternativ : ");
        Shop shp = new Shop();

        shp.MainMenu();


    }

}
