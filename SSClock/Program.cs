namespace SSClock {

  using System;
  using System.Windows.Forms;


  static class Program {

    /// <summary> The main entry point for the application. </summary>
    [STAThread]
    static void Main( String[] args ) {

      LibTskTemperature.TskTemperature.INIT();

      var arg = args.Length > 0 ? args[ 0 ].ToLower().Trim().Substring( 0, 2 ) : String.Empty;

      switch( arg ) {

        case "/s": //show
                   //run the screen saver
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault( false );
          ShowScreensaver();
          Application.Run();
          break;

        case "/p": //preview
                   //show the screen saver preview
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault( false );
          Application.Run( new FormMain( new IntPtr( Int64.Parse( args[ 1 ] ) ) ) ); //args[1] is the handle to the preview window
          break;

        case "/c": //configure
                   //inform the user no options can be set in this screen saver
          MessageBox.Show(
                          "This screensaver has no options that you can set",
                          "Blue Screen Saver",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information );
          break;

        default:
          //an argument was passed, but it wasn't /s, /p, or /c, so we don't care wtf it was
          //show the screen saver anyway
          //no arguments were passed
          //run the screen saver
          Application.EnableVisualStyles();
          Application.SetCompatibleTextRenderingDefault( false );
          ShowScreensaver();
          Application.Run();
          break;
      }
    }


    static void ShowScreensaver() {
      //loops through all the computer's screens (monitors)
      var screens = Screen.AllScreens;
      foreach( var screen in screens ) {
        //creates a form just for that screen and passes it the bounds of that screen
        var screensaver = new FormMain( screen.Bounds );
        screensaver.Show();
      }
    }

  } // class Program
} // namespace SSClock
