public static class ApiResults
{
    public static IResult Ok<T>(T data, IReadOnlyDictionary<string, object?>? meta = null, int status = 200)
    {
            var body = new ApiResponse<T>(data, null, meta);
            return Results.Json(body, statusCode: status);
    }
    public static IResult Fail(string error, int status)
    {
        var body = new ApiResponse<object>(null,error ,null);
        return Results.Json(body, statusCode: status);
    }
}