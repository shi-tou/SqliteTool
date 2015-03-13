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

namespace SqliteDemo
{
    public partial class BaseForm : Form
    {
        /// <summary>
        /// 注入
        /// </summary>
        public IUserService userService
        {
            get { return IocContainer<UserService>.GetInstance(); }
        }

        public BaseForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }



        #region 公共
        public void BindDropdownList(ComboBox cb,List<TextValue> list)
        {
            cb.Items.Clear();
            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";
            cb.DataSource = list;
            cb.SelectedIndex = 0;
            
        }
        #endregion
    }
}
