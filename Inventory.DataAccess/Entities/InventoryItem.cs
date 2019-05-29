using System;
using System.Collections.Generic;

namespace Inventory.DataAccess.Entities
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int DaysToSellBeforeExpire { get; set; }
        public List<InventoryItemStore> InventoryStored { get; set; }
        public bool EnabledForSale { get; set; }
    }
}
