namespace ELNour.Frm
{
    partial class frmUser
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewUser = new DevExpress.XtraEditors.SimpleButton();
            this.btnShowAll = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Appearance.Options.UseTextOptions = true;
            this.groupControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl1.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.btnExportToExcel);
            this.groupControl1.Controls.Add(this.btnDeleteUser);
            this.groupControl1.Controls.Add(this.btnEditUser);
            this.groupControl1.Controls.Add(this.btnNewUser);
            this.groupControl1.Controls.Add(this.btnShowAll);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1198, 126);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "بيانات المستخدم";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Appearance.Options.UseTextOptions = true;
            this.btnPrint.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnPrint.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.AppearanceDisabled.Options.UseFont = true;
            this.btnPrint.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnPrint.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnPrint.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.AppearanceHovered.Options.UseFont = true;
            this.btnPrint.AppearanceHovered.Options.UseTextOptions = true;
            this.btnPrint.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnPrint.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.AppearancePressed.Options.UseFont = true;
            this.btnPrint.AppearancePressed.Options.UseTextOptions = true;
            this.btnPrint.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnPrint.ImageOptions.Image = global::ELNour.Properties.Resources.printer_32x32;
            this.btnPrint.Location = new System.Drawing.Point(27, 79);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(191, 33);
            this.btnPrint.TabIndex = 56;
            this.btnPrint.Tag = "Print";
            this.btnPrint.Text = "طباعة القائمة";
            this.btnPrint.ToolTip = "تصدير البيانات الي اكسيل";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportToExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToExcel.Appearance.Options.UseFont = true;
            this.btnExportToExcel.Appearance.Options.UseTextOptions = true;
            this.btnExportToExcel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnExportToExcel.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.AppearanceDisabled.Options.UseFont = true;
            this.btnExportToExcel.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnExportToExcel.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnExportToExcel.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.AppearanceHovered.Options.UseFont = true;
            this.btnExportToExcel.AppearanceHovered.Options.UseTextOptions = true;
            this.btnExportToExcel.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnExportToExcel.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnExportToExcel.AppearancePressed.Options.UseFont = true;
            this.btnExportToExcel.AppearancePressed.Options.UseTextOptions = true;
            this.btnExportToExcel.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnExportToExcel.ImageOptions.Image = global::ELNour.Properties.Resources.exporttoxls_32x32;
            this.btnExportToExcel.Location = new System.Drawing.Point(27, 25);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(191, 33);
            this.btnExportToExcel.TabIndex = 55;
            this.btnExportToExcel.Tag = "Export";
            this.btnExportToExcel.Text = "استخراج أكسيل";
            this.btnExportToExcel.ToolTip = "تصدير البيانات الي اكسيل";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteUser.Appearance.Options.UseFont = true;
            this.btnDeleteUser.Appearance.Options.UseTextOptions = true;
            this.btnDeleteUser.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteUser.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteUser.AppearanceDisabled.Options.UseFont = true;
            this.btnDeleteUser.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnDeleteUser.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteUser.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteUser.AppearanceHovered.Options.UseFont = true;
            this.btnDeleteUser.AppearanceHovered.Options.UseTextOptions = true;
            this.btnDeleteUser.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteUser.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteUser.AppearancePressed.Options.UseFont = true;
            this.btnDeleteUser.AppearancePressed.Options.UseTextOptions = true;
            this.btnDeleteUser.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteUser.ImageOptions.Image = global::ELNour.Properties.Resources.clear_32x32;
            this.btnDeleteUser.Location = new System.Drawing.Point(273, 77);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(247, 33);
            this.btnDeleteUser.TabIndex = 33;
            this.btnDeleteUser.Tag = "Delete";
            this.btnDeleteUser.Text = "حذف المستخدم المحدد";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditUser.Appearance.Options.UseFont = true;
            this.btnEditUser.Appearance.Options.UseTextOptions = true;
            this.btnEditUser.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditUser.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditUser.AppearanceDisabled.Options.UseFont = true;
            this.btnEditUser.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnEditUser.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditUser.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditUser.AppearanceHovered.Options.UseFont = true;
            this.btnEditUser.AppearanceHovered.Options.UseTextOptions = true;
            this.btnEditUser.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditUser.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditUser.AppearancePressed.Options.UseFont = true;
            this.btnEditUser.AppearancePressed.Options.UseTextOptions = true;
            this.btnEditUser.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditUser.ImageOptions.Image = global::ELNour.Properties.Resources.edit_32x32;
            this.btnEditUser.Location = new System.Drawing.Point(273, 23);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(247, 33);
            this.btnEditUser.TabIndex = 32;
            this.btnEditUser.Tag = "Edit";
            this.btnEditUser.Text = "تعديل المستخدم المحدد";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnNewUser
            // 
            this.btnNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewUser.Appearance.Options.UseFont = true;
            this.btnNewUser.Appearance.Options.UseTextOptions = true;
            this.btnNewUser.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewUser.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewUser.AppearanceDisabled.Options.UseFont = true;
            this.btnNewUser.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnNewUser.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewUser.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewUser.AppearanceHovered.Options.UseFont = true;
            this.btnNewUser.AppearanceHovered.Options.UseTextOptions = true;
            this.btnNewUser.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewUser.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewUser.AppearancePressed.Options.UseFont = true;
            this.btnNewUser.AppearancePressed.Options.UseTextOptions = true;
            this.btnNewUser.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewUser.ImageOptions.Image = global::ELNour.Properties.Resources.add_32x32;
            this.btnNewUser.Location = new System.Drawing.Point(577, 77);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(191, 33);
            this.btnNewUser.TabIndex = 31;
            this.btnNewUser.Tag = "Add";
            this.btnNewUser.Text = "مستخدم جديد";
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.Appearance.Options.UseFont = true;
            this.btnShowAll.Appearance.Options.UseTextOptions = true;
            this.btnShowAll.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnShowAll.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnShowAll.AppearanceDisabled.Options.UseFont = true;
            this.btnShowAll.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnShowAll.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnShowAll.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnShowAll.AppearanceHovered.Options.UseFont = true;
            this.btnShowAll.AppearanceHovered.Options.UseTextOptions = true;
            this.btnShowAll.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnShowAll.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnShowAll.AppearancePressed.Options.UseFont = true;
            this.btnShowAll.AppearancePressed.Options.UseTextOptions = true;
            this.btnShowAll.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnShowAll.ImageOptions.Image = global::ELNour.Properties.Resources.show_32x32;
            this.btnShowAll.Location = new System.Drawing.Point(577, 25);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(191, 33);
            this.btnShowAll.TabIndex = 30;
            this.btnShowAll.Tag = "Show";
            this.btnShowAll.Text = "عرض الكل";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearanceDisabled.Options.UseFont = true;
            this.labelControl7.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearanceHovered.Options.UseFont = true;
            this.labelControl7.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelControl7.AppearancePressed.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(1145, 54);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 18);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "الاسم";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(861, 50);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtName.Size = new System.Drawing.Size(278, 26);
            this.txtName.TabIndex = 13;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.Appearance.Options.UseTextOptions = true;
            this.groupControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl2.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl2.Controls.Add(this.dgvUser);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 126);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1198, 494);
            this.groupControl2.TabIndex = 14;
            this.groupControl2.Text = "المستخدمين";
            // 
            // dgvUser
            // 
            this.dgvUser.AllowUserToAddRows = false;
            this.dgvUser.AllowUserToDeleteRows = false;
            this.dgvUser.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(1)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(1)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUser.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUser.ColumnHeadersHeight = 32;
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUser.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.EnableHeadersVisualStyles = false;
            this.dgvUser.Location = new System.Drawing.Point(2, 22);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.ReadOnly = true;
            this.dgvUser.RowHeadersVisible = false;
            this.dgvUser.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUser.RowTemplate.Height = 35;
            this.dgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUser.Size = new System.Drawing.Size(1194, 470);
            this.dgvUser.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "رقم المستخدم";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "الاسم بالكامل";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 550;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "اسم المستخدم";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 250;
            // 
            // frmUser
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 620);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmUser.IconOptions.Icon")));
            this.Name = "frmUser";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "المستخدمين";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.SimpleButton btnDeleteUser;
        private DevExpress.XtraEditors.SimpleButton btnEditUser;
        private DevExpress.XtraEditors.SimpleButton btnNewUser;
        private DevExpress.XtraEditors.SimpleButton btnShowAll;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        internal System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}