using Microsoft.EntityFrameworkCore.Migrations;

namespace MCC61_API_Project.Migrations
{
    public partial class addmodelAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account");
        }
    }
}
