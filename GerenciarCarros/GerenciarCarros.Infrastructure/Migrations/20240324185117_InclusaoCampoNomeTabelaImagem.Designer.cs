﻿// <auto-generated />
using System;
using GerenciarCarros.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GerenciarCarros.Infrastructure.Migrations
{
    [DbContext(typeof(CarrosDBContext))]
    [Migration("20240324185117_InclusaoCampoNomeTabelaImagem")]
    partial class InclusaoCampoNomeTabelaImagem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Carro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Km")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Carros", (string)null);
                });

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Imagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdCarro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdCarro");

                    b.ToTable("Imagens", (string)null);
                });

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Opcionais", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("IdCarro")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdCarro");

                    b.ToTable("Opcionais", (string)null);
                });

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Imagem", b =>
                {
                    b.HasOne("GerenciarCarros.Domain.Entities.Carro", "Carro")
                        .WithMany("Imagens")
                        .HasForeignKey("IdCarro")
                        .IsRequired();

                    b.Navigation("Carro");
                });

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Opcionais", b =>
                {
                    b.HasOne("GerenciarCarros.Domain.Entities.Carro", "Carro")
                        .WithMany("Opcionais")
                        .HasForeignKey("IdCarro")
                        .IsRequired();

                    b.Navigation("Carro");
                });

            modelBuilder.Entity("GerenciarCarros.Domain.Entities.Carro", b =>
                {
                    b.Navigation("Imagens");

                    b.Navigation("Opcionais");
                });
#pragma warning restore 612, 618
        }
    }
}