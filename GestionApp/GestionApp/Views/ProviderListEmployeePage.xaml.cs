using GestionApp.Models;
using GestionApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProviderListEmployeePage : ContentPage
    {
        public ProviderListEmployeePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProviders();
        }

        async void LoadProviders()
        {
            var providers = await App.Database.GetProvidersAsync();
            ProviderListView.ItemsSource = providers;
        }

        async void OnAddProviderButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateProviderPage());
        }

        async void OnEditProviderButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var provider = button?.CommandParameter as Provider;
            if (provider != null)
            {
                await Navigation.PushAsync(new EditProviderPage(provider));
            }
        }
    }
}
