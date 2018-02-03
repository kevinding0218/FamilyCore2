﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApi.Persistent;

namespace WebApi.Migrations
{
    [DbContext(typeof(FcDbContext))]
    [Migration("20180203021314_AddApplicationMenu")]
    partial class AddApplicationMenu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DomainLibrary.Location.Supermarket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int>("AddressRefId");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("AddressRefId")
                        .IsUnique();

                    b.ToTable("Supermarket");
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("CurrentRank");

                    b.Property<int>("EntreeCatagoryId");

                    b.Property<int>("EntreeStyleId");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.Property<int?>("StapleFoodId");

                    b.HasKey("Id");

                    b.HasIndex("EntreeCatagoryId");

                    b.HasIndex("EntreeStyleId");

                    b.HasIndex("StapleFoodId");

                    b.ToTable("Entree");
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreeCatagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("Catagory")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.HasKey("Id");

                    b.ToTable("EntreeCatagory");
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int>("EntreeDetailTypeId");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("EntreeDetailTypeId");

                    b.ToTable("EntreeDetail");
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreeDetailType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("DetailName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("DetailType")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.HasKey("Id");

                    b.ToTable("EntreeDetailType");
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntreeId");

                    b.Property<byte[]>("FileBlob");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("EntreeId");

                    b.ToTable("EntreePhoto");
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entrees_Details", b =>
                {
                    b.Property<int>("EntreeId");

                    b.Property<int>("EntreeDetailId");

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Note");

                    b.Property<int>("Quantity");

                    b.HasKey("EntreeId", "EntreeDetailId");

                    b.HasIndex("EntreeDetailId");

                    b.ToTable("Entrees_Details");
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entrees_Orders", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("EntreeId");

                    b.Property<int?>("Count");

                    b.Property<string>("Note");

                    b.Property<DateTime?>("ScheduledDate");

                    b.HasKey("OrderId", "EntreeId");

                    b.HasIndex("EntreeId");

                    b.ToTable("Entrees_Orders");
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreeStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("EntreeStyle");
                });

            modelBuilder.Entity("DomainLibrary.Meal.StapleFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Note")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("StapleFood");
                });

            modelBuilder.Entity("DomainLibrary.Meal.Supermarket_EntreeDetail", b =>
                {
                    b.Property<int>("SupermarketId");

                    b.Property<int>("EntreeDetailId");

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Note");

                    b.HasKey("SupermarketId", "EntreeDetailId");

                    b.HasIndex("EntreeDetailId");

                    b.ToTable("Supermarket_EntreeDetail");
                });

            modelBuilder.Entity("DomainLibrary.Meal.Supermarket_StapleFood", b =>
                {
                    b.Property<int>("SuperMarketId");

                    b.Property<int>("StapleFoodId");

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Note")
                        .HasMaxLength(255);

                    b.HasKey("SuperMarketId", "StapleFoodId");

                    b.HasIndex("StapleFoodId");

                    b.ToTable("Supermarket_StapleFood");
                });

            modelBuilder.Entity("DomainLibrary.Member.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<bool?>("IsFCUser");

                    b.Property<DateTime>("LastLogIn");

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Note")
                        .HasMaxLength(255);

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DomainLibrary.Menu.ApplicationMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationMenuId");

                    b.Property<string>("BadgeText");

                    b.Property<string>("BadgeVariant");

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationMenuId");

                    b.ToTable("ApplicationMenu");
                });

            modelBuilder.Entity("DomainLibrary.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("Note");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DomainLibrary.Shared.ContactAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddedById");

                    b.Property<DateTime>("AddedOn");

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("City");

                    b.Property<int?>("LastUpdatedById");

                    b.Property<DateTime?>("LastUpdatedByOn");

                    b.Property<string>("State");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("ContactAddress");
                });

            modelBuilder.Entity("DomainLibrary.Location.Supermarket", b =>
                {
                    b.HasOne("DomainLibrary.Shared.ContactAddress", "AddressInfo")
                        .WithOne("Supermarket")
                        .HasForeignKey("DomainLibrary.Location.Supermarket", "AddressRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entree", b =>
                {
                    b.HasOne("DomainLibrary.Meal.EntreeCatagory", "EntreeCatagory")
                        .WithMany("Entrees")
                        .HasForeignKey("EntreeCatagoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Meal.EntreeStyle", "EntreeStyle")
                        .WithMany("Entrees")
                        .HasForeignKey("EntreeStyleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Meal.StapleFood", "StapleFood")
                        .WithMany("EntreesWithCurrentStapleFood")
                        .HasForeignKey("StapleFoodId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreeDetail", b =>
                {
                    b.HasOne("DomainLibrary.Meal.EntreeDetailType", "EntreeDetailType")
                        .WithMany("EntreeDetails")
                        .HasForeignKey("EntreeDetailTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.EntreePhoto", b =>
                {
                    b.HasOne("DomainLibrary.Meal.Entree")
                        .WithMany("EntreePhotos")
                        .HasForeignKey("EntreeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entrees_Details", b =>
                {
                    b.HasOne("DomainLibrary.Meal.EntreeDetail", "EntreeDetail")
                        .WithMany("EntreesWithCurrentEntreeWithDetail")
                        .HasForeignKey("EntreeDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Meal.Entree", "Entree")
                        .WithMany("MappingDetailsWithCurrentEntree")
                        .HasForeignKey("EntreeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.Entrees_Orders", b =>
                {
                    b.HasOne("DomainLibrary.Meal.Entree", "Entree")
                        .WithMany("MappingOrdersWithCurrentEntree")
                        .HasForeignKey("EntreeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Order.Order", "Order")
                        .WithMany("MappingEntreesWithCurrentOrder")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.Supermarket_EntreeDetail", b =>
                {
                    b.HasOne("DomainLibrary.Meal.EntreeDetail", "EntreeDetail")
                        .WithMany()
                        .HasForeignKey("EntreeDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Location.Supermarket", "Supermarket")
                        .WithMany()
                        .HasForeignKey("SupermarketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Meal.Supermarket_StapleFood", b =>
                {
                    b.HasOne("DomainLibrary.Meal.StapleFood", "StapleFood")
                        .WithMany()
                        .HasForeignKey("StapleFoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLibrary.Location.Supermarket", "Supermarket")
                        .WithMany()
                        .HasForeignKey("SuperMarketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DomainLibrary.Menu.ApplicationMenu", b =>
                {
                    b.HasOne("DomainLibrary.Menu.ApplicationMenu")
                        .WithMany("Children")
                        .HasForeignKey("ApplicationMenuId");
                });
#pragma warning restore 612, 618
        }
    }
}
