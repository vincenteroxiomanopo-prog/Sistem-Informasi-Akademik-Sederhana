using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademicApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dosen",
                columns: table => new
                {
                    DosenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nidn = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    NamaDosen = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Prodi = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosen", x => x.DosenId);
                });

            migrationBuilder.CreateTable(
                name: "Mahasiswa",
                columns: table => new
                {
                    MahasiswaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nim = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    NamaMahasiswa = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Angkatan = table.Column<int>(type: "INTEGER", nullable: false),
                    DosenId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahasiswa", x => x.MahasiswaId);
                    table.ForeignKey(
                        name: "FK_Mahasiswa_Dosen_DosenId",
                        column: x => x.DosenId,
                        principalTable: "Dosen",
                        principalColumn: "DosenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dosen",
                columns: new[] { "DosenId", "Email", "NamaDosen", "Nidn", "Prodi" },
                values: new object[,]
                {
                    { 1, "anita@kampus.ac.id", "Dr. Anita Wijaya", "0512345678", "Sistem Informasi" },
                    { 2, "budi.hartono@kampus.ac.id", "Budi Hartono, M.Kom.", "0523456789", "Informatika" },
                    { 3, "citra.lestari@kampus.ac.id", "Citra Lestari, M.T.", "0534567890", "Sistem Informasi" }
                });

            migrationBuilder.InsertData(
                table: "Mahasiswa",
                columns: new[] { "MahasiswaId", "Angkatan", "DosenId", "Email", "NamaMahasiswa", "Nim" },
                values: new object[,]
                {
                    { 1, 2023, 1, "budi@student.kampus.ac.id", "Budi Santoso", "72230001" },
                    { 2, 2023, 1, "sari@student.kampus.ac.id", "Sari Wulandari", "72230002" },
                    { 3, 2024, 2, "dimas@student.kampus.ac.id", "Dimas Prasetyo", "72240001" },
                    { 4, 2024, 2, "putri@student.kampus.ac.id", "Putri Maharani", "72240002" },
                    { 5, 2022, 2, "rizky@student.kampus.ac.id", "Rizky Ananda", "72220001" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dosen_Nidn",
                table: "Dosen",
                column: "Nidn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mahasiswa_DosenId",
                table: "Mahasiswa",
                column: "DosenId");

            migrationBuilder.CreateIndex(
                name: "IX_Mahasiswa_Nim",
                table: "Mahasiswa",
                column: "Nim",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mahasiswa");

            migrationBuilder.DropTable(
                name: "Dosen");
        }
    }
}
