namespace Inventory.Model
{
    public class InventoryItemStore
    {
        public InventoryItemStore(int inventoryItemStoreId, int itemId, InventoryItem inventoryItem, int stockCount)
        {
            InventoryItemStoreId = inventoryItemStoreId;
            //Store = store;
            //StoreId = storeId;
            InventoryItem = inventoryItem;
            ItemId = itemId;
            StockCount = stockCount;
        }

        public int InventoryItemStoreId { get; }

        public int StoreId { get; }
        public Store Store { get; }

        public int ItemId { get; }
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
