using GestionApp.Models;
using GestionApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProducts();
        }

        async void LoadProducts()
        {
            var products = await App.Database.GetProductsAsync();
            ProductListView.ItemsSource = products;
        }

        async void OnAddProductButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateProductPage());
        }

        async void OnEditProductButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.CommandParameter as Product;
            if (product != null)
            {
                await Navigation.PushAsync(new EditProductPage(product));
            }
        }

        async void OnDeleteProductButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.CommandParameter as Product;
            if (product != null)
            {
                await App.Database.DeleteProductAsync(product);
                LoadProducts();
            }
        }
    }
}
