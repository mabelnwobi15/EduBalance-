using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduBalance.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudyBalanceId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StressCheckIns_AspNetUsers_UserId",
                table: "StressCheckIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StressCheckIns",
                table: "StressCheckIns");

            migrationBuilder.RenameTable(
                name: "StressCheckIns",
                newName: "StressCheckIn");

            migrationBuilder.RenameIndex(
                name: "IX_StressCheckIns_UserId",
                table: "StressCheckIn",
                newName: "IX_StressCheckIn_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StressCheckIn",
                table: "StressCheckIn",
                column: "StressCheckInId");

            migrationBuilder.AddForeignKey(
                name: "FK_StressCheckIn_AspNetUsers_UserId",
                table: "StressCheckIn",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StressCheckIn_AspNetUsers_UserId",
                table: "StressCheckIn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StressCheckIn",
                table: "StressCheckIn");

            migrationBuilder.RenameTable(
                name: "StressCheckIn",
                newName: "StressCheckIns");

            migrationBuilder.RenameIndex(
                name: "IX_StressCheckIn_UserId",
                table: "StressCheckIns",
                newName: "IX_StressCheckIns_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StressCheckIns",
                table: "StressCheckIns",
                column: "StressCheckInId");

            migrationBuilder.AddForeignKey(
                name: "FK_StressCheckIns_AspNetUsers_UserId",
                table: "StressCheckIns",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
