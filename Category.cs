namespace NMLT
{
    public struct Category
    {
        public string Code;
        public string Name;
        
        public Category(string code, string name) {
            this.Code = code;
            this.Name = name;
        }
    }

    public class CategoryHelper
    {
        public static Category Input(string code, string name, string expiryDate, string company, int yearOfMan, string category)
        {
            Category p;
            p.Code = code;
            p.Name = name;
            return p;
        }

        public static void Print(Category p)
        {
            Console.WriteLine("Product:");
            Console.WriteLine("Name: " + p.Name);
            Console.WriteLine("Code: " + p.Code);
        }
    }
}