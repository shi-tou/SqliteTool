using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// SQLite数据类型
    /// 1、SQLite没有单独的布尔存储类型，它使用INTEGER作为存储类型，0为false，1为true
    /// 2、SQLite没有为存储日期和时间设定一个存储类集，内置的sqlite日期和时间函数能够将日期和时间以TEXT，REAL或INTEGER形式存放
    /// 注意：字段类型很多：建议统一只使用INTEGER和TEXT和BLOB（字节流）
    /// </summary>
    public enum SQLiteDataType
    {
        /// <summary>
        /// 带符号的整型，具体取决于存入数字的范围大小。
        /// </summary>
        INTEGER = 1,
        /// <summary>
        /// 浮点型值，存储为8-byte IEEE浮点数。
        /// </summary>
        REAL = 2,
         /// <summary>
        /// 文本字符串，使用数据库编码（UTF-8，UTF-16BE或者UTF-16LE）存放
        /// </summary>
        TEXT = 3,
        /// <summary>
        /// 二进制对象(只是一个数据块，完全按照输入存放（即没有转换换）)
        /// </summary>
        BLOB = 4       
    }
}
