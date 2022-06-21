using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Pustok.Migrations
{
    public partial class IsNewAndIsFeaturedCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InNew",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InNew",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Books");
        }
    }
}
