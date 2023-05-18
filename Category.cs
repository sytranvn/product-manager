namespace NMLT
{
    public struct Category
    {
        private static int lastId = 0;
        private static List<Category> categories = new List<Category>();       
        public string Name;
        public string ID;
        
        public Category(string name) {
            if (Has(name)) throw new Exception("Value exists.");
            lastId += 1;
            this.Name = name;
            this.ID = "C" + lastId;
            categories.Add(this);
        }

        public static ref List<Category> All() {
            return ref categories;
        }

        public static bool Has(string c) {
            return categories.Any(x => x.Name.ToLower().Equals(c.ToLower()));
        }

        public static Category Find(Func<Category, bool> predicate) {
            return categories.Where(predicate).First();
        }

        public static Category Add(string c) {
            return new Category(c);
        }

        public static int Count {
            get { return categories.Count; }
        }

        public static ref List<Category> Init() {
            if (categories.Count == 0) {
                Add("Mobile phone");
                Add("CPU");
                Add("GPU");
            }
            return ref categories;
        }
    }

    public class CategoryHelper
    {
        public static void Print(Category p)
        {
            Console.WriteLine("Category:");
            Console.WriteLine("Name: " + p.Name);
        }

        public static Category Edit(Category c)
        {
            Console.WriteLine("Update category");
            var categories = Category.All();
            var catIndex = categories.FindIndex(x => x.ID == c.ID);
            var category = categories[catIndex];
            bool duplicated;
            string newName;
            do {
                duplicated = false;
                newName = ConsoleHelper.ReadLine("Enter new name", c.Name);
                for (int i = 0; i < categories.Count; i++) {
                    if (i != catIndex && categories[i].Name.ToLower().Equals(newName.ToLower())) {
                        duplicated = true;
                        Console.Write("Category name exists. ");
                    }
                }
            }
            while (duplicated);
            category.Name = newName;
            categories[catIndex] = category;
            
            var products = Product.All();
            for (int i = 0; i < products.Count; i++) {
                if (products[i].Category.ID.Equals(category.ID)) {
                    // https://stackoverflow.com/a/414989 cannot update element property directly. Replace old element with updated one.
                    var pc = products[i];
                    pc.Category = category;
                    products[i] = pc;
                }
            }

            return category;
        }



        public static void Table(List<Category> categories) {
            var columns = new Dictionary<string, Func<Category, int, string>> {
                {"Name", (x, _) => x.Name}, 
            };
            ConsoleHelper.WriteTable(categories, columns);
        }

        internal static void Delete(Category c)
        {
            if (Product.All().Exists(p => p.Category.Equals(c))) {
                if (ConsoleHelper.Confirm("There are some products under this category. Do you really want to delete this category and products under it?")) {
                    Product.All().FindAll(p => p.Category.Equals(c)).ForEach(p => Product.Remove(p));
                } else {
                    return;
                }
            }
            Category.All().Remove(c);
        }
    }
}
