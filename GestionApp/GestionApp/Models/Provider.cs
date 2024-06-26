using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Provider
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Email { get; set; }

        [Ignore]
        public List<Product> Products { get; set; }

        public async Task LoadProductsAsync(SQLiteAsyncConnection database)
        {
            var productProviders = await database.Table<ProductProvider>().Where(pp => pp.ProviderId == Id).ToListAsync();
            Products = new List<Product>();
            foreach (var pp in productProviders)
            {
                var product = await database.Table<Product>().Where(p => p.Id == pp.ProductId).FirstOrDefaultAsync();
                if (product != null)
                {
                    Products.Add(product);
                }
            }
        }
    }
}
