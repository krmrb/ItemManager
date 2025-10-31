using ItemManagerClient.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ItemManagerClient
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         // Configuration des services
         var services = new ServiceCollection();
         services.AddSingleton<IDataService, DataService>();
         services.AddScoped<IItemService, ItemService>();
         services.AddScoped<ICategoryService, CategoryService>();

         var serviceProvider = services.BuildServiceProvider();

         // Initialisation asynchrone des données
         _ = InitializeDataAsync(serviceProvider);

         // Utiliser NavigationPage pour permettre la navigation
         MainPage = new NavigationPage(new MainPage());
      }

      private async Task InitializeDataAsync(IServiceProvider serviceProvider)
      {
         try
         {
            var dataService = serviceProvider.GetService<IDataService>();
            if (dataService != null)
            {
               await dataService.InitializeDataAsync();
               Console.WriteLine("✅ Données initialisées");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"❌ Erreur initialisation données: {ex.Message}");
         }
      }
   }
}