using ItemManagerClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ItemManagerClient
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         var services = new ServiceCollection();
         ConfigureServices(services);
         var serviceProvider = services.BuildServiceProvider();

         // Initialiser les données
         var dataService = serviceProvider.GetService<IDataService>();
         dataService?.InitializeDataAsync().Wait();

         MainPage = new MainPage();
      }

      private void ConfigureServices(IServiceCollection services)
      {
         services.AddSingleton<IDataService, DataService>();
         services.AddScoped<IItemService, ItemService>();
         services.AddScoped<ICategoryService, CategoryService>();
      }
   }
}