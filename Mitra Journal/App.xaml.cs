using Microsoft.Extensions.DependencyInjection;
using Mitra_Journal.Data;


namespace Mitra_Journal

{

    public partial class App : Application

    {

        public App(IServiceProvider services)

        {

            InitializeComponent();
 
            using var scope = services.CreateScope();

            var dbConfig = scope.ServiceProvider.GetRequiredService<LocalDbContext>();

            dbConfig.Database.EnsureCreated();
 
            MainPage = new MainPage();

        }

    }

}