using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mitra_Journal.Data;

namespace Mitra_Journal
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();

            // Run database initialization asynchronously to avoid UI blocking
            Task.Run(() =>
            {
                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<LocalDbContext>();

                // Ensure DB exists
                db.Database.EnsureCreated();

                var conn = db.Database.GetDbConnection();
                conn.Open();

                // Set WAL mode and busy timeout to reduce locking issues
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA journal_mode=WAL;";
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA busy_timeout = 5000;";
                    cmd.ExecuteNonQuery();
                }

                // Check if MoodCategory exists
                bool columnExists = false;
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PRAGMA table_info(Moods);";
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == "MoodCategory")
                        {
                            columnExists = true;
                            break;
                        }
                    }
                }

                // Add column if missing
                if (!columnExists)
                {
                    using var alterCmd = conn.CreateCommand();
                    alterCmd.CommandText = "ALTER TABLE Moods ADD COLUMN MoodCategory TEXT;";
                    alterCmd.ExecuteNonQuery();
                }

                conn.Close();
            });

            MainPage = new MainPage();
        }
    }
}