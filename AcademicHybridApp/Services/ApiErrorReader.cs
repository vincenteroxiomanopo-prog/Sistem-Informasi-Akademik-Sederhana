using System.Net;
using System.Text.Json;

namespace AcademicHybridApp.Services;

public static class ApiErrorReader
{
    // Massage if API Tidak aktif/terakses
    public const string ServerUnreachable =
        "Tidak dapat menghubungi server. Pastikan API sudah berjalan, lalu coba lagi.";

    // Respons massage error dari API 
    public static async Task<string> ReadMessageAsync(HttpResponseMessage response)
    {
        try
        {
            var json = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(json))
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Object)
                {
                    // Bentuk 1: { "message": "..." } (404, 409, dan 400)
                    if (root.TryGetProperty("message", out var message)
                        && message.ValueKind == JsonValueKind.String)
                    {
                        return message.GetString() ?? string.Empty;
                    }

                    // Bentuk 2: validasi otomatis ASP.NET: { "errors": { "Email": ["..."] } }
                    if (root.TryGetProperty("errors", out var errors)
                        && errors.ValueKind == JsonValueKind.Object)
                    {
                        var messages = new List<string>();
                        foreach (var field in errors.EnumerateObject())
                        {
                            if (field.Value.ValueKind != JsonValueKind.Array) continue;
                            foreach (var item in field.Value.EnumerateArray())
                            {
                                if (item.GetString() is { } text) messages.Add(text);
                            }
                        }

                        if (messages.Count > 0) return string.Join(" ", messages);
                    }
                }
            }
        }
        catch (JsonException)
        {
        }

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => "Data tidak ditemukan.",
            HttpStatusCode.Conflict => "Data bentrok dengan data yang sudah ada.",
            HttpStatusCode.BadRequest => "Data yang dikirim tidak valid.",
            _ => $"Terjadi kesalahan pada server (kode {(int)response.StatusCode})."
        };
    }
}