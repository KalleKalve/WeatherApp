
using WeatherApp.DatabaseConnector;
using Microsoft.EntityFrameworkCore;
using WeatherApp.MVC.Services;
using WeatherApp.Infrastructure.Services;
using WeatherApp.Core.Interfaces;
using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IConfiguration>().GetSection("DefaultQueryValues").Get<DefaultQueryValues>());

builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
    {
        var weatherApiConfig = builder.Configuration.GetSection("WeatherApi");
        client.BaseAddress = new Uri(weatherApiConfig["BaseUrl"]);
        client.DefaultRequestHeaders.Add("key", weatherApiConfig["ApiKey"]);
    });

builder.Services.AddScoped<IWeatherEntryStorageService, WeatherEntryStorageService>();
builder.Services.AddScoped<IDataFetchService, DataFetchService>();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();

var app = builder.Build();

var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

var cities = app.Configuration.GetSection("DefaultQueryValues").Get<DefaultQueryValues>();

foreach (var city in cities.CityNames)
{
    var jobId = $"FetchData-{city.Replace(" ", "-")}";
    recurringJobManager.AddOrUpdate<IDataFetchService>(jobId,
        service => service.FetchAndSaveDataAsync(city),
        "*/1 * * * *"); // For Demo purpose, this is 1m, but for this weather Api,
                        // 10m would be better, since current data is updated ~ every 15m,
                        // even tho UI client fetches it every 1m
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
