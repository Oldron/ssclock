namespace SSClock {

    using System;
    using System.Net;
    using System.Web.Script.Serialization;

    class MskTemperature {

        const string URL = "http://api.openweathermap.org/data/2.5/weather?id=524894&units=metric&APPID=xxx"; // TODO: set API key

        /// <summary> Last web request time </summary>
        private static DateTime dtmLastReq = DateTime.MinValue;
        /// <summary> Web request response body </summary>
        private static string json = string.Empty;
        /// <summary> Lock object </summary>
        private static readonly object lockObj = new object();
        /// <summary> Is doing request flag </summary>
        private static bool doRequest = false;


        /// <summary> Init temperature getter </summary>
        public static void INIT() { getFromWeb(); }


        /// <summary> Get current temperature </summary>
        /// <returns>String with temperature. NULL - if error.</returns>
        public static string GET_TEMPERATURE_CURR() {

            string tempCurr;
            getFromWeb();

            try {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic item = serializer.Deserialize<object>(json);
                Decimal temp = item["main"]["temp"];
                tempCurr = string.Concat(temp.ToString(".0"), "°");
            } catch (Exception) {
                tempCurr = null;
            }

            return tempCurr;
        }


        /// <summary> Web request </summary>
        private static void getFromWeb() {

            if (doRequest) {
                return;
            }

            doRequest = true;

            lock (lockObj) {

                if (1 > DateTime.Now.Subtract(dtmLastReq).TotalHours) {
                    doRequest = false;
                    return;
                }

                try {

                    dtmLastReq = DateTime.Now;
                    using (var client = new WebClient()) {
                        json = client.DownloadString(URL);
                        // json = "{\"coord\":{\"lon\":37.6067,\"lat\":55.7617},\"weather\":[{\"id\":601,\"main\":\"Snow\",\"description\":\"snow\",\"icon\":\"13d\"}],\"base\":\"stations\",\"main\":{\"temp\":-14.64,\"feels_like\":-22.37,\"temp_min\":-15,\"temp_max\":-13.33,\"pressure\":1006,\"humidity\":72},\"visibility\":9000,\"wind\":{\"speed\":6,\"deg\":320},\"snow\":{\"1h\":0.56},\"clouds\":{\"all\":40},\"dt\":1613291642,\"sys\":{\"type\":1,\"id\":9027,\"country\":\"RU\",\"sunrise\":1613278600,\"sunset\":1613313064},\"timezone\":10800,\"id\":524894,\"name\":\"Moscow\",\"cod\":200}";
                    }

                } catch (Exception) {}

                doRequest = false;
            }
        }

    }
}
