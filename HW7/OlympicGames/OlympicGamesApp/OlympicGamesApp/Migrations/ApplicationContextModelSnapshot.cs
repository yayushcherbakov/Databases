// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OlympicGamesApp.Database;

#nullable disable

namespace OlympicGamesApp.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Country", b =>
                {
                    b.Property<string>("CountryId")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("country_id");

                    b.Property<int>("AreaSqkm")
                        .HasColumnType("integer")
                        .HasColumnName("area_sqkm");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("name");

                    b.Property<int>("Population")
                        .HasColumnType("integer")
                        .HasColumnName("population");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Event", b =>
                {
                    b.Property<string>("EventId")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("event_id");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("eventtype");

                    b.Property<bool>("IsTeamEvent")
                        .HasColumnType("boolean")
                        .HasColumnName("is_team_event");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("name");

                    b.Property<int>("NumPlayersInTeam")
                        .HasColumnType("integer")
                        .HasColumnName("num_players_in_team");

                    b.Property<string>("OlympicId")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("olympic_id");

                    b.Property<string>("ResultNotedIn")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("result_noted_in");

                    b.HasKey("EventId");

                    b.HasIndex("OlympicId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Olympic", b =>
                {
                    b.Property<string>("OlympicId")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("olympic_id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("enddate");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("startdate");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("OlympicId");

                    b.HasIndex("CountryId");

                    b.ToTable("Olympics");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Player", b =>
                {
                    b.Property<string>("PlayerId")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("player_id");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("country_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("name");

                    b.HasKey("PlayerId");

                    b.HasIndex("CountryId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("result_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ResultId"));

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("event_id");

                    b.Property<string>("Medal")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("medal");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("player_id");

                    b.Property<float>("Score")
                        .HasColumnType("real")
                        .HasColumnName("result");

                    b.HasKey("ResultId");

                    b.HasIndex("EventId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Event", b =>
                {
                    b.HasOne("OlympicGamesApp.Database.Entities.Olympic", "Olympic")
                        .WithMany("Events")
                        .HasForeignKey("OlympicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Olympic");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Olympic", b =>
                {
                    b.HasOne("OlympicGamesApp.Database.Entities.Country", "Country")
                        .WithMany("Olympics")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Player", b =>
                {
                    b.HasOne("OlympicGamesApp.Database.Entities.Country", "Country")
                        .WithMany("Players")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Result", b =>
                {
                    b.HasOne("OlympicGamesApp.Database.Entities.Event", "Event")
                        .WithMany("Results")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OlympicGamesApp.Database.Entities.Player", "Player")
                        .WithMany("Results")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Country", b =>
                {
                    b.Navigation("Olympics");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Event", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Olympic", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("OlympicGamesApp.Database.Entities.Player", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
