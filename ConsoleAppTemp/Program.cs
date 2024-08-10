using FreeSql.DataAnnotations;
using System;
using System.Reflection;

using FreeSql;

namespace ConsoleAppTemp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string output1 = string.Join(" ", TakeWhilePositive(new int[] { 2, 3, 4, 5, -1, 3, 4 }));
            Console.WriteLine(output1);
            // Output: 2 3 4 5
            string output2 = string.Join(" ", TakeWhilePositive(new int[] { 9, 8, 7 }));

            Console.WriteLine(output2);
            // Output: 9 8 7
        }

        private static IEnumerable<int> TakeWhilePositive(IEnumerable<int> numbers)
        {
            foreach (int n in numbers)
            {
                if (n > 0)
                {
                    yield return n;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}