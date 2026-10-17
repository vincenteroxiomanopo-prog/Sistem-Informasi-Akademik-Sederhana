namespace AcademicHybridApp.Services;

// Loc Notif 
public class NotificationService
{
    private int _version;

    public string? Message { get; private set; }
    public bool IsSuccess { get; private set; }

    public event Action? OnChange;

    public void ShowSuccess(string message) => Show(message, true);

    public void ShowError(string message) => Show(message, false);

    public void Clear()
    {
        Message = null;
        OnChange?.Invoke();
    }

    private void Show(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
        OnChange?.Invoke();

        // Pesan hilang setelah 5 detik.
        _ = HideLaterAsync(++_version);
    }

    private async Task HideLaterAsync(int version)
    {
        await Task.Delay(TimeSpan.FromSeconds(5));

        // Hanya hapus jika belum ada pesan terbaru.
        if (version == _version)
        {
            Clear();
        }
    }
}