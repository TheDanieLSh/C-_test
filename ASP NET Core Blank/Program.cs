using System.Text.Json;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var HTTP = new HttpClient();

app.MapGet("/", () => "Hello World!");

app.MapPost("/file_process", async (HttpRequest req) =>
{
    using (var streamReader = new StreamReader(req.Body))
    {
        string body = await streamReader.ReadToEndAsync();

        HttpResponseMessage resp = await HTTP.GetAsync(body);

        if (resp.Content.Headers.ContentType.MediaType == "application/json")
        {
            var json = await resp.Content.ReadAsStringAsync();
            return json;
        }

        return $"Failed to fetch {body}";
    }
});

app.Run();
