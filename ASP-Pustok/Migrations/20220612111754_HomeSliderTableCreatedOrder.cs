using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Pustok.Migrations
{
    public partial class HomeSliderTableCreatedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "HomeSliders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "HomeSliders");
        }
    }
}
