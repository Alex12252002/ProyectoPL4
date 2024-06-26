using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using GestionApp.Models;
using Xamarin.Essentials;
using System.IO;
using System.Linq;

namespace GestionApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "GestionApp.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<Provider>().Wait();
            _database.CreateTableAsync<ProductProvider>().Wait();
        }

        public SQLiteAsyncConnection GetDatabaseConnection()
        {
            return _database;
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public async Task<User> GetUserAsync(string email, string password)
        {
            var users = await _database.Table<User>().ToListAsync();
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        public async Task<bool> AdminExistsAsync()
        {
            var users = await _database.Table<User>().ToListAsync();
            return users.Any(u => u.Role == "Admin");
        }

        public Task<int> DeleteAllUsersAsync()
        {
            return _database.ExecuteAsync("DELETE FROM User");
        }


        // CRUD para Producto
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.Id != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }

        // CRUD para Proveedor
        public Task<int> SaveProviderAsync(Provider provider)
        {
            if (provider.Id != 0)
            {
                return _database.UpdateAsync(provider);
            }
            else
            {
                return _database.InsertAsync(provider);
            }
        }

        public Task<List<Provider>> GetProvidersAsync()
        {
            return _database.Table<Provider>().ToListAsync();
        }

        public Task<int> DeleteProviderAsync(Provider provider)
        {
            return _database.DeleteAsync(provider);
        }

        // CRUD para ProductProvider
        public Task<int> SaveProductProviderAsync(ProductProvider productProvider)
        {
            if (productProvider.Id != 0)
            {
                return _database.UpdateAsync(productProvider);
            }
            else
            {
                return _database.InsertAsync(productProvider);
            }
        }

        public Task<List<ProductProvider>> GetProductProvidersAsync()
        {
            return _database.Table<ProductProvider>().ToListAsync();
        }

        public Task<int> DeleteProductProviderAsync(ProductProvider productProvider)
        {
            return _database.DeleteAsync(productProvider);
        }

    }
}
