using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Common;
using System.IO;
using System.Collections;

namespace Dao
{
    public class UserDao : BaseDao, IUserDao
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetUserList(Pager p, Hashtable hs)
        {
            string sql = "select * from T_User where 1=1";
            string sqlWhere = "";
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            if (hs.Count > 0)
            {
                foreach (KeyValuePair<String, String> val in hs)
                {
                    sqlWhere += String.Format(sqlWhere == "" ? "{0}=@{0}" : ",{0}=@{0}", val.Key, val.Value);
                    param.Add(new SQLiteParameter(val.Key, val.Value));
                }
            }
            sql += sqlWhere;
            sql = p.PagerSql(sql);
            DataSet ds = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, sql, param.ToArray());
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return Result.RESULT_SUCCESS;
        }
    }
}
