﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Models;

namespace TemporadaDeBasquete.Migrations
{
    [DbContext(typeof(BasqueteContext))]
    [Migration("20201004011148_Numero do jogo")]
    partial class Numerodojogo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TemporadaDeBasquete.Models.RegistroJogo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaximoTemporada")
                        .HasColumnType("int");

                    b.Property<int?>("MinimoTemporada")
                        .HasColumnType("int");

                    b.Property<int>("NumeroJogo")
                        .HasColumnType("int");

                    b.Property<int>("Placar")
                        .HasColumnType("int");

                    b.Property<int?>("QuebraRecordeMaximo")
                        .HasColumnType("int");

                    b.Property<int?>("QuebraRecordeMinimo")
                        .HasColumnType("int");

                    b.Property<Guid>("TemporadaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TemporadaId");

                    b.ToTable("RegistroJogo");
                });

            modelBuilder.Entity("TemporadaDeBasquete.Models.Temporada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TemporadaDescricao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Temporada");
                });

            modelBuilder.Entity("TemporadaDeBasquete.Models.RegistroJogo", b =>
                {
                    b.HasOne("TemporadaDeBasquete.Models.Temporada", "Temporada")
                        .WithMany()
                        .HasForeignKey("TemporadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
