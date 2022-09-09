using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addrelationuseremployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employee_Id",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Employee_Id",
                table: "Users",
                column: "Employee_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_Employee_Id",
                table: "Users",
                column: "Employee_Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_Employee_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Employee_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Employee_Id",
                table: "Users");
        }
    }
}
