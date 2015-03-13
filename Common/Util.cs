using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using System.Web;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Xml;

namespace Common
{
    public static class Util
    {
        const string ENCRYPT_KEY = "mtyt110310";// 密钥，8个字符

        #region 加密 解密
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String EncryptMD5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(input);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;

        }
        /// <summary>
        /// 64位DES加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptDES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(ENCRYPT_KEY);
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//对称算法的初始化向量，64位（8个字节）
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] bInput = Encoding.UTF8.GetBytes(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(bInput, 0, bInput.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        public static string EncryptDES(string input, string passord)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(passord);
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//对称算法的初始化向量，64位（8个字节）
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] bInput = Encoding.UTF8.GetBytes(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(bInput, 0, bInput.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 64位DES解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecryptDES(string input)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(ENCRYPT_KEY);
            byte[] len = new Byte[input.Length];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                len = Convert.FromBase64String(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(len, 0, len.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string DecryptDES(string input, string password)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(password);
            byte[] len = new Byte[input.Length];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                len = Convert.FromBase64String(input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);
                cs.Write(len, 0, len.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64EnCode(string input)
        {
            char[] Base64Code = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
             'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
             'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
             '8','9','+','/','='};
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(input));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                int[] outstr = new int[4];
                outstr[0] = instr[0] >> 2;
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }
        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64Decode(string input)
        {
            if ((input.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "input");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(input, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "input");
            }
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            int page = input.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = input.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }

        #endregion

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="MessageTo">收件人邮箱地址</param>
        /// <param name="MessageSubject">邮件主题</param>
        /// <param name="MessageBody">邮件内容</param>
        /// <returns></returns>
        public static bool SendInternalMail(string to, string subject, string body)
        {
            string Smtp = GetConfig("Smtp");//服务端口
            string SendMailbox = GetConfig("SendMailbox");//邮箱发件人
            string UserName = GetConfig("UserName");//邮箱用户名
            string Password = GetConfig("Password");//邮件服务密码
            string displayname = GetConfig("EmailDisplayname");//邮件签名

            //  string SendName = System.Configuration.ConfigurationSettings.AppSettings["SendName"];//邮件服务密码
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SendMailbox, displayname);
            mail.To.Add(to);              //收件人邮箱地址可以是多个以实现群发
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;              //是否为html格式
            mail.Priority = MailPriority.High;   //发送邮件的优先等级
            mail.SubjectEncoding = Encoding.UTF8;
            SmtpClient sc = new SmtpClient();
            sc.Host = Smtp;              //指定发送邮件的服务器地址或IP
            sc.Port = 25;                           //指定发送邮件端口
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential(UserName, Password); //指定登录服务器的用户名和密码
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                sc.Send(mail);       //发送邮件
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strContent"></param>
        public static void SaveLog(string strTitle, string strContent)
        {
            try
            {
                string Path = AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string FilePath = Path + DateTime.Now.Day + "_Log.txt";
                if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
                if (!File.Exists(FilePath))
                {
                    FileStream FsCreate = new FileStream(FilePath, FileMode.Create);
                    FsCreate.Close();
                    FsCreate.Dispose();
                }
                FileStream FsWrite = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                StreamWriter SwWrite = new StreamWriter(FsWrite);
                SwWrite.WriteLine(string.Format("{0}{1}[{2}]{3}", "--------------------------------", strTitle, DateTime.Now.ToString("HH:mm"), "--------------------------------"));
                SwWrite.Write(strContent);
                SwWrite.WriteLine("\r\n");
                SwWrite.WriteLine(" ");
                SwWrite.Flush();
                SwWrite.Close();
            }
            catch { }
        }

        /// <summary>
        /// 获取配置文件键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();

            }
            catch
            {
                return "";
            }
        }

        #region 生成验证码
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 6; i++)
            {
                number = random.Next();

                code = (char)('0' + (char)(number % 10));

                checkCode += code.ToString();
            }

            return checkCode;
        }
        #endregion

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例
                    if (originalImage.Width <= width)
                    {
                        towidth = ow;
                        toheight = oh;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case "H"://指定高，宽按比例
                    if (originalImage.Height <= height)
                    {
                        towidth = ow;
                        toheight = oh;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                string Path = thumbnailPath.Substring(0, thumbnailPath.LastIndexOf('\\'));
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }

                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 从一个Datatable中按条件分离出另一个datatable
        /// </summary>
        public static DataTable SelectDataTable(DataTable dt, string strWhere)
        {
            DataView view = new DataView();
            view.Table = dt;
            view.RowFilter = strWhere;
            return view.ToTable();
        }
        /// <summary>
        /// 两个结构一样的DT合并
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            DataTable newDataTable = dt1.Clone();
            object[] obj = new object[newDataTable.Columns.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }
            return newDataTable;
        }

        /// <summary>
        /// 输入Float格式数字，将其转换为货币表达方式
        /// </summary>
        /// <param name="ftype">货币表达类型：0=带￥的货币表达方式；1=不带￥的货币表达方式；其它=带￥的货币表达方式</param>
        /// <param name="fmoney">传入的int数字</param>
        /// <returns>返回转换的货币表达形式</returns>
        public static string FormatMoney(int type, double money)
        {
            string _money;
            try
            {
                switch (type)
                {
                    case 0:
                        _money = string.Format("{0:C2}", money);
                        break;

                    case 1:
                        _money = string.Format("{0:N2}", money);
                        break;

                    default:
                        _money = string.Format("{0:C2}", money);
                        break;
                }
            }
            catch
            {
                _money = "";
            }

            return _money;
        }
        /// <summary>
        /// 取随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="type">1= 数字 2=大写字母 3=数字字母</param>
        /// <returns></returns>
        public static string GetRandomCode(int len, string type)
        {
            string strLetters = "";
            if (type == "1")
            {
                strLetters = "1234567890";
            }
            else if (type == "2")
            {
                strLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else if (type == "3")
            {
                strLetters = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            else
            {
                strLetters = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            StringBuilder s = new StringBuilder();
            Random r = new Random();
            //将随机生成的字符串绘制到图片上
            for (int i = 0; i < len; i++)
            {
                s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
            }
            return s.ToString();
        }
    }
}
