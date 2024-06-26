using GestionApp.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProductPage : ContentPage
    {
        public CreateProductPage()
        {
            InitializeComponent();
            LoadProviders();
        }

        async void LoadProviders()
        {
            var providers = await App.Database.GetProvidersAsync();
            ProviderListView.ItemsSource = providers.Select(p => new SelectableProvider { Provider = p }).ToList();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                await DisplayAlert("Error", "El nombre del producto es obligatorio", "OK");
                return;
            }
            if (string.IsNullOrEmpty(DescriptionEntry.Text))
            {
                await DisplayAlert("Error", "La descripción del producto es obligatoria", "OK");
                return;
            }
            if (!decimal.TryParse(PriceEntry.Text, out decimal price) || price <= 0)
            {
                await DisplayAlert("Error", "El precio debe ser un número positivo", "OK");
                return;
            }
            if (!int.TryParse(StockEntry.Text, out int stock) || stock < 0)
            {
                await DisplayAlert("Error", "El stock debe ser un número no negativo", "OK");
                return;
            }

            // Validar que al menos un proveedor esté seleccionado
            var selectedProviders = ProviderListView.ItemsSource.Cast<SelectableProvider>().Where(p => p.IsSelected).ToList();
            if (!selectedProviders.Any())
            {
                await DisplayAlert("Error", "Debe seleccionar al menos un proveedor", "OK");
                return;
            }

            // Crear el producto
            var product = new Product
            {
                Name = NameEntry.Text,
                Description = DescriptionEntry.Text,
                Price = price,
                Stock = stock
            };

            await App.Database.SaveProductAsync(product);

            // Guardar las relaciones producto-proveedor
            foreach (var item in selectedProviders)
            {
                var productProvider = new ProductProvider
                {
                    ProductId = product.Id,
                    ProviderId = item.Provider.Id
                };
                await App.Database.SaveProductProviderAsync(productProvider);
            }

            await DisplayAlert("Éxito", "Producto creado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
