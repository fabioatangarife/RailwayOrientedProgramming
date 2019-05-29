using Inventory.DataAccess.Entities;
using Inventory.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.DataAccess.Repositories
{
    public class InventoryItemRepository 
        : GeneralRepository<InventoryItem, Model.InventoryItem>
        , IInventoryItemRepository
    {
        public override Model.InventoryItem GetById(int itemId)
        {
            return MockItemsList().FirstOrDefault(r => r.ItemId == itemId);
        }

        private IEnumerable<Model.InventoryItem> MockItemsList()
        {
            var list = new List<Model.InventoryItem>();

            list.Add(new Model.InventoryItem(1, "Chocorramo", DateTime.Now.AddDays(50), 5, true));
            list.Add(new Model.InventoryItem(2, "Chocorramo", DateTime.Now.AddDays(50), 5, false));
            list.Add(new Model.InventoryItem(3, "Chocorramo", DateTime.Now.AddDays(70), 5, true));

            return list;
        }
    }
}
 