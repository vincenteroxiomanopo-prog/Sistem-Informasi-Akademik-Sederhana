using System.Net.Http.Json;

namespace AcademicHybridApp.Services;

// Logic semua service: mengirim request, membaca hasil,
// menangani API yang tidak bisa dihubungi.
public abstract class ApiServiceBase
{
    protected readonly HttpClient Http;

    protected ApiServiceBase(HttpClient http)
    {
        Http = http;
    }

    // Request GET
    protected async Task<ServiceResult<T>> GetDataAsync<T>(string url)
    {
        try
        {
            var response = await Http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return ServiceResult<T>.Failure(await ApiErrorReader.ReadMessageAsync(response));
            }

            var data = await response.Content.ReadFromJsonAsync<T>();

            return data is null
                ? ServiceResult<T>.Failure("Data dari server kosong atau tidak dapat dibaca.")
                : ServiceResult<T>.Success(data);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return ServiceResult<T>.Failure(ApiErrorReader.ServerUnreachable);
        }
    }

    // Request POST, PUT, DELETE
    protected async Task<ServiceResult> ExecuteAsync(
        Func<Task<HttpResponseMessage>> request, string successMessage)
    {
        try
        {
            var response = await request();

            return response.IsSuccessStatusCode
                ? ServiceResult.Success(successMessage)
                : ServiceResult.Failure(await ApiErrorReader.ReadMessageAsync(response));
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return ServiceResult.Failure(ApiErrorReader.ServerUnreachable);
        }
    }
}