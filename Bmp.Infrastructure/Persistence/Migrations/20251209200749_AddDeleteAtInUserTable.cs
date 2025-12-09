using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bmp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteAtInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users"
            );
        }
    }
}
