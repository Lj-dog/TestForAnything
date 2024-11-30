using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
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

            #region 1 -函数形参跳过默认参数给定

            //(1)
            //Print(0, num3: 5);

            #endregion 1

            #region 2 -委托解耦调用

            //(2)

            #endregion

            #region 3 -异步方法

            //(3)异步方法
            //int a;
            //Task.Run(() => {
            //    a = await Sum(1, 4);
            //});
            //int a = await Sum(1, 4);
            //Console.WriteLine(a);

            #endregion 3

            #region 4 -ushrot shrot转换(不丢失任何数据)

            //(4) ushrot shrot转换(不丢失任何数据)
            //short sh = -2;
            //ushort ush = (ushort)sh;
            //Console.WriteLine($"{(short)ush}");

            #endregion 4

            #region 5 -显示调用拥有不定参数和默认参数的函数

            //(5) 显示调用拥有不定参数和默认参数的函数
            //TestParams(bools: [true, false]);
            //TestParams(2, bools: [false, true]);

            #endregion 5

            #region 6 JSON XML序列化/反序列化

            //(6) JSON XML序列化/反序列化

            School school = new School()
            {
                level = Level.LEVELTWO,
                SchoolAge = 20,
                SchoolName = "NANA",
                //GradePeopleNum = new Dictionary<string, int>()
                //{
                //    {"Freshman",8 },
                //    {"Sophomore",4 },
                //    {"Junior",8 },
                //    {"Senior",4 },
                //},
                Grades = new List<Grade>()
                {
                    new Grade()
                    {
                        GradeName = "Freshman",
                        ClassNum = 1,
                        ClassList = new List<Class>()
                        {
                            new Class() { Students = 1 },
                            new Class() { Students = 2 },
                            new Class() { Students = 3 },
                        },
                    },
                    new Grade()
                    {
                        GradeName = "Sophomore",
                        ClassNum = 2,
                        ClassList = new List<Class>()
                        {
                            new Class() { Students = 5 },
                            new Class() { Students = 5 },
                            new Class() { Students = 5 },
                        },
                    },
                    new Grade()
                    {
                        GradeName = "Junior",
                        ClassNum = 3,
                        ClassList = new List<Class>()
                        {
                            new Class() { Students = 6 },
                            new Class() { Students = 6 },
                            new Class() { Students = 6 },
                        },
                    },
                    new Grade()
                    {
                        GradeName = "Senior",
                        ClassNum = 4,
                        ClassList = new List<Class>()
                        {
                            new Class() { Students = 9 },
                            new Class() { Students = 9 },
                            new Class() { Students = 9 },
                        },
                    },
                },
            };

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
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("books", "http");

            ////XML序列化
            XmlSerializer xml1 = new XmlSerializer(school.GetType());
            XmlSerializer xml = new XmlSerializer(typeof(School));
            ////字符流

            using StringWriter sw = new StringWriter();
            xml.Serialize(sw, school, ns);
            Console.WriteLine(sw.ToString());

            //////写入文件流
            //string path = "SchoolXML.xml";
            //using FileStream fileStream = new(path, FileMode.OpenOrCreate);

            //using FileStream fileStream = new(path, FileMode.OpenOrCreate | FileMode.Truncate);
            //xml.Serialize(fileStream, school, ns);

            ////XML反序列化
            ////字节流
            ////using StringReader stringReader = new StringReader(sw.ToString());
            ////School xmlSchool = (School)xml.Deserialize(stringReader);
            ////Console.WriteLine($"{xmlSchool.SchoolName}");
            ////Console.WriteLine($"{xmlSchool.SchoolAge}");
            ////Console.WriteLine($"{xmlSchool.level}");
            ////Console.WriteLine($"{xmlSchool.Grades}");

            ////文件流
            ////string path = "SchoolXML.xml";
            ////using FileStream fileStream = new(path, FileMode.Open);
            ////using StreamReader streamReader = new(fileStream, Encoding.UTF8);
            ////School xmlSchoolFile = (School)xml.Deserialize(streamReader);
            ////Console.WriteLine("------------------------------");
            ////Console.WriteLine("Read XML File");

            ////Console.WriteLine($"{xmlSchoolFile.SchoolName}");
            ////Console.WriteLine($"{xmlSchoolFile.SchoolAge}");
            ////Console.WriteLine($"{xmlSchoolFile.level}");
            ////Console.WriteLine($"{xmlSchoolFile.Grades}");

            #endregion XML

            #endregion 6 JSON XML序列化/反序列化

            #region 7 -DateTime 转符合文件格式的字符串

            //7 DateTime 转符合文件格式的字符串
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

            #endregion 7

            #region 8 -LINQ 惰性处理特性

            //LINQ 惰性处理特性
            //Dictionary<string, string> Strs = new Dictionary<string, string>();
            //Strs.Add("Ini", "1");
            //Strs.Add("Inied", "2");
            //Strs.Add("Re", "3");
            //Strs.Add("Reed", "3");
            //Strs.Add("TC_1", "4");
            //Strs.Add("TC_2", "5");
            //Strs.Add("Dir_1", "6");
            //Strs.Add("Dir_2", "7");
            //Strs.Add("SendI", "8");
            //Strs.Add("Sen", "9");
            //Strs.Add("Sta", "2");
            //Strs.Add("Arr_1", "2");
            //Strs.Add("Arr_2", "3");
            //Strs.Add("Vir_1", "3");
            //Strs.Add("Vir_2", "5");

            //var DirllList = Strs.Keys.Where(key => key.StartsWith("Dir_"));
            //foreach (var item in DirllList)
            //{
            //    Console.WriteLine($"{item}");
            //}

            #endregion 8

            #region 9 Git 临时分支提交

            //Git 临时分支提交

            #endregion 9

            #region 10 避免添加相同地址的内容


            #endregion

            #region 11 Ctrl K S 快速添加外层嵌套


            //if (true)
            //{
            //    Console.WriteLine("11");
            //    Console.WriteLine("11");

            //    Console.WriteLine("11");
            //    Console.WriteLine("11");
            //}

            #endregion

            #region 12 反射与特性
            //var reflect = new ForReflect()
            //{
            //    ID = 123,
            //    Name = "Tom",
            //    Value = 1.0,
            //};

            //Console.WriteLine(ReflectOutput(reflect));

            //ReflectTest(typeof(Methods));
            #endregion



            #region 13 Lambda
            //int Method(int o) => o==42 ? 100 : 0;

            //Console.WriteLine($"{Method(42)}");
            #endregion
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

        #region 12 反射与特性

        public static string ReflectOutput(object obj)
        {
            /*  JsonIgnoreAttribute
                             .Where(o =>{
                   var ores = o.GetCustomAttribute<JsonIgnoreAttribute>();
                    if (ores!=null)
                        return false;
                    else
                        return true;
            })
             */
            var res = obj.GetType()
                .GetProperties()
                .Where(o =>
                {
                    var ores = o.GetCustomAttribute<IsShowAttribute>();
                    if (ores != null)
                        return ores.IsShow;
                    else
                        return true;
                })
                .Select(o => new { key = o.Name, value = o.GetValue(obj) });
            return string.Join(Environment.NewLine, res);
        }

        public static bool ReflectTest(Type type)
        {
            var constructorinfos = type.GetConstructors();
            if (constructorinfos.Length == 0)
                return false;
            foreach (var constructor in constructorinfos)
            {
                object? obj;
                var con_Attribute = constructor.GetCustomAttribute<TestMethodAttribute>();
                if (con_Attribute == null)
                    continue;
                else
                {
                    try
                    {
                        obj = Activator.CreateInstance(type, con_Attribute.Parameters);
                        if (obj == null)
                            return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                var methods = type.GetMethods();
                foreach (var method in methods)
                {
                    var method_Attribute = method.GetCustomAttribute<TestMethodAttribute>();
                    if (method_Attribute is not null)
                        method.Invoke(obj, method_Attribute.Parameters);
                }
            }

            return false;
        }

        #endregion
    }

    #region 12 反射与特性
    [AttributeUsage(AttributeTargets.Property)] //限制属性
    class IsShowAttribute : Attribute
    {
        public int key { get; set; }
        public bool IsShow { get; set; }

        public IsShowAttribute(bool b)
        {
            IsShow = b;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    class TestMethodAttribute : Attribute
    {
        public object[]? Parameters { get; set; }

        public TestMethodAttribute(object[]? objects)
        {
            Parameters = objects;
        }
    }

    class ForReflect
    {
        //[JsonIgnore]
        [IsShow(true, key = 2)]
        public int ID { get; set; }

        public string Name { get; set; }

        [IsShow(false)]
        public double Value { get; set; }
    }

    class Methods
    {
        private double priData;

        [TestMethodAttribute([3.0])]
        public Methods(double @double)
        {
            this.priData = @double;
        }

        [TestMethodAttribute(null)]
        public void OutputPriData()
        {
            Console.WriteLine(priData);
        }

        [TestMethodAttribute(["asdf"])]
        public void OutputTest(string str)
        {
            Console.WriteLine(str);
        }

        [TestMethodAttribute([1234])]
        public void OutputTest(int i)
        {
            Console.WriteLine(i);
        }
    }
    #endregion

    #region 10 避免添加相同地址的内容
    class A
    {
        public static List<B> Bs = new();

        public B b;

        public A(int a)
        {
            B btemp = new(a);

            if (Bs.Contains(btemp)) //在list里找与B里的所有或特定成员相等。
            {
                //b=相等的成员
            }
            else
            {
                Bs.Add(btemp);
            }
        }
    }

    class B
    {
        public int c;

        public B(int cc)
        {
            c = cc;
        }
    }

    #endregion

    //(2) 委托解耦调用

    internal class ClassPrint
    {
        public delegate void Print(string str);
    }

    //override
    #region Override

    class Override
    {
        public int OverridePro { get; set; }
    }

    class OverideChild : Override
    {
        public int OverridePro { get; set; }
    }
    #endregion

}
