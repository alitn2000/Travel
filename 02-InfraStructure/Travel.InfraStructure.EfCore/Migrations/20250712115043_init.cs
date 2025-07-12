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
                    ChekListType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNameType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckListTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    CheckListId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTrips_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrips_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CheckLists",
                columns: new[] { "Id", "ChekListType", "CreateDate", "CreatedUserId", "TripType", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "A", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(994), 0, 4, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(996) },
                    { 2, "B", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(999), 0, 1, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(999) },
                    { 3, "C", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1000), 0, 2, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1000) },
                    { 4, "D", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1001), 0, 3, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1001) },
                    { 5, "F", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1002), 0, 5, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1002) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatedUserId", "UpdateDate", "UserName", "UserNameType" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(445), 0, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(448), "test@gmail.com", 0 },
                    { 2, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(452), 0, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(452), "test2@gmail.com", 1 },
                    { 3, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(454), 0, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(454), "alitahmasebinia@gmail.com", 0 }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Address", "Age", "CreateDate", "CreatedUserId", "FirstName", "Gender", "LastName", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { 1, "SomeWhere", 25, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1830), 0, "Ali", 1, "Tahmasebinia", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1830), 1 },
                    { 2, "SomeWhere", 26, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1834), 0, "Alireza", 1, "Tahmasebinia", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1834), 2 },
                    { 3, "SomeWhere", 25, new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1836), 0, "Sepide", 2, "Sepidei", new DateTime(2025, 7, 12, 11, 50, 43, 341, DateTimeKind.Utc).AddTicks(1836), 3 }
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
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTrips_TripId",
                table: "UserTrips",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrips_UserId",
                table: "UserTrips",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListTrips");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "UserTrips");

            migrationBuilder.DropTable(
                name: "CheckLists");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
