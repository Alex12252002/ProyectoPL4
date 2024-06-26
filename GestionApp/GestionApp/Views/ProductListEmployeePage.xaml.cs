using GestionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListEmployeePage : ContentPage
    {
        public ProductListEmployeePage()
        {
            InitializeComponent();
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
    }
}