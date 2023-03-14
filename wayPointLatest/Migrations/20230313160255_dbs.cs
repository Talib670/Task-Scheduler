using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wayPointLatest.Migrations
{
    public partial class dbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessCampaign",
                table: "AdminLogin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessProvider",
                table: "AdminLogin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessSource",
                table: "AdminLogin",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessCampaign",
                table: "AdminLogin");

            migrationBuilder.DropColumn(
                name: "AccessProvider",
                table: "AdminLogin");

            migrationBuilder.DropColumn(
                name: "AccessSource",
                table: "AdminLogin");
        }
    }
}
