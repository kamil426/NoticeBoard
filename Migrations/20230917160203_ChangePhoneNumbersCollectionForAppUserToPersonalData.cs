using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoticeBoard.Migrations
{
    public partial class ChangePhoneNumbersCollectionForAppUserToPersonalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_AspNetUsers_ApplicationUserId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_ApplicationUserId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PhoneNumbers");

            migrationBuilder.AddColumn<int>(
                name: "PersonalDataId",
                table: "PhoneNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonalDataId",
                table: "PhoneNumbers",
                column: "PersonalDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_PersonalDatas_PersonalDataId",
                table: "PhoneNumbers",
                column: "PersonalDataId",
                principalTable: "PersonalDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_PersonalDatas_PersonalDataId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_PersonalDataId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "PersonalDataId",
                table: "PhoneNumbers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PhoneNumbers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_ApplicationUserId",
                table: "PhoneNumbers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_AspNetUsers_ApplicationUserId",
                table: "PhoneNumbers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
