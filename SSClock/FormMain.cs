namespace SSClock {

  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Timers;
  using System.Windows.Forms;
  using Timer = System.Timers.Timer;


  public partial class FormMain : Form {


    #region Preview API's
    [DllImport("user32.dll")]
    static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    [DllImport("user32.dll")]
    static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);
    [DllImport("user32.dll")]
    static extern Boolean GetClientRect(IntPtr hWnd, out Rectangle lpRect);
    #endregion

    readonly Boolean isPreviewMode = false;


    #region Construtors
    public FormMain() { this.InitializeComponent(); }


    //This constructor is passed the bounds this form is to show in
    //It is used when in normal mode
    public FormMain(Rectangle bounds)
    {

      this.InitializeComponent();

      this.x = bounds.X;
      this.y = bounds.Y;
      this.Width = bounds.Width;
      this.Height = bounds.Height;
      Cursor.Hide();
    }


    //This constructor is the handle to the select screensaver dialog preview window
    //It is used when in preview mode (/p)
    public FormMain(IntPtr previewHandle)
    {

      this.InitializeComponent();

      //set the preview window as the parent of this window
      SetParent(this.Handle, previewHandle);

      //make this a child window, so when the select screensaver dialog closes, this will also close
      SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

      //set our window's size to the size of our window's new parent
      Rectangle parentRect;
      GetClientRect(previewHandle, out parentRect);
      this.Size = parentRect.Size;

      //set our location at (0, 0)
      this.Location = new Point(0, 0);

      this.isPreviewMode = true;
    }
    #endregion Construtors


    #region Handlers
    private void MainForm_KeyDown(Object sender, KeyEventArgs e)
    {
      //disable exit functions for preview
      if (!this.isPreviewMode)
      {
        this.timerDispose();
        Application.Exit();
      }
    }


    private void MainForm_Click(Object sender, EventArgs e)
    {
      //disable exit functions for preview
      if (!this.isPreviewMode)
      {
        this.timerDispose();
        Application.Exit();
      }
    }


    //start off OriginalLoction with an X and Y of int.MaxValue, because
    //it is impossible for the cursor to be at that position. That way, we
    //know if this variable has been set yet.
    Point originalLocation = new Point(Int32.MaxValue, Int32.MaxValue);


    private void MainForm_MouseMove(Object sender, MouseEventArgs e)
    {

      //disable exit functions for preview
      if (this.isPreviewMode)
      {
        return;
      }

      //see if originallocat5ion has been set
      if (this.originalLocation.X == Int32.MaxValue & this.originalLocation.Y == Int32.MaxValue)
      {
        this.originalLocation = e.Location;
      }
      //see if the mouse has moved more than 20 pixels in any direction. If it has, close the application.
      if (Math.Abs(e.X - this.originalLocation.X) > 20 | Math.Abs(e.Y - this.originalLocation.Y) > 20)
      {
        this.timerDispose();
        Application.Exit();
      }
    }
    #endregion Handlers


    #region Draw
    private const Int32 SZ_TIME = 160;
    private const Int32 SZ_DATE = 48;
    private const Int32 IDX_DRAW_POS_CHANGE = 15;

    private Timer aTimer = null;
    private readonly Random rnd = new Random();
    private readonly Font fntTime = new Font("Arial Black", SZ_TIME);
    private readonly Font fntDate = new Font("Arial Black", SZ_DATE);
    private readonly FontFamily fontFamily = new FontFamily("Arial Black");
    private readonly SolidBrush drawBrush = new SolidBrush(Color.DarkGray);
    private Int32 cntDraws = -2;
    private Double x = 0D;
    private Double y = 0D;
    private const Int32 LINE_SPACE = 8;
    private RectangleF boundClr = new RectangleF();
    private Int32 minutePre = -1;


    private void timerStart()
    {

      if (null != this.aTimer)
      {
        return;
      }

      this.aTimer = new Timer(1000);
      this.aTimer.Elapsed += this.aTimer_Elapsed;
      this.aTimer.AutoReset = true;
      this.aTimer.Enabled = true;
    }


    private void aTimer_Elapsed(Object sender, ElapsedEventArgs e) { this.draw(); }


    private void timerDispose()
    {
      if (null == this.aTimer)
      {
        return;
      }
      this.aTimer.Stop();
      this.aTimer.Dispose();
    }


    private void MainForm_Shown(Object sender, EventArgs e)
    {

      //we don't want all those effects for just a preview
      if (!this.isPreviewMode)
      {
        this.Refresh();
        //keep the screen black for one second to simulate the changing of screen resolution
        Thread.Sleep(1000);
      }

      this.draw();
      this.timerStart();
    }


    private void draw()
    {

      var now = DateTime.Now;
      if (this.minutePre == now.Minute && 0 <= this.cntDraws && this.cntDraws < IDX_DRAW_POS_CHANGE)
      {
        this.cntDraws++;
        return;
      }

      this.minutePre = now.Minute;

      var formGraphics = this.CreateGraphics();
      formGraphics.SmoothingMode = SmoothingMode.HighQuality;
      var strTime = now.ToString("HH:mm");
      var strDate = now.ToString("d MMMM yyyy\ndddd");
      var temp = MskTemperature.GET_TEMPERATURE_CURR();
      if (!String.IsNullOrEmpty(temp))
      {
        strDate = String.Concat(strDate, "\n", temp);
      }
      var szStrTime = formGraphics.MeasureString(strTime, this.fntTime);
      var szStrDate = formGraphics.MeasureString(strDate, this.fntDate);
      var szStr = new Size((Int32)Math.Max(szStrTime.Width, szStrDate.Width),
                            (Int32)(szStrTime.Height + LINE_SPACE + szStrDate.Height));

      if (0 > this.cntDraws || this.cntDraws >= IDX_DRAW_POS_CHANGE)
      {
        this.x = (this.rnd.NextDouble() * (this.Size.Width - szStr.Width)) + (szStr.Width / 2);
        this.y = (this.rnd.NextDouble() * (this.Size.Height - szStr.Height)) + (szStr.Height / 2);
        this.cntDraws = 0;
      }

      var drawFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

      var path = new GraphicsPath();
      path.AddString(strTime, this.fontFamily, (Int32)FontStyle.Regular, SZ_TIME,
        new Point((Int32)this.x, (Int32)this.y),
        drawFormat);
      path.AddString(strDate, this.fontFamily, (Int32)FontStyle.Regular, SZ_DATE,
        new Point((Int32)this.x, (Int32)(this.y + (szStrTime.Height / 2) + LINE_SPACE)),
        drawFormat);

      if (Math.Abs(this.boundClr.Width) > 0.1)
      {
        formGraphics.FillRectangle(Brushes.Black,
                                    this.boundClr.X - 20, this.boundClr.Y - 20,
                                    this.boundClr.Width + 40, this.boundClr.Height + 40);
      }
      formGraphics.FillPath(this.drawBrush, path);
      this.boundClr = path.GetBounds();

      formGraphics.Dispose();
    }
    #endregion Draw

  }
}
