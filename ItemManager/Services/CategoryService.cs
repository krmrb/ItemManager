using ItemManagerClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Category service implementation
   /// </summary>
   public class CategoryService : ICategoryService
   {
      private readonly IDataService _dataService;

      public CategoryService(IDataService dataService)
      {
         _dataService = dataService;
      }

      public async Task<List<Category>> GetCategoriesAsync()
      {
         return await _dataService.GetAllCategoriesAsync();
      }

      public async Task<Category> GetCategoryAsync(int id)
      {
         return await _dataService.GetCategoryByIdAsync(id);
      }

      public async Task CreateCategoryAsync(Category category)
      {
         await _dataService.AddCategoryAsync(category);
      }

      public async Task UpdateCategoryAsync(Category category)
      {
         await _dataService.UpdateCategoryAsync(category);
      }

      public async Task DeleteCategoryAsync(int id)
      {
         await _dataService.DeleteCategoryAsync(id);
      }
   }
}