﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uzd2.Datatypes;

#nullable disable

namespace Uzd2.Migrations
{
    [DbContext(typeof(Uzd2Context))]
    partial class Uzd2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasPrecision(18, 2)
                        .HasColumnType("float(18)");

                    b.Property<int?>("IeiedzSkaits")
                        .HasColumnType("int");

                    b.Property<int?>("IstabSkaits")
                        .HasColumnType("int");

                    b.Property<long>("MajaID")
                        .HasColumnType("bigint");

                    b.Property<double?>("Platiba")
                        .HasPrecision(18, 2)
                        .HasColumnType("float(18)");

                    b.Property<int?>("Stavs")
                        .HasColumnType("int");

                    b.HasKey("DzivNumurs");

                    b.HasIndex("MajaID");

                    b.ToTable("DzivoklisItems");
                });

            modelBuilder.Entity("Uzd2.Datatypes.DzivoklisIedzivotajs", b =>
                {
                    b.Property<long>("DzivoklisNumurs")
                        .HasColumnType("bigint");

                    b.Property<long>("IedzivotajsKods")
                        .HasColumnType("bigint");

                    b.HasKey("DzivoklisNumurs", "IedzivotajsKods");

                    b.HasIndex("IedzivotajsKods");

                    b.ToTable("DzivoklisIedzivotajs");
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

                    b.Property<bool>("IsOwner")
                        .HasColumnType("bit");

                    b.Property<string>("Uzvards")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Vards")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

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
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PastIndeks")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Pilseta")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Valsts")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("MajaNumurs");

                    b.ToTable("MajaItems");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Dzivoklis", b =>
                {
                    b.HasOne("Uzd2.Datatypes.Maja", "Maja")
                        .WithMany("Dzivoklis")
                        .HasForeignKey("MajaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maja");
                });

            modelBuilder.Entity("Uzd2.Datatypes.DzivoklisIedzivotajs", b =>
                {
                    b.HasOne("Uzd2.Datatypes.Dzivoklis", "Dzivoklis")
                        .WithMany("DzivIedz")
                        .HasForeignKey("DzivoklisNumurs")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Uzd2.Datatypes.Iedzivotajs", "Iedzivotajs")
                        .WithMany("DzivIedz")
                        .HasForeignKey("IedzivotajsKods")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dzivoklis");

                    b.Navigation("Iedzivotajs");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Iedzivotajs", b =>
                {
                    b.HasOne("Uzd2.Datatypes.Dzivoklis", "Dzivoklis")
                        .WithMany()
                        .HasForeignKey("DzivoklisDzivNumurs");

                    b.Navigation("Dzivoklis");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Dzivoklis", b =>
                {
                    b.Navigation("DzivIedz");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Iedzivotajs", b =>
                {
                    b.Navigation("DzivIedz");
                });

            modelBuilder.Entity("Uzd2.Datatypes.Maja", b =>
                {
                    b.Navigation("Dzivoklis");
                });
#pragma warning restore 612, 618
        }
    }
}
