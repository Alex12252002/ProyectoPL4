using GestionApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEmployeePage : ContentPage
    {
        private User _employee;

        public EditEmployeePage(User employee)
        {
            InitializeComponent();
            _employee = employee;
            LoadEmployeeDetails();
        }

        void LoadEmployeeDetails()
        {
            UsernameEntry.Text = _employee.Username;
            CedulaEntry.Text = _employee.CedulaOrRuc.ToString();
            EmailEntry.Text = _employee.Email;
            PasswordEntry.Text = _employee.Password;
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

            _employee.Username = UsernameEntry.Text;
            _employee.CedulaOrRuc = cedula;
            _employee.Email = EmailEntry.Text;
            _employee.Password = PasswordEntry.Text;

            await App.Database.SaveUserAsync(_employee);

            await DisplayAlert("Éxito", "Empleado actualizado exitosamente", "OK");
            await Navigation.PopAsync();
        }
    }
}
