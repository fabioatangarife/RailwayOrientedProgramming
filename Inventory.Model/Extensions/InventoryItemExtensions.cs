using Inventory.Model.Exceptions;
using Inventory.Model.Monads;
using Inventory.Model.Results;
using System;

namespace Inventory.Model.Extensions
{
    public static class InventoryItemExtensions
    {
        public static CheckInResult CheckInInventoryItemOld(Store store, InventoryItem item)
        {
            try
            {
                if (!item.VerifyExpiration())
                    return CreateErrorResult(new ExpirationDateException("El item esta próximo a vencer"));

                if (!item.VerifyStats())
                    return CreateErrorResult(new StatusException("El item no esta habilitado para la venta"));

                if (item.VerifyStores(store))
                    return CreateErrorResult(new StoreException("La tienda no corresponde al item"));

                item.CheckItIn();
            }
            catch (Exception ex)
            {
                return CreateErrorResult(ex);
            }

            return CreateSuccessResult(item);
        }

        public static CheckInResult CheckInInventoryItemWithoutExtensions(this InventoryItem item, Store store, int count)
        {
            return item
                    .Map(r => r.VerifyExpirationDate())
                    .Map(r => r.VerifyStatus())
                    .Map(r => r.VerifyStore(store))
                    .Map(r => r.CheckIn(count))
                    .Map(CreateSuccessResult)
                    .Reduce(CreateExpirationDateErrorResult, r => r is ExpirationDateException)
                    .Reduce(CreateStatusErrorResult, r => r is StatusException)
                    .Reduce(CreateStoreErrorResult, r => r is StoreException)
                    .Reduce(CreateErrorResult);
        }

        public static Either<Exception, InventoryItem> CheckInInventoryItem(this InventoryItem item, Store store, int count)
        {
            return item
                    .Map(VerifyStatus)
                    .Map(VerifyExpirationDate)
                    .Map(VerifyStore(store))
                    .Map(CheckIn(count));
        }

        public static CheckInResult CheckInInventoryItemHandlindDifferentExceptions(this InventoryItem item, Store store, int count)
        {
            return item
                    .Map(VerifyExpirationDate)
                    .Map(VerifyStatus)
                    .Map(VerifyStore(store))
                    .Map(CheckIn(count))
                    .Map(CreateSuccessResult)
                    .Reduce(CreateExpirationDateErrorResult, r => r is ExpirationDateException)
                    .Reduce(CreateStatusErrorResult, r => r is StatusException)
                    .Reduce(CreateStoreErrorResult, r => r is StoreException)
                    .Reduce(CreateErrorResult);
        }

        #region Collection extensions
        private static Either<Exception, InventoryItem> Map(this InventoryItem item, Func<InventoryItem, Either<Exception, InventoryItem>> funcMap)
        {
            return funcMap(item);
        }
        #endregion

        #region Arrow functions Extensions
        // Arrow Functions
        private static Func<InventoryItem, Either<Exception, InventoryItem>> VerifyStore(Store store) { return r => r.VerifyStore(store); }
        private static readonly Func<InventoryItem, Either<Exception, InventoryItem>> VerifyExpirationDate = r => r.VerifyExpirationDate();
        private static readonly Func<InventoryItem, Either<Exception, InventoryItem>> VerifyStatus = r => r.VerifyStatus();
        private static Func<InventoryItem, Either<Exception, InventoryItem>> CheckIn(int count) { return r => r.CheckIn(count); }
        private static readonly Func<InventoryItem, CheckInResult> CreateSuccessResult = r => new CheckInResult() { Success = true, Item = r };
        private static readonly Func<Exception, CheckInResult> CreateErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private static readonly Func<Exception, CheckInResult> CreateExpirationDateErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private static readonly Func<Exception, CheckInResult> CreateStatusErrorResult = ex => new CheckInResult() { Success = false, Error = ex };
        private static readonly Func<Exception, CheckInResult> CreateStoreErrorResult = ex => new CheckInResult() { Success = false, Error = ex };

        #endregion
    }
}
