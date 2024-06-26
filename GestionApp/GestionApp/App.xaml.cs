using GestionApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionApp.Views;

namespace GestionApp
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            Database = new DatabaseService();

            MainPage = new NavigationPage(new LoginPage());
        }


    }

}