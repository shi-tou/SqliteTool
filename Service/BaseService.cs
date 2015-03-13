using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dao;
using Autofac;
using Autofac.Configuration;
using Common;
using System.Data;
using System.Collections;
namespace Service
{
    public class BaseService : IBaseService
    {
        private IBaseDao baseDao
        {
            get
            {
                return IocContainer<BaseDao>.GetInstance();
            }
        }
        #region 增删改
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public int Insert(DataTable dt)
        {
            return baseDao.Insert(dt);
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="dt">修改的表数据</param>
        /// <param name="primary">表主键</param>
        /// <returns></returns>
        public int Update(DataTable dt, string primary)
        {
            return baseDao.Update(dt, primary);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName)
        {
            return baseDao.Delete(tableName);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName, string key, object value)
        {
            return baseDao.Delete(tableName, key, value);
        }
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        public DataTable GetDatable(string tableName)
        {
            return baseDao.GetDatable(tableName);
        }
        /// <summary>
        ///  获取DataTable表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dicWhere"></param>
        /// <returns></returns>
        public DataTable GetDatable(string tableName, Hashtable htWhere)
        {
            return baseDao.GetDatable(tableName, htWhere);
        }
        /// <summary>
        /// 获取表记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="field"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public DataTable GetDataByKey(string tablename, string key, object value)
        {
            return baseDao.GetDataByKey(tablename, key, value);
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
            return baseDao.ExecuteNonQuery(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>一个对象</returns>
        public object ExecuteScalar(CommandType commandType, string sql)
        {
            return baseDao.ExecuteScalar(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        public DataSet ExecuteDataset(CommandType commandType, string sql)
        {
            return baseDao.ExecuteDataset(commandType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        public DataTable ExecuteDatatable(CommandType commandType, string sql)
        {
            return baseDao.ExecuteDatatable(commandType, sql);
        }
        #endregion
    }
}
