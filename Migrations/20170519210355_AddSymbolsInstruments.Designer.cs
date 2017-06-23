using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using trading.Persistence;

namespace Trading.Migrations
{
    [DbContext(typeof(TradingDbContext))]
    [Migration("20170519210355_AddSymbolsInstruments")]
    partial class AddSymbolsInstruments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("trading.Models.Broker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ShortName")
                        .HasMaxLength(2);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("trading.Models.BrokerInstrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerSymbolId");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("InstrumentTypeId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<string>("expiry")
                        .HasMaxLength(50);

                    b.Property<int?>("multiplicator");

                    b.HasKey("Id");

                    b.HasIndex("BrokerSymbolId");

                    b.HasIndex("InstrumentTypeId");

                    b.ToTable("BrokerInstruments");
                });

            modelBuilder.Entity("trading.Models.BrokerSymbol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("ExchangeId");

                    b.Property<int>("InstrumentNameId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ExchangeId");

                    b.HasIndex("InstrumentNameId");

                    b.HasIndex("Name");

                    b.ToTable("BrokerSymbols");
                });

            modelBuilder.Entity("trading.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<decimal>("Rate");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("trading.Models.Exchange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerId");

                    b.Property<TimeSpan>("CloseTime");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("CurrencyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<TimeSpan>("OpenTime");

                    b.Property<string>("TimeZoneId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("Name", "BrokerId")
                        .IsUnique();

                    b.ToTable("Exchanges");
                });

            modelBuilder.Entity("trading.Models.InstrumentName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("InstrumentNames");
                });

            modelBuilder.Entity("trading.Models.InstrumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("InstrumentTypes");
                });

            modelBuilder.Entity("trading.Models.TimeInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<long>("DurationTicks");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("DurationTicks")
                        .IsUnique();

                    b.ToTable("TimeIntervals");
                });

            modelBuilder.Entity("trading.Models.BrokerInstrument", b =>
                {
                    b.HasOne("trading.Models.BrokerSymbol", "BrokerSymbol")
                        .WithMany("BrokerInstruments")
                        .HasForeignKey("BrokerSymbolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.InstrumentType", "InstrumentType")
                        .WithMany()
                        .HasForeignKey("InstrumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.BrokerSymbol", b =>
                {
                    b.HasOne("trading.Models.Exchange", "Exchange")
                        .WithMany()
                        .HasForeignKey("ExchangeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.InstrumentName", "InstrumentName")
                        .WithMany("BrokerSymbols")
                        .HasForeignKey("InstrumentNameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.Exchange", b =>
                {
                    b.HasOne("trading.Models.Broker", "Broker")
                        .WithMany("Exchanges")
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.Currency", "Currency")
                        .WithMany("Exchanges")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
