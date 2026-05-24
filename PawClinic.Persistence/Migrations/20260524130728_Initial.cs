using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PawClinic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Vets",
                columns: table => new
                {
                    VetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Specialisation = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vets", x => x.VetId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Species = table.Column<int>(type: "INTEGER", nullable: false),
                    Breed = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ScheduledDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReasonForVisit = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Vets_VetId",
                        column: x => x.VetId,
                        principalTable: "Vets",
                        principalColumn: "VetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "Address", "CreatedBy", "CreatedDate", "Email", "LastModifiedBy", "LastModifiedDate", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), "14 Elm Street, Springfield", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "emily.johnson@example.com", null, null, "Mrs. Emily Johnson", "555-0101" },
                    { new Guid("b2000000-0000-0000-0000-000000000002"), "7 Maple Avenue, Shelbyville", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pierre.dubois@example.com", null, null, "Mr. Pierre Dubois", "555-0102" },
                    { new Guid("b3000000-0000-0000-0000-000000000003"), "23 Oak Lane, Capital City", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "carmen.reyes@example.com", null, null, "Ms. Carmen Reyes", "555-0103" }
                });

            migrationBuilder.InsertData(
                table: "Vets",
                columns: new[] { "VetId", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name", "Specialisation" },
                values: new object[,]
                {
                    { new Guid("a1000000-0000-0000-0000-000000000001"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Dr. Sarah Patel", 0 },
                    { new Guid("a2000000-0000-0000-0000-000000000002"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Dr. James Okafor", 1 },
                    { new Guid("a3000000-0000-0000-0000-000000000003"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Dr. Mei Lin", 2 }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "Breed", "CreatedBy", "CreatedDate", "DateOfBirth", "IsArchived", "LastModifiedBy", "LastModifiedDate", "Name", "OwnerId", "Species" },
                values: new object[,]
                {
                    { new Guid("c1000000-0000-0000-0000-000000000001"), "Labrador Retriever", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Bella", new Guid("b1000000-0000-0000-0000-000000000001"), 0 },
                    { new Guid("c2000000-0000-0000-0000-000000000002"), "Maine Coon", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Oscar", new Guid("b2000000-0000-0000-0000-000000000002"), 1 },
                    { new Guid("c3000000-0000-0000-0000-000000000003"), "Holland Lop", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Pip", new Guid("b2000000-0000-0000-0000-000000000002"), 2 },
                    { new Guid("c4000000-0000-0000-0000-000000000004"), "Border Collie", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "Luna", new Guid("b3000000-0000-0000-0000-000000000003"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Notes", "PetId", "ReasonForVisit", "ScheduledDateTime", "Status", "VetId" },
                values: new object[,]
                {
                    { new Guid("d1000000-0000-0000-0000-000000000001"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Routine vaccination administered. All clear. Next visit in 12 months.", new Guid("c1000000-0000-0000-0000-000000000001"), "Annual vaccination", new DateTime(2025, 4, 10, 10, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a1000000-0000-0000-0000-000000000001") },
                    { new Guid("d2000000-0000-0000-0000-000000000002"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Mild tartar build-up noted. Dental clean recommended in 6 months.", new Guid("c2000000-0000-0000-0000-000000000002"), "Dental check-up", new DateTime(2025, 5, 3, 14, 0, 0, 0, DateTimeKind.Utc), 1, new Guid("a3000000-0000-0000-0000-000000000003") },
                    { new Guid("d3000000-0000-0000-0000-000000000003"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Owner cancelled — will reschedule.", new Guid("c3000000-0000-0000-0000-000000000003"), "Weight check", new DateTime(2025, 5, 20, 11, 0, 0, 0, DateTimeKind.Utc), 2, new Guid("a1000000-0000-0000-0000-000000000001") },
                    { new Guid("d4000000-0000-0000-0000-000000000004"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("c1000000-0000-0000-0000-000000000001"), "Annual check-up", new DateTime(2027, 6, 15, 10, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("a1000000-0000-0000-0000-000000000001") },
                    { new Guid("d5000000-0000-0000-0000-000000000005"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, new Guid("c4000000-0000-0000-0000-000000000004"), "New patient consultation", new DateTime(2027, 6, 20, 9, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("a2000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId",
                table: "Appointments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VetId",
                table: "Appointments",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Vets");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
