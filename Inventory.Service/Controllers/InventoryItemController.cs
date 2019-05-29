using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Infrastructure.Repositories;
using Inventory.Model;
using Inventory.Model.Exceptions;
using Inventory.Model.Extensions;
using Inventory.Model.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Service.Controllers
{
    [Route("api/[controller]/{itemId}")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {

        private readonly IInventoryItemRepository _InventoryItemRepository;
        private readonly IStoreRepository _StoreRepository;
        public InventoryItemController(IInventoryItemRepository inventoryItemRepository, IStoreRepository storeRepository)
        {
            _InventoryItemRepository = inventoryItemRepository;
            _StoreRepository = storeRepository;
        }

        [HttpPost("[action]/{count}")]
        public ActionResult CheckIn(int itemId, int storeId, int count)
        {
            Store store = _StoreRepository.GetById(storeId);

            CheckInResult result = _InventoryItemRepository
                .GetById(itemId)
                .CheckInInventoryItemHandlindDifferentExceptions(store, count);

            return result.Success ? Ok(result.Item) : BadRequest(result.Error) as ObjectResult;
        }

        [HttpPost("[action]/{count}")]
        public ActionResult CheckIn2(int itemId, int storeId, int count)
        {
            Store store = _StoreRepository.GetById(storeId);

            return _InventoryItemRepository
                .GetById(itemId)
                .CheckInInventoryItem(store, count)
                .Map(r => Ok(r) as ActionResult)
                .Reduce(UnprocessableEntity, r => r is ExpirationDateException)
                .Reduce(BadRequest, r => r is StatusException)
                .Reduce(NotFound, r => r is StoreException)
                .Reduce(r => ValidationProblem());
        }
    }
}