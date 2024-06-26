using GestionApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IO;
using System.Linq;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountSettingsPage : ContentPage
    {
        private User _currentUser;

        public AccountSettingsPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadUserData();
        }

        private void LoadUserData()
        {
            CedulaOrRucEntry.Text = _currentUser.CedulaOrRuc.ToString();  // Convertir int a string
            UsernameEntry.Text = _currentUser.Username;
            EmailEntry.Text = _currentUser.Email;
            PasswordEntry.Text = _currentUser.Password;
            if (!string.IsNullOrEmpty(_currentUser.ProfileImage))
            {
                ProfileImageView.Source = _currentUser.ProfileImage;
            }
        }

        private async void OnChangeProfileImageClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var filePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                using (var fileStream = File.Create(filePath))
                {
                    stream.CopyTo(fileStream);
                }
                _currentUser.ProfileImage = filePath;
                ProfileImageView.Source = filePath;
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(CedulaOrRucEntry.Text) ||
                string.IsNullOrEmpty(EmailEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "OK");
                return;
            }

            if (!int.TryParse(CedulaOrRucEntry.Text, out int cedulaOrRuc))
            {
                await DisplayAlert("Error", "La cédula o RUC debe ser un número válido.", "OK");
                return;
            }

            if (!IsValidEmail(EmailEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, ingrese un correo electrónico válido.", "OK");
                return;
            }

            _currentUser.CedulaOrRuc = cedulaOrRuc;  // Convertir string a int
            _currentUser.Username = UsernameEntry.Text;
            _currentUser.Email = EmailEntry.Text;
            _currentUser.Password = PasswordEntry.Text;

            await App.Database.SaveUserAsync(_currentUser);
            await DisplayAlert("Éxito", "Cambios guardados exitosamente", "OK");
        }

        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            // Limpiar los datos del LoginPage
            var loginPage = (LoginPage)Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault(p => p is LoginPage);
            if (loginPage != null)
            {
                loginPage.ClearLoginFields();
            }

            await Navigation.PopToRootAsync();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
