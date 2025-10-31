using ItemManagerClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Main data service interface for all data operations
   /// </summary>
   public interface IDataService
   {
      // Item operations
      Task<List<Item>> GetAllItemsAsync();
      Task<Item> GetItemByIdAsync(int id);
      Task AddItemAsync(Item item);
      Task UpdateItemAsync(Item item);
      Task DeleteItemAsync(int id);

      // Category operations
      Task<List<Category>> GetAllCategoriesAsync();
      Task<Category> GetCategoryByIdAsync(int id);
      Task AddCategoryAsync(Category category);
      Task UpdateCategoryAsync(Category category);
      Task DeleteCategoryAsync(int id);

      // Initialization
      Task InitializeDataAsync();
   }
}