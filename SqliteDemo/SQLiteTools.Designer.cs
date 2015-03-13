namespace SqliteDemo
{
    partial class SQLiteTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLiteTools));
            this.txtNotice = new ICSharpCode.TextEditor.TextEditorControl();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.imageSmall = new System.Windows.Forms.ImageList(this.components);
            this.pageNotice = new System.Windows.Forms.TabPage();
            this.rtbMessage = new System.Windows.Forms.RichTextBox();
            this.panResult = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTableName = new System.Windows.Forms.TextBox();
            this.btnDelRow = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvTableSchema = new System.Windows.Forms.DataGridView();
            this.FieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Primary = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AotoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Null = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textEditor_Search = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textEditor_Effect = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbAllTable = new System.Windows.Forms.ComboBox();
            this.btnSelectTable = new System.Windows.Forms.Button();
            this.tbSelectTableSql = new System.Windows.Forms.TextBox();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.pageGrid = new System.Windows.Forms.TabPage();
            this.pageMessage = new System.Windows.Forms.TabPage();
            this.panTop = new System.Windows.Forms.Panel();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label_Tip = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbExsit = new System.Windows.Forms.RadioButton();
            this.btnCreateFile = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tbDataSource = new System.Windows.Forms.TextBox();
            this.panel_New = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.pageNotice.SuspendLayout();
            this.panResult.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableSchema)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.pageGrid.SuspendLayout();
            this.pageMessage.SuspendLayout();
            this.panTop.SuspendLayout();
            this.panel_New.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNotice
            // 
            this.txtNotice.AllowCaretBeyondEOL = true;
            this.txtNotice.AllowDrop = true;
            this.txtNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotice.Encoding = ((System.Text.Encoding)(resources.GetObject("txtNotice.Encoding")));
            this.txtNotice.Location = new System.Drawing.Point(3, 3);
            this.txtNotice.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.ShowEOLMarkers = true;
            this.txtNotice.ShowInvalidLines = false;
            this.txtNotice.ShowSpaces = true;
            this.txtNotice.ShowTabs = true;
            this.txtNotice.ShowVRuler = true;
            this.txtNotice.Size = new System.Drawing.Size(647, 151);
            this.txtNotice.TabIndex = 1;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.Color.White;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 4);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(647, 149);
            this.dgvResult.TabIndex = 0;
            // 
            // imageSmall
            // 
            this.imageSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageSmall.ImageStream")));
            this.imageSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageSmall.Images.SetKeyName(0, "ResultNonQuery.png");
            this.imageSmall.Images.SetKeyName(1, "ResultGridView.png");
            this.imageSmall.Images.SetKeyName(2, "ResultNotice.png");
            this.imageSmall.Images.SetKeyName(3, "add.gif");
            this.imageSmall.Images.SetKeyName(4, "effect.gif");
            this.imageSmall.Images.SetKeyName(5, "search.gif");
            // 
            // pageNotice
            // 
            this.pageNotice.Controls.Add(this.txtNotice);
            this.pageNotice.ImageKey = "ResultNotice.png";
            this.pageNotice.Location = new System.Drawing.Point(4, 23);
            this.pageNotice.Name = "pageNotice";
            this.pageNotice.Padding = new System.Windows.Forms.Padding(3);
            this.pageNotice.Size = new System.Drawing.Size(653, 157);
            this.pageNotice.TabIndex = 2;
            this.pageNotice.Text = "注意事项";
            this.pageNotice.UseVisualStyleBackColor = true;
            // 
            // rtbMessage
            // 
            this.rtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbMessage.Location = new System.Drawing.Point(3, 4);
            this.rtbMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtbMessage.Name = "rtbMessage";
            this.rtbMessage.Size = new System.Drawing.Size(647, 149);
            this.rtbMessage.TabIndex = 0;
            this.rtbMessage.Text = "";
            // 
            // panResult
            // 
            this.panResult.Controls.Add(this.label2);
            this.panResult.Controls.Add(this.tbTableName);
            this.panResult.Controls.Add(this.btnDelRow);
            this.panResult.Controls.Add(this.btnAddRow);
            this.panResult.Controls.Add(this.btnGo);
            this.panResult.Controls.Add(this.tabControl1);
            this.panResult.Controls.Add(this.tabResult);
            this.panResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panResult.Location = new System.Drawing.Point(0, 0);
            this.panResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panResult.Name = "panResult";
            this.panResult.Size = new System.Drawing.Size(661, 520);
            this.panResult.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "表名：";
            // 
            // tbTableName
            // 
            this.tbTableName.Location = new System.Drawing.Point(64, 307);
            this.tbTableName.Name = "tbTableName";
            this.tbTableName.Size = new System.Drawing.Size(122, 21);
            this.tbTableName.TabIndex = 11;
            // 
            // btnDelRow
            // 
            this.btnDelRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelRow.Location = new System.Drawing.Point(361, 305);
            this.btnDelRow.Name = "btnDelRow";
            this.btnDelRow.Size = new System.Drawing.Size(75, 25);
            this.btnDelRow.TabIndex = 10;
            this.btnDelRow.Text = "删除行";
            this.btnDelRow.UseVisualStyleBackColor = true;
            this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRow.ImageKey = "(无)";
            this.btnAddRow.Location = new System.Drawing.Point(277, 305);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 25);
            this.btnAddRow.TabIndex = 9;
            this.btnAddRow.Text = "添加行";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(195, 305);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(73, 25);
            this.btnGo.TabIndex = 8;
            this.btnGo.Text = "保存";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.ImageList = this.imageSmall;
            this.tabControl1.Location = new System.Drawing.Point(0, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(661, 222);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTableSchema);
            this.tabPage1.ImageKey = "add.gif";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(653, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "创建表";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvTableSchema
            // 
            this.dgvTableSchema.BackgroundColor = System.Drawing.Color.White;
            this.dgvTableSchema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableSchema.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FieldName,
            this.DataType,
            this.Primary,
            this.AotoIncrement,
            this.Null,
            this.DefaultValue});
            this.dgvTableSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTableSchema.Location = new System.Drawing.Point(3, 3);
            this.dgvTableSchema.Name = "dgvTableSchema";
            this.dgvTableSchema.RowTemplate.Height = 23;
            this.dgvTableSchema.Size = new System.Drawing.Size(647, 189);
            this.dgvTableSchema.TabIndex = 1;
            // 
            // FieldName
            // 
            this.FieldName.DataPropertyName = "FieldName";
            this.FieldName.HeaderText = "列名";
            this.FieldName.Name = "FieldName";
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.DataType.HeaderText = "数据类型";
            this.DataType.Items.AddRange(new object[] {
            "INTEGER",
            "REAL",
            "TEXT",
            "BLOB"});
            this.DataType.Name = "DataType";
            // 
            // Primary
            // 
            this.Primary.DataPropertyName = "Primary";
            this.Primary.HeaderText = "主键";
            this.Primary.Name = "Primary";
            this.Primary.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // AotoIncrement
            // 
            this.AotoIncrement.DataPropertyName = "AotoIncrement";
            this.AotoIncrement.HeaderText = "自增";
            this.AotoIncrement.Name = "AotoIncrement";
            this.AotoIncrement.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Null
            // 
            this.Null.DataPropertyName = "Null";
            this.Null.HeaderText = "允许Null值";
            this.Null.Name = "Null";
            this.Null.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DefaultValue
            // 
            this.DefaultValue.DataPropertyName = "DefaultValue";
            this.DefaultValue.HeaderText = "默认值";
            this.DefaultValue.Name = "DefaultValue";
            this.DefaultValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DefaultValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textEditor_Search);
            this.tabPage2.ImageKey = "search.gif";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(653, 195);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "查询";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textEditor_Search
            // 
            this.textEditor_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor_Search.Encoding = ((System.Text.Encoding)(resources.GetObject("textEditor_Search.Encoding")));
            this.textEditor_Search.Location = new System.Drawing.Point(3, 3);
            this.textEditor_Search.Name = "textEditor_Search";
            this.textEditor_Search.ShowEOLMarkers = true;
            this.textEditor_Search.ShowInvalidLines = false;
            this.textEditor_Search.ShowSpaces = true;
            this.textEditor_Search.ShowTabs = true;
            this.textEditor_Search.ShowVRuler = true;
            this.textEditor_Search.Size = new System.Drawing.Size(647, 189);
            this.textEditor_Search.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textEditor_Effect);
            this.tabPage3.ImageKey = "effect.gif";
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(653, 195);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "影响";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textEditor_Effect
            // 
            this.textEditor_Effect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor_Effect.Encoding = ((System.Text.Encoding)(resources.GetObject("textEditor_Effect.Encoding")));
            this.textEditor_Effect.Location = new System.Drawing.Point(3, 3);
            this.textEditor_Effect.Name = "textEditor_Effect";
            this.textEditor_Effect.ShowEOLMarkers = true;
            this.textEditor_Effect.ShowInvalidLines = false;
            this.textEditor_Effect.ShowSpaces = true;
            this.textEditor_Effect.ShowTabs = true;
            this.textEditor_Effect.ShowVRuler = true;
            this.textEditor_Effect.Size = new System.Drawing.Size(647, 189);
            this.textEditor_Effect.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.cbAllTable);
            this.tabPage4.Controls.Add(this.btnSelectTable);
            this.tabPage4.Controls.Add(this.tbSelectTableSql);
            this.tabPage4.ImageKey = "ResultGridView.png";
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(653, 195);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "数据库信息";
            // 
            // cbAllTable
            // 
            this.cbAllTable.FormattingEnabled = true;
            this.cbAllTable.Location = new System.Drawing.Point(148, 105);
            this.cbAllTable.Name = "cbAllTable";
            this.cbAllTable.Size = new System.Drawing.Size(121, 20);
            this.cbAllTable.TabIndex = 2;
            this.cbAllTable.SelectedIndexChanged += new System.EventHandler(this.cbAllTable_SelectedIndexChanged);
            // 
            // btnSelectTable
            // 
            this.btnSelectTable.Location = new System.Drawing.Point(42, 104);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTable.TabIndex = 1;
            this.btnSelectTable.Text = "查询所有表";
            this.btnSelectTable.UseVisualStyleBackColor = true;
            this.btnSelectTable.Click += new System.EventHandler(this.btnSelectTable_Click);
            // 
            // tbSelectTableSql
            // 
            this.tbSelectTableSql.Location = new System.Drawing.Point(37, 26);
            this.tbSelectTableSql.Multiline = true;
            this.tbSelectTableSql.Name = "tbSelectTableSql";
            this.tbSelectTableSql.Size = new System.Drawing.Size(266, 62);
            this.tbSelectTableSql.TabIndex = 0;
            // 
            // tabResult
            // 
            this.tabResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabResult.Controls.Add(this.pageGrid);
            this.tabResult.Controls.Add(this.pageMessage);
            this.tabResult.Controls.Add(this.pageNotice);
            this.tabResult.ImageList = this.imageSmall;
            this.tabResult.Location = new System.Drawing.Point(0, 336);
            this.tabResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(661, 184);
            this.tabResult.TabIndex = 0;
            // 
            // pageGrid
            // 
            this.pageGrid.Controls.Add(this.dgvResult);
            this.pageGrid.ImageKey = "ResultGridView.png";
            this.pageGrid.Location = new System.Drawing.Point(4, 23);
            this.pageGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageGrid.Name = "pageGrid";
            this.pageGrid.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageGrid.Size = new System.Drawing.Size(653, 157);
            this.pageGrid.TabIndex = 0;
            this.pageGrid.Text = "结果";
            this.pageGrid.UseVisualStyleBackColor = true;
            // 
            // pageMessage
            // 
            this.pageMessage.Controls.Add(this.rtbMessage);
            this.pageMessage.ImageKey = "ResultNonQuery.png";
            this.pageMessage.Location = new System.Drawing.Point(4, 23);
            this.pageMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageMessage.Name = "pageMessage";
            this.pageMessage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pageMessage.Size = new System.Drawing.Size(653, 157);
            this.pageMessage.TabIndex = 1;
            this.pageMessage.Text = "消息";
            this.pageMessage.UseVisualStyleBackColor = true;
            // 
            // panTop
            // 
            this.panTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panTop.Controls.Add(this.panel_New);
            this.panTop.Controls.Add(this.label_Tip);
            this.panTop.Controls.Add(this.rbNew);
            this.panTop.Controls.Add(this.rbExsit);
            this.panTop.Controls.Add(this.btnCreateFile);
            this.panTop.Controls.Add(this.btnSelectFile);
            this.panTop.Controls.Add(this.tbDataSource);
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Margin = new System.Windows.Forms.Padding(4);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(661, 70);
            this.panTop.TabIndex = 7;
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.Location = new System.Drawing.Point(77, 5);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(73, 21);
            this.tbFileName.TabIndex = 5;
            // 
            // label_Tip
            // 
            this.label_Tip.AutoSize = true;
            this.label_Tip.Location = new System.Drawing.Point(17, 41);
            this.label_Tip.Name = "label_Tip";
            this.label_Tip.Size = new System.Drawing.Size(65, 12);
            this.label_Tip.TabIndex = 11;
            this.label_Tip.Text = "选择文件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(152, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = ".db";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "文件名：";
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Location = new System.Drawing.Point(134, 13);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(83, 16);
            this.rbNew.TabIndex = 10;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "创建新文件";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.radiio_CheckedChanged);
            // 
            // rbExsit
            // 
            this.rbExsit.AutoSize = true;
            this.rbExsit.Checked = true;
            this.rbExsit.Location = new System.Drawing.Point(15, 13);
            this.rbExsit.Name = "rbExsit";
            this.rbExsit.Size = new System.Drawing.Size(107, 16);
            this.rbExsit.TabIndex = 10;
            this.rbExsit.TabStop = true;
            this.rbExsit.Text = "选择已存在文件";
            this.rbExsit.UseVisualStyleBackColor = true;
            this.rbExsit.CheckedChanged += new System.EventHandler(this.radiio_CheckedChanged);
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.Location = new System.Drawing.Point(563, 36);
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(81, 23);
            this.btnCreateFile.TabIndex = 9;
            this.btnCreateFile.Text = "创建";
            this.btnCreateFile.UseVisualStyleBackColor = true;
            this.btnCreateFile.Visible = false;
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(476, 36);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(81, 23);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "浏览...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tbDataSource
            // 
            this.tbDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDataSource.Location = new System.Drawing.Point(87, 37);
            this.tbDataSource.Name = "tbDataSource";
            this.tbDataSource.Size = new System.Drawing.Size(385, 21);
            this.tbDataSource.TabIndex = 5;
            // 
            // panel_New
            // 
            this.panel_New.Controls.Add(this.tbFileName);
            this.panel_New.Controls.Add(this.label1);
            this.panel_New.Controls.Add(this.label3);
            this.panel_New.Location = new System.Drawing.Point(275, 4);
            this.panel_New.Name = "panel_New";
            this.panel_New.Size = new System.Drawing.Size(200, 31);
            this.panel_New.TabIndex = 2;
            this.panel_New.Visible = false;
            // 
            // SQLiteTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 520);
            this.Controls.Add(this.panTop);
            this.Controls.Add(this.panResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SQLiteTools";
            this.Text = "SQLite辅助工具-小杨精品制作";
            this.Load += new System.EventHandler(this.SQLiteTools_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.pageNotice.ResumeLayout(false);
            this.panResult.ResumeLayout(false);
            this.panResult.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableSchema)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.pageGrid.ResumeLayout(false);
            this.pageMessage.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panel_New.ResumeLayout(false);
            this.panel_New.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl txtNotice;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.ImageList imageSmall;
        private System.Windows.Forms.TabPage pageNotice;
        private System.Windows.Forms.RichTextBox rtbMessage;
        private System.Windows.Forms.Panel panResult;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage pageGrid;
        private System.Windows.Forms.TabPage pageMessage;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbDataSource;
        private ICSharpCode.TextEditor.TextEditorControl textEditor_Search;
        private ICSharpCode.TextEditor.TextEditorControl textEditor_Effect;
        private System.Windows.Forms.DataGridView dgvTableSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldName;
        private System.Windows.Forms.DataGridViewComboBoxColumn DataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Primary;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AotoIncrement;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Null;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTableName;
        private System.Windows.Forms.Button btnDelRow;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox cbAllTable;
        private System.Windows.Forms.Button btnSelectTable;
        private System.Windows.Forms.TextBox tbSelectTableSql;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbExsit;
        private System.Windows.Forms.Button btnCreateFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label_Tip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_New;
    }
}