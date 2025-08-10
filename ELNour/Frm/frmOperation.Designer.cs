namespace ELNour.Frm
{
    partial class frmOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperation));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblMainWeight = new System.Windows.Forms.Label();
            this.tsPlatteWeight = new DevExpress.XtraEditors.ToggleSwitch();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtBoxWeight = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtQA = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCountBoxes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbVendor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxesWeight = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPlateWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblNetWeight = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalDiscount = new System.Windows.Forms.TextBox();
            this.txtDiscountWeight = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsPlatteWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblMainWeight);
            this.panelControl1.Controls.Add(this.tsPlatteWeight);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(736, 84);
            this.panelControl1.TabIndex = 0;
            // 
            // lblMainWeight
            // 
            this.lblMainWeight.BackColor = System.Drawing.Color.Black;
            this.lblMainWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblMainWeight.Font = new System.Drawing.Font("Stencil", 36.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainWeight.ForeColor = System.Drawing.Color.Lime;
            this.lblMainWeight.Location = new System.Drawing.Point(2, 2);
            this.lblMainWeight.Name = "lblMainWeight";
            this.lblMainWeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblMainWeight.Size = new System.Drawing.Size(506, 80);
            this.lblMainWeight.TabIndex = 2;
            this.lblMainWeight.Text = "0.00";
            this.lblMainWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMainWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // tsPlatteWeight
            // 
            this.tsPlatteWeight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tsPlatteWeight.EditValue = true;
            this.tsPlatteWeight.Location = new System.Drawing.Point(508, 2);
            this.tsPlatteWeight.Name = "tsPlatteWeight";
            this.tsPlatteWeight.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsPlatteWeight.Properties.Appearance.Options.UseFont = true;
            this.tsPlatteWeight.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tsPlatteWeight.Properties.EditorToThumbWidthRatio = 2F;
            this.tsPlatteWeight.Properties.EditValueChangedDelay = 2;
            this.tsPlatteWeight.Properties.OffText = "بدون باليته";
            this.tsPlatteWeight.Properties.OnText = "خصم وزن الباليته";
            this.tsPlatteWeight.Size = new System.Drawing.Size(226, 80);
            this.tsPlatteWeight.TabIndex = 0;
            this.tsPlatteWeight.Toggled += new System.EventHandler(this.tsPlatteWeight_Toggled);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 84);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(736, 171);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl1.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl1.Controls.Add(this.txtBoxWeight);
            this.groupControl1.Controls.Add(this.label13);
            this.groupControl1.Controls.Add(this.txtUser);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.txtQA);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.txtNotes);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtCountBoxes);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.cmbItems);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.cmbVendor);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(732, 167);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "بيانات الوزنة";
            // 
            // txtBoxWeight
            // 
            this.txtBoxWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxWeight.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxWeight.Location = new System.Drawing.Point(10, 57);
            this.txtBoxWeight.Name = "txtBoxWeight";
            this.txtBoxWeight.Size = new System.Drawing.Size(258, 26);
            this.txtBoxWeight.TabIndex = 25;
            this.txtBoxWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            this.txtBoxWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpress);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(274, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 18);
            this.label13.TabIndex = 24;
            this.label13.Text = "وزن الصندوق";
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(10, 89);
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(258, 26);
            this.txtUser.TabIndex = 23;
            this.txtUser.Text = "Admin";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(295, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 22;
            this.label8.Text = "المستخدم";
            // 
            // txtQA
            // 
            this.txtQA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQA.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQA.Location = new System.Drawing.Point(398, 89);
            this.txtQA.Name = "txtQA";
            this.txtQA.Size = new System.Drawing.Size(238, 26);
            this.txtQA.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(675, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "الجودة";
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(10, 137);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(626, 26);
            this.txtNotes.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(653, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "ملاحظات";
            // 
            // txtCountBoxes
            // 
            this.txtCountBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCountBoxes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountBoxes.Location = new System.Drawing.Point(398, 57);
            this.txtCountBoxes.Name = "txtCountBoxes";
            this.txtCountBoxes.Size = new System.Drawing.Size(238, 26);
            this.txtCountBoxes.TabIndex = 13;
            this.txtCountBoxes.TextChanged += new System.EventHandler(this.WeightChanged);
            this.txtCountBoxes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpress);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(653, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "الصناديق";
            // 
            // cmbItems
            // 
            this.cmbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbItems.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(10, 25);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(258, 26);
            this.cmbItems.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(321, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "الصنف";
            // 
            // cmbVendor
            // 
            this.cmbVendor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVendor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbVendor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbVendor.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVendor.FormattingEnabled = true;
            this.cmbVendor.ItemHeight = 18;
            this.cmbVendor.Location = new System.Drawing.Point(398, 25);
            this.cmbVendor.Name = "cmbVendor";
            this.cmbVendor.Size = new System.Drawing.Size(238, 26);
            this.cmbVendor.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(675, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "المورد";
            // 
            // txtBoxesWeight
            // 
            this.txtBoxesWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxesWeight.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtBoxesWeight.Location = new System.Drawing.Point(18, 120);
            this.txtBoxesWeight.Name = "txtBoxesWeight";
            this.txtBoxesWeight.ReadOnly = true;
            this.txtBoxesWeight.Size = new System.Drawing.Size(137, 30);
            this.txtBoxesWeight.TabIndex = 17;
            this.txtBoxesWeight.Text = "0";
            this.txtBoxesWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            this.txtBoxesWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpress);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(237, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "وزن الصناديق";
            // 
            // txtPlateWeight
            // 
            this.txtPlateWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlateWeight.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtPlateWeight.Location = new System.Drawing.Point(18, 69);
            this.txtPlateWeight.Name = "txtPlateWeight";
            this.txtPlateWeight.Size = new System.Drawing.Size(137, 30);
            this.txtPlateWeight.TabIndex = 15;
            this.txtPlateWeight.Text = "0";
            this.txtPlateWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            this.txtPlateWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(233, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "وزن الباليتة";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.groupControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 255);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(736, 293);
            this.panelControl3.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl2.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl2.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.groupControl2.AppearanceCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.groupControl2.Controls.Add(this.lblNetWeight);
            this.groupControl2.Controls.Add(this.label12);
            this.groupControl2.Controls.Add(this.groupBox2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(2, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(732, 289);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "بيانات الوزن";
            // 
            // lblNetWeight
            // 
            this.lblNetWeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNetWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblNetWeight.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblNetWeight.ForeColor = System.Drawing.Color.Navy;
            this.lblNetWeight.Location = new System.Drawing.Point(3, 131);
            this.lblNetWeight.Name = "lblNetWeight";
            this.lblNetWeight.Size = new System.Drawing.Size(293, 45);
            this.lblNetWeight.TabIndex = 16;
            this.lblNetWeight.Text = "0.00";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(174, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 23);
            this.label12.TabIndex = 15;
            this.label12.Text = "صافي الوزن";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtTotalDiscount);
            this.groupBox2.Controls.Add(this.txtDiscountWeight);
            this.groupBox2.Controls.Add(this.txtWeight);
            this.groupBox2.Controls.Add(this.txtBoxesWeight);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPlateWeight);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(299, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(431, 265);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(228, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(195, 23);
            this.label11.TabIndex = 17;
            this.label11.Text = "إجمالي خصم الأوزان";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(233, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 23);
            this.label10.TabIndex = 16;
            this.label10.Text = "خصم وزن إضافي";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(237, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 23);
            this.label9.TabIndex = 15;
            this.label9.Text = "الوزن قبل الخصم";
            // 
            // txtTotalDiscount
            // 
            this.txtTotalDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalDiscount.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtTotalDiscount.Location = new System.Drawing.Point(18, 229);
            this.txtTotalDiscount.Name = "txtTotalDiscount";
            this.txtTotalDiscount.ReadOnly = true;
            this.txtTotalDiscount.Size = new System.Drawing.Size(137, 30);
            this.txtTotalDiscount.TabIndex = 14;
            this.txtTotalDiscount.Text = "0.00";
            // 
            // txtDiscountWeight
            // 
            this.txtDiscountWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscountWeight.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtDiscountWeight.Location = new System.Drawing.Point(18, 176);
            this.txtDiscountWeight.Name = "txtDiscountWeight";
            this.txtDiscountWeight.Size = new System.Drawing.Size(137, 30);
            this.txtDiscountWeight.TabIndex = 13;
            this.txtDiscountWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            this.txtDiscountWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpress);
            // 
            // txtWeight
            // 
            this.txtWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWeight.Font = new System.Drawing.Font("Tahoma", 13.75F, System.Drawing.FontStyle.Bold);
            this.txtWeight.Location = new System.Drawing.Point(18, 18);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(137, 30);
            this.txtWeight.TabIndex = 12;
            this.txtWeight.Text = "0.00";
            this.txtWeight.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.btnSave);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 548);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(736, 83);
            this.panelControl4.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.BorderColor = System.Drawing.Color.Navy;
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.Appearance.Options.UseBorderColor = true;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Appearance.Options.UseTextOptions = true;
            this.btnSave.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnSave.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnSave.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnSave.AppearanceDisabled.BorderColor = System.Drawing.Color.Navy;
            this.btnSave.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnSave.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.AppearanceDisabled.Options.UseBorderColor = true;
            this.btnSave.AppearanceDisabled.Options.UseFont = true;
            this.btnSave.AppearanceDisabled.Options.UseForeColor = true;
            this.btnSave.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnSave.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnSave.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnSave.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnSave.AppearanceHovered.BorderColor = System.Drawing.Color.Navy;
            this.btnSave.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnSave.AppearanceHovered.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.AppearanceHovered.Options.UseBorderColor = true;
            this.btnSave.AppearanceHovered.Options.UseFont = true;
            this.btnSave.AppearanceHovered.Options.UseForeColor = true;
            this.btnSave.AppearanceHovered.Options.UseTextOptions = true;
            this.btnSave.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnSave.AppearanceHovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnSave.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnSave.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.AppearancePressed.Options.UseFont = true;
            this.btnSave.AppearancePressed.Options.UseTextOptions = true;
            this.btnSave.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnSave.AppearancePressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnSave.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.Location = new System.Drawing.Point(2, 34);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(732, 47);
            this.btnSave.TabIndex = 0;
            this.btnSave.Tag = "Save";
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmOperation
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 631);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl4);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmOperation.IconOptions.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmOperation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "العمليات";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOperation_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOperation_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsPlatteWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.ToggleSwitch tsPlatteWeight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Label lblMainWeight;
        private System.Windows.Forms.ComboBox cmbItems;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbVendor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCountBoxes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBoxesWeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPlateWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalDiscount;
        private System.Windows.Forms.TextBox txtDiscountWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblNetWeight;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBoxWeight;
        private System.Windows.Forms.Label label13;
    }
}