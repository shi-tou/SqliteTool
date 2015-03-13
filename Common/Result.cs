using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class Result
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static int RESULT_SUCCESS = 0;
        /// <summary>
        /// 失败
        /// </summary>
        public static int RESULT_FAILED = -1;
        /// <summary>
        /// 已存在/重复
        /// </summary>
        public static int RESULT_EXIST = 101;
        /// <summary>
        /// 不存在
        /// </summary>
        public static int RESULT_NOT_EXIST = 102;
        /// <summary>
        /// 错误密码
        /// </summary>
        public static int RESULT_ERROR_PASSWORD = 103;
        /// <summary>
        /// 锁定
        /// </summary>
        public static int RESULT_LOCK = 103;

    }
}
