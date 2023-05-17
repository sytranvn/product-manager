namespace NMLT
{
    public class Program
    {
        public static List<Product> products = new List<Product>();
        public static HashSet<string> categories = new HashSet<string>();

        public static int MainMenu() {
            Console.Clear();
            Console.WriteLine("Select function");
            Console.WriteLine("1. Add new product");
            Console.WriteLine("2. Add new category");
            Console.WriteLine("3. Search product");
            Console.WriteLine("4. Search category");
            Console.WriteLine("-------------------");
            Console.WriteLine("100. Exit");
            int result;
            int.TryParse(Console.ReadLine(), out result);
            return result;
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
            Dictionary<int, string> functionMap = new Dictionary<int, string> {
                {1, "code"},
                {2, "name"},
                {3, "expiry date"},
                {4, "company"},
                {5, "year of manufacture"},
                {6, "category"}
            };
            foreach (var f in functionMap) {
                Console.WriteLine(f.Key.ToString() + ". Search by " + f.Value);
            }
            Console.WriteLine("100. Back");
            int searchOption = 0;
            while (!functionMap.ContainsKey(searchOption)) {
                int.TryParse(Console.ReadLine(), out searchOption);
                if (searchOption == 100) break;
                string searchString = ConsoleHelper.ReadLine("Enter " + functionMap[searchOption] + ": ");
                ViewProducts(products.FindAll(x => ProductHelper.matchProduct(x, searchOption, searchString)));
            }
        }

        public static void Main() {
            products.Add(new Product("P1", "Product 1", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P2", "Product 2", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P3", "Product 3", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P4", "Product 4", "01/01/2023", "AMD", 2023, "Computer"));
            products.Add(new Product("P5", "Product 5", "01/01/2023", "AMD", 2023, "Computer"));
            int function = 0;
            while (function != 100) {
                function = MainMenu();
                switch (function)
                {
                    case 1:
                        NewProduct();
                        Console.ReadLine();
                    break;
                    case 2:
                        NewCategory();
                        Console.ReadLine();
                    break;
                    case 3:
                        ViewProducts(products);
                    break;
                    case 4:
                    // do4
                    break;
                    default:
                    break;
                }
            } 
        }
    }
}