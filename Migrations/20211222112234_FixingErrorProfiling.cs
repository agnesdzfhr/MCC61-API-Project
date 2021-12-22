using Microsoft.EntityFrameworkCore.Migrations;

namespace MCC61_API_Project.Migrations
{
    public partial class FixingErrorProfiling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_profiling");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_tr_profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_tr_profiling_tb_m_education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_m_education",
                        principalColumn: "EducationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_profiling_tb_tr_account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_tr_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profiling_EducationId",
                table: "tb_tr_profiling",
                column: "EducationId");
        }
    }
}
