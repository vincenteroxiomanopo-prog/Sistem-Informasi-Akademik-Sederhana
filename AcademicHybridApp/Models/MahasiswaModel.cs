using System.ComponentModel.DataAnnotations;

namespace AcademicHybridApp.Models;

public class MahasiswaModel
{
    public int MahasiswaId { get; set; }

    [Required(ErrorMessage = "NIM wajib diisi.")]
    [StringLength(20, ErrorMessage = "NIM maksimal 20 karakter.")]
    public string Nim { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nama mahasiswa wajib diisi.")]
    [StringLength(100, ErrorMessage = "Nama mahasiswa maksimal 100 karakter.")]
    public string NamaMahasiswa { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email wajib diisi.")]
    [StringLength(100, ErrorMessage = "Email maksimal 100 karakter.")]
    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    public string Email { get; set; } = string.Empty;

    [Range(2020, 2026, ErrorMessage = "Angkatan harus antara 2020 dan 2026.")]
    public int Angkatan { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Dosen wali wajib dipilih.")]
    public int DosenId { get; set; }

    // Hanya diisi oleh API saat read data (untuk ditampilkan di daftar)
    public string NamaDosenWali { get; set; } = string.Empty;
}