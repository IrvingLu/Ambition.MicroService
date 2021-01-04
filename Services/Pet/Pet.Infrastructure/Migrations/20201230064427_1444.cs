using Microsoft.EntityFrameworkCore.Migrations;

namespace Pet.Infrastructure.Migrations
{
    public partial class _1444 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Pet",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pet_ApplicationUserId",
                table: "Pet",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_AspNetUsers_ApplicationUserId",
                table: "Pet",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_AspNetUsers_ApplicationUserId",
                table: "Pet");

            migrationBuilder.DropIndex(
                name: "IX_Pet_ApplicationUserId",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Pet");
        }
    }
}
