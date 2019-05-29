namespace Inventory.Model
{
    public class InventoryItemStore
    {
        public Store Store { get; }

        public InventoryItem InventoryItem { get; }

        public int StockCount { get; }

        public bool ExistsStock
        {
            get
            {
                return StockCount > 0;
            }
        }

        public bool YellowAlertStock
        {
            get
            {
                return StockCount > 5;
            }
        }

        public bool RedAlertStock
        {
            get
            {
                return StockCount > 1;
            }
        }
    }
}
