using GestionApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(CedulaOrRucEntry.Text) ||
                string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            if (!int.TryParse(CedulaOrRucEntry.Text, out int cedulaOrRuc))
            {
                await DisplayAlert("Error", "La cédula o RUC debe ser un número.", "OK");
                return;
            }

            bool adminExists = await App.Database.AdminExistsAsync();

            if (adminExists)
            {
                await DisplayAlert("Error", "Ya existe un administrador. Solicita permiso al administrador actual para crear otro administrador.", "OK");
                return;
            }

            var user = new User
            {
                Username = UsernameEntry.Text,
                CedulaOrRuc = cedulaOrRuc,
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Role = "Admin"
            };

            await App.Database.SaveUserAsync(user);

            await DisplayAlert("Éxito", "Administrador registrado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
