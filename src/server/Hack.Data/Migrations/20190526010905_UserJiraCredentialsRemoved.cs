using Microsoft.EntityFrameworkCore.Migrations;

namespace Hack.Data.Migrations
{
    public partial class UserJiraCredentialsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credentials_Token",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Credentials_Username",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Credentials_Token",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Credentials_Username",
                table: "User",
                nullable: true);
        }
    }
}
