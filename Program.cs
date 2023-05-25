namespace ProductManager
{
    public class Program
    {
        private static List<Category> categories = Category.All();
        private static List<Product> products = Product.All();
        private static ConsoleKey MainMenu()
        {
            Console.Clear();
            return ConsoleHelper.MenuSelect(
                "Select function",
                new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "View all products"},
                    {ConsoleKey.D2, "View all categories"},
                    {ConsoleKey.D3, "Initialize sample data"},
                },
                "Exit"
            );
        }

        public static void Main()
        {
            ConsoleKey function;
            ConsoleHelper.Banner();
            do
            {
                function = MainMenu();
                switch (function)
                {
                    case ConsoleKey.D1:
                        viewProducts(ref products);
                        break;
                    case ConsoleKey.D2:
                        viewCategories(ref categories);
                        break;
                    case ConsoleKey.D3:
                        Product.Init();
                        Console.Write("Data imported.");
                        Console.ReadLine();
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

        private static void viewCategories(ref List<Category> categories)
        {
            Console.Clear();
            CategoryHelper.Table(categories);
            var items = categories.Count == 0 ?
                new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "Add new category"}
                }
                : new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "Add new category"},
                    {ConsoleKey.D2, "Search for categories"},
                    {ConsoleKey.D3, "Edit a category"},
                    {ConsoleKey.D4, "Delete a category"},
                };
            var k = ConsoleHelper.MenuSelect("Select option:", items);
            switch (k)
            {
                case ConsoleKey.D1:
                    newCategory();
                    break;
                case ConsoleKey.D2:
                    searchCategories(categories);
                    break;
                case ConsoleKey.D3:
                    editCategory(categories);
                    break;
                case ConsoleKey.D4:
                    deleteCategory(categories);
                    break;
                default:
                    break;
            }
        }

        private static void searchCategories(List<Category> categories)
        {
            var searchString = ConsoleHelper.ReadLine("Enter name");
            var results = categories.FindAll(x => x.Name.ToLower().Contains(searchString.ToLower()));
            viewCategories(ref results);
        }

        private static void editCategory(List<Category> categories)
        {
            var c = ConsoleHelper.TableSelect(categories, "Select product # to update");
            CategoryHelper.Edit(c);
            Console.Write("Category " + c.Name + " has been updated.");
            Console.ReadLine();
        }

        private static void deleteCategory(List<Category> categories)
        {
            var c = ConsoleHelper.TableSelect(categories, "Select product # to delete");
            CategoryHelper.Delete(c);
            Console.Write("Category " + c.Name + " has been deleted.");
            Console.ReadLine();
        }

        private static void viewProducts(ref List<Product> products)
        {
            Console.Clear();
            ProductHelper.Table(products);
            Dictionary<ConsoleKey, string> items = products.Count == 0 ?
                new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "Add new product"},
                } : new Dictionary<ConsoleKey, string> {
                    {ConsoleKey.D1, "Add new product"},
                    {ConsoleKey.D2, "Search for products"},
                    {ConsoleKey.D3, "Edit a product"},
                    {ConsoleKey.D4, "Delete a product"},
                };
            ConsoleKey k = ConsoleHelper.MenuSelect("", items, "Back");

            switch (k)
            {
                case ConsoleKey.D1:
                    newProduct();
                    break;
                case ConsoleKey.D2:
                    searchProducts(products);
                    break;
                case ConsoleKey.D3:
                    editProduct(products);
                    break;
                case ConsoleKey.D4:
                    deleteProduct(products);
                    break;
                default:
                    break;
            }
        }
        private static void searchProducts(List<Product> products)
        {
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
            if (k != ConsoleKey.Escape)
            {
                searchString = ConsoleHelper.ReadLine("Enter " + items[k]);
                var products2 = products.FindAll(x => ProductHelper.matchProduct(
                            x,
                            k - ConsoleKey.D0,
                            searchString)
                        );
                viewProducts(ref products2);
            }
        }

        private static void newProduct()
        {
            Console.Clear();
            Product p = ProductHelper.Input("Please enter new product information");
            if (products.Exists(x => x.Code == p.Code))
            {
                Console.WriteLine("Product code " + p.Code + " is duplicated. Please check and enter product again.");
                if (ConsoleHelper.Confirm("Re-enter information?"))
                {
                    newProduct();
                    return;
                }
            }
            products.Add(p);
            Console.WriteLine("Product " + p.Name + " added. (Total: " + products.Count + ")");
            if (ConsoleHelper.Confirm("Do you want to add an other product? "))
            {
                newProduct();
            }
        }


        private static void editProduct(List<Product> products)
        {
            var p = ConsoleHelper.TableSelect(products, "Select product # to update");
            ProductHelper.Edit(p);
            Console.Write("Product " + p.Name + " has been updated.");
            Console.ReadLine();
        }

        private static void deleteProduct(List<Product> products)
        {
            var p = ConsoleHelper.TableSelect(products, "Select product # to delete");
            ProductHelper.Delete(p);
            Console.Write("Product " + p.Name + " has been deleted.");
            Console.ReadLine();
        }

        private static void newCategory()
        {
            Console.Clear();
            string c = ConsoleHelper.ReadLine("Please enter new category");
            if (!Category.Has(c))
            {
                Category.Add(c);
                Console.WriteLine("Category " + c + " added. (Total: " + Category.Count + ")");
            }
            else
            {
                Console.WriteLine("Category " + c + " already exists. (Total: " + Category.Count + ")");
            }
            if (ConsoleHelper.Confirm("Do you want to add an other category? "))
            {
                newCategory();
            }
        }

        private static string banner = @"
         ______               _                            
        (_____ \             | |              _            
         _____) )___ ___   __| |_   _  ____ _| |_          
        |  ____/ ___) _ \ / _  | | | |/ ___|_   _)         
        | |   | |  | |_| ( (_| | |_| ( (___  | |_          
        |_|   |_|   \___/ \____|____/ \____)  \__)         
                                                           
                                                           
                                                           
     ____  _____ ____  _____  ____ _____  ____ _____ _____ 
    |    \(____ |  _ \(____ |/ _  | ___ |/ ___|_____|_____)
    | | | / ___ | | | / ___ ( (_| | ____| |                
    |_|_|_\_____|_| |_\_____|\___ |_____)_|                
                            (_____|
    ";

    }

}
