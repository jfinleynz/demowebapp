using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Content(RenderPage(null), "text/html"));
app.MapPost("/", () => Results.Content(RenderPage(Random.Shared.Next(10, 101)), "text/html"));

app.MapGet("/error", () => Results.Content("<h1>Something went wrong.</h1>", "text/html"));

app.Run();

static string RenderPage(int? randomNumber)
{
    var resultHtml = randomNumber.HasValue
        ? $"<div class=\"result\">Random number: <strong>{randomNumber}</strong></div>"
        : string.Empty;

    return """
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="utf-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title>Demo Web App</title>
  <style>
    body { font-family: Arial, sans-serif; background: #f5f7fb; color: #202124; margin: 0; padding: 2rem; }
    .container { max-width: 560px; margin: 0 auto; background: white; border-radius: 12px; padding: 2rem; box-shadow: 0 12px 32px rgba(0,0,0,.08); text-align: center; }
    button { font-size: 1rem; padding: 0.8rem 1.4rem; border: none; border-radius: 8px; background: #0b5ed7; color: white; cursor: pointer; }
    button:hover { background: #084298; }
    .result { margin-top: 1.5rem; padding: 1rem; border-radius: 8px; background: #e7f1ff; color: #0b3e85; font-size: 1.2rem; }
  </style>
</head>
<body>
  <div class="container">
    <h1>Demo Web App</h1>
    <p>Click the button to generate a random number between 10 and 100.</p>
    <form method="post">
      <button type="submit">Demo</button>
    </form>
""" + resultHtml + """
  </div>
</body>
</html>
""";
}
