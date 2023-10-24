namespace Diamond.Domain.Models.TseInstrument
{
    public class GetAllTseInstrumentRequest : IPageableRequest
    {
        public string? InstrumentId { get; set; }
        public bool Valid { get; set; }
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 0;
        public string Sort { get; set; }
    }
}
