using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tasks_TaskId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Tasks_TaskId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_TaskId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_TaskId",
                table: "Attachments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2024caa4-5b34-45fe-9ca7-d44f3c940ef0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93304d99-18cf-49a8-86e9-74f9f53a1b8c");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Tasks",
                newName: "IssueId");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Reports",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Attachments",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2360529-57f7-484f-8138-eb707b27771d", "7244c3ca-f1d6-40b4-9c6f-ddc54522ec74", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1b1c1dea-6873-458a-8152-4ff2258ea3f8", "7f5fb6f0-846d-455b-b39a-fa193e480a76", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IssueId",
                table: "Reports",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_IssueId",
                table: "Attachments",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tasks_IssueId",
                table: "Attachments",
                column: "IssueId",
                principalTable: "Tasks",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Tasks_IssueId",
                table: "Reports",
                column: "IssueId",
                principalTable: "Tasks",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Tasks_IssueId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Tasks_IssueId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_IssueId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_IssueId",
                table: "Attachments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b1c1dea-6873-458a-8152-4ff2258ea3f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2360529-57f7-484f-8138-eb707b27771d");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Tasks",
                newName: "TaskId");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaskId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "93304d99-18cf-49a8-86e9-74f9f53a1b8c", "2da795bd-3a2a-4ff4-9edf-6523b7560115", "Viewer", "VIEWER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2024caa4-5b34-45fe-9ca7-d44f3c940ef0", "c2b32c62-7979-49f7-a7e2-e22bcff2e3e8", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TaskId",
                table: "Reports",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TaskId",
                table: "Attachments",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Tasks_TaskId",
                table: "Attachments",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Tasks_TaskId",
                table: "Reports",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
