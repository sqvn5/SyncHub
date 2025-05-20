var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *** 1. Add CORS services ***
// Define a CORS policy. You can name it whatever you like.
// Replace "https://localhost:XXXX" and "http://localhost:YYYY" with the actual URLs
// your Blazor app runs on during development. Check your Blazor app's launchSettings.json.
// It's common for Blazor Server apps to run on an HTTPS port.
string[] blazorAppOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ??
                            new string[] { "https://localhost:7001", "http://localhost:5001" }; // Example ports, adjust these!


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", // You can name the policy
        policyBuilder =>
        {
            policyBuilder.WithOrigins(blazorAppOrigins) // Specify the origin(s) of your Blazor app
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Good for debugging API issues
}

app.UseHttpsRedirection();

// *** 2. Use CORS middleware ***
// This MUST come before UseAuthorization and MapControllers.
app.UseCors("AllowBlazorApp"); // Apply the CORS policy you defined

app.UseAuthorization();
app.MapControllers();

app.Run();