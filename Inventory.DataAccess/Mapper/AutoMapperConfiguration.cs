using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.DataAccess.MapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        static AutoMapperConfiguration() {

            AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Entities.InventoryItemStore, Model.InventoryItemStore>()
                            //.ForCtorParam("inventoryItemStoreId", opt => opt.MapFrom(src => src.InventoryItemStoreId))
                            ////.ForCtorParam("store", opt => opt.MapFrom(src => src.Store))
                            //.ForCtorParam("inventoryItem", opt => opt.MapFrom(src => src.InventoryItem))
                            //.ForCtorParam("stockCount", opt => opt.MapFrom(src => src.StockCount))
                            ;
                    cfg.CreateMap<Model.InventoryItemStore, Entities.InventoryItemStore>();

                    cfg.CreateMap<Entities.InventoryItem, Model.InventoryItem>();
                    cfg.CreateMap<Model.InventoryItem, Entities.InventoryItem>();

                    cfg.CreateMap<Entities.Store, Model.Store>();
                    cfg.CreateMap<Model.Store, Entities.Store>();
                    
                    //cfg.ConstructServicesUsing(t => _kernel.Get(t));

                });

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        public static void Configure() { }
    }
}
