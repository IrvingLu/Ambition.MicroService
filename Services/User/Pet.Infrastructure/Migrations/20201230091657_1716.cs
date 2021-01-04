using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet.User.Infrastructure.Migrations
{
    public partial class _1716 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Suggest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Content = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PicPath = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ContactWay = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Suggest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Suggest");
        }
    }
}
