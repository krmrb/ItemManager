using ItemManagerClient.Enums;
using ItemManagerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Item service implementation
   /// </summary>
   public class ItemService : IItemService
   {
      private readonly IDataService _dataService;

      public ItemService(IDataService dataService)
      {
         _dataService = dataService;
      }

      public async Task<List<Item>> GetItemsAsync()
      {
         var items = await _dataService.GetAllItemsAsync();

         // Populate category information for each item
         var categories = await _dataService.GetAllCategoriesAsync();
         foreach (var item in items)
         {
            item.Category = categories.FirstOrDefault(c => c.Id == item.CategoryId);
         }

         return items;
      }

      public async Task<Item> GetItemAsync(int id)
      {
         var item = await _dataService.GetItemByIdAsync(id);
         if (item != null)
         {
            var categories = await _dataService.GetAllCategoriesAsync();
            item.Category = categories.FirstOrDefault(c => c.Id == item.CategoryId);
         }
         return item;
      }

      public async Task CreateItemAsync(Item item)
      {
         await _dataService.AddItemAsync(item);
      }

      public async Task UpdateItemAsync(Item item)
      {
         await _dataService.UpdateItemAsync(item);
      }

      public async Task DeleteItemAsync(int id)
      {
         await _dataService.DeleteItemAsync(id);
      }

      public async Task<List<Item>> GetItemsByStatusAsync(ItemStatus status)
      {
         var items = await GetItemsAsync();
         return items.Where(i => i.Status == status).ToList();
      }

      public async Task<List<Item>> GetItemsByCategoryAsync(int categoryId)
      {
         var items = await GetItemsAsync();
         return items.Where(i => i.CategoryId == categoryId).ToList();
      }

      public async Task<List<Item>> SearchItemsAsync(string searchTerm)
      {
         if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetItemsAsync();

         var items = await GetItemsAsync();
         return items.Where(i =>
             i.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
             i.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
         ).ToList();
      }
   }
}