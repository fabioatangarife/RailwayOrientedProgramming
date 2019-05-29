using Inventory.Filters;
using Inventory.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventory.Extensions;
using Inventory.Results;
using Inventory.Exceptions;
using Inventory.Monads;

namespace Inventory.Services
{
    public class InventoryService
    {
        public IEnumerable<InventoryItem> GetItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryItem> GetItemsForDiscount(DiscountFilter filter)
        {
            IEnumerable<InventoryItem> inventoryList = GetItems();

            IEnumerable<InventoryItem> discountItems = 
                // Function chain
                inventoryList
                    .GetEnabledsForSale()
                    .GetInStock(filter.Store)
                    .GetNextToExpire(filter.MinExpirationDate)
                    .GetNextToExpirePure(filter.MinExpirationDate, InventoryItem.IsInventoryItemNextToExpire);

                //// Extensions don't define behavior in a OOP
                //// Extensions define behavior in a Functional programming but C# is OOP
                //inventoryList
                //    .Where(r => r.EnabledForSale)
                //    .Where(r => r.IsInStock(filter.Store))
                //    .Where(r => r.IsNextToExpire(filter.MinExpirationDate));

            return discountItems;
        }

        public CheckInResult CheckInInventoryItem(InventoryItem item, Store store)
        {
            var result = item
                .Map(VerifyExpirationDate)
                .Map(VerifyStatus)
                .Map(VerifyStore(store))
                .Map(CheckIn)
                .Map(CreateSuccessResult)
                .Reduce(CreateExpirationDateErrorResult, r => r is ExpirationDateException)
                .Reduce(CreateStatusErrorResult, r => r is StatusException)
                .Reduce(CreateStoreErrorResult, r => r is StoreException)
                .Reduce(CreateErrorResult);

            return result;
        }

        #region Arrow functions
        // Arrow Functions
        private Func<InventoryItem, Either<Exception, InventoryItem>> VerifyStore(Store store) { return r => r.VerifyStore(store); }
        private readonly Func<InventoryItem, Either<Exception, InventoryItem>> VerifyExpirationDate = r => r.VerifyExpirationDate();
        private readonly Func<InventoryItem, Either<Exception, InventoryItem>> VerifyStatus = r => r.VerifyStatus();
        private readonly Func<InventoryItem, Either<Exception, InventoryItem>> CheckIn = r => r.CheckIn();
        private readonly Func<InventoryItem, CheckInResult> CreateSuccessResult = r => new CheckInResult() { Success = true, Item = r };
        private readonly Func<Exception, CheckInResult> CreateErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private readonly Func<Exception, CheckInResult> CreateExpirationDateErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private readonly Func<Exception, CheckInResult> CreateStatusErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private readonly Func<Exception, CheckInResult> CreateStoreErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        #endregion

        public CheckInResult CheckInInventoryItem2(InventoryItem item, Store store)
        {
            try
            {
                if (!item.VerifyExpiration())
                    return CreateErrorResult(new ExpirationDateException());
                
                if (!item.VerifyStats())
                    return CreateErrorResult(new StatusException());

                if (item.VerifyStores(store))
                    return CreateErrorResult(new StoreException());

                item.CheckItIn();
                var sb = new StringBuilder();
            }
            catch (Exception ex)
            {
                return CreateErrorResult(ex);
            }

            return new CheckInResult() { Success = true, Item = item };
        }
    }
}
