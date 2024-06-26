using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;
namespace GestionApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public int CedulaOrRuc { get; set; }  // Asegúrate de que sea int
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string ProfileImage { get; set; }

    }
}
