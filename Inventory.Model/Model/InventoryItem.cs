using Inventory.Model.Exceptions;
using Inventory.Model.Monads;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Inventory.Model
{
    public class InventoryItem
    {
        public InventoryItem(int itemId, string name, DateTime expirationDate, int daysToSellBeforeExpire, bool enabledForSale)
        {
            ItemId = itemId;
            Name = name;
            ExpirationDate = expirationDate;
            DaysToSellBeforeExpire = daysToSellBeforeExpire;
            EnabledForSale = enabledForSale;
            //InventoryStored = inventoryStored;
        }

        #region InventoryItem state
        public int ItemId { get; }
        public string Name { get; }
        public DateTime ExpirationDate { get; }
        public int DaysToSellBeforeExpire { get; }
        public List<InventoryItemStore> InventoryStored { get; }
        public bool EnabledForSale { get; }
        #endregion

        #region InventoryItem behavior

        // Non pure functions
        public bool IsInStock(Store store)
        {
            return IsInventoryItemInStock(InventoryStored, store);
        }

        public bool IsNextToExpire(DateTime minExpirationDate)
        {
            return IsInventoryItemNextToExpire(ExpirationDate, minExpirationDate);
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
        #endregion

        private bool IsInventoryItemInStock(IEnumerable<InventoryItemStore> InventoryStored, Store store)
        {
            return InventoryStored
                        .Where(r => r.Store == store)
                        .Where(r => r.ExistsStock)
                        .Any();
        }

        #region Pure functions
        // Pure function
        public static Func<DateTime, DateTime, bool> IsInventoryItemNextToExpire { get; } = 
                        (DateTime inventoryItemExpirationDate, DateTime minExpirationDate) =>
                            DateTime.Compare(inventoryItemExpirationDate, minExpirationDate) >= 0;

        #endregion

        #region Verifications

        public Either<Exception, InventoryItem> VerifyExpirationDate()
        {
            if (DateTime.Compare(ExpirationDate, DateTime.Now.AddDays(60)) <= 0)
                return new ExpirationDateException("El item esta próximo a vencer");

            return this;
        }

        public Either<Exception, InventoryItem> VerifyStatus()
        {
            if (!this.EnabledForSale)
                return new StatusException("El item no esta habilitado para la venta");

            return this;
        }

        public Either<Exception, InventoryItem> VerifyStore(Store store)
        {
            if (this.InventoryStored != null && this.InventoryStored.Any(r => r.Store == store))
                return new StoreException("La tienda no corresponde al item");

            return this;
        }

        public Either<Exception, InventoryItem> CheckIn(int count)
        {
            return this;
        }
        
        public bool VerifyExpiration()
        {
            if (DateTime.Compare(ExpirationDate, DateTime.Now.AddDays(60)) <= 0)
                return false;

            return true;
        }

        public bool VerifyStats()
        {
            if (!this.EnabledForSale)
                return false;

            return true;
        }

        public bool VerifyStores(Store store)
        {
            if (this.InventoryStored.Any(r => r.Store == store))
                return false;

            return true;
        }

        public bool CheckItIn()
        {
            return true;
        }

        #endregion
    }
}
