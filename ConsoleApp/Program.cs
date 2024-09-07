using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

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

    public enum Level
    {
        LEVELONE,
        LEVELTWO,
        LEVELTHREE,
    }

    public class School
    {
        //[JsonInclude]
        //[JsonPropertyOrder(-1)]
        //[JsonPropertyName("SchoolLevel")]
        //[JsonConverter(typeof(JsonStringEnumConverter))]
        [XmlAttribute("SchoolLevel")]
        public Level level;

        //[JsonPropertyOrder(1)]
        [XmlElement(Order = 2)]
        public List<Grade> Grades { get; set; }

        //[JsonPropertyOrder(1)]
        [XmlElement("Name", Order = 1)]
        public string SchoolName { get; set; }

        //[JsonPropertyOrder(0)]
        [XmlElement(Order = 0)]
        public int SchoolAge { get; set; }

        //public Dictionary<string, int> GradePeopleNum { get; set; }
    }

    public class Grade
    {
        public int ClassNum { get; set; }

        public string GradeName { get; set; }

        public List<Class> ClassList { get; set; }
    }

    public class Class
    {
        public int Students { get; set; }
    }

    #endregion JSON XAML

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");\

            #region 1

            //(1)
            //Print(0, num3: 5);

            #endregion 1

            //(2)

            #region 3

            //(3)异步方法
            //int a;
            //Task.Run(() => {
            //    a = await Sum(1, 4);
            //});
            //int a = await Sum(1, 4);
            //Console.WriteLine(a);

            #endregion 3

            #region 4

            //(4) ushrot shrot转换(不丢失任何数据)
            //short sh = -2;
            //ushort ush = (ushort)sh;
            //Console.WriteLine($"{(short)ush}");

            #endregion 4

            #region 5

            //(5) 显示调用拥有不定参数和默认参数的函数
            //TestParams(bools: [true, false]);
            //TestParams(2, bools: [false, true]);

            #endregion 5

            #region 6 JSON XML序列化/反序列化

            //(6) JSON XML序列化/反序列化

            //School school = new School()
            //{
            //    level = Level.LEVELONE,
            //    SchoolAge = 10,
            //    SchoolName = "NAN",
            //    //GradePeopleNum = new Dictionary<string, int>()
            //    //{
            //    //    {"Freshman",8 },
            //    //    {"Sophomore",4 },
            //    //    {"Junior",8 },
            //    //    {"Senior",4 },
            //    //},
            //    Grades = new List<Grade>()
            //    {
            //        new Grade(){GradeName="Freshman",ClassNum=1,ClassList= new List<Class>(){
            //         new Class(){Students = 1},
            //         new Class(){Students = 2},
            //         new Class(){Students = 3},
            //        } },
            //        new Grade(){GradeName="Sophomore",ClassNum=2,ClassList= new List<Class>(){
            //         new Class(){Students = 5},
            //         new Class(){Students = 5},
            //         new Class(){Students = 5},
            //        }},
            //        new Grade(){GradeName="Junior",ClassNum=3,ClassList= new List<Class>(){
            //         new Class(){Students = 6},
            //         new Class(){Students = 6},
            //         new Class(){Students = 6},
            //        }},
            //        new Grade(){GradeName="Senior",ClassNum=4,ClassList= new List<Class>(){
            //         new Class(){Students = 9},
            //         new Class(){Students = 9},
            //         new Class(){Students = 9},
            //        }},
            //    }
            //};

            #region JSON

            //JSON

            //var options = new JsonSerializerOptions { WriteIndented = true };
            //同步方法
            //string jsonstr = JsonSerializer.Serialize(school);
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
            //string fileNameAsync = "SchoolAsync.json";
            //string jsonString = File.ReadAllText(fileNameAsync);
            //School? schoolDeserialize = JsonSerializer.Deserialize<School>(jsonstr);
            //if (schoolDeserialize != null)
            //{
            //    Console.WriteLine($"Level:{schoolDeserialize.level}");
            //    Console.WriteLine($"Age:{schoolDeserialize.SchoolAge}");
            //    Console.WriteLine($"Name:{schoolDeserialize.SchoolName}");
            //}

            #endregion JSON

            #region XML

            ////XML 命名空间
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("books", "http");

            ////XML序列化
            ////字符流
            //XmlSerializer xml = new XmlSerializer(typeof(School));
            //using StringWriter sw = new StringWriter();
            //xml.Serialize(sw, school, ns);
            //Console.WriteLine(sw.ToString());

            ////写入文件流
            //string path = "SchoolXML.xml";
            //using FileStream fileStream = new(path, FileMode.OpenOrCreate);
            //xml.Serialize(fileStream, school, ns);

            #endregion XML

            #endregion 6 JSON XML序列化/反序列化

            #region 7

            //7 DateTime 转符合文件格式的字符串
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            #endregion 7

            #region 8

            //LINQ 惰性处理特性
            //Dictionary<string, string> StrToREG = new Dictionary<string, string>();
            //StrToREG.Add("Inital", "1");
            //StrToREG.Add("InitalCompleted", "2");
            //StrToREG.Add("Reset", "3");
            //StrToREG.Add("ResetCompleted", "3");
            //StrToREG.Add("TCPOnline_1", "4");
            //StrToREG.Add("TCPOnline_2", "5");
            //StrToREG.Add("DirType_1", "6");
            //StrToREG.Add("DirType_2", "7");
            //StrToREG.Add("SendID", "8");
            //StrToREG.Add("SendType", "9");
            //StrToREG.Add("StartIN", "2");
            //StrToREG.Add("Arrive_1", "2");
            //StrToREG.Add("Arrive_2", "3");
            //StrToREG.Add("VirID_1", "3");
            //StrToREG.Add("VirID_2", "5");

            //StrToREG.Add("OutDr_1", "5");
            //StrToREG.Add("OutDr_2", "5");

            //StrToREG.Add("Pan_1", "3");
            //StrToREG.Add("Pan_2", "2");
            //var DirllList = StrToREG.Keys.Where(key => key.StartsWith("DirType_"));
            //foreach (var item in DirllList)
            //{
            //    Console.WriteLine($"{item}");
            //}

            #endregion 8

            #region 9

            //Git 临时分支提交

            #endregion 9
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