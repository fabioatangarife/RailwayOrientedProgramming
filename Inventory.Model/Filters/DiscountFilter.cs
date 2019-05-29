using System;

namespace Inventory.Model.Filters
{
    public class DiscountFilter
    {
        public DiscountFilter(Store store, int defaultDaysToSellBeforeExpire, int recordsCount)
        {
            Store = store;
            DefaultDaysToSellBeforeExpire = defaultDaysToSellBeforeExpire;
            RecordsCount = recordsCount;
        }

        public Store Store { get; }

        public int DefaultDaysToSellBeforeExpire { get; }

        public int RecordsCount { get; }

        public DateTime GetMinExpirationDate(int daysToSellBeforeExpiration)
        {
            var currDate = DateTime.Now;

            var daysBeforeExpire = GetDaysToSellBeforeExpire(DefaultDaysToSellBeforeExpire, daysToSellBeforeExpiration);

            return CalculateMinExpirationDate(DateTime.Now, daysBeforeExpire);
        }

        // Pure function
        private readonly Func<int, int, int> GetDaysToSellBeforeExpire = 
                                                (int defaultDays, int definedItemDays) 
                                                    => definedItemDays > 0 ? definedItemDays : defaultDays;

        // Pure function
        private readonly Func<DateTime, int, DateTime> CalculateMinExpirationDate = 
                                                (DateTime startDate, int daysToExpire)
                                                    => startDate.AddDays(daysToExpire);
    }
}
