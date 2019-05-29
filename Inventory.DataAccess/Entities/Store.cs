using System.Collections.Generic;

namespace Inventory.DataAccess.Entities
{
    public class Store
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public List<InventoryItemStore> InventoryStored { get; set; }
    }
}
