
namespace SSClock {
  partial class FormCitySelect {
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
      this.lblSelectCity = new System.Windows.Forms.Label();
      this.cbCity = new System.Windows.Forms.ComboBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblSelectCity
      // 
      this.lblSelectCity.AutoSize = true;
      this.lblSelectCity.Location = new System.Drawing.Point(13, 13);
      this.lblSelectCity.Name = "lblSelectCity";
      this.lblSelectCity.Size = new System.Drawing.Size(56, 13);
      this.lblSelectCity.TabIndex = 0;
      this.lblSelectCity.Text = "Select city";
      // 
      // cbCity
      // 
      this.cbCity.FormattingEnabled = true;
      this.cbCity.Location = new System.Drawing.Point(76, 13);
      this.cbCity.Name = "cbCity";
      this.cbCity.Size = new System.Drawing.Size(365, 21);
      this.cbCity.TabIndex = 1;
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(365, 41);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(284, 40);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // FormCitySelect
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(453, 78);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.cbCity);
      this.Controls.Add(this.lblSelectCity);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormCitySelect";
      this.Text = "Select city";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblSelectCity;
    private System.Windows.Forms.ComboBox cbCity;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
  }
}