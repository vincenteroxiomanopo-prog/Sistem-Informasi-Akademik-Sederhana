using AcademicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Data;

public class AcademicDbContext : DbContext
{
    public AcademicDbContext(DbContextOptions<AcademicDbContext> options) : base(options)
    {
    }

    //DbSet = 1 tabel
    public DbSet<Dosen> Dosen => Set<Dosen>();
    public DbSet<Mahasiswa> Mahasiswa => Set<Mahasiswa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dosen>(e =>
        {
            e.Property(d => d.Nidn).IsRequired().HasMaxLength(20);
            e.Property(d => d.NamaDosen).IsRequired().HasMaxLength(100);
            e.Property(d => d.Email).IsRequired().HasMaxLength(100);
            e.Property(d => d.Prodi).IsRequired().HasMaxLength(100);
            e.HasIndex(d => d.Nidn).IsUnique(); 
        });

        modelBuilder.Entity<Mahasiswa>(e =>
        {
            e.Property(m => m.Nim).IsRequired().HasMaxLength(20);
            e.Property(m => m.NamaMahasiswa).IsRequired().HasMaxLength(100);
            e.Property(m => m.Email).IsRequired().HasMaxLength(100);
            e.HasIndex(m => m.Nim).IsUnique();   

            e.HasOne(m => m.Dosen)
             .WithMany(d => d.DaftarMahasiswa)
             .HasForeignKey(m => m.DosenId)
             .OnDelete(DeleteBehavior.Restrict);
        });
        // Data awal dosen & mahasiswa
        modelBuilder.Entity<Dosen>().HasData(
            new Dosen { DosenId = 1, Nidn = "0512345678", NamaDosen = "Dr. Anita Wijaya", Email = "anita@kampus.ac.id", Prodi = "Sistem Informasi" },
            new Dosen { DosenId = 2, Nidn = "0523456789", NamaDosen = "Budi Hartono, M.Kom.", Email = "budi.hartono@kampus.ac.id", Prodi = "Informatika" },
            new Dosen { DosenId = 3, Nidn = "0534567890", NamaDosen = "Citra Lestari, M.T.", Email = "citra.lestari@kampus.ac.id", Prodi = "Sistem Informasi" }
        );

        modelBuilder.Entity<Mahasiswa>().HasData(
            new Mahasiswa { MahasiswaId = 1, Nim = "72230001", NamaMahasiswa = "Budi Santoso", Email = "budi@student.kampus.ac.id", Angkatan = 2023, DosenId = 1 },
            new Mahasiswa { MahasiswaId = 2, Nim = "72230002", NamaMahasiswa = "Sari Wulandari", Email = "sari@student.kampus.ac.id", Angkatan = 2023, DosenId = 1 },
            new Mahasiswa { MahasiswaId = 3, Nim = "72240001", NamaMahasiswa = "Dimas Prasetyo", Email = "dimas@student.kampus.ac.id", Angkatan = 2024, DosenId = 2 },
            new Mahasiswa { MahasiswaId = 4, Nim = "72240002", NamaMahasiswa = "Putri Maharani", Email = "putri@student.kampus.ac.id", Angkatan = 2024, DosenId = 2 },
            new Mahasiswa { MahasiswaId = 5, Nim = "72220001", NamaMahasiswa = "Rizky Ananda", Email = "rizky@student.kampus.ac.id", Angkatan = 2022, DosenId = 2 }
        );
    }
}