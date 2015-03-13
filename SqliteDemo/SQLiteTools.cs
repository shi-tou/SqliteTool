using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using Dao;
using Model;
using System.IO;

namespace SqliteDemo
{
    public partial class SQLiteTools : BaseForm
    {
        public List<SQLiteTableSchema> listSchema = null;
        public string DataSource
        {
            get { return this.tbDataSource.Text; }
        }
        public SQLiteTools()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载、初始化数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SQLiteTools_Load(object sender, EventArgs e)
        {
            List<TextValue> listType = new List<TextValue>();
            listType.Add(new TextValue("INTEGER", SQLiteDataType.INTEGER));
            listType.Add(new TextValue("REAL", SQLiteDataType.REAL));
            listType.Add(new TextValue("TEXT", SQLiteDataType.TEXT));
            listType.Add(new TextValue("BLOB", SQLiteDataType.BLOB));
            this.DataType.DisplayMember = "Text";
            this.DataType.ValueMember = "Value";
            this.DataType.DataSource = listType;
            this.DataType.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

            this.tbDataSource.Text = SQLiteHelper.DataSource;
            listSchema = new List<SQLiteTableSchema>();
            SQLiteTableSchema info = new SQLiteTableSchema();
            info.FieldName = "ID";
            info.DataType = SQLiteDataType.INTEGER;
            info.Primary = true;
            info.AotoIncrement = true;
            info.Null = false;
            listSchema.Add(info);
            this.dgvTableSchema.DataSource = listSchema;
        }
        /// <summary>
        /// 执行添加表、查询、数据操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            string msg = "";
            int tabIndex = this.tabControl1.SelectedIndex;
            if (tabIndex == 0)
            {
                if (this.tbTableName.Text == "")
                {
                    MessageBox.Show("请输入表名");
                    return;
                }
                if (listSchema.Count == 0)
                {
                    MessageBox.Show("请设置表结构");
                    return;
                }

                string sql = "";
                string TableTemplate = "CREATE TABLE [{0}] ({1})";
                foreach (SQLiteTableSchema s in listSchema)
                {
                    if (sql != "")
                        sql += ",";
                    sql += s.ToString();
                }
                sql = string.Format(TableTemplate, this.tbTableName.Text, sql);
                try
                {
                    int res = SQLiteHelper.ExecuteNonQuery(DataSource, CommandType.Text, sql);
                }
                catch (Exception ex)
                {
                    this.rtbMessage.Text = ex.Message;
                    this.tabResult.SelectedIndex = 1;
                }
            }
            else if (tabIndex == 1)
            {
                if (textEditor_Search.Text == "")
                {
                    MessageBox.Show("请输入要执行的语句");
                    return;
                }
                try
                {
                    DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, this.textEditor_Search.Text).Tables[0];
                    this.dgvResult.DataSource = dt;
                    tabResult.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    rtbMessage.Text = ex.Message;
                    tabResult.SelectedIndex = 1;
                }
            }
            else if (tabIndex == 2)
            {
                if (textEditor_Effect.Text == "")
                {
                    MessageBox.Show("请输入要执行的语句");
                    return;
                }
                try
                {
                    int res = SQLiteHelper.ExecuteNonQuery(DataSource, CommandType.Text, this.textEditor_Effect.Text);
                    msg += "执行成功！";
                    msg += "\n" + res.ToString() + "行受影响。";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                rtbMessage.Text = msg;
                tabResult.SelectedIndex = 1;
            }
            else
            {
                tabResult.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            SQLiteTableSchema info = new SQLiteTableSchema();
            info.FieldName = "";
            info.DataType = SQLiteDataType.INTEGER;
            info.Primary = false;
            info.AotoIncrement = false;
            info.Null = false;
            info.DefaultValue = "";
            listSchema.Add(info);
            this.dgvTableSchema.DataSource = null;
            this.dgvTableSchema.DataSource = listSchema;
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvTableSchema.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请先选中要删除的行");
                    return;
                }
                listSchema.RemoveAt(this.dgvTableSchema.CurrentRow.Index);
                this.dgvTableSchema.DataSource = null;
                this.dgvTableSchema.DataSource = listSchema;
            }
            catch (Exception ex)
            {
                this.rtbMessage.Text = ex.Message;
                this.tabResult.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// tabcontrol切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.tabControl1.SelectedIndex;
            this.label2.Visible = (index == 0 ? true : false);
            this.tbTableName.Visible = (index == 0 ? true : false);
            this.btnGo.Text = (index == 0 ? "保存" : "执行语句");
            this.btnGo.Left = (index == 0 ? 195 : 20);
            this.btnGo.Visible = true;
            this.btnAddRow.Visible = (index == 0 ? true : false);
            this.btnDelRow.Visible = (index == 0 ? true : false);
            if (index == 3)
            {
                this.tbSelectTableSql.Text = "select name from sqlite_master where type='table' order by name;";
                this.tbSelectTableSql.Enabled = false;
                GetSqliteTable();
                this.btnGo.Visible = false;
            }
        }
        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (this.rbExsit.Checked)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.tbDataSource.Text = "Data Source=" + dialog.FileName;
                }
            }
            else
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.tbDataSource.Text = dlg.SelectedPath;
                }
            }
        }
        /// <summary>
        /// 获取表信息
        /// </summary>
        public void GetSqliteTable()
        {
            if (DataSource == "")
                return;
            this.cbAllTable.Items.Clear();
            if (this.tbSelectTableSql.Text == "")
                return;
            DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text,  this.tbSelectTableSql.Text).Tables[0];
            if (dt.Rows.Count == 0)
                return;
           
            foreach (DataRow dr in dt.Rows)
            {
                this.cbAllTable.Items.Add(dr["Name"].ToString());
            }
            this.tabResult.SelectedIndex = 0;
            this.cbAllTable.SelectedIndex = 0;
            GetTableSchema();
        }
        /// <summary>
        /// 表结构信息
        /// </summary>
        public void GetTableSchema()
        {
            if (DataSource == "")
                return;
            if (this.cbAllTable.Items.Count > 0)
            {
                string sql = "PRAGMA table_info([" + this.cbAllTable.SelectedItem.ToString() + "])";
                DataTable dt = SQLiteHelper.ExecuteDataset(DataSource, CommandType.Text, sql).Tables[0];
                this.dgvResult.DataSource = dt;
            }
        }
        /// <summary>
        /// 重新查询表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            GetSqliteTable();
        }
        /// <summary>
        /// 表结构信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbAllTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTableSchema();
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (this.tbDataSource.Text == "")
            {
                MessageBox.Show("请选择创建路径");
                return;
            }
            if (this.tbFileName.Text == "")
            {
                MessageBox.Show("请填写创建的文件名");
                return;
            }
            if (!Directory.Exists(DataSource))
            {
                Directory.CreateDirectory(DataSource);
            }
            string fileName = DataSource + this.tbFileName.Text + ".db";
            SQLiteHelper.CreateDataBase(fileName);
            this.rbExsit.Checked = true;
            this.tbFileName.Text = "";
            this.tbDataSource.Text = "Data Source=" + fileName;
            this.btnCreateFile.Visible = false;
            MessageBox.Show("创建完成，在下面执行相关操作");
        }

        private void radiio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbExsit.Checked)
            {
                this.btnCreateFile.Visible = false;
                label_Tip.Text = "选择文件：";
            }
            else
            {
                this.tbDataSource.Text = "";
                this.btnCreateFile.Visible = true;
                this.panel_New.Visible = true;
                label_Tip.Text = "保存路径：";
            }
        }
    }
}
