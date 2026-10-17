namespace AcademicHybridApp.Services;

// Lokasi alamat API
public static class ApiSettings
{
    // Windows: API komputer local.
    // Android emulator: 10.0.2.2 komputer host.
    public static string BaseAddress =>
        DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5072/"
            : "http://localhost:5072/";
}