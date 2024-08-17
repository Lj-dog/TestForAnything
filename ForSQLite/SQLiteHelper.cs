using FreeSql.DataAnnotations;
using System;
using FreeSql;
using ForSQLite.Models;

namespace ForSQLite
{
    public class SQLiteHelper
    {
        private static IFreeSql fsql;

        public SQLiteHelper(string dbFilePath, bool isAutoAddTableOrFile)
        {
            //this.dbTabletype = dbTabletype;
            try
            {
                fsql = new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=" + dbFilePath)
                              .UseAutoSyncStructure(isAutoAddTableOrFile) //自动同步实体结构到数据库
                                  .Build(); //请务必定义成 Singleton 单例模式
            }
            catch (Exception)
            {
                throw new ArgumentException("路径下不存在该数据库");
            }
        }

        //public int LoadToDB<T>(IEnumerable<T> dbData) where T : class
        //{
        //    try
        //    {
        //        var row = fsql.Insert(dbData)
        //                        .ExecuteIdentity();
        //        return (int)row;
        //    }
        //    catch (Exception)
        //    {
        //        return -1;
        //    }
        //}

        public int LoadToDB(People dbData)
        {
            try
            {
                var row = fsql.Insert<People>(dbData)
                                .ExecuteIdentity();
                return (int)row;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}