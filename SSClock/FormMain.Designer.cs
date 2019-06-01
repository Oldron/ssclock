namespace SSClock {

  using System.Windows.Forms;


  partial class FormMain {

    /// <summary> Required designer variable. </summary>
    private System.ComponentModel.IContainer components = null;


    /// <summary> Clean up any resources being used. </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing ) {
      if( disposing && ( components != null ) ) {
        components.Dispose();
      }
      base.Dispose( disposing );
    }


    #region Windows Form Designer generated code

    /// <summary> Required method for Designer support - do not modify
    /// the contents of this method with the code editor. </summary>
    private void InitializeComponent() {

      this.components = new System.ComponentModel.Container();

      this.SuspendLayout();
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.BackgroundImageLayout = ImageLayout.Stretch;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = "SSClock";
      this.ShowInTaskbar = false;
      this.Text = "SSClock";
      this.TopMost = true;
      this.Shown += new System.EventHandler( this.MainForm_Shown );
      this.Click += new System.EventHandler( this.MainForm_Click );
      this.MouseMove += new MouseEventHandler( this.MainForm_MouseMove );
      this.KeyDown += new KeyEventHandler( this.MainForm_KeyDown );
      this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true );

      this.ResumeLayout( false );
    }

    #endregion

  } // class FormMain
} // namespace SSClock

