using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomerServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class addClientLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientAddressLookups",
                columns: table => new
                {
                    ClientAddressLookupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAddressLookups", x => x.ClientAddressLookupId);
                    table.ForeignKey(
                        name: "FK_ClientAddressLookups_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAddressLookups_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientEmailLookups",
                columns: table => new
                {
                    ClientEmailLookupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEmailLookups", x => x.ClientEmailLookupId);
                    table.ForeignKey(
                        name: "FK_ClientEmailLookups_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientEmailLookups_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPhoneLookups",
                columns: table => new
                {
                    ClientPhoneLookupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPhoneLookups", x => x.ClientPhoneLookupId);
                    table.ForeignKey(
                        name: "FK_ClientPhoneLookups_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPhoneLookups_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7579));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7598));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddressLookups_AddressId",
                table: "ClientAddressLookups",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAddressLookups_ClientId",
                table: "ClientAddressLookups",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmailLookups_ClientId",
                table: "ClientEmailLookups",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientEmailLookups_EmailId",
                table: "ClientEmailLookups",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPhoneLookups_ClientId",
                table: "ClientPhoneLookups",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPhoneLookups_PhoneId",
                table: "ClientPhoneLookups",
                column: "PhoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAddressLookups");

            migrationBuilder.DropTable(
                name: "ClientEmailLookups");

            migrationBuilder.DropTable(
                name: "ClientPhoneLookups");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5402));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5405));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5399));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5389));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5409));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 19, 21, 29, 52, 75, DateTimeKind.Utc).AddTicks(5306));
        }
    }
}
