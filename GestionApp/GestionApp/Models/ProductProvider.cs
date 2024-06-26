using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionApp.Models
{
    public class ProductProvider
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProviderId { get; set; }
    }
}
