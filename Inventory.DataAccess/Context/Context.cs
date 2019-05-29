using Inventory.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataAccess.Context
{
    public partial class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-9RD00UR;Initial Catalog=Inventory;Integrated Security=True;");
        }

        public virtual DbSet<InventoryItem> InventoryItems { get; set; }

        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region InventoryItem
            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false);

            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.ExpirationDate)
                .IsRequired();

            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.DaysToSellBeforeExpire);
            
            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.EnabledForSale)
                .IsRequired();

            modelBuilder.Entity<InventoryItem>()
                .HasKey(e => e.ItemId);
            #endregion

            #region Store
            modelBuilder.Entity<Store>()
                .Property(e => e.StoreId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<Store>()
                .Property(e => e.StoreName)
                .IsRequired()
                .IsUnicode(false);
            
            modelBuilder.Entity<Store>()
                .HasKey(e => e.StoreId);
            #endregion

            #region InventoryItemStore
            modelBuilder.Entity<InventoryItemStore>()
                .ToTable("InventoryItemStores");

            modelBuilder.Entity<InventoryItemStore>()
                .Property(e => e.InventoryItemStoreId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<InventoryItemStore>()
                .Property(e => e.StoreId)
                .IsRequired();

            modelBuilder.Entity<InventoryItemStore>()
                .Property(e => e.ItemId)
                .IsRequired();
            
            modelBuilder.Entity<InventoryItemStore>()
                .HasKey(e => e.InventoryItemStoreId);

            modelBuilder.Entity<InventoryItemStore>()
                .Property(e => e.StockCount)
                .IsRequired();
            #endregion

            #region Relationships
            modelBuilder.Entity<InventoryItem>()
                .HasMany(e => e.InventoryStored)
                .WithOne(b => b.InventoryItem)
                .HasForeignKey(f => f.ItemId);

            modelBuilder.Entity<Store>()
                .HasMany(e => e.InventoryStored)
                .WithOne(b => b.Store)
                .HasForeignKey(f => f.StoreId);
            
            modelBuilder.Entity<InventoryItemStore>()
                .HasOne(e => e.InventoryItem)
                .WithMany(b => b.InventoryStored)
                .HasForeignKey(f => f.ItemId);

            modelBuilder.Entity<InventoryItemStore>()
                .HasOne(e => e.Store)
                .WithMany(b => b.InventoryStored)
                .HasForeignKey(f => f.StoreId);
            #endregion

        }
    }
}
