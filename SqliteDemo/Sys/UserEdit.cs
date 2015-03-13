using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqliteDemo.Sys
{
    public partial class UserEdit : BaseForm
    {
        /// <summary>
        /// 声明委托(添加用户成功)
        /// </summary>
        /// <param name="message"></param>
        public delegate void UserEditSuccessDelegate();
        /// <summary>
        /// //委托对象
        /// </summary>
        public UserEditSuccessDelegate UserEditSuccess;
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID = 0;
        /// <summary>
        /// 构造
        /// </summary>
        public UserEdit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserEdit_Load(object sender, EventArgs e)
        {
            if (ID != 0)
                BindUserInfo();
            this.cbSex.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定会员信息
        /// </summary>
        public void BindUserInfo()
        {
            DataTable dt = userService.GetDataByKey("T_User", "ID", ID);
            if (dt.Rows.Count == 0)
                return;
            DataRow dr = dt.Rows[0];
            this.tbUserName.Text = dr["UserName"].ToString();
            this.tbPassword.Text = dr["Password"].ToString();
            this.tbName.Text = dr["Name"].ToString();
            this.cbSex.SelectedValue = dr["Sex"].ToString()=="M"?"男":"女";
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbUserName.Text == "")
            {
                this.tbUserName.Focus();
                return;
            }
            if (this.tbPassword.Text == "")
            {
                this.tbPassword.Focus();
                return;
            }
            if (this.tbName.Text == "")
            {
                this.tbName.Focus();
                return;
            }

            DataTable dt = userService.GetDataByKey("T_User", "ID", ID);
            DataRow dr = null;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
            }
            dr["UserName"] = this.tbUserName.Text;
            dr["Password"] = this.tbPassword.Text;
            dr["Name"] = this.tbName.Text;
            dr["Sex"] = this.cbSex.SelectedIndex == 0 ? "M" : "F";
            dr["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int res = 0;
            if (ID != 0)
            {
                res = userService.Update(dt, "ID");
            }
            else
            {
                res = userService.Insert(dt);
            }
            if (res > 0)
            {
                MessageBox.Show("操作成功！");
                this.Close();
                if (UserEditSuccess != null)
                {
                    UserEditSuccess();
                }
            }
            else
            {
                MessageBox.Show("操作失败！");
            }
           
        }
    }
}
