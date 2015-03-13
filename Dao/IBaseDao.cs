using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Dao
{
    public interface IBaseDao
    {

        #region 增删改
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        int Insert(DataTable dt);
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="dt">修改的表数据</param>
        /// <param name="primary">表主键</param>
        /// <returns></returns>
        int Update(DataTable dt, string primary);
        /// <summary>
        /// 删除记录
        /// </summary>
        int Delete(string tableName);
        /// <summary>
        /// 删除记录
        /// </summary>
        int Delete(string tableName, string key, object value);
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        DataTable GetDatable(string tableName);
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <param name="where">条件，如：string fields="ID=1"</param>
        DataTable GetDatable(string tableName, Hashtable htWhere);
        /// <summary>
        /// 获取表记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="field"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        DataTable GetDataByKey(string tablename, string key, object value);
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句(通常情况下为数据库事务处理的首选，当需要执行插入、删除、更新等操作时，首选)
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        int ExecuteNonQuery(CommandType commandType, string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>一个对象</returns>
        object ExecuteScalar(CommandType commandType, string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        DataSet ExecuteDataset(CommandType commandType, string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        DataTable ExecuteDatatable(CommandType commandType, string sql);
        #endregion
    }
}
