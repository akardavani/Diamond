using Diamond.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Diamond.DataStore
//{
//    public class MarketDataStore
//    {
//        private readonly DiamondDbContext _dbContext;
//        private ImmutableDictionary<string, Instrument> _instruments;
//        public DateTime InitializeDateTime { get; set; }
//        public MarketDataStore(CancellationToken cancellationToken = default)
//        {
//            _instruments = ImmutableDictionary<string, Instrument>.Empty;
//            Initialize(cancellationToken).GetAwaiter();
//        }

//        public async Task Initialize(CancellationToken cancellationToken)
//        {
//            //_logger.LogInformation("Instruments...");
//            await FetchInstruments(cancellationToken);
//            InitializeDateTime = DateTime.Now;
//        }

//        private async Task FetchInstruments(CancellationToken cancellation)
//        {
//            if (cancellation.IsCancellationRequested == false)
//            {
//                var instruments = await _dbContext
//                                .Set<Instrument>()
//                                .AsNoTracking()
//                                .ToListAsync(cancellation);

//                foreach (var instrument in instruments)
//                {
//                    _instruments.TryAdd(instrument.InstrumentId, instrument);
//                }


//                //using (var repo = _serviceProvider.GetService<InstrumentRepository>())
//                //{
//                //    var instruments = await repo.GetInstrumentsAsync(cancellationToken);

//                    //    _instruments = ImmutableDictionary<string, Instrument>.Empty;
//                    //    //_sectorWithInstruments.Clear();

//                    //    foreach (var instrument in instruments)
//                    //    {
//                    //        instrument.SectorCode = instrument.SectorCode.TrimAll();
//                    //        instrument.SubSectorCode = instrument.SubSectorCode.TrimAll();
//                    //        instrument.InstrumentPersianName = instrument.InstrumentPersianName.TrimAll();
//                    //        instrument.InstrumentId = instrument.InstrumentId.TrimAll();
//                    //        instrument.SearchString =
//                    //            $"{instrument.InstrumentPersianName}{instrument.CompanyName}{instrument.InstrumentMnemonic}{instrument.InstrumentName}"
//                    //                .Trim()
//                    //                .Replace(" ", "")
//                    //                .ToLower();

//                    //        SetInstrumentMarkets(instrument);

//                    //        ImmutableInterlocked.TryAdd(ref _instruments, instrument.InstrumentId, instrument); //Interlock is allowed only in Fetch Methods, please do not use in AddOrUpdate

//                    //        if (!string.IsNullOrWhiteSpace(instrument.SectorCode.TrimAll()))
//                    //            _sectorWithInstruments.AddOrUpdate(instrument.SectorCode.TrimAll(), sectorCode =>
//                    //            {
//                    //                var sectorWithInstruments = new SectorWithInstruments(instrument.SectorCode.TrimAll());
//                    //                sectorWithInstruments.Instruments.TryAdd(instrument.InstrumentId, 0);
//                    //                return sectorWithInstruments;
//                    //            },
//                    //                (key, value) =>
//                    //                {
//                    //                    value.Instruments.TryAdd(instrument.InstrumentId, 0);
//                    //                    return value;
//                    //                });
//                    //    }
//                    //}
//            }
//        }
//    }
//}
