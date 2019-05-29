using System.Collections.Generic;
using System.Linq;

namespace Inventory.Model
{
    public class Store
    {
        public int StoreId { get; }

        public string StoreName { get; }

        public IEnumerable<InventoryItemStore> Inventory { get; }

        public bool IsInStock(InventoryItem item)
        {
            return Inventory
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
