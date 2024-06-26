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
    public partial class EmployeeListPage : ContentPage
    {
        public EmployeeListPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        
        async void LoadUsers()
        {
            var users = await App.Database.GetUsersAsync();
            AdminListView.ItemsSource = users.Where(u => u.Role == "Admin");
            EmployeeListView.ItemsSource = users.Where(u => u.Role == "Empleado");
        }


        async void OnAddEmployeeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateEmployeePage());
        }

        async void OnEditEmployeeButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.CommandParameter as User;
            if (user != null)
            {
                await Navigation.PushAsync(new EditEmployeePage(user));
            }
        }

        async void OnDeleteEmployeeButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.CommandParameter as User;
            if (user != null)
            {
                await App.Database.DeleteUserAsync(user);
                LoadUsers();
            }
        }
    }
}
