namespace SSClock {

  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Timers;
  using System.Windows.Forms;
  using LibTskTemperature;
  using Timer = System.Timers.Timer;


  public partial class FormMain : Form {


    #region Preview API's
    [DllImport( "user32.dll" )]
    static extern IntPtr SetParent( IntPtr hWndChild, IntPtr hWndNewParent );
    [DllImport( "user32.dll" )]
    static extern Int32 SetWindowLong( IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong );
    [DllImport( "user32.dll", SetLastError = true )]
    static extern Int32 GetWindowLong( IntPtr hWnd, Int32 nIndex );
    [DllImport( "user32.dll" )]
    static extern Boolean GetClientRect( IntPtr hWnd, out Rectangle lpRect );
    #endregion

    readonly Boolean _isPreviewMode = false;


    #region Construtors
    public FormMain() { this.InitializeComponent(); }


    //This constructor is passed the bounds this form is to show in
    //It is used when in normal mode
    public FormMain( Rectangle bounds ) {

      this.InitializeComponent();

      this.Bounds = bounds;
      Cursor.Hide();
    }


    //This constructor is the handle to the select screensaver dialog preview window
    //It is used when in preview mode (/p)
    public FormMain( IntPtr previewHandle ) {

      this.InitializeComponent();

      //set the preview window as the parent of this window
      SetParent( this.Handle, previewHandle );

      //make this a child window, so when the select screensaver dialog closes, this will also close
      SetWindowLong( this.Handle, -16, new IntPtr( GetWindowLong( this.Handle, -16 ) | 0x40000000 ) );

      //set our window's size to the size of our window's new parent
      Rectangle parentRect;
      GetClientRect( previewHandle, out parentRect );
      this.Size = parentRect.Size;

      //set our location at (0, 0)
      this.Location = new Point( 0, 0 );

      this._isPreviewMode = true;
    }
    #endregion Construtors


    #region Handlers
    private void MainForm_KeyDown( Object sender, KeyEventArgs e ) {
      //disable exit functions for preview
      if( !this._isPreviewMode ) {
        this._timerDispose();
        Application.Exit();
      }
    }


    private void MainForm_Click( Object sender, EventArgs e ) {
      //disable exit functions for preview
      if( !this._isPreviewMode ) {
        this._timerDispose();
        Application.Exit();
      }
    }


    //start off OriginalLoction with an X and Y of int.MaxValue, because
    //it is impossible for the cursor to be at that position. That way, we
    //know if this variable has been set yet.
    Point _originalLocation = new Point( Int32.MaxValue, Int32.MaxValue );


    private void MainForm_MouseMove( Object sender, MouseEventArgs e ) {

      //disable exit functions for preview
      if( this._isPreviewMode ) {
        return;
      }

      //see if originallocat5ion has been set
      if( this._originalLocation.X == Int32.MaxValue & this._originalLocation.Y == Int32.MaxValue ) {
        this._originalLocation = e.Location;
      }
      //see if the mouse has moved more than 20 pixels in any direction. If it has, close the application.
      if( Math.Abs( e.X - this._originalLocation.X ) > 20 | Math.Abs( e.Y - this._originalLocation.Y ) > 20 ) {
        this._timerDispose();
        Application.Exit();
      }
    }
    #endregion Handlers


    #region Draw
    private Timer _aTimer = null;
    private readonly Random _rnd = new Random();
    private const Int32 SzTime = 120;
    private const Int32 SzDate = 32;
    private readonly Font _fntTime = new Font( "Arial Black", SzTime );
    private readonly Font _fntDate = new Font( "Arial Black", SzDate );
    private readonly FontFamily _ff = new FontFamily( "Arial Black" );
    private readonly SolidBrush _drawBrush = new SolidBrush( Color.DarkGray );
    private const Int32 IdxDrawPosChange = 15;
    private Int32 _cntDraws = -2;
    private Double _x = 0D;
    private Double _y = 0D;
    private const Int32 LineSpace = 0;
    private RectangleF _boundClr = new RectangleF();
    private Int32 _minutePre = -1;


    private void _timerStart() {

      if( null != this._aTimer ) {
        return;
      }

      this._aTimer = new Timer( 1000 );
      this._aTimer.Elapsed += this._aTimer_Elapsed;
      this._aTimer.AutoReset = true;
      this._aTimer.Enabled = true;
    }


    private void _aTimer_Elapsed( Object sender, ElapsedEventArgs e ) { this._draw(); }


    private void _timerDispose() {
      if( null == this._aTimer ) {
        return;
      }
      this._aTimer.Stop();
      this._aTimer.Dispose();
    }


    private void MainForm_Shown( Object sender, EventArgs e ) {

      //we don't want all those effects for just a preview
      if( !this._isPreviewMode ){
        this.Refresh();
        //keep the screen black for one second to simulate the changing of screen resolution
        Thread.Sleep( 1000 );
      }

      this._draw();
      this._timerStart();
    }


    private void _draw() {

      var now = DateTime.Now;
      if( this._minutePre == now.Minute && 0 <= this._cntDraws && this._cntDraws < IdxDrawPosChange ) {
        this._cntDraws++;
        return;
      }

      this._minutePre = now.Minute;

      var formGraphics = this.CreateGraphics();
      formGraphics.SmoothingMode = SmoothingMode.HighQuality;
      var strTime = now.ToString( "HH:mm" );
      var strDate = now.ToString( "d MMMM yyyy\ndddd" );
      var temp = TskTemperature.GET_TEMPERATURE_CURR();
      if( !String.IsNullOrEmpty( temp ) ) {
        strDate = String.Concat( strDate, "\n", temp );
      }
      var szStrTime = formGraphics.MeasureString( strTime, this._fntTime );
      var szStrDate = formGraphics.MeasureString( strDate, this._fntDate );
      var szStr = new Size( (Int32)Math.Max( szStrTime.Width, szStrDate.Width ),
                            (Int32)( szStrTime.Height + LineSpace + szStrDate.Height ) );

      if( 0 > this._cntDraws || this._cntDraws >= IdxDrawPosChange ) {
        this._x = ( this._rnd.NextDouble() * ( this.Size.Width - szStr.Width ) ) + ( szStr.Width / 2 );
        this._y = ( this._rnd.NextDouble() * ( this.Size.Height - szStr.Height ) ) + ( szStr.Height / 2 );
        this._cntDraws = 0;
      }

      var drawFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

      var path = new GraphicsPath();
      path.AddString( strTime, this._ff, (Int32)FontStyle.Regular, SzTime,
                           new Point( (Int32)this._x, (Int32)this._y ), drawFormat );
      path.AddString( strDate, this._ff, (Int32)FontStyle.Regular, SzDate,
                           new Point( (Int32)this._x, (Int32)( this._y + ( szStrTime.Height / 2 ) + LineSpace ) ), drawFormat );

      if( Math.Abs( this._boundClr.Width ) > 0.1 ) {
        formGraphics.FillRectangle( Brushes.Black,
                                    this._boundClr.X - 20, this._boundClr.Y - 20,
                                    this._boundClr.Width + 40, this._boundClr.Height + 40 );
      }
      formGraphics.FillPath( this._drawBrush, path );
      this._boundClr = path.GetBounds();

      formGraphics.Dispose();
    }
    #endregion Draw

  } // class FormMain
} // namespace SSClock
