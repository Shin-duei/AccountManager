using AccountManager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    public class SQLiteHelper
    {
        public string connstr = Path.Combine(Environment.CurrentDirectory, "User.db");//沒有資料庫會創建資料庫
        public SQLiteConnection db;
        public SQLiteHelper()
        {
            db = new SQLiteConnection(connstr);

            db.CreateTable<EssentialModel>();//表已存在不會重覆創建
        }

        public int Add<T>(T model)
        {
            return db.Insert(model);
        }

        public int Update<T>(T model)
        {
            return db.Update(model);
        }

        public int Delete<T>(T model)
        {
            return db.Update(model);
        }
        public List<T> Query<T>(string sql) where T : new()
        {
            return db.Query<T>(sql);
        }
        public int Execute(string sql)
        {
            return db.Execute(sql);
        }
    }
}
