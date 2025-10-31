using System;

namespace ItemManagerClient.Models
{
   /// <summary>
   /// Represents a category to organize items
   /// </summary>
   public class Category
   {
      /// <summary>
      /// Unique category identifier
      /// </summary>
      public int Id { get; set; }

      /// <summary>
      /// Category name (e.g., "Meeting", "Project")
      /// </summary>
      public string Name { get; set; } = string.Empty;

      /// <summary>
      /// Detailed category description
      /// </summary>
      public string Description { get; set; } = string.Empty;

      /// <summary>
      /// Icon associated with the category (e.g., "📅", "📊")
      /// </summary>
      public string Icon { get; set; } = string.Empty;

      /// <summary>
      /// Category creation date
      /// </summary>
      public DateTime CreatedAt { get; set; } = DateTime.Now;

      /// <summary>
      /// Last modification date
      /// </summary>
      public DateTime UpdatedAt { get; set; } = DateTime.Now;
   }
}