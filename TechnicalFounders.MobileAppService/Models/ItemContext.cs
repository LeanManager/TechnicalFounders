using System;
using Microsoft.EntityFrameworkCore;
using TechnicalFounders.Models;

namespace TechnicalFounders.MobileAppService.Models
{
    public class ItemContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }

        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Text).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(p => p.Category)
                        .HasConversion(
                            // Going to DB
                            v => v.ToString(),
                            // Coming from DB
                            v => (ItemCategory)Enum.Parse(typeof(ItemCategory), v));
            });
        }
    }
}
