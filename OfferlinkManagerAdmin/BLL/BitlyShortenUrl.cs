using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Web;

namespace BLL
{
    public class BitlyShortenUrl
    {
        static AuthInfo _auth;

        public BitlyShortenUrl()
        {
            _auth = new AuthInfo();
        }

        public  string ShortenUrl(string urlToShorten)
        {
            string statusCode = string.Empty;// The variable which we will be storing the status code of the server response
            string statusText = string.Empty;// The variable which we will be storing the status text of the server response
            string shortUrl = string.Empty;// The variable which we will be storing the shortened url
            string longUrl = string.Empty;// The variable which we will be storing the long url
            try
            {
                XmlDocument xmlDoc = new XmlDocument();// The xml document which we will use to parse the response from the server
                WebRequest request = WebRequest.Create("http://api.bitly.com/v3/shorten");

                byte[] data = Encoding.UTF8.GetBytes(string.Format("login={0}&apiKey={1}&longUrl={2}&format={3}",
                    _auth.BITLYUSERNAME,                             // Your username
                    _auth.APIKEY,                              // Your API key
                    HttpUtility.UrlEncode(urlToShorten),         // Encode the url we want to shorten
                    "xml"));                                     // The format of the response we want the server to reply with

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (Stream ds = request.GetRequestStream())
                {
                    ds.Write(data, 0, data.Length);
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        xmlDoc.LoadXml(sr.ReadToEnd());
                    }
                }
                statusCode = xmlDoc.GetElementsByTagName("status_code")[0].InnerText;
                statusText = xmlDoc.GetElementsByTagName("status_txt")[0].InnerText;
                shortUrl = xmlDoc.GetElementsByTagName("url")[0].InnerText;
                longUrl = xmlDoc.GetElementsByTagName("long_url")[0].InnerText;
            }
            catch (Exception ex)
            {
                shortUrl = "";
                CommonLib.ExceptionHandler.WriteLog(CommonLib.Sections.BLL, "BitlyShortenUrl.cs ShortenUrl()", ex);
            }
            return shortUrl;
        }
    }
}
