using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        [Ignore]
        public List<Provider> Providers { get; set; }

        public async Task LoadProvidersAsync(SQLiteAsyncConnection database)
        {
            var productProviders = await database.Table<ProductProvider>().Where(pp => pp.ProductId == Id).ToListAsync();
            Providers = new List<Provider>();
            foreach (var pp in productProviders)
            {
                var provider = await database.Table<Provider>().Where(p => p.Id == pp.ProviderId).FirstOrDefaultAsync();
                if (provider != null)
                {
                    Providers.Add(provider);
                }
            }
        }
    }
}
