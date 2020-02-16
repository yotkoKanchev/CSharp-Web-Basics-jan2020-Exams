using Microsoft.EntityFrameworkCore.Migrations;

namespace PANDA.Data.Migrations
{
    public partial class RemovePasswordModelLengthLimitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Packages_PackageId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Users_RecipientId",
                table: "Receipts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Packages_PackageId",
                table: "Receipts",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Users_RecipientId",
                table: "Receipts",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Packages_PackageId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Users_RecipientId",
                table: "Receipts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Users_RecipientId",
                table: "Packages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Packages_PackageId",
                table: "Receipts",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Users_RecipientId",
                table: "Receipts",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
