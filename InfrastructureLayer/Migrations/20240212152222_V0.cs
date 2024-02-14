using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class V0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catagorytb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagorytb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificationstb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificationstb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usertb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usertb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTokentb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tokenResetPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTokentb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTokentb_Usertb_UserId",
                        column: x => x.UserId,
                        principalTable: "Usertb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itemtb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginPrice = table.Column<float>(type: "real", nullable: false),
                    UpPrice = table.Column<float>(type: "real", nullable: false),
                    WinningPrice = table.Column<float>(type: "real", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sellerId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itemtb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itemtb_Usertb_sellerId",
                        column: x => x.sellerId,
                        principalTable: "Usertb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReFreshTokenstb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReFreshTokenstb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReFreshTokenstb_Usertb_UserId",
                        column: x => x.UserId,
                        principalTable: "Usertb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HighestBid = table.Column<float>(type: "real", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    WinnerId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionHistory_Itemtb_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itemtb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionHistory_Usertb_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Usertb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidAmount = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bid_Itemtb_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itemtb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bid_Usertb_UserId",
                        column: x => x.UserId,
                        principalTable: "Usertb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CateItemtb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CateId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CateItemtb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CateItemtb_Catagorytb_CateId",
                        column: x => x.CateId,
                        principalTable: "Catagorytb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CateItemtb_Itemtb_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itemtb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemNotitb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    NotiId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNotitb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemNotitb_Itemtb_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itemtb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemNotitb_Notificationstb_NotiId",
                        column: x => x.NotiId,
                        principalTable: "Notificationstb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratingtb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratingtb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratingtb_Itemtb_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itemtb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratingtb_Usertb_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Usertb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Catagorytb",
                columns: new[] { "Id", "BeginDate", "Description", "EndDate", "Name" },
                values: new object[,]
                {
                    { 1, null, "des1", null, "category1" },
                    { 2, null, "des2", null, "category2" },
                    { 3, null, "des3", null, "category3" },
                    { 4, null, "des4", null, "category4" },
                    { 5, null, "des5", null, "category5" }
                });

            migrationBuilder.InsertData(
                table: "Usertb",
                columns: new[] { "Id", "Avatar", "BeginDate", "Email", "EndDate", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, null, null, "User1@gmail.com", null, "User1", "User1234", "User" },
                    { 2, null, null, "User2@gmail.com", null, "User2", "User1234", "User" },
                    { 3, null, null, "User3@gmail.com", null, "User3", "User1234", "User" },
                    { 4, null, null, "User4@gmail.com", null, "User4", "User1234", "User" },
                    { 5, null, null, "User5@gmail.com", null, "User5", "User1234", "User" },
                    { 6, null, null, "User6@gmail.com", null, "User6", "User1234", "User" },
                    { 7, null, null, "User7@gmail.com", null, "User7", "User1234", "User" },
                    { 8, null, null, "User8@gmail.com", null, "User8", "User1234", "User" },
                    { 9, null, null, "User@gmail.com", null, "User9", "User1234", "User" },
                    { 10, null, null, "User10@gmail.com", null, "User10", "User1234", "User" },
                    { 11, null, null, "Admin@gmail.com", null, "Admin", "Admin1234", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Itemtb",
                columns: new[] { "Id", "BeginDate", "BeginPrice", "Description", "Document", "EndDate", "Image", "Name", "UpPrice", "WinningPrice", "sellerId" },
                values: new object[,]
                {
                    { 1, null, 10f, "des for item1", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item1", 2f, 20f, 1 },
                    { 2, null, 10f, "des for item2", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item2", 2f, 20f, 2 },
                    { 3, null, 10f, "des for item3", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item3", 2f, 20f, 3 },
                    { 4, null, 10f, "des for item4", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item4", 2f, 20f, 4 },
                    { 5, null, 10f, "des for item5", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item5", 2f, 20f, 5 },
                    { 6, null, 10f, "des for item6", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item6", 2f, 20f, 6 },
                    { 7, null, 10f, "des for item7", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item7", 2f, 20f, 7 },
                    { 8, null, 10f, "des for item8", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item8", 2f, 20f, 8 },
                    { 9, null, 10f, "des for item9", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item9", 2f, 20f, 9 },
                    { 10, null, 10f, "des for item10", "a", null, "https://res.cloudinary.com/dcxzqj0ta/image/upload/v1706760311/nulybxknb3bsdbgwbjxt.png", "item10", 2f, 20f, 10 }
                });

            migrationBuilder.InsertData(
                table: "Bid",
                columns: new[] { "Id", "BeginDate", "BidAmount", "EndDate", "ItemId", "UserId" },
                values: new object[,]
                {
                    { 1, null, 10f, null, 1, 10 },
                    { 2, null, 10f, null, 2, 9 },
                    { 3, null, 10f, null, 3, 8 },
                    { 4, null, 10f, null, 4, 7 },
                    { 5, null, 10f, null, 5, 6 },
                    { 6, null, 10f, null, 6, 5 },
                    { 7, null, 10f, null, 7, 4 },
                    { 8, null, 10f, null, 8, 3 },
                    { 9, null, 10f, null, 9, 2 },
                    { 10, null, 10f, null, 10, 1 }
                });

            migrationBuilder.InsertData(
                table: "CateItemtb",
                columns: new[] { "Id", "BeginDate", "CateId", "EndDate", "ItemId" },
                values: new object[,]
                {
                    { 1, null, 1, null, 1 },
                    { 2, null, 2, null, 1 },
                    { 3, null, 3, null, 2 },
                    { 4, null, 4, null, 2 },
                    { 5, null, 5, null, 3 },
                    { 6, null, 1, null, 3 },
                    { 7, null, 2, null, 4 },
                    { 8, null, 3, null, 5 },
                    { 9, null, 4, null, 6 },
                    { 10, null, 5, null, 7 },
                    { 11, null, 1, null, 8 },
                    { 12, null, 2, null, 9 },
                    { 13, null, 3, null, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionHistory_ItemId",
                table: "AuctionHistory",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionHistory_WinnerId",
                table: "AuctionHistory",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_ItemId",
                table: "Bid",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_UserId",
                table: "Bid",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CateItemtb_CateId",
                table: "CateItemtb",
                column: "CateId");

            migrationBuilder.CreateIndex(
                name: "IX_CateItemtb_ItemId",
                table: "CateItemtb",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTokentb_UserId",
                table: "EmailTokentb",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemNotitb_ItemId",
                table: "ItemNotitb",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNotitb_NotiId",
                table: "ItemNotitb",
                column: "NotiId");

            migrationBuilder.CreateIndex(
                name: "IX_Itemtb_sellerId",
                table: "Itemtb",
                column: "sellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratingtb_ItemId",
                table: "Ratingtb",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratingtb_SellerId",
                table: "Ratingtb",
                column: "SellerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReFreshTokenstb_UserId",
                table: "ReFreshTokenstb",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionHistory");

            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "CateItemtb");

            migrationBuilder.DropTable(
                name: "EmailTokentb");

            migrationBuilder.DropTable(
                name: "ItemNotitb");

            migrationBuilder.DropTable(
                name: "Ratingtb");

            migrationBuilder.DropTable(
                name: "ReFreshTokenstb");

            migrationBuilder.DropTable(
                name: "Catagorytb");

            migrationBuilder.DropTable(
                name: "Notificationstb");

            migrationBuilder.DropTable(
                name: "Itemtb");

            migrationBuilder.DropTable(
                name: "Usertb");
        }
    }
}
