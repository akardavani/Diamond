using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Enums;
using Diamond.Domain.Models;
using Diamond.Domain.Setting;
using Diamond.Utils.BrokerExtention;
using Diamond.Utils.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Diamond.Services.CandelClient
{
    public class CandelClientService : IBusinessService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Settings _settings;

        public CandelClientService(IOptions<Settings> settings, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;

            if (_settings != null)
            {
                if (string.IsNullOrWhiteSpace(_settings.CandelSettings.HistoryUrl))
                {
                    //_logger.LogError("Base url is empty");
                    _settings = null;
                }
            }
            else
            {
                //_logger.LogError("Can not read ");
            }
        }



        public async Task<CandelDataModel> GetDataByUrl(string symbol, TimeframeEnum timeframe)
        {
            try
            {
                var candelModels = new List<CandelModel>();
                var resolution = NahayatnegarExtention.GetTimeFrame(timeframe);
                var from = BrokerExtention.DateTimeToUnixTimestamp(DateTime.Now.AddYears(-15));
                var to = BrokerExtention.DateTimeToUnixTimestamp(DateTime.Now);

                var adjustmentType = "3";

                var url = _settings.CandelSettings.HistoryUrl
                    .Replace("@Symbol", symbol + adjustmentType)
                    .Replace("@Resolution", resolution)
                    .Replace("@From", from)
                    .Replace("@To", to)
                    .Replace("@AdjustmentType", adjustmentType);

                candelModels = await GetDataByUrl(url);

                return new CandelDataModel
                {
                    Candels = candelModels,
                    Symbol = symbol,
                    Timeframe = timeframe
                };
            }
            catch (Exception ex)
            {
                return new CandelDataModel();
            }
        }

        public async Task<List<CandelModel>> GetDataByUrl(string url)
        {
            try
            {
                var data = await GetDataFromUrl.Get(url);
                
                var model = new List<CandelModel>();

                if (data != null)
                {
                    for (int i = 0; i < data["o"].Count; i++)
                    {
                        model.Add(new CandelModel()
                        {
                            Timestamp = Convert.ToInt64(data["t"][i]),
                            Date = BrokerExtention.UnixTimeStampToDateTime(Convert.ToInt64(data["t"][i])), 
                            Open = Convert.ToDecimal((string)data["o"][i]),
                            High = Convert.ToDecimal((string)data["h"][i]),
                            Low = Convert.ToDecimal((string)data["l"][i]),
                            Close = Convert.ToDecimal((string)data["c"][i]),
                            Volume = Convert.ToDecimal((string)data["v"][i])
                        });
                    }
                }                
                
                return model.OrderBy(x => x.Timestamp).ToList();

            }
            catch (Exception e)
            {
                return new List<CandelModel>();
                //Extention.Logging(Environment.NewLine + " Error => " + e.Message +
                //Environment.NewLine + url);
            }
        }

        private Dictionary<int, string> CreateDic(dynamic json)
        {
            var dic = new Dictionary<int, string>();

            var split_my_text = json.ToString().Split(",");
            var count = 0;
            try
            {
                foreach (var split in split_my_text)
                {
                    var s1 = (string)split;

                    var newText = s1.Replace("[", "").Replace("\"", "").Replace(Environment.NewLine, "").Replace(" ", "");
                    var key = count;
                    var val = newText;

                    dic.Add(key, val);
                    count++;
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return dic;
        }
        //private async Task<List<CandelModel>> CreateData(string url)
        //{
        //    var httpRequest = (HttpWebRequest)WebRequest.Create(url);

        //    httpRequest.PreAuthenticate = true;

        //    httpRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";

        //    var my_text = "";

        //    try
        //    {
        //        using (var resp = httpRequest.GetResponseAsync().Result)
        //        {
        //            using (var str = resp.GetResponseStream())
        //            using (var gsr = new GZipStream(str, CompressionMode.Decompress))
        //            using (var sr = new StreamReader(gsr))

        //            {
        //                my_text = await sr.ReadToEndAsync();
        //            }
        //        }

        //        dynamic json = JsonConvert.DeserializeObject(my_text);

        //        var open = json["o"];
        //        var close = json["c"];
        //        var low = json["l"];
        //        var high = json["h"];
        //        var date = json["t"];
        //        var volume = json["v"];

        //        var model = new List<CandelModel>();

        //        var count_o = Enumerable.Count(open);
        //        for (int i = 0; i < count_o; i++)
        //        {
        //            model.Add(new CandelModel()
        //            {
        //                UnixTimestamp = Convert.ToInt64(date[i]),
        //                Date = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(date[i])).DateTime,
        //                Open = Convert.ToDecimal(open[i]),
        //                Close = Convert.ToDecimal(close[i]),
        //                High = Convert.ToDecimal(high[i]),
        //                Low = Convert.ToDecimal(low[i]),
        //                Volume = Convert.ToDecimal(volume[i])
        //            });
        //        }

        //        return model.OrderBy(x => x.UnixTimestamp).ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}

        //public static string DateTimeToUnixTimestamp(DateTime dateTime)
        //{
        //    var ss = (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
        //           new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;

        //    return string.Format("{0}", (long)ss);
        //}
    }
}
