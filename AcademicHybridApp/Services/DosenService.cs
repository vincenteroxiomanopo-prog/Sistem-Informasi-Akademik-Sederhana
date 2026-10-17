using System.Net.Http.Json;
using AcademicHybridApp.Models;

namespace AcademicHybridApp.Services;

public class DosenService : ApiServiceBase
{
    private const string Endpoint = "api/dosen";

    public DosenService(HttpClient http) : base(http)
    {
    }

    public Task<ServiceResult<List<DosenModel>>> GetAllAsync() =>
        GetDataAsync<List<DosenModel>>(Endpoint);

    public Task<ServiceResult<DosenModel>> GetByIdAsync(int id) =>
        GetDataAsync<DosenModel>($"{Endpoint}/{id}");

    public Task<ServiceResult> CreateAsync(DosenModel dosen) =>
        ExecuteAsync(() => Http.PostAsJsonAsync(Endpoint, dosen),
            "Data dosen berhasil ditambahkan.");

    public Task<ServiceResult> UpdateAsync(DosenModel dosen) =>
        ExecuteAsync(() => Http.PutAsJsonAsync($"{Endpoint}/{dosen.DosenId}", dosen),
            "Data dosen berhasil diperbarui.");

    public Task<ServiceResult> DeleteAsync(int id) =>
        ExecuteAsync(() => Http.DeleteAsync($"{Endpoint}/{id}"),
            "Data dosen berhasil dihapus.");
}