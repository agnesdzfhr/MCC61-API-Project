using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCC61_API_Project.Migrations
{
    public partial class addAtributAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "tb_tr_account",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "tb_tr_account",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "tb_tr_account",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "tb_tr_account",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isUsed",
                table: "tb_tr_account",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "tb_m_education",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "tb_tr_account");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "tb_tr_account");

            migrationBuilder.DropColumn(
                name: "isUsed",
                table: "tb_tr_account");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "tb_tr_account",
                newName: "password");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "tb_tr_account",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "tb_m_education",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
