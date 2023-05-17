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
    }
}