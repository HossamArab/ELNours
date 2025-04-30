namespace ELNour.Frm
{
    partial class frmVendors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendors));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteImporter = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditImporter = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewImporter = new DevExpress.XtraEditors.SimpleButton();
            this.btnShowAll = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dgvImporter = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporter)).BeginInit();
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
            this.groupControl1.Controls.Add(this.btnDeleteImporter);
            this.groupControl1.Controls.Add(this.btnEditImporter);
            this.groupControl1.Controls.Add(this.btnNewImporter);
            this.groupControl1.Controls.Add(this.btnShowAll);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1198, 126);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "بيانات المورد";
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
            this.btnPrint.Location = new System.Drawing.Point(76, 77);
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
            this.btnExportToExcel.Location = new System.Drawing.Point(76, 23);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(191, 33);
            this.btnExportToExcel.TabIndex = 55;
            this.btnExportToExcel.Tag = "Export";
            this.btnExportToExcel.Text = "استخراج أكسيل";
            this.btnExportToExcel.ToolTip = "تصدير البيانات الي اكسيل";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnDeleteImporter
            // 
            this.btnDeleteImporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteImporter.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteImporter.Appearance.Options.UseFont = true;
            this.btnDeleteImporter.Appearance.Options.UseTextOptions = true;
            this.btnDeleteImporter.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteImporter.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteImporter.AppearanceDisabled.Options.UseFont = true;
            this.btnDeleteImporter.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnDeleteImporter.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteImporter.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteImporter.AppearanceHovered.Options.UseFont = true;
            this.btnDeleteImporter.AppearanceHovered.Options.UseTextOptions = true;
            this.btnDeleteImporter.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteImporter.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnDeleteImporter.AppearancePressed.Options.UseFont = true;
            this.btnDeleteImporter.AppearancePressed.Options.UseTextOptions = true;
            this.btnDeleteImporter.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnDeleteImporter.ImageOptions.Image = global::ELNour.Properties.Resources.clear_32x32;
            this.btnDeleteImporter.Location = new System.Drawing.Point(310, 79);
            this.btnDeleteImporter.Name = "btnDeleteImporter";
            this.btnDeleteImporter.Size = new System.Drawing.Size(223, 33);
            this.btnDeleteImporter.TabIndex = 33;
            this.btnDeleteImporter.Tag = "Delete";
            this.btnDeleteImporter.Text = "حذف المورد المحدد";
            this.btnDeleteImporter.Click += new System.EventHandler(this.btnDeleteImporter_Click);
            // 
            // btnEditImporter
            // 
            this.btnEditImporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditImporter.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditImporter.Appearance.Options.UseFont = true;
            this.btnEditImporter.Appearance.Options.UseTextOptions = true;
            this.btnEditImporter.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditImporter.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditImporter.AppearanceDisabled.Options.UseFont = true;
            this.btnEditImporter.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnEditImporter.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditImporter.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditImporter.AppearanceHovered.Options.UseFont = true;
            this.btnEditImporter.AppearanceHovered.Options.UseTextOptions = true;
            this.btnEditImporter.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditImporter.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnEditImporter.AppearancePressed.Options.UseFont = true;
            this.btnEditImporter.AppearancePressed.Options.UseTextOptions = true;
            this.btnEditImporter.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnEditImporter.ImageOptions.Image = global::ELNour.Properties.Resources.edit_32x32;
            this.btnEditImporter.Location = new System.Drawing.Point(310, 25);
            this.btnEditImporter.Name = "btnEditImporter";
            this.btnEditImporter.Size = new System.Drawing.Size(223, 33);
            this.btnEditImporter.TabIndex = 32;
            this.btnEditImporter.Tag = "Edit";
            this.btnEditImporter.Text = "تعديل المورد المحدد";
            this.btnEditImporter.Click += new System.EventHandler(this.btnEditImporter_Click);
            // 
            // btnNewImporter
            // 
            this.btnNewImporter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewImporter.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewImporter.Appearance.Options.UseFont = true;
            this.btnNewImporter.Appearance.Options.UseTextOptions = true;
            this.btnNewImporter.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewImporter.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewImporter.AppearanceDisabled.Options.UseFont = true;
            this.btnNewImporter.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnNewImporter.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewImporter.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewImporter.AppearanceHovered.Options.UseFont = true;
            this.btnNewImporter.AppearanceHovered.Options.UseTextOptions = true;
            this.btnNewImporter.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewImporter.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnNewImporter.AppearancePressed.Options.UseFont = true;
            this.btnNewImporter.AppearancePressed.Options.UseTextOptions = true;
            this.btnNewImporter.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnNewImporter.ImageOptions.Image = global::ELNour.Properties.Resources.add_32x32;
            this.btnNewImporter.Location = new System.Drawing.Point(574, 77);
            this.btnNewImporter.Name = "btnNewImporter";
            this.btnNewImporter.Size = new System.Drawing.Size(191, 33);
            this.btnNewImporter.TabIndex = 31;
            this.btnNewImporter.Tag = "Add";
            this.btnNewImporter.Text = "مورد جديد";
            this.btnNewImporter.Click += new System.EventHandler(this.btnNewImporter_Click);
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
            this.btnShowAll.Location = new System.Drawing.Point(574, 25);
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
            this.txtName.Location = new System.Drawing.Point(848, 50);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtName.Size = new System.Drawing.Size(291, 26);
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
            this.groupControl2.Controls.Add(this.dgvImporter);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 126);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1198, 494);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "الموردين";
            // 
            // dgvImporter
            // 
            this.dgvImporter.AllowUserToAddRows = false;
            this.dgvImporter.AllowUserToDeleteRows = false;
            this.dgvImporter.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(1)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(92)))), ((int)(((byte)(1)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImporter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvImporter.ColumnHeadersHeight = 32;
            this.dgvImporter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvImporter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImporter.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvImporter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImporter.EnableHeadersVisualStyles = false;
            this.dgvImporter.Location = new System.Drawing.Point(2, 22);
            this.dgvImporter.Name = "dgvImporter";
            this.dgvImporter.ReadOnly = true;
            this.dgvImporter.RowHeadersVisible = false;
            this.dgvImporter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvImporter.RowTemplate.Height = 35;
            this.dgvImporter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImporter.Size = new System.Drawing.Size(1194, 470);
            this.dgvImporter.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "رقم المورد";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "اسم المورد";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 550;
            // 
            // frmVendors
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 620);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmVendors.IconOptions.Icon")));
            this.KeyPreview = true;
            this.Name = "frmVendors";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الموردين";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImporter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.SimpleButton btnDeleteImporter;
        private DevExpress.XtraEditors.SimpleButton btnEditImporter;
        private DevExpress.XtraEditors.SimpleButton btnNewImporter;
        private DevExpress.XtraEditors.SimpleButton btnShowAll;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        internal System.Windows.Forms.DataGridView dgvImporter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}