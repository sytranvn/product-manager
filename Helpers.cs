namespace NMLT {
    public class ConsoleHelper {
        public static string ReadLine(string note, string defaultValue = "") {
            Console.Write(note);
            if (defaultValue != "") {
                Console.Write("(" + defaultValue + ") ");
            }
            string v;

            v = Console.ReadLine() ?? "";
            if (v != "") {
                return v;
            }
            if (defaultValue == "") {
                return ReadLine(note, defaultValue);
            }
            return defaultValue;
        }

        public static void Confirm(string note, Action yFunc) {
            Console.Write(note);
            Console.Write(" [y]/n: ");
            var ans = Console.ReadLine();
            if (String.IsNullOrEmpty(ans) || ans.ToLower() == "y" || ans.ToLower() == "yes") {
                yFunc();
            }
        }

        public static ConsoleKey Menu(string note, Dictionary<ConsoleKey, string> items, string backtext = "Back") {
            Console.WriteLine(note);
            foreach (var i in items) {
                if (i.Key >= ConsoleKey.D0 && i.Key <= ConsoleKey.D9)
                    Console.WriteLine("  " + (i.Key - ConsoleKey.D0) + ". " + i.Value);
                else 
                Console.WriteLine(("  " + i.Key.ToString()) + ". " + i.Value);
            }
            Console.WriteLine("Esc. " + backtext);
            ConsoleKeyInfo k;
            do {
                k = Console.ReadKey(true);
            } while (k.Key != ConsoleKey.Escape && !items.ContainsKey(k.Key));
            return k.Key;
        }
    }
}
