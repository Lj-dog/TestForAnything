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
            try
            {
                fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=" + dbFilePath)
                    .UseAutoSyncStructure(isAutoAddTableOrFile) //自动同步实体结构到数据库
                    .Build(); //请务必定义成 Singleton 单例模式
            }
            catch (Exception)
            {
                throw new ArgumentException("路径下不存在该数据库");
            }
        }


        public int LoadToDB(List<People> dbData)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ChangeLastData(uint age)
        {
            try
            {
                var person = fsql.Select<People>().Where(p => p.Name == "小明").OrderByDescending(p => p.DataInputTime)
                    .ToOne();
                if (person != null)
                {
                    //person.Age = age;
                    int i = fsql.Update<People>()
                        .Set(p => p.Age, age)
                        .Where(p => p.Guid == person.Guid)
                        .ExecuteAffrows();
                    return i;
                }

                return -1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public int CoverListData(List<People> dbData)
        {
            try
            {
                //int i = fsql.Update<People>()
                //    .SetSource(dbData)
                //    .Where(p => p.Name == "小明")
                //    .ExecuteAffrows();

//******************************************************************************************
                //var personList = fsql.Select<People>().Where(p => p.Name == "小明").ToList();
                //var updateList = (from personData in personList
                //    join coverData in dbData
                //        on personData.Age equals coverData.Age
                //    select new People
                //    {
                //        Name = coverData.Name,
                //        Age = coverData.Age,
                //        PhoneNum = coverData.PhoneNum
                //    }).ToList();
                //foreach (var person in updateList)
                //{
                //    int i =
                //        fsql.Update<People>()
                //            .Set(p => p.PhoneNum, person.PhoneNum)
                //            .Where(p => p.Name == person.Name)
                //            .Where(p => p.Age == person.Age)
                //            .ExecuteAffrows();
                //}

//****************************************************************************
                var personList = fsql.Select<People>().Where(p => p.Name == "小明").ToList();
                //foreach (var person in personList)
                //{
                //    if (person.Age == 555)
                //    {

                //        var tempGuid = person.Guid;
                //         person = dbData.FirstOrDefault(p=>p.Age == 555);
                //        person.PhoneNum = 9999;
                //    }
                //}

                for (int j = 0; j < personList.Count; j++)
                {
                    if (personList[j].Age == 555)
                    {
                        var tempGuid = personList[j].Guid;
                        personList[j] = dbData.FirstOrDefault(p => p.Age == 555 && p.Name == "小明");
                        personList[j].Guid = tempGuid;
                    }
                }

                int i =
                    fsql.Update<People>()
                        .SetSource(personList)
                        .ExecuteAffrows();
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}