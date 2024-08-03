using System.Text.Json;

namespace ConsoleApp
{
    //抽象类里使用虚方法

    #region 抽象类里使用虚方法

    internal abstract class People
    {
        public virtual void Age()
        {
            Console.WriteLine($"teenager");
        }

        public abstract void Name(string name);
    }

    internal class Student : People
    {
        public override void Name(string name)
        {
            Console.WriteLine($"I'm student {name}");
        }
    }

    internal class Teacher : People
    {
        public override void Age()
        {
            Console.WriteLine($"Adult");
        }

        public override void Name(string name)
        {
            Console.WriteLine($"I'm teacher {name}");
        }
    }

    #endregion 抽象类里使用虚方法

    #region JSON XAML

    internal enum Level
    {
        LEVELONE,
        LEVELTWO,
        LEVELTHREE,
    }

    internal class School
    {
        public Level level;
        public List<Grade> Grades { get; set; }

        public string SchoolName { get; set; }

        public int SchoolAge { get; set; }

        public Dictionary<string, int> GradePeopleNum { get; set; }
    }

    internal class Grade
    {
        public int ClassNum { get; set; }

        public string GradeName { get; set; }

        public List<Class> ClassList { get; set; }
    }

    internal class Class
    {
        public int Students { get; set; }
    }

    #endregion JSON XAML

    internal class Program
    {
        private static async Task Main(string[] args)
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

            //(4) ushrot shrot转换(不丢失任何数据)
            //short sh = -2;
            //ushort ush = (ushort)sh;
            //Console.WriteLine($"{(short)ush}");

            //(5) 显示调用拥有不定参数和默认参数的函数
            //TestParams(bools: [true, false]);
            //TestParams(2, bools: [false, true]);

            //(6) JSON XML序列化/反序列化

            School school = new School()
            {
                level = Level.LEVELONE,
                SchoolAge = 10,
                SchoolName = "NAN",
                GradePeopleNum = new Dictionary<string, int>()
                {
                    {"Freshman",8 },
                    {"Sophomore",4 },
                    {"Junior",8 },
                    {"Senior",4 },
                },
                Grades = new List<Grade>()
                {
                    new Grade(){GradeName="Freshman",ClassNum=1,ClassList= new List<Class>(){
                     new Class(){Students = 1},
                     new Class(){Students = 2},
                     new Class(){Students = 3},
                    } },
                    new Grade(){GradeName="Sophomore",ClassNum=2,ClassList= new List<Class>(){
                     new Class(){Students = 5},
                     new Class(){Students = 5},
                     new Class(){Students = 5},
                    }},
                    new Grade(){GradeName="Junior",ClassNum=3,ClassList= new List<Class>(){
                     new Class(){Students = 6},
                     new Class(){Students = 6},
                     new Class(){Students = 6},
                    }},
                    new Grade(){GradeName="Senior",ClassNum=4,ClassList= new List<Class>(){
                     new Class(){Students = 9},
                     new Class(){Students = 9},
                     new Class(){Students = 9},
                    }},
                }
            };
            //JSON

            var options = new JsonSerializerOptions { WriteIndented = true };
            //同步方法
            string jsonstr = JsonSerializer.Serialize(school);
            //string fileName = "School.json";
            //File.WriteAllText(fileName, jsonstr);
            //Console.WriteLine(File.ReadAllText(fileName));
            //异步方法
            //string fileNameAsync = "SchoolAsync.json";
            //FileStream createStream = File.Create(fileNameAsync);
            ////await using FileStream createStream = File.Create(fileNameAsync);
            //await JsonSerializer.SerializeAsync(createStream, school, options);
            ////await JsonSerializer.SerializeAsync(createStream, school);

            //createStream.Close();
            //Console.WriteLine(File.ReadAllText(fileNameAsync));

            //反序列化
            //同步方法
            //School? schoolDeserialize = JsonSerializer.Deserialize<School>(jsonstr);
            //if (schoolDeserialize != null)
            //{
            //    Console.WriteLine($"Level:{schoolDeserialize.level}");
            //    Console.WriteLine($"Age:{schoolDeserialize.SchoolAge}");
            //    Console.WriteLine($"Name:{schoolDeserialize.SchoolName}");
            //}
            //异步方法
            string fileNameAsync = "SchoolAsync.json";
            string jsonString = File.ReadAllText(fileNameAsync);
            School? schoolDeserialize = JsonSerializer.Deserialize<School>(jsonstr);
            if (schoolDeserialize != null)
            {
                Console.WriteLine($"Level:{schoolDeserialize.level}");
                Console.WriteLine($"Age:{schoolDeserialize.SchoolAge}");
                Console.WriteLine($"Name:{schoolDeserialize.SchoolName}");
            }
        }

        //(1)函数形参跳过默认参数给定
        private static void Print(int num1, int num2 = 2, int num3 = 3)
        {
            Console.WriteLine($"num1:{num1},num2:{num2},num3:{num3}");
        }

        //(3)异步方法
        public static async Task<int> Sum(int a, int b)
        {
            await Task.Delay(1);
            return a + b;
        }

        //(5) 显示调用拥有不定参数和默认参数的函数
        public static void TestParams(int a = 1, params bool[] bools)

        {
            Console.WriteLine($"{a}");
            foreach (var b in bools)
            {
                Console.WriteLine($"{b}");
            }
        }
    }

    //(2) 委托解耦调用

    internal class ClassPrint
    {
        public delegate void Print(string str);
    }
}