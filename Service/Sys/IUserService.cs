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
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetUserList(Pager p, Hashtable hs);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int UserLogin(string userName, string password);
    }
}
