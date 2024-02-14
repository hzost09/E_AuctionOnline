using DomainLayer.Core.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfrastructureLayer.AppDbContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        {

        }

        public DbSet<User> Usertb { get; set; }
        public DbSet<Item> Itemtb { get; set; }
        public DbSet<Category> Catagorytb { get; set; }
        public DbSet<CateItem> CateItemtb { get; set; }
        public DbSet<Notification> Notificationstb { get; set; }
        public DbSet<Rating> Ratingtb { get; set; }
        public DbSet<ItemNoti> ItemNotitb { get; set; }
        public DbSet<ReFreshToken> ReFreshTokenstb { get; set; }
        public DbSet<EmailToken> EmailTokentb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "User1",
                    Password = "User1234",
                    Email = "User1@gmail.com",
                    Role = "User"
                },
                   new User
                   {
                       Id = 2,
                       Name = "User2",
                       Password = "User1234",
                       Email = "User2@gmail.com",
                       Role = "User"
                   },
                      new User
                      {
                          Id = 3,
                          Name = "User3",
                          Password = "User1234",
                          Email = "User3@gmail.com",
                          Role = "User"
                      },
                         new User
                         {
                             Id = 4,
                             Name = "User4",
                             Password = "User1234",
                             Email = "User4@gmail.com",
                             Role = "User"
                         },
                            new User
                            {
                                Id = 5,
                                Name = "User5",
                                Password = "User1234",
                                Email = "User5@gmail.com",
                                Role = "User"
                            },
                               new User
                               {
                                   Id = 6,
                                   Name = "User6",
                                   Password = "User1234",
                                   Email = "User6@gmail.com",
                                   Role = "User"
                               },
                                  new User
                                  {
                                      Id = 7,
                                      Name = "User7",
                                      Password = "User1234",
                                      Email = "User7@gmail.com",
                                      Role = "User"
                                  }, 
                                  new User
                                  {
                                      Id = 8,
                                      Name = "User8",
                                      Password = "User1234",
                                      Email = "User8@gmail.com",
                                      Role = "User"
                                  }, 
                                  new User
                                  {
                                      Id = 9,
                                      Name = "User9",
                                      Password = "User1234",
                                      Email = "User@gmail.com",
                                      Role = "User"
                                  }, 
                                  new User
                                  {
                                      Id = 10,
                                      Name = "User10",
                                      Password = "User1234",
                                      Email = "User10@gmail.com",
                                      Role = "User"
                                  },
                                  new User
                                  {
                                      Id = 11,
                                      Name = "Admin",
                                      Password = "Admin1234",
                                      Email = "Admin@gmail.com",
                                      Role = "Admin"
                                  }
                );
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "item1",
                    Description = "des for item1",
                    BeginPrice = 10,
                    UpPrice = 2,
                    WinningPrice = 20,
                    sellerId = 1,
                    Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                    Document = "a"
                },
                   new Item
                   {
                       Id = 2,
                       Name = "item2",
                       Description = "des for item2",
                       BeginPrice = 10,
                       UpPrice = 2,
                       WinningPrice = 20,
                       sellerId = 2,
                       Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                       Document = "a"
                   },
                         new Item
                         {
                             Id = 3,
                             Name = "item3",
                             Description = "des for item3",
                             BeginPrice = 10,
                             UpPrice = 2,
                             WinningPrice = 20,
                             sellerId = 3,
                             Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                             Document = "a"
                         },
                            new Item
                            {
                                Id = 4,
                                Name = "item4",
                                Description = "des for item4",
                                BeginPrice = 10,
                                UpPrice = 2,
                                WinningPrice = 20,
                                sellerId = 4,
                                Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                Document = "a"
                            },
                               new Item
                               {
                                   Id = 5,
                                   Name = "item5",
                                   Description = "des for item5",
                                   BeginPrice = 10,
                                   UpPrice = 2,
                                   WinningPrice = 20,
                                   sellerId = 5,
                                   Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                   Document = "a"
                               },
                                  new Item
                                  {
                                      Id = 6,
                                      Name = "item6",
                                      Description = "des for item6",
                                      BeginPrice = 10,
                                      UpPrice = 2,
                                      WinningPrice = 20,
                                      sellerId=6,
                                      Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                      Document = "a"
                                  },
                                     new Item
                                     {
                                         Id = 7,
                                         Name = "item7",
                                         Description = "des for item7",
                                         BeginPrice = 10,
                                         UpPrice = 2,
                                         WinningPrice = 20,
                                         sellerId=7,
                                         Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                         Document = "a"
                                     },
                                        new Item
                                        {
                                            Id = 8,
                                            Name = "item8",
                                            Description = "des for item8",
                                            BeginPrice = 10,
                                            UpPrice = 2,
                                            WinningPrice = 20,
                                            sellerId=8,
                                            Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                            Document = "a"
                                        },
                                           new Item
                                           {
                                               Id = 9,
                                               Name = "item9",
                                               Description = "des for item9",
                                               BeginPrice = 10,
                                               UpPrice = 2,
                                               WinningPrice = 20,
                                                sellerId=9,
                                               Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                               Document = "a"
                                           },
                                              new Item
                                              {
                                                  Id = 10,
                                                  Name = "item10",
                                                  Description = "des for item10",
                                                  BeginPrice = 10,
                                                  UpPrice = 2,
                                                  WinningPrice = 20,
                                                  sellerId=10,
                                                  Image = "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png",
                                                  Document = "a"
                                              }
                );
            modelBuilder.Entity<Bid>().HasData(
                new Bid
                {
                    Id = 1,
                    BidAmount = 10,
                    UserId = 10,
                    ItemId = 1
                },
                  new Bid
                {
                      Id = 2,
                      BidAmount = 10,
                      UserId = 9,
                      ItemId = 2
                  },
                    new Bid
                    {
                        Id = 3,
                        BidAmount = 10,
                        UserId = 8,
                        ItemId = 3
                    },
                         new Bid
                         {
                             Id = 4,
                             BidAmount = 10,
                             UserId = 7,
                             ItemId = 4
                         },
                              new Bid
                              {
                                  Id = 5,
                                  BidAmount = 10,
                                  UserId = 6,
                                  ItemId = 5
                              },
                                   new Bid
                                   {
                                       Id = 6,
                                       BidAmount = 10,
                                       UserId = 5,
                                       ItemId = 6
                                   },
                                        new Bid
                                        {
                                            Id = 7,
                                            BidAmount = 10,
                                            UserId = 4,
                                            ItemId = 7
                                        },
                                             new Bid
                                             {
                                                 Id = 8,
                                                 BidAmount = 10,
                                                 UserId = 3,
                                                 ItemId = 8
                                             },
                                                  new Bid
                                                  {
                                                      Id = 9,
                                                      BidAmount = 10,
                                                      UserId = 2,
                                                      ItemId = 9
                                                  },
                                                       new Bid
                                                       {
                                                           Id = 10,
                                                           BidAmount = 10,
                                                           UserId = 1,
                                                           ItemId = 10
                                                       }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id =1,
                    Name = "category1",
                    Description= "des1"
                },
                    new Category
                    {
                        Id = 2,
                        Name = "category2",
                        Description = "des2"
                    },
                        new Category
                        {
                            Id = 3,
                            Name = "category3",
                            Description = "des3"
                        },
                            new Category
                            {
                                Id = 4,
                                Name = "category4",
                                Description = "des4"
                            },
                                new Category
                                {
                                    Id = 5,
                                    Name = "category5",
                                    Description = "des5"
                                }
                );
            modelBuilder.Entity<CateItem>().HasData(
                new CateItem
                {
                    Id=1,
                    CateId=1,
                    ItemId=1,   
                },
                   new CateItem
                   {
                       Id = 2,
                       CateId = 2,
                       ItemId = 1,
                   },
                      new CateItem
                      {
                          Id = 3,
                          CateId = 3,
                          ItemId = 2,
                      },
                         new CateItem
                         {
                             Id = 4,
                             CateId =4,
                             ItemId = 2,
                         },
                            new CateItem
                            {
                                Id = 5,
                                CateId = 5,
                                ItemId = 3,
                            },
                               new CateItem
                               {
                                   Id = 6,
                                   CateId = 1,
                                   ItemId = 3,
                               },
                                  new CateItem
                                  {
                                      Id = 7,
                                      CateId = 2,
                                      ItemId = 4,
                                  },
                                     new CateItem
                                     {
                                         Id = 8,
                                         CateId = 3,
                                         ItemId = 5,
                                     },
                                        new CateItem
                                        {
                                            Id = 9,
                                            CateId = 4,
                                            ItemId = 6,
                                        },
                                           new CateItem
                                           {
                                               Id = 10,
                                               CateId = 5,
                                               ItemId = 7,
                                           },
                                              new CateItem
                                              {
                                                  Id = 11,
                                                  CateId = 1,
                                                  ItemId = 8,
                                              },
                                                 new CateItem
                                                 {
                                                     Id = 12,
                                                     CateId = 2,
                                                     ItemId = 9,
                                                 },
                                                    new CateItem
                                                    {
                                                        Id = 13,
                                                        CateId = 3,
                                                        ItemId = 10,
                                                    }

                );
            // Configure the relationship between Category and CategoryItem
            modelBuilder.Entity<Category>()
                .HasMany(c => c.cateItems)
                .WithOne(ci => ci.category)
                .HasForeignKey(ci => ci.CateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between Item and CategoryItem
            modelBuilder.Entity<Item>()
                .HasMany(i => i.cateItems)
                .WithOne(ci => ci.item)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between Item and ItemNoti
            modelBuilder.Entity<Item>()
                .HasMany(c => c.itemNotis)
                .WithOne(ci => ci.Item)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between Notification and ItemNoti
            modelBuilder.Entity<Notification>()
                .HasMany(i => i.ItemNoti)
                .WithOne(ci => ci.Notification)
                .HasForeignKey(ci => ci.NotiId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between item and user
            modelBuilder.Entity<User>()
                .HasMany(c => c.soldItem)
                .WithOne(ci => ci.Seller)
                .HasForeignKey(ci => ci.sellerId)
                .OnDelete(DeleteBehavior.ClientCascade);       
        }
    }

    public class BloggingContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=LInh;Initial Catalog=E_AcutionDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
