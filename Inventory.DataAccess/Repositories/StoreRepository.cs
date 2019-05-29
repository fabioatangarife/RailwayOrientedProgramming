using Inventory.DataAccess.Entities;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.DataAccess.Repositories
{
    public class StoreRepository 
        : GeneralRepository<Store, Model.Store>
        , IStoreRepository
    {
        public IEnumerable<Model.InventoryItemStore> GetItems(int storeId)
        {
            var store = DbSet.Include(r => r.InventoryStored).ThenInclude<Store, InventoryItemStore, InventoryItem>(r => r.InventoryItem).SingleOrDefault(r => r.StoreId == storeId);

            var modelStore = AutoMapper.Mapper.Map<Model.Store>(store);
            
            return modelStore?.InventoryStored;
        }
    }
}
