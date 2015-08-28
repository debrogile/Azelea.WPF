using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Windows;

namespace Azelea.WPF.TradeClient
{
    public class HttpUtil
    {
        public static string HttpGet(string url, NameValueCollection parameters)
        {
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    MessageBox.Show(parameters[i]);
                }

                url += "?";

                var e = parameters.GetEnumerator();
                while (e.MoveNext())
                {
                    //url += string.Format("{0}={1}&", e.Key, e.Value);
                }
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse response = null;
            string result;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response;
            }
            finally
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }

        public static string HttpPost(string url)
        {
            return string.Empty;
        }
    }
}
