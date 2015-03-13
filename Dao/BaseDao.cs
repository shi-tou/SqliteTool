using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Common;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Dao
{
    public class BaseDao : IBaseDao
    {
        #region 初始化数据
        /// <summary>
        /// SQLite数据库连接字符串
        /// </summary>
        public static string DataSource
        {
            get { return SQLiteHelper.DataSource; }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        public int InitDatabase(string sql)
        {
            try
            {
                string path = SQLiteHelper.DataFullPath;
                if (!File.Exists(path))
                {
                    SQLiteHelper.CreateDataBase();  //创建新的数据库 使用时注意，如果已经存在，则覆盖旧的数据库
                    SQLiteHelper.ExecuteNonQuery(DataSource, CommandType.Text, sql);
                }
                return 0;
            }
            catch (Exception ex)
            {
                Util.SaveLog("InitDatabase", ex.Message);
                return -1;
            }
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public int Insert(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                List<SQLiteParameter> param = new List<SQLiteParameter>();
                DataRow dr = dt.Rows[0];
                string sql = "insert into {0} ({1}) values ({2});select last_insert_rowid() from {0}; ";
                string cols = "";
                string vals = "";
                foreach (DataColumn col in dt.Columns)
                {
                    string key = col.ColumnName;
                    if (dr[key] != DBNull.Value)
                    {

                        cols += (cols != "" ? "," : "") + key;
                        vals += (vals != "" ? ",@" : "@") + key;
                        SQLiteParameter p = new SQLiteParameter( col.ColumnName, dr[col.ColumnName]);
                        param.Add(p);
                    }
                }
                sql = string.Format(sql, dt.TableName, cols, vals);
                Object objValue = SQLiteHelper.ExecuteScalar(DataSource, CommandType.Text, sql, param.ToArray());
                if (objValue == null)
                    return 0;
                else
                    return Convert.ToInt32(objValue);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="dt">修改的表数据</param>
        /// <param name="primary">表主键</param>
        /// <returns></returns>
        public int Update(DataTable dt, string primary)
        {
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            string sql = "update {0} set {1} where {2}";
            string sqlSet = "";
            string sqlWhere = "";
            DataRow dr = dt.Rows[0];
            foreach (DataColumn col in dt.Columns)
            {
                string key = col.ColumnName;
                if (dr[key] != DBNull.Value)
                {

                    if (key == primary)
                    {
                        sqlWhere += string.Format("{0}=@{0}", key);
                    }
                    else
                    {
                        sqlSet += (sqlSet != "" ? "," : "") + string.Format("{0}=@{0}", key);
                    }
                    SQLiteParameter p = new SQLiteParameter( col.ColumnName, dr[col.ColumnName]);
                    param.Add(p);
                }
            }
            sql = string.Format(sql, dt.TableName, sqlSet, sqlWhere);
            int res = SQLiteHelper.ExecuteNonQuery(DataSource, CommandType.Text, sql, param.ToArray());
            return res;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName)
        {
            return Delete(tableName, "", "");
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName, string key, object value)
        {
            string sql = string.Format("delete from {0} where {1}=@{1}", tableName, key);
            SQLiteParameter[] param = { new SQLiteParameter(key, value) };
            return SQLiteHelper.ExecuteNonQuery(DataSource, CommandType.Text, sql, param);
        }
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        public DataTable GetDatable(string tableName)
        {
            string sql = string.Format("select * from {0}", tableName);
            DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, sql).Tables[0];
            dt.TableName = tableName;
            return dt;
        }
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <param name="where">条件，如：string fields="ID=1"</param>
        public DataTable GetDatable(string tableName, Hashtable htWhere)
        {
            List<SQLiteParameter> param = new List<SQLiteParameter>();
            string sql = string.Format("select * from {0} where 1=1 ", tableName);
            string sqlWhere = "";
            if (htWhere.Count > 0)
            {
                foreach (KeyValuePair<String, String> val in htWhere)
                {
                    sqlWhere += String.Format(" and {0}=@{0}", val.Key, val.Value);
                    param.Add(new SQLiteParameter(val.Key, val.Value));
                }
            }
            sql += sqlWhere;

            DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, sql, param.ToArray()).Tables[0];
            dt.TableName = tableName;
            return dt;
        }
        /// <summary>
        /// 获取表记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="field"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable GetDataByKey(string tableName, string key, object value)
        {
            string sql = string.Format("select * from {0} where {1}=@{1}", tableName, key);
            SQLiteParameter[] param = { new SQLiteParameter( key, value) };
            DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, sql, param).Tables[0];
            dt.TableName = tableName;
            return dt;
        }
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句(通常情况下为数据库事务处理的首选，当需要执行插入、删除、更新等操作时，首选)
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        public int ExecuteNonQuery(CommandType commandType, string sql)
        {
            return SQLiteHelper.ExecuteNonQuery(DataSource, commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>一个对象</returns>
        public object ExecuteScalar(CommandType commandType, string sql)
        {
            return SQLiteHelper.ExecuteScalar(DataSource, commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        public DataSet ExecuteDataset(CommandType commandType, string sql)
        {
            return SQLiteHelper.ExecuteDataset(DataSource, commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        public DataTable ExecuteDatatable(CommandType commandType, string sql)
        {
            return SQLiteHelper.ExecuteDataset(DataSource, commandType, sql).Tables[0];
        }
        #endregion
    }
}
