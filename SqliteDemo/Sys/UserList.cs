using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Service;
using Common;
using System.Collections;

namespace SqliteDemo.Sys
{
    

    public partial class UserList : BaseForm
    {
        /// <summary>
        /// 构造
        /// </summary>
        public UserList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserList_Load(object sender, EventArgs e)
        {
            BindUserList();
        }
        /// <summary>
        /// 绑定用户列表
        /// </summary>
        public void BindUserList()
        {
            Hashtable hs = new Hashtable();
            if (this.tbUserName.Text != "")
                hs.Add("UserName",this.tbUserName.Text);
            Pager p = new Pager(10, 1, "CreateTime");
            userService.GetUserList(p, hs);
            this.dataGridView1.DataSource = p.DataSource;
        }

        private void ToolStripLabel_Clicked(object sender, EventArgs e)
        {
            ToolStripLabel label = (ToolStripLabel)sender;
            UserEdit userEdit = new UserEdit();
            userEdit.UserEditSuccess = BindUserList;
            switch (label.Name)
            {
                case "add":
                    userEdit.ShowDialog();
                    break;
                case "edit":
                    if (this.dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("请选择要编辑的记录！");
                        return;
                    }
                    userEdit.ID = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    userEdit.ShowDialog();
                    break;
                case "del":
                    if (this.dataGridView1.SelectedRows.Count == 0)
                    {
                        MessageBox.Show("请选择要删除的记录！");
                        return;
                    }
                    if (MessageBox.Show("您确认要删除吗吗？", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        int ID = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        int res = userService.Delete("T_User", "ID", ID);
                        if (res > 0)
                        {
                            MessageBox.Show("操作成功");
                            BindUserList();
                        }
                        else
                        {
                            MessageBox.Show("操作失败");
                        }
                    }
                    break;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindUserList();
        }
    }
}
