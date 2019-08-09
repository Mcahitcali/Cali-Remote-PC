using Microsoft.EntityFrameworkCore.Migrations;

namespace Cali_Remote_Pc.Migrations
{
    public partial class baseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Actions",
                newName: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Actions",
                newName: "IsActive");
        }
    }
}
