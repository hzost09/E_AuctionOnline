﻿// <auto-generated />
using System;
using InfrastructureLayer.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240224075100_V0")]
    partial class V0
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DomainLayer.Core.Enities.AuctionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<float?>("HighestBid")
                        .HasColumnType("real");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.HasIndex("WinnerId");

                    b.ToTable("AuctionHistory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HighestBid = 10f,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            HighestBid = 10f,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 3,
                            HighestBid = 10f,
                            ItemId = 3
                        },
                        new
                        {
                            Id = 4,
                            HighestBid = 10f,
                            ItemId = 4
                        },
                        new
                        {
                            Id = 5,
                            HighestBid = 10f,
                            ItemId = 5
                        },
                        new
                        {
                            Id = 6,
                            HighestBid = 10f,
                            ItemId = 6
                        },
                        new
                        {
                            Id = 7,
                            HighestBid = 10f,
                            ItemId = 7
                        },
                        new
                        {
                            Id = 8,
                            HighestBid = 10f,
                            ItemId = 8
                        },
                        new
                        {
                            Id = 9,
                            HighestBid = 10f,
                            ItemId = 9
                        },
                        new
                        {
                            Id = 10,
                            HighestBid = 10f,
                            ItemId = 10
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Bid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("BidAmount")
                        .HasColumnType("real");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Bid");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BidAmount = 10f,
                            ItemId = 1,
                            UserId = 10
                        },
                        new
                        {
                            Id = 2,
                            BidAmount = 10f,
                            ItemId = 2,
                            UserId = 9
                        },
                        new
                        {
                            Id = 3,
                            BidAmount = 10f,
                            ItemId = 3,
                            UserId = 8
                        },
                        new
                        {
                            Id = 4,
                            BidAmount = 10f,
                            ItemId = 4,
                            UserId = 7
                        },
                        new
                        {
                            Id = 5,
                            BidAmount = 10f,
                            ItemId = 5,
                            UserId = 6
                        },
                        new
                        {
                            Id = 6,
                            BidAmount = 10f,
                            ItemId = 6,
                            UserId = 5
                        },
                        new
                        {
                            Id = 7,
                            BidAmount = 10f,
                            ItemId = 7,
                            UserId = 4
                        },
                        new
                        {
                            Id = 8,
                            BidAmount = 10f,
                            ItemId = 8,
                            UserId = 3
                        },
                        new
                        {
                            Id = 9,
                            BidAmount = 10f,
                            ItemId = 9,
                            UserId = 2
                        },
                        new
                        {
                            Id = 10,
                            BidAmount = 10f,
                            ItemId = 10,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.CateItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CateId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CateId");

                    b.HasIndex("ItemId");

                    b.ToTable("CateItemtb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CateId = 1,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            CateId = 2,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 3,
                            CateId = 3,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 4,
                            CateId = 4,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 5,
                            CateId = 5,
                            ItemId = 3
                        },
                        new
                        {
                            Id = 6,
                            CateId = 1,
                            ItemId = 3
                        },
                        new
                        {
                            Id = 7,
                            CateId = 2,
                            ItemId = 4
                        },
                        new
                        {
                            Id = 8,
                            CateId = 3,
                            ItemId = 5
                        },
                        new
                        {
                            Id = 9,
                            CateId = 4,
                            ItemId = 6
                        },
                        new
                        {
                            Id = 10,
                            CateId = 5,
                            ItemId = 7
                        },
                        new
                        {
                            Id = 11,
                            CateId = 1,
                            ItemId = 8
                        },
                        new
                        {
                            Id = 12,
                            CateId = 2,
                            ItemId = 9
                        },
                        new
                        {
                            Id = 13,
                            CateId = 3,
                            ItemId = 10
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catagorytb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "des1",
                            Name = "category1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "des2",
                            Name = "category2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "des3",
                            Name = "category3"
                        },
                        new
                        {
                            Id = 4,
                            Description = "des4",
                            Name = "category4"
                        },
                        new
                        {
                            Id = 5,
                            Description = "des5",
                            Name = "category5"
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.EmailToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("tokenResetPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmailTokentb");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("BeginPrice")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UpPrice")
                        .HasColumnType("real");

                    b.Property<float>("WinningPrice")
                        .HasColumnType("real");

                    b.Property<int?>("sellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("sellerId");

                    b.ToTable("Itemtb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BeginPrice = 10f,
                            Description = "des for item1",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item1",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 1
                        },
                        new
                        {
                            Id = 2,
                            BeginPrice = 10f,
                            Description = "des for item2",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item2",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 2
                        },
                        new
                        {
                            Id = 3,
                            BeginPrice = 10f,
                            Description = "des for item3",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item3",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 3
                        },
                        new
                        {
                            Id = 4,
                            BeginPrice = 10f,
                            Description = "des for item4",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item4",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 4
                        },
                        new
                        {
                            Id = 5,
                            BeginPrice = 10f,
                            Description = "des for item5",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item5",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 5
                        },
                        new
                        {
                            Id = 6,
                            BeginPrice = 10f,
                            Description = "des for item6",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item6",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 6
                        },
                        new
                        {
                            Id = 7,
                            BeginPrice = 10f,
                            Description = "des for item7",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item7",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 7
                        },
                        new
                        {
                            Id = 8,
                            BeginPrice = 10f,
                            Description = "des for item8",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item8",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 8
                        },
                        new
                        {
                            Id = 9,
                            BeginPrice = 10f,
                            Description = "des for item9",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item9",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 9
                        },
                        new
                        {
                            Id = 10,
                            BeginPrice = 10f,
                            Description = "des for item10",
                            Document = "a",
                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                            Name = "item10",
                            UpPrice = 2f,
                            WinningPrice = 20f,
                            sellerId = 10
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.ItemNoti", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("NotiId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("NotiId");

                    b.ToTable("ItemNotitb");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notificationstb");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.Property<int>("RateUserId")
                        .HasColumnType("int");

                    b.Property<int?>("RatedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.HasIndex("RateUserId");

                    b.HasIndex("SellerId");

                    b.ToTable("Ratingtb");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.ReFreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ReFreshTokenstb");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("EmailConfirm")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usertb");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "User1@gmail.com",
                            EmailConfirm = false,
                            Name = "User1",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Email = "User2@gmail.com",
                            EmailConfirm = false,
                            Name = "User2",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 3,
                            Email = "User3@gmail.com",
                            EmailConfirm = false,
                            Name = "User3",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 4,
                            Email = "User4@gmail.com",
                            EmailConfirm = false,
                            Name = "User4",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 5,
                            Email = "User5@gmail.com",
                            EmailConfirm = false,
                            Name = "User5",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 6,
                            Email = "User6@gmail.com",
                            EmailConfirm = false,
                            Name = "User6",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 7,
                            Email = "User7@gmail.com",
                            EmailConfirm = false,
                            Name = "User7",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 8,
                            Email = "User8@gmail.com",
                            EmailConfirm = false,
                            Name = "User8",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 9,
                            Email = "User@gmail.com",
                            EmailConfirm = false,
                            Name = "User9",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 10,
                            Email = "User10@gmail.com",
                            EmailConfirm = false,
                            Name = "User10",
                            Password = "User1234",
                            Role = "User"
                        },
                        new
                        {
                            Id = 11,
                            Email = "Admin@gmail.com",
                            EmailConfirm = true,
                            Name = "Admin",
                            Password = "Admin1234",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("DomainLayer.Core.VerifyEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VerifyToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("VerifyEmail");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.AuctionHistory", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.Item", "Item")
                        .WithOne("Auctionhistory")
                        .HasForeignKey("DomainLayer.Core.Enities.AuctionHistory", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.User", "Winner")
                        .WithMany("AuctionHistories")
                        .HasForeignKey("WinnerId");

                    b.Navigation("Item");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Bid", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.Item", "Item")
                        .WithMany("bid")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.User", "User")
                        .WithMany("bids")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.CateItem", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.Category", "category")
                        .WithMany("cateItems")
                        .HasForeignKey("CateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.Item", "item")
                        .WithMany("cateItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("item");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.EmailToken", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.User", "User")
                        .WithOne("EmailToken")
                        .HasForeignKey("DomainLayer.Core.Enities.EmailToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Item", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.User", "Seller")
                        .WithMany("soldItem")
                        .HasForeignKey("sellerId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.ItemNoti", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.Item", "Item")
                        .WithMany("itemNotis")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.Notification", "Notification")
                        .WithMany("ItemNoti")
                        .HasForeignKey("NotiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Rating", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.Item", "Item")
                        .WithOne("rating")
                        .HasForeignKey("DomainLayer.Core.Enities.Rating", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.User", "RateUser")
                        .WithMany("Rater")
                        .HasForeignKey("RateUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DomainLayer.Core.Enities.User", "seller")
                        .WithMany("BeingRateds")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("RateUser");

                    b.Navigation("seller");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.ReFreshToken", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.User", "User")
                        .WithOne("ReFreshToken")
                        .HasForeignKey("DomainLayer.Core.Enities.ReFreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Core.VerifyEmail", b =>
                {
                    b.HasOne("DomainLayer.Core.Enities.User", "User")
                        .WithOne("Verifyemail")
                        .HasForeignKey("DomainLayer.Core.VerifyEmail", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Category", b =>
                {
                    b.Navigation("cateItems");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Item", b =>
                {
                    b.Navigation("Auctionhistory");

                    b.Navigation("bid");

                    b.Navigation("cateItems");

                    b.Navigation("itemNotis");

                    b.Navigation("rating");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.Notification", b =>
                {
                    b.Navigation("ItemNoti");
                });

            modelBuilder.Entity("DomainLayer.Core.Enities.User", b =>
                {
                    b.Navigation("AuctionHistories");

                    b.Navigation("BeingRateds");

                    b.Navigation("EmailToken");

                    b.Navigation("Rater");

                    b.Navigation("ReFreshToken");

                    b.Navigation("Verifyemail")
                        .IsRequired();

                    b.Navigation("bids");

                    b.Navigation("soldItem");
                });
#pragma warning restore 612, 618
        }
    }
}
