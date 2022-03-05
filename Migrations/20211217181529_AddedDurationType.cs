using Microsoft.EntityFrameworkCore.Migrations;

namespace BELTEXAM.Migrations
{
    public partial class AddedDurationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DurationType",
                table: "Activities",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "Activities");
        }
    }
}
