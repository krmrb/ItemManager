using ItemManagerClient.Enums;
using ItemManagerClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Item-specific service interface
   /// </summary>
   public interface IItemService
   {
      Task<List<Item>> GetItemsAsync();
      Task<Item> GetItemAsync(int id);
      Task CreateItemAsync(Item item);
      Task UpdateItemAsync(Item item);
      Task DeleteItemAsync(int id);
      Task<List<Item>> GetItemsByStatusAsync(ItemStatus status);
      Task<List<Item>> GetItemsByCategoryAsync(int categoryId);
      Task<List<Item>> SearchItemsAsync(string searchTerm);
   }
}