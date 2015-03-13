using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dao;
using SqliteDemo.Properties;
using System.IO;
using Common;
using System.Data;
using SqliteDemo.Sys;

namespace SqliteDemo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //初始化
            InitDatabase(Resources.InitDataSql);
            SQLiteTools tool = new SQLiteTools();
            tool.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(tool);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public static int InitDatabase(string sql)
        {
            try
            {

                if (!Directory.Exists(SQLiteHelper.DataFullPath))
                {
                    Directory.CreateDirectory(SQLiteHelper.DataFullPath);
                }

                if (!File.Exists(SQLiteHelper.DataFilePath))
                {
                    SQLiteHelper.CreateDataBase();  
                    SQLiteHelper.ExecuteNonQuery(SQLiteHelper.DataSource, CommandType.Text, sql);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Util.SaveLog("InitDatabase", ex.Message);
                return -1;
            }
        }
    }
}
