using Inventory.Model;
using System;

namespace Inventory.Filters
{
    public class DiscountFilter
    {
        public Store Store { get; set; }

        public int DaysToExpire { get; set; }

        public DateTime MinExpirationDate
        {
            get
            {
                return GetMinExpirationDate(DateTime.Now, DaysToExpire);
            }
        }

        // Pure function
        private readonly Func<DateTime, int, DateTime> GetMinExpirationDate = 
                                                (DateTime startDate, int daysToExpire)
                                                    => startDate.AddDays(daysToExpire);
    }
}
