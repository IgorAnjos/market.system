using Microsoft.EntityFrameworkCore.Migrations;

namespace mktSystem.Data.Migrations
{
    public partial class UpdateFornecedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedores",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");
        }
    }
}
