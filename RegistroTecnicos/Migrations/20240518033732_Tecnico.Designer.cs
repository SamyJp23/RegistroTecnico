﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroTecnicos.DAL;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20240518033732_Tecnico")]
    partial class Tecnico
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("RegistroTecnicos.Models.Tecnicos", b =>
                {
                    b.Property<int>("IdTecnico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("SueldoHora")
                        .HasColumnType("REAL");

                    b.Property<int>("TipoTecnicoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdTecnico");

                    b.HasIndex("TipoTecnicoId");

                    b.ToTable("Tecnicos");
                });

            modelBuilder.Entity("RegistroTecnicos.Models.TiposTecnicos", b =>
                {
                    b.Property<int>("TipoTecnicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TipoTecnicoId");

                    b.ToTable("TiposTecnicos");
                });

            modelBuilder.Entity("RegistroTecnicos.Models.Tecnicos", b =>
                {
                    b.HasOne("RegistroTecnicos.Models.TiposTecnicos", "TipoTecnico")
                        .WithMany("Tecnicos")
                        .HasForeignKey("TipoTecnicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoTecnico");
                });

            modelBuilder.Entity("RegistroTecnicos.Models.TiposTecnicos", b =>
                {
                    b.Navigation("Tecnicos");
                });
#pragma warning restore 612, 618
        }
    }
}
