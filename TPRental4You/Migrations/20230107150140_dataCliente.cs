﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRental4You.Migrations
{
    public partial class dataCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeRegisto",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeRegisto",
                table: "AspNetUsers");
        }
    }
}
