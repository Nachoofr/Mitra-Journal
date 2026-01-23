using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mitra_Journal;
using Mitra_Journal.Data;
using Mitra_Journal.Entities;
using Mitra_Journal.Services;
using Mitra_Journal.Services.Interface;
using MudBlazor.Services;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
 
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
        
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<ThemeService>();
        builder.Services.AddMudServices();
        
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
        builder.Services.AddScoped<IJournalService, JournalService>();
        builder.Services.AddScoped<IMoodService, MoodService>();
        builder.Services.AddScoped<ITagsService, TagsService>();
#endif
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Mitra.db3");
        Console.WriteLine("DB path: " + dbPath);
        builder.Services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlite($"Filename={dbPath}")
        );
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Console.WriteLine($"Unhandled exception: {e.ExceptionObject}");
        };

        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            Console.WriteLine($"Unobserved task exception: {e.Exception}");
        };

        return builder.Build();
    }
}