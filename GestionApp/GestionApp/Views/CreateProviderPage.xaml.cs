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
    public partial class CreateProviderPage : ContentPage
    {
        public CreateProviderPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(NameEntry.Text) || string.IsNullOrEmpty(ContactEntry.Text) || string.IsNullOrEmpty(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            // Validar que el campo Contact sea un número
            if (!int.TryParse(ContactEntry.Text, out int contact))
            {
                await DisplayAlert("Error", "El contacto debe ser un número.", "OK");
                return;
            }

            var provider = new Provider
            {
                Name = NameEntry.Text,
                Contact = contact,
                Email = EmailEntry.Text
            };

            await App.Database.SaveProviderAsync(provider);

            await DisplayAlert("Success", "Proveedor creado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
