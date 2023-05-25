namespace ProductManager
{
    public struct Product
    {
        private static List<Product> products = new List<Product>();

        public string Code;
        public string Name;
        public DateTime ExpiryDate;
        public string Company;
        public int YearOfManufacture;
        public Category Category;

        public Product(string code, string name, string expiryDate, string company, int year, Category category)
        {
            this.Code = code;
            this.Name = name;
            DateTime.TryParse(expiryDate, out this.ExpiryDate);
            this.Company = company;
            this.YearOfManufacture = year;
            this.Category = category;
        }

        public static List<Product> Init()
        {
            /* initialize sample data */
            if (products.Count == 0)
            {
                Category.Init();
                var phone = Category.All()[0];
                var cpu = Category.All()[1];
                var gpu = Category.All()[2];
                products.AddRange(new List<Product>() {
                    new Product("R3", "Ryzen 3", "22/11/2025", "AMD", 2017, cpu),
                    new Product("R5", "Ryzen 5", "15/02/2033", "AMD", 2018, cpu),
                    new Product("R7", "Ryzen 7", "31/01/2027", "AMD", 2020, cpu),
                    new Product("RX580", "Radeon RX 580", "01/06/2033", "AMD", 2023, gpu),
                    new Product("I3", "Core i3", "02/12/2030", "Intel", 2012, cpu),
                    new Product("I5", "Core i5", "21/12/2031", "Intel", 2014, cpu),
                    new Product("I7", "Core i7", "01/01/2033", "Intel", 2018, cpu),
                    new Product("RTX3050", "GeForce RTX 3050", "10/10/2028", "NVDIA", 2020, gpu),
                    new Product("SS10", "Samsung S10", "01/01/2019", "Samsung", 2023, phone),
                });
            }
            return products;
        }

        public static ref List<Product> All()
        {
            return ref products;
        }

        public static void Remove(Product p)
        {
            products.Remove(p);
        }
    }

    public class ProductHelper
    {
        public static Product Input(string note)
        {
            Product p;
            Console.WriteLine(note);
            p.Code = ConsoleHelper.ReadLine("Code");
            p.Name = ConsoleHelper.ReadLine("Name");
            string expiryDate = "";
            string nextYear = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            while (!DateTime.TryParse(expiryDate, out p.ExpiryDate))
            {
                expiryDate = ConsoleHelper.ReadLine("Expiry date", nextYear);
            }
            p.Company = ConsoleHelper.ReadLine("Company");
            p.YearOfManufacture = int.Parse(ConsoleHelper.ReadLine("Year of manufacture", DateTime.Now.Year.ToString()));
            var category = ConsoleHelper.ReadLine("Category");
            if (Category.Has(category))
            {
                p.Category = Category.All().Find(x => x.Name.ToLower().Equals(category));
            }
            else
            {
                p.Category = Category.Add(category);
                Console.WriteLine("Category " + category + " is new. Added to category list.");
            }
            return p;
        }

        public static Product Edit(Product p)
        {
            Console.WriteLine("Updating product: " + p.Name);
            var productIndex = Product.All().FindIndex(x => x.Code == p.Code);
            var product = Product.All()[productIndex];
            product.Code = ConsoleHelper.ReadLine("Code", p.Code);
            product.Name = ConsoleHelper.ReadLine("Name", p.Name);
            string expiryDate = "";
            while (!DateTime.TryParse(expiryDate, out product.ExpiryDate))
            {
                expiryDate = ConsoleHelper.ReadLine("Expiry date", p.ExpiryDate.ToString("dd/MM/yyyy"));
            }
            product.Company = ConsoleHelper.ReadLine("Company", p.Company);
            product.YearOfManufacture = int.Parse(ConsoleHelper.ReadLine("Year of manufacture", DateTime.Now.Year.ToString()));
            var category = ConsoleHelper.ReadLine("Category", p.Category.Name);
            if (Category.Has(category))
            {
                product.Category = Category.All().Find(x => x.Name.ToLower().Equals(category));
            }
            else
            {
                product.Category = Category.Add(category);
                Console.WriteLine("Category " + category + " is new. Added to category list.");
            }
            Product.All()[productIndex] = product;
            return product;
        }

        public static void Print(Product p)
        {
            Console.WriteLine("Product:");
            Console.WriteLine("Name: " + p.Name);
            Console.WriteLine("Code: " + p.Code);
            Console.WriteLine("Expiry Date: " + p.ExpiryDate.ToString("dd/MM/yyyy"));
            Console.WriteLine("Company: " + p.Company);
            Console.WriteLine("Year of Manufacture: " + p.YearOfManufacture);
            Console.WriteLine("Category: " + p.Category);
        }


        public static void Table(List<Product> products)
        {
            var columns = new Dictionary<string, Func<Product, int, string>> {
                {"Code", (x, _) => x.Code},
                {"Name", (x, _) => x.Name},
                {"Expiry date", (x, _) => x.ExpiryDate.ToString("dd/MM/yyyy")},
                {"Company", (x, _) => x.Company},
                {"YOM", (x, _) => x.YearOfManufacture.ToString()},
                {"Category", (x, _) => x.Category.Name}
            };
            ConsoleHelper.WriteTable(products, columns);
        }

        internal static bool matchProduct(Product x, int searchOption, string searchString)
        {
            switch (searchOption)
            {
                case 1:
                    return x.Code.ToLower().Contains(searchString.ToLower());
                case 2:
                    return x.Name.ToLower().Contains(searchString.ToLower());
                case 3:
                    DateTime searchDate;
                    DateTime.TryParse(searchString, out searchDate);
                    return x.ExpiryDate.Equals(searchDate);
                case 4:
                    return x.Company.ToLower().Contains(searchString.ToLower());
                case 5:
                    int yom = int.MinValue;
                    return x.YearOfManufacture == yom;
                case 6:
                    return x.Category.Name.ToLower().Equals(searchString.ToLower());
                default:
                    break;
            }
            return false;
        }

        internal static void Delete(Product p)
        {
            Product.All().Remove(p);
        }
    }
}
