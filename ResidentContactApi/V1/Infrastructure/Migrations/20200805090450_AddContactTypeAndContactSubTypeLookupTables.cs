using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentContactApi.V1.Infrastructure.Migrations
{
    public partial class AddContactTypeAndContactSubTypeLookupTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "contact_details");

            migrationBuilder.DropColumn(
                name: "subtype",
                table: "contact_details");

            migrationBuilder.AddColumn<int>(
                name: "contact_subtype_lookup_id",
                table: "contact_details",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "contact_type_lookup_id",
                table: "contact_details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "contact-sub-type-lookup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact-sub-type-lookup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact-type-lookup",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact-type-lookup", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contact_details_contact_subtype_lookup_id",
                table: "contact_details",
                column: "contact_subtype_lookup_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_details_contact_type_lookup_id",
                table: "contact_details",
                column: "contact_type_lookup_id");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact-sub-type-lookup_contact_subtype_loo~",
                table: "contact_details",
                column: "contact_subtype_lookup_id",
                principalTable: "contact-sub-type-lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact-type-lookup_contact_type_lookup_id",
                table: "contact_details",
                column: "contact_type_lookup_id",
                principalTable: "contact-type-lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact-sub-type-lookup_contact_subtype_loo~",
                table: "contact_details");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact-type-lookup_contact_type_lookup_id",
                table: "contact_details");

            migrationBuilder.DropTable(
                name: "contact-sub-type-lookup");

            migrationBuilder.DropTable(
                name: "contact-type-lookup");

            migrationBuilder.DropIndex(
                name: "IX_contact_details_contact_subtype_lookup_id",
                table: "contact_details");

            migrationBuilder.DropIndex(
                name: "IX_contact_details_contact_type_lookup_id",
                table: "contact_details");

            migrationBuilder.DropColumn(
                name: "contact_subtype_lookup_id",
                table: "contact_details");

            migrationBuilder.DropColumn(
                name: "contact_type_lookup_id",
                table: "contact_details");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "contact_details",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "subtype",
                table: "contact_details",
                type: "text",
                nullable: true);
        }
    }
}
