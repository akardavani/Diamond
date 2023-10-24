using Diamond.Domain.Entities;
using Diamond.Domain.Models;
using Diamond.Domain.Models.InstrumentExtraInfo;
using Diamond.Domain.Setting;
using Diamond.Utils;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.TseTmcClient
{
    public class TseTmcClientService : IBusinessService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Settings _settings;

        public TseTmcClientService(IOptions<Settings> settings, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _settings= settings.Value;

            if (_settings != null)
            {
                if (string.IsNullOrWhiteSpace(_settings.TseTmcSettings.TseTmcLoaderUrl))
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

        public async Task<InstrumentExtraInfoResponse> GetTseTmcData(string insCode, CancellationToken cancellation = default)
        {
            const int maxItem = 15;
            if (_settings.TseTmcSettings == null) return new InstrumentExtraInfoResponse();
            var url = $"{_settings.TseTmcSettings.TseTmcLoaderUrl.Replace("@InsCode",insCode)}";

            var res = new InstrumentExtraInfoResponse();

            try
            {
                var my_text = await GetByGzip(url, cancellation);
                if (!string.IsNullOrWhiteSpace(my_text))
                {
                    //_logger.LogWarning($"DPClient {url} Response: {my_text}");
                    string[] lines = my_text.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );
                    var itemsCount = 0;
                    foreach (var line in lines)
                    {
                        if (line.Contains("EstimatedEPS"))
                        {
                            var containData = line.Split(",");

                            foreach (var item in containData)
                            {
                                if (item.Contains("EstimatedEPS"))
                                {
                                    res.Eps = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("SectorPE"))
                                {
                                    res.SectorPE = CalculateAmount(item, res);
                                    itemsCount++;
                                }

                                                                
                                if (item.Contains("InstrumentID"))
                                {
                                    res.InstrumentId = CalculateString(item, res);
                                    itemsCount++;
                                }
                                // 
                                if (item.Contains("BaseVol"))
                                {
                                    res.BaseVol = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("CSecVal"))
                                {
                                    var cs = CalculateString(item, res);
                                    res.IndustryGroupCode = Convert.ToInt32(cs);
                                    itemsCount++;
                                }
                                if (item.Contains("LSecVal"))
                                {
                                    res.IndustryGroup = CalculateString(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("PSGelStaMax"))
                                {
                                    res.AllowedPriceMax = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("PSGelStaMin"))
                                {
                                    res.AllowedPriceMin = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("Title"))
                                {
                                    res.Title = CalculateString(item, res);
                                    res.MarketName = res.Title.Split('-')[1].Trim();
                                    itemsCount++;
                                }

                                if (item.Contains("MinWeek"))
                                {
                                    res.MinWeek = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("MaxWeek"))
                                {
                                    res.MaxWeek = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("MinYear"))
                                {
                                    res.MinYear = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("MaxYear"))
                                {
                                    res.MaxYear = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("QTotTran5JAvg"))
                                {
                                    res.AverageMonthVolume = CalculateAmount(item, res);
                                    itemsCount++;
                                }
                                if (item.Contains("KAjCapValCpsIdx"))
                                {
                                    res.FloatingShares = CalculateAmount(item, res);
                                    itemsCount++;
                                }

                                if (itemsCount >= maxItem)
                                    return res;
                            }
                        }
                    }
                }
                else
                {
                    //_logger.LogWarning($"Response of {url} is null");
                    return null;
                }

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"{ex.Message} --- {ex.StackTrace ?? string.Empty}");
                throw;
            }
        }

        public async Task<List<InstrumentListModel>> GetInstrumentList(CancellationToken cancellation = default)
        {
            if (_settings.TseTmcSettings == null) return new List<InstrumentListModel>();
            var url = _settings.TseTmcSettings.InstrumentList;

            try
            {
                HtmlDocument document = new HtmlDocument();                

                var urls = await GetUrls(url,cancellation);
                var instruments = new List<InstrumentListModel>();

                foreach (var newUrl in urls)
                {
                    var newText = await GetByGzip(newUrl, cancellation);
                    document.LoadHtml(newText);
                    var count = 0;
                    foreach (var row in document.DocumentNode.SelectNodes("//*[@id='tblToGrid']//tr"))
                    {
                        count++;
                        if (count == 1)
                            continue;
                        var nodes = row.SelectNodes("td");
                        if (nodes != null)
                        {
                            instruments.Add(new InstrumentListModel
                            {
                                InstrumentId = nodes[0].InnerText,
                                InstrumentGroupId = nodes[1].InnerText,
                                InstrumentSectorGroup = nodes[2].InnerText,
                                Board = nodes[3].InnerText,
                                InstrumentPersianName = nodes[6].InnerText.Split(',')[1],
                                InstrumentName = nodes[7].InnerText.Split(',')[1],
                                InsCode = nodes[6].InnerText.Split(',')[0]
                            });
                        }
                    }
                }
                return instruments;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"{ex.Message} --- {ex.StackTrace ?? string.Empty}");
                throw;
            }
        }
               
        public async Task<InstrumentExtraInfo> GetTseTmcNav(string insCode, CancellationToken cancellation = default)
        {
            if (_settings.TseTmcSettings == null) return new InstrumentExtraInfo();
            var url = _settings.TseTmcSettings.TseTmcInfo.Replace("@InsCode", insCode);

            var res = new InstrumentExtraInfo();

            try
            {
                var my_text = await GetByGzip(url, cancellation);
                if (!string.IsNullOrWhiteSpace(my_text))
                {
                    //_logger.LogWarning($"DPClient {url} Response: {my_text}");
                    var split_my_text = my_text.Split(",");

                    if (split_my_text[14] != "")
                    {
                        var data = split_my_text[14].ToString();

                        var splitdate = data.Split(" ")[0];
                        var splittime = data.Split(" ")[1];

                        var year = splitdate.Split("/")[0];
                        var month = splitdate.Split("/")[1];
                        var day = splitdate.Split("/")[2];

                        if (Convert.ToInt32(month) < 10)
                            month = $"0{Convert.ToInt32(month)}";
                        if (Convert.ToInt32(day) < 10)
                            day = $"0{Convert.ToInt32(day)}";

                        var date = $@"{year}/{month}/{day} {splittime}";

                        var datetime = date.PersianToGregorianDateTime();
                        res.DateTime = datetime;
                    }
                    if (split_my_text[15] != "")
                    {
                        var nav = split_my_text[15].Split(";");
                        if (nav[0] != "")
                        {
                            res.Nav = Convert.ToDecimal(nav[0]);
                            return res;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                {
                    //_logger.LogWarning($"Response of {url} is null");
                    return null;
                }

                return res;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"{ex.Message} --- {ex.StackTrace ?? string.Empty}");
                throw;
            }
        }

        private async Task<List<string>> GetUrls(string url, CancellationToken cancellation)
        {
            var my_text = await GetByGzip(url, cancellation);

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(my_text);

            var list = document.DocumentNode.SelectNodes("//a")
              .Select(p => p.GetAttributeValue("href", "not found"))
              .ToList();

            var urls = new List<string>();
            foreach (var item in list)
            {
                var split = item.Split('&')[1];
                var newUrl = $"{url}&{split}";
                urls.Add(newUrl);
            }

            return urls;
        }

        private decimal CalculateAmount(string item, InstrumentExtraInfoResponse res)
        {
            var max = item.Replace("'", "").Split("=");
            if (max[1].Split(".")[0] != "")
            {
                return Convert.ToDecimal(max[1]);
            }

            return 0;
        }

        private string CalculateString(string item, InstrumentExtraInfoResponse res)
        {
            var max = item.Replace("'", "").Split("=");
            if (max[1].Split(".")[0] != "")
            {
                return max[1];
            }

            return string.Empty;
        }
        private async Task<string> GetByGzip(string url, CancellationToken cancellationToken)
        {
            
            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Add("Accept-Encoding", new string[] { "gzip" });
            var request = await client.GetAsync(url, cancellationToken);
            var responseStream = await request.Content.ReadAsStreamAsync();
            using var gsr = new GZipStream(responseStream, CompressionMode.Decompress);
            using var reader = new StreamReader(gsr);
            return reader.ReadToEnd();

        }
    }
}
