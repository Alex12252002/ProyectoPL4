using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProductPage : ContentPage
    {
        private Product _product;

        public EditProductPage(Product product)
        {
            InitializeComponent();
            _product = product;
            LoadProductDetails();
            LoadProviders();
        }

        void LoadProductDetails()
        {
            NameEntry.Text = _product.Name;
            DescriptionEntry.Text = _product.Description;
            PriceEntry.Text = _product.Price.ToString();
            StockEntry.Text = _product.Stock.ToString();
        }

        async void LoadProviders()
        {
            var providers = await App.Database.GetProvidersAsync();
            var selectableProviders = providers.Select(p => new SelectableProvider { Provider = p }).ToList();
            ProviderListView.ItemsSource = selectableProviders;

            // Usar la conexión SQLite directamente desde DatabaseService
            var dbConnection = App.Database.GetDatabaseConnection();
            await _product.LoadProvidersAsync(dbConnection);

            foreach (var provider in _product.Providers)
            {
                var selectableProvider = selectableProviders.FirstOrDefault(sp => sp.Provider.Id == provider.Id);
                if (selectableProvider != null)
                {
                    selectableProvider.IsSelected = true;
                }
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _product.Name = NameEntry.Text;
            _product.Description = DescriptionEntry.Text;
            _product.Price = decimal.Parse(PriceEntry.Text);
            _product.Stock = int.Parse(StockEntry.Text);

            await App.Database.SaveProductAsync(_product);

            // Eliminar relaciones existentes
            var existingRelations = await App.Database.GetProductProvidersAsync();
            var productProviders = existingRelations.Where(pp => pp.ProductId == _product.Id).ToList();
            foreach (var relation in productProviders)
            {
                await App.Database.DeleteProductProviderAsync(relation);
            }

            // Guardar nuevas relaciones producto-proveedor
            foreach (SelectableProvider item in ProviderListView.ItemsSource)
            {
                if (item.IsSelected)
                {
                    var productProvider = new ProductProvider
                    {
                        ProductId = _product.Id,
                        ProviderId = item.Provider.Id
                    };
                    await App.Database.SaveProductProviderAsync(productProvider);
                }
            }

            await DisplayAlert("Éxito", "Producto actualizado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}