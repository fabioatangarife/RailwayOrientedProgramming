using Inventory.Model;
using System.Collections.Generic;

namespace Inventory.Infrastructure.Repositories
{
    public interface IStoreRepository : IGeneralRepository<Store>
    {
        IEnumerable<InventoryItemStore> GetItems(int storeId);
    }
}
