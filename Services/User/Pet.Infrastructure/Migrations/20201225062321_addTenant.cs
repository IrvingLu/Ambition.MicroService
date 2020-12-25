using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet.User.Infrastructure.Migrations
{
    public partial class addTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LogoPath = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Phone = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Address = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Longitude = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Latitude = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Announcement = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IsOfficial = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsOfficialRecommend = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BusinessInfo_LicensePath = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BusinessInfo_LegalPerson = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BusinessInfo_Contact = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BusinessInfo_ContactPhone = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
