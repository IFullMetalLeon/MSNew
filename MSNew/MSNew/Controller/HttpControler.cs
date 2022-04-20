using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MSNew.Controller
{
    public class HttpControler
    {
        public static string httpMainUrl = "http://markscan.sibatom.com/";
        public static async void SendPostDocSpec(string t_name, string barcode, string doc_rn, string box_rn, string del_sign, string quant, string channel_name)
        {
            try
            {

                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                http.BaseAddress = new Uri(httpMainUrl);
                http.DefaultRequestHeaders.Accept.Clear();

                var values = new Dictionary<string, string>();
                values.Add("t_name", t_name);
                values.Add("bar", barcode);
                values.Add("in_doc_rn", doc_rn);
                values.Add("in_box_rn", box_rn);
                values.Add("in_del_sign", del_sign);
                values.Add("in_quant", quant);

                var response = await http.PostAsync(http.BaseAddress, new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetDocQueue(string term)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=doc_queue&term=" + term;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetDocQueue", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetDocSpec(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=doc&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetDocSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetDocHead(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=head&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetDocHead", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetBoxSpec(string doc_rn, string nm_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=box&doc_rn=" + doc_rn + "&nm_rn=" + nm_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetBoxSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetMarkSpec(string box_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=mark&box_rn=" + box_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetMarkSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendPostInfo(string barcode, string channel_name)
        {
            try
            {

                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                http.BaseAddress = new Uri(httpMainUrl);
                http.DefaultRequestHeaders.Accept.Clear();

                var values = new Dictionary<string, string>();
                values.Add("type", "info");
                values.Add("bar", barcode);

                var response = await http.PostAsync(http.BaseAddress, new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetInfoHead(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=info_head&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetInfoHead", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetInfoSpec(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=info_spec&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetInfoSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendPostLink(string t_name, string barcode, string box_rn, string del_sign, string channel_name)
        {
            try
            {

                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                http.BaseAddress = new Uri(httpMainUrl);
                http.DefaultRequestHeaders.Accept.Clear();

                var values = new Dictionary<string, string>();
                values.Add("type", "link");
                values.Add("t_name", t_name);
                values.Add("bar", barcode);
                values.Add("in_box_rn", box_rn);
                values.Add("in_del_sign", del_sign);

                var response = await http.PostAsync(http.BaseAddress, new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }

        }

        public static async void SendGetLinkSpec(string box_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=link&box_rn=" + box_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetLinkSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendPostRevizor(string t_name, string barcode, string doc_rn, string do_sign, string item_rn, string quant, string channel_name)
        {
            try
            {

                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                http.BaseAddress = new Uri(httpMainUrl);
                http.DefaultRequestHeaders.Accept.Clear();

                var values = new Dictionary<string, string>();
                values.Add("type", "revizor");
                values.Add("t_name", t_name);
                values.Add("bar", barcode);
                values.Add("in_doc_rn", doc_rn);
                values.Add("in_do_sign", do_sign);
                values.Add("in_item_rn", item_rn);
                values.Add("in_quant", quant);

                var response = await http.PostAsync(http.BaseAddress, new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetRevizorHead(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=revizor_head&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetRevizorHead", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetRevizorBox(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=revizor_box&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetRevizorBox", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetRevizorMark(string box_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=revizor_mark&box_rn=" + box_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetRevizorMark", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendPostDmnRevizor(string t_name, string barcode, string channel_name)
        {
            try
            {

                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                http.BaseAddress = new Uri(httpMainUrl);
                http.DefaultRequestHeaders.Accept.Clear();

                var values = new Dictionary<string, string>();
                values.Add("type", "dmn_revizor");
                values.Add("t_name", t_name);
                values.Add("bar", barcode);

                var response = await http.PostAsync(http.BaseAddress, new FormUrlEncodedContent(values));
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetDmnRevizorHead(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=revizor_dmn_head&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetDmnRevizorHead", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendGetDmnRevizorSpec(string doc_rn)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string param = "?type=revizor_dmn_spec&doc_rn=" + doc_rn;

                http.BaseAddress = new Uri(httpMainUrl + param);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetDmnRevizorSpec", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void SendPostDmnRevizorXmlResult(string xml, string channel_name)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(30);

                http.BaseAddress = new Uri(httpMainUrl);

                http.DefaultRequestHeaders.Accept.Clear();

                string FormStuff = string.Format("type={0}&xml={1}", "dmn_revizor_result", xml);

                StringContent c = new StringContent(FormStuff, Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await http.PostAsync(http.BaseAddress, c);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                MessagingCenter.Send<string, string>("HttpControler", channel_name, content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }

        public static async void GetServerVersion()
        {
            try
            {
                HttpClient http = new HttpClient();
                http.Timeout = TimeSpan.FromMinutes(5);

                string addr = "http://sibatom.com/share/MarkScanAndroidVersion.txt";

                http.BaseAddress = new Uri(addr);
                http.DefaultRequestHeaders.Accept.Clear();

                var response = await http.GetAsync(http.BaseAddress);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();

                MessagingCenter.Send<string, string>("HttpControler", "GetServerVersion", content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("address associated with hostname"))
                    MessagingCenter.Send<string, string>("HttpControler", "Error", "Нет подключения к интернету");
                else
                    MessagingCenter.Send<string, string>("HttpControler", "Error", ex.Message);
            }
        }
    }
}
