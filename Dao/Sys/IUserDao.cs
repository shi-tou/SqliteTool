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
    public interface IUserDao : IBaseDao
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetUserList(Pager p, Hashtable hs);
    }
}
