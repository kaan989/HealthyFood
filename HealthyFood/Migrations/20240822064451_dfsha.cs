﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthyFood.Migrations
{
    /// <inheritdoc />
    public partial class dfsha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AppUserId",
                table: "Recipes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AppUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Recipes");
        }
    }
}
