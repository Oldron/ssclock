
namespace SSClock {
    partial class FormConfig {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.lblApiKey = new System.Windows.Forms.Label();
      this.tbApiKey = new System.Windows.Forms.TextBox();
      this.lblCity = new System.Windows.Forms.Label();
      this.lblCityValue = new System.Windows.Forms.Label();
      this.btnCitySelect = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(253, 98);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(172, 98);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // lblApiKey
      // 
      this.lblApiKey.AutoSize = true;
      this.lblApiKey.Location = new System.Drawing.Point(14, 15);
      this.lblApiKey.Name = "lblApiKey";
      this.lblApiKey.Size = new System.Drawing.Size(47, 13);
      this.lblApiKey.TabIndex = 2;
      this.lblApiKey.Text = "API key:";
      // 
      // tbApiKey
      // 
      this.tbApiKey.Location = new System.Drawing.Point(64, 12);
      this.tbApiKey.Name = "tbApiKey";
      this.tbApiKey.Size = new System.Drawing.Size(264, 20);
      this.tbApiKey.TabIndex = 3;
      // 
      // lblCity
      // 
      this.lblCity.AutoSize = true;
      this.lblCity.Location = new System.Drawing.Point(14, 43);
      this.lblCity.Name = "lblCity";
      this.lblCity.Size = new System.Drawing.Size(27, 13);
      this.lblCity.TabIndex = 4;
      this.lblCity.Text = "City:";
      // 
      // lblCityValue
      // 
      this.lblCityValue.AutoSize = true;
      this.lblCityValue.Location = new System.Drawing.Point(64, 43);
      this.lblCityValue.Name = "lblCityValue";
      this.lblCityValue.Size = new System.Drawing.Size(65, 13);
      this.lblCityValue.TabIndex = 5;
      this.lblCityValue.Text = "not selected";
      // 
      // btnCitySelect
      // 
      this.btnCitySelect.Location = new System.Drawing.Point(64, 60);
      this.btnCitySelect.Name = "btnCitySelect";
      this.btnCitySelect.Size = new System.Drawing.Size(75, 23);
      this.btnCitySelect.TabIndex = 6;
      this.btnCitySelect.Text = "Select city";
      this.btnCitySelect.UseVisualStyleBackColor = true;
      this.btnCitySelect.Click += new System.EventHandler(this.btnCitySelect_Click);
      // 
      // FormConfig
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(340, 133);
      this.Controls.Add(this.btnCitySelect);
      this.Controls.Add(this.lblCityValue);
      this.Controls.Add(this.lblCity);
      this.Controls.Add(this.tbApiKey);
      this.Controls.Add(this.lblApiKey);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormConfig";
      this.Text = "SSClock Config";
      this.Load += new System.EventHandler(this.FormConfig_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox tbApiKey;
    private System.Windows.Forms.Label lblCity;
    private System.Windows.Forms.Label lblCityValue;
    private System.Windows.Forms.Button btnCitySelect;
  }
}