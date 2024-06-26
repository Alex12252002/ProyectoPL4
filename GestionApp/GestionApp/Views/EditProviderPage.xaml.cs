using GestionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProviderPage : ContentPage
    {
        private Provider _provider;

        public EditProviderPage(Provider provider)
        {
            InitializeComponent();
            _provider = provider;
            LoadProviderDetails();
        }

        void LoadProviderDetails()
        {
            NameEntry.Text = _provider.Name;
            ContactEntry.Text = _provider.Contact.ToString();
            EmailEntry.Text = _provider.Email;
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _provider.Name = NameEntry.Text;
            ContactEntry.Text = _provider.Contact.ToString();
            _provider.Email = EmailEntry.Text;

            await App.Database.SaveProviderAsync(_provider);

            await DisplayAlert("éxito", "Proveedor actualizado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}