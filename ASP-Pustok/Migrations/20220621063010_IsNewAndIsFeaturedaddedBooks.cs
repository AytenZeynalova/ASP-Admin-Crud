using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Pustok.Migrations
{
    public partial class IsNewAndIsFeaturedaddedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InNew",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "InNew",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
