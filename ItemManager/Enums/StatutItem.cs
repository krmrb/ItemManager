namespace ItemManagerClient.Enums
{
   /// <summary>
   /// Represents the different possible statuses for an item
   /// </summary>
   public enum ItemStatus
   {
      /// <summary>
      /// Item being drafted, not finalized
      /// </summary>
      Draft = 0,

      /// <summary>
      /// Active and ongoing item
      /// </summary>
      Open = 1,

      /// <summary>
      /// Item pending action or validation
      /// </summary>
      Pending = 2,

      /// <summary>
      /// Cancelled item
      /// </summary>
      Canceled = 3,

      /// <summary>
      /// Completed item
      /// </summary>
      Closed = 4
   }
}