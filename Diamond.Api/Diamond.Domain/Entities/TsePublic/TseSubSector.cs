using Newtonsoft.Json;

namespace Diamond.Domain.Entities.TsePublic
{
    public partial class TseSubSector
    {
        [JsonProperty("CSecVal")]
        public string SectorCode { get; set; }

        [JsonProperty("CSoSecVal")]
        public string SubSectorCode { get; set; }

        [JsonProperty("LSoSecVal")]
        public string SubSectorName { get; set; }

        [JsonProperty("DEven")]
        public string Date { get; set; }
    }
}
