namespace NMLT
{
    public struct Product
    {
        public string Code;
        public string Name;
        public DateTime ExpiryDate;
        public string Company;
        public int YearOfManufacture;
        public string Category;
        
        public Product(string code, string name, string expiryDate, string company, int year, string category) {
            this.Code = code;
            this.Name = name;
            DateTime.TryParse(expiryDate, out this.ExpiryDate);
            this.Company = company;
            this.YearOfManufacture = year;
            this.Category = category;
        }
    }

    public class ProductHelper
    {
        public static Product Input(string note)
        {
            Product p;
            Console.WriteLine(note);
            p.Code = ConsoleHelper.ReadLine("Code: ");
            p.Name = ConsoleHelper.ReadLine("Name: ");
            string expiryDate = "";
            string nextYear = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            while (!DateTime.TryParse(expiryDate, out p.ExpiryDate)) {
                expiryDate = ConsoleHelper.ReadLine("Expiry date: ", nextYear); 
            }
            p.Company = ConsoleHelper.ReadLine("Company: ");
            p.YearOfManufacture = int.Parse(ConsoleHelper.ReadLine("Year of manufacture: ", DateTime.Now.Year.ToString()));
            p.Category = ConsoleHelper.ReadLine("Category: ");
            return p;
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


        public static void AddColumn(List<Product> products, string header, Func<Product, string> getter, ref List<string> rows, ref int maxLen, int minWidth = 8) {
            int nextMax = maxLen + minWidth;
            if (rows[1].Length < maxLen) {
                rows[1] += new string(' ', maxLen - rows[1].Length);
            }
            rows[1] +=  "| " + header + " ";
            if (nextMax < rows[1].Length) nextMax = rows[1].Length;
            for (int i=0; i < products.Count; i++) {
                if (rows[i+3].Length < maxLen) {
                    rows[i+3] += new string(' ', maxLen - rows[i+3].Length);
                }
                string value = getter(products[i]);
                rows[i+3] += "| " + value + " ";
                if (nextMax < rows[i+3].Length) nextMax = rows[i+3].Length;
            }
            maxLen = nextMax;
            rows[0] = rows[2] = new string('-', maxLen);
        }
        public static void Table(List<Product> products)
        {
            List<string> rows = Enumerable.Repeat("", products.Count + 3).ToList();
            int maxLen = 0;
            AddColumn(products, "Code", x => x.Code, ref rows, ref maxLen);
            AddColumn(products, "Name", x => x.Name, ref rows, ref maxLen);
            AddColumn(products, "Expiry date", x => x.ExpiryDate.ToString("dd/MM/yyyy"), ref rows, ref maxLen);
            AddColumn(products, "Company", x => x.Company, ref rows, ref maxLen);
            AddColumn(products, "YOM", x => x.YearOfManufacture.ToString(), ref rows, ref maxLen);
            AddColumn(products, "Category", x => x.Category, ref rows, ref maxLen);
            foreach (var row in rows) {
                Console.WriteLine(row + "|");
            }
            Console.WriteLine(new string('-', maxLen + 1));
        }

        internal static bool matchProduct(Product x, int searchOption, string searchString)
        {
            switch (searchOption) {
                case 1:
                    return x.Code.ToLower().Contains(searchString.ToLower());
                case 2:
                    return x.Name.ToLower().Contains(searchString.ToLower());
                case 3:
                    // return x.ExpiryDate.
                    // TODO
                    return false;
                case 4:
                    return x.Company.ToLower().Contains(searchString.ToLower());
                case 5:
                    int yom;
                    if (int.TryParse(searchString, out yom)) {
                        return x.YearOfManufacture == yom;
                    }
                        return false;
                case 6:
                    return x.Category.ToLower() == searchString.ToLower();
                default:
                    break;
            }
            return false;
        }
    }
}