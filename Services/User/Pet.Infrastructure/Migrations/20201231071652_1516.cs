using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet.User.Infrastructure.Migrations
{
    public partial class _1516 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Tenant_ServiceCategory",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Tenant_ServiceCategory");
        }
    }
}
