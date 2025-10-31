using ItemManagerClient.Enums;
using System;

namespace ItemManagerClient.Models
{
   /// <summary>
   /// Represents an item in the application
   /// NOTE: We don't use the word "task" as requested
   /// </summary>
   public class Item
   {
      /// <summary>
      /// Unique item identifier
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// Item title
      /// </summary>
      public string Title { get; set; } = string.Empty;

      /// <summary>
      /// Detailed item description
      /// </summary>
      public string Description { get; set; } = string.Empty;

      /// <summary>
      /// Scheduled date for the item
      /// </summary>
      public DateTime Date { get; set; } = DateTime.Now;

      /// <summary>
      /// Start time (nullable if "all day")
      /// </summary>
      public TimeSpan? StartTime { get; set; }

      /// <summary>
      /// End time (nullable if "all day")
      /// </summary>
      public TimeSpan? EndTime { get; set; }

      /// <summary>
      /// Indicates if the item lasts all day
      /// </summary>
      public bool IsAllDay { get; set; }

      /// <summary>
      /// Current item status
      /// </summary>
      public ItemStatus Status { get; set; } = ItemStatus.Draft;

      /// <summary>
      /// Item category
      /// </summary>
      public Category? Category { get; set; }

      /// <summary>
      /// Category identifier (for relationship)
      /// </summary>
      public int CategoryId { get; set; }

      /// <summary>
      /// Item creation date
      /// </summary>
      public DateTime CreatedAt { get; set; } = DateTime.Now;

      /// <summary>
      /// Last modification date
      /// </summary>
      public DateTime UpdatedAt { get; set; } = DateTime.Now;
   }
}