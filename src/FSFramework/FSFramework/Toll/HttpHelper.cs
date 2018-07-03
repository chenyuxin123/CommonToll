using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FSFramework.Toll
{
    /// <summary>
    /// HTTP帮助类
    /// </summary>
    public class HttpHelper
    {
        //public static string HttpGet2(string url)
        //{
        //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        //    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //    StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
        //    string responseContent = streamReader.ReadToEnd();
        //    httpWebResponse.Close();
        //    streamReader.Close();
        //    return responseContent;
        //}

        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static string HttpGet(string requestUri)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage httpWebResponse = client.GetAsync(requestUri).Result;
            if (httpWebResponse.IsSuccessStatusCode)
            {
                return httpWebResponse.Content.ReadAsStringAsync().Result;
            }
            return "";
        }

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="body"></param>
        /// <param name="method"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpPost(string requestUri, string body, string method = "POST", string contentType = "application/json")
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(baseAddress);
            HttpResponseMessage httpWebResponse = client.PostAsync(requestUri, new StringContent(body, Encoding.UTF8, contentType)).Result;
            if (httpWebResponse.IsSuccessStatusCode)
            {
                return httpWebResponse.Content.ReadAsStringAsync().Result;
            }
            return "";
        }

        /// <summary>
        /// HTTP GET Basic验证
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HttpGet_Basic(string requestUri, string username, string password)
        {
            string usernamePassword = username + ":" + password;
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));
            ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;//验证服务器证书回调自动验证  
            HttpResponseMessage httpWebResponse = client.GetAsync(requestUri).Result;
            if (httpWebResponse.IsSuccessStatusCode)
            {
                return httpWebResponse.Content.ReadAsStringAsync().Result;
            }
            return "";
        }

        /// <summary>
        /// HTTP POST Basic验证
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="body"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="method">POST</param>
        /// <param name="contentType">application/json</param>
        /// <returns></returns>
        public static string HttpPost_Basic(string requestUri, string body, string username, string password, string method = "POST", string contentType = "application/json")
        {
            string usernamePassword = username + ":" + password;
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));
            ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;//验证服务器证书回调自动验证  
            HttpResponseMessage httpWebResponse = client.PostAsync(requestUri, new StringContent(body, Encoding.UTF8, contentType)).Result;
            if (httpWebResponse.IsSuccessStatusCode)
            {
                return httpWebResponse.Content.ReadAsStringAsync().Result;
            }
            return "";
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            //为了通过证书验证，总是返回true
            return true;
        }
    }
}
