using Microsoft.EntityFrameworkCore.Migrations;

namespace MCC61_API_Project.Migrations
{
    public partial class fixingNamingConvetion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account");

            migrationBuilder.RenameTable(
                name: "tb_m_account",
                newName: "tb_tr_account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_account",
                table: "tb_tr_account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_account_tb_m_employee_NIK",
                table: "tb_tr_account",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_account_tb_m_employee_NIK",
                table: "tb_tr_account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_account",
                table: "tb_tr_account");

            migrationBuilder.RenameTable(
                name: "tb_tr_account",
                newName: "tb_m_account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_employee_NIK",
                table: "tb_m_account",
                column: "NIK",
                principalTable: "tb_m_employee",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
