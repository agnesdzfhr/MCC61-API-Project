using Microsoft.EntityFrameworkCore.Migrations;

namespace MCC61_API_Project.Migrations
{
    public partial class fixingBugEduAndUnivError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_m_university",
                newName: "UniversityID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tb_m_education",
                newName: "EducationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniversityID",
                table: "tb_m_university",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "EducationID",
                table: "tb_m_education",
                newName: "ID");
        }
    }
}
