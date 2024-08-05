using FreeSql.DataAnnotations;
using System;
using FreeSql;

namespace ForSQLite
{
    public class SQLiteHelper
    {
        private static IFreeSql fsql;

        private Type dbTabletype;

        public SQLiteHelper(string dbFilePath, bool isAutoAddTableOrFile/*, Type dbTabletype*/)
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

        public int LoadToDB(object dbData)
        {
            //if (dbData.GetType()!= dbTabletype)
            //{
            //    return false;
            //}
            try
            {
                var row = fsql.Insert(dbData)
                                .ExecuteAffrows();
                return row;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}