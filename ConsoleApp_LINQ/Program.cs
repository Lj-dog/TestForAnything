namespace ConsoleApp_LINQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region Dict.Where
            //Dictionary<string, string> dict = new Dictionary<string, string>() {
            //    {"asdf_1","110" },{"qwer1","5"},{"asdf_2","111"},{"qwer2","5"},
            //};
            //var dictlist = dict.Keys.Where(key => key.StartsWith("asdf_"));

            //foreach (var key in dictlist)
            //{
            //    Console.WriteLine(key);
            //}
            #endregion

            #region bools.Select

            bool[] data = { true, false, true, true }; // 示例数据
            int specificIndex = 1; // 特定的索引

            bool hasOnlyOneFalseAtSpecificIndex = data
                .Select((a, i) => new { Index = i, Value = a })
                .Count(item => item.Value == false && item.Index == specificIndex) == 1;
            
            Console.WriteLine(hasOnlyOneFalseAtSpecificIndex);
            #endregion
        }
    }
}