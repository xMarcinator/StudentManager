using StudentManager.Models;
using Xunit.Abstractions;

namespace StudentManagerTest;
using Microsoft.AspNetCore.Mvc.Testing;

public class BasicTests 
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper output;
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        output.WriteLine("test initated");
        _factory = factory;
        this.output = output;

        //factory.Services.GetService(typeof(StudentManagementDb))
    }
    
    [Theory(Skip = "can't use live db in github test currently")]
    [InlineData("/")]
    [InlineData("/Student/List")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        output.WriteLine("test starting");
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType?.ToString());
    }
}
