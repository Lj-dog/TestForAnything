namespace ConsoleApp
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");\

            //(1)
            //Print(0, num3: 5);

            //(2)

            //(3)异步方法
            //int a;
            //Task.Run(() => {
            //    a = await Sum(1, 4);
            //});
            //int a = await Sum(1, 4);
            //Console.WriteLine(a);
        }

        //(1)函数形参跳过默认参数给定
        static void Print(int num1, int num2 = 2, int num3 = 3)
        {
            Console.WriteLine($"num1:{num1},num2:{num2},num3:{num3}");
        }

        //(3)异步方法
        public static async Task<int> Sum(int a, int b)
         {
            await Task.Delay(1);
            return a + b;
        }
    }

    //(2) 委托解耦调用

    class ClassPrint
    {
        public  delegate void Print(string str);
    }
}
