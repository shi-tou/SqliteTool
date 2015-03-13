using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 分页器
    /// </summary>
    public class Pager
    {
        public Pager() { }
        public Pager(int pagesize, int pageindex, string orderkey)
        {
            PageSize = pagesize;
            PageIndex = pageindex;
            OrderKey = orderkey;
        }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 页序号
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ItemCount
        {
            get;
            set;
        }
        /// <summary>
        /// DataTable
        /// </summary>
        public DataTable DataSource
        {
            get;
            set;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderKey
        {
            get;
            set;
        }
        /// <summary>
        /// 将普通sql语句拼成分页用的sql语句
        /// </summary>
        /// <param name="str"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        public string PagerSql(string oldsql)
        {
            string sql = oldsql + string.Format(" limit {0},{1};", (PageIndex - 1) * PageSize, PageSize);
            sql += " select count(*) from (" + oldsql + ") as tmp;";
            return sql;
        }
    }
}
