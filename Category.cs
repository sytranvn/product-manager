namespace NMLT
{
    public struct Category
    {
        private static int lastId = 0;
        private static HashSet<Category> categories = new HashSet<Category>();       
        public string Name;
        public string ID;
        
        public Category(string name) {
            if (Has(name)) throw new Exception("Value exists.");
            lastId += 1;
            this.Name = name;
            this.ID = "C" + lastId;
            categories.Add(this);
        }

        public static List<Category> All() {
            return categories.ToList();
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

        public static HashSet<Category> Init() {
            if (categories.Count == 0) {
                Add("Phone");
                Add("Computer");
            }
            return categories;
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
            var catIndex = Category.All().FindIndex(x => x.ID == c.ID);
            var category = Category.All()[catIndex];
            category.Name = ConsoleHelper.ReadLine("Name", c.Name);
            Category.All()[catIndex] = category;
            return category;
        }



        public static void Table(List<Category> categories) {
            var columns = new Dictionary<string, Func<Category, int, string>> {
                {"Name", (x, _) => x.Name}, 
            };
            ConsoleHelper.WriteTable(categories, columns);
        }
       
    }
}
