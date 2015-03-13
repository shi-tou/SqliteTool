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
    public class UserService : BaseService,IUserService
    {
        private IUserDao userDao
        {
            get
            {
                return IocContainer<UserDao>.GetInstance();
            }
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetUserList(Pager p, Hashtable hs)
        {
            return userDao.GetUserList(p, hs);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int UserLogin(string userName, string password)
        {
            DataTable dt = GetDataByKey("T_User", "UserName", userName);
            if (dt.Rows.Count == 0)
            {
                return Result.RESULT_NOT_EXIST;
            }
            if (dt.Rows[0]["Password"].ToString() != password)
            {
                return Result.RESULT_ERROR_PASSWORD;
            }
            return Result.RESULT_SUCCESS;
        }
    }
}
