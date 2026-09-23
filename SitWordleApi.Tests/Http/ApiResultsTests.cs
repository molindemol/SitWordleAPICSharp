using Microsoft.AspNetCore.Http.HttpResults;

namespace SitWordleApi.Tests;

public class ApiResultsTests
{
    
    private sealed record Payload(string Word);

    [Fact]
    public void Ok_WrapsDataInEnvelopeWithNullError()
    {
        var payload = new Payload("test");

        var result = ApiResults.Ok(payload);

        var json = Assert.IsType<JsonHttpResult<ApiResponse<Payload>>>(result);
        Assert.Equal(200, json.StatusCode);
        Assert.Null(json.Value!.Error);
        Assert.Same(payload, json.Value.Data);
    }

    [Theory]
    [InlineData(400)]
    [InlineData(404)]
    [InlineData(500)]
    public void Fail_UsesGivenStatus(int status)
    {
        var result = ApiResults.Fail("oops", status);
        var json = Assert.IsType<JsonHttpResult<ApiResponse<object>>>(result);
        Assert.Equal(status, json.StatusCode);
    }
}