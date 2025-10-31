using ItemManagerClient.Enums;
using ItemManagerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerClient.Services
{
   /// <summary>
   /// Implementation of data service using in-memory data (for learning/POC)
   /// </summary>
   public class DataService : IDataService
   {
      private List<Item> _items;
      private List<Category> _categories;
      private int _nextItemId = 1;
      private int _nextCategoryId = 1;

      public DataService()
      {
         _items = new List<Item>();
         _categories = new List<Category>();
      }

      // Item operations
      public Task<List<Item>> GetAllItemsAsync()
      {
         return Task.FromResult(_items.OrderByDescending(i => i.UpdatedAt).ToList());
      }

      public Task<Item> GetItemByIdAsync(int id)
      {
         var item = _items.FirstOrDefault(i => i.Id == id);
         return Task.FromResult(item);
      }

      public Task AddItemAsync(Item item)
      {
         item.Id = _nextItemId++;
         item.CreatedAt = DateTime.Now;
         item.UpdatedAt = DateTime.Now;
         _items.Add(item);
         return Task.CompletedTask;
      }

      public Task UpdateItemAsync(Item item)
      {
         var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
         if (existingItem != null)
         {
            existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.Date = item.Date;
            existingItem.StartTime = item.StartTime;
            existingItem.EndTime = item.EndTime;
            existingItem.IsAllDay = item.IsAllDay;
            existingItem.Status = item.Status;
            existingItem.CategoryId = item.CategoryId;
            existingItem.Category = item.Category;
            existingItem.UpdatedAt = DateTime.Now;
         }
         return Task.CompletedTask;
      }

      public Task DeleteItemAsync(int id)
      {
         var item = _items.FirstOrDefault(i => i.Id == id);
         if (item != null)
         {
            _items.Remove(item);
         }
         return Task.CompletedTask;
      }

      // Category operations
      public Task<List<Category>> GetAllCategoriesAsync()
      {
         return Task.FromResult(_categories.OrderBy(c => c.Name).ToList());
      }

      public Task<Category> GetCategoryByIdAsync(int id)
      {
         var category = _categories.FirstOrDefault(c => c.Id == id);
         return Task.FromResult(category);
      }

      public Task AddCategoryAsync(Category category)
      {
         category.Id = _nextCategoryId++;
         category.CreatedAt = DateTime.Now;
         category.UpdatedAt = DateTime.Now;
         _categories.Add(category);
         return Task.CompletedTask;
      }

      public Task UpdateCategoryAsync(Category category)
      {
         var existingCategory = _categories.FirstOrDefault(c => c.Id == category.Id);
         if (existingCategory != null)
         {
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.Icon = category.Icon;
            existingCategory.UpdatedAt = DateTime.Now;
         }
         return Task.CompletedTask;
      }

      public Task DeleteCategoryAsync(int id)
      {
         var category = _categories.FirstOrDefault(c => c.Id == id);
         if (category != null)
         {
            _categories.Remove(category);
         }
         return Task.CompletedTask;
      }

      public Task InitializeDataAsync()
      {
         // Initialize with default categories
         if (!_categories.Any())
         {
            var defaultCategories = new List<Category>
                {
                    new Category { Name = "Meeting", Description = "Team meetings and discussions", Icon = "📅" },
                    new Category { Name = "Project", Description = "Project-related work", Icon = "📊" },
                    new Category { Name = "Maintenance", Description = "System maintenance and updates", Icon = "🔧" },
                    new Category { Name = "Training", Description = "Learning and development", Icon = "🎓" },
                    new Category { Name = "Reporting", Description = "Reports and documentation", Icon = "📋" },
                    new Category { Name = "Other", Description = "Other activities", Icon = "❓" }
                };

            foreach (var category in defaultCategories)
            {
               AddCategoryAsync(category).Wait();
            }
         }

         // Initialize with sample items if empty
         if (!_items.Any())
         {
            var sampleItem = new Item
            {
               Title = "Weekly Team Meeting",
               Description = "Discuss project progress and next steps with the development team",
               Date = DateTime.Today.AddDays(1),
               StartTime = new TimeSpan(9, 0, 0),
               EndTime = new TimeSpan(10, 30, 0),
               IsAllDay = false,
               Status = ItemStatus.Open,
               CategoryId = 1 // Meeting category
            };
            AddItemAsync(sampleItem).Wait();
         }

         return Task.CompletedTask;
      }
   }
}