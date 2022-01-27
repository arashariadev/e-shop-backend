using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Api.Migrations
{
    public partial class Receivemails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReceiveMails",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveMails",
                table: "AspNetUsers");
        }
    }
}
