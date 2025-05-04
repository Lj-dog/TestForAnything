using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
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

    public class Program
    {
        // 14 值改变时中断
        static ushort interruptTest = 10;

        #region 16 enum 成员类型设置为byte
        [Flags]
        enum MyEnum:ulong
        {
            MY0 = 0x00,
            MY1 = 0x01,
            MY2 = 0x02,
            MY3 = 0x04,
            MY5 = 0x08,
            MY6 = 0x10,
            MY11 = MY1 | MY2,
            MY101 = MY1 | MY5,
            MY111 = MY3 | MY6,
            MY512 = 512,
            MY1024 = 1024,
            MY524288 = 524288,
            MY2147483648 = 2147483648,
            MY4294967296 = 4294967296,
        }

        static MyEnum ReturnEnum(byte a)
        {
            return (MyEnum)a;
        }
        #endregion

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
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("books", "http");

            //////XML序列化
            //XmlSerializer xml1 = new XmlSerializer(school.GetType());
            //XmlSerializer xml = new XmlSerializer(typeof(School));
            //////字符流

            //using StringWriter sw = new StringWriter();
            //xml.Serialize(sw, school, ns);
            //Console.WriteLine(sw.ToString());

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

            #region 8 -LINQ 惰性处理特性 ,Dictionary可以通过索引器添加不存在的键值对

            //LINQ 惰性处理特性
            //Dictionary<string, string> Strs = new Dictionary<string, string>();

            ////Dictionary可以通过索引器添加不存在的键值对
            //Strs["Ini"] = "1";
            //Strs["Inied"] = "2";
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

            #region 13 Lambda 与 表达式成员

            //            其次是 =>，这个特性叫表达式体成员，expression - bodied member，用法有三种：
            //            1、代替只有get的属性：
            //            int Property { get { return 42; } }
            //            int Property { get => 42; }
            //            int Property => 42;
            //            这样逐步简化得来
            //            2、返回void的成员
            //            void Method() { Console.WriteLine(); }
            //            void Method() => Console.WriteLine();
            //            3、返回非void的成员
            //            int Method() { return _field >= 42 ? 100 : 0; }
            //            int Method() => field >= 42 ? 100 : 0;
            //            通过借用lambda语法来达成简化的目的

            //int Method_1(int o) => o == 42 ? 100 : 0;

            //Console.WriteLine($"{Method_1(42)}");


            //Func<int,int> Method_2 = o => o == 42 ? 100 : 0; //lambda表达式
            //Console.WriteLine($"{Method_2(41)}");

            //var Method_3 = (int o =2) =>  //lambad语句块
            //{
            //    return o == 42 ? 100 : 0;
            //};

            //Console.WriteLine($"{Method_3(42)}");

            #endregion

            #region 14 值改变时中断(失败，C#不可)

            //interruptTest = 10;

            //ChangeInterruptValue();

            //var interruptClass = new InterruptValueClass();

            //interruptClass.ChangeInterruptValue();
            #endregion

            #region 15 查看调用堆栈找到调用函数的地方

            //while (true)
            //{
            //    int a = int.Parse(Console.ReadLine());

            //    if (a == 1)
            //        CheckFun();
            //    else if (a == 2)
            //        CheckFun();
            //    else if (a == 3)
            //        CheckFun();

            //    CheckFun();
            //    CheckFun();
            //    CheckFun();

            //}

            #endregion

            #region 16 enum 成员类型设置为byte ,测试枚举值是否能超过16位
            //Console.WriteLine(MyEnum.MY101);
            //if ((MyEnum)0x01 == MyEnum.MY1)
            //    Console.WriteLine(true);
            //if ((MyEnum.MY2 & MyEnum.MY5) == MyEnum.MY0)
            //    Console.WriteLine(true);
            //if ((ReturnEnum(0x09) & MyEnum.MY5) == MyEnum.MY0)
            //    Console.WriteLine(true);
            //else
            //    Console.WriteLine(false);


            //Console.WriteLine((ulong)MyEnum.MY4294967296);
            #endregion

            #region 17 object与数组转换
            //int[] ints = { 3, 4, 5 };
            //object obj = Array.ConvertAll(ints, s => (object)s);

            //Console.WriteLine(obj.ToString());
            //object[] objarry = (object[])obj;
            //foreach (object o in objarry)
            //{
            //    Console.WriteLine(o.ToString());
            //}

            //object[] objs = { "1", 2 };

            //foreach (object item in objs)
            //{
            //    string str = item.ToString();
            //    Console.WriteLine(str);
            //}
            #endregion

            #region 18 迭代器,迭代时删除
            //var numbers = ProduceEvenNumbers(5);
            //Console.WriteLine("Caller: about to iterate.");
            //foreach (int i in numbers)
            //{
            //    Console.WriteLine($"Caller: {i}");
            //}

            //// Output:
            //// Caller: about to iterate.
            //// Iterator: start.
            //// Iterator: about to yield 0
            //// Caller: 0
            //// Iterator: yielded 0
            //// Iterator: about to yield 2
            //// Caller: 2
            //// Iterator: yielded 2
            //// Iterator: about to yield 4
            //// Caller: 4
            //// Iterator: yielded 4
            //// Iterator: end.
            ///

            //删除
            //List<int> ints = new List<int>() { 1,2,3,4,5};
            // List<int> delints=new();
            // delints.Add(ints[1]);
            // delints.Add(ints[2]);
            // delints.Add(ints[3]);

            // foreach (var item in delints)
            // {
            //     Console.WriteLine(item);
            //     ints.Remove(item);
            // }
            // foreach (var item in ints)
            // {
            //     Console.Write(item + " ");
            // }

            ///////////////////////////////////////////////////////
            //List<ForIterator> forIterators = new List<ForIterator>()
            //{
            //    new(1),
            //    new(2),
            //    new(3),
            //    new(4),
            //    new(5),
            //};
            //List<ForIterator> delforIterators = new();
            //delforIterators.Add(forIterators[1]);
            //delforIterators.Add(forIterators[2]);
            //delforIterators.Add(forIterators[3]);

            //foreach (var item in delforIterators)
            //{
            //    Console.WriteLine(item);
            //    forIterators.Remove(item);
            //}
            //foreach (var item in forIterators)
            //{
            //    Console.Write(item + " ");
            //}
            #endregion

            #region 19 字符串根据不同编码格式转字节数组byte ,Ushrot数组转byte数组,byte数组转Ushort数组
            //byte[] ASCIIbytes = new byte[] { 0x61, 0x73, 0x64, 0x66 }; //asdf  97 115 100 102
            //byte[] Unicodebytes = new byte[] { 0x3f, 0x96, 0xaf, 0x65, 0x82, 0x84, 0xac, 0x82 }; //阿斯蒂芬

            //Console.WriteLine(StrBytesHelper.StringToBytes("asdf", Encoding.ASCII).OutputBytes());
            //Console.WriteLine(StrBytesHelper.StringToBytes("asdf", Encoding.Unicode).OutputBytes());

            //Console.WriteLine(StrBytesHelper.BytesToString(ASCIIbytes, Encoding.ASCII));
            //Console.WriteLine(StrBytesHelper.BytesToString(Unicodebytes, Encoding.Unicode));

            //ushort u= 513;   //0x0201
            //ushort[] us = { 513, 1027,1541 }; //0x0201 0x0403 0x0605

            //Console.WriteLine(UshortBytesHelper.UshortToBytes(u).OutputBytes());
            //Console.WriteLine(UshortBytesHelper.UshortsToBytes(us).OutputBytes());

            //byte b = 0x11;
            //byte[] bs = { 0x11,0x22,0x33};

            //Console.WriteLine(BytesUshortHelper.ByteToUshort(b).ToString("X2"));
            //Console.WriteLine(BytesUshortHelper.BytesToUshorts(bs).OutputBytes());
            #endregion

            #region 20 List添加null数据占位
            //List<string?> strings = new();
            //while (strings.Count < 2)
            //{
            //    strings.Add(null);
            //}
            //strings.Add("2");
            //foreach (var item in strings)
            //{
            //    Console.WriteLine(item);
            //    Console.WriteLine("//////////////");
            //}
            #endregion

            #region 21 匿名类型使用
            //var o = new { Name ="Roger" ,Age = 20 };

            //Console.WriteLine(o);

            //List<ForAnonymousType> forAnonymousTypes = new List<ForAnonymousType>()
            //{
            //    new(){X =10 ,Y =10 , Z = 10},
            //    new(){X =20 ,Y =20 , Z = 20},
            //    new(){X =30 ,Y =30 , Z = 30},
            //    new(){X =40 ,Y =40 , Z = 40},
            //};

            //var list = forAnonymousTypes.Select(o => new { o.X, o.Y });

            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region 22 异常后是否能执行catch{}后面的代码
            //Test();
            #endregion

            #region 23 探究C# Interface多继承是否会造成菱形继承

            #endregion

            #region 24 深拷贝
            DeepCopy original = new DeepCopy()
            {
                ID = 1,
                Name = "Tom",
                List = new List<string>() { "1", "2", "3" },
            };

            Console.WriteLine("Original: ");
            Console.WriteLine($"ID: {original.ID}");
            Console.WriteLine($"Name: {original.Name}");
            Console.WriteLine($"List: {string.Join(", ", original.List)}");
            Console.WriteLine("-------------------------------------------------");

            //深拷贝
            DeepCopy deepCopyXML  = DeepCopy.DeepCopyXML(original);
            deepCopyXML.ID = 2;
            deepCopyXML.Name = "Jerry";
            deepCopyXML.List[0] = "4";
            Console.WriteLine("XMLCopy: ");
            Console.WriteLine($"ID: {deepCopyXML.ID}");
            Console.WriteLine($"Name: {deepCopyXML.Name}");
            Console.WriteLine($"List: {string.Join(", ", deepCopyXML.List)}");
            Console.WriteLine("-------------------------------------------------");

            DeepCopy deepCopyJson = DeepCopy.DeepCopyJson(original);
            deepCopyJson.ID = 3;
            deepCopyJson.Name = "Jack";
            deepCopyJson.List[0] = "5";
            Console.WriteLine("JsonCopy: ");
            Console.WriteLine($"ID: {deepCopyJson.ID}");
            Console.WriteLine($"Name: {deepCopyJson.Name}");
            Console.WriteLine($"List: {string.Join(", ", deepCopyJson.List)}");
            Console.WriteLine("-------------------------------------------------");

            //原始对象
            Console.WriteLine("Original: ");
            Console.WriteLine($"ID: {original.ID}");
            Console.WriteLine($"Name: {original.Name}");
            Console.WriteLine($"List: {string.Join(", ", original.List)}");
            Console.WriteLine("-------------------------------------------------");
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


        #region 14 值改变时中断

        public static void ChangeInterruptValue()
        {
            interruptTest = 20;
        }

        public class InterruptValueClass
        {
            private int interrupt;

            public InterruptValueClass()
            {
                interrupt = 20;
            }

            public void ChangeInterruptValue()
            {
                interrupt = 100;
            }
        }
        #endregion

        //(15)  查看调用堆栈找到调用函数的地方
        public static void CheckFun()
        {
            Console.WriteLine(DateTime.Now);
        }

        #region 16 new 其他用法

        //override
        #region Override

        class Override
        {
            public int OverridePro { get; set; }
        }

        class OverideChild : Override
        {
            public new int OverridePro { get; set; }
        }
        #endregion

        //约束类型不能为抽象类。当与其他约束一起使用时，new() 约束必须最后指定：
        class ItemFactory<T>
            where T : IComparable, new()
        {
            public T GetNewItem()
            {
                return new T();
            }
        }
        #endregion

        #region 18 迭代器
        private static IEnumerable<int> ProduceEvenNumbers(int upto)
        {
            Console.WriteLine("Iterator: start.");
            for (int i = 0; i <= upto; i += 2)
            {
                Console.WriteLine($"Iterator: about to yield {i}");
                yield return i;
                Console.WriteLine($"Iterator: yielded {i}");
            }
            Console.WriteLine("Iterator: end.");
        }
        #endregion

        #region 22 异常后是否能执行catch{}后面的代码
        public static void Test()
        {
            object _lock = new object();
            try
            {
                lock (_lock)
                {
                    Console.WriteLine("try");
                    throw new Exception("test"); 
                }
            }
            catch (Exception)
            {
                Console.WriteLine("catch");
            }
            Console.WriteLine("AfterCatch");
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

    #region 18 迭代器
    class ForIterator
    {
        private int i;

        public ForIterator(int i)
        {
            this.i = i;
        }

        public override string ToString()
        {
            return i.ToString();
        }
    }
    #endregion

    #region 19 字符串根据不同编码格式转字节数组byte,Ushrot数组转bytes数组
    static class StrBytesHelper
    {
        public static byte[] StringToBytes(string s, Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static StringBuilder OutputBytes(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                stringBuilder.Append("0x" + b.ToString("X2") + " ");
            }
            stringBuilder.Append('\n');
            return stringBuilder;
        }
    }

    static class UshortBytesHelper
    {
        public static byte[] UshortToBytes(ushort s)
        {
            return BitConverter.GetBytes(s);
        }

        public static byte[] UshortsToBytes(ushort[] ushorts)
        {
            byte[] bytes = new byte[ushorts.Length * sizeof(ushort)];
            Buffer.BlockCopy(ushorts, 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }

    static class BytesUshortHelper
    {
        public static ushort ByteToUshort(byte b)
        {
            return (ushort)b;
        }

        public static ushort[] BytesToUshorts(byte[] bs)
        {
            ushort[] res = new ushort[(bs.Length + 1) / 2];
            if (bs.Length % 2 != 0)
            {
                Array.Resize(ref bs, bs.Length + 1);
            }

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = BitConverter.ToUInt16(bs, 2 * i);
            }
            return res;
        }

        public static StringBuilder OutputBytes(this ushort[] us)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var b in us)
            {
                stringBuilder.Append("0x" + b.ToString("X2") + " ");
            }
            stringBuilder.Append('\n');
            return stringBuilder;
        }
    }
    #endregion

    #region 21 匿名类型 （截取类里的部分数据）
    class ForAnonymousType
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
    #endregion

    #region 23 探究C# Interface多继承是否会造成菱形继承
    interface IA
    {
        int MA { get; set; }
    }

    interface IB:IA
    {
        int MB { get; set; }
    }

    interface IC:IA
    {
        int MC {  get; set; } 
    }
    #endregion

    #region 24 深拷贝
  public  class DeepCopy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> List { get; set; } = new();

        public static T? DeepCopyXML<T>(T obj)
        {
            object? retval;
            using (MemoryStream ms = new())
            {
                XmlSerializer xml = new(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }
            return (T?)retval;
        }

        public static T? DeepCopyJson<T>(T obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json);
        }
    }

   
    #endregion
}
