namespace NMLT
{
    public class Program
    {
        
        private static List<Product> products = new List<Product>();
        private static ConsoleKey MainMenu() {
            Console.Clear();
            return ConsoleHelper.MenuSelect(
                "Select function",
                new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "View all products"},
                    {ConsoleKey.D2, "View all categories"}
                },
                "Exit"
            );
        }

        public static void Main() {
            Category.Add("Phone");
            Category.Add("Computer");
            products = new List<Product> {
                new Product("R3", "Ryzen 3", "01/01/2033", "AMD", 2023, Category.All()[1]),
                new Product("R4", "Ryzen 4", "01/01/2033", "AMD", 2023, Category.All()[1]),
                new Product("R5", "Ryzen 5", "01/01/2033", "AMD", 2023, Category.All()[1]),
                new Product("I3", "Core I 3", "01/01/2033", "Intel", 2023, Category.All()[1]),
                new Product("I4", "Core I 4", "01/01/2033", "Intel", 2023, Category.All()[1]),
                new Product("I5", "Core I 5", "01/01/2033", "Intel", 2023, Category.All()[1]),
                new Product("SS10", "Samsung S10", "01/01/2033", "Samsung", 2023, Category.All()[0]),
            };

            ConsoleKey function;
            do {
                function = MainMenu();
                switch (function)
                {
                    case ConsoleKey.D1:
                        viewProducts(products);
                    break;
                    case ConsoleKey.D2:
                        viewCategories(Category.All());
                    break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("See you later....!");
                        Thread.Sleep(1000);
                        return;
                    default:
                    break;
                }
            } while (true); 
        }

        private static void viewCategories(List<Category> categories)
        {
            Console.Clear();
            CategoryHelper.Table(categories);
            var items = new Dictionary<ConsoleKey, string> {
                {ConsoleKey.D1, "Add new category"},
                {ConsoleKey.D2, "Search for categories"},
                {ConsoleKey.D3, "Edit a category"},
                {ConsoleKey.D4, "Delete a category"},
            };
            var k = ConsoleHelper.MenuSelect("Select option:", items);
            switch (k) {
                case ConsoleKey.D1:
                    newCategory();
                    break;
                case ConsoleKey.D2:
                    searchCategories(categories);
                    break;
                case ConsoleKey.D3:
                    editCategory();
                    break;
                case ConsoleKey.D4:
                    deleteCategory();
                    break;
                default:
                    break;
            }
        }

        private static void searchCategories(List<Category> categories) {
            var searchString = ConsoleHelper.ReadLine("Enter name");
            viewCategories(categories.FindAll(x => x.Name.ToLower().Contains(searchString.ToLower())));
        }
        
        private static void editCategory() {
            Console.WriteLine("Not Implemented");
            Console.ReadLine();
        }

        private static void deleteCategory() {
            Console.WriteLine("Not Implemented");
            Console.ReadLine();
        }

        private static void viewProducts(List<Product> products) {
            Console.Clear();
            ProductHelper.Table(products);
            Dictionary<ConsoleKey, string> items = new Dictionary<ConsoleKey, string> {
                {ConsoleKey.D1, "Add new product"},
                {ConsoleKey.D2, "Search for products"},
                {ConsoleKey.D3, "Edit a product"},
                {ConsoleKey.D4, "Delete a product"},
            };
            ConsoleKey k = ConsoleHelper.MenuSelect("", items, "Back");

            switch (k) {
                case ConsoleKey.D1:
                    newProduct();
                    break;
                case ConsoleKey.D2:
                    searchProducts(products);
                    break;
                case ConsoleKey.D3:
                    editProduct();
                    break;
                case ConsoleKey.D4:
                    deleteProduct();
                    break;
                default:
                    break;
            }
        }
        private static void searchProducts(List<Product> products) {
            var items = new Dictionary<ConsoleKey, string> {
                {ConsoleKey.D1, "code"},
                {ConsoleKey.D2, "name"},
                {ConsoleKey.D3, "expiry date"},
                {ConsoleKey.D4, "company"},
                {ConsoleKey.D5, "year of manufacture"},
                {ConsoleKey.D6, "category"},
            };
            ConsoleKey k = ConsoleHelper.MenuSelect("Search by: ", items);
            string searchString;
            if (k != ConsoleKey.Escape) {
                searchString = ConsoleHelper.ReadLine("Enter " + items[k]);
                viewProducts(products.FindAll(x => ProductHelper.matchProduct(x, k-ConsoleKey.D0, searchString)));
            }
        }

        private static void newProduct() {
            Console.Clear();
            Product p = ProductHelper.Input("Please enter new product information");
            if (products.Exists(x => x.Code == p.Code)) {
                Console.WriteLine("Product code " + p.Code + " is duplicated. Please check and enter product again.");
                ConsoleHelper.Confirm("Re-enter information?", () => newProduct());
                return;
            }
            products.Add(p);
            Console.WriteLine("Product " + p.Name + " added. (Total: " + products.Count + ")");
            ConsoleHelper.Confirm("Do you want to add an other product? ", () => newProduct());
        }


        private static void editProduct() {
            Console.WriteLine("Not Implemented");
            Console.ReadLine();
        }

        private static void deleteProduct() {
            Console.WriteLine("Not Implemented");
            Console.ReadLine();
        }

        private static void newCategory() {
            Console.Clear();
            string c = ConsoleHelper.ReadLine("Please enter new category");
            if (!Category.Has(c)) {
                Category.Add(c);
                Console.WriteLine("Category " + c + " added. (Total: " + Category.Count + ")");
            }
            else {
                Console.WriteLine("Category " + c + "already exists. (Total: "+ Category.Count + ")");
            }
            ConsoleHelper.Confirm("Do you want to add an other category? ", () => newCategory());
        }
    }
}
