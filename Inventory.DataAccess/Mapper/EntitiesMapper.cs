//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Inventory.DataAccess.Mapper
//{
//    public class EntitiesMapper
//    {
//        public static Model.Store MapStore(Entities.Store store)
//        {
//            if (store == null)
//                return null;

//            var inventoryItemList = new List<Model.InventoryItemStore>();
//            var mappedStore = new Model.Store(store.StoreId, store.StoreName, inventoryItemList);

//            inventoryItemList.AddRange(store.InventoryStored.Select(r => MapItemStore(r, mappedStore)));

//            return mappedStore;
//        }

//        public static Model.InventoryItem MapInventoryitem(Entities.InventoryItem item)
//        {
//            if (item == null)
//                return null;

//            var inventoryItemList = new List<Model.InventoryItemStore>();
//            var mappedItem = new Model.InventoryItem(item.ItemId, item.Name, item.ExpirationDate, item.DaysToSellBeforeExpire, item.EnabledForSale, inventoryItemList);
            
//            //inventoryItemList.AddRange(item.InventoryStored.Select(r => MapItemStore(r, mappedItem)));

//            return mappedItem;
//        }

//        public static Model.InventoryItemStore MapItemStore(Entities.InventoryItemStore itemStore)
//        {
//            return MapItemStore(itemStore, MapStore(itemStore.Store), MapInventoryitem(itemStore.InventoryItem));
//        }

//        public static Model.InventoryItemStore MapItemStore(Entities.InventoryItemStore itemStore, Model.Store store)
//        {
//            return MapItemStore(itemStore, store, MapInventoryitem(itemStore.InventoryItem));
//        }

//        public static Model.InventoryItemStore MapItemStore(Entities.InventoryItemStore itemStore, Model.InventoryItem item)
//        {
//            return MapItemStore(itemStore, MapStore(itemStore.Store), item);
//        }
        
//        public static Model.InventoryItemStore MapItemStore(Entities.InventoryItemStore itemStore, Model.Store store, Model.InventoryItem item)
//        {
//            return new Model.InventoryItemStore(itemStore.InventoryItemStoreId, itemStore.StoreId, store, itemStore.ItemId, item, itemStore.StockCount);
//        }
//    }
//}
