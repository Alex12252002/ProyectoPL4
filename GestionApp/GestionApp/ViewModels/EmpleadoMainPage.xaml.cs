using GestionApp.Models;
using GestionApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpleadoMainPage : ContentPage
    {
        private User _currentUser;

        public EmpleadoMainPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            NavigationPage.SetHasBackButton(this, false); // Ocultar botón de navegación atrás
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await DisplayAlert("Salir", "¿Estás seguro de que deseas salir de la aplicación?", "Sí", "No");
                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true; // Indica que has manejado el evento del botón atrás
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountSettingsPage(_currentUser));
        }

        async void OnProductsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductListPage());
        }

        async void OnProvidersButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProviderListPage());
        }
    }
}
