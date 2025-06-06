﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SafeEntry.Infrastructure.Data;

#nullable disable

namespace SafeEntry.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250601065910_CondominiumChanges")]
    partial class CondominiumChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SafeEntry.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CondominiumId")
                        .HasColumnType("integer");

                    b.Property<int>("HomeNumber")
                        .HasColumnType("integer");

                    b.Property<string>("HomeStreet")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CondominiumId");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Condominium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Condominiums", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Employee", b =>
                {
                    b.HasBaseType("SafeEntry.Domain.Entities.Person");

                    b.Property<int>("CondominiumId")
                        .HasColumnType("integer");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("CondominiumId");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Resident", b =>
                {
                    b.HasBaseType("SafeEntry.Domain.Entities.Person");

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.HasIndex("AddressId");

                    b.ToTable("Residents", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Visitor", b =>
                {
                    b.HasBaseType("SafeEntry.Domain.Entities.Person");

                    b.ToTable("Visitors", (string)null);
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Address", b =>
                {
                    b.HasOne("SafeEntry.Domain.Entities.Condominium", "Condominium")
                        .WithMany()
                        .HasForeignKey("CondominiumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condominium");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.HasOne("SafeEntry.Domain.Entities.Person", "Person")
                        .WithOne("User")
                        .HasForeignKey("User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SafeEntry.Domain.ValueObjects.PasswordHash", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PasswordHash");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Employee", b =>
                {
                    b.HasOne("SafeEntry.Domain.Entities.Condominium", "Condominium")
                        .WithMany()
                        .HasForeignKey("CondominiumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SafeEntry.Domain.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("SafeEntry.Domain.Entities.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condominium");
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Resident", b =>
                {
                    b.HasOne("SafeEntry.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SafeEntry.Domain.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("SafeEntry.Domain.Entities.Resident", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Visitor", b =>
                {
                    b.HasOne("SafeEntry.Domain.Entities.Person", null)
                        .WithOne()
                        .HasForeignKey("SafeEntry.Domain.Entities.Visitor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SafeEntry.Domain.Entities.Person", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
