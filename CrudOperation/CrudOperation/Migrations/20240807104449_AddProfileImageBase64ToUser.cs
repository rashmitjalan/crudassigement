using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudOperation.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageBase64ToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "Users",
                newName: "ProfileImageBase64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImageBase64",
                table: "Users",
                newName: "ProfileImage");
        }
    }
}
