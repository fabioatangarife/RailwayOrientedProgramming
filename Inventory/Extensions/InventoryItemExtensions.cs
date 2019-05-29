using Inventory.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventory.Monads;

namespace Inventory.Extensions
{
    public static class InventoryItemExtensions
    {
        public static IEnumerable<InventoryItem> GetEnabledsForSale(this IEnumerable<InventoryItem> items)
        {
            return items.Where(r => r.EnabledForSale);
        }

        public static IEnumerable<InventoryItem> GetInStock(this IEnumerable<InventoryItem> items, Store store)
        {
            return items.Where(r => r.IsInStock(store));
        }

        public static IEnumerable<InventoryItem> GetNextToExpire(this IEnumerable<InventoryItem> items, DateTime minExpirationDate)
        {
            return items.Where(r => r.IsNextToExpire(minExpirationDate));
        }
        
        // High order function -> Receive a function as parameter or is composed of other functions
        public static IEnumerable<InventoryItem> GetNextToExpirePure
            (this IEnumerable<InventoryItem> items, DateTime minExpirationDate, Func<DateTime, DateTime, bool> IsBeforeExpiration)
        {
            // Functor -> Who executes a function!
            return items.Where(r => IsBeforeExpiration(r.ExpirationDate, minExpirationDate));
        }

        public static Either<Exception, InventoryItem> Map(this InventoryItem item, Func<InventoryItem, Either<Exception, InventoryItem>> funcMap)
        {
            return funcMap(item);
        }
    }
}
