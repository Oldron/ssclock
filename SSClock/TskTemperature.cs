// ReSharper disable once CheckNamespace
namespace LibTskTemperature {

    using System;
    using System.Net;
    using System.Xml;


    public static class TskTemperature {

        /*<?xml version="1.1" encoding="UTF-8"?>
    <city>
        <id>tomsk</id>
        <url>http://termopogoda.ru/tomsk/</url>
        <name>Томск</name>
        <name_gen>Томске</name_gen>
        <description>Актуальная температура в г. Томске</description>
        <data>
            <date>01.06.2019</date>
            <time>18:44</time>
            <temp change="">14.1</temp>
        </data>
    </city>*/

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
                xml.LoadXml(_xmlLastReq);

                var node = xml["city"]["data"]["temp"];

                tempCurr = node.InnerText;

                var changeVal = node.Attributes["change"].Value;
                var change = String.Empty;
                if ("+" == changeVal) {
                    change = " ↑";
                }
                if ("-" == changeVal) {
                    change = " ↓";
                }

                tempCurr = String.Concat(tempCurr, "°", change);

            } catch (Exception) {
                tempCurr = null;
            }

            return tempCurr;
        } // GET_TEMPERATURE_CURR()


        /// <summary> Web request </summary>
        private static void _getFromWeb() {

            if (_doRequest) {
                return;
            }

            _doRequest = true;

            lock (_lock) {

                if (1 > DateTime.Now.Subtract(_dtmLastReq).TotalHours) {
                    _doRequest = false;
                    return;
                }

                try {

                    _dtmLastReq = DateTime.Now;
                    using (var client = new WebClient()) {
                        _xmlLastReq = client.DownloadString("http://termopogoda.ru/data_tomsk.xml");
                        _xmlLastReq = _xmlLastReq.Replace("\"1.1\"", "\"1.0\""); // Fix error on parse XML ver. 1.1
                    }

                } catch (Exception) { }

                _doRequest = false;
            }
        } // _getFromWeb()

    } // class TskTemperature
} // namespace LibTskTemperature
