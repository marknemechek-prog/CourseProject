using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Semester = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionType = table.Column<string>(type: "TEXT", nullable: false),
                    ActionDetails = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "ivan.petrov@uni.edu", "Іван Петров", "HASH1" },
                    { 2, "olena.ivanenko@uni.edu", "Олена Іваненко", "HASH2" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name", "Semester", "UserId" },
                values: new object[,]
                {
                    { 1, "C# базовий курс", "Програмування 1", 1, 1 },
                    { 2, "SQL та БД", "Бази даних", 3, 1 },
                    { 3, "Основи мереж", "Комп'ютерні мережі", 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionDetails", "ActionType", "CreatedAt", "ItemId", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "Створити курс", "Create", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Approved", 1 },
                    { 2, "Обʼєднати з іншим", "Delete", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Pending", 1 },
                    { 3, "Новий курс з мереж", "Create", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Rejected", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ItemId",
                table: "Actions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_UserId",
                table: "Actions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
