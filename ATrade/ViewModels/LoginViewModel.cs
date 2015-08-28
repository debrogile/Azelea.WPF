using Azelea.WPF.Controls;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;

namespace Azelea.WPF.TradeClient.ViewModels
{
    [Export]
    public class LoginViewModel : Screen
    {
        public void Login()
        {
            string url = "http://sandbox.hs.net/oauth2/oauth2/oauthacct_trade_bind";

            string data = "";
            data += "targetcomp_id=91000&";
            data += "sendercomp_id=200352&";
            data += "targetbusinsys_no=1000&";
            data += "op_station=http%3A%2F%2Fwww.baidu.com&";
            data += "input_content=6&";
            data += "account_content=70000176&";
            data += "password=111111";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic YTE1ZTZhMTEtNTk5My00MTA5LWI0N2MtMzUyOGFhNzA3ZmYwOjMxNjAwNDdlLTBhNWEtNDc1OS05ZDQ1LWM5N2ZkY2E3OGU5MQ==");

            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(data);
            }

            try
            {
                var response = request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    MessageBox.Show(sr.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response;
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    string s = sr.ReadToEnd();
                    var result = MessageBox.Show(s, "系统提示", MessageBoxButtonType.OKCancel);

                    MessageBox.Show(result.ToString());
                    MessageBox.Show(s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s);
                    System.Windows.MessageBox.Show(s+s+s+s+s+s+s+s+s+ s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s + s);
                }
            }
        }
    }
}
