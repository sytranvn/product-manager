namespace NMLT
{
    public class Program
    {
        public static List<Product> products = new List<Product>();
        public static HashSet<string> categories = new HashSet<string>();

        public static ConsoleKey MainMenu() {
            Console.Clear();
            return ConsoleHelper.Menu("Select function", new Dictionary<ConsoleKey, string> {
            {ConsoleKey.D1, "Add new product"},
            {ConsoleKey.D2, "Add new category"},
            {ConsoleKey.D3, "Search product"},
            {ConsoleKey.D4, "Search category"}
            }, "Exit");
        }

        public static void NewProduct() {
            Console.Clear();
            Product p = ProductHelper.Input("Please enter new product information");
            if (products.Exists(x => x.Code == p.Code)) {
                Console.WriteLine("Product code " + p.Code + " is duplicated. Please check and enter product again.");
                ConsoleHelper.Confirm("Re-enter information?", () => NewProduct());
                return;
            }
            products.Add(p);
            Console.WriteLine("Product " + p.Name + " added. (Total: " + products.Count + ")");
            if (!categories.Contains(p.Category)) {
                categories.Add(p.Category);
                Console.WriteLine("New category is also added: "+ p.Category);
            }
            ConsoleHelper.Confirm("Do you want to add an other product? ", () => NewProduct());
        }


        public static void DeleteProduct() {

        }

        public static void NewCategory() {
            Console.Clear();
            string c = ConsoleHelper.ReadLine("Please enter new category: ");
            if (!categories.Contains(c)) {
                categories.Add(c);
                Console.WriteLine("Category " + c + " added. (Total: " + categories.Count + ")");
            }
            else {
                Console.WriteLine("Category " + c + "already exists. (Total: "+ categories.Count + ")");
            }
            ConsoleHelper.Confirm("Do you want to add an other category? ", () => NewCategory());
        }

        public static void ViewProducts(List<Product> products) {
            Console.Clear();
            ProductHelper.Table(products);
            Dictionary<ConsoleKey, string> items = new Dictionary<ConsoleKey, string> {
                {ConsoleKey.D1, "code"},
                {ConsoleKey.D2, "name"},
                {ConsoleKey.D3, "expiry date"},
                {ConsoleKey.D4, "company"},
                {ConsoleKey.D5, "year of manufacture"},
                {ConsoleKey.D6, "category"},
            };
            ConsoleKey k = ConsoleHelper.Menu("Search by: ", items);
            string searchString;
            if (k != ConsoleKey.Escape) {
                searchString = ConsoleHelper.ReadLine("Enter " + items[k] + ": ");
                ViewProducts(products.FindAll(x => ProductHelper.matchProduct(x, k-ConsoleKey.D0, searchString)));
            }
        }

        public static void Main() {
            products.Add(new Product("P1", "Product 1", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P2", "Product 2", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P3", "Product 3", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P4", "Product 4", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P5", "Product 5", "01/01/2023", "AMD", 2023, "Computer"));
            ConsoleKey function;
            do {
                function = MainMenu();
                switch (function)
                {
                    case ConsoleKey.D1:
                        NewProduct();
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D2:
                        NewCategory();
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D3:
                        ViewProducts(products);
                    break;
                    case ConsoleKey.D4:
                    // do4
                    break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Bye");
                        return;
                    default:
                    break;
                }
            } while (true); 
        }
    }
}
