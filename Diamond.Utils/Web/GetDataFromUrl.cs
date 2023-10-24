using Newtonsoft.Json;
using System.IO.Compression;
using System.Net;
using System.Security.Policy;

namespace Diamond.Utils.Web
{
    public static class GetDataFromUrl
    {
        public static async Task<dynamic> Get(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.PreAuthenticate = true;

            httpRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";

            var my_text = "";

            try
            {
                using (var resp = await httpRequest.GetResponseAsync())
                {
                    using (var str = resp.GetResponseStream())
                    using (var gsr = new GZipStream(str, CompressionMode.Decompress))
                    using (var sr = new StreamReader(gsr))

                    {
                        my_text = await sr.ReadToEndAsync();
                    }
                }

                dynamic data = JsonConvert.DeserializeObject(my_text);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static async Task<dynamic> Get2(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            //httpRequest.PreAuthenticate = true;

            httpRequest.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";
            httpRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";

            var my_text = "";

            try
            {
                using (var resp = await httpRequest.GetResponseAsync())
                {
                    using (var str = resp.GetResponseStream())
                    using (var gsr = new GZipStream(str, CompressionMode.Decompress))
                    using (var sr = new StreamReader(gsr))

                    {
                        my_text = await sr.ReadToEndAsync();
                    }
                }

                dynamic data = JsonConvert.DeserializeObject(my_text);

                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static async Task<dynamic> GetInvestingUrl(string url)
        {
            try
            {
                //var _baseUri = new Uri(url);

                //var cookieContainer = new CookieContainer();

                //foreach (var item in cookies)
                //{
                //    cookieContainer.Add(@"__cf_bm=hEvfndGfVeImbDRg7KcajrfOSEP2t1pOHUm.fETtpIo-1688549745-0-AR4NVC+MShzyR5P+g62DEhznRlXvfh4rz/yvjGPRbf472uj+/htVnoyNN2Pz8Bop3v/IAa4ORTd9byWUmxi3yoc=; __cflb=02DiuEaBtsFfH7bEbN4priA7DVv4KqV9Xbp5QZgQC1VUg");
                //}

                //var handler = new HttpClientHandler()
                //{
                //    CookieContainer = cookieContainer
                //};

                //var client = new HttpClient(handler: handler, disposeHandler: true)
                //{
                //    BaseAddress = _baseUri
                //};

                //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/114.0");
                //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

                //var response = await client.GetAsync("");

                //var content = await response.Content.ReadAsStreamAsync();
                var my_text = "";


                //using (var gsr = new GZipStream(content, CompressionMode.Decompress))
                //using (var sr = new StreamReader(gsr))
                //{
                //    my_text = await sr.ReadToEndAsync();
                //}

                return my_text;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}
