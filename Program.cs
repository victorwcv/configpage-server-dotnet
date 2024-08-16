using backend_dotnet.Configuration;
using backend_dotnet.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using dotenv.net;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
                       ?? throw new InvalidOperationException("MongoDB connection string is not configured.");
var databaseName = builder.Configuration.GetSection("MongoDB:DatabaseName").Value
                    ?? throw new InvalidOperationException("MongoDB database name is not configured.");

var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
var corsConnection = Environment.GetEnvironmentVariable("CORS_CONNECTION");

var allowedOrigins = environment == "development" ? "http://localhost:5173" : corsConnection;

// CORS settings
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        }
    );
});

// MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = connectionString;
    options.DatabaseName = databaseName;
}
   );

// Register the MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


// Register the MongoDB database
builder.Services.AddScoped(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Register the UserService
builder.Services.AddScoped<UserService>();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// CORS
app.UseCors("AllowSpecificOrigins");


app.UseHttpsRedirection();

// Controladores
app.MapControllers();

// Test endpoint
app.MapGet("/", () => Results.Json(new { connected = true }));

// Start the server
app.Run();
