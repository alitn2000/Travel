using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel.InfraStructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChekListType = table.Column<int>(type: "int", nullable: false),
                    TripType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckListTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    CheckListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckListTrips_CheckLists_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "CheckLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckListTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CheckLists",
                columns: new[] { "Id", "ChekListType", "TripType" },
                values: new object[,]
                {
                    { 1, 1, 4 },
                    { 2, 2, 1 },
                    { 3, 1, 2 },
                    { 4, 2, 3 },
                    { 5, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "test1@gmail.com", "name1", "123" },
                    { 2, "test2@gmail.com", "name2", "123" },
                    { 3, "test3@gmail.com", "name3", "123" }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "Destination", "End", "Start", "TripType", "UserId" },
                values: new object[,]
                {
                    { 1, "tehran", new DateTime(2025, 5, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, 1 },
                    { 2, "ardabil", new DateTime(2025, 5, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, "mashhad", new DateTime(2025, 5, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListTrips_CheckListId",
                table: "CheckListTrips",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListTrips_TripId",
                table: "CheckListTrips",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListTrips");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
