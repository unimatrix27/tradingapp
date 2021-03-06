﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using trading.Models;
using trading.Persistence;

namespace Trading.Migrations
{
    [DbContext(typeof(TradingDbContext))]
    [Migration("20170614131943_AddUseTodayToScreenerTypes")]
    partial class AddUseTodayToScreenerTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-preview1-24937")
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

            modelBuilder.Entity("trading.Models.BrokerInstrumentScreenerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerInstrumentId");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("ScreenerTypeId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ScreenerTypeId");

                    b.HasIndex("BrokerInstrumentId", "ScreenerTypeId")
                        .IsUnique();

                    b.ToTable("BrokerInstrumentScreenerTypes");
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

            modelBuilder.Entity("trading.Models.BrokerTimeInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerId");

                    b.Property<string>("BrokerName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("TimeIntervalId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<int>("dataPriority");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("TimeIntervalId", "BrokerId")
                        .IsUnique();

                    b.ToTable("BrokerTimeIntervals");
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

            modelBuilder.Entity("trading.Models.ExitRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("Amount");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("ExitType");

                    b.Property<int>("RuleRelation");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<decimal?>("Value");

                    b.HasKey("Id");

                    b.ToTable("ExitRules");
                });

            modelBuilder.Entity("trading.Models.IndicatorEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<decimal>("Data");

                    b.Property<bool>("IsDirty");

                    b.Property<int>("PriceEntryId");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("PriceEntryId", "Type")
                        .IsUnique();

                    b.ToTable("IndicatorEntries");
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

            modelBuilder.Entity("trading.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrokerOrderId");

                    b.Property<decimal?>("Costs");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("Direction");

                    b.Property<decimal?>("ExecutedLevel");

                    b.Property<int>("Function");

                    b.Property<decimal?>("LimitLevel");

                    b.Property<int>("Size");

                    b.Property<int>("State");

                    b.Property<decimal?>("StopLevel");

                    b.Property<int>("TradeId");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<DateTimeOffset?>("ValidAfter");

                    b.HasKey("Id");

                    b.HasIndex("TradeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("trading.Models.PriceEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerInstrumentId");

                    b.Property<decimal>("Close");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<decimal>("High");

                    b.Property<bool>("IsFinished");

                    b.Property<decimal>("Low");

                    b.Property<decimal>("Open");

                    b.Property<int>("TimeIntervalId");

                    b.Property<DateTimeOffset>("TimeStamp");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<long?>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("TimeIntervalId");

                    b.HasIndex("BrokerInstrumentId", "TimeIntervalId", "TimeStamp")
                        .IsUnique();

                    b.ToTable("PriceEntries");
                });

            modelBuilder.Entity("trading.Models.Screener", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("ImageFile");

                    b.Property<string>("ImageHash");

                    b.Property<bool>("IsProcessed");

                    b.Property<int?>("NextId");

                    b.Property<bool>("ParseError");

                    b.Property<string>("ParseErrorString");

                    b.Property<int?>("PrevId");

                    b.Property<int>("ScreenerTypeId");

                    b.Property<DateTimeOffset>("TimeStamp");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ImageFile")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[ImageFile] IS NOT NULL");

                    b.HasIndex("NextId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[NextId] IS NOT NULL");

                    b.HasIndex("PrevId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[PrevId] IS NOT NULL");

                    b.HasIndex("ScreenerTypeId");

                    b.ToTable("Screeners");
                });

            modelBuilder.Entity("trading.Models.ScreenerEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Bg");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("Fg");

                    b.Property<int>("ScreenerEntryTypeId");

                    b.Property<int>("ScreenerLineId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<int?>("value");

                    b.HasKey("Id");

                    b.HasIndex("ScreenerLineId");

                    b.HasIndex("ScreenerEntryTypeId", "ScreenerLineId")
                        .IsUnique();

                    b.ToTable("ScreenerEntries");
                });

            modelBuilder.Entity("trading.Models.ScreenerEntryMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("ScreenerEntryTypeId");

                    b.Property<int>("ScreenerTypeId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<bool>("active");

                    b.Property<int>("position");

                    b.HasKey("Id");

                    b.HasIndex("ScreenerTypeId");

                    b.HasIndex("ScreenerEntryTypeId", "ScreenerTypeId")
                        .IsUnique();

                    b.ToTable("ScreenerEntryMappings");
                });

            modelBuilder.Entity("trading.Models.ScreenerEntryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ScreenerEntryTypes");
                });

            modelBuilder.Entity("trading.Models.ScreenerLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerInstrumentId");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("ScreenerId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ScreenerId");

                    b.HasIndex("BrokerInstrumentId", "ScreenerId")
                        .IsUnique();

                    b.ToTable("ScreenerLines");
                });

            modelBuilder.Entity("trading.Models.ScreenerReferenceImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrokerInstrumentId");

                    b.Property<int>("CellColor");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<string>("ImageSignature");

                    b.Property<int>("ScreenerTypeId");

                    b.Property<bool>("Unused");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("BrokerInstrumentId");

                    b.HasIndex("ImageSignature")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[ImageSignature] IS NOT NULL");

                    b.HasIndex("ScreenerTypeId");

                    b.ToTable("ScreenerReferenceImages");
                });

            modelBuilder.Entity("trading.Models.ScreenerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<DateTime>("LastCheck");

                    b.Property<string>("LastHash");

                    b.Property<string>("LastResult");

                    b.Property<int?>("MarkerBlue");

                    b.Property<int?>("MarkerGreen");

                    b.Property<int?>("MarkerRed");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("NameColumn");

                    b.Property<string>("Path");

                    b.Property<int?>("TimeIntervalId");

                    b.Property<string>("URL");

                    b.Property<long>("UpdateFrequencyTicks");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<bool>("UseToday");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Filter", "[Name] IS NOT NULL");

                    b.HasIndex("TimeIntervalId");

                    b.ToTable("ScreenerTypes");
                });

            modelBuilder.Entity("trading.Models.Signal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BrokerInstrumentId");

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("SignalType");

                    b.Property<int>("TimeIntervalId");

                    b.Property<int>("TradeDirection");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("BrokerInstrumentId");

                    b.HasIndex("TimeIntervalId");

                    b.ToTable("Signals");
                });

            modelBuilder.Entity("trading.Models.SignalStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<decimal?>("Entry");

                    b.Property<int?>("EntryType");

                    b.Property<int?>("ExitType");

                    b.Property<int>("PriceEntryId");

                    b.Property<string>("Reason");

                    b.Property<int>("SignalId");

                    b.Property<int>("SignalStepType");

                    b.Property<decimal?>("Sl");

                    b.Property<int?>("SlType");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("PriceEntryId");

                    b.HasIndex("SignalId");

                    b.ToTable("SignalSteps");
                });

            modelBuilder.Entity("trading.Models.StopLossRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("NewRuleRelation");

                    b.Property<decimal?>("NewValue");

                    b.Property<int>("RuleRelation");

                    b.Property<int>("SlRuleType");

                    b.Property<int>("SlType");

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<decimal?>("Value");

                    b.HasKey("Id");

                    b.ToTable("StopLossRules");
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

            modelBuilder.Entity("trading.Models.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<int>("SignalId");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("SignalId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("trading.Models.TradeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<decimal?>("EntryLevel");

                    b.Property<decimal?>("ProfitLevel");

                    b.Property<int?>("Reason");

                    b.Property<int>("Result");

                    b.Property<int?>("Size");

                    b.Property<decimal?>("StopLevel");

                    b.Property<int>("TradeId");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("TradeId");

                    b.ToTable("TradeSteps");
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

            modelBuilder.Entity("trading.Models.BrokerInstrumentScreenerType", b =>
                {
                    b.HasOne("trading.Models.BrokerInstrument", "BrokerInstrument")
                        .WithMany("BrokerInstrumentScreenerTypes")
                        .HasForeignKey("BrokerInstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.ScreenerType", "ScreenerType")
                        .WithMany("BrokerInstrumentScreenerTypes")
                        .HasForeignKey("ScreenerTypeId")
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

            modelBuilder.Entity("trading.Models.BrokerTimeInterval", b =>
                {
                    b.HasOne("trading.Models.Broker", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.TimeInterval", "TimeInterval")
                        .WithMany("BrokerTimeIntervals")
                        .HasForeignKey("TimeIntervalId")
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

            modelBuilder.Entity("trading.Models.IndicatorEntry", b =>
                {
                    b.HasOne("trading.Models.PriceEntry", "PriceEntry")
                        .WithMany("IndicatorEntries")
                        .HasForeignKey("PriceEntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.Order", b =>
                {
                    b.HasOne("trading.Models.Trade", "Trade")
                        .WithMany("Orders")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.PriceEntry", b =>
                {
                    b.HasOne("trading.Models.BrokerInstrument", "BrokerInstrument")
                        .WithMany("PriceEntries")
                        .HasForeignKey("BrokerInstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.TimeInterval", "TimeInterval")
                        .WithMany()
                        .HasForeignKey("TimeIntervalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.Screener", b =>
                {
                    b.HasOne("trading.Models.ScreenerType", "ScreenerType")
                        .WithMany("Screeners")
                        .HasForeignKey("ScreenerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.ScreenerEntry", b =>
                {
                    b.HasOne("trading.Models.ScreenerEntryType", "ScreenerEntryType")
                        .WithMany()
                        .HasForeignKey("ScreenerEntryTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.ScreenerLine", "ScreenerLine")
                        .WithMany("ScreenerEntries")
                        .HasForeignKey("ScreenerLineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.ScreenerEntryMapping", b =>
                {
                    b.HasOne("trading.Models.ScreenerEntryType", "ScreenerEntryType")
                        .WithMany("ScreenerEntryMappings")
                        .HasForeignKey("ScreenerEntryTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.ScreenerType", "ScreenerType")
                        .WithMany("ScreenerEntryMappings")
                        .HasForeignKey("ScreenerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.ScreenerLine", b =>
                {
                    b.HasOne("trading.Models.BrokerInstrument", "BrokerInstrument")
                        .WithMany("ScreenerLines")
                        .HasForeignKey("BrokerInstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.Screener", "Screener")
                        .WithMany("ScreenerLines")
                        .HasForeignKey("ScreenerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.ScreenerReferenceImage", b =>
                {
                    b.HasOne("trading.Models.BrokerInstrument", "BrokerInstrument")
                        .WithMany()
                        .HasForeignKey("BrokerInstrumentId");

                    b.HasOne("trading.Models.ScreenerType", "ScreenerType")
                        .WithMany("ScreenerReferenceImages")
                        .HasForeignKey("ScreenerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.ScreenerType", b =>
                {
                    b.HasOne("trading.Models.TimeInterval", "TimeInterval")
                        .WithMany()
                        .HasForeignKey("TimeIntervalId");
                });

            modelBuilder.Entity("trading.Models.Signal", b =>
                {
                    b.HasOne("trading.Models.BrokerInstrument", "BrokerInstrument")
                        .WithMany()
                        .HasForeignKey("BrokerInstrumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.TimeInterval", "TimeInterval")
                        .WithMany()
                        .HasForeignKey("TimeIntervalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.SignalStep", b =>
                {
                    b.HasOne("trading.Models.PriceEntry", "PriceEntry")
                        .WithMany()
                        .HasForeignKey("PriceEntryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("trading.Models.Signal", "Signal")
                        .WithMany("SignalSteps")
                        .HasForeignKey("SignalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.Trade", b =>
                {
                    b.HasOne("trading.Models.Signal", "Signal")
                        .WithMany("Trades")
                        .HasForeignKey("SignalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("trading.Models.TradeStep", b =>
                {
                    b.HasOne("trading.Models.Trade", "Trade")
                        .WithMany("TradeSteps")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
