using Inventory.Infrastructure.Repositories;
using Inventory.Model;
using Inventory.Model.Extensions;
using Inventory.Model.Filters;
using Inventory.Model.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Inventory.Service.Controllers
{
    [Route("api/[controller]/{storeId}")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IInventoryItemRepository _InventoryItemRepository;
        private readonly IStoreRepository _StoreRepository;
        public StoreController(IInventoryItemRepository inventoryItemRepository, IStoreRepository storeRepository)
        {
            _InventoryItemRepository = inventoryItemRepository;
            _StoreRepository = storeRepository;
        }

        // GET api/values
        [HttpGet("[action]/{daysToSellBeforeExpire}/{recordsCount}")]
        public ActionResult<IEnumerable<InventoryItem>> GetForDiscount(int storeId, int daysToSellBeforeExpire, int recordsCount)
        {
            DiscountFilter filter = new DiscountFilter(_StoreRepository.GetById(storeId), daysToSellBeforeExpire, recordsCount);

            return Ok(
                    _StoreRepository
                    .GetItems(storeId)
                    .GetItemsForDiscount(filter)
                );
        }
    }
}
