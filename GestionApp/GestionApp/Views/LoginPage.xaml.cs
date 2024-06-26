using GestionApp.Models;
using GestionApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void ClearLoginFields()
        {
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = await App.Database.GetUserAsync(EmailEntry.Text, PasswordEntry.Text);

            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    // Navegar a la página de administración
                    await Navigation.PushAsync(new MainPage(user));
                }
                else if (user.Role == "Empleado")
                {
                    // Navegar a la página de empleado
                    await Navigation.PushAsync(new EmpleadoMainPage(user));
                }
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }

        async void OnCreateAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        async void OnDeleteAllUsersButtonClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirmación", "¿Estás seguro de que deseas eliminar todos los usuarios?", "Sí", "No");
            if (confirm)
            {
                await App.Database.DeleteAllUsersAsync();
                await DisplayAlert("Éxito", "Todos los usuarios han sido eliminados", "OK");
            }
        }
    }
}
