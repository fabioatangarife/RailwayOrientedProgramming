using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inventory.DataAccess.Entities
{
    public class InventoryItemStore
    {
        public int InventoryItemStoreId { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }
        
        public int ItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }

        public int StockCount { get; set; }
    }
}
