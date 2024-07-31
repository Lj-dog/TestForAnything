namespace ConsoleApp_LINQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>() {
                {"asdf_1","110" },{"qwer1","5"},{"asdf_2","111"},{"qwer2","5"},
            };
            var dictlist = dict.Keys.Where(key => key.StartsWith("asdf_"));

            foreach (var key in dictlist)
            {
                Console.WriteLine(key);
            }
        }
    }
}