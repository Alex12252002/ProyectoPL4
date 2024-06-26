using GestionApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEmployeePage : ContentPage
    {
        public CreateEmployeePage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(CedulaEntry.Text) ||
                string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            if (!int.TryParse(CedulaEntry.Text, out int cedula))
            {
                await DisplayAlert("Error", "La cédula debe ser un número.", "OK");
                return;
            }

            if (RolePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Por favor, seleccione un rol.", "OK");
                return;
            }

            var user = new User
            {
                Username = UsernameEntry.Text,
                CedulaOrRuc = cedula,
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Role = RolePicker.SelectedItem.ToString() // Rol asignado basado en la selección del Picker
            };

            await App.Database.SaveUserAsync(user);

            await DisplayAlert("Éxito", "Empleado creado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
