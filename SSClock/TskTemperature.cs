 // ReSharper disable once CheckNamespace
namespace LibTskTemperature {

  using System;
  using System.Net;
  using System.Xml;


  public static class TskTemperature {

    /*private const String XmlTemp = @"<?xml version=""1.0"" ?>
<termo>
  <copyright>http://termo.tomsk.ru</copyright>
  <url>http://termo.tomsk.ru/</url>
  <current temp = ""-22.1"" date=""04.01.2016"" time=""14:29"" change=""+"" />
  <day>
    <min temp = ""-25.9"" date=""04.01"" time=""08:21"" />
    <max temp = ""-20.5"" date=""03.01"" time=""14:54"" />
    <avg temp = ""-23.7"" />
  </day>
  <week>
    <min temp=""-28.9"" date=""03.01"" time=""08:07"" />
    <max temp = ""1.2"" date=""29.12"" time=""14:06"" />
    <avg temp = ""-15.3"" />
  </week>
  <month>
    <min temp=""-28.9"" date=""03.01"" time=""08:07"" />
    <max temp = ""2.7"" date=""09.12"" time=""12:30"" />
    <avg temp = ""-8.4"" />
  </month>
</termo> ";*/

    /// <summary> Last web request time </summary>
    private static DateTime _dtmLastReq = DateTime.MinValue;
    /// <summary> Web request response body </summary>
    private static String _xmlLastReq = String.Empty;
    /// <summary> Lock object </summary>
    private static readonly Object _lock = new Object();
    /// <summary> Is doing request flag </summary>
    private static bool _doRequest = false;


    /// <summary> Init temperature getter </summary>
    public static void INIT() { _getFromWeb(); }


    /// <summary> Get current temperature </summary>
    /// <returns>String with temperature. NULL - if error.</returns>
    public static String GET_TEMPERATURE_CURR() {

      String tempCurr = null;
      _getFromWeb();

      try {

        var xml = new XmlDocument();
        xml.LoadXml( _xmlLastReq );

        var node = xml["termo"]["current"];

        tempCurr = node.Attributes[ "temp" ].Value;

        var changeVal = node.Attributes[ "change" ].Value;
        var change = String.Empty;
        if( "+" == changeVal ) {
          change = " ↑";
        }
        if( "-" == changeVal ) {
          change = " ↓";
        }

        tempCurr = String.Concat( tempCurr, "°", change );

      } catch( Exception ) {
        tempCurr = null;
      }

      return tempCurr;
    } // GET_TEMPERATURE_CURR()


    /// <summary> Web request </summary>
    private static void _getFromWeb() {

      if( _doRequest ) {
        return;
      }

      _doRequest = true;

      lock ( _lock ) {

        if( 1 > DateTime.Now.Subtract( _dtmLastReq ).TotalHours ) {
          _doRequest = false;
          return;
        }

        try {

          _dtmLastReq = DateTime.Now;
          using( var client = new WebClient() ) {
            _xmlLastReq = client.DownloadString( "http://termo.tomsk.ru/data.xml" );
          }

        } catch( Exception ) { }

        _doRequest = false;
      }
    } // _getFromWeb()

  } // class TskTemperature
} // namespace LibTskTemperature
