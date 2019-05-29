using Inventory.Model;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Extensions
{
    public static class InventoryItemStoreExtensions
    {
        public static IEnumerable<InventoryItemStore> GetByStore
            (this IEnumerable<InventoryItemStore> itemStores, Store store) =>
                itemStores.Where(r => r.Store == store);

        public static IEnumerable<InventoryItemStore> ExistsStock
            (this IEnumerable<InventoryItemStore> itemStores) =>
                itemStores.Where(r => r.ExistsStock);
    }
}
