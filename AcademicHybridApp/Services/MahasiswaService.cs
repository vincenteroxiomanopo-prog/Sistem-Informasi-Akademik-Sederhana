using System.Net.Http.Json;
using AcademicHybridApp.Models;

namespace AcademicHybridApp.Services;

public class MahasiswaService : ApiServiceBase
{
    private const string Endpoint = "api/mahasiswa";

    public MahasiswaService(HttpClient http) : base(http)
    {
    }

    public Task<ServiceResult<List<MahasiswaModel>>> GetAllAsync(string? cari = null)
    {
        var url = string.IsNullOrWhiteSpace(cari)
            ? Endpoint
            : $"{Endpoint}?cari={Uri.EscapeDataString(cari.Trim())}";

        return GetDataAsync<List<MahasiswaModel>>(url);
    }

    public Task<ServiceResult<MahasiswaModel>> GetByIdAsync(int id) =>
        GetDataAsync<MahasiswaModel>($"{Endpoint}/{id}");

    public Task<ServiceResult> CreateAsync(MahasiswaModel mahasiswa) =>
        ExecuteAsync(() => Http.PostAsJsonAsync(Endpoint, mahasiswa),
            "Data mahasiswa berhasil ditambahkan.");

    public Task<ServiceResult> UpdateAsync(MahasiswaModel mahasiswa) =>
        ExecuteAsync(() => Http.PutAsJsonAsync($"{Endpoint}/{mahasiswa.MahasiswaId}", mahasiswa),
            "Data mahasiswa berhasil diperbarui.");

    public Task<ServiceResult> DeleteAsync(int id) =>
        ExecuteAsync(() => Http.DeleteAsync($"{Endpoint}/{id}"),
            "Data mahasiswa berhasil dihapus.");
}