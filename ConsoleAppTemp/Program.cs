namespace ConsoleAppTemp
{
    using FreeSql.DataAnnotations;
    using System;
    using System.Reflection;

    using FreeSql;

    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }

        [Column(IsIgnore = true)]
        public string Url { get; set; }

        public int Rating { get; set; }
    }

    public class WithBlog

    {
        public string asdf { get; set; }

        public DateTime? Date { get; set; }

        [Column(MapType = typeof(Blog))]
        public Blog Blog { get; set; }
    }

    public class Blo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }

        public string Url { get; set; }
        public int Rating { get; set; }
    }

    internal class Program
    {
        private static IFreeSql fsql

              = new FreeSql.FreeSqlBuilder()
        .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=./db/33.db")
        .UseAutoSyncStructure(true) //自动同步实体结构到数据库
        .Build(); //请务必定义成 Singleton 单例模式

        private static void Main(string[] args)
        {
            //try
            //{
            //    fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=./sf/787.db")
            //                  .UseAutoSyncStructure(false) //自动同步实体结构到数据库
            //                      .Build(); //请务必定义成 Singleton 单例模式
            //}
            //catch (Exception)
            //{
            //    throw new ArgumentException("路径下不存在该数据库");
            //}
            var blog = new Blog
            {
                BlogId = 1,
                //Url = "",
                Rating = 1,
            };
            var blo = new Blo
            {
                BlogId = 2,
                //Url = "",
                Rating = 2,
            };
            var wblog = new WithBlog
            {
                asdf = "sdf",
                Blog = blog,
                Date = DateTime.Now,
            };
            var row = fsql.Insert(wblog)
            .ExecuteAffrows();
            //        int affrows = fsql.Select<Blog>()
            //.Limit(10)
            //.InsertInto(null, a => new Blo { BlogId = 2, Rating = 2 });
            Console.WriteLine($"{row}");
        }
    }
}