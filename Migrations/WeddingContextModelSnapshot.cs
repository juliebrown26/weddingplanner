﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using weddingplanner.Models;

namespace weddingplanner.Migrations
{
    [DbContext(typeof(WeddingContext))]
    partial class WeddingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("weddingplanner.Models.Rsvp", b =>
                {
                    b.Property<int>("RsvpId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("WeddingId");

                    b.HasKey("RsvpId");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("Rsvps");
                });

            modelBuilder.Entity("weddingplanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("weddingplanner.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("PartnerOne")
                        .IsRequired();

                    b.Property<string>("PartnerTwo")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<DateTime>("WeddingDate");

                    b.HasKey("WeddingId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("weddingplanner.Models.Rsvp", b =>
                {
                    b.HasOne("weddingplanner.Models.User", "User")
                        .WithMany("Rsvps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("weddingplanner.Models.Wedding", "Wedding")
                        .WithMany("Rsvps")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
