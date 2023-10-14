namespace TestLabb2;

public class Shop
{
    private List<Customer> customers;
    private Customer CurrentCustomer { get; set; }
    private List<Product> Products { get; set; }

    public Shop()
    {
        customers = new List<Customer>
            {
                new Customer("Knatte", "123"),
                new Customer("Fnatte", "321"),
                new Customer("Tjatte", "213"),
                

            };
        Products = new List<Product>
            {
                new Product {Id = "1",Name = "           Korv             ",Price = 10.00 },
                new Product {Id = "2",Name = "           Ägg              ",Price = 26.00 },
                new Product {Id = "3",Name = "           Coca Cola        ",Price = 18.00 },
                new Product {Id = "4",Name = "           Handtvål         ",Price = 15.00 },
                new Product {Id = "5",Name = "           Pommes           ",Price = 22.00 },
                new Product {Id = "6",Name = "           Kex              ",Price = 8.00  },

            };
    }

    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("1-  Logga in");
            Console.WriteLine("2-  Skapa ny kund");
            Console.WriteLine("3-  Lämna Butiken");
            Console.Write("Välj ett alternativ : ");

            string opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    return;
                    break;
                default:
                    Console.WriteLine(" Du valde inte något av de tre alternativen !! Försök igen ");
                    break;
            }
        }
        Console.WriteLine("___________________________________");

    }


    private void Login()
    {
        Console.Write(" Skriv ditt användarnamn : ");
        string name = Console.ReadLine();
        Console.Write("     Skriv ditt lösenord : ");
        string password = Console.ReadLine();

        
        Customer customer = customers.Find(cust => cust.Name == name);

        if (customer == null)
        {
            Console.WriteLine(" Vi hittar dig inte i vårt system. Skapa ett nytt konto.");
            
            
                Register(); 
        }
        else
        {
            if (customer.CheckPassword(password))
            {
                Console.WriteLine("Hej " + customer.Name);
                Console.WriteLine("_________________________________");
                CurrentCustomer = customer;
                ShopMenu();
            }
            else
            {
                Console.WriteLine("Fel lösenord, Försök igen!!!");
            }
        }
    }

    private void Register()
    {
        Console.Write(" Skriv ditt användarnamn : ");
        string name = Console.ReadLine();
        Console.Write("     Skriv ditt lösenord : ");
        string password = Console.ReadLine();

        Customer newCustomer = new Customer(name, password);
        customers.Add(newCustomer);
        Console.WriteLine(" Nu har du ditt eget konto !!");
    }

    private void ShopMenu()
    {
        while (true)
        {

            var shop_list = new List<string>()
            {   "1- Handla",
                "2- Kundvagnen ",
                "3- Ta bort en produkt",
                "4- Kassan och Betalningen",
                "5- Logga ut "
            };

            
            Console.WriteLine("Välj ett alternativ :\n 1- Handla  \n 2- Kundvagn \n 3- Ta bort en produkt \n 4- Kassan och Betalningen\n 5- Logga ut  ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Handla();
                    break;
                case "2":
                    ViewCart();
                    break;
                case "3":
                    RemoveFromCart();
                    break;
                case "4":
                    Checkout();
                    break;
                case "5":
                    CurrentCustomer = null;
                    return;
                default:
                    Console.WriteLine(" Du valde inte något av de Fem alternativen !! Försök igen. ");
                    break;
            }
        }
    }

    private void Handla()
    {
        Console.WriteLine("\n Handla:");
        Console.WriteLine("===========================================");
        Console.WriteLine("    ID    |   Product     |     Price ");
        Console.WriteLine("===========================================");
        foreach (var product in Products)
        {
            Console.WriteLine(product);
        }

        Console.Write("Skriv produkts Id som du vill lägga till i kundvagnen: ");
        string productId = Console.ReadLine();
        Product selectedProduct = Products.Find(p => p.Id.Equals(productId, StringComparison.OrdinalIgnoreCase));

        if (selectedProduct != null)
        {
            CurrentCustomer.AddToCart(selectedProduct);
            Console.WriteLine($"{selectedProduct.Id} lägg till i kundvagnen.");
        }
        else
        {
            Console.WriteLine("====================");
        }
    }

    private void ViewCart()
    {
        Console.WriteLine("\n Kundvagnen:");
        List<string> productsName = new List<string>();
        Console.WriteLine("==================================================================");
        Console.WriteLine("   Id     |      Product     |   Amount   | Price  | Total ");
        Console.WriteLine("==================================================================");
        foreach (var product in CurrentCustomer.Cart)
        {
            if (productsName.Contains(product.Name))
                continue;
            int stycken = CurrentCustomer.Cart.Count(p => p.Id == product.Id);
            double totalPris = stycken * product.Price;

            Console.WriteLine($" {product.Id}    {product.Name}{stycken} Stycken  {product.Price} Kr   total = {totalPris} Kr");
            productsName.Add(product.Name);
        }

        double totalCartPris = CurrentCustomer.CartTotal();
        Console.WriteLine($"Totalt: {totalCartPris} Kr");
    }

    private void RemoveFromCart()
    {
        Console.Write(" Vilken produkt vill du ta bort?? skriv produkts Id : ");
        string productId = Console.ReadLine();
        Product selectedProduct = Products.Find(p => p.Id.Equals(productId, StringComparison.OrdinalIgnoreCase));

        if (selectedProduct != null)
        {
            CurrentCustomer.RemoveFromCart(selectedProduct);
            Console.WriteLine(selectedProduct.Id + " Ta bort från kundvagnen ");
        }
        else
        {
            Console.WriteLine("Vi kunnde inte hitta produkten");
        }
    }
    private void Checkout()
    {
        Console.WriteLine("_______________________________________");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(" OBS!! Du kan inte betala kontanter, bara med kort eller swish !"); 
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Vänta tills totalt beräknas...");
        Thread.Sleep(4000);
        Console.ResetColor();
        ViewCart();
        Console.WriteLine(" ====== Ha det så bra ======");
        Console.WriteLine("==============================================");
        CurrentCustomer.Cart.Clear();
    }
}