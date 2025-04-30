namespace ELNour.Admin
{
    partial class frmNewYear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewYear));
            this.btnGenerateScript = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnGenerateScript
            // 
            this.btnGenerateScript.Appearance.BorderColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnGenerateScript.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.Appearance.Options.UseBorderColor = true;
            this.btnGenerateScript.Appearance.Options.UseFont = true;
            this.btnGenerateScript.Appearance.Options.UseForeColor = true;
            this.btnGenerateScript.Appearance.Options.UseTextOptions = true;
            this.btnGenerateScript.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnGenerateScript.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnGenerateScript.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnGenerateScript.AppearanceDisabled.BorderColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnGenerateScript.AppearanceDisabled.ForeColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.AppearanceDisabled.Options.UseBorderColor = true;
            this.btnGenerateScript.AppearanceDisabled.Options.UseFont = true;
            this.btnGenerateScript.AppearanceDisabled.Options.UseForeColor = true;
            this.btnGenerateScript.AppearanceDisabled.Options.UseTextOptions = true;
            this.btnGenerateScript.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnGenerateScript.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnGenerateScript.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnGenerateScript.AppearanceHovered.BorderColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.btnGenerateScript.AppearanceHovered.ForeColor = System.Drawing.Color.Navy;
            this.btnGenerateScript.AppearanceHovered.Options.UseBorderColor = true;
            this.btnGenerateScript.AppearanceHovered.Options.UseFont = true;
            this.btnGenerateScript.AppearanceHovered.Options.UseForeColor = true;
            this.btnGenerateScript.AppearanceHovered.Options.UseTextOptions = true;
            this.btnGenerateScript.AppearanceHovered.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnGenerateScript.AppearanceHovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnGenerateScript.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnGenerateScript.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGenerateScript.AppearancePressed.Options.UseFont = true;
            this.btnGenerateScript.AppearancePressed.Options.UseTextOptions = true;
            this.btnGenerateScript.AppearancePressed.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnGenerateScript.AppearancePressed.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnGenerateScript.AppearancePressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.btnGenerateScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerateScript.Location = new System.Drawing.Point(0, 228);
            this.btnGenerateScript.Name = "btnGenerateScript";
            this.btnGenerateScript.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnGenerateScript.Size = new System.Drawing.Size(679, 46);
            this.btnGenerateScript.TabIndex = 50;
            this.btnGenerateScript.Tag = "Save";
            this.btnGenerateScript.Text = "حفظ";
            this.btnGenerateScript.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmNewYear
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 274);
            this.Controls.Add(this.btnGenerateScript);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmNewYear.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNewYear";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تصفير قاعدة البيانات";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGenerateScript;
    }
}