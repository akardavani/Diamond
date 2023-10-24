using Newtonsoft.Json;
using System.IO.Compression;
using System.Net;

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
    }
}
