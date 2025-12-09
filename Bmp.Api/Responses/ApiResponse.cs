namespace Bmp.Api.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? Error_code { get; set; }
    public T Data { get; set; } = default!;

    public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, int errorCode = 1000)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Error_code = errorCode,
            Data = default!
        };
    }
}
