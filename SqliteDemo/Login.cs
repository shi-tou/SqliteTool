using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using SqliteDemo.Sys;

namespace SqliteDemo
{
    public partial class Login : BaseForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.tbUserName.Focus();
            List<TextValue> list = new List<TextValue>();
            list.Add(new TextValue("主界面", 1));
            list.Add(new TextValue("SQLite工具", 2));
            BindDropdownList(this.cbType, list);
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
            int res = userService.UserLogin(this.tbUserName.Text, this.tbPassword.Text);
            if (res == Result.RESULT_NOT_EXIST)
            {
                MessageBox.Show("用户不存在");
                return;
            }
            if (res == Result.RESULT_ERROR_PASSWORD)
            {
                MessageBox.Show("密码错误");
                return;
            }
            if (res == Result.RESULT_SUCCESS)
            {
                if (this.cbType.SelectedValue.ToString() == "1")
                {
                    UserList user = new UserList();
                    user.StartPosition = FormStartPosition.CenterScreen;
                    user.Show();
                }
                else
                {
                    SQLiteTools tool = new SQLiteTools();
                    tool.StartPosition = FormStartPosition.CenterScreen;
                    tool.Show();
                }
                this.Hide();
            }
        }
        
    }
}
