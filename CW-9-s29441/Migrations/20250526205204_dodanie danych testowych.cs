using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CW_9_s29441.Migrations
{
    /// <inheritdoc />
    public partial class dodaniedanychtestowych : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "s29441@pjwstk.edu.pl", "Adam", "Skorupski" },
                    { 2, "tomasz@tomaszowski.edu.pl", "Tomasz", "Tomaszowski" },
                    { 3, "anna@annowska.pl", "Anna", "Annowska" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Paracetamol", "APAP", "przeciwbólowy" },
                    { 2, "Ibuprofen", "Ibuprom", "przeciwbólowy" },
                    { 3, "Witamina C", "Rutinoscorbin", "odpornościowy" }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "Birthdate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Magdalena", "Lena" },
                    { 2, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Katarzyna", "Kata" },
                    { 3, new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aleksandra", "Mandra" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 2, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 3, new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 4, new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Prescription_Medicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "codziennie wieczorem", 5 },
                    { 2, 1, "raz na tydzień", null },
                    { 2, 2, "co 2 dni", 1 },
                    { 3, 3, "codziennie rano", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Prescription_Medicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);
        }
    }
}
