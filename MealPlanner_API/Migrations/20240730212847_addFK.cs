using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealPlanner_API.Migrations
{
    /// <inheritdoc />
    public partial class addFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealID",
                table: "MealIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 30, 22, 28, 46, 565, DateTimeKind.Local).AddTicks(7297), new DateTime(2024, 7, 30, 22, 28, 46, 565, DateTimeKind.Local).AddTicks(7337) });

            migrationBuilder.CreateIndex(
                name: "IX_MealIngredients_MealID",
                table: "MealIngredients",
                column: "MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_MealIngredients_Meals_MealID",
                table: "MealIngredients",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealIngredients_Meals_MealID",
                table: "MealIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MealIngredients_MealID",
                table: "MealIngredients");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "MealIngredients");

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 7, 28, 20, 37, 55, 559, DateTimeKind.Local).AddTicks(873), new DateTime(2024, 7, 28, 20, 37, 55, 559, DateTimeKind.Local).AddTicks(914) });
        }
    }
}
