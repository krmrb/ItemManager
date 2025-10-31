using ItemManagerClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Category-specific service interface
   /// </summary>
   public interface ICategoryService
   {
      Task<List<Category>> GetCategoriesAsync();
      Task<Category> GetCategoryAsync(int id);
      Task CreateCategoryAsync(Category category);
      Task UpdateCategoryAsync(Category category);
      Task DeleteCategoryAsync(int id);
   }
}