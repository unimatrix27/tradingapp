using System;
using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using trading.Models;

namespace trading.Persistence
{
    public class TradingDbContext : DbContextWithTriggers, ITransientTradingDbContext
    {
        public TradingDbContext(DbContextOptions<TradingDbContext> options)
            : base (options){

            }
        public TradingDbContext(){

        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=192.168.2.91; database=trading3;user id=unimatrix;password=lt5kee;MultipleActiveResultSets=true");
        }
        public DbSet<InstrumentName> InstrumentNames {get;set;}
        public DbSet<Broker> Brokers {get;set;}
        public DbSet<Currency> Currencies {get;set;}
        public DbSet<InstrumentType> InstrumentTypes {get;set;}
        public DbSet<Exchange> Exchanges {get;set;}
        public DbSet<TimeInterval> TimeIntervals {get;set;}
         public DbSet<BrokerTimeInterval> BrokerTimeIntervals {get;set;}
        public DbSet<BrokerSymbol> BrokerSymbols {get;set;}
        public DbSet<BrokerInstrument> BrokerInstruments {get;set;}
        public DbSet<PriceEntry> PriceEntries {get;set;}
        public DbSet<ScreenerType> ScreenerTypes {get;set;}
        public DbSet<ScreenerReferenceImage> ScreenerReferenceImages {get;set;}
        public DbSet<ScreenerLine> ScreenerLines {get;set;}
        public DbSet<ScreenerEntryType> ScreenerEntryTypes {get;set;}
        public DbSet<ScreenerEntryMapping> ScreenerEntryMappings {get;set;}
        public DbSet<ScreenerEntry> ScreenerEntries {get;set;}
        public DbSet<Screener> Screeners {get;set;}
        public DbSet<BrokerInstrumentScreenerType> BrokerInstrumentScreenerTypes { get; set; }
        public DbSet<IndicatorEntry> IndicatorEntries { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<SignalStep> SignalSteps { get; set; }
        public DbSet<StopLossRule> StopLossRules { get; set; }
        public DbSet<ExitRule> ExitRules { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TradeStep> TradeSteps { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder){
                      
            
            modelBuilder.Entity<InstrumentName>()
               .ToTable("InstrumentNames")
               .HasIndex(i => i.Name)
               .IsUnique();

            modelBuilder.Entity<InstrumentType>()
               .ToTable("InstrumentTypes")
               .HasIndex(i => i.Name)
               .IsUnique();   

            modelBuilder.Entity<Currency>()
               .ToTable("Currencies")
               .HasIndex(i => i.Name)
               .IsUnique();   
            modelBuilder.Entity<Broker>()
               .ToTable("Brokers")
               .HasIndex(i => i.Name)
               .IsUnique();   
            modelBuilder.Entity<Exchange>()
               .ToTable("Exchanges")
               .HasIndex(p => new {p.Name , p.BrokerId})
               .IsUnique();
            modelBuilder.Entity<TimeInterval>()
               .ToTable("TimeIntervals")
               .HasIndex(p => p.DurationTicks)
               .IsUnique();
            modelBuilder.Entity<BrokerInstrument>()
               .ToTable("BrokerInstruments");
            modelBuilder.Entity<BrokerSymbol>()
               .ToTable("BrokerSymbols")
               .HasIndex(p => p.Name);
            modelBuilder.Entity<PriceEntry>()
               .ToTable("PriceEntries")
               .HasIndex(p => new {p.BrokerInstrumentId, p.TimeIntervalId,p.TimeStamp})
               .IsUnique();
            modelBuilder.Entity<BrokerTimeInterval>()
                .ToTable("BrokerTimeIntervals")
                .HasIndex(p => new {p.TimeIntervalId, p.BrokerId})
                .IsUnique();
            modelBuilder.Entity<ScreenerType>()
               .ToTable("ScreenerTypes")
               .HasIndex(p => p.Name)
               .IsUnique();
            modelBuilder.Entity<ScreenerReferenceImage>()
               .ToTable("ScreenerReferenceImages")
               .HasIndex(p => p.ImageSignature)
               .IsUnique();
            modelBuilder.Entity<ScreenerLine>()
               .ToTable("ScreenerLines")
               .HasIndex(p => new {p.BrokerInstrumentId, p.ScreenerId})
               .IsUnique();
            modelBuilder.Entity<ScreenerEntryType>()
               .ToTable("ScreenerEntryTypes")
               .HasIndex(p => p.Name)
               .IsUnique();
           modelBuilder.Entity<ScreenerEntryMapping>()
               .ToTable("ScreenerEntryMappings")
               .HasIndex(p => new {p.ScreenerEntryTypeId,p.ScreenerTypeId})
               .IsUnique();
            modelBuilder.Entity<ScreenerEntry>()
               .ToTable("ScreenerEntries")
               .HasIndex(p => new {p.ScreenerEntryTypeId,p.ScreenerLineId})
               .IsUnique();
          modelBuilder.Entity<Screener>()
               .ToTable("Screeners")
               .HasIndex(p => p.NextId)
               .IsUnique();
            modelBuilder.Entity<Screener>()
               .HasIndex(p => p.PrevId)
               .IsUnique();
              modelBuilder.Entity<Screener>()
               .HasIndex(p => p.ImageFile)
               .IsUnique();

            modelBuilder.Entity<BrokerInstrumentScreenerType>()
                .ToTable("BrokerInstrumentScreenerTypes")
                .HasIndex(p => new {p.BrokerInstrumentId, p.ScreenerTypeId})
                .IsUnique();

            modelBuilder.Entity<IndicatorEntry>()
               .ToTable("IndicatorEntries")
               .HasIndex(p => new { p.PriceEntryId, p.Type })
               .IsUnique();




        }
    }
}