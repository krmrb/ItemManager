using ItemManagerClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ItemManagerClient
{
   public partial class App : Application
   {
      public static IServiceProvider ServiceProvider { get; private set; }

      public App()
      {
         InitializeComponent();

         var services = new ServiceCollection();
         ConfigureServices(services);
         ServiceProvider = services.BuildServiceProvider();

         MainPage = new MainPage();
      }

      private void ConfigureServices(IServiceCollection services)
      {
         // Register services for dependency injection
         services.AddSingleton<IDataService, DataService>();
         services.AddScoped<IItemService, ItemService>();
         services.AddScoped<ICategoryService, CategoryService>();

         // Initialize mock data
         var dataService = new DataService();
         dataService.InitializeDataAsync().Wait();
         services.AddSingleton<IDataService>(dataService);
      }
   }
}