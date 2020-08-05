using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentContactApi.V1.Infrastructure.Migrations
{
    public partial class AddContactsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contact_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_default = table.Column<bool>(nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    added_by = table.Column<string>(nullable: true),
                    date_added = table.Column<DateTime>(nullable: false),
                    modified_by = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    contact_details_value = table.Column<string>(nullable: true),
                    date_modified = table.Column<DateTime>(nullable: false),
                    subtype = table.Column<string>(nullable: true),
                    resident_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_details_residents_resident_id",
                        column: x => x.resident_id,
                        principalTable: "residents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_details_resident_id",
                table: "contact_details",
                column: "resident_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact_details");
        }
    }
}
