using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodDelieveryManagementAPI.Migrations
{
    public partial class NewMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrders",
                table: "UserDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalOrders",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
