using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace barbeariaPro.Migrations
{
    /// <inheritdoc />
    public partial class isadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "Usuarios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "MotivoCancelamento",
                table: "Agendamentos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Agendamentos",
                keyColumn: "MotivoCancelamento",
                keyValue: null,
                column: "MotivoCancelamento",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MotivoCancelamento",
                table: "Agendamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
