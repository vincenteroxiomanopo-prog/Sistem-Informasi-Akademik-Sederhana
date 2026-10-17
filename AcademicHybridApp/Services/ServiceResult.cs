namespace AcademicHybridApp.Services;

// CUD operation tanpa data
public class ServiceResult
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;

    public static ServiceResult Success(string message = "") =>
        new() { IsSuccess = true, Message = message };

    public static ServiceResult Failure(string message) =>
        new() { IsSuccess = false, Message = message };
}

// Hasil operasi yang membawa data (ambil daftar/detail)
public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public T? Data { get; init; }

    public static ServiceResult<T> Success(T data) =>
        new() { IsSuccess = true, Data = data };

    public static ServiceResult<T> Failure(string message) =>
        new() { IsSuccess = false, Message = message };
}