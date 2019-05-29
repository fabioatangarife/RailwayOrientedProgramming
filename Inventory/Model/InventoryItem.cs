using Inventory.Exceptions;
using Inventory.Extensions;
using Inventory.Monads;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Inventory.Model
{
    public class InventoryItem
    {
        #region InventoryItem state
        public int ItemId { get; }
        public DateTime ExpirationDate { get; }
        public IEnumerable<InventoryItemStore> InventoryStored { get; }
        public bool EnabledForSale
        {
            get
            {
                // TODO: Call Sales behavior
                throw new NotImplementedException();
            }
        }
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
                return new ExpirationDateException();

            return this;
        }

        public Either<Exception, InventoryItem> VerifyStatus()
        {
            if (!this.EnabledForSale)
                return new StatusException();

            return this;
        }

        public Either<Exception, InventoryItem> VerifyStore(Store store)
        {
            if (this.InventoryStored.Any(r => r.Store == store))
                return new StoreException();

            return this;
        }

        public Either<Exception, InventoryItem> CheckIn()
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
