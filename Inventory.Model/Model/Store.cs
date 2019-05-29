using System.Collections.Generic;
using System.Linq;

namespace Inventory.Model
{
    public class Store
    {
        public Store(int storeId, string storeName, List<InventoryItemStore> inventoryStored)
        {
            StoreId = storeId;
            StoreName = storeName;
            InventoryStored = inventoryStored;
        }
        public int StoreId { get; }

        public string StoreName { get; }

        public List<InventoryItemStore> InventoryStored { get; }

        public bool IsInStock(InventoryItem item)
        {
            return InventoryStored
                .Where(r => r.InventoryItem == item)
                .Where(r => r.ExistsStock)
                .Any();
        }

        public override int GetHashCode()
        {
            return StoreId.GetHashCode();
        }
    }
}
