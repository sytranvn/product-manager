namespace NMLT {
    public class ConsoleHelper {
        public static string ReadLine(string note, string defaultValue = "") {
            Console.Write(note + ": ");
            if (defaultValue != "") {
                Console.Write("(" + defaultValue + ") ");
            }
            string v;

            v = (Console.ReadLine() ?? "").Trim();
            if (v != "") {
                return v;
            }
            if (defaultValue == "") {
                Console.Write(note + " is required. ");
                return ReadLine(note, defaultValue);
            }
            return defaultValue;
        }

        public static void ClearLines(int lines) {
            var origRow = Console.CursorTop;
            for (int i = 0; i < lines; i++) {
                Console.SetCursorPosition(0, origRow - i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public static void Confirm(string note, Action yFunc) {
            Console.Write(note);
            Console.Write(" [y]/n: ");
            var ans = Console.ReadLine();
            if (String.IsNullOrEmpty(ans) || ans.ToLower() == "y" || ans.ToLower() == "yes") {
                yFunc();
            }
        }

        public static ConsoleKey MenuSelect(string note, Dictionary<ConsoleKey, string> items, string backtext = "Back") {
            Console.WriteLine(note);
            foreach (var i in items) {
                if (i.Key >= ConsoleKey.D0 && i.Key <= ConsoleKey.D9)
                    Console.WriteLine("  " + (i.Key - ConsoleKey.D0) + ". " + i.Value);
                else 
                Console.WriteLine(("  " + i.Key.ToString()) + ". " + i.Value);
            }
            Console.Write("Esc. " + backtext);
            ConsoleKeyInfo k;
            do {
                k = Console.ReadKey(true);
            } while (k.Key != ConsoleKey.Escape && !items.ContainsKey(k.Key));
            ConsoleHelper.ClearLines(items.Count + 2);
            return k.Key;
        }


        private static void addColumn<T>(
            List<T> products, string header, Func<T, int, string> getter, ref List<string> rows, ref int maxLen, 
            int minWidth = 4
        ) {
            int nextMax = maxLen + minWidth;
            if (rows[1].Length < maxLen) {
                rows[1] += new string(' ', maxLen - rows[1].Length);
            }
            rows[1] +=  "| " + header + " ";
            if (nextMax < rows[1].Length) nextMax = rows[1].Length;
            for (int i=0; i < products.Count; i++) {
                // add space if previous column is shorter than maxLen
                if (rows[i+3].Length < maxLen) {
                    rows[i+3] += new string(' ', maxLen - rows[i+3].Length);
                }
                string value = getter(products[i], i);
                rows[i+3] += "| " + value + " ";
                if (nextMax < rows[i+3].Length) nextMax = rows[i+3].Length;
            }
            maxLen = nextMax;
            rows[0] = rows[2] = new string('-', maxLen);
        }
        public static void WriteTable<T>(List<T> items, Dictionary<string, Func<T, int, string>> colums)
        {
            List<string> rows = Enumerable.Repeat("", items.Count + 3).ToList();
            int maxLen = 0;
            addColumn(items, "#", (x, i) => (i+1).ToString(), ref rows, ref maxLen);
            foreach (var c in colums) {
                addColumn(items, c.Key, c.Value, ref rows, ref maxLen);
            }
            foreach (var row in rows) {
                Console.WriteLine(row + new string(' ', maxLen - row.Length) + "|");
            }
            Console.WriteLine(new string('-', maxLen + 1));
        }
    }
}
