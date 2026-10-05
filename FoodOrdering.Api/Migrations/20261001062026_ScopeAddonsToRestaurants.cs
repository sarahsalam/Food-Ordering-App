using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrdering.Api.Migrations
{
    public partial class ScopeAddonsToRestaurants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Addons",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE "Addons" AS addon
                SET "RestaurantId" = linked."RestaurantId"
                FROM (
                    SELECT pa."AddonId", (array_agg(DISTINCT c."RestaurantId"))[1] AS "RestaurantId"
                    FROM "ProductAddons" pa
                    JOIN "Products" p ON p."Id" = pa."ProductId"
                    JOIN "Categories" c ON c."Id" = p."CategoryId"
                    GROUP BY pa."AddonId"
                    HAVING count(DISTINCT c."RestaurantId") = 1
                ) linked
                WHERE addon."Id" = linked."AddonId";
                UPDATE "Addons" SET "IsActive" = false WHERE "RestaurantId" IS NULL;
                """);

            migrationBuilder.CreateIndex(
                name: "IX_Addons_RestaurantId",
                table: "Addons",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Restaurants_RestaurantId",
                table: "Addons",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Restaurants_RestaurantId",
                table: "Addons");

            migrationBuilder.DropIndex(
                name: "IX_Addons_RestaurantId",
                table: "Addons");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Addons");
        }
    }
}
