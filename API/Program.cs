using API.Extensions;
using NLog;
using NLog.Web;

var logger = LogManager
    .Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    ServiceExtension.RegisterMapsterConfiguration();
    builder.Services
        .AddAuthentication(builder.Configuration)
        .RegisterServices(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGenWithJwtTokenHeaders();

    // Enable serving static files from wwwroot folder
    builder.Services.AddDirectoryBrowser();

    // Configure static file options
    builder.Services.Configure<StaticFileOptions>(options =>
    {
        options.ServeUnknownFileTypes = true; // Serve all file types
    });

    Application.Extensions.ServiceExtension.ConfigureValidator(builder.Services);
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application encountered a critical error.");
    throw;
}
finally
{
    LogManager.Shutdown();
}