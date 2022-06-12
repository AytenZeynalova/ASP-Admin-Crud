using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Pustok.Migrations
{
    public partial class HomeSliderTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "HomeSliders");

            migrationBuilder.AddColumn<string>(
                name: "ButtonUrl",
                table: "HomeSliders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "HomeSliders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HomeSliders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonUrl",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "HomeSliders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "HomeSliders");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "HomeSliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
