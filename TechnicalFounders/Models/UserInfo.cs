using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TechnicalFounders.Models
{
    public class UserInfo : DbContext
    {
        private string _dbPath;

        public UserInfo(string dbPath) : base()
        {
            _dbPath = dbPath;

            CheckTable();
        }

        private void CheckTable()
        {
            try
            {
                int rows = base.Database.ExecuteSqlCommand(@"SELECT * FROM Users");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();
                databaseCreator.CreateTables();
            }
            finally
            {
                // Create database if it's not there. This will also ensure the data seeding will happen.
                Database.EnsureCreated();
            }
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        // Add model attributes on the fly (Models classes are cleaner).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make ID property the primary key.
            modelBuilder.Entity<User>()
                        .HasKey(p => p.Id);

            // Require email to be set.
            modelBuilder.Entity<User>()
                        .Property(p => p.EmailAddress)
                        .IsRequired();

            // Require password to be set.
            modelBuilder.Entity<User>()
                        .Property(p => p.Password)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(p => p.UserName);
        }

        // Store and retrieve user login information methods

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                await Users.AddAsync(user).ConfigureAwait(false);
                await SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding User to local database: {ex.Message}");
                return false;
            }
        }

        public async Task<User> GetUserAsync()
        {
            try
            {
                var user = await Users.FirstAsync().ConfigureAwait(false);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync()
        {
            try
            {
                var itemToRemove = Users.FirstOrDefault();
                if (itemToRemove != null)
                {
                    Users.Remove(itemToRemove);
                    await SaveChangesAsync().ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // UpdateUserAsync method for later

    }
}
