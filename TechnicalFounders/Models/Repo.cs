﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalFounders.Services;
using Xamarin.Forms;

namespace TechnicalFounders.Models
{
    public class Repo : DbContext, IDataStore<Item>
    {
        private string _dbPath;

        /// <summary>
        /// Creates our repo.
        /// </summary>
        /// <param name="dbPath">The platform-specific path to the database.</param>
        public Repo(string dbPath) : base()
        {
            _dbPath = dbPath;

            // Create database if it's not there. This will also ensure the data seeding will happen.
            Database.EnsureCreated();
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        // Add model attributes on the fly (Models classes are cleaner).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make ID property the primary key.
            modelBuilder.Entity<Item>()
                        .HasKey(p => p.Id);

            // Require text to be set.
            modelBuilder.Entity<Item>()
                        .Property(p => p.Text)
                        .IsRequired();

            // Add a value converter to store category enum as string instead of int.
            modelBuilder.Entity<Item>()
                        .Property(p => p.Category)
                        .HasConversion(
                            // Going to DB
                            v => v.ToString(),
                            // Coming from DB
                            v => (ItemCategory)Enum.Parse(typeof(ItemCategory), v));

            // Built-in converters in Entity Framework Core - refer to documentation.
            //modelBuilder.Entity<Item>()
            //.Property(p => p.Category)
            //.HasConversion(new EnumToStringConverter<ItemCategory>());

            // Add some initial data.
#if DEBUG
            modelBuilder.Entity<Item>()
                        .HasData(
                            new Item { Id = Guid.NewGuid().ToString(), Text = "Private item", Description = "This is a private item description.", Category = ItemCategory.Private },
                            new Item { Id = Guid.NewGuid().ToString(), Text = "Shopping item", Description = "This is a shopping item description.", Category = ItemCategory.Shopping },
                            new Item { Id = Guid.NewGuid().ToString(), Text = "Work item", Description = "This is a work item description.", Category = ItemCategory.Work }
                           );
#endif
        }

        #region IDataStore<Item>

        public async Task<Item> GetItemAsync(string id)
        {
            var item = await Items.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            // Careful if the list of items is too large because it will end up in memory.
            var allItems = await Items.ToListAsync().ConfigureAwait(false);

            return allItems;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            await Items.AddAsync(item).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);

            // Error handling is still required.
            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            Items.Update(item);
            await SaveChangesAsync().ConfigureAwait(false);

            // Error handling is still required.
            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var itemToRemove = Items.FirstOrDefault(x => x.Id == id);
            if (itemToRemove != null)
            {
                Items.Remove(itemToRemove);
                await SaveChangesAsync().ConfigureAwait(false);
            }

            // Error handling is still required.
            return true;
        }
        #endregion
    }
}
