﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uzd2.Datatypes;

#nullable disable

namespace Uzd2.Migrations
{
    [DbContext(typeof(Uzd2Context))]
    [Migration("20240918072449_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Uzd2.Datatypes.Dzivoklis", b =>
                {
                    b.Property<long>("DzivNumurs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DzivNumurs"));

                    b.Property<double?>("DzivPlatiba")
                        .HasColumnType("float");

                    b.Property<int?>("IeiedzSkaits")
                        .HasColumnType("int");

                    b.Property<int?>("IstabSkaits")
                        .HasColumnType("int");

                    b.Property<long>("MajaID")
                        .HasColumnType("bigint");

                    b.Property<double?>("Platiba")
                        .HasColumnType("float");

                    b.Property<int?>("Stavs")
                        .HasColumnType("int");

                    b.HasKey("DzivNumurs");

                    b.HasIndex("MajaID");

                    b.ToTable("DzivoklisItems");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Iedzivotajs", b =>
                {
                    b.Property<long>("PersKods")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PersKods"));

                    b.Property<DateOnly?>("DzimDat")
                        .HasColumnType("date");

                    b.Property<long>("DzivNumurs")
                        .HasColumnType("bigint");

                    b.Property<long?>("DzivoklisDzivNumurs")
                        .HasColumnType("bigint");

                    b.Property<string>("Uzvards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vards")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersKods");

                    b.HasIndex("DzivoklisDzivNumurs");

                    b.ToTable("IedzivotajsItems");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Maja", b =>
                {
                    b.Property<long>("MajaNumurs")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MajaNumurs"));

                    b.Property<string>("Iela")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PastIndeks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pilseta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Valsts")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajaNumurs");

                    b.ToTable("MajaItems");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Dzivoklis", b =>
                {
                    b.HasOne("Uzd2.Datatypes.Maja", "Maja")
                        .WithMany()
                        .HasForeignKey("MajaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maja");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Iedzivotajs", b =>
                {
                    b.HasOne("Uzd2.Datatypes.Dzivoklis", "Dzivoklis")
                        .WithMany()
                        .HasForeignKey("DzivoklisDzivNumurs");

                    b.Navigation("Dzivoklis");
                });
#pragma warning restore 612, 618
        }
    }
}
