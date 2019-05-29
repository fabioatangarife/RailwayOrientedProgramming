using Inventory.Model.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Model.Extensions
{
    public static class InventoryItemStoreExtensions
    {
        public static IEnumerable<InventoryItemStore> GetItemsForDiscountOld(this IEnumerable<InventoryItemStore> inventoryList, Store store, int defaultDaysToSellBeforeExpire, int recordsCount)
        {
            List<InventoryItemStore> items = new List<InventoryItemStore>();
            DateTime minExpirationDate;
            foreach (InventoryItemStore item in inventoryList)
            {
                if (item.InventoryItem.EnabledForSale && item.InventoryItem.IsInStock(store))
                {
                    minExpirationDate = DateTime.Now;
                    if (item.InventoryItem.DaysToSellBeforeExpire > 0)
                        minExpirationDate = minExpirationDate.AddDays(item.InventoryItem.DaysToSellBeforeExpire);
                    else
                        minExpirationDate = minExpirationDate.AddDays(defaultDaysToSellBeforeExpire);

                    if (item.InventoryItem.IsNextToExpire(minExpirationDate))
                        items.Add(item);

                    if (items.Count >= recordsCount)
                        break;
                }
            }
            return items;
        }

        // Function chain
        public static IEnumerable<InventoryItem> GetItemsForDiscountWithoutExtensions(this IEnumerable<InventoryItemStore> inventoryList, DiscountFilter filter)
        {
            return inventoryList
                        .Where(r => r.InventoryItem.EnabledForSale)
                        .Where(r => r.InventoryItem.IsNextToExpire(filter.GetMinExpirationDate(r.InventoryItem.DaysToSellBeforeExpire)))
                        .Where(r => r.ExistsStock)
                        .Select(r => r.InventoryItem);
        }

        // Function chain
        //// Extensions don't define behavior in a OOP
        //// Extensions define behavior in a Functional programming but C# is OOP
        public static IEnumerable<InventoryItem> GetItemsForDiscount(this IEnumerable<InventoryItemStore> inventoryList, DiscountFilter filter)
        {
            return inventoryList
                        .GetEnabledsForSale()
                        .GetInStock()
                        .GetNextToExpire(filter.GetMinExpirationDate)
                        .Select(r => r.InventoryItem);
        }


        #region Collection extensions
        private static IEnumerable<InventoryItemStore> GetEnabledsForSale(this IEnumerable<InventoryItemStore> items)
        {
            return items.Where(r => r.InventoryItem.EnabledForSale);
        }

        private static IEnumerable<InventoryItemStore> GetInStock(this IEnumerable<InventoryItemStore> items)
        {
            return items.Where(r => r.ExistsStock);
        }

        // High order function -> Receive a function as parameter or is composed of other functions
        private static IEnumerable<InventoryItemStore> GetNextToExpire(this IEnumerable<InventoryItemStore> items, Func<int, DateTime> funcGetMinExpirationDate)
        {
            return items.Where(r => r.InventoryItem.IsNextToExpire(funcGetMinExpirationDate(r.InventoryItem.DaysToSellBeforeExpire)));
        }
        #endregion

    }
}
