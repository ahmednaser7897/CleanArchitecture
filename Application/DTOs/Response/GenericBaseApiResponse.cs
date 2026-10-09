
namespace Application.DTOs.Response;

public class BaseApiResponse<T> : BaseApiResponse
{
    public T? Data { get; set; }


    public BaseApiResponse() { }

    private BaseApiResponse(int statusCode, string message, bool status = true, PagedResponse? pagination = null, T? data = default)
        : base(statusCode, message, status, pagination)
    {
        Data = data;
    }

    public static BaseApiResponse<T> Success(T data, string message = "Success", PagedResponse? pagination = null, int statusCode = 200)
    {
        return new BaseApiResponse<T>(statusCode: statusCode, message: message, status: true, pagination: pagination, data: data);
    }

    public static BaseApiResponse<T> Fail(string message = "Failed", int statusCode = 400)
    {
        return new BaseApiResponse<T>(statusCode: statusCode, message: message, status: false, pagination: null, data: default);
    }
}

