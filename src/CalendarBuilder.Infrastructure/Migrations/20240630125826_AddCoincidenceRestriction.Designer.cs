﻿// <auto-generated />
using System;
using CalendarBuilder.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CalendarBuilder.Infrastructure.Migrations
{
    [DbContext(typeof(CalendarBuilderDbContext))]
    [Migration("20240630125826_AddCoincidenceRestriction")]
    partial class AddCoincidenceRestriction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoincidenceRestriction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FirstSportId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SecondSportId")
                        .HasColumnType("uuid");

                    b.Property<int>("SessionsGap")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstSportId");

                    b.HasIndex("SecondSportId");

                    b.ToTable("CoincidenceRestrictions");
                });

            modelBuilder.Entity("Sport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("CoincidenceRestriction", b =>
                {
                    b.HasOne("Sport", "FirstSport")
                        .WithMany()
                        .HasForeignKey("FirstSportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sport", "SecondSport")
                        .WithMany()
                        .HasForeignKey("SecondSportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirstSport");

                    b.Navigation("SecondSport");
                });
#pragma warning restore 612, 618
        }
    }
}
