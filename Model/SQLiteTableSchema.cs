using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SQLiteTableSchema
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        public SQLiteDataType DataType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool Primary
        {
            get;
            set;
        }
        /// <summary>
        /// 自动增长
        /// </summary>
        public bool AotoIncrement
        {
            get;
            set;
        }
        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool Null
        {
            get;
            set;
        }
        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue
        {
            get;
            set;
        }
        /// <summary>
        /// 获取类型名
        /// </summary>
        /// <returns></returns>
        public string GetDateType()
        {
            string s = "";
            switch (DataType)
            {
                case SQLiteDataType.INTEGER:
                    s = "INTEGER";
                    break;
                case SQLiteDataType.REAL:
                    s = "REAL";
                    break;
                case SQLiteDataType.TEXT:
                    s = "TEXT";
                    break;
                case SQLiteDataType.BLOB:
                    s = "BLOB";
                    break;
            }
            return s;
        }
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        public string GetPrimary()
        {
            return Primary ? "PRIMARY KEY" : "";
        }
        /// <summary>
        /// 自增
        /// </summary>
        /// <returns></returns>
        public string GetAotoIncrement()
        {
            return AotoIncrement ? "AUTOINCREMENT" : "";
        }
        /// <summary>
        /// 允许为空
        /// </summary>
        /// <returns></returns>
        public string GetNull()
        {
            return Null ? "" : "NOT NULL";
        }
        /// <summary>
        /// 默认值
        /// </summary>
        /// <returns></returns>
        public string GetDefaultValue()
        {
            return (DefaultValue == null || Convert.ToString(DefaultValue) == "") ? "" : "DEFAULT " + DefaultValue;
        }
        /// <summary>
        /// 将结构转换为字段语句
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0}] {1} {2} {3} {4} {5}", FieldName, GetDateType(), GetPrimary(), GetAotoIncrement(), GetNull(), GetDefaultValue());
        }
    }
}
